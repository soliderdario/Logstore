using FluentValidation;
using Logstore.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Logstore.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(c => HasItem(c.Items))
                .Equal(true)
                .WithMessage("Um pedido precisa conter items");
        }

        private bool HasItem(IEnumerable<OrderItem> orderItem)
        {
            return orderItem.ToList().Any();
        }
    }
}
