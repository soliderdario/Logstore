using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Logdtore.Domain.View;

namespace Logstore.Domain.Validations
{
    public class OrderValidation : AbstractValidator<OrderNoCustomerView>
    {
        public OrderValidation()
        {
            RuleFor(c => AggregateValidation.HasItem(c.Items))
                .Equal(true)
                .WithMessage("Um pedido só pode ter no máximo 10 items");

            RuleFor(c => AggregateValidation.HasFlavor(c.Items))
                .Equal(true)
                .WithMessage("Um dos items do pedido possuem mais de dois sabores ou nenhum sabor");
        }        
    }

    public class OrderYesCustomerValidation : AbstractValidator<OrderYesCustomerView>
    {
        public OrderYesCustomerValidation()
        {
            RuleFor(c => AggregateValidation.HasItem(c.Items))
                .Equal(true)
                .WithMessage("Um pedido só pode ter no máximo 10 items");

            RuleFor(c => AggregateValidation.HasFlavor(c.Items))
                .Equal(true)
                .WithMessage("Um dos items do pedido possuem mais de dois sabores ou nenhum sabor");
        }       
    }

    internal static class AggregateValidation
    {
        public static bool HasItem(IEnumerable<OrderItemView> orderItem)
        {
            var count = orderItem.ToList().Count();
            return (count >= 1 && count <= 10);
        }

        public static bool HasFlavor(IEnumerable<OrderItemView> orderItem)
        {
            foreach (var current in orderItem)
            {
                var count = current.Flavors.Count();
                if (count == 0 || count > 2)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
