using FluentValidation;
using Common.Application.Validation;

namespace Shop.Application.Categories.Add_Child
{
    public class AddChildCategoryCommandValidator : AbstractValidator<AddChildCategoryCommand>
    {
        public AddChildCategoryCommandValidator()
        {
            RuleFor(r => r.Title)
                  .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Title)
                  .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}
