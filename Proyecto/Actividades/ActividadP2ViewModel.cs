using Proyecto.Actividades.Entidades;
using Proyecto.ViewModels;
using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class ActividadP2ViewModel : ViewModelBase
    {
        public int TotalIngresos { get; set; }
        public int PosicionActual { get; set; }
        public int TotalGastos { get; set; }
        public string TotalGastosFormat { get; set; }
        public string TotalIngresosFormat { get; set; }
        public bool ActivarPicker { get; set; }
        public IngresoGastos Tema { get; set; }
        public Periodo Periocicidad { get; set; }
        public Descripciones DescEscogido { get; set; }        
        public string Valor { get; set; }
        public List<IngresoGastos> IngreGastos { get; set; }
        public List<Descripciones> ListaDescrip { get; set; }
        public List<Periodo> ListaPeriodo { get; set; }
        public List<TPresupuesto> ListaTPresupuesto { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand BotonReporteCommand { get; set; }
        public ICommand BotonLimpiarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand ItemSeleccionadoCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        private TPresupuesto itemSeleccionado;
        private ObservableCollection<TPresupuesto> itemsList;
        public TPresupuesto ItemSeleccionado
        {
            get { return itemSeleccionado; }
            set
            {
                itemSeleccionado = value;
                OnPropertyChanged();
            }

        }               
        public ObservableCollection<TPresupuesto> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                OnPropertyChanged();
            }
        }
        public ActividadP2ViewModel()
        {
            InicializarVars();
            InicializarListasDesplegables();           
            InicializarComandos();
        }

        private void InicializarVars()
        {
            PosicionActual = 0;
            TotalIngresos = 0;
            TotalGastos = 0;
            TotalGastosFormat = 0.ToString("C");
            TotalIngresosFormat = 0.ToString("C");
            ActivarPicker = false;
            ListaTPresupuesto = new List<TPresupuesto>();
            ItemsList = new ObservableCollection<TPresupuesto>();
        }

        private void InicializarListasDesplegables()
        {
            IngreGastos = new List<IngresoGastos>()
            {
                new IngresoGastos(){ ID = 1, Nombre = "Ingresos"},
                new IngresoGastos(){ ID = 2, Nombre = "Gastos"}
            };

            ListaPeriodo = new List<Periodo>
            {
                new Periodo(){ID = 1, Valor = "Mensual"},
                new Periodo(){ID = 2, Valor = "Anual"}
            };
        }

        private void InicializarComandos()
        {
            AgregarCommand = new Command(AgregarItemLista);
            BotonAtrasCommand = new Command(async () => await IrAPresupuesto(), () => true);
            ItemSeleccionadoCommand = new Command(HabilitarPickerDescripcion);
            BotonReporteCommand = new Command(async () => await GenerarReporte(), () => true);
            BotonLimpiarCommand = new Command(VaciarListaItems);
            EliminarCommand = new Command(EliminarItemSeleccionado);
        }

        public void VaciarListaItems()
        {
            ItemsList.Clear();
            TotalGastos = 0;
            TotalIngresos = 0;
            PosicionActual = 0;
            TotalIngresosFormat = 0.ToString("C"); 
            TotalGastosFormat = 0.ToString("C");
        }

        public void EliminarItemSeleccionado()
        {
            if(itemsList.Count > 0)
            {
                if(itemSeleccionado.Ingresos != "")
                {
                    TotalIngresos -= int.Parse(ItemSeleccionado.Ingresos, NumberStyles.Currency);
                    TotalIngresosFormat = TotalIngresos.ToString("C");                    
                }
                else
                {
                    TotalGastos -= int.Parse(ItemSeleccionado.Gastos, NumberStyles.Currency);
                    TotalGastosFormat = TotalGastos.ToString("C");
                }                
                int indice = ItemsList.IndexOf(ItemSeleccionado);
                ItemsList.RemoveAt(indice);
                PosicionActual -= 1;
            }            
        }
        public void HabilitarPickerDescripcion()
        {
            ActivarPicker = true;
            if(Tema.Nombre == "Ingresos")
            {
                ListaDescrip = new List<Descripciones>()
                {
                //tipos de ingresos
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Salario"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Auxilio de transporte"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Subsidio"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Negocio"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Inversión"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Pensión"},
                new Descripciones(){ Tipo = "Ingresos", Descripcion = "Otros"},
                };
            }
            else
            {
                ListaDescrip = new List<Descripciones>()
                {
                //tipos de gastos
                new Descripciones() { Tipo = "Gastos", Descripcion = "Transporte" },
                new Descripciones() { Tipo = "Gastos", Descripcion = "Arriendo" },
                new Descripciones() { Tipo = "Gastos", Descripcion = "Salud" },
                new Descripciones() { Tipo = "Gastos", Descripcion = "Entretenimiento" },
                new Descripciones() { Tipo = "Gastos", Descripcion = "Alimentación" },
                new Descripciones() { Tipo = "Gastos", Descripcion = "Improvistos" }
                };
            }
        }
        public void AgregarItemLista()
        {
            TPresupuesto temp = new TPresupuesto();
            int valorAnual;
            temp.ID = PosicionActual;
            temp.Descripcion = DescEscogido.Descripcion;
            temp.Fecha = DateTime.Now;
            PosicionActual += 1;
            
            if(Tema.Nombre == "Ingresos")
            {
                if(Periocicidad.Valor == "Anual")
                {
                    valorAnual = Convert.ToInt32(Valor) * 12;
                    TotalIngresos += valorAnual;
                    TotalIngresosFormat = TotalIngresos.ToString("C");
                    temp.Ingresos = valorAnual.ToString("C");
                }
                else
                {
                    temp.Ingresos = Convert.ToInt32(Valor).ToString("C");
                    TotalIngresos += Convert.ToInt32(Valor);
                    TotalIngresosFormat = Convert.ToInt32(TotalIngresos).ToString("C");
                }                
            }
            else
            {
                if (Periocicidad.Valor == "Anual")
                {
                    valorAnual = Convert.ToInt32(Valor) * 12;
                    TotalGastos += valorAnual;
                    TotalGastosFormat = TotalGastos.ToString("C");
                    temp.Gastos = valorAnual.ToString("C");

                }
                else
                {
                    temp.Gastos = Convert.ToInt32(Valor).ToString("C");
                    TotalGastos += Convert.ToInt32(Valor);
                    TotalGastosFormat = Convert.ToInt32(TotalGastos).ToString("C");
                }                
            }
            ItemsList.Add(temp);        
        }

        public async Task GenerarReporte()
        {
            PopUp PopUpView = new PopUp();
            bool mensual;
            string mensaje;
            if(Periocicidad.Valor == "Mensual")
            {
                mensual = true;
            }
            else
            {
                mensual = false;
            }
            if (TotalIngresos > TotalGastos)
            {
                mensaje = "Tus ingresos están por encima de tus gastos, vas muy bien," +
                    " eso significa que puedes ahorrar o darte un gusto de vez en cuando, pero con moderación, también podrías ahorrar lo suficiente" +
                    " para poder invertirlo en lo que tu quieras.";
                ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, image: "bien.png", activarConsejos: true, totalIngresos: TotalIngresos, totalGastos: TotalGastos, mensual: mensual);
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }
            else if(TotalIngresos < TotalGastos){
                mensaje = "Tus gastos están por encima de tus ingresos, eso es una mala noticia," +
                    " recuerda que si gastas mas de lo que ganas no podrás ahorrar para cumplir tus metas, así que ponte las pilas y revisa tus gastos " +
                    " para poder gastar un poco menos. También podrías buscar alternativas de ingresos para poder nivelarte con tus gastos.";
                ((MessageViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((MessageViewModel)PopUpView.BindingContext), mensaje: mensaje, image: "rodri.png");
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }
            
        }
        public async Task IrAPresupuesto() 
        {
            await Shell.Current.GoToAsync("../.."); //Con esto se logra retroceder al inicio de presupuesto, se realizan dos retrocesos
        }
        public class IngresoGastos
        {
            public int ID { get; set; }
            public string Nombre {get; set;}
        }

        public class Descripciones
        {
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
        }

        public class Periodo
        {
            public int ID { get; set; }
            public string Valor { get; set; }
        }
    }
}
