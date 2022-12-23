using Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace WebAPI.Helper
{
    public class TwoStepAuthentication
    {
        //private readonly IAccessService _accessService;
        private readonly BookishDbContext _bookishDb;
        private readonly UserManager<Account> _userManager;
        //private readonly IEmailService _emailService;

        //public async Task<ActionResult<string>> EnableTwoStep(EnableTwoStepModel model)
        //{
        //    var user = _accessService.GetCurrentUser();
        //    if (user == null)
        //    {
        //        return new UnauthorizedObjectResult("User Not Found");
        //    }
        //    if (model.EnableTwoStep == true && String.IsNullOrEmpty(model.TwoStepCode))
        //    {
        //        var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
        //        //await _emailService.SendNotifyAsync(user.Email, $"Your OTP code is {code}");
        //        _emailService.SendNotifyQueued(user.Email, $"Your OTP code is {code}");
        //        return "Please enter the code we just sent to your email";
        //    }
        //    if (model.EnableTwoStep == true && !String.IsNullOrEmpty(model.TwoStepCode))
        //    {
        //        var result = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, model.TwoStepCode);
        //        if (result == true)
        //        {
        //            user.TwoFactorEnabled = true;
        //            await _bookishDb.SaveChangesAsync();
        //            return "Enabling Two-Factor operation was successful";
        //        }
        //    }
        //    return new BadRequestObjectResult("Bad request");
        //}
    }
}
