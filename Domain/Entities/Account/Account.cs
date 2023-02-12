using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.Account
{
    public class Account : IdentityUser
    {
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
