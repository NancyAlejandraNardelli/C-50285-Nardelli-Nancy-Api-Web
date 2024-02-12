## DESCRIPCIÓN DE CADA CAPA Y CÓMO SE RELACIONAN

### Controllers: 
En esta capa se colocan los controladores, que son responsables de manejar las solicitudes HTTP.
### DataAccess:
Esta capa se encarga de la interacción con la base de datos. Aquí se definen las entidades de datos(usando Entity Framework), configuración de DbContext.
### DTOs:
En esta capa se colocan las clases DTO. Los Data Transfer Objects (DTO) se utilizan para transferir datos entre capas, ayuda a separar preocupaciones y reducir el tráfico de datos innecesarios entre el cliente y el servidor.
### Models: 
En esta capa se encuentran las clases que representan las entidades de dominio, reflejan la estructura de los datos de la BD .
### Helpers:
Esta capa contiene clases y métodos de utilidad que pueden ser reutilizados en todo el proyecto. Puede contener clases para manejar paginación, autenticación, autorización u otras tares comunes.
### Repositories:
En esta capa se implementa el patrón de repositorio para abstraer el acceso a la base de datos. Los repositorios proporcionan una interfaz para interactuar con las entidades de datos y ocultan los detalles de implementación subyacentes.
### Services:
En esta capa estan los servicios de aplicación. Contiene la lógica de negocio y orquestan la interacción entre los controladores y los repositorios.
