namespace TiendaMascotasHuevos;
internal static class Program {
    [STAThread] static void Main() {
        ApplicationConfiguration.Initialize();
        try { var store=new SqliteStore(new Database()); Application.Run(new MainForm(store)); }
        catch(Exception ex){MessageBox.Show(ex.Message,"Error de inicio",MessageBoxButtons.OK,MessageBoxIcon.Error);}
    }
}
