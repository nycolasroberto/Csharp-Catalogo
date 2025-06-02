using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{
    
    /// Interface para operações de repositório de desenvolvedores
    
    public interface IDesenvolvedorRepositorio
    {
        
        /// Obtém todos os desenvolvedores
        
        /// <returns>Lista de desenvolvedores</returns>
        Task<IEnumerable<Desenvolvedor>> ObterTodosAsync();

        
        /// Obtém um desenvolvedor pelo ID
        
        /// <param name="id">ID do desenvolvedor</param>
        /// <returns>Desenvolvedor encontrado ou null</returns>
        Task<Desenvolvedor?> ObterPorIdAsync(int id);

        
        /// Adiciona um novo desenvolvedor
        
        /// <param name="desenvolvedor">Desenvolvedor a ser adicionado</param>
        Task AdicionarAsync(Desenvolvedor desenvolvedor);

        
        /// Atualiza um desenvolvedor existente
        
        /// <param name="desenvolvedor">Desenvolvedor com os dados atualizados</param>
        Task AtualizarAsync(Desenvolvedor desenvolvedor);

        
        /// Remove um desenvolvedor pelo ID
        
        /// <param name="id">ID do desenvolvedor a ser removido</param>
        Task RemoverAsync(int id);
    }
}