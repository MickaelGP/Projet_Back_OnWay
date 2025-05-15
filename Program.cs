using System.Text;
using BackOnWay.Metier.Auth;
using BackOnWay.Metier.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Passager;
using BackOnWay.Repository.Auth;
using BackOnWay.Repository.Utilisateur.Conducteur;
using BackOnWay.Repository.Utilisateur.Passager;
using BackOnWay.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//JWT
var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);
var secretKey = jwtSection.GetValue<string>("SecretKey");
var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtSettings>>().Value);
builder.Services.AddScoped<GenerateTokens>();
// Ajout pour injection de dépendances
builder.Services.AddScoped<ICovoiturageRepo, CovoiturageRepo>();
builder.Services.AddScoped<ICovoiturageMetier, CovoiturageMetier>();
builder.Services.AddScoped<IParticiperCovoitRepo, ParticiperCovoitRepo>();
builder.Services.AddScoped<IParticiperCovoitMetier, ParticiperCovoitMetier>();
builder.Services.AddScoped<IHistoriqueCovoitRepo, HistoriqueCovoitRepo>();
builder.Services.AddScoped<IHistoriqueCovoitMetier, HistoriqueCovoitMetier>();
builder.Services.AddScoped<IDeposerAvisRepo, DeposerAvisRepo>();
builder.Services.AddScoped<IDeposerAvisMetier, DeposerAvisMetier>();
builder.Services.AddScoped<IDeposerReclamationRepo, DeposerReclamationRepo>();
builder.Services.AddScoped<IDeposerReclamationMetier, DeposerReclamationMetier>();
builder.Services.AddScoped<IUpdateCovoitRepo, UpdateCovoitRepo>();
builder.Services.AddScoped<IUpdateCovoitMetier, UpdateCovoitMetier>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAuthMetier, AuthMetier>();


// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // à mettre true en prod
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7071",
        ValidAudience = "https://localhost:7071",
        IssuerSigningKey = new SymmetricSecurityKey(key),

    };
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
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

app.UseAuthentication();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
