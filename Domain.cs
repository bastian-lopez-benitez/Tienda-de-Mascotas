namespace TiendaMascotasHuevos;

public enum Categoria { ArenaGatos, Huevos }
public sealed record Producto(int Id, string Codigo, string Nombre, Categoria Categoria, string Presentacion, decimal Precio, int Stock, int StockMinimo, bool Activo);
public sealed record Cliente(int Id, string Nombre, string Documento, string Telefono, string Email, string Direccion);
public sealed record ItemVenta(int ProductoId, string Nombre, int Cantidad, decimal PrecioUnitario) { public decimal Subtotal => Cantidad * PrecioUnitario; }
public sealed record VentaResumen(int Id, DateTime Fecha, int? ClienteId, string Cliente, string MedioPago, decimal Total);
public sealed record MovimientoStock(int Id, int ProductoId, DateTime Fecha, int Delta, string Motivo);
public sealed record NuevaVenta(int? ClienteId, string MedioPago, IReadOnlyList<(int ProductoId, int Cantidad)> Items);
public interface IProductoRepository { IReadOnlyList<Producto> Listar(bool soloActivos = true); int Guardar(Producto p); }
public interface IClienteRepository { IReadOnlyList<Cliente> Listar(); int Guardar(Cliente c); }
public interface IVentaRepository { int Registrar(NuevaVenta venta); IReadOnlyList<VentaResumen> Listar(); IReadOnlyList<ItemVenta> Detalle(int ventaId); }
public interface IInventarioRepository { void Ajustar(int productoId, int delta, string motivo); IReadOnlyList<MovimientoStock> Movimientos(int productoId); }
public static class Reglas {
    public static void Producto(Producto p) {
        if (string.IsNullOrWhiteSpace(p.Codigo) || string.IsNullOrWhiteSpace(p.Nombre)) throw new ArgumentException("Código y nombre obligatorios.");
        if (p.Precio <= 0 || p.Stock < 0 || p.StockMinimo < 0) throw new ArgumentException("Precio y stock inválidos.");
    }
    public static void Cliente(Cliente c) { if (string.IsNullOrWhiteSpace(c.Nombre)) throw new ArgumentException("Nombre de cliente obligatorio."); }
    public static void Venta(NuevaVenta v) {
        if (v.Items.Count == 0 || v.Items.Any(x => x.Cantidad <= 0) || v.Items.GroupBy(x=>x.ProductoId).Any(g=>g.Count()>1)) throw new ArgumentException("Detalle de venta inválido o duplicado.");
        if (string.IsNullOrWhiteSpace(v.MedioPago)) throw new ArgumentException("Indique medio de pago.");
    }
}
