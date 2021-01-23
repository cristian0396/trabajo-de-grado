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
        public ICommand ActividadP1Command { get; set; }
        public ICommand PopUpCommand { get; set; }
        public PopUp PopUpView { get; set; }

        public LeccionP1ViewModel() //constructor
        {
            InicializarComandos();
            PopUpView = new PopUp();
        }

        public void InicializarComandos()
        {
            ActividadP1Command = new Command(async () => await IrActividadP1(), () => true);
            PopUpCommand = new Command(async () => await ActivarPopUps(), () => true);
        }

        public async Task IrActividadP1()
        {
            await Shell.Current.GoToAsync("ActividadP1");
            ((MessageViewModel)PopUpView.BindingContext).Titulo = "Instrucciones";
            ((MessageViewModel)PopUpView.BindingContext).Message = "1. Relaciona conceptos, encaja la ficha derecha con alguna del lado izquierdo.\n" +
                "\n" +
                "2. Si encajas la ficha donde no es, pierdes 5 puntos.\n" +
                "\n" +
                "3. Si la encajas correctamente ganas 10 puntos.\n" +
                "\n" +
                "4. Cada vez que encajes una pieza, aparece otra en la esquina superior derecha.\n" +
                "\n" +
                "5. Los puntos que obtengas, se guardaran automaticamente.\n";
            await PopupNavigation.Instance.PushAsync(PopUpView);
        }
                
        public async Task ActivarPopUps()
        {
            //await PopupNavigation.Instance.PushAsync(PopUpView2);
        }
        
    }
}
