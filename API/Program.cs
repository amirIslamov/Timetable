using System.Text;
using API.Auth;
using API.Auth.Authorization;
using Arch.EntityFrameworkCore.UnitOfWork;
using FilteringOrderingPagination.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.Auth;
using Model.Dal;
using Model.Entities;
using Model.Users;
using Model.Validation;
using Model.Validation.Abstractions;
using RetardCheck.Auth.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o =>
{
    o.ModelBinderProviders.Add(new FilterBinderProvider());
    o.ModelBinderProviders.Add(new PagingBinderProvider());
});

builder.Services.AddDbContext<TimetableDbContext>(o =>
    {
        o.UseSqlite(builder.Configuration.GetConnectionString("timetableDb"));
    })
    .AddUnitOfWork<TimetableDbContext>();

builder.Services.AddSingleton<TimetableErrorDescriber>();
builder.Services.AddSingleton<AuthErrorDescriber>();

builder.Services.AddScoped<IValidator<Discipline>, DisciplineValidator>();
builder.Services.AddScoped<IValidator<Group>, GroupValidator>();
builder.Services.AddScoped<IValidator<TeacherLoad>, LoadValidator>();
builder.Services.AddScoped<IValidator<Student>, StudentValidator>();
builder.Services.AddScoped<IValidator<Subject>, SubjectValidator>();
builder.Services.AddScoped<IValidator<Teacher>, TeacherValidator>();
builder.Services.AddScoped<IValidator<TimetableEntry>, TimetableEntryValidator>();
builder.Services.AddScoped<IValidator<TimetableException>, TimetableExceptionValidator>();
builder.Services.AddScoped<TimetableUserManager>();
builder.Services.AddScoped<PasswordManager>();
builder.Services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddSingleton<PasswordValidator>();
builder.Services.AddScoped<IValidator<TimetableUser>, UserValidator>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory, UserClaimsPrincipalFactory>();


builder.Services.Configure<PasswordHasherOptions>(
    builder.Configuration.GetSection(PasswordHasherOptions.PasswordHasher));
builder.Services.Configure<PasswordValidationOptions>(
    builder.Configuration.GetSection(PasswordValidationOptions.PasswordValidation));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));
builder.Services.AddScoped<JwtService>();

var jwtOptions = builder.Configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    });

if (builder.Environment.IsDevelopment())
    builder.Services.AddCors(
        options => options.AddPolicy("devCors", opts => opts
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

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