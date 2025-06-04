using CatalogoGames.API.Modelos;

namespace CatalogoGames.API.Repositorios
{
    
    // Interface para operações de repositório de categorias
    
    public interface ICategoriaRepositorio
    {
        
        // Obtém todas as categorias
        
        //Lista de categorias
        Task<IEnumerable<Categoria>> ObterTodasAsync();

        
        // Obtém uma categoria pelo ID
        
        //ID da categoria
        //Categoria encontrada ou null
        Task<Categoria?> ObterPorIdAsync(int id);

        
        // Adiciona uma nova categoria
        
        //Categoria a ser adicionada
        Task AdicionarAsync(Categoria categoria);

        
        // Atualiza uma categoria existente
        
        //Categoria com os dados atualizados
        Task AtualizarAsync(Categoria categoria);

        
        // Remove uma categoria pelo ID
        
        //ID da categoria a ser removida
        Task RemoverAsync(int id);
    }
}