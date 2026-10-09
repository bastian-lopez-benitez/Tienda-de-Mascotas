using Microsoft.Data.Sqlite;
namespace TiendaMascotasHuevos;
public sealed class Database {
    public string ConnectionString { get; }
    public Database(string? file = null) {
        file ??= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TiendaMascotasHuevos", "tienda.db");
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(file))!);
        ConnectionString = new SqliteConnectionStringBuilder { DataSource = file, ForeignKeys = true }.ToString();
        using var c = Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS productos(id INTEGER PRIMARY KEY AUTOINCREMENT,codigo TEXT NOT NULL UNIQUE,nombre TEXT NOT NULL,categoria INTEGER NOT NULL,presentacion TEXT NOT NULL,precio INTEGER NOT NULL CHECK(precio>0),stock INTEGER NOT NULL CHECK(stock>=0),stock_minimo INTEGER NOT NULL DEFAULT 0,activo INTEGER NOT NULL DEFAULT 1);
CREATE TABLE IF NOT EXISTS clientes(id INTEGER PRIMARY KEY AUTOINCREMENT,nombre TEXT NOT NULL,documento TEXT NOT NULL DEFAULT '',telefono TEXT NOT NULL DEFAULT '',email TEXT NOT NULL DEFAULT '',direccion TEXT NOT NULL DEFAULT '');
CREATE TABLE IF NOT EXISTS ventas(id INTEGER PRIMARY KEY AUTOINCREMENT,fecha TEXT NOT NULL,cliente_id INTEGER NULL REFERENCES clientes(id),medio_pago TEXT NOT NULL,total INTEGER NOT NULL CHECK(total>=0));
CREATE TABLE IF NOT EXISTS venta_items(id INTEGER PRIMARY KEY AUTOINCREMENT,venta_id INTEGER NOT NULL REFERENCES ventas(id),producto_id INTEGER NOT NULL REFERENCES productos(id),nombre TEXT NOT NULL,cantidad INTEGER NOT NULL CHECK(cantidad>0),precio_unitario INTEGER NOT NULL CHECK(precio_unitario>0));
CREATE TABLE IF NOT EXISTS movimientos(id INTEGER PRIMARY KEY AUTOINCREMENT,producto_id INTEGER NOT NULL REFERENCES productos(id),fecha TEXT NOT NULL,delta INTEGER NOT NULL,motivo TEXT NOT NULL);";
        cmd.ExecuteNonQuery();
        using var count = c.CreateCommand(); count.CommandText="SELECT COUNT(*) FROM productos";
        if (Convert.ToInt32(count.ExecuteScalar()) == 0) {
            using var seed=c.CreateCommand(); seed.CommandText=@"INSERT INTO productos(codigo,nombre,categoria,presentacion,precio,stock,stock_minimo,activo) VALUES
('ARE-010','Arena aglomerante',0,'Bolsa 10 kg',8990,25,5,1),('ARE-005','Arena sanitaria',0,'Bolsa 5 kg',4990,30,5,1),('HUE-030','Huevos blancos',1,'Caja 30 unidades',6990,40,8,1),('HUE-012','Huevos de campo',1,'Caja 12 unidades',3990,35,6,1);"; seed.ExecuteNonQuery();
        }
    }
    public SqliteConnection Open() { var c=new SqliteConnection(ConnectionString); c.Open(); using var pragma=c.CreateCommand(); pragma.CommandText="PRAGMA busy_timeout=5000;"; pragma.ExecuteNonQuery(); return c; }
}
