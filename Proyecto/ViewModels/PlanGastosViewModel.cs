using Proyecto.Vistas;
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

        public PlanGastosViewModel()
        {
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Leccion1Command = new Command(async () => await IrALeccion1("fondoPlanGastos.png"), () => true);
            Leccion2Command = new Command(async () => await IrALeccion2("fondolecciondos.png"), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
        }
        public async Task IrALeccion1(string sourceImage) //Función que se activa al hacer click en la imagen de la primer lección
        {
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
        }

        public async Task IrALeccion2(string sourceImage) //Función que se activa al hacer click en la imagen de la primer lección
        {
            await Shell.Current.GoToAsync($"{nameof(FondoLecciones)}?SourceImg={sourceImage}");
        }
        public async Task IrAInicio() //Función que se activa al dar click en el boton de atrás
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio, se realizan dos retrocesos
        }
    }
}
