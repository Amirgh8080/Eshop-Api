using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Create
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(r => r.Title)
                .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Title)
                  .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}
