using System.Text;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Identity.Logic;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Identity.Repositories;
using Gradebook.Foundation.Identity.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class IdentityService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<ApplicationIdentityDatabaseContext>
            (options => options.UseMySql(configuration.GetConnectionString("DefaultAppDatabase"), new MySqlServerVersion(new Version(8, 30, 0))));

        services.AddIdentity<ApplicationUser, IdentityRole>(o =>
        {
            o.SignIn.RequireConfirmedEmail = true;
            o.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationIdentityDatabaseContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,

                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api/signalr"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        services.AddScoped<IIdentityLogic, IdentityLogic>();

        services.AddCors(e =>
        {
            e.AddDefaultPolicy(p =>
            {
                //TODO: move to appsettings.json
                p.WithOrigins("http://development.gradebook.com", "http://api-tests.gradebook.com")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddScoped<IQueriesRepository, QueriesRepository>();
        services.AddScoped<ICommandsRepository, CommandsRepository>();

    }
}
