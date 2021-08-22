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
        public ICommand Leccion1Command { get; set; }
        public ICommand Leccion2Command { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand JuegoCommand { get; set; }
        public PopUp PopUp { get; set; }

        private int x = new int();
        public int llaves = new int();
        public PresupuestoViewModel()
        {
            PopUp = new PopUp();
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Leccion1Command = new Command(async () => await IrALeccion1(), () => true);
            Leccion2Command = new Command(async () => await IrALeccion2(), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
            JuegoCommand = new Command(async () => await IrAJuego(), () => true);
        }
        public async Task IrALeccion1() //Función que se activa al hacer click en la imagen de la primer lección
        {
            string sourceImage = "leccionfondo.png";
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
            if (x != 5)
            {
                x = 5;
            }
        }

        public async Task IrALeccion2() //Función que se activa al hacer click en la imagen de la segunda lección
        {
            string sourceImage = "fondo01.png";
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
         
        public async Task IrAJuego() //Función que se activa al hacer click en la imagen emoji
        {
            if (x == 10)
            {
                await Shell.Current.GoToAsync("JuegoFruit");
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
