using BackOnWay.Metier.Admin;
using BackOnWay.Metier.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Passager;
using BackOnWay.Repository.Admin;
using BackOnWay.Repository.Utilisateur.Conducteur;
using BackOnWay.Repository.Utilisateur.Passager;
using BackOnWay.Utils;

var builder = WebApplication.CreateBuilder(args);

//Test Factory
builder.Services.AddSingleton<IDbConnectionFactory, SqlDbConnectionFactory>();
builder.Services.AddScoped<ListeCompteRepo>();
builder.Services.AddScoped<ListeCompteMetier>();
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
// Add services to the container.
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

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
