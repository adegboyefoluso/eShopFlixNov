using AuthService.Application.DTO;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services
{
    public class UserAPPService : IUserAppServices
    {
        IUserRepository _userRepo;
        IConfiguration _configuration;
        public UserAPPService(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }
        // using JWT Token generation 
        public string GenerateToken(UserDTO user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials= new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            int ExpireMinutes = Convert.ToInt32(_configuration["JWT:ExpireMinutes"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim ("Roles", string.Join(",", user.Roles)),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
                                              _configuration["JWT:Audience"],
                                              claims,
                                              expires:DateTime.UtcNow.AddMinutes(ExpireMinutes), 
                                              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
                                              

        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = _userRepo.GetUsers().Select(x => new UserDTO
            {
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Roles = x.Roles.Select(r => r.Name).ToArray(),
                UserId = x.Id

            });

            return users;

        }

        public UserDTO LoginUser(string username, string password)
        {
            var user = _userRepo.LoginUser(username, password);

            if (user != null)
            {
                UserDTO model = new UserDTO
                {
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    UserId = user.Id,
                    Roles = user.Roles.Select(r => r.Name).ToArray(),

                };

                model.Token = GenerateToken(model);
                return model;
            }
            return null;
        }

        public bool RegisterUser(SignUpDTO model, string Role)
        {
            var user = new User
            {

                CreatedDate = DateTime.Now,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = false,
                Password = model.Password

            };

            return _userRepo.RegisterUser(user, Role);
        }
    }
}
