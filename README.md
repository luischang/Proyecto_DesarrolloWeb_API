# Proyecto API - Pet

En este README se encontraran algunas normativas para la construccion de las API.

## Actualizar las entidades de la BD con scafolding

1. Cambiar la cadena de conexion y ejecutar lo siguiente:

    ```csharp
    Scaffold-DbContext "Server=MI-PC\SQLEXPRESS01;Database=PetRescueDB;Trusted_Connection=true;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Core/Entities -nopluralize -force
    ```
    
2. Script de la base de datos

https://github.com/luischang/Proyecto_DesarrolloWeb_API/files/8644064/PetsToTheRescue_vf.txt
