# Usa .NET 8.0 SDK como imagen base para la construcci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura las dependencias
COPY ["MyBackend.csproj", "."]
RUN dotnet restore "MyBackend.csproj"

# Copia el resto de los archivos y compila la aplicaci�n
COPY . .
RUN dotnet build "MyBackend.csproj" -c Release -o /app/build

# Publica la aplicaci�n en la carpeta /app/publish
RUN dotnet publish "MyBackend.csproj" -c Release -o /app/publish

# Usa .NET 8.0 runtime como imagen base para la ejecuci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exponer el puerto 8080
EXPOSE 8080

# Copiar los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# Ejecutar la aplicaci�n
ENTRYPOINT ["dotnet", "MyBackend.dll"]
