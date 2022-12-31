using Core.Common.Interfaces;
using Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccessService : IAccessService
    {
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IBookishDbContext _bookishDb;
        public AccessService(IHttpContextAccessor httpcontext, IBookishDbContext bookishDb)
        {
            _httpcontext = httpcontext;
            _bookishDb = bookishDb;
        }

        public ClaimsPrincipal User
        {
            get
            {
                if (_httpcontext.HttpContext != null)
                {
                    return _httpcontext.HttpContext.User;
                }
                else
                {
                    return new ClaimsPrincipal();
                }

            }
        }

        public Account? GetCurrentUser()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = _bookishDb.Users
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
            return user ?? null;
        }
    }
}
