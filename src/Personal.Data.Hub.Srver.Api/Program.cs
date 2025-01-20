using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Personal.Dara.Hub.Server.BLL.Services;
using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Data.Context;
using Personal.Dara.Hub.Server.Data.Interfaces;
using Personal.Dara.Hub.Server.Data.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ServerDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true; 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
#region Services and repositories

    builder.Services.AddTransient<IPasswordService, PasswordService>();
    builder.Services.AddTransient<IEmailService, EmailService>();
    builder.Services.AddTransient<IAccountService, AccountService>();
    builder.Services.AddTransient<ITokenService, TokenService>();

    builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
