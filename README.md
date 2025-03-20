## Super Admin: 

	- email: admin@ad.com
	- password: admnistrator


El proyecto *TaskApp_BackEnd* es una aplicación de gestión de tareas desarrollada con Node.js y Express. Proporciona una API RESTful para manejar tareas, usuarios y autenticación.

## Características

- *Gestión de tareas*: creación, lectura, actualización y eliminación de tareas.
- *Gestión de usuarios*: registro, inicio de sesión y perfiles de usuario.
- *Autenticación*: manejo de tokens JWT para la autenticación de usuarios.

## Requisitos previos

- Node.js (versión 14 o superior)
- npm (versión 6 o superior)
- MongoDB (versión 4 o superior)

## Instalación

1. Clona el repositorio:

   bash
   git clone https://github.com/borismg04/TaskApp_BackEnd.git

2. Navega al directorio del proyecto:
   bash
   cd TaskApp_BackEnd

3. Instala las dependencias:

   bash
   npm install

## Configuración

1. Crea un archivo .env en la raíz del proyecto con las siguientes variables de entorno:

   env
   PORT=3000
   MONGODB_URL=mongodb://localhost:27017/taskapp
   JWT_SECRET=tu_secreto_jwt
   
   - PORT: Puerto en el que se ejecutará la aplicación.
   - MONGODB_URL: URL de conexión a la base de datos MongoDB.
   - JWT_SECRET: Clave secreta para la firma de tokens JWT.

## Uso

1. Inicia el servidor:

   bash
   npm start

2. La API estará disponible en http://localhost:3000.

## Endpoints principales

- *Usuarios*:
  - POST /users: Registrar un nuevo usuario.
  - POST /users/login: Iniciar sesión de un usuario.
  - GET /users/me: Obtener el perfil del usuario autenticado.
  - POST /users/logout: Cerrar sesión del usuario autenticado.

- *Tareas*:
  - POST /tasks: Crear una nueva tarea.
  - GET /tasks: Obtener todas las tareas del usuario autenticado.
  - GET /tasks/:id: Obtener una tarea por su ID.
  - PATCH /tasks/:id: Actualizar una tarea por su ID.
  - DELETE /tasks/:id: Eliminar una tarea por su ID.

## Dependencias principales

- *express*: Framework web para Node.js.
- *mongoose*: ODM para MongoDB y Node.js.
- *jsonwebtoken*: Implementación de JSON Web Tokens.
- *bcryptjs*: Biblioteca para hashing de contraseñas.
- *dotenv*: Carga variables de entorno desde un archivo .env.

## Desarrollo

Para iniciar el servidor en modo de desarrollo con recarga automática:

bash
npm run dev

Este comando utiliza *nodemon* para reiniciar el servidor automáticamente cuando se detectan cambios en el código.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama: git checkout -b feature/nueva-funcionalidad.
3. Realiza tus cambios y haz commits: git commit -m 'Añadir nueva funcionalidad'.
4. Envía tus cambios al repositorio remoto: git push origin feature/nueva-funcionalidad.
5. Abre un Pull Request en GitHub.

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.

---

Este archivo README.md proporciona una visión general del proyecto *TaskApp_BackEnd*, incluyendo su instalación, configuración y uso. Para más detalles, consulta la documentación del código fuente y los comentarios en el mismo.
