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
    public class PlanGastosViewModel : ViewModelBase
    {
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand Leccion1Command { get; set; }
        public ICommand Leccion2Command { get; set; }
        public ICommand JuegoCommand { get; set; }
        public PopUp PopUp { get; set; }

        private int x = new int();
        public PlanGastosViewModel()
        {
            PopUp = new PopUp();
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Leccion1Command = new Command(async () => await IrALeccion1("fondoPlanGastos.png"), () => true);
            Leccion2Command = new Command(async () => await IrALeccion2("fondolecciondos.png"), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
            JuegoCommand = new Command(async () => await IrAJuego(), () => true);
        }
        public async Task IrALeccion1(string sourceImage) //Función que se activa al hacer click en la imagen de la primer lección
        {
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
            if (x != 5)
            {
                x = 5;
            }
        }
        public async Task IrALeccion2(string sourceImage) //Función que se activa al hacer click en la imagen de la primer lección
        {
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
            App.llaves += 1;
            if (x == 5)
            {
                x += 5;
            }
        }
        public async Task IrAInicio() //Función que se activa al dar click en el boton de atrás
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio, se realizan dos retrocesos
        }
        public async Task IrAJuego()
        {
            if (x == 10)
            {
                await Shell.Current.GoToAsync("JuegoBouncingBall");
            }
            else
            {
                ((MessageViewModel)PopUp.BindingContext).Titulo = "Aviso";
                ((MessageViewModel)PopUp.BindingContext).Message = "Aún no has terminado todas las lecciones del modulo";
                await PopupNavigation.Instance.PushAsync(PopUp);
            }
        }
    }
}
