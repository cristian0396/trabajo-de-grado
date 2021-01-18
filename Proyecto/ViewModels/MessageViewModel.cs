using Proyecto.Vistas;
using Proyecto.Vistas.PopUps;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        #region Properties
        private string titulo;
        private string message;

        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }
        #endregion

        #region Getters y Setters
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters y Setters

        public MessageViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task NewPopUp()
        {
            PopUp5 PopUpView = new PopUp5();
            await PopupNavigation.Instance.PopAsync();
            ((MessageViewModel)PopUpView.BindingContext).Message = "Esta funcionando.";
            await PopupNavigation.Instance.PushAsync(PopUpView);
        }
    }
}
