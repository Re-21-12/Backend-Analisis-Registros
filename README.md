# Backend-Analisis-Registros

```
Ejecutar para hacer build
dotnet publish -c Release -o ./publish

Construir imagen
docker build -t backend-analisis .

etiquetar imagen
docker tag backend-analisis revic2112/dev-analisis-backend:latest

pushear imagen
docker push revic2112/dev-analisis-backend:latest
```
