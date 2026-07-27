

# API Gestión de Usuarios 

Proyecto desarrollado como parte de la certificación de Coursera en **Back-End Development with .NET**.  
La API permite gestionar usuarios mediante operaciones CRUD y está construida con **ASP.NET Core** y **C#**.

##  Características
- CRUD completo de usuarios (`GET`, `POST`, `PUT`, `DELETE`).
- Middleware personalizado para:
  - Manejo global de errores.
  - Logging de solicitudes.
  - Autenticación por token.
- Inyección de dependencias con `IUserService` y `UserService`.
- Documentación automática con **Swagger**.
- Modelo `User` con propiedades básicas: `Id`, `Name`, `Age`, `Email`.

##  Estructura del proyecto
- **Controllers/** → Controladores de la API (ej. `UserController`).
- **Models/** → Modelos de datos (ej. `User`).
- **Services/** → Lógica de negocio (`IUserService`, `UserService`).
- **Middleware/** → Middlewares personalizados (Errores, Logging, Token).
- **Program.cs** → Configuración principal de la aplicación.


##  Endpoints disponibles
- **GET /api/User** → Lista todos los usuarios.
- **GET /api/User/{id}** → Obtiene un usuario por Id.
- **POST /api/User** → Crea un nuevo usuario.
- **PUT /api/User/{id}** → Actualiza un usuario existente por su Id.
- **DELETE /api/User/{id}** → Elimina un usuario.

##  Tecnologías usadas
- ASP.NET Core
- C# (programación orientada a objetos)
- Swagger para documentación
- Middleware personalizado
- Inyección de dependencias


##  Cómo ejecutar
1. Clona el repositorio:
   ```bash
   git clone https://github.com/SebasARo/API-Gestion-Usuario.git
