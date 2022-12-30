using Domain.Entities.Account;
using Domain.Utility;

namespace WebAPI.SeedData
{
    public class UserSeed
    {
        public Account MasterChiefAccount { get; set; }
        public string MasterChiefPassword { get; set; }
        public Account NathanDrakesAccount { get; set; }
        public string NathanDrakesPassword { get; set; }
        public Account TomCruiseAccount { get; set; }
        public string TomCruisePassword { get; set; }
        public Task Seed()
        {
            MasterChiefAccount = new Account
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = "Masterchief@gmail.com",
                IsActive = true,
                UserName = "Masterchief@gmail.com",
                UserType = UserType.SuperAdmin.ToString(),
                TwoFactorEnabled = false
            };
            MasterChiefPassword = "test@123";
            NathanDrakesAccount = new Account
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = "Nathan@gmail.com",
                IsActive = true,
                UserName = "Nathan@gmail.com",
                UserType = UserType.Admin.ToString(),
                TwoFactorEnabled = false
            };
            NathanDrakesPassword = "test@123";
            TomCruiseAccount = new Account
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = "Tom@gmail.com",
                IsActive = true,
                UserName = "Tom@gmail.com",
                UserType = UserType.User.ToString(),
                TwoFactorEnabled = false
            };
            TomCruisePassword = "test@123";
            return Task.CompletedTask;
        }
    }
}
