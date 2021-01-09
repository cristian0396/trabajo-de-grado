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
    public class PresupuestoViewModel : ViewModelBase
    {
        public ICommand Actividad1Command { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand juegoCommand { get; set; }
        public PopUp PopUpView { get; set; }

        public PresupuestoViewModel()
        {
            PopUpView = new PopUp();
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Actividad1Command = new Command(async () => await IrAActividad1(), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
            juegoCommand = new Command(async () => await IrAJuego(), () => true);
        }
        public async Task IrAActividad1() //Función para que se activa al hacer click en la imagen de la primer lección
        {
            await Shell.Current.GoToAsync("LeccionP1");
        }

        public async Task IrAInicio() //Función que se activa al dar click en el boton de atrás
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio, se realizan dos retrocesos
        }
         
        public async Task IrAJuego() //Función para que se activa al hacer click en la imagen de la primer lección
        {
            //await Shell.Current.GoToAsync("JuegoFruit");
            ((MessageViewModel)PopUpView.BindingContext).Titulo = "Instrucciones";
            ((MessageViewModel)PopUpView.BindingContext).Message = "Esta es la primer instrucción";            
            await PopupNavigation.Instance.PushAsync(PopUpView);
        }
    }
}
