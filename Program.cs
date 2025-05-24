using Backend_Analisis.Data;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger y servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - Configuración mejorada
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularNetlify",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "https://front-analisis-registros.netlify.app",
                "http://frontend:4200",
                "https://proy-analisis-re2112.duckdns.org",
                "http://localhost:5035"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Agregar si usas cookies/auth
        });
});

// 🔥 Solución: Configurar el puerto HTTPS para redirección (añade esto)
builder.Services.Configure<HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 443; // Puerto estándar HTTPS en Render
});

// DbContext
builder.Services.AddDbContext<RegistroPersonaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistroPersonaConnection")));

var app = builder.Build();

// Configuración de puerto para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5035";
app.Urls.Add($"http://0.0.0.0:{port}");

// Remover esta línea para producción en Render
// app.Urls.Add("http://+:5035"); 

// Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Reordenar middleware
app.UseCors("AllowAngularNetlify"); // CORS debe ir antes que HTTPS redirection
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // Solo en producción
}

app.MapControllers();

app.Run();