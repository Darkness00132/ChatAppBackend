using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class Di
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    string JwtBearerKey = config.GetValue<string>("Jwt:Key");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtBearerKey)),
                        ValidateIssuer = true,
                        ValidIssuer = config.GetValue<string>("Jwt:Issuer"),
                        ValidateAudience = true,
                        ValidAudience = config.GetValue<string>("Jwt:Audience"),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Query["access_token"].FirstOrDefault();

                            if (string.IsNullOrEmpty(token))
                            {
                                var auth = context.Request.Headers["Authorization"].FirstOrDefault();
                                if (!string.IsNullOrEmpty(auth) &&
                                    auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                                {
                                    token = auth.Substring("Bearer ".Length).Trim();
                                }
                            }

                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccessTokenService, JwtToken>();
            return services;
        }
    }
}
