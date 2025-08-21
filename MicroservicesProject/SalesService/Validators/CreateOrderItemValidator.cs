using FluentValidation;
using SalesService.DTOs.OrderDTO;

namespace SalesService.Validators
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItem>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
