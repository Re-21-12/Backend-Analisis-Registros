using Backend_Analisis.Data;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger y servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularNetlify",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "https://front-analisis-registros.netlify.app",
                "http://frontend:4200",
                "https://proy-analisis-re2112.duckdns.org"
                                )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
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
app.Urls.Add("http://+:5035"); // Puerto interno en Render

// Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa CORS antes de los controladores
app.UseCors("AllowAngularNetlify");
app.UseHttpsRedirection(); // ✅ Ahora usará el puerto 443 correctamente

app.MapControllers();

app.Run();