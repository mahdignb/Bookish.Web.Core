using Domain.Entities.Account;
using Domain.Entities.Bookish;
using Domain.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            //Roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "USER"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "SuperAdmin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "SUPERADMIN"
            });
        }
    }
}
