using BackOnWay.Metier.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Passager;
using BackOnWay.Middlewares;
using BackOnWay.Repository.Utilisateur.Conducteur;
using BackOnWay.Repository.Utilisateur.Passager;

var builder = WebApplication.CreateBuilder(args);

// Injection de dépendances
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

// Contrôleurs
builder.Services.AddControllers();

// CORS pour autoriser Next.js (http://localhost:3000) avec cookies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJsLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowCredentials()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger (si besoin)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// DevTools (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS doit être avant Authorization/Middleware
app.UseCors("AllowNextJsLocalhost");

app.UseAuthorization();
app.UseMiddleware<SessionMiddleware>();

app.MapControllers();

app.Run();
