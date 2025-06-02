using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Repositorios
{
    
    /// Implementação do repositório de categorias
    
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly CatalogoDbContext _context;

        
        /// Construtor
        
        /// <param name="context">Contexto do banco de dados</param>
        public CategoriaRepositorio(CatalogoDbContext context)
        {
            _context = context;
        }

        
        /// Obtém todas as categorias
        
        /// <returns>Lista de categorias</returns>
        public async Task<IEnumerable<Categoria>> ObterTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        
        /// Obtém uma categoria pelo ID
        
        /// <param name="id">ID da categoria</param>
        /// <returns>Categoria encontrada ou null</returns>
        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        
        /// Adiciona uma nova categoria
        
        /// <param name="categoria">Categoria a ser adicionada</param>
        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        
        /// Atualiza uma categoria existente
        
        /// <param name="categoria">Categoria com os dados atualizados</param>
        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        
        /// Remove uma categoria pelo ID
        
        /// <param name="id">ID da categoria a ser removida</param>
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