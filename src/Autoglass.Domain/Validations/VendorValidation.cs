using Autoglass.Domain.Entities;
using FluentValidation;

namespace Autoglass.Domain.Validations
{
    public class VendorValidation : AbstractValidator<Supplier>
    {
        public VendorValidation()
        {
            RuleFor(f => f.Description)
                .NotEmpty().WithMessage("A descrição do fornecedor é obrigatória.")
                .MaximumLength(100).WithMessage("A descrição do fornecedor deve ter no máximo 100 caracteres.");

            RuleFor(f => f.CompanyDocument)
                .NotEmpty().WithMessage("O CNPJ do fornecedor é obrigatório.")
                .Length(14).WithMessage("O CNPJ do fornecedor deve ter 14 caracteres.");
        }
    }
}
