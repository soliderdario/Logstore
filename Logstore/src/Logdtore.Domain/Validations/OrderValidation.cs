using FluentValidation;
using Logdtore.Domain.View;
using Logstore.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Logstore.Domain.Validations
{
    public class OrderValidation : AbstractValidator<OrderView>
    {
        public OrderValidation()
        {
            RuleFor(c => HasItem(c.Items))
                .Equal(true)
                .WithMessage("Um pedido só pode ter no máximo 10 items");

            RuleFor(c => HasFlavor(c.Items))
                .Equal(true)
                .WithMessage("Um dos items do pedido possuem mais de dois sabores ou nenhum sabor");
        }

        private bool HasItem(IEnumerable<OrderItemView> orderItem)
        {
            var count = orderItem.ToList().Count();
            return (count >= 1 && count <=10);
        }

        private bool HasFlavor(IEnumerable<OrderItemView> orderItem)
        {            
            foreach (var current in orderItem)
            {
                var count = current.Flavors.Count();
                if (count ==0 || count > 2)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
