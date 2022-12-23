using Domain.Entities;
using Domain.Utility;

namespace API.Models.UserModels
{
    public class AddUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
    }
}
