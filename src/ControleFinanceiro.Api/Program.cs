using ControleFinanceiro.Api.Configuration;
using ControleFinanceiro.Api.Extensions;
using ControleFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;

// Configurar o host da aplicação
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ResolveDependencies();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

// Log das origens configuradas
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
logger?.LogInformation("Origens CORS configuradas: {Origins}", string.Join(", ", corsOrigins));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithExposedHeaders("*");
    });
});

// Construir a aplicação
var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// Aplicar CORS antes de outros middlewares
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Inicializar o banco de dados e aplicar migrações
try
{
    await app.InitializeDatabaseAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "Erro ao inicializar o banco de dados");
}

// Iniciar a aplicação
await app.RunAsync();
