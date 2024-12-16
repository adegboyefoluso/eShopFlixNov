using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        AuthDbContext _authDbContext;
        public UserRepository(AuthDbContext Authdb) 
        { 
            _authDbContext= Authdb;
        }
        public IEnumerable<User> GetUsers()
        {
            return _authDbContext.Users.ToList();
        }

        public User LoginUser(string Email, string Password)
        {
            var user = _authDbContext.Users.Include(u => u.Roles).Where(u=>u.Email == Email).FirstOrDefault();
            if (user != null)
            {
                bool isvalid = BCrypt.Net.BCrypt.Verify(Password, user.Password);
                if (isvalid)
                {
                    return user;    
                }
            }
            return null;
        }

        public bool RegisterUser(User user, string Role)
        {
           var role= _authDbContext.Roles.Where(r=>r.Name == Role).FirstOrDefault();

            if(role != null)
            {
                user.Password= BCrypt.Net.BCrypt.HashPassword(user.Password);  // hashing password
                user.Roles.Add(role);
                 _authDbContext.Users.Add(user);
                _authDbContext.SaveChanges();
                return true;    
            }
            return false;   
        }
    }
}
