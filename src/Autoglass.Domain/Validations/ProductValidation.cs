using Autoglass.Domain.Entities;
using FluentValidation;

namespace Autoglass.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .MaximumLength(100).WithMessage("A descrição do produto deve ter no máximo 100 caracteres.");

            RuleFor(p => p.ManufacturingDate)
                .NotEmpty().WithMessage("A data de fabricação do produto é obrigatória.")
                .LessThan(p => p.ExpirationDate).WithMessage("A data de fabricação não pode ser maior ou igual à data de validade.");
        }
    }
}
