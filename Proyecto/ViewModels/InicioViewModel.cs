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
    public class InicioViewModel : ViewModelBase
    {
        #region Properties
        public ICommand PresupuestoCommand { get; set; }
        public ICommand PlanGCommand { get; set; }
        public ICommand AhorroCommand { get; set; }
        public PopUp PopUp { get; set; }
        #endregion Properties

        #region Metodos        
        public InicioViewModel()
        {
            PopUp = new PopUp();
            InicializarComandos();            
        }

        public void InicializarComandos()
        {
            PresupuestoCommand = new Command(async () => await IrAlModulo("1"), () => true);
            PlanGCommand = new Command(async () => await IrAlModulo("2"), () => true);
            AhorroCommand = new Command(async () => await IrAlModulo("3"), () => true);
        }
        //Función que se encarga de dirigir la navegación hacia las vistas de introducción de los modulos
        public async Task IrAlModulo(string idModulo) 
        { //dependiendo del parametro idModulo se determina a que introducción ir, 1 para presupuesto, 2 para gastos, 3 para Ahorro
            if (idModulo == "1")
            {
                await Shell.Current.GoToAsync($"{nameof(introPresupuesto)}?Modulo={idModulo}"); //Navegación mediante URIS O URL's, se usan queryparameters para pasar información entre vistas
            }
            else if (idModulo == "2" && App.llaves >= 1)
            {
                await Shell.Current.GoToAsync($"{nameof(introPresupuesto)}?Modulo={idModulo}"); //Navegación mediante URIS O URL's, se usan queryparameters para pasar información entre vistas
            }
            else if (idModulo == "3" && App.llaves >= 2)
            {
                await Shell.Current.GoToAsync($"{nameof(introPresupuesto)}?Modulo={idModulo}"); //Navegación mediante URIS O URL's, se usan queryparameters para pasar información entre vistas
            }
            else
            {
                ((MessageViewModel)PopUp.BindingContext).Titulo = "Aviso";
                ((MessageViewModel)PopUp.BindingContext).Message = "¡Aún no has terminado el modulo anterior, por favor regresa terminalo!";
                await PopupNavigation.Instance.PushAsync(PopUp);
            }
        } //El queryParameter llega al cs de la vista de IntroPresupuesto para luego ser enviado al viewModel
        #endregion Metodos
    }
}
