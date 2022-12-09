using API.Models;
using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Helper
{
    public class AccessTokenModelValidator : AbstractValidator<TokenModel>
    {
        public AccessTokenModelValidator()
        {
            RuleFor(x => x.Username)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage("Username must have a value and cannot be empty")
             .NotNull()
             .WithMessage("Username must have a value and cannot be null");

            RuleFor(x => x.Password)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithMessage("Password must have a value and cannot be empty")
              .NotNull()
              .WithMessage("Password must have a value and cannot be null");
        }
    }
}
