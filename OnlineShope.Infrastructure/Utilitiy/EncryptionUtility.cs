using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShope.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Core.Utilitiy
{
    public class EncryptionUtility
    {
        private readonly Configs configs;

        public EncryptionUtility(IOptions<Configs> options)
        {
            this.configs=options.Value;
        }
        public string GetSH256(string password, string salt)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password+salt));
                var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hash;
                //StringBuilder builder = new StringBuilder();
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    builder.Append(bytes[i].ToString("x2"));
                //}
                //return builder.ToString();
            }
        }

        public string GetNewSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public string GetNewToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configs.TokenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("userId", userId.ToString()),
                        //new Claim("TimeOut-Minute", tokenTimeOut.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(configs.TokenTimeout),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
