using System;
using System.Collections.Generic;
using System.Text;
using Proyecto.Propagacion;
using Proyecto.Servicios.Navegacion;
using System.Threading.Tasks;

namespace Proyecto.ViewModels
{
    public class ViewModelBase : NotificationObjectModel
    {
        #region Properties
        public NavegationService NavigationService { get; set; }
        #endregion

        #region Getters & Setters

        #endregion Getters & Setters

        public ViewModelBase()
        {
            NavigationService = App.NavigationService;
        }

        public virtual async Task ConstructorAsync(object parameters)
        {
            await Task.FromResult(true);
        }
    }
}
