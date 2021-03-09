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
    [QueryProperty(nameof(Modulo), nameof(Modulo))] //Esta propiedad es necesaria para indicar que va a llegar un queryParameter, el idModulo
    public partial class introPresupuesto : ContentPage
    {
        string modulo;
        public string Modulo
        {
            set
            {
                BindingContext = new IntroPresupuestoViewModel(value); //necesario para recibir el queryparameter y mandarlo al ModelView 
            }
            get { return modulo; }
        }
        
        public introPresupuesto()
        {
            InitializeComponent();
            this.SizeChanged += NewsListPageSizeChanged; //se usa la propiedad sizeChanged para que el queryParametro se pueda manipular antes de que se ejecute todo el constructor, ya que en shell hay ese problema
        }

        private void NewsListPageSizeChanged(object sender, EventArgs e) //En shell, los queryparametros son obtenidos despues de que el constructor de la clase se ha ejecutado, esto es un gran problema para hacer el bindingContext
        {
            if (BindingContext == null)
            {
                string idModulo = Modulo;
                BindingContext = new IntroPresupuestoViewModel(idModulo); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
            }
            this.SizeChanged -= NewsListPageSizeChanged;
        }
    }
}