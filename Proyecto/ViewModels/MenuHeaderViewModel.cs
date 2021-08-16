using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.ViewModels
{
    public class MenuHeaderViewModel : ViewModelBase
    {
        private string correo;
        private string usuario;

        public string Correo
        {
            get { return correo; }
            set
            {
                correo = value;
                OnPropertyChanged();
            }
        }

        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged();
            }
        }
        public MenuHeaderViewModel()
        {
            Correo = App.CurrentUser.Correo;
            Usuario = App.CurrentUser.Usuario;
        }
    }
}
