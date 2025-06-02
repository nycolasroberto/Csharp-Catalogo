using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{

    /// Interface para operações de repositório de jogos
    
    public interface IJogoRepositorio
    {
    
        
        //Lista de jogos
        /// Obtém todos os jogos
        Task<IEnumerable<Jogo>> ObterTodosAsync();

    
        /// Obtém um jogo pelo ID
        
        /// <param name="id">ID do jogo</param>
        //Jogo encontrado ou null
        Task<Jogo?> ObterPorIdAsync(int id);

    
        /// Adiciona um novo jogo
        
        /// <param name="jogo">Jogo a ser adicionado</param>
        Task AdicionarAsync(Jogo jogo);

    
        /// Atualiza um jogo existente
        
        /// <param name="jogo">Jogo com os dados atualizados</param>
        Task AtualizarAsync(Jogo jogo);

    
        /// Remove um jogo pelo ID
        
        /// <param name="id">ID do jogo a ser removido</param>
        Task RemoverAsync(int id);
    }
}