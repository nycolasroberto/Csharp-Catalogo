using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using CatalogoGames.API.Repositorios;
using CatalogoGames.API.Validacoes;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catálogo de Games API",
        Version = "v1",
        Description = "API para gerenciamento de um catálogo de games"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configura o contexto do banco de dados SQLite
builder.Services.AddDbContext<CatalogoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona serviços de validação
builder.Services.AddScoped<IValidator<Jogo>, JogoValidator>();
builder.Services.AddScoped<IValidator<Categoria>, CategoriaValidator>();
builder.Services.AddScoped<IValidator<Desenvolvedor>, DesenvolvedorValidator>();

// Adiciona repositórios
builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IDesenvolvedorRepositorio, DesenvolvedorRepositorio>();

// Configura JSON para ignorar referências cíclicas
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Adiciona CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catálogo de Games API v1"));
    
    // Cria o banco de dados e aplica as migrações
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogoDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Configura os endpoints da API
app.MapJogosEndpoints();
app.MapCategoriasEndpoints();
app.MapDesenvolvedoresEndpoints();

app.Run();

// Extensão para configurar os endpoints de Jogos
public static class JogosEndpoints
{
    public static WebApplication MapJogosEndpoints(this WebApplication app)
    {
        var jogosGroup = app.MapGroup("/api/jogos").WithTags("Jogos");

        // Obter todos os jogos
        jogosGroup.MapGet("/", async (IJogoRepositorio repositorio) =>
        {
            var jogos = await repositorio.ObterTodosAsync();
            return Results.Ok(jogos);
        })
        .WithName("ObterTodosJogos")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém todos os jogos";
            operation.Description = "Retorna uma lista com todos os jogos cadastrados";
            return operation;
        });

        // Obter jogo por ID
        jogosGroup.MapGet("/{id}", async (int id, IJogoRepositorio repositorio) =>
        {
            var jogo = await repositorio.ObterPorIdAsync(id);
            if (jogo == null)
                return Results.NotFound();

            return Results.Ok(jogo);
        })
        .WithName("ObterJogoPorId")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém um jogo pelo ID";
            operation.Description = "Retorna um jogo específico com base no ID fornecido";
            return operation;
        });

        // Criar novo jogo
        jogosGroup.MapPost("/", async (Jogo jogo, IJogoRepositorio repositorio, IValidator<Jogo> validator) =>
        {
            var validationResult = await validator.ValidateAsync(jogo);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AdicionarAsync(jogo);
            return Results.Created($"/api/jogos/{jogo.Id}", jogo);
        })
        .WithName("CriarJogo")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Cria um novo jogo";
            operation.Description = "Adiciona um novo jogo ao catálogo";
            return operation;
        });

        // Atualizar jogo
        jogosGroup.MapPut("/{id}", async (int id, Jogo jogoAtualizado, IJogoRepositorio repositorio, IValidator<Jogo> validator) =>
        {
            var jogo = await repositorio.ObterPorIdAsync(id);
            if (jogo == null)
                return Results.NotFound();

            // Atualiza as propriedades
            jogoAtualizado.Id = id;
            
            var validationResult = await validator.ValidateAsync(jogoAtualizado);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AtualizarAsync(jogoAtualizado);
            return Results.NoContent();
        })
        .WithName("AtualizarJogo")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Atualiza um jogo";
            operation.Description = "Atualiza os dados de um jogo existente com base no ID";
            return operation;
        });

        // Excluir jogo
        jogosGroup.MapDelete("/{id}", async (int id, IJogoRepositorio repositorio) =>
        {
            var jogo = await repositorio.ObterPorIdAsync(id);
            if (jogo == null)
                return Results.NotFound();

            await repositorio.RemoverAsync(id);
            return Results.NoContent();
        })
        .WithName("ExcluirJogo")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Exclui um jogo";
            operation.Description = "Remove um jogo do catálogo com base no ID";
            return operation;
        });

        return app;
    }
}

