using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class PopUpViewModel : ViewModelBase
    {
        public int Rotacion { get; set; }
        public float Escala { get; set; }
        public string Source { get; set; }
        public List<int> Alturas { get; set; }        
        public string Opcion { get; set; }
        public int GridColumn { get; set; }
        public int Width { get; set; }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }

        public PopUpViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
        }
        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public void InitializeFields(PopUpViewModel _popUp, int rotacion = default(int), float escala =  default(float), string source = default(string), List<int> alturas = default(List<int>), string opcion = default(string), int gridColumn = default(int), int width = default(int), string message = default(string))
        {
            _popUp.Rotacion = rotacion;
            _popUp.Escala = escala;
            _popUp.Source = source;
            _popUp.Alturas = alturas;
            _popUp.Opcion = opcion;
            _popUp.GridColumn = gridColumn;
            _popUp.Width = width;
            _popUp.Message = message;
        }
        public async Task NewPopUp()
        {
            PopUpUniversal PopUpView = new PopUpUniversal();
            List<int> Heights = new List<int>();
            string message, _source = "flecha.png";
            int _rotacion = -90, _escala = 1, _gridColumn = 0;

            await PopupNavigation.Instance.PopAsync();
            switch (Opcion)
            {
                case "3":
                    Heights.Add(200);
                    Heights.Add(170);
                    Heights.Add(180);
                    message = "Estos gastos generalmente no cambian" +
                        " es decir, se mantienen porque cubren las necesidades más basicas de cada persona.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: _rotacion, escala: _escala, source: _source, alturas: Heights, opcion: "4", gridColumn: _gridColumn, message: message);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "4":
                    Heights.Add(248);
                    Heights.Add(170);
                    Heights.Add(180);
                    message = "Estos gastos son todos aquellos que pueden diferir en cada mes" +
                        " según la actividad de la persona.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: _rotacion, escala: _escala, source: _source, alturas: Heights, opcion: "5", gridColumn: _gridColumn, message: message);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "5":
                    Heights.Add(290);
                    Heights.Add(170);
                    Heights.Add(180);
                    message = "El ahorro es definido por cada persona para que se acomode a sus capacidades económicas, " +
                        "se recomienda que sea fijo y de al menos el 10% del total de los ingresos.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: _rotacion, escala: _escala, source: _source, alturas: Heights, opcion: "6", gridColumn: _gridColumn, message: message);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "6":
                    Heights.Add(320);
                    Heights.Add(180);
                    message = "Este es el Total de gastos," +
                        " se calcula sumando todos los gastos, tanto gastos fijos como variables, se debe tener en cuenta cualquier salida de plata.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 90, escala: _escala, source: "flecha1.png", alturas: Heights, gridColumn: 1, message: message, width: 60);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "7":
                    Heights.Add(300);
                    Heights.Add(180);
                    message = "Seguida de la vivienda está la alimentación, te sugerimos comprar raciones grandes de forma quincenal o mensual y de esta forma economizar un poco más.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 120, escala: 0.7f, source: _source, alturas: Heights, opcion: "8",  gridColumn: 1, message: message, width: 60);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "8":
                    Heights.Add(250);
                    Heights.Add(180);
                    message = "Luego viene el transporte, en este gasto te sugerimos analizar bien tus opciones de desplazamiento, muchas veces no se tienen en cuenta alternativas como usar bicicleta o motocicletas que a mediano y largo plazo pueden ser un alivio para tu bolsillo.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 100, escala: 0.7f, source: _source, alturas: Heights, opcion: "9", gridColumn: 0, message: message, width: 60);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "9":
                    Heights.Add(180);
                    Heights.Add(180);
                    message = "La educación, vestuario y calzado representan cerca del 10% de los gastos totales, recuerda asistir y/o inscribirte en cursos de educación financiera!";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 90, escala: 0.7f, source: _source, alturas: Heights, opcion: "10", gridColumn: 0, message: message, width: 60);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "10":
                    Heights.Add(270);
                    Heights.Add(180);
                    message = "Por ultimo, pero no menos importante se sitúan los gastos varios, comunicaciones y cultura-diversión, que es el excedente de gastos totales.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: -90, escala: 0.7f, source: _source, alturas: Heights, gridColumn: 1, message: message, width: 60);
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
            }
        }
    }
}
