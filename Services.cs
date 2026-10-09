namespace TiendaMascotasHuevos;

// Servicios de aplicación: coordinan reglas de negocio sin conocer SQLite ni WinForms.
public sealed class CatalogoService {
    private readonly IProductoRepository productos;
    public CatalogoService(IProductoRepository productos) => this.productos = productos;
    public IReadOnlyList<Producto> Disponibles() => productos.Listar().Where(p => p.Activo && p.Stock > 0).ToList();
    public IReadOnlyList<ProductoComercial> ObjetosDisponibles() => Disponibles().Select(FabricaProductoComercial.Crear).ToList();
    public IReadOnlyList<Producto> StockBajo() => productos.Listar().Where(p => p.Activo && p.Stock <= p.StockMinimo).ToList();
    public int Guardar(Producto producto) { Reglas.Producto(producto); return productos.Guardar(producto); }
}
public sealed class ClientesService {
    private readonly IClienteRepository clientes;
    public ClientesService(IClienteRepository clientes) => this.clientes = clientes;
    public IReadOnlyList<Cliente> Buscar(string texto) => clientes.Listar().Where(c => c.Nombre.Contains(texto ?? "", StringComparison.OrdinalIgnoreCase) || c.Documento.Contains(texto ?? "", StringComparison.OrdinalIgnoreCase)).ToList();
    public int Guardar(Cliente cliente) { Reglas.Cliente(cliente); return clientes.Guardar(cliente); }
}
public sealed class VentasService {
    private readonly IVentaRepository ventas;
    public VentasService(IVentaRepository ventas) => this.ventas = ventas;
    public int Registrar(NuevaVenta nueva) { Reglas.Venta(nueva); return ventas.Registrar(nueva); }
    public IReadOnlyList<VentaResumen> Historial() => ventas.Listar();
}
public interface IFormateadorComprobante { string Formatear(VentaResumen venta, IReadOnlyList<ItemVenta> items); }
public sealed class ComprobanteTexto : IFormateadorComprobante {
    public string Formatear(VentaResumen venta, IReadOnlyList<ItemVenta> items) {
        var lineas = new List<string> { "COMPROBANTE INTERNO - NO TRIBUTARIO", $"Venta #{venta.Id}", $"Cliente: {venta.Cliente}", $"Pago: {venta.MedioPago}" };
        lineas.AddRange(items.Select(i => $"{i.Nombre}: {i.Cantidad} x ${i.PrecioUnitario:N0} = ${i.Subtotal:N0}"));
        lineas.Add($"TOTAL: ${venta.Total:N0}"); return string.Join(Environment.NewLine, lineas);
    }
}
