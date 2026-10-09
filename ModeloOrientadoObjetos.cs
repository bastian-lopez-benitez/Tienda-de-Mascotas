namespace TiendaMascotasHuevos;

// ABSTRACCION: comportamiento comun de productos comerciales.
// HERENCIA: cada tipo de producto implementa su propia descripcion.
// ENCAPSULAMIENTO: se valida el estado al construir el objeto.
public abstract class ProductoComercial
{
    public int Id { get; }
    public string Nombre { get; }
    public decimal Precio { get; }
    public int Stock { get; }

    protected ProductoComercial(int id, string nombre, decimal precio, int stock)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre obligatorio.");
        if (precio <= 0) throw new ArgumentOutOfRangeException(nameof(precio));
        if (stock < 0) throw new ArgumentOutOfRangeException(nameof(stock));
        Id = id;
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }

    public bool HayStock(int cantidad) => cantidad > 0 && cantidad <= Stock;
    public abstract string DescribirPresentacion();
}

public sealed class ArenaParaGatos : ProductoComercial
{
    public string Presentacion { get; }
    public ArenaParaGatos(int id, string nombre, decimal precio, int stock, string presentacion)
        : base(id, nombre, precio, stock)
    {
        Presentacion = presentacion ?? "";
    }
    public override string DescribirPresentacion() => $"Arena para gatos: {Nombre} ({Presentacion})";
}

public sealed class CajaDeHuevos : ProductoComercial
{
    public string Presentacion { get; }
    public CajaDeHuevos(int id, string nombre, decimal precio, int stock, string presentacion)
        : base(id, nombre, precio, stock)
    {
        Presentacion = presentacion ?? "";
    }
    public override string DescribirPresentacion() => $"Caja de huevos: {Nombre} ({Presentacion})";
}

// POLIMORFISMO: el consumidor no necesita preguntar la categoria para describir.
public static class FabricaProductoComercial
{
    public static ProductoComercial Crear(Producto producto) => producto.Categoria switch
    {
        Categoria.ArenaGatos => new ArenaParaGatos(producto.Id, producto.Nombre, producto.Precio, producto.Stock, producto.Presentacion),
        Categoria.Huevos => new CajaDeHuevos(producto.Id, producto.Nombre, producto.Precio, producto.Stock, producto.Presentacion),
        _ => throw new ArgumentOutOfRangeException(nameof(producto))
    };
}
