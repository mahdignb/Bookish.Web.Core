using Domain.Entities.Account;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using WebAPI.Helper;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly BookishDbContext _bookishDb;
        private readonly UserManager<Account> _userManager;
        //private readonly IEmailService _emailService;
        private readonly IValidator<RefreshRequest> _refreshRequestValidator;
        private readonly IValidator<TokenModel> _accessTokenValidator;
        //private readonly IAccessService _accessService;

        public UserController(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator, UserManager<Account> userManager, BookishDbContext bookishDb, IValidator<RefreshRequest> refreshRequestValidator, IValidator<TokenModel> accessTokenValidator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _userManager = userManager;
            _bookishDb = bookishDb;
            _refreshRequestValidator = refreshRequestValidator;
            _accessTokenValidator = accessTokenValidator;
        }

        [HttpPost]
        [Route("AccessToken")]
        public async Task<ActionResult<TokenResponse>> Create([FromBody] TokenModel model)
        {
            var createAccessToken = new CreateAccessToken(_accessTokenGenerator, _refreshTokenGenerator, _accessTokenValidator);
            return await createAccessToken.GenerateToken(model);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshRequest request)
        {
            var createRefreshToken = new CreateRefreshToken(_refreshTokenValidator, _refreshTokenGenerator,
                _bookishDb, _accessTokenGenerator, _refreshRequestValidator);
            return await createRefreshToken.GenerateToken(request);
        }
        [Authorize]
        [HttpPost]
        [Route("EnableTwoFactor")]
        public async Task<ActionResult<string>> EnableTwoFactor([FromBody] EnableTwoStepModel model)
        {
            //var enableTwoStep = new TwoStepAuthentication(_accessService, _bookishDb,
            //                                              _userManager, _emailService);
            //return await enableTwoStep.EnableTwoStep(model);
            return "";
        }
    }
}
