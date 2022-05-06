# Proyecto API - Pet

En este README se encontraran algunas normativas para la construccion de las API.

## Actualizar las entidades de la BD con scafolding

1. Cambiar la cadena de conexion y ejecutar lo siguiente:

    ```csharp
    Scaffold-DbContext "Server=MI-PC\SQLEXPRESS01;Database=PetRescueDB;Trusted_Connection=true;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Core/Entities -nopluralize -force
    ```