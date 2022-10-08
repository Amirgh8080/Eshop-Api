using FluentValidation;

namespace Shop.Application.Orders.IncreaseItemCount
{
    public class DecreaserderItemCountCommandValidator : AbstractValidator<IncreaseOrderItemCountCommand>
    {
        public DecreaserderItemCountCommandValidator()
        {
            RuleFor(r => r.Count)
              .GreaterThanOrEqualTo(0).WithMessage("تعداد باید بیشتر از صفر باشد");
        }
    }
}
