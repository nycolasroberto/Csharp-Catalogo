using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{

    // Interface para operações de repositório de jogos
    
    public interface IJogoRepositorio
    {
    
        
        //Lista de jogos
        // Obtém todos os jogos
        Task<IEnumerable<Jogo>> ObterTodosAsync();

    
        // Obtém um jogo pelo ID
        
        //ID do jogo
        //Jogo encontrado ou null
        Task<Jogo?> ObterPorIdAsync(int id);

    
        // Adiciona um novo jogo
        
        //Jogo a ser adicionado
        Task AdicionarAsync(Jogo jogo);

    
        // Atualiza um jogo existente
        
        //Jogo com os dados atualizados
        Task AtualizarAsync(Jogo jogo);

    
        // Remove um jogo pelo ID
        
        //ID do jogo a ser removido
        Task RemoverAsync(int id);
    }
}