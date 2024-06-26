﻿using AtmEquityProject.Data;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtmEquityProject.Repositories
{
    public class UserService : IUserService

    {
        private readonly AppSettings _appSettings;
        private readonly DataContext db;

        public UserService(IOptions<AppSettings> appSettings, DataContext _db)
        {
            _appSettings = appSettings.Value;
            db = _db;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = await db.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null) return null;
            var token = await generateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.Where(x => x.isActive == true).ToListAsync();
        }
        public async Task<User?> GetById(int id)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public User GetUserById(int id)
        {
            return db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<User?> AddAndUpdateUser(User userData)
        {
            bool isSuccess = false;
            if (userData.Id > 0)
            {
                var obj = await db.Users.FirstOrDefaultAsync(c => c.Id == userData.Id);
                if (obj != null)
                {
                    obj.FirstName = userData.FirstName;
                    obj.LastName = userData.LastName;
                    db.Users.Update(obj);
                    isSuccess = await db.SaveChangesAsync() > 0;
                }
            }
            else
            {
                await db.Users.AddAsync(userData);
                isSuccess = await db.SaveChangesAsync() > 0;
            }

            return isSuccess ? userData : null;
        }
        // Cette méthode permet de générer un token pour le user
        // Le délai de validité du token est de 3
        private async Task<string> generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });
            return tokenHandler.WriteToken(token);
        }
        public User GetUserInfo(string token)
        {
            // Logique d'extraction des informations de l'utilisateur à partir du token JWT
            // ...
            if (token == null | token =="")
                return new User { FirstName="", Username=""};
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var securityToken);
            var userId = int.Parse(claimsPrincipal.Claims.First(x => x.Type == "id").Value);
            var user = this.GetUserById(userId);
            if(user == null)
                return new User { FirstName="", Username=""};
            return user;
        }
    }
}
