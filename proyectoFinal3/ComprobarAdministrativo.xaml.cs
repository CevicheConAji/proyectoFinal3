using System.Diagnostics;
using SQLite;

namespace proyectoFinal3;

public partial class ComprobarAdministrativo : ContentPage
{
    private readonly SQLiteAsyncConnection database;

    public ComprobarAdministrativo()
    {
        InitializeComponent();

        // Ruta de la base de datos SQLite
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clientes.db3");
        database = new SQLiteAsyncConnection(dbPath);

        // Crear la tabla Clientes si no existe
        database.CreateTableAsync<Administrativo>().Wait();
    }
    
    private async void OnBtnLoggin(object sender, EventArgs e)
    {
        var username = UserEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Por favor ingrese usuario y contrase�a";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Autenticar usuario en la base de datos SQLite
        var user = await AuthenticateAdministrativoAsync(username, password);
        Debug.WriteLine("dato usuario: " + username);
        
        Administrativo administrativo = new Administrativo();
        administrativo.usuario = username;
        

        if (user != null)
        {
            Debug.WriteLine("Administrativo autenticado correctamente.");
            // Redirige a la siguiente p�gina si la autenticaci�n fue exitosa
            await Navigation.PushAsync(new AdministrativoMainPage(user));
        }
        else
        {
            // Muestra un error si las credenciales no coinciden
            ErrorLabel.Text = "Usuario o contrase�a incorrectos";
            ErrorLabel.IsVisible = true;
        }

    }
    

    // M�todo para agregar un usuario de prueba
    private async void OnAddTestAdministrativoAsync(object sender, EventArgs e)
    {
        Administrativo administrativo = new Administrativo();
        administrativo.usuario = "jorge";
        administrativo.password = "jorge";
       


        await database.InsertAsync(administrativo);
        Debug.WriteLine("Usuario agregado exitosamente.");
    }

    
    private async Task<Administrativo>AuthenticateAdministrativoAsync(string username, string password)
    {
        try
        {
            // Buscar usuario en la base de datos SQLite
            var user = await database.Table<Administrativo>()
                .Where(u => u.usuario == username && u.password == password)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                Debug.WriteLine($"Usuario autenticado: {user.usuario}");
                
                return user;
                
            }

            Debug.WriteLine("Usuario o contrase�a incorrectos.");
            return null;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al acceder a SQLite: {ex.Message}");
            return null;

        }
        
    }   

}