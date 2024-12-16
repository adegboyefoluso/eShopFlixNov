using AuthService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public  interface IUserAppServices
    {
        UserDTO LoginUser (string username, string password);
        bool RegisterUser(SignUpDTO user, string Role);
        IEnumerable<UserDTO> GetUsers();


    }
}
