using Proyecto.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class InicioViewModel : ViewModelBase
    {
        #region Properties
        public ICommand PresupuestoCommand { get; set; }
        public ICommand PlanGCommand { get; set; }
        public ICommand AhorroCommand { get; set; }
        #endregion Properties

        public InicioViewModel()
        {
            InicializarComandos();            
        }

        public void InicializarComandos()
        {
            PresupuestoCommand = new Command(async () => await IrAPresupuesto("1"), () => true);
            PlanGCommand = new Command(async () => await IrAPresupuesto("2"), () => true);
            AhorroCommand = new Command(async () => await IrAPresupuesto("3"), () => true);
        }
        public async Task IrAPresupuesto(string idModulo)
        {
            await Shell.Current.GoToAsync("moduloPresupuesto");
        }
    }
}
