using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using CoR = wiwi.domain.service.auth;


using wiwi.interfaces.command;
using wiwi.interfaces.command.account;
using wiwi.interfaces.command.auth;
using wiwi.infrastructure.context;
using wiwi.infrastructure.repository.user;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<UserDbContext>(option => option.UseNpgsql(
      builder.Configuration.GetConnectionString("PostgresDb")));
builder.Services.AddTransient<IUserRepository, UserRepository>();


builder.Services.AddScoped<ICommand<TRegisterAction>, RegisterCommand>();
builder.Services.AddScoped<ICommand<TDeleteAccountAction>,DeleteCommand>();
builder.Services.AddScoped<ICommand<TLoginAction>,LoginCommand>();


builder.Services.AddScoped<CoR.AuthHandler>();
builder.Services.AddScoped<CoR.BanCheckHandler>();
builder.Services.AddScoped<CoR.TokenHandler>();
builder.Services.AddScoped<CoR.ILoginService>(provider => {
    var authHNDLR = provider.GetRequiredService<CoR.AuthHandler>();
    var banCHCK   = provider.GetRequiredService<CoR.BanCheckHandler>();
    var tokenHNDLR= provider.GetRequiredService<CoR.TokenHandler>();

    authHNDLR.next = banCHCK;
    banCHCK.next = tokenHNDLR;
    return authHNDLR;
});



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication ( 
    options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    }
                                  )   
    .AddJwtBearer( 
        jwtoptions => {
          jwtoptions.TokenValidationParameters = new TokenValidationParameters() {
          ValidateIssuer   = true,
          ValidIssuer      = "http://localhost:5225",
          ValidateAudience = true,
          ValidAudience    = "http://localhost:5225",

          ValidateLifetime = true, 
          ValidateIssuerSigningKey = true,

          NameClaimType = ClaimTypes.NameIdentifier,
          RoleClaimType = "role",
        };  
                      }
    );
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseDefaultFiles(); 
app.UseStaticFiles();
app.Run();
















