using Auth.Application.Config;
using Auth.Domain.Entities.Data;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Auth.Application.Commands.RegisterUser;
using Auth.Application.Queries.LoginUser;
using Auth.Application.Queries.RefreshToken;
using Auth.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

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



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
