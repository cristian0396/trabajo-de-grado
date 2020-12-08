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
    public partial class Presupuesto : ContentPage
    {
        PresupuestoViewModel context = new PresupuestoViewModel();
        public Presupuesto()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}