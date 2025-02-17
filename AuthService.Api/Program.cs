using AuthMicroservice.Application.Commands.RegisterUser;
using AuthMicroservice.Application.Queries.LoginUser;
using AuthMicroservice.Application.Queries.RefreshToken;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Entities.Data;
using AuthService.Api.Config;
using AuthService.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddDbContext<AuthDbContext>(options =>options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(

    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),

            ValidIssuer = jwtConfig.Issuer,

            ValidateAudience = false,

            ClockSkew = TimeSpan.Zero,

            ValidateLifetime = true
    };
    
    });



builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(RegisterUserCommandHandler));
    cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserQueryHandler));
    cfg.RegisterServicesFromAssemblyContaining(typeof(RefreshTokenQueryHandler));
});

//builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
