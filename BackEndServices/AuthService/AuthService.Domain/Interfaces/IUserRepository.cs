using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Interfaces
{
   public  interface IUserRepository
    {
        User LoginUser(string Email, string Password);
        bool RegisterUser(User user, string Role);
        IEnumerable<User> GetUsers();
    }
}
