# Pruebas sugeridas en Visual Studio

1. Compilar el proyecto Windows Forms para `net8.0-windows`.
2. Agregar cliente y verificar persistencia al reiniciar.
3. Crear producto de arena y otro de huevos; validar código duplicado.
4. Intentar precio 0, stock negativo y venta vacía: deben rechazarse.
5. Registrar venta con dos categorías; verificar total, historial y disminución de stock.
6. Intentar vender más unidades que las disponibles: rechazar sin alterar stock.
7. Registrar reposición y comprobar movimiento.
8. Exportar CSV y comprobante interno.
9. Verificar clientes con nombre parcial usando `ClientesService.Buscar`.
10. Verificar productos bajo mínimo usando `CatalogoService.StockBajo`.

**Estado:** casos definidos; no se presentan como ejecutados.
