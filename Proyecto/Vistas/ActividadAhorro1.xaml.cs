using Proyecto.Actividades;
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
    public partial class ActividadAhorro1 : ContentPage
    {
        ActividadAhorroViewModel context = new ActividadAhorroViewModel();
        public ActividadAhorro1()
        {
            InitializeComponent();
            BindingContext = context;
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");
            return base.OnBackButtonPressed();
        }
    }
}