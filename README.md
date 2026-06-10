# Gimnasio API

API REST desarrollada con ASP.NET Core 9 para la gestión integral de un gimnasio.

El sistema permite administrar clientes, membresías, pagos, rutinas, ejercicios y evaluaciones físicas, proporcionando una solución centralizada para el seguimiento de socios y la organización de actividades dentro del gimnasio.

## Tecnologías utilizadas

* ASP.NET Core 9
* C#
* MySQL
* Dapper
* JWT Authentication
* Redis Output Cache
* Swagger / OpenAPI
* GitHub Actions

## Características principales

* Gestión de clientes.
* Gestión de membresías.
* Gestión de pagos.
* Gestión de rutinas de entrenamiento.
* Gestión de ejercicios.
* Gestión de evaluaciones físicas.
* Gestión de tipos de membresía.
* Gestión de formas de pago.
* Autenticación mediante JWT.
* Documentación interactiva con Swagger.
* Caché de respuestas utilizando Redis.
* Integración y despliegue continuo mediante GitHub Actions.

## Estructura del proyecto

```text
Gimnasio.Server
│
├── Controllers
├── Datos
│   ├── Repositorio
│   └── MySQLConfig
│
├── Modelos
│   ├── DTO
│   └── Entidades
│
├── Utilidades
├── Properties
└── Program.cs
```

### Controllers

Contienen los endpoints de la API y gestionan las solicitudes HTTP.

### Repositorios

Implementan el acceso a datos utilizando Dapper para interactuar con MySQL.

### DTOs

Objetos utilizados para la transferencia de datos entre cliente y servidor.

### Modelos

Representan las entidades del dominio del sistema.

## Requisitos

* .NET 9 SDK
* MySQL Server
* Redis (opcional para caché)
* Visual Studio 2022 o superior

## Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/lucagaggero7/gimnasio-api.git
```

### 2. Configurar la cadena de conexión

Editar el archivo `appsettings.json` con los datos correspondientes:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=gimnasio;Uid=usuario;Pwd=password;"
  }
}
```

### 3. Configurar JWT

Completar la configuración de autenticación:

```json
{
  "Jwt": {
    "Key": "clave-secreta",
    "Issuer": "GimnasioAPI",
    "Audience": "GimnasioAPI"
  }
}
```

### 4. Restaurar dependencias

```bash
dotnet restore
```

### 5. Ejecutar la aplicación

```bash
dotnet run
```

## Documentación de la API

Una vez iniciada la aplicación, la documentación estará disponible en:

```text
https://localhost:{puerto}/swagger
```

## Entidades principales

* Cliente
* Membresía
* Tipo de Membresía
* Pago
* Forma de Pago
* Rutina
* Rutina Ejercicio
* Ejercicio
* Evaluación

## Seguridad

La API utiliza autenticación basada en JWT para proteger los endpoints que requieren autorización.

## Integración Continua

El proyecto incluye una configuración de GitHub Actions para automatizar procesos de compilación y despliegue.

## Autor

Stefano Luca Gaggero

.NET Full Stack Developer
