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
    public partial class LeccionP2 : ContentPage
    {
        LeccionP2ViewModel context = new LeccionP2ViewModel();
        public LeccionP2()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}