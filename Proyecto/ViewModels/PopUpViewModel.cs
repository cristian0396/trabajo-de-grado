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
        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }
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

        public PopUpViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
        }
        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task NewPopUp()
        {
            PopUpUniversal PopUpView = new PopUpUniversal();
            List<int> Heights = new List<int>();
            await PopupNavigation.Instance.PopAsync();
            switch (Opcion)
            {
                case "3":
                    Heights.Add(200);
                    Heights.Add(170);
                    Heights.Add(180);
                    ((PopUpViewModel)PopUpView.BindingContext).Message = "Estos gastos generalmente no cambian" +
                        " es decir, se mantienen porque cubren las necesidades más basicas de cada persona.";
                    ((PopUpViewModel)PopUpView.BindingContext).Opcion = "4";
                    ((PopUpViewModel)PopUpView.BindingContext).Rotacion = -90;
                    ((PopUpViewModel)PopUpView.BindingContext).Escala = 1;
                    ((PopUpViewModel)PopUpView.BindingContext).Source = "flecha.png";
                    ((PopUpViewModel)PopUpView.BindingContext).Alturas = Heights;
                    ((PopUpViewModel)PopUpView.BindingContext).GridColumn = 0;
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "4":
                    Heights.Add(248);
                    Heights.Add(170);
                    Heights.Add(180);
                    ((PopUpViewModel)PopUpView.BindingContext).Message = "Estos gastos son todos aquellos que pueden diferir en cada mes" +
                        " según la actividad de la persona.";
                    ((PopUpViewModel)PopUpView.BindingContext).Opcion = "5";
                    ((PopUpViewModel)PopUpView.BindingContext).Rotacion = -90;
                    ((PopUpViewModel)PopUpView.BindingContext).Escala = 1;
                    ((PopUpViewModel)PopUpView.BindingContext).Source = "flecha.png";
                    ((PopUpViewModel)PopUpView.BindingContext).Alturas = Heights;
                    ((PopUpViewModel)PopUpView.BindingContext).GridColumn = 0;
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "5":
                    Heights.Add(290);
                    Heights.Add(170);
                    Heights.Add(180);
                    ((PopUpViewModel)PopUpView.BindingContext).Message = "El ahorro es definido por cada persona para que se acomode a sus capacidades económicas, " +
                        "se recomienda que sea fijo y de al menos el 10% del total de los ingresos.";
                    ((PopUpViewModel)PopUpView.BindingContext).Opcion = "6";
                    ((PopUpViewModel)PopUpView.BindingContext).Rotacion = -90;
                    ((PopUpViewModel)PopUpView.BindingContext).Escala = 1;
                    ((PopUpViewModel)PopUpView.BindingContext).Source = "flecha.png";
                    ((PopUpViewModel)PopUpView.BindingContext).Alturas = Heights;
                    ((PopUpViewModel)PopUpView.BindingContext).GridColumn = 0;
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                case "6":
                    Heights.Add(320);
                    Heights.Add(180);
                    ((PopUpViewModel)PopUpView.BindingContext).Message = "Este es el Total de gastos," +
                        " se calcula sumando todos los gastos, tanto gastos fijos como variables, se debe tener en cuenta cualquier salida de plata.";
                    ((PopUpViewModel)PopUpView.BindingContext).Rotacion = 90;
                    ((PopUpViewModel)PopUpView.BindingContext).Escala = 1;
                    ((PopUpViewModel)PopUpView.BindingContext).Source = "flecha1.png";
                    ((PopUpViewModel)PopUpView.BindingContext).Alturas = Heights;
                    ((PopUpViewModel)PopUpView.BindingContext).GridColumn = 1;
                    ((PopUpViewModel)PopUpView.BindingContext).Width = 60;
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
            }
        }
    }
}
