using CatalogoGames.API.Modelos;
using FluentValidation;

namespace CatalogoGames.API.Validacoes
{
    
    /// Validador para a entidade Jogo
    
    public class JogoValidator : AbstractValidator<Jogo>
    {
        
        /// Configura as regras de validação para Jogo
        
        public JogoValidator()
        {
            RuleFor(j => j.Titulo)
                .NotEmpty().WithMessage("O título do jogo é obrigatório")
                .MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres");

            RuleFor(j => j.Descricao)
                .NotEmpty().WithMessage("A descrição do jogo é obrigatória")
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres");

            RuleFor(j => j.DataLancamento)
                .NotEmpty().WithMessage("A data de lançamento é obrigatória")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de lançamento não pode ser no futuro");

            RuleFor(j => j.Preco)
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo");

            RuleFor(j => j.CategoriaId)
                .NotEmpty().WithMessage("A categoria do jogo é obrigatória");

            RuleFor(j => j.DesenvolvedorId)
                .NotEmpty().WithMessage("O desenvolvedor do jogo é obrigatório");
        }
    }
}