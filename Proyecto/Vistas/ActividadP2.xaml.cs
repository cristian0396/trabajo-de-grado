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

    public partial class ActividadP2 : ContentPage
    {
        ActividadP2ViewModel context = new ActividadP2ViewModel();
        public ActividadP2()
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