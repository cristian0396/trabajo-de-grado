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
        public ICommand juegoCommand { get; set; }        

        public PresupuestoViewModel()
        {            
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Leccion1Command = new Command(async () => await IrALeccion1(), () => true);
            Leccion2Command = new Command(async () => await IrALeccion2(), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
            juegoCommand = new Command(async () => await IrAJuego(), () => true);
        }
        public async Task IrALeccion1() //Función que se activa al hacer click en la imagen de la primer lección
        {
            string sourceImage = "leccionfondo.png";
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
        }

        public async Task IrALeccion2() //Función que se activa al hacer click en la imagen de la segunda lección
        {
            string sourceImage = "fondo01.png";
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
        }

        public async Task IrAInicio() //Función que se activa al dar click en el boton de atrás
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio, se realizan dos retrocesos
        }
         
        public async Task IrAJuego() //Función que se activa al hacer click en la imagen emoji
        {
            await Shell.Current.GoToAsync("JuegoFruit");
        }
    }
}
