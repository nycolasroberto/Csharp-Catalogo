using System.ComponentModel.DataAnnotations;

namespace CatalogoGames.API.Modelos
{
    // Representa uma categoria de jogos no catálogo
    public class Categoria
    {
        // Identificador único da categoria
        public int Id { get; set; }

        // Nome da categoria
        public string Nome { get; set; } = string.Empty;

        // Descrição da categoria
        public string? Descricao { get; set; }

        // Jogos associados a esta categoria
        public ICollection<Jogo>? Jogos { get; set; }
    }
}