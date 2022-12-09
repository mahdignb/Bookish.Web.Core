using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Helper
{
    public class CreateAccessToken
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IValidator<TokenModel> _validator;
        public CreateAccessToken(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, IValidator<TokenModel> validator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _validator = validator;
        }

        public async Task<ActionResult<TokenResponse>> GenerateToken([FromBody] TokenModel model)
        {
            var validate = _validator.Validate(model);

            if (validate.Errors.Any())
            {
                return new BadRequestObjectResult(validate.ToString());
            }
            var accessToken = await _accessTokenGenerator.GenerateAccessToken(model);
            if (!string.IsNullOrEmpty(accessToken.AdditionalMessages))
            {
                return new TokenResponse
                {
                    AdditionalMessages = "Please enter the two-step code"
                };
            }
            if (accessToken.Token == "")
            {
                return new UnauthorizedObjectResult("");
            }
            var refreshToken = await _refreshTokenGenerator.GenerateRefreshToken(model);
            return new OkObjectResult(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            });
        }
    }
}
