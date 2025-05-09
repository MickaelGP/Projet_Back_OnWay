using BackOnWay.Metier.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Passager;
using BackOnWay.Repository.Utilisateur.Conducteur;
using BackOnWay.Repository.Utilisateur.Passager;

var builder = WebApplication.CreateBuilder(args);

// Ajout pour injection de dépendances
builder.Services.AddScoped<ICovoiturageRepo, CovoiturageRepo>();
builder.Services.AddScoped<ICovoiturageMetier, CovoiturageMetier>();
builder.Services.AddScoped<IParticiperCovoitRepo, ParticiperCovoitRepo>();
builder.Services.AddScoped<IParticiperCovoitMetier, ParticiperCovoitMetier>();

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
