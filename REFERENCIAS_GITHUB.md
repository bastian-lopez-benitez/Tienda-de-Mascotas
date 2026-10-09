# Trazabilidad con los seis repositorios de referencia

Este proyecto **no afirma copiar código** de los repositorios originales: adapta funcionalidades y patrones observables al dominio de ventas de arena sanitaria y cajas de huevos. Los enlaces y atribuciones se conservan.

| Repositorio | Evidencia de la referencia | Adaptación concreta | Archivos locales |
|---|---|---|---|
| [FendoStark/inventario](https://github.com/FendoStark/inventario) | Proyecto de inventario, con carpeta `sistema-inventario-main` | Productos, existencias, stock mínimo, movimientos y reposiciones | `Database.cs`, `Repositories.cs`, `MainForm.cs`, `Services.cs` |
| [HiroMiyagi1/Contactos](https://github.com/HiroMiyagi1/Contactos) | README declara agregar, ver, actualizar y borrar contactos | Alta, consulta y edición de clientes; búsqueda en `ClientesService`. **La eliminación aún no está implementada** | `Domain.cs`, `Repositories.cs`, `Services.cs`, `MainForm.cs` |
| [katiuska-png/juego_gato](https://github.com/katiuska-png/juego_gato) | Proyecto Python con carpetas por responsabilidades, pruebas, clases base e interfaces pequeñas, SOLID explícito | Separación de dominio, repositorios, servicios, UI y comprobantes; interfaces inyectables | `Domain.cs`, `Services.cs`, `MainForm.cs`, `tests/` |
| [liz-dev99/projec-ingreso-factu-boleta](https://github.com/liz-dev99/projec-ingreso-factu-boleta) | Registro de facturas y boletas según descripción pública | Registro histórico y comprobante interno exportable; **no es boleta electrónica del SII** | `Repositories.cs`, `MainForm.cs`, `Services.cs` |
| [c3nf3/Proyecto](https://github.com/c3nf3/Proyecto) | Proyecto general; su estructura interna no se ha verificado exhaustivamente | Referencia complementaria, sin atribuirle funcionalidades específicas | Documentación |
| [jhpavezc27-ui/backend/Untitled8.ipynb](https://github.com/jhpavezc27-ui/backend/blob/main/Untitled8.ipynb) | Notebook público; no se ha confirmado una equivalencia directa de su código con C# | Referencia complementaria, sin atribución de código no comprobada | Documentación |

## Principios SOLID aplicados

- **S**: entidades, persistencia, interfaz y generación de comprobantes tienen funciones distintas.
- **O**: `IFormateadorComprobante` permite añadir otro formato sin cambiar la entidad `VentaResumen`.
- **L**: cualquier implementación que cumpla `IFormateadorComprobante` debe producir un comprobante válido sin requerir cambios en quien la consume.
- **I**: `IProductoRepository`, `IClienteRepository`, `IVentaRepository` e `IInventarioRepository` exponen contratos separados.
- **D**: `CatalogoService`, `ClientesService` y `VentasService` dependen de interfaces, no de `SqliteStore`.

## Limitaciones verificables

El sistema utiliza una sola implementación física SQLite para varios contratos, aunque los servicios pueden recibir dobles de prueba. No se ha compilado con .NET en este entorno; los escenarios manuales son una pauta, no resultados de pruebas ejecutadas. Los precios son ficticios y la emisión de documentos tributarios requiere integración SII.
