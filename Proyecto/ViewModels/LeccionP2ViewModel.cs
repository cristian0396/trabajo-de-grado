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
    public class LeccionP2ViewModel : ViewModelBase
    {
        public ICommand PopUpCommand { get; set; }

        public LeccionP2ViewModel() //constructor
        {
            InicializarComandos();
            ActivarPopUps();
        }

        public void InicializarComandos()
        {
            PopUpCommand = new Command(async () => await ActivarPopUps(), () => true);
        }

        public async Task ActivarPopUps()
        {
            List<int> Heights = new List<int>() { 240, 260 };
            List<string> Sources = new List<string> { "tablero10.png", "mono3.png" };
            Profes PopUpView2 = new Profes("6", false);
            ((ProfesViewModel)PopUpView2.BindingContext).Sources = Sources;
            ((ProfesViewModel)PopUpView2.BindingContext).Alturas = Heights;
            ((ProfesViewModel)PopUpView2.BindingContext).Opcion = "7";
            ((ProfesViewModel)PopUpView2.BindingContext).Actividad = 2;
            await PopupNavigation.Instance.PushAsync(PopUpView2);
        }
    }
}
