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
            BindingContext = context; //Aqui se establece la conexión entre el view y el viewModel
        }
    }    
}