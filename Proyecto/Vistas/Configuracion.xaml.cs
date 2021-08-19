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
    public partial class Configuracion : ContentPage
    {
        ConfiguracionViewModel context = new ConfiguracionViewModel();
        public Configuracion()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}