# Technical Test - User Management System

## 📄 Descripción del proyecto

Este repositorio contiene la solución a la prueba técnica para un sistema de administración de usuarios, compuesta por:

- **Backend**: API REST desarrollada en .NET 8 con Clean Architecture, autenticación JWT y persistencia en SQLite.
- **Frontend**: SPA desarrollada en Angular 20 que consume la API.
- **Dockerización completa** para facilitar la ejecución y evaluación del proyecto.

El objetivo del proyecto es gestionar usuarios con autenticación segura y operaciones CRUD protegidas.

.
├── backend/
├── frontend/
└── docker-compose.yml

---

## 🛠️ Tecnologías utilizadas

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- MediatR
- xUnit + Moq
- Docker

## 🏗️ Arquitectura

El backend sigue principios de **Clean Architecture**, separando:

- Domain
- Application
- Infrastructure
- API

Se utilizan:
- CQRS con MediatR
- Inyección de dependencias
- Separación de responsabilidades
- Principios SOLID

### Frontend
- Angular 20
- Bootstrap
- Standalone Components
- Reactive Forms
- HTTP Interceptors
- Route Guards
- Docker + Nginx

---

## 🚀 Ejecución local (sin Docker)

### Backend

1. Abrir la carpeta `backend`.
2. Crear archivo `.env` a partir del ejemplo:

```bash
cp .env.example .env
```

3. Ejecutar:

```bash
dotnet restore
dotnet run
```

La API quedará disponible en:
```
http://localhost:{puerto}/swagger
```

### Frontend

1. Ir a `frontend/users-management-frontend`
2. Instalar dependencias:

```bash
npm install
```

3. Ejecutar:

```bash
npm start
```

Frontend disponible en:
```
http://localhost:4200
```

---

## 🐳 Ejecución con Docker (recomendado)

### Requisitos
- Docker
- Docker Compose

### Pasos

1. Desde la raíz del proyecto, crear el archivo de entorno del backend:

```bash
cp backend/.env.example backend/.env
```

2. Ejecutar:

```bash
docker compose up --build
```

### Accesos

- **API / Swagger**
```
http://localhost:5000/swagger
```

- **Frontend**
```
http://localhost:4200
```

---

## ⚙️ Configuración (Variables de entorno)

Archivo `backend/.env`:

```env
DB_PATH=/data/users.db

JWT_KEY=THIS_IS_A_MINIMUM_32_CHAR_SECRET_KEY
JWT_ISSUER=Technicaltest
JWT_AUDIENCE=Technicaltest.Users
JWT_EXPIRE_MINUTES=60
```

---

## 🔐 Autenticación

La API utiliza **JWT (JSON Web Tokens)**.

### Credenciales de prueba (seed automático)

```text
Email: admin@mail.com
Password: Admin123!
```

Este usuario se crea automáticamente al iniciar la aplicación.

---

## 🔄 Flujo de autenticación

1. El usuario realiza login.
2. El backend genera un JWT firmado.
3. El frontend almacena el token.
4. El token se envía en cada request en el header:

Authorization: Bearer {token}

---

## 📌 Endpoints de la API

Todos los endpoints están documentados en Swagger:

```
http://localhost:{puerto}/swagger
```

### Auth
- POST `/api/auth/login`
- POST `/api/auth/register`

### Users (protegidos con JWT)
- GET `/api/users`
- GET `/api/users/{id}`
- POST `/api/users`
- PUT `/api/users/{id}`
- DELETE `/api/users/{id}`

---

## 🧪 Ejecución de pruebas

Las pruebas unitarias se encuentran en el proyecto:

```
UserManagement.UnitTest
```

Para ejecutarlas:

```bash
dotnet test
```

Incluyen pruebas para:
- Login
- Registro de usuario
- Validaciones de autenticación

---

## 🧠 Decisiones técnicas

- Se eligió SQLite para simplificar la evaluación y evitar dependencias externas.
- Se utilizó Clean Architecture para mantener desacoplamiento y testabilidad.
- JWT se implementó por ser un estándar ampliamente utilizado en APIs REST.
- Docker permite ejecución inmediata sin configuración manual.

---

## 📝 Notas finales

- La base de datos SQLite se persiste mediante volúmenes Docker.
- El proyecto está preparado para evaluación técnica inmediata.
- Se priorizó arquitectura limpia, seguridad y buenas prácticas.

---

## 👤 Autor

**Dario Correa Vélez**  
Desarrollador FullStack  