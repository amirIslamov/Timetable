using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Model.Dal;
using Model.Dal.Identity;
using Model.Dal.Repositories;
using RetardCheck.Auth.Options;
using API.Auth;
using API.Auth.Authorization.Identity;
using Model.Profile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TimetableDbContext>(o =>
{
    o.UseSqlite(builder.Configuration.GetConnectionString("timetableDb"));
});
builder.Services.AddScoped<TimetableUserStore>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<StudentsRepository>();
builder.Services.AddScoped<TeachersRepository>();
builder.Services.AddScoped<GroupsRepository>();

builder.Services.AddIdentityCore<TimetableUser>()
    .AddUserStore<TimetableUserStore>()
    .AddClaimsPrincipalFactory<TimetableUserClaimsPrincipalFactory>()
    .AddDefaultTokenProviders();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));
builder.Services.AddScoped<JwtService>();
            
JwtOptions jwtOptions = builder.Configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SigningKey)
            )
        };
    });

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(
        options => options.AddPolicy("devCors", opts => opts
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));    
}

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
