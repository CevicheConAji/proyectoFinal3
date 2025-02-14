using SQLite;

namespace proyectoFinal3;

public partial class MedicoMainPage : ContentPage
{
    private Medico medico;
    private Cliente clienteEncontrado;
    private readonly SQLiteAsyncConnection database;

    public MedicoMainPage(Medico medico)
    {
        InitializeComponent();
        this.medico = medico;

        // Ruta de la base de datos SQLite
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clientes.db3");
        database = new SQLiteAsyncConnection(dbPath);

        MostrarClientes();
        BindingContext = this;
    }

    public void MostrarNombreCliente()
    {
        NombreMedicoLabel.Text = "¡Bienvenido: " + medico.usuario + "!";
    }

    private async void OnBuscarClienteClicked(object sender, EventArgs e)
    {
        int clienteId;
        if (int.TryParse(ClienteIdEntry.Text, out clienteId))
        {
            clienteEncontrado = await database.Table<Cliente>()
                .Where(c => c.idCliente == clienteId)
                .FirstOrDefaultAsync();

            if (clienteEncontrado != null)
            {
                ClienteInfoLabel.Text = $"Cliente encontrado: {clienteEncontrado.usuario}";
            }
            else
            {
                ClienteInfoLabel.Text = "Cliente no encontrado";
            }
        }
        else
        {
            ClienteInfoLabel.Text = "ID de cliente no válido";
        }
    }

    private async void OnAñadirRecetaClicked(object sender, EventArgs e)
    {
        if (clienteEncontrado != null && !string.IsNullOrEmpty(NuevaRecetaEntry.Text))
        {
            clienteEncontrado.recetas += ";" + NuevaRecetaEntry.Text;
            await database.UpdateAsync(clienteEncontrado);
            ClienteInfoLabel.Text = $"Receta añadida a {clienteEncontrado.usuario}";
        }
        else
        {
            ClienteInfoLabel.Text = "No se puede añadir la receta";
        }
    }
    public void MostrarClientes()
    {
        ClientesLabel.Text = "Citas: " + string.Join(", ", medico.listaClientes.Split(';'));
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}