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
    options.AddPolicy("AllowLocalhost4200",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// DbContext
builder.Services.AddDbContext<RegistroPersonaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistroPersonaConnection")));

var app = builder.Build();

// Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa CORS antes de los controladores
app.UseCors("AllowLocalhost4200");

app.UseHttpsRedirection();

app.MapControllers(); // ❗ Importante

app.Run();
