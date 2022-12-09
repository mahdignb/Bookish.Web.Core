using API.Models;
using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Helper
{
    public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
    {
        public RefreshRequestValidator()
        {
            RuleFor(w => w.RefreshToken).NotEmpty();
        }
    }
}
