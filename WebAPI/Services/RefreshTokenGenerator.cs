using Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class RefreshTokenGenerator
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;
        private readonly BookishDbContext _bookishDb;
        public RefreshTokenGenerator(UserManager<Account> userManager, IConfiguration configuration, BookishDbContext bookishDb)
        {
            _userManager = userManager;
            _configuration = configuration;
            _bookishDb = bookishDb;
        }

        public async Task<TokenResponse> GenerateRefreshToken(TokenModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && user.IsActive == true)
            {
                var generatedToken = CreateRefreshToken();
                if (generatedToken == null)
                {
                    return new TokenResponse
                    {
                        Token = "",
                    };
                }
                var refreshToken = new JwtSecurityTokenHandler().WriteToken(generatedToken);
                await _bookishDb.SaveChangesAsync();
                return new TokenResponse
                {
                    Token = refreshToken,
                    ValidTo = generatedToken.ValidTo,
                    UserType = user.UserType.ToString()
                };
            }
            return new TokenResponse
            {
                Token = "",
            };
        }
        public async Task<TokenResponse> GenerateRefreshToken(Account user)
        {
            var generatedToken = CreateRefreshToken();
            if (generatedToken == null)
            {
                return new TokenResponse
                {
                    Token = "",
                };
            }
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(generatedToken); await _bookishDb.SaveChangesAsync();
            return new TokenResponse
            {
                Token = refreshToken,
                ValidTo = generatedToken.ValidTo,
                UserType = user.UserType.ToString()
            };
        }
        public JwtSecurityToken CreateRefreshToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["refresh_token_secret"]));
            return new JwtSecurityToken(
                issuer: _configuration["jwt_valid_issuer"],
                audience: _configuration["jwt_valid_audience"],
                expires: DateTime.Now.AddDays(Double.Parse(_configuration["refresh_token_lifetime"])),
                claims: null,
                signingCredentials: new SigningCredentials
                (authSigningKey, SecurityAlgorithms.HmacSha256));
        }
    }
}
