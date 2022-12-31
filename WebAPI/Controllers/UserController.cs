using Core.Common.Interfaces;
using Domain.Entities.Account;
using Domain.Utility;
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
        private readonly IAccessService _accessService;

        public UserController(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator, UserManager<Account> userManager, BookishDbContext bookishDb, IValidator<RefreshRequest> refreshRequestValidator, IValidator<TokenModel> accessTokenValidator, IAccessService accessService)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _userManager = userManager;
            _bookishDb = bookishDb;
            _refreshRequestValidator = refreshRequestValidator;
            _accessTokenValidator = accessTokenValidator;
            _accessService = accessService;
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
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterModel model)
        {
            var account = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.UserName,
                UserName = model.UserName,
                UserType = UserType.Admin.ToString(),
            };
            var result = await _userManager.CreateAsync(account, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<ActionResult<GetCurrentUserDto>> GetCurrentUser()
        {
            var user = _accessService.GetCurrentUser();
            if (user == null)
            {
                return BadRequest("User not found");
            }

            return new GetCurrentUserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserType = user.UserType
            };
        }
        public class RegisterModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
