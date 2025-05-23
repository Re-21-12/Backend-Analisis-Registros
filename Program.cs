using Backend_Analisis.Data;
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
// ...existing code...
// DbContext
builder.Services.AddDbContext<RegistroPersonaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistroPersonaConnection")));

var app = builder.Build();
app.Urls.Add("http://+:5035"); // Cambia el puerto si es necesario
// Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa CORS antes de los controladores
app.UseCors("AllowAngularNetlify");
app.UseHttpsRedirection();

app.MapControllers(); // ❗ Importante

app.Run();
