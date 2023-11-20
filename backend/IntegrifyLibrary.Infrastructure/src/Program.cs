using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Infrastructure;

using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    options.AddDefaultPolicy(builder =>
   {
       builder.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
   });
});


builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

    // Add a custom converter for the Role enum
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<IBookRepo, BookRepo>()
    .AddScoped<ILoanRepo, LoanRepo>()
    .AddScoped<IAuthorRepo, AuthorRepo>()
    .AddScoped<IGenreRepo, GenreRepo>();

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IBookService, BookService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ILoanService, LoanService>()
    .AddScoped<IAuthorService, AuthorService>()
    .AddScoped<IGenreService, GenreService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddSingleton<ErrorHandlerMiddleware>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "integrify-assignment",
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("integrify-assignment-secret-key-1234567890")),
        ValidateIssuerSigningKey = true
    };
})
.AddGoogle(options => {
    options.ClientId = configuration["Google:ClientId"];
    options.ClientSecret = configuration["Google:ClientSecret"];
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmailWhiteList", policy => policy.RequireClaim(ClaimTypes.Email, "admin@mail.com"));
});




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = ""
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Integrify Library",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
// Testing
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
