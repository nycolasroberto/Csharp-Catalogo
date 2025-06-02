using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{
    
    /// Interface para operações de repositório de categorias
    
    public interface ICategoriaRepositorio
    {
        
        /// Obtém todas as categorias
        
        /// <returns>Lista de categorias</returns>
        Task<IEnumerable<Categoria>> ObterTodasAsync();

        
        /// Obtém uma categoria pelo ID
        
        /// <param name="id">ID da categoria</param>
        /// <returns>Categoria encontrada ou null</returns>
        Task<Categoria?> ObterPorIdAsync(int id);

        
        /// Adiciona uma nova categoria
        
        /// <param name="categoria">Categoria a ser adicionada</param>
        Task AdicionarAsync(Categoria categoria);

        
        /// Atualiza uma categoria existente
        
        /// <param name="categoria">Categoria com os dados atualizados</param>
        Task AtualizarAsync(Categoria categoria);

        
        /// Remove uma categoria pelo ID
        
        /// <param name="id">ID da categoria a ser removida</param>
        Task RemoverAsync(int id);
    }
}