using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Identity;
using WebApi.Core_Layer.Service_Interfaces;
using WebApi.Core_Layer.Services;
using WebApi.Infrastructure_Layer.DbContext_Data;
using WebApi.Infrastructure_Layer.Repositories;

var builder = WebApplication.CreateBuilder(args);

/* Add services to the container */
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/* Add Temperature services */
builder.Services.AddScoped<ITemperature, TemperatureRepository>();
builder.Services.AddScoped<ITemperatureAdderService, TemperatureAdderService>();
builder.Services.AddScoped<ITemperatureGetterService, TemperatureGetterService>();
builder.Services.AddScoped<ITemperatureRemoverService, TemperatureRemoverService>();
/* Add Humidity services */
builder.Services.AddScoped<IHumidity, HumidityRepository>();
builder.Services.AddScoped<IHumidityAdderService, HumidityAdderService>();
builder.Services.AddScoped<IHumidityGetterService, HumidityGetterService>();
builder.Services.AddScoped<IHumidityRemoverService, HumidityRemoverService>();
/* Add Jwt services */
builder.Services.AddTransient<IJwtService, JwtService>();
/* Add SignalIR service */
builder.Services.AddSignalR();

/* Add DbContext */
builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnectionString"));
});
/* Add IdentityDbContext */
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"));
});

/* IdentityDbContext configuration */
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, AuthDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, AuthDbContext, Guid>>()
 ;

/* Add authentication middleware to validate the JWT token */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        /* Check some validation options */
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // There is a token validation parameter called ClockSkew, it gets or sets the clock skew to apply when validating a time. The default value of ClockSkew is 5 minutes. That means if you haven't set it, your token will be still valid for up to 5 minutes. If you want to expire your token on the exact time; you'd need to set ClockSkew to zero 
    });

/* Add CORS */
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            // .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
            .WithOrigins("*")
            .WithHeaders("Authorization", "origin", "accept", "content-type", "Content-type", "dataType")
            .WithMethods("GET", "POST", "DELETE");
    });
});

var app = builder.Build();

/* Configure the HTTP request pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
