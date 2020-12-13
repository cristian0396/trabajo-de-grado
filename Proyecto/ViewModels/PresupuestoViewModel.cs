using Proyecto.Vistas;
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

        public PresupuestoViewModel()
        {
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            Actividad1Command = new Command(async () => await IrAActividad1(), () => true);
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
        }
        public async Task IrAActividad1()
        {
            await Shell.Current.GoToAsync("ActividadP1");
        }

        public async Task IrAInicio()
        {
            await Shell.Current.GoToAsync("../..");
        }
    }
}
