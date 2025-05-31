using CatalogoGames.API.Modelos;
using FluentValidation;

namespace CatalogoGames.API.Validacoes
{
    
    /// Validador para a entidade Categoria
    
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        
        /// Configura as regras de validação para Categoria
        
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório")
                .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres");

            RuleFor(c => c.Descricao)
                .MaximumLength(200).WithMessage("A descrição não pode ter mais de 200 caracteres");
        }
    }
}