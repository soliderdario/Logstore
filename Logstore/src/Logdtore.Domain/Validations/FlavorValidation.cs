using FluentValidation;
using Logdtore.Domain.Model;

namespace Logstore.Domain.Validations
{
    public class FlavorValidation : AbstractValidator<Flavor>
    {
        public FlavorValidation()
        {
            RuleFor(c => c.Price > 0)
                .Equal(true)
                .WithMessage("O campo preço precisa ser preenchido");
        }
    }
}
