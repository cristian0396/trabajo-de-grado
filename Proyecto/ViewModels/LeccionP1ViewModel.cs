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

        public LeccionP1ViewModel() //constructor
        {
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            ActividadP1Command = new Command(async () => await IrActividadP1(), () => true);
        }

        public async Task IrActividadP1() 
        { 
            await Shell.Current.GoToAsync("ActividadP2"); 
        } 
    }
}
