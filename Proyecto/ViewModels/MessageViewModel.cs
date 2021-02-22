﻿using Proyecto.Vistas;
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
        public ICommand CloseCommand { get; set; }
        public string Opcion { get; set; }
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

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
