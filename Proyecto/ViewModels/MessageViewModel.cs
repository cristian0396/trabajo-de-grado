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

        public void InitializeFields(MessageViewModel _popUp, string titulo = default(string), string mensaje = default(string), bool activarConsejos = default(bool), string image = default(string), int totalIngresos = default(int), int totalGastos = default(int), bool mensual = default(bool))
        {
            _popUp.Titulo = titulo;
            _popUp.Message = mensaje;
            _popUp.ActivarConsejos = activarConsejos;
            _popUp.TotalIngresos = totalIngresos;
            _popUp.TotalGastos = totalGastos;
            _popUp.Mensual = mensual;
        }
        public async Task ActivarVentanaActividadP1(PopUp PopUpView)
        {
            string mensaje = "1. Relaciona conceptos, encaja la ficha derecha con alguna del lado izquierdo.\n" +
                "\n" +
                "2. Si encajas la ficha donde no es, pierdes 5 puntos.\n" +
                "\n" +
                "3. Si la encajas correctamente ganas 10 puntos.\n" +
                "\n" +
                "4. Cada vez que encajes una pieza, aparece otra en la esquina superior derecha.\n" +
                "\n" +
                "5. Los puntos que obtengas, se guardaran automaticamente.\n";
            ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, titulo: "Instrucciones", activarConsejos: false);
            await PopupNavigation.Instance.PushAsync(PopUpView);
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
                string mensaje = "Te recomendamos en tu caso especifico destinar a \n" +
                    "\n-Ahorro: " + ahorro.ToString("C") + " (el 10% del dinero libre) \n" +
                    "\nlo que quiere decir que el dinero restante ( " + otros.ToString("C") + " ) lo puedes utilizar en entretenimiento o lo que tu quieras!!\n" +
                    "\nPor otro lado, en " + anios + " año(s) puedes tener alrededor de " + inversion.ToString("C") + " pesos e invertirlos en algún negocio!";
                ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, titulo: "Consejos!!");
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }            
        }
    }
}
