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
    public class LeccionP1ViewModel : ViewModelBase
    {
        public ICommand PopUpCommand { get; set; }
        public PopUp PopUpView { get; set; }

        public LeccionP1ViewModel() //constructor
        {
            InicializarComandos();
            PopUpView = new PopUp();
            ActivarPopUps();
        }

        public void InicializarComandos()
        {
            PopUpCommand = new Command(async () => await ActivarPopUps(), () => true);
        }
                
        public async Task ActivarPopUps()
        {
            List<int> Heights = new List<int>() {270, 270};
            List<string> Sources = new List<string> { "tablero0.png", "juan1.png"};
            Profes PopUpView2 = new Profes("1");
            ((ProfesViewModel)PopUpView2.BindingContext).Sources = Sources;
            ((ProfesViewModel)PopUpView2.BindingContext).Alturas = Heights;
            ((ProfesViewModel)PopUpView2.BindingContext).Opcion = "2";
            await PopupNavigation.Instance.PushAsync(PopUpView2);
        }
        
    }
}
