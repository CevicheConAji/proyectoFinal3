namespace proyectoFinal3;

public partial class ClienteMainPage : ContentPage
{
    private Cliente cliente;

    public string nombreCliente => cliente.usuario;
    public ClienteMainPage(Cliente cliente)
	{
        InitializeComponent();
        this.cliente = cliente;
        
        MostrarCitas();
        MostrarRecetas();
        MostrarNombreCliente();
        BindingContext = this;
    }
    public void MostrarNombreCliente()
    {
        NombreClienteLabel.Text = "¡Bienvenido: " + cliente.usuario +"!";
    }
    public void MostrarCitas()
    {
        CitasLabel.Text = "Citas: " + string.Join(", ", cliente.citas.Split(';'));
    }

    public void MostrarRecetas()
    {
        RecetasLabel.Text = "Recetas: " + string.Join(", ", cliente.recetas.Split(';'));
    }

}