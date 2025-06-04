using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Repositorios
{
    
    // Implementação do repositório de categorias
    
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly CatalogoDbContext _context;

        
        
        //Contexto do banco de dados
        public CategoriaRepositorio(CatalogoDbContext context)
        {
            _context = context;
        }

        
        // Obtém todas as categorias
        
        //Lista de categorias
        public async Task<IEnumerable<Categoria>> ObterTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        
        // Obtém uma categoria pelo ID
        
        // da categoria
        //Categoria encontrada ou null
        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        
        // Adiciona uma nova categoria
        
        //Categoria a ser adicionada
        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        
        // Atualiza uma categoria existente
        
        //Categoria com os dados atualizados
        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        
        // Remove uma categoria pelo ID
        
        //ID da categoria a ser removida
        public async Task RemoverAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}