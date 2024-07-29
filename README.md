# MyBackend

Este proyecto es la parte de backend para la aplicación de streaming de películas y series Veuw. Está desarrollado utilizando .NET Core 8 y Entity Framework Core.

## Descripción

MyBackend proporciona la funcionalidad de backend para la aplicación Veuw, incluyendo autenticación de usuarios, gestión de favoritos y almacenamiento de datos de películas y series.

## Características

- **Autenticación y Autorización**: Implementado con JWT para manejar la autenticación y autorización de usuarios.
- **Gestión de Usuarios**: Registro y login de usuarios.
- **Favoritos**: Los usuarios pueden agregar películas y series a su lista de favoritos.
- **Visualización de Thumbnails**: Carga de imágenes en miniatura de películas y series.

## Tecnologías Utilizadas

- **.NET Core 8**
- **Entity Framework Core**
- **T-SQL y C#**
- **SQL Server**
- **JWT (JSON Web Tokens)**
- **Visual Studio 2022**

## Instalación

### Requisitos Previos

1. [Instalar .NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Pasos

1. Clona el repositorio:
    ```sh
    git clone https://github.com/tu-usuario/mybackend.git
    ```
2. Navega al directorio del proyecto:
    ```sh
    cd mybackend
    ```
3. Configura la cadena de conexión a tu base de datos SQL Server en el archivo `appsettings.json`. Crea un archivo llamado `appsettings.json.example` en la raíz de tu proyecto con el siguiente contenido (sin valores sensibles):
    ```json
    {
      "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
      },
      "Jwt": {
          "Key": "YOUR_JWT_KEY",
          "Issuer": "YOUR_JWT_ISSUER",
          "Audience": "YOUR_JWT_AUDIENCE"
      },
      "Logging": {
          "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
          }
      },
      "AllowedHosts": "*"
    }
    ```
4. Aplica las migraciones para crear la base de datos:
    ```sh
    dotnet ef database update
    ```
5. Ejecuta la aplicación:
    ```sh
    dotnet run
    ```
6. La aplicación estará disponible en `https://localhost:7284`.

## Uso

1. Al iniciar la aplicación, puedes registrar nuevos usuarios mediante el endpoint `/api/auth/register`.
2. Inicia sesión con un usuario registrado mediante el endpoint `/api/auth/login` para obtener un token JWT.
3. Utiliza el token JWT para autenticarte y acceder a los demás endpoints (por ejemplo, agregar favoritos).

### Endpoints Disponibles

- **Autenticación**
  - `POST /api/auth/login`: Inicia sesión y obtiene un token JWT.
  - `POST /api/auth/register`: Registra un nuevo usuario.

- **Usuarios**
  - `GET /api/usuarios`: Obtiene la lista de usuarios.
  - `POST /api/usuarios`: Crea un nuevo usuario.

- **Thumbnails**
  - `GET /api/thumbnails`: Obtiene la lista de thumbnails.
  - `POST /api/thumbnails`: Crea un nuevo thumbnail.

- **Favoritos**
  - `GET /api/favorites`: Obtiene la lista de favoritos.
  - `POST /api/favorites`: Agrega un nuevo favorito.
  - `DELETE /api/favorites/{id}`: Elimina un favorito por ID.

## Proceso que seguí para hacerlo

1. Configuré un nuevo proyecto de .NET Core en Visual Studio 2022.
2. Añadí Entity Framework Core y configuré la cadena de conexión a SQL Server.
3. Implementé los modelos de datos (`Usuario`, `Favorite`, `Thumbnail`).
4. Configuré la autenticación JWT.
5. Creé los controladores para manejar las solicitudes HTTP.
6. Probé los endpoints utilizando herramientas como Postman.

## 4to Sprint Review

| ¿Qué salió bien? | ¿Qué puedo hacer diferente? | ¿Qué no salió bien? |
|------------------|-----------------------------|---------------------|
| Se logró integrar la base de datos para el login y registro. | Mejorar la validación de datos al registrarse. | Tuve algunos problemas con la configuración inicial de la base de datos. |
| Se implementó la autenticación con JWT. | Aprender más sobre seguridad en aplicaciones web. | Al principio tuve problemas para manejar la autenticación con JWT. |
| Se muestran películas y series en el home desde la base de datos. | Identificar mejor los problemas. | Me llevó mucho tiempo lograr que se pudieran agregar a favoritos. |
| Se mejoró la seguridad del login y registro. |  |  |
| Se logró una conexión entre ambas APIs para poder guardar las películas y series en la base de datos dependiendo del ID del usuario. |  |  |
| Ya no se guardan datos de manera local. |  |  |
| Se implementó el backend utilizando C# y .NET para evitar el retrabajo. |  |  |

## Diagrama de Entidad-Relación de la Base de Datos

(Diagrama de ER aquí)

## Problemas Conocidos

- La autenticación JWT puede requerir ajustes adicionales para asegurar la máxima seguridad.
- La página puede mejorar en rendimiento.

## Retrospectiva

- **¿Qué salió bien?**
  - La integración de la base de datos y la autenticación JWT se lograron con éxito.
  - Se mejoró la seguridad del login y registro.
  - Se implementó una conexión efectiva entre el frontend y el backend.

- **¿Qué no salió bien?**
  - La implementación de los favoritos tomó más tiempo debido a problemas con la obtención del UserId.

- **¿Qué puedo hacer diferente?**
  - Mejorar la validación de datos durante el registro de usuarios.
  - Aprender más sobre seguridad en aplicaciones web para fortalecer la autenticación JWT.
