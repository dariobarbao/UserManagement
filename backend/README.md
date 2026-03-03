# Users Management – Technical Test (Backend)

Backend desarrollado como parte de la prueba técnica.

El proyecto está construido con **.NET 8**, siguiendo principios de **Clean Architecture**, utilizando **Entity Framework Core**, **SQLite**, **JWT Authentication** y **Docker** para facilitar su despliegue y evaluación.

---

## 🛠️ Stack Tecnológico

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- MediatR
- Docker & Docker Compose
- xUnit + Moq (Unit Tests)

---

## 🧱 Arquitectura

El proyecto sigue una **Clean Architecture**, separando responsabilidades en las siguientes capas:

- **Domain**: Entidades y lógica de dominio
- **Application**: Casos de uso, CQRS, validaciones y contratos
- **Persistence**: EF Core, DbContext, repositorios
- **WebAPI**: Endpoints, autenticación, middlewares

Esta estructura facilita el mantenimiento, testeo y escalabilidad del sistema.

---

## ✅ Requisitos

- Docker Desktop
- Docker Compose

---

## 🚀 Cómo ejecutar el proyecto

Desde la carpeta `backend`:

```bash
docker compose up --build
```
http://localhost:5000/swagger

---

## 💾 Persistencia de datos

El proyecto utiliza **SQLite** como base de datos, almacenada en un volumen Docker para garantizar persistencia entre reinicios del contenedor.

La base de datos se crea automáticamente al iniciar la aplicación mediante migraciones de Entity Framework Core.

---

## 🔐 Autenticación

La API implementa autenticación basada en **JWT (JSON Web Tokens)**.

### Flujo:
1. Registro de usuario
2. Login
3. Recepción de token JWT
4. Uso del token en endpoints protegidos mediante el header: Authorization: Bearer {token}

- Se incluye un usuario administrador creado automáticamente al iniciar la aplicación:
  - Email: admin@mail.com
  - Password: Admin123!

---

## 📌 Endpoints principales

### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

### Users
- `GET /api/users`
- `GET /api/users/{id}`
- `POST /api/users`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

---

## 🧪 Testing

Se incluyen pruebas unitarias para los principales casos de uso, especialmente en el módulo de autenticación.

Frameworks utilizados:
- xUnit
- Moq

---

## 📝 Notas

- El proyecto está dockerizado para facilitar su evaluación.
- Las variables sensibles (JWT, conexión a base de datos) se gestionan mediante variables de entorno.
- Se priorizó claridad, mantenibilidad y buenas prácticas sobre complejidad innecesaria.


