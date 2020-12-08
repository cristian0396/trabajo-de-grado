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
    [QueryProperty("Modulo", "modulo")]

    public partial class introPresupuesto : ContentPage
    {
        string modulo = "";
        public string Modulo
        {
            get => modulo;
            set 
            {
                modulo = Uri.UnescapeDataString(value);
                OnPropertyChanged();
            }
        }

        IntroPresupuestoViewModel context;
        public introPresupuesto()
        {
            InitializeComponent();
            context = new IntroPresupuestoViewModel(Modulo);
            BindingContext = context;
        }
    }
}