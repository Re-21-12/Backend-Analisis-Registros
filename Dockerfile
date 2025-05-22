# Usa la imagen oficial de .NET para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia los archivos del proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o ./publish

# Usa la imagen de runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Configura el entorno como Production
ENV ASPNETCORE_ENVIRONMENT=Staging

# Expone el puerto en el que corre tu aplicación
EXPOSE 5035

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Backend Analisis.dll"]