using FluentValidation;

namespace Shop.Application.Orders.AddItem
{
    public class AddItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddItemCommandValidator()
        {
            RuleFor(r => r.Count)
                .GreaterThanOrEqualTo(0).WithMessage("تعداد باید بیشتر از صفر باشد");
        }
    }
}
