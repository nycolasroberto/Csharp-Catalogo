using CatalogoGames.API.Data;
using CatalogoGames.API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CatalogoGames.API.Repositorios
{
    
    // Implementação do repositório de jogos
    
    public class JogoRepositorio : IJogoRepositorio
    {
        private readonly CatalogoDbContext _context;

                
        //Contexto do banco de dados
        public JogoRepositorio(CatalogoDbContext context)
        {
            _context = context;
        }

        
        // Obtém todos os jogos
        
        //Lista de jogos</returns>
        public async Task<IEnumerable<Jogo>> ObterTodosAsync()
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.Desenvolvedor)
                .ToListAsync();
        }

        
        // Obtém um jogo pelo ID
        
        //ID do jogo
        //Jogo encontrado ou null</returns>
        public async Task<Jogo?> ObterPorIdAsync(int id)
        {
            return await _context.Jogos
                .Include(j => j.Categoria)
                .Include(j => j.Desenvolvedor)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        
        // Adiciona um novo jogo
        
        //Jogo a ser adicionado
        public async Task AdicionarAsync(Jogo jogo)
        {
            await _context.Jogos.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }

        
        // Atualiza um jogo existente
        
        //Jogo com os dados atualizados
        public async Task AtualizarAsync(Jogo jogo)
        {
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
        }

        
        // Remove um jogo pelo ID
        
        //ID do jogo a ser removido
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