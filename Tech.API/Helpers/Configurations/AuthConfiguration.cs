using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Tech.API.Helpers.Configurations;

public static class AuthConfiguration
{
	public static IServiceCollection AddConfigureCors(
		this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", builder =>
			{
				builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
			});
		});

		return services;
	}

	public static IServiceCollection AddSwaggerService(
		this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tech.Api", Version = "v1" });
			var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Description = "JWT Authorization header using the Bearer scheme. Example \"Authorization Bearer {Token}\"",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
		});

		return services;
	}

	public static IServiceCollection AddJwtService(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(o =>
		{
			var key = Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"]);
			o.SaveToken = true;
			o.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["Jwt:Issuer"],
				ValidAudience = configuration["Jwt:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ClockSkew = TimeSpan.FromMinutes(1)
			};
		});

		return services;
	}
}
