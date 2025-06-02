using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Repositorios
{
    
    /// Implementação do repositório de desenvolvedores
    
    public class DesenvolvedorRepositorio : IDesenvolvedorRepositorio
    {
        private readonly CatalogoDbContext _context;

        
        
        /// <param name="context">Contexto do banco de dados</param>
        public DesenvolvedorRepositorio(CatalogoDbContext context)
        {
            _context = context;
        }

        
        /// Obtém todos os desenvolvedores
        
        /// <returns>Lista de desenvolvedores</returns>
        public async Task<IEnumerable<Desenvolvedor>> ObterTodosAsync()
        {
            return await _context.Desenvolvedores.ToListAsync();
        }
        
        /// Obtém um desenvolvedor pelo ID
        
        /// <param name="id">ID do desenvolvedor</param>
        /// <returns>Desenvolvedor encontrado ou null</returns>
        public async Task<Desenvolvedor?> ObterPorIdAsync(int id)
        {
            return await _context.Desenvolvedores.FindAsync(id);
        }

        
        /// Adiciona um novo desenvolvedor
        
        /// <param name="desenvolvedor">Desenvolvedor a ser adicionado</param>
        public async Task AdicionarAsync(Desenvolvedor desenvolvedor)
        {
            await _context.Desenvolvedores.AddAsync(desenvolvedor);
            await _context.SaveChangesAsync();
        }

        
        /// Atualiza um desenvolvedor existente
        
        /// <param name="desenvolvedor">Desenvolvedor com os dados atualizados</param>
        public async Task AtualizarAsync(Desenvolvedor desenvolvedor)
        {
            _context.Desenvolvedores.Update(desenvolvedor);
            await _context.SaveChangesAsync();
        }

        
        /// Remove um desenvolvedor pelo ID
        
        /// <param name="id">ID do desenvolvedor a ser removido</param>
        public async Task RemoverAsync(int id)
        {
            var desenvolvedor = await _context.Desenvolvedores.FindAsync(id);
            if (desenvolvedor != null)
            {
                _context.Desenvolvedores.Remove(desenvolvedor);
                await _context.SaveChangesAsync();
            }
        }
    }
}