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
    public partial class PlanGastos : ContentPage
    {
        PlanGastosViewModel context = new PlanGastosViewModel();
        public PlanGastos()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}