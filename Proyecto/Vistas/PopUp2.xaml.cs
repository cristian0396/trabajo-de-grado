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
    public partial class PopUp2
    {
        MessageViewModel context = new MessageViewModel();
        public PopUp2()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}