using System;
using System.Collections.Generic;
using System.Text;
using Proyecto.Propagacion;
using System.Threading.Tasks;
using Proyecto.Vistas;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class ViewModelBase : NotificationObjectModel
    {
        public ViewModelBase()
        {
            RegistrarRutas();
        }
        public void RegistrarRutas()
        {
            Routing.RegisterRoute("//moduloPresupuesto", typeof(introPresupuesto));
            Routing.RegisterRoute("Presupuesto", typeof(Presupuesto));
            Routing.RegisterRoute("PlanGastos", typeof(PlanGastos));
            Routing.RegisterRoute("Ahorro", typeof(Ahorro));
            Routing.RegisterRoute("ActividadP1", typeof(ActividadP1));
        }

        public virtual async Task ConstructorAsync(object parameters)
        {
            await Task.FromResult(true);
        }
    }
}
