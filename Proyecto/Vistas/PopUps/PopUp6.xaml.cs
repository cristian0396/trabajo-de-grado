using Proyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Vistas.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUp6
    {
        MessageViewModel context = new MessageViewModel();
        public PopUp6()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}