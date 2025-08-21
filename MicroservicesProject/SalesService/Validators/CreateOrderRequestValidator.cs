using FluentValidation;
using SalesService.DTOs.OrderDTO;

namespace SalesService.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Item).NotEmpty().WithMessage("Pedido deve conter ao menos 1 item.");
            RuleForEach(x => x.Item).SetValidator(new CreateOrderItemValidator());
        }
    }
}
