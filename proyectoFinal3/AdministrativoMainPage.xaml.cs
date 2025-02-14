namespace proyectoFinal3;

public partial class AdministrativoMainPage : ContentPage
{
	private Administrativo administrativo;

	public string nombreAdministrativo => administrativo.usuario;
    public AdministrativoMainPage(Administrativo administrativo)
	{
		InitializeComponent();
		this.administrativo = administrativo;
        BindingContext = this;
    }

}