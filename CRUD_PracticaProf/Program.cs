using CRUD_PracticaProf.Datos;
using CRUD_PracticaProf.Datos.Repositorio;
using MySql.Data.MySqlClient;


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


builder.Services.AddControllers();


//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Testing
var MySQLConfig = new MySQLConfig(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(MySQLConfig);

//Dev
//builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MYSqlConnection")
//));

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

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
