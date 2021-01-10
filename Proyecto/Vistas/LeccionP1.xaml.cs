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
    public partial class LeccionP1 : ContentPage
    {
        LeccionP1ViewModel context = new LeccionP1ViewModel();
        public LeccionP1()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}