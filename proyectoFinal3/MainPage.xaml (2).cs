namespace proyectoFinal3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnEntrarCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComprobarCliente());
        }

        private async void btnEntrarAdministrativo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComprobarAdministrativo());
        }
        private async void btnEntrarMedico_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComprobarMedico());
        }
    }
}
