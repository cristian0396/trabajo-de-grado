using Proyecto.Actividades.Entidades;
using Proyecto.ViewModels;
using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class ActividadAhorroViewModel : ViewModelBase
    {
        public int Ingresos { get; set; }
        public bool ActivarInput { get; set; }
        public bool ActivarInput2 { get; set; }
        public ObjetivoAhorro Objetivo { get; set; }
        public int Tiempo { get; set; }
        public int Costo { get; set; }
        public UnidadTiempoAhorro UnidadTiempo { get; set; }
        public PorcentajeAhorro Porcentaje { get; set; }
        public List<ObjetivoAhorro> ListaObjetivo { get; set; }
        public List<PorcentajeAhorro> ListaPorcentaje { get; set; }
        public List<UnidadTiempoAhorro> ListaUnidad { get; set; }
        public List<TAhorro> ListaTAhorro { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand BotonSimuladorCommand { get; set; }
        public ICommand ItemSeleccionadoCommand { get; set; }
        public ICommand ItemSeleccionadoCommand2 { get; set; }

        private TAhorro itemSeleccionado;
        private ObservableCollection<TAhorro> itemsList;
        public TAhorro ItemSeleccionado
        {
            get { return itemSeleccionado; }
            set
            {
                itemSeleccionado = value;
                OnPropertyChanged();
            }

        }
        public ObservableCollection<TAhorro> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                OnPropertyChanged();
            }
        }
        public ActividadAhorroViewModel()
        {
            ListaTAhorro = new List<TAhorro>();
            ItemsList = new ObservableCollection<TAhorro>();
            InicializarComandos();
            InicializarListasDesplegables();            
        }
        private void InicializarComandos()
        {
            BotonAtrasCommand = new Command(async () => await IrAAhorro(), () => true);
            ItemSeleccionadoCommand = new Command(HabilitarInputOtros1);
            ItemSeleccionadoCommand2 = new Command(HabilitarInputOtros2);
            BotonSimuladorCommand = new Command(async () => await GenerarReporte(), () => true);
        }

        public void HabilitarInputOtros1()
        {            
            if (Objetivo.Descripcion == "Otros")
            {
                ActivarInput = true;
            } else
            {
                ActivarInput = false;
            }
        }

        public void HabilitarInputOtros2()
        {            
            if (Porcentaje.Valor == "Otro")
            {
                ActivarInput2 = true;
            } else
            {
                ActivarInput2 = false;
            }
        }

        private void InicializarListasDesplegables()
        {
            ListaObjetivo = new List<ObjetivoAhorro>()
            {
                new ObjetivoAhorro(){ ID = 1, Descripcion = "Vivienda"},
                new ObjetivoAhorro(){ ID = 2, Descripcion = "Moto"},
                new ObjetivoAhorro(){ ID = 3, Descripcion = "Carro"},
                new ObjetivoAhorro(){ ID = 4, Descripcion = "Vacaciones"},
                new ObjetivoAhorro(){ ID = 5, Descripcion = "Negocio"},
                new ObjetivoAhorro(){ ID = 6, Descripcion = "Viajes"},
                new ObjetivoAhorro(){ ID = 7, Descripcion = "Ropa"},
                new ObjetivoAhorro(){ ID = 8, Descripcion = "Remodelación"},
                new ObjetivoAhorro(){ ID = 8, Descripcion = "Otros"},
            };

            ListaPorcentaje = new List<PorcentajeAhorro>
            {
                new PorcentajeAhorro(){ID = 1, Valor = "5%"},
                new PorcentajeAhorro(){ID = 2, Valor = "10%"},
                new PorcentajeAhorro(){ID = 3, Valor = "15%"},
                new PorcentajeAhorro(){ID = 4, Valor = "20%"},
                new PorcentajeAhorro(){ID = 5, Valor = "25%"},
                new PorcentajeAhorro(){ID = 6, Valor = "30%"},
                new PorcentajeAhorro(){ID = 7, Valor = "Otro"},
            };

            ListaUnidad = new List<UnidadTiempoAhorro>()
            {
                new UnidadTiempoAhorro(){ ID = 3, Valor = "Meses"},
                new UnidadTiempoAhorro(){ ID = 4, Valor = "Años"},
            };
        }
        public void GenerarTabla(int meses, float cuota)
        {
            ItemsList.Clear();
            var currentDate = DateTime.Now;
            float total = 0;
            for (int i = 1; i <= meses; i++)
            {
                TAhorro nuevaCuota = new TAhorro();
                nuevaCuota.Mes = i;
                nuevaCuota.Fecha = currentDate;
                nuevaCuota.Cuota = cuota;
                nuevaCuota.CuotaFormat = nuevaCuota.Cuota.ToString("C0");
                nuevaCuota.Total = cuota + total;
                nuevaCuota.TotalFormat = nuevaCuota.Total.ToString("C0");
                ItemsList.Add(nuevaCuota);
                currentDate = currentDate.AddMonths(1);
                total += cuota;
            }
        }

        public async Task GenerarReporte()
        {
            float cuota = (float)(Ingresos * (Convert.ToDouble(Porcentaje.Valor.Remove(Porcentaje.Valor.Length - 1)) /100));
            float total = 0;
            int cantidadCuotas = 0;
            PopUp PopUpView = new PopUp();
            switch (UnidadTiempo.Valor)
            {                
                case "Meses":
                    total = cuota * Tiempo;
                    cantidadCuotas = Tiempo;
                    break;
                case "Años":
                    total = cuota * Tiempo * 12;
                    cantidadCuotas = Tiempo * 12;
                    break;
            }
            if(total < Costo)
            {
                string mensaje = "Lo sentimos... parece ser que el tiempo para cumplir tu meta es muy corto" +
                    " pues el valor monetario de tu objetivo es más alto que lo que alcanzas a ahorrar en " + Tiempo + " " + UnidadTiempo.Valor;
                ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, image: "rodri.png");
                await PopupNavigation.Instance.PushAsync(PopUpView);
            } else
            {
                string mensaje = "Muy bien! Revisa la tabla que hemos generado para ti, podrás ver las cuotas " +
                    " que tienes que aportar mensualmente para alcanzar tu objetivo en el tiempo deseado. Tambien podras ver las fechas en que debes dar el aporte y el saldo que irás acumulando, mucha suerte!";
                ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, image: "bien.png");
                await PopupNavigation.Instance.PushAsync(PopUpView);
                GenerarTabla(cantidadCuotas, cuota);
            }
        }
        public async Task IrAAhorro()
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio de presupuesto, se realizan dos retrocesos
        }
        public class ObjetivoAhorro
        {
            public int ID { get; set; }
            public string Descripcion { get; set; }
        }

        public class UnidadTiempoAhorro
        {
            public int ID { get; set; }
            public string Valor { get; set; }
        }

        public class PorcentajeAhorro
        {
            public int ID { get; set; }
            public string Valor { get; set; }
        }
    }
}
