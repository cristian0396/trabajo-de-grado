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
    public partial class Profes
    {
        public Profes(string opcionMusica, bool vozEncendida)
        {
            InitializeComponent();
            BindingContext = new ProfesViewModel(opcionMusica, vozEncendida); 
        }
    }
}