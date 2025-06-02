using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Repositorios
{
    
    /// Implementação do repositório de jogos
    
    public class JogoRepositorio : IJogoRepositorio
    {
        private readonly CatalogoDbContext _context;

        
        /// Construtor
        
        /// <param name="context">Contexto do banco de dados</param>
        public JogoRepositorio(CatalogoDbContext context)
        {
            _context = context;
        }

        
        /// Obtém todos os jogos
        
        /// <returns>Lista de jogos</returns>
        public async Task<IEnumerable<Jogo>> ObterTodosAsync()
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.Desenvolvedor)
                .ToListAsync();
        }

        
        /// Obtém um jogo pelo ID
        
        /// <param name="id">ID do jogo</param>
        /// <returns>Jogo encontrado ou null</returns>
        public async Task<Jogo?> ObterPorIdAsync(int id)
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.Desenvolvedor)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        
        /// Adiciona um novo jogo
        
        /// <param name="jogo">Jogo a ser adicionado</param>
        public async Task AdicionarAsync(Jogo jogo)
        {
            await _context.Jogos.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }

        
        /// Atualiza um jogo existente
        
        /// <param name="jogo">Jogo com os dados atualizados</param>
        public async Task AtualizarAsync(Jogo jogo)
        {
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
        }

        
        /// Remove um jogo pelo ID
        
        /// <param name="id">ID do jogo a ser removido</param>
        public async Task RemoverAsync(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo != null)
            {
                _context.Jogos.Remove(jogo);
                await _context.SaveChangesAsync();
            }
        }
    }
}