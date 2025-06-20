## Documentación del Proyecto

#### Gestión de estudiantes

Este proyecto es una solución compuesta por un backend el cual está desarrollado casi en su totalidad, endpoints funionales y un frontend que se encuentra en desarrollo para realizar su integración con el backend.

El objetivo principal es gestionar estudiantes y sus inscripciones a materias de forma sencilla, estructurada y escalable.

Está desarrollado siguiendo los principios de arquitectura limpia, lo cual permite una alta separación de responsabilidades, escalabilidad y facilidad de mantenimiento. Además, se emplean patrones y enfoques modernos para el desarrollo backend en .NET:

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

- **Clonación del proyecto**: ejecutar desde un terminal el comando:
  git clone https://github.com/Sgutiero/StudentManagement.git y abrir la solución del proyecto estableciendo el StudentManagement.API como proyecto de inicio.
- **Swagger**: Una vez ejecutada la API, se puede acceder a la documentación y probar los endpoints desde Swagger.
