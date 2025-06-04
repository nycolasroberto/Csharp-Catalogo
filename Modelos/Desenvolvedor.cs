using System.ComponentModel.DataAnnotations;

namespace CatalogoGames.API.Modelos
{
    // Representa um desenvolvedor de jogos no catálogo
    public class Desenvolvedor
    {
        // Identificador único do desenvolvedor
        public int Id { get; set; }

        // Nome do desenvolvedor
        public string Nome { get; set; } = string.Empty;

        // País de origem do desenvolvedor
        public string? Pais { get; set; }

        // Site oficial do desenvolvedor
        public string? Website { get; set; }

        // Ano de fundação do desenvolvedor
        public int? AnoFundacao { get; set; }

        // Jogos desenvolvidos por este desenvolvedor
        public ICollection<Jogo>? Jogos { get; set; }
    }
}