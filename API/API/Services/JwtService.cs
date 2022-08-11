using API.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expDate;

        public JwtService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _issuer = config.GetSection("JwtConfig").GetSection("Issuer").Value;
            _audience = config.GetSection("JwtConfig").GetSection("Audience").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(LoginVM login)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", login.Email));
            foreach (var item in login.Role) claims.Add(new Claim("roles", item.Name));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new
                JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
                );
            var idToken = new JwtSecurityTokenHandler().WriteToken(token);
            return idToken;
        }
    }
}
