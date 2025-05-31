using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Data
{
    /// <summary>
    /// Contexto do banco de dados para o catálogo de games
    /// </summary>
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Conjunto de dados para jogos
        /// </summary>
        public DbSet<Jogo> Jogos => Set<Jogo>();

        /// <summary>
        /// Conjunto de dados para categorias
        /// </summary>
        public DbSet<Categoria> Categorias => Set<Categoria>();

        /// <summary>
        /// Conjunto de dados para desenvolvedores
        /// </summary>
        public DbSet<Desenvolvedor> Desenvolvedores => Set<Desenvolvedor>();

        /// <summary>
        /// Configura o modelo de dados
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para Jogo
            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.DataLancamento).IsRequired();
                entity.Property(e => e.Preco).IsRequired().HasPrecision(10, 2);
                entity.Property(e => e.UrlImagem).HasMaxLength(255);
                entity.Property(e => e.ClassificacaoEtaria).HasMaxLength(10);
                entity.Property(e => e.Plataformas).HasMaxLength(255);

                // Relacionamentos
                entity.HasOne(e => e.Categoria)
                      .WithMany(c => c.Jogos)
                      .HasForeignKey(e => e.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Desenvolvedor)
                      .WithMany(d => d.Jogos)
                      .HasForeignKey(e => e.DesenvolvedorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descricao).HasMaxLength(200);
            });

            // Configuração para Desenvolvedor
            modelBuilder.Entity<Desenvolvedor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Pais).HasMaxLength(50);
                entity.Property(e => e.Website).HasMaxLength(255);
            });

            // Dados iniciais
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Popula o banco de dados com dados iniciais
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Categorias
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Ação", Descricao = "Jogos de ação e aventura" },
                new Categoria { Id = 2, Nome = "Estratégia", Descricao = "Jogos de estratégia e táticas" },
                new Categoria { Id = 3, Nome = "RPG", Descricao = "Jogos de interpretação de papéis" },
                new Categoria { Id = 4, Nome = "Esportes", Descricao = "Jogos de esportes e competições" },
                new Categoria { Id = 5, Nome = "Simulação", Descricao = "Jogos de simulação de atividades reais" }
            );

            // Desenvolvedores
            modelBuilder.Entity<Desenvolvedor>().HasData(
                new Desenvolvedor { Id = 1, Nome = "Rockstar Games", Pais = "Estados Unidos", Website = "https://www.rockstargames.com", AnoFundacao = 1998 },
                new Desenvolvedor { Id = 2, Nome = "Electronic Arts", Pais = "Estados Unidos", Website = "https://www.ea.com", AnoFundacao = 1982 },
                new Desenvolvedor { Id = 3, Nome = "CD Projekt Red", Pais = "Polônia", Website = "https://www.cdprojektred.com", AnoFundacao = 1994 }
            );

            // Jogos
            modelBuilder.Entity<Jogo>().HasData(
                new Jogo
                {
                    Id = 1,
                    Titulo = "Grand Theft Auto V",
                    Descricao = "Um jogo de ação-aventura em mundo aberto desenvolvido pela Rockstar North",
                    DataLancamento = new DateTime(2013, 9, 17),
                    Preco = 99.90M,
                    CategoriaId = 1,
                    DesenvolvedorId = 1,
                    ClassificacaoEtaria = "18+",
                    Plataformas = "PC, PlayStation, Xbox"
                },
                new Jogo
                {
                    Id = 2,
                    Titulo = "FIFA 23",
                    Descricao = "Jogo de simulação de futebol desenvolvido pela EA Sports",
                    DataLancamento = new DateTime(2022, 9, 30),
                    Preco = 199.90M,
                    CategoriaId = 4,
                    DesenvolvedorId = 2,
                    ClassificacaoEtaria = "Livre",
                    Plataformas = "PC, PlayStation, Xbox, Nintendo Switch"
                },
                new Jogo
                {
                    Id = 3,
                    Titulo = "The Witcher 3: Wild Hunt",
                    Descricao = "Um RPG de mundo aberto baseado na série de livros The Witcher",
                    DataLancamento = new DateTime(2015, 5, 19),
                    Preco = 79.90M,
                    CategoriaId = 3,
                    DesenvolvedorId = 3,
                    ClassificacaoEtaria = "18+",
                    Plataformas = "PC, PlayStation, Xbox, Nintendo Switch"
                }
            );
        }
    }
}
