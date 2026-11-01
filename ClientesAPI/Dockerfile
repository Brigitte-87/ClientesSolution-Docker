
# ETAPA 1: COMPILAR NUESTRA API CON .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copiar el archivo del proyecto
COPY ClientesAPI.csproj ./

# Restaurar dependencias
RUN dotnet restore ClientesAPI.csproj

# Copiar el código fuente
COPY . .

# Publicar la API
RUN dotnet publish ClientesAPI.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false


# ETAPA 2: EJECUTAR LA API
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# Configurar el puerto del contenedor
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Iniciar nuestra API
ENTRYPOINT ["dotnet", "ClientesAPI.dll"]
