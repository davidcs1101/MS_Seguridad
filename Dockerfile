# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el código al contenedor
COPY . .

# Publicar la API (indicando la carpeta correcta del proyecto)
RUN dotnet publish SEG.Api.Seguridad/SEG.Api.Seguridad.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/out .

# Configurar puerto de ejecución (Render usa la variable $PORT)
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080

# Ejecutar la API
ENTRYPOINT ["dotnet", "SEG.Api.Seguridad.dll"]
