using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Helper
{
    public class CreateRefreshToken
    {
        private readonly BookishDbContext _bookishDb;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly IValidator<RefreshRequest> _validator;
        public CreateRefreshToken(RefreshTokenValidator refreshTokenValidator, RefreshTokenGenerator refreshTokenGenerator, BookishDbContext bookishDb, AccessTokenGenerator accessTokenGenerator, IValidator<RefreshRequest> validator)
        {
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _bookishDb = bookishDb;
            _accessTokenGenerator = accessTokenGenerator;
            _validator = validator;
        }

        public async Task<ActionResult<TokenResponse>> GenerateToken(RefreshRequest request)
        {
            var validate = _validator.Validate(request);

            if (validate.Errors.Any())
            {
                return new TokenResponse
                {
                    Token = "Refresh token cannot be empty",
                };
            }
            var isValidRefreshToken = _refreshTokenValidator.Validate(request.RefreshToken);
            if (!isValidRefreshToken)
            {
                return new BadRequestObjectResult("Invalid refresh token");
            }
            var user = _bookishDb.Users
                .Where(w => w.IsActive == true)
                .FirstOrDefault();
            if (user == null)
            {
                return new NotFoundObjectResult("Invalid refresh token");
            }
            var accessToken = _accessTokenGenerator.GenerateAccessToken(user);
            var refreshToken = await _refreshTokenGenerator.GenerateRefreshToken(user);
            return new OkObjectResult(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            });
        }
    }
}
