using Dapper;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;
using Gimnasio.Server.Datos;
using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Services.Dapper.ManejadorTipos;
using Gimnasio.Server.Services.Dapper.ConvertirJson;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.Expire(TimeSpan.FromDays(7));
    });
});

builder.Services.AddStackExchangeRedisOutputCache(opciones =>
{
    opciones.Configuration = builder.Configuration.GetConnectionString("redis");

});

// Add services to the container.

var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(origenesPermitidos)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

Console.WriteLine("Orígenes permitidos: " + string.Join(", ", origenesPermitidos));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new FechaJson());
        options.JsonSerializerOptions.Converters.Add(new HoraJson());
    });


//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var env = builder.Environment;

    var title = "Gimnasio Server";
    if (env.IsDevelopment())
    {
        title += " (Development)";
    }

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = title,
        Version = "v1"
    });

    var xmlFile = "Gimnasio.Server.xml"; 
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Testing
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
if (string.IsNullOrEmpty(connectionString))
{
    var environment = builder.Environment.EnvironmentName;
    var message = environment == "Development"
        ? "MySQL connection string not found at Development env"
        : "MySQL connection string not found. Check App Service";

    throw new InvalidOperationException(message);
}

var mySQLConfig = new MySQLConfig(connectionString);
builder.Services.AddSingleton(mySQLConfig);

SqlMapper.AddTypeHandler(new Fecha());
SqlMapper.AddTypeHandler(new Hora());

//Dev
//builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MYSqlConnection")
//));

builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<IEjerciciosRepositorio, EjerciciosRepositorio>();
builder.Services.AddScoped<IPagosRepositorio, PagosRepositorio>();
builder.Services.AddScoped<IFormasPagoRepositorio, FormasPagoRepositorio>();
builder.Services.AddScoped<IMembresiasRepositorio, MembresiasRepositorio>();
builder.Services.AddScoped<ITiposMembresiaRepositorio, TiposMembresiaRepositorio>();
builder.Services.AddScoped<IRutinasRepositorio, RutinasRepositorio>();
builder.Services.AddScoped<IEvaluacionesRepositorio, EvaluacionesRepositorio>();
builder.Services.AddScoped<IEjerciciosPorRutinaRepositorio, EjerciciosPorRutinaRepositorio>();
builder.Services.AddScoped<ITiposRutinaRepositorio, TiposRutinaRepositorio>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

app.Run();


