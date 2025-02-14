using SQLite;
using System.Diagnostics;

namespace proyectoFinal3;

public partial class ComprobarMedico : ContentPage
{
    private readonly SQLiteAsyncConnection database;

    public ComprobarMedico()
    {
        InitializeComponent();

        // Ruta de la base de datos SQLite
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clientes.db3");
        database = new SQLiteAsyncConnection(dbPath);

        // Crear la tabla Clientes si no existe
        database.CreateTableAsync<Medico>().Wait();
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
        var user = await AuthenticateMedicoAsync(username, password);
        Debug.WriteLine("dato usuario: " + username);

        if (user != null)
        {
            Debug.WriteLine("Medico autenticado correctamente.");
            // Redirige a la siguiente página si la autenticación fue exitosa
            await Navigation.PushAsync(new MedicoMainPage());
        }
        else
        {
            // Muestra un error si las credenciales no coinciden
            ErrorLabel.Text = "Usuario o contraseña incorrectos";
            ErrorLabel.IsVisible = true;
        }

    }
    private async void OnAddTestMedicoAsync(object sender, EventArgs e)
    {

        var nuevoUsuario = new Medico
        {
            usuario = "oscar",
            password = "oscar",
            listaClientes="piero1,1;sergio(cliente),2;paco(cliente),3"
        };
        await database.InsertAsync(nuevoUsuario);
        Debug.WriteLine("Usuario agregado exitosamente.");
    }

    private async Task<Medico> AuthenticateMedicoAsync(string username, string password)
    {
        try
        {
            // Buscar usuario en la base de datos SQLite
            var user = await database.Table<Medico>()
                .Where(u => u.usuario == username && u.password == password)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                Debug.WriteLine($"Usuario autenticado: {user.usuario}");

                return user;

            }

            Debug.WriteLine("Usuario o contraseña incorrectos.");
            return null;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al acceder a SQLite: {ex.Message}");
            return null;

        }

    }
}