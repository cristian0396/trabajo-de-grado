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
            Routing.RegisterRoute(nameof(introPresupuesto), typeof(introPresupuesto));
            Routing.RegisterRoute(nameof(Presupuesto), typeof(Presupuesto));
            Routing.RegisterRoute(nameof(PlanGastos), typeof(PlanGastos));
            Routing.RegisterRoute(nameof(Ahorro), typeof(Ahorro));
            Routing.RegisterRoute(nameof(ActividadP1), typeof(ActividadP1));
            Routing.RegisterRoute(nameof(JuegoFruit), typeof(JuegoFruit));
            Routing.RegisterRoute(nameof(LeccionP1), typeof(LeccionP1));
        }

        public virtual async Task ConstructorAsync(object parameters)
        {
            await Task.FromResult(true);
        }
    }
}
