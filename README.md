# MyBackend

Este proyecto es la parte de backend para la aplicaci�n de streaming de pel�culas y series Veuw. Est� desarrollado utilizando .NET Core 8 y Entity Framework Core.

## Descripci�n

MyBackend proporciona la funcionalidad de backend para la aplicaci�n Veuw, incluyendo autenticaci�n de usuarios, gesti�n de favoritos y almacenamiento de datos de pel�culas y series.

## Caracter�sticas

- **Autenticaci�n y Autorizaci�n**: Implementado con JWT para manejar la autenticaci�n y autorizaci�n de usuarios.
- **Gesti�n de Usuarios**: Registro y login de usuarios.
- **Favoritos**: Los usuarios pueden agregar pel�culas y series a su lista de favoritos.
- **Visualizaci�n de Thumbnails**: Carga de im�genes en miniatura de pel�culas y series.

## Tecnolog�as Utilizadas

- **.NET Core 8**
- **Entity Framework Core**
- **T-SQL y C#**
- **SQL Server**
- **JWT (JSON Web Tokens)**
- **Visual Studio 2022**

## Instalaci�n

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
3. Configura la cadena de conexi�n a tu base de datos SQL Server en el archivo `appsettings.json`. Crea un archivo llamado `appsettings.json.example` en la ra�z de tu proyecto con el siguiente contenido (sin valores sensibles):
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
5. Ejecuta la aplicaci�n:
    ```sh
    dotnet run
    ```
6. La aplicaci�n estar� disponible en `https://localhost:7284`.

## Uso

1. Al iniciar la aplicaci�n, puedes registrar nuevos usuarios mediante el endpoint `/api/auth/register`.
2. Inicia sesi�n con un usuario registrado mediante el endpoint `/api/auth/login` para obtener un token JWT.
3. Utiliza el token JWT para autenticarte y acceder a los dem�s endpoints (por ejemplo, agregar favoritos).

### Endpoints Disponibles

- **Autenticaci�n**
  - `POST /api/auth/login`: Inicia sesi�n y obtiene un token JWT.
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

## Proceso que segu� para hacerlo

1. Configur� un nuevo proyecto de .NET Core en Visual Studio 2022.
2. A�ad� Entity Framework Core y configur� la cadena de conexi�n a SQL Server.
3. Implement� los modelos de datos (`Usuario`, `Favorite`, `Thumbnail`).
4. Configur� la autenticaci�n JWT.
5. Cre� los controladores para manejar las solicitudes HTTP.
6. Prob� los endpoints utilizando herramientas como Postman.

## 4to Sprint Review

| �Qu� sali� bien? | �Qu� puedo hacer diferente? | �Qu� no sali� bien? |
|------------------|-----------------------------|---------------------|
| Se logr� integrar la base de datos para el login y registro. | Mejorar la validaci�n de datos al registrarse. | Tuve algunos problemas con la configuraci�n inicial de la base de datos. |
| Se implement� la autenticaci�n con JWT. | Aprender m�s sobre seguridad en aplicaciones web. | Al principio tuve problemas para manejar la autenticaci�n con JWT. |
| Se muestran pel�culas y series en el home desde la base de datos. | Identificar mejor los problemas. | Me llev� mucho tiempo lograr que se pudieran agregar a favoritos. |
| Se mejor� la seguridad del login y registro. |  |  |
| Se logr� una conexi�n entre ambas APIs para poder guardar las pel�culas y series en la base de datos dependiendo del ID del usuario. |  |  |
| Ya no se guardan datos de manera local. |  |  |
| Se implement� el backend utilizando C# y .NET para evitar el retrabajo. |  |  |

## Diagrama de Entidad-Relaci�n de la Base de Datos

(Diagrama de ER aqu�)

## Problemas Conocidos

- La autenticaci�n JWT puede requerir ajustes adicionales para asegurar la m�xima seguridad.
- La p�gina puede mejorar en rendimiento.

## Retrospectiva

- **�Qu� sali� bien?**
  - La integraci�n de la base de datos y la autenticaci�n JWT se lograron con �xito.
  - Se mejor� la seguridad del login y registro.
  - Se implement� una conexi�n efectiva entre el frontend y el backend.

- **�Qu� no sali� bien?**
  - La implementaci�n de los favoritos tom� m�s tiempo debido a problemas con la obtenci�n del UserId.

- **�Qu� puedo hacer diferente?**
  - Mejorar la validaci�n de datos durante el registro de usuarios.
  - Aprender m�s sobre seguridad en aplicaciones web para fortalecer la autenticaci�n JWT.
