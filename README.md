# Clientes API y Blazor

Aplicación local de administración de clientes: API REST en ASP.NET Core/.NET 10, frontend Blazor Server, SQL Server y Docker Compose.

## Requisitos

- Docker Desktop con el motor Linux activo y Docker Compose.
- SQL Server 2022 instalado en Windows, con TCP/IP habilitado y escuchando en el puerto `1433`.
- SQL Server Management Studio (SSMS).

La aplicación en Docker se conecta al SQL Server del host mediante `host.docker.internal`. Compose no crea ni ejecuta un contenedor SQL Server.

## 1. Crear la base local

En SSMS, conectado a la instancia local, ejecuta [database/create-dbclientes.sql](database/create-dbclientes.sql). El script crea `DBClientes` y `dbo.clientes` solo si aún no existen. No elimina ni altera otras bases.

## 2. Preparar el usuario SQL de la API

La API debe usar el login SQL `clientes_api`, con permisos CRUD únicamente sobre `DBClientes.dbo.clientes`.

Si todavía no existe, abre [database/create-clientes-api-login.template.sql](database/create-clientes-api-login.template.sql) en SSMS, reemplaza el marcador de contraseña directamente en el editor y ejecuta el script. No guardes el archivo con la contraseña real. El script comprueba la configuración de autenticación y se detiene si el login o el usuario ya existen; no cambia opciones globales de SQL Server. Si el login ya está creado, no vuelvas a ejecutar esa plantilla.

## 3. Configurar el secreto local

Desde la raíz del proyecto, copia `.env.example` como `.env` y sustituye el valor de ejemplo de `DB_CONNECTION_STRING` por la contraseña local que configuraste para `clientes_api`. No compartas ni confirmes `.env` en Git.

La cadena de ejemplo cifra la conexión, pero usa `TrustServerCertificate=True`, apropiado solo para esta configuración local de pruebas. Para validar el certificado TLS, configura un certificado confiable en SQL Server y cambia esa opción a `False`.

## 4. Iniciar la aplicación

Desde la raíz:

```powershell
docker compose up -d --build
docker compose ps
```

URLs locales:

- Blazor: <http://localhost:8081>
- Administración de clientes: <http://localhost:8081/clientes>
- API: <http://localhost:8082/api/Clientes>

La URL de la API dentro de la red Compose es `http://api:8080/`; Blazor usa esa dirección para las solicitudes servidor-servidor.

Para ver los registros de los servicios:

```powershell
docker compose logs -f api frontend
```

Para detener los contenedores:

```powershell
docker compose down
```

La base de datos continúa en el SQL Server local de Windows al detener o reconstruir los contenedores.

## Desarrollo sin Docker

La API lee `ConnectionStrings:DefaultConnection` y Blazor lee `ClientesAPI:BaseAddress` de la configuración. No incluyas credenciales en `appsettings*.json`; suministra los valores desde configuración local segura o variables de entorno. Para Docker Compose, configura únicamente `.env`.

## Preparar el primer envío a GitHub

Antes de crear un commit, verifica el estado y que los archivos locales sensibles estén ignorados:

```powershell
git status --short
git check-ignore .env
git check-ignore ClientesAPI/Properties/PublishProfiles
```

Si los archivos esperados aparecen como ignorados y la lista de cambios contiene solo archivos de proyecto, puedes preparar y confirmar el primer commit:

```powershell
git add -A
git diff --cached --stat
git status --short
git commit -m "Preparar aplicación local con Docker Compose"
```

Antes de publicar un repositorio remoto, revisa el contenido del commit y confirma que no contiene `.env`, perfiles de publicación, archivos de usuario, `bin` ni `obj`. No publiques hasta configurar el remoto correcto y revisar su visibilidad.
