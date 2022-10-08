using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.Checkout
{

    public partial class CheckoutOrderCommandHandler
    {
        public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
        {
            public CheckoutOrderCommandValidator()
            {
                RuleFor(r => r.Name)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage(ValidationMessages.required("نام"));

                RuleFor(r => r.Family)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage(ValidationMessages.required("نام خانوادگی"));

                RuleFor(r => r.City)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage(ValidationMessages.required("شهر"));

                RuleFor(r => r.Shire)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage(ValidationMessages.required("استان"));


                RuleFor(r => r.PostaAdderss)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage(ValidationMessages.required("آدرس"));

                RuleFor(r => r.PostalCode)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage(ValidationMessages.required("کد پستی"))
                  .MaximumLength(10).WithMessage("کد پستی نا معتبر است.")
                  .MinimumLength(10).WithMessage("کد پستی نا معتبر است.");

                RuleFor(r => r.PhoneNumber)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(ValidationMessages.required("شماره موبایل"))
                    .MaximumLength(11).WithMessage("شماره موبایل نا معتبر است.")
                    .MinimumLength(11).WithMessage("شماره موبایل نا معتبر است.");

                RuleFor(r => r.NationalCode)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(ValidationMessages.required("کد ملی"))
                    .MaximumLength(10).WithMessage("کد ملی نا معتبر است.")
                    .MinimumLength(10).WithMessage("کد ملی نا معتبر است.")
                    .ValidNationalId();


            }
        }
    }

}