// Extensão para configurar os endpoints de Categorias
public static class CategoriasEndpoints
{
    public static WebApplication MapCategoriasEndpoints(this WebApplication app)
    {
        var categoriasGroup = app.MapGroup("/api/categorias").WithTags("Categorias");

        // Obter todas as categorias
        categoriasGroup.MapGet("/", async (ICategoriaRepositorio repositorio) =>
        {
            var categorias = await repositorio.ObterTodasAsync();
            return Results.Ok(categorias);
        })
        .WithName("ObterTodasCategorias")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém todas as categorias";
            operation.Description = "Retorna uma lista com todas as categorias cadastradas";
            return operation;
        });

        // Obter categoria por ID
        categoriasGroup.MapGet("/{id}", async (int id, ICategoriaRepositorio repositorio) =>
        {
            var categoria = await repositorio.ObterPorIdAsync(id);
            if (categoria == null)
                return Results.NotFound();

            return Results.Ok(categoria);
        })
        .WithName("ObterCategoriaPorId")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém uma categoria pelo ID";
            operation.Description = "Retorna uma categoria específica com base no ID fornecido";
            return operation;
        });

        // Criar nova categoria
        categoriasGroup.MapPost("/", async (Categoria categoria, ICategoriaRepositorio repositorio, IValidator<Categoria> validator) =>
        {
            var validationResult = await validator.ValidateAsync(categoria);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AdicionarAsync(categoria);
            return Results.Created($"/api/categorias/{categoria.Id}", categoria);
        })
        .WithName("CriarCategoria")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Cria uma nova categoria";
            operation.Description = "Adiciona uma nova categoria ao catálogo";
            return operation;
        });

        // Atualizar categoria
        categoriasGroup.MapPut("/{id}", async (int id, Categoria categoriaAtualizada, ICategoriaRepositorio repositorio, IValidator<Categoria> validator) =>
        {
            var categoria = await repositorio.ObterPorIdAsync(id);
            if (categoria == null)
                return Results.NotFound();

            // Atualiza as propriedades
            categoriaAtualizada.Id = id;
            
            var validationResult = await validator.ValidateAsync(categoriaAtualizada);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AtualizarAsync(categoriaAtualizada);
            return Results.NoContent();
        })
        .WithName("AtualizarCategoria")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Atualiza uma categoria";
            operation.Description = "Atualiza os dados de uma categoria existente com base no ID";
            return operation;
        });

        // Excluir categoria
        categoriasGroup.MapDelete("/{id}", async (int id, ICategoriaRepositorio repositorio) =>
        {
            var categoria = await repositorio.ObterPorIdAsync(id);
            if (categoria == null)
                return Results.NotFound();

            await repositorio.RemoverAsync(id);
            return Results.NoContent();
        })
        .WithName("ExcluirCategoria")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Exclui uma categoria";
            operation.Description = "Remove uma categoria do catálogo com base no ID";
            return operation;
        });

        return app;
    }
}

// Extensão para configurar os endpoints de Desenvolvedores
public static class DesenvolvedoresEndpoints
{
    public static WebApplication MapDesenvolvedoresEndpoints(this WebApplication app)
    {
        var desenvolvedoresGroup = app.MapGroup("/api/desenvolvedores").WithTags("Desenvolvedores");

        // Obter todos os desenvolvedores
        desenvolvedoresGroup.MapGet("/", async (IDesenvolvedorRepositorio repositorio) =>
        {
            var desenvolvedores = await repositorio.ObterTodosAsync();
            return Results.Ok(desenvolvedores);
        })
        .WithName("ObterTodosDesenvolvedores")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém todos os desenvolvedores";
            operation.Description = "Retorna uma lista com todos os desenvolvedores cadastrados";
            return operation;
        });

        // Obter desenvolvedor por ID
        desenvolvedoresGroup.MapGet("/{id}", async (int id, IDesenvolvedorRepositorio repositorio) =>
        {
            var desenvolvedor = await repositorio.ObterPorIdAsync(id);
            if (desenvolvedor == null)
                return Results.NotFound();

            return Results.Ok(desenvolvedor);
        })
        .WithName("ObterDesenvolvedorPorId")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Obtém um desenvolvedor pelo ID";
            operation.Description = "Retorna um desenvolvedor específico com base no ID fornecido";
            return operation;
        });

        // Criar novo desenvolvedor
        desenvolvedoresGroup.MapPost("/", async (Desenvolvedor desenvolvedor, IDesenvolvedorRepositorio repositorio, IValidator<Desenvolvedor> validator) =>
        {
            var validationResult = await validator.ValidateAsync(desenvolvedor);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AdicionarAsync(desenvolvedor);
            return Results.Created($"/api/desenvolvedores/{desenvolvedor.Id}", desenvolvedor);
        })
        .WithName("CriarDesenvolvedor")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Cria um novo desenvolvedor";
            operation.Description = "Adiciona um novo desenvolvedor ao catálogo";
            return operation;
        });

        // Atualizar desenvolvedor
        desenvolvedoresGroup.MapPut("/{id}", async (int id, Desenvolvedor desenvolvedorAtualizado, IDesenvolvedorRepositorio repositorio, IValidator<Desenvolvedor> validator) =>
        {
            var desenvolvedor = await repositorio.ObterPorIdAsync(id);
            if (desenvolvedor == null)
                return Results.NotFound();

            // Atualiza as propriedades
            desenvolvedorAtualizado.Id = id;
            
            var validationResult = await validator.ValidateAsync(desenvolvedorAtualizado);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await repositorio.AtualizarAsync(desenvolvedorAtualizado);
            return Results.NoContent();
        })
        .WithName("AtualizarDesenvolvedor")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Atualiza um desenvolvedor";
            operation.Description = "Atualiza os dados de um desenvolvedor existente com base no ID";
            return operation;
        });

        // Excluir desenvolvedor
        desenvolvedoresGroup.MapDelete("/{id}", async (int id, IDesenvolvedorRepositorio repositorio) =>
        {
            var desenvolvedor = await repositorio.ObterPorIdAsync(id);
            if (desenvolvedor == null)
                return Results.NotFound();

            await repositorio.RemoverAsync(id);
            return Results.NoContent();
        })
        .WithName("ExcluirDesenvolvedor")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Exclui um desenvolvedor";
            operation.Description = "Remove um desenvolvedor do catálogo com base no ID";
            return operation;
        });

        return app;
    }
}