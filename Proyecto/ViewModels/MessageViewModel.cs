using Proyecto.Vistas;
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
        private string image;
        public bool ActivarConsejos { get; set; }
        public int TotalIngresos { get; set; }
        public int TotalGastos { get; set; }
        public bool Mensual { get; set; }        
        public string Opcion { get; set; }
        public ICommand CloseCommand { get; set; }
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

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
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
        }

        public void InitializeFields(MessageViewModel _popUp, string titulo = default(string), string mensaje = default(string), bool activarConsejos = default(bool))
        {
            _popUp.Titulo = titulo;
            _popUp.Message = mensaje;
            _popUp.ActivarConsejos = activarConsejos;
        }
        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
            if (ActivarConsejos)
            {
                int dineroLibre = TotalIngresos - TotalGastos;
                int ahorro = Convert.ToInt32(dineroLibre * 0.1);
                int otros = Convert.ToInt32(dineroLibre * 0.9);
                int inversion = Convert.ToInt32(ahorro * 0.5);
                int anios = 0;
                if (Mensual)
                {
                    while(inversion < 500000)
                    {
                        inversion += ahorro * 12;
                        anios += 1;
                    }
                }
                else
                {
                    anios = 1;
                    while (inversion < 500000)
                    {
                        inversion += ahorro * anios;
                        anios += 1;
                    }

                }
                PopUp PopUpView = new PopUp();
                ((MessageViewModel)PopUpView.BindingContext).Titulo = "Consejos!!";
                ((MessageViewModel)PopUpView.BindingContext).Message = "Te recomendamos en tu caso especifico destinar a \n" +
                    "\n-Ahorro: " +  ahorro.ToString("C") + " (el 10% del dinero libre) \n" +
                    "\nlo que quiere decir que el dinero restante ( " + otros.ToString("C") + " ) lo puedes utilizar en entretenimiento o lo que tu quieras!!\n" +
                    "\nPor otro lado, en " + anios +  " año(s) puedes tener alrededor de " + inversion.ToString("C") +  " pesos e invertirlos en algún negocio!";
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }            
        }
    }
}
