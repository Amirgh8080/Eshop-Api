using FluentValidation;

namespace Shop.Application.Orders.DecreaseItemCount
{
    public class DecreaserderItemCountCommandValidator : AbstractValidator<DecreaseOrderItemCountCommand>
    {
        public DecreaserderItemCountCommandValidator()
        {
            RuleFor(r => r.Count)
              .GreaterThanOrEqualTo(0).WithMessage("تعداد باید بیشتر از صفر باشد");
        }
    }
}
