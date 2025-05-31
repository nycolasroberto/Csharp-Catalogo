using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogoGames.API.Modelos
{
    // Representa um jogo no catálogo
    public class Jogo
    {
        // Identificador único do jogo
        public int Id { get; set; }

        // Título do jogo
        public string Titulo { get; set; } = string.Empty;

        // Descrição do jogo
        public string Descricao { get; set; } = string.Empty;

        // Data de lançamento do jogo
        public DateTime DataLancamento { get; set; }

        // URL da imagem de capa do jogo
        public string? UrlImagem { get; set; }

        // Preço do jogo
        public decimal Preco { get; set; }

        // Identificador da categoria do jogo
        public int CategoriaId { get; set; }

        // Categoria do jogo
        public Categoria? Categoria { get; set; }

        // Identificador do desenvolvedor do jogo
        public int DesenvolvedorId { get; set; }

        // Desenvolvedor do jogo
        public Desenvolvedor? Desenvolvedor { get; set; }

        // Classificação etária do jogo
        public string? ClassificacaoEtaria { get; set; }

        // Plataformas disponíveis para o jogo
        public string? Plataformas { get; set; }
    }
}