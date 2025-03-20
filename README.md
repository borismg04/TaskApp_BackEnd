# TaskAppBackEnd

Este proyecto ha sido desplegado en producción en Heroku. Puedes acceder al servicio en el siguiente enlace:
[https://taskappbackend-4929b0971b62.herokuapp.com/](https://localhost:7121/)

- email: admin@ad.com
- password: admnistrator

## Descripción

TaskAppBackEnd es un servicio backend para la gestión de tareas y autenticación de usuarios. Este proyecto está desarrollado en C# utilizando .NET 8 y Entity Framework Core para la gestión de la base de datos.

## Características

- Autenticación de usuarios
- Gestión de tareas
- Registro de errores y logs

## Estructura del Proyecto

El proyecto está organizado en las siguientes carpetas y archivos:

1. **Models**: Contiene las clases de modelo para los usuarios y las tareas.
2. **Services**: Contiene los servicios para la autenticación y la gestión de respuestas.
3. **Interfaces**: Contiene las interfaces que definen los contratos para los servicios.
4. **Controllers**: Contiene los controladores para manejar las solicitudes HTTP.

Nota : En el proyecto hay un documento donde estan todas las colecciones de postman para probar los endpoints.
Nombre : TaskApp.postman_collection.json para descargar.

![image](https://github.com/user-attachments/assets/04095ab1-1223-46e2-940e-cb4afab9a8bf)


## Instalación

1. Clona el repositorio:

   git clone https://github.com/borismg04/TaskApp_BackEnd.git

2. Navega al directorio del proyecto:

   cd TaskApp_BackEnd
   
3. Restaura los paquetes NuGet:
   
4. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`.

5. Aplica las migraciones a la base de datos:

   dotnet ef database update


## Uso

Para ejecutar el proyecto, utiliza el siguiente comando:

dotnet run


El servicio estará disponible en `http://localhost:5000`.

## Endpoints

### 1. Autenticación

- **POST /api/login**
    - **Descripción**: Autentica a un usuario.
    - **Parámetros**:
        - `email`: Correo electrónico del usuario.
        - `password`: Contraseña del usuario.
    - **Respuesta**: `ReponseModel` con la información del usuario autenticado.

### 2. Tareas

- **GET /api/tasks**
    - **Descripción**: Obtiene todas las tareas.
    - **Respuesta**: Lista de `TaskModel`.

- **POST /api/tasks**
    - **Descripción**: Crea una nueva tarea.
    - **Parámetros**: `TaskModel` con la información de la tarea.
    - **Respuesta**: `ReponseModel` con la tarea creada.

## Controladores

### 1. LoginController

#### Descripción

El `LoginController` es responsable de manejar las solicitudes HTTP relacionadas con la autenticación de usuarios. Proporciona un endpoint para autenticar a los usuarios.

#### Endpoints

- **Endpoint**: `GET /LoginController/login`
    - **Descripción**: Autentica a un usuario.
    - **Parámetros**:
        - `email` (query): Correo electrónico del usuario.
        - `pass` (query): Contraseña del usuario.
    - **Respuesta**: `ReponseModel` con la información del usuario autenticado.

### 2. TaskController

#### Descripción

El `TaskController` es responsable de manejar las solicitudes HTTP relacionadas con la gestión de tareas. Proporciona endpoints para obtener, crear, actualizar y eliminar tareas.

#### Endpoints

- **Endpoint**: `GET /TaskController/GetTasks`
    - **Descripción**: Obtiene todas las tareas.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
    - **Respuesta**: `ReponseModel` con la lista de tareas.

- **Endpoint**: `GET /TaskController/GetTaskAdmin`
    - **Descripción**: Obtiene todas las tareas para el administrador.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
    - **Respuesta**: `ReponseModel` con la lista de tareas para el administrador.

- **Endpoint**: `POST /TaskController/CreateTask`
    - **Descripción**: Crea una nueva tarea.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
        - `task` (body): Modelo de la tarea a crear.
    - **Respuesta**: `ReponseModel` con la tarea creada.

- **Endpoint**: `POST /TaskController/UpdateTask`
    - **Descripción**: Actualiza una tarea existente.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
        - `task` (body): Modelo de la tarea a actualizar.
    - **Respuesta**: `ReponseModel` con la tarea actualizada.

- **Endpoint**: `DELETE /TaskController/DeleteTask`
    - **Descripción**: Elimina una tarea existente.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
        - `id` (query): ID de la tarea a eliminar.
    - **Respuesta**: `ReponseModel` con el resultado de la eliminación.

### 3. UsersController

#### Descripción

El `UsersController` es responsable de manejar las solicitudes HTTP relacionadas con la gestión de usuarios. Proporciona endpoints para obtener, actualizar y eliminar usuarios.

#### Endpoints

- **Endpoint**: `GET /UsersController/GetUsuarios`
    - **Descripción**: Obtiene todos los usuarios.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
    - **Respuesta**: `ReponseModel` con la lista de usuarios.

- **Endpoint**: `POST /UsersController/UpdateUser`
    - **Descripción**: Actualiza un usuario existente.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
        - `id` (query): ID del usuario a actualizar.
        - `user` (body): Modelo del usuario a actualizar.
    - **Respuesta**: `ReponseModel` con el usuario actualizado.

- **Endpoint**: `DELETE /UsersController/DeleteUser`
    - **Descripción**: Elimina un usuario existente.
    - **Parámetros**:
        - `email` (header): Correo electrónico del usuario.
        - `pass` (header): Contraseña del usuario (opcional).
        - `id` (query): ID del usuario a eliminar.
    - **Respuesta**: `ReponseModel` con el resultado de la eliminación.


## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.
