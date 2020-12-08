using Proyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        InicioViewModel context = new InicioViewModel();
        public Inicio()
        {
            InitializeComponent();
            BindingContext = context;
        }
        private void TapGestureRecognizer_Tapped_Presupuesto(object sender, EventArgs e)
        {
            var img = sender as Image;
            img.ScaleTo(1.3, 300);
            img.ScaleTo(1, 300, Easing.BounceOut);
        }
        private async void TapGestureRecognizer_Tapped_Plan_Gastos(object sender, EventArgs e)
        {
            var img = sender as Image;
            await img.ScaleTo(1.3, 300);
            await img.ScaleTo(1, 300, Easing.BounceOut);
            await Navigation.PushAsync(new PlanGastos());
        }

        private async void TapGestureRecognizer_Tapped_Ahorro(object sender, EventArgs e)
        {
            var img = sender as Image;
            await img.ScaleTo(1.3, 300);
            await img.ScaleTo(1, 300, Easing.BounceOut);
            await Navigation.PushAsync(new Ahorro());
        }
    }    
}