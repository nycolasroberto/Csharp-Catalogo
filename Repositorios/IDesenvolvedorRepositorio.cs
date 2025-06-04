using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{
    
    // Interface para operações de repositório de desenvolvedores
    
    public interface IDesenvolvedorRepositorio
    {
        
        // Obtém todos os desenvolvedores
        
        //Lista de desenvolvedores
        Task<IEnumerable<Desenvolvedor>> ObterTodosAsync();

        
        // Obtém um desenvolvedor pelo ID
        
        //ID do desenvolvedor
        //Desenvolvedor encontrado ou null
        Task<Desenvolvedor?> ObterPorIdAsync(int id);

        
        // Adiciona um novo desenvolvedor
        
        //Desenvolvedor a ser adicionado
        Task AdicionarAsync(Desenvolvedor desenvolvedor);

        
        // Atualiza um desenvolvedor existente
        
        //Desenvolvedor com os dados atualizados
        Task AtualizarAsync(Desenvolvedor desenvolvedor);

        
        // Remove um desenvolvedor pelo ID
        
        //ID do desenvolvedor a ser removido
        Task RemoverAsync(int id);
    }
}