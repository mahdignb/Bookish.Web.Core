using Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class AccessTokenGenerator
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly IEmailService _emailService;
        public AccessTokenGenerator(UserManager<Account> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            //_emailService = emailService;
        }

        public async Task<TokenResponse> GenerateAccessToken(TokenModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && user.IsActive == true)
            {
                if (user.TwoFactorEnabled && String.IsNullOrEmpty(model.TwoFactorToken))
                {
                    var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
                    //await _emailService.SendNotifyAsync(user.Email, $"Your OTP code is {code}");
                    //_emailService.SendNotifyQueued(user.Email, $"Your OTP code is {code}");
                    return new TokenResponse
                    {
                        Token = "",
                        AdditionalMessages = "Two factor code has been sent to your email"
                    };
                }
                if (user.TwoFactorEnabled && !String.IsNullOrEmpty(model.TwoFactorToken))
                {
                    var result = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, model.TwoFactorToken);
                    if (result == false)
                    {
                        return new TokenResponse
                        {
                            Token = "Invalid token"
                        };
                    }
                }
                var generatedToken = CreateAccessToken(user);
                if (generatedToken == null)
                {
                    return new TokenResponse
                    {
                        Token = "",
                    };
                }
                return new TokenResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(generatedToken),
                    ValidTo = generatedToken.ValidTo,
                    UserType = user.UserType.ToString()
                };
            }
            return new TokenResponse
            {
                Token = "",
            };
        }

        public TokenResponse GenerateAccessToken(Account user)
        {
            var generatedToken = CreateAccessToken(user);
            if (generatedToken == null)
            {
                return new TokenResponse
                {
                    Token = "",
                };
            }
            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(generatedToken),
                ValidTo = generatedToken.ValidTo,
                UserType = user.UserType.ToString()
            };
        }
        public JwtSecurityToken CreateAccessToken(Account user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt_secret"]));
            return new JwtSecurityToken(
                issuer: _configuration["jwt_valid_issuer"],
                audience: _configuration["jwt_valid_audience"],
                expires: DateTime.Now.AddDays(Double.Parse(_configuration["access_token_lifetime"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials
                (authSigningKey, SecurityAlgorithms.HmacSha256));
        }
    }
}