using Proyecto.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class AhorroViewModel : ViewModelBase
    {
        public ICommand BotonAtrasCommand { get; set; }

        public AhorroViewModel()
        {
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            BotonAtrasCommand = new Command(async () => await IrAInicio(), () => true);
        }
        public async Task IrAInicio()
        {
            await Shell.Current.GoToAsync("../..");
        }
    }
}
