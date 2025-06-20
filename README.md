## Documentación del Proyecto

#### Gestión de estudiantes

Este proyecto es una solución compuesta por un backend el cual está desarrollado casi en su totalidad, endpoints funionales y un frontend que se encuentra en desarrollo para realizar su integración con el backend.

El objetivo principal es gestionar estudiantes y sus inscripciones a materias de forma sencilla, estructurada y escalable.

La API proporciona endpoints para listar vulnerabilidades, registrar vulnerabilidades corregidas, consultar vulnerabilidades no corregidas, obtener una sumatoria de vulnerabilidades por severidad, eliminar vulnerabilidades y consultar vulnerabilidades por su identificador CVE.

La API está documentada con Redoc para facilitar su uso. Utiliza drf-yasg para generar documentación en formato Swagger y Redoc, que proporciona una vista más interactiva de los endpoints que tiene disponibles.

### Funcionalidades

- **Gestión de estudiantes**
  - Crear estudiantes
  - Listar estudiantes con paginación

- **Gestión de materias**
  - Crear materias
  - Listar materias con paginación

- **Inscripción de materias**
  - Inscribir estudiantes a materias, validando reglas de negocio (como máximo 3 materias con más de 4 créditos)
  - Listar materias inscritas por un estudiante
  - Eliminar una inscripción existente

- **Autenticación**
  - Inicio de sesión por email y documento
  - Generación de token JWT
  - Acceso a rutas protegidas con [Authorize]

### Configuración de Inicio del API

- **Clonación del proyecto**: ejecutar desde una terminal git clone https://github.com/Sgutiero/StudentManagement.git y abrir la solución del proyecto estableciendo el StudentManagement.API como proyecto de inicio.
- **Swagger**: Una vez ejecutada la API, se puede acceder a la documentación y probar los endpoints desde Swagger.
