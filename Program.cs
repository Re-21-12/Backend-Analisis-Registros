using Backend_Analisis.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración del puerto
var port = Environment.GetEnvironmentVariable("PORT") ?? "5035";
builder.WebHost.UseUrls($"http://*:{port}");

// Swagger y servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "https://front-analisis-registros.netlify.app",
            "https://proy-analisis-re2112.duckdns.org"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// DbContext
builder.Services.AddDbContext<RegistroPersonaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistroPersonaConnection")));

var app = builder.Build();

// Middlewares
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();