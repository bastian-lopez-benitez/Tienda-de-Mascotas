# Tienda Mascotas y Huevos — sistema de ventas e inventario

Aplicación de escritorio **Windows Forms / C# / .NET 8 / SQLite**. Administra venta de arena para gatos y cajas de huevos, clientes, precios en pesos chilenos, inventario, ventas, comprobantes internos y reportes CSV.

## Ejecutar en Windows
1. Instalar **.NET 8 SDK** y Visual Studio 2022 con la carga de trabajo **Desarrollo de escritorio con .NET**.
2. Abrir `src/TiendaMascotasHuevos.csproj` en Visual Studio.
3. Restaurar paquetes NuGet (requiere conexión la primera vez).
4. Presionar **F5**, o ejecutar `dotnet run --project src/TiendaMascotasHuevos.csproj` en Windows.
5. La base SQLite se crea en `%LOCALAPPDATA%\TiendaMascotasHuevos\tienda.db` y se cargan cuatro productos de ejemplo al iniciar por primera vez.

## Módulos
- Productos: alta, edición, activación/desactivación, precio, presentación, categoría y umbral mínimo.
- Clientes: alta, edición, nombre, RUT/documento, teléfono, correo y dirección.
- Inventario: ajustes positivos/negativos con motivo y trazabilidad en base de datos.
- Venta: carrito, selección de cliente o consumidor final, medio de pago, total y validación de existencias.
- Historial: listado de ventas, comprobante interno de texto, exportación CSV.
- Ayuda: instrucciones dentro del programa.

## Arquitectura SOLID
- **S**: `Reglas` valida; `SqliteStore` persiste; `MainForm` muestra y recibe acciones.
- **O**: `Categoria` y `Producto` permiten nuevos tipos de mercancía sin modificar el cálculo de venta; nuevas implementaciones de interfaces permiten otro almacenamiento.
- **L**: contratos de repositorios admiten implementaciones sustituibles que respeten validaciones y resultados.
- **I**: interfaces separadas `IProductoRepository`, `IClienteRepository`, `IVentaRepository`, `IInventarioRepository`.
- **D**: las interfaces documentan dependencias de acceso a datos. La interfaz gráfica de esta versión todavía utiliza `SqliteStore` concreto; refactorizar a inyección de interfaces completa es una mejora pendiente.

## Reglas de negocio
- Precio positivo y cantidades de venta mayores que cero.
- No se vende un producto desactivado o sin stock suficiente.
- La venta, sus líneas, el descuento de existencias y el registro de movimientos se guardan en **una sola transacción**.
- Las ventas conservan nombre y precio histórico de cada producto.
- Los precios del catálogo son ficticios y están expresados en CLP enteros.
- El comprobante es **interno, no tributario**. No sustituye una boleta electrónica autorizada por el SII.

## Alcance y limitaciones
Este repositorio es una base funcional de escritorio, no un sistema de facturación certificado ni un POS multiusuario. No incluye autenticación, emisión DTE/SII, integración de pagos, descuentos tributarios, auditoría de usuarios ni gestión de proveedores. El inventario puede editarse solo mediante movimientos después de crear un producto.

**Validación:** los archivos se generaron y revisaron estructuralmente, pero no se pudo ejecutar `dotnet build` ni pruebas reales en este entorno porque no hay SDK .NET instalado. La compilación y prueba en Visual Studio están pendientes.

## Fuentes de estudio
Diseño POO/SOLID, .NET, Windows Forms, SQL, UML, pruebas y validaciones: siete PDF PRO205 suministrados. Los seis enlaces de GitHub fueron aportados como referencias; **no se incorporó ni se verificó código externo** de esos repositorios.


## Basado en referencias de GitHub

Consultar [REFERENCIAS_GITHUB.md](REFERENCIAS_GITHUB.md) para la matriz de correspondencia de los seis repositorios, diferencias de lenguaje, límites de reutilización y principios SOLID. La nueva capa `src/Services.cs` permite separar casos de uso de persistencia y formularios.


## Programación Orientada a Objetos (POO)

El proyecto implementa los cuatro pilares de POO, además de SOLID:

- **Abstracción:** `ProductoComercial` define las características y operaciones comunes.
- **Encapsulamiento:** su constructor valida nombre, precio y stock; las propiedades son de solo lectura.
- **Herencia:** `ArenaParaGatos` y `CajaDeHuevos` heredan de `ProductoComercial`.
- **Polimorfismo:** cada clase implementa `DescribirPresentacion()`; `CatalogoService.ObjetosDisponibles()` devuelve objetos a través de la clase base.
- **Composición y colaboración:** `VentasService` utiliza `IVentaRepository`, y `CatalogoService` utiliza `IProductoRepository`, ambos por inyección de dependencias.

Los `record` de `Domain.cs` son DTO/entidades de persistencia para mantener compatibilidad con SQLite y Windows Forms; el modelo `ProductoComercial` añade una jerarquía orientada a objetos sin romper la base existente.

**Ejemplo de polimorfismo:**

```csharp
foreach (ProductoComercial producto in catalogo.ObjetosDisponibles())
    Console.WriteLine(producto.DescribirPresentacion());
```

**Verificación:** se ha revisado la estructura de archivos, pero no se ha compilado ni ejecutado el proyecto en .NET en este entorno.
