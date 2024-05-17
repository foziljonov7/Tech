﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tech.Domain.Entities;
using Tech.Services.Interfaces.Users;

namespace Tech.Services.Services.Users;

public class AuthService : IAuthService
{
	private readonly IConfigurationSection configuration;

	public AuthService(IConfiguration configuration)
    {
        this.configuration = configuration.GetSection("Jwt");
    }

    public string GenerateToken(User user)
	{
		var claims = new[]
	   {
			new Claim("Id", user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname),
			new Claim("PhoneNumber", user.PhoneNumber),
			new Claim(ClaimTypes.Role, user.UserRole.ToString())
		};

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
		var tokenDestrictor = new JwtSecurityToken(
			configuration["Issuer"],
			configuration["Audience"],
			claims,
			expires: DateTime.Now.AddMinutes(double.Parse(configuration["Lifetime"])),
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(tokenDestrictor);
	}
}
