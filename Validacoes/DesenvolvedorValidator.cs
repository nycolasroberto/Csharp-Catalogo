using CatalogoGames.API.Modelos;
using FluentValidation;

namespace CatalogoGames.API.Validacoes
{
    
    /// Validador para a entidade Desenvolvedor
    
    public class DesenvolvedorValidator : AbstractValidator<Desenvolvedor>
    {
        
        /// Configura as regras de validação para Desenvolvedor
        
        public DesenvolvedorValidator()
        {
            RuleFor(d => d.Nome)
                .NotEmpty().WithMessage("O nome do desenvolvedor é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(d => d.Pais)
                .MaximumLength(50).WithMessage("O país não pode ter mais de 50 caracteres");

            RuleFor(d => d.Website)
                .MaximumLength(255).WithMessage("O website não pode ter mais de 255 caracteres")
                .Must(uri => uri == null || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("O website deve ser uma URL válida");

            RuleFor(d => d.AnoFundacao)
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano de fundação não pode ser no futuro");
        }
    }
}