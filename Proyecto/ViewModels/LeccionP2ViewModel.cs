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
        public ICommand ActividadP2Command { get; set; }

        public LeccionP2ViewModel() //constructor
        {
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            ActividadP2Command = new Command(async () => await IrActividadP2(), () => true);
        }

        public async Task IrActividadP2()
        {
            await Shell.Current.GoToAsync("ActividadP2");
        }
    }
}
