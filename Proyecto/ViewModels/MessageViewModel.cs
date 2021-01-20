using Proyecto.Vistas;
using Proyecto.Vistas.PopUps;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        #region Properties
        private string titulo;
        private string message;

        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }

        public string Opcion { get; set; }
        #endregion

        #region Getters y Setters
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters y Setters

        public MessageViewModel()
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
            PopUp3 PopUpView3 = new PopUp3();
            PopUp4 PopUpView4 = new PopUp4();
            PopUp5 PopUpView5 = new PopUp5();
            PopUp6 PopUpView6 = new PopUp6();
            await PopupNavigation.Instance.PopAsync();
            switch (Opcion) 
            {
                case "3":
                    ((MessageViewModel)PopUpView3.BindingContext).Message = "Estos gastos generalmente no cambian" +
                        " es decir, se mantienen porque cubren las necesidades más basicas de cada persona.";
                    ((MessageViewModel)PopUpView3.BindingContext).Opcion = "4";
                    await PopupNavigation.Instance.PushAsync(PopUpView3);
                    break;
                case "4":
                    ((MessageViewModel)PopUpView4.BindingContext).Message = "Estos gastos son todos aquellos que pueden diferir en cada mes" +
                        " según la actividad de la persona.";
                    ((MessageViewModel)PopUpView4.BindingContext).Opcion = "5";
                    await PopupNavigation.Instance.PushAsync(PopUpView4);
                    break;
                case "5":
                    ((MessageViewModel)PopUpView5.BindingContext).Message = "El ahorro es definido por cada persona para que se acomode a sus capacidades económicas, " +
                        "se recomienda que sea fijo y de al menos el 10% del total de los ingresos.";
                    ((MessageViewModel)PopUpView5.BindingContext).Opcion = "6";
                    await PopupNavigation.Instance.PushAsync(PopUpView5);
                    break;
                case "6":
                    ((MessageViewModel)PopUpView6.BindingContext).Message = "Este es el Total de gastos," +
                        " se calcula sumando todos los gastos, tanto gastos fijos como variables, se debe tener en cuenta cualquier salida de plata.";
                    await PopupNavigation.Instance.PushAsync(PopUpView6);
                    break;                    
            }
        }
    }
}
