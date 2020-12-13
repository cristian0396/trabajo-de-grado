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
    [QueryProperty(nameof(Modulo), nameof(Modulo))]
    public partial class introPresupuesto : ContentPage
    {
        string modulo;
        IntroPresupuestoViewModel context;
        public string Modulo
        {
            set
            {
                BindingContext = new IntroPresupuestoViewModel(value);
            }
            get { return modulo; }
        }
        
        public introPresupuesto()
        {
            InitializeComponent();
            this.SizeChanged += NewsListPageSizeChanged;
        }

        private void NewsListPageSizeChanged(object sender, EventArgs e)
        {
            if (BindingContext == null)
            {
                string idModulo = Modulo;
                BindingContext = new IntroPresupuestoViewModel(idModulo);
            }
            this.SizeChanged -= NewsListPageSizeChanged;
        }
    }
}