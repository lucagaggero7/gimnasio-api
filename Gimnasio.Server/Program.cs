using Dapper;
using Gimnasio.Server.Controllers;
using Gimnasio.Server.Datos;
using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Services.Dapper.ConvertirJson;
using Gimnasio.Server.Services.Dapper.ManejadorTipos;
using Gimnasio.Server.Servicios.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.Expire(TimeSpan.FromDays(7));
    });
});

var redisConn = builder.Configuration.GetConnectionString("redis");

if (string.IsNullOrWhiteSpace(redisConn))
{
    builder.Services.AddOutputCache(options =>
    {
        options.DefaultExpirationTimeSpan = TimeSpan.FromDays(7);
    });

    Console.WriteLine("Usando OutputCache local.");
}
else
{
    builder.Services.AddStackExchangeRedisOutputCache(options =>
    {
        var config = ConfigurationOptions.Parse(redisConn);
        config.ReconnectRetryPolicy = new ExponentialRetry(1000, 30000);
        config.CommandMap = CommandMap.Create(
            new HashSet<string> { "INFO", "CONFIG", "CLUSTER", "PING", "ECHO", "CLIENT" },
            available: false
        );
        config.SocketManager = SocketManager.Shared;
        options.ConfigurationOptions = config;
    });

    Console.WriteLine("Usando OutputCache distribuido.");
}


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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token con el formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
});

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

//JWT

builder.Services.AddSingleton<JwtService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
//

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
builder.Services.AddScoped<IRutinaEjercicioRepositorio, RutinaEjercicioRepositorio>();
builder.Services.AddScoped<ITiposRutinaRepositorio, TiposRutinaRepositorio>();

builder.Services.AddHttpClient<CodigosAreaControllers>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

app.Run();


