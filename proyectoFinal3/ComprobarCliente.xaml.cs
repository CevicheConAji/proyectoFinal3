using System.Diagnostics;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace proyectoFinal3;

public partial class ComprobarCliente : ContentPage
{
    private readonly SQLiteAsyncConnection database;

    public ComprobarCliente()
    {
        InitializeComponent();

        // Ruta de la base de datos SQLite
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clientes.db3");
        database = new SQLiteAsyncConnection(dbPath);

        // Crear la tabla Clientes si no existe
        database.CreateTableAsync<Cliente>().Wait();
    }

    private async void OnBtnLoggin(object sender, EventArgs e)
    {
        var username = UserEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Por favor ingrese usuario y contraseña";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Autenticar usuario en la base de datos SQLite
        var user = await AuthenticateUserAsync(username, password);
        Debug.WriteLine("dato usuario: " + username);

        if (user != null)
        {
            Debug.WriteLine("Usuario autenticado correctamente.");
            // Redirige a la siguiente página si la autenticación fue exitosa
            await Navigation.PushAsync(new ClienteMainPage(user));
        }
        else
        {
            // Muestra un error si las credenciales no coinciden
            ErrorLabel.Text = "Usuario o contraseña incorrectos";
            ErrorLabel.IsVisible = true;
        }
    }

    private async void OnAddTestUserAsync(object sender, EventArgs e)
    {
        var nuevoUsuario = new Cliente
        {
            idCliente = 1,
            usuario = "piero1",
            password = "piero1",

        };

        await database.InsertAsync(nuevoUsuario);
        Debug.WriteLine("Usuario agregado exitosamente.");
    }

    private async Task<Cliente> AuthenticateUserAsync(string username, string password)
    {
        try
        {
            // Buscar usuario en la base de datos SQLite
            var user = await database.Table<Cliente>()
                .Where(u => u.usuario == username && u.password == password)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                Debug.WriteLine($"Usuario autenticado: {user.usuario}");
                return user; // Usuario encontrado
            }

            Debug.WriteLine("Usuario o contraseña incorrectos.");
            return null; // Usuario no encontrado
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al acceder a SQLite: {ex.Message}");
            return null;
        }
    }

}