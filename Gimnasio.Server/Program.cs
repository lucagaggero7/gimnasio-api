using CRUD_PracticaProf.Dapper.ConvertirJson;
using CRUD_PracticaProf.Dapper.ManejadorTipos;
using CRUD_PracticaProf.Datos;
using CRUD_PracticaProf.Datos.Repositorio;
using Dapper;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gimnasio Server", Version = "v1" });

    // Incluye los comentarios XML
    var xmlFile = "Gimnasio.Server.xml"; // <-- nombre de tu archivo XML
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Testing
var MySQLConfig = new MySQLConfig(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(MySQLConfig);

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

app.MapControllers();

app.Run();


