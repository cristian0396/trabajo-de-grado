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
    [QueryProperty(nameof(SourceImg), nameof(SourceImg))] //Esta propiedad es necesaria para indicar que va a llegar un queryParameter (sourceImg)
    public partial class FondoLecciones : ContentPage
    {
        string sourceImg;
        public string SourceImg
        {
            set
            {
                switch (value)
                {
                    case "leccionfondo.png":
                        BindingContext = new FondoLeccionesViewModel(value, "1"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondo01.png":
                        BindingContext = new FondoLeccionesViewModel(value, "2"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoPlanGastos.png":
                        BindingContext = new FondoLeccionesViewModel(value, "3"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondolecciondos.png":
                        BindingContext = new FondoLeccionesViewModel(value, "4"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoahorrouno.jpg":
                        BindingContext = new FondoLeccionesViewModel(value, "5"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoahorrotres.jpg":
                        BindingContext = new FondoLeccionesViewModel(value, "6"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                }
            }
            get { return sourceImg; }
        }

        public FondoLecciones()
        {
            InitializeComponent();
            this.SizeChanged += NewsListPageSizeChanged; //se usa la propiedad sizeChanged para que el queryParametro se pueda manipular antes de que se ejecute todo el constructor, ya que en shell hay ese problema
        }

        private void NewsListPageSizeChanged(object sender, EventArgs e) //En shell, los queryparametros son obtenidos despues de que el constructor de la clase se ha ejecutado, esto es un gran problema para hacer el bindingContext
        {
            if (BindingContext == null)
            {
                string _sourceImg = SourceImg;
                switch (_sourceImg)
                {
                    case "leccionfondo.png":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "1"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondo01.png":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "2"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoPlanGastos.png":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "3"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondolecciondos.png":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "4"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoahorrouno.jpg":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "5"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                    case "fondoahorrotres.jpg":
                        BindingContext = new FondoLeccionesViewModel(_sourceImg, "6"); //Se establece la comunicación con el modelView y se manda un parametro indicando la información del modulo que se necesita
                        break;
                }                             
            }
            this.SizeChanged -= NewsListPageSizeChanged;
        }
    }
}