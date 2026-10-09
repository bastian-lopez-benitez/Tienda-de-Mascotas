# Plan de pruebas manuales (pendiente de ejecución)

| ID | Escenario | Resultado esperado |
|---|---|---|
| P01 | Iniciar con base inexistente | Crea SQLite y cuatro productos de muestra |
| P02 | Crear cliente | Aparece en listado y selector de ventas |
| P03 | Registrar venta de 2 arenas y 1 caja de huevos | Registra total, líneas y resta stock |
| P04 | Vender más unidades que el stock | Rechaza venta sin registrar cambios |
| P05 | Registrar producto con código duplicado | SQLite rechaza duplicado |
| P06 | Ajustar stock a valor negativo | Operación rechazada |
| P07 | Exportar historial | Archivo CSV con encabezados y ventas |
| P08 | Guardar comprobante | TXT interno con detalle, total y fecha |
| P09 | Cerrar y reabrir | Conserva clientes, ventas y stock |
| P10 | Intentar vender producto inactivo | No aparece en selector y se rechaza en capa de datos |

**Estado:** casos diseñados, no ejecutados en este entorno.
