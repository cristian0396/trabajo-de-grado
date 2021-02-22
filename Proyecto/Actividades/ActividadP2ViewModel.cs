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
    public class ActividadP2ViewModel : ViewModelBase
    {
        public int TotalIngresos { get; set; }
        public int PosicionActual { get; set; }
        public int TotalGastos { get; set; }
        public bool ActivarPicker { get; set; }
        public IngresoGastos Tema { get; set; }
        public Periodo Periocicidad { get; set; }
        public Descripciones DescEscogido { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand BotonReporteCommand { get; set; }
        public ICommand BotonLimpiarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand ItemSeleccionadoCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public string Valor { get; set; }
        public List<IngresoGastos> IngreGastos { get; set; }
        public List<Descripciones> ListaDescrip { get; set; }
        public List<Periodo> ListaPeriodo { get; set; }
        public List<TPresupuesto> ListaTPresupuesto { get; set; }

        private TPresupuesto itemSeleccionado;
        public TPresupuesto ItemSeleccionado
        {
            get { return itemSeleccionado; }
            set
            {
                itemSeleccionado = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<TPresupuesto> itemsList; 
                
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
            PosicionActual = 0;
            TotalIngresos = 0;
            TotalGastos = 0;
            ActivarPicker = false;
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
            ListaTPresupuesto = new List<TPresupuesto>();
            ItemsList = new ObservableCollection<TPresupuesto>();
            InicializarComandos();
        }

        public void InicializarComandos()
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
        }

        public void EliminarItemSeleccionado()
        {
            if(itemsList.Count > 0)
            {
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
            PosicionActual += 1;
            temp.Descripcion = DescEscogido.Descripcion;
            temp.Fecha = DateTime.Now;
            if(Tema.Nombre == "Ingresos")
            {
                if(Periocicidad.Valor == "Anual")
                {
                    valorAnual = Convert.ToInt32(Valor) * 12;
                    TotalIngresos += valorAnual;
                    temp.Ingresos = Convert.ToString(valorAnual);
                }
                else
                {
                    temp.Ingresos = Valor;
                    TotalIngresos += Convert.ToInt32(Valor);
                }                
            }
            else
            {
                if (Periocicidad.Valor == "Anual")
                {
                    valorAnual = Convert.ToInt32(Valor) * 12;
                    TotalGastos += valorAnual;
                    temp.Gastos = Convert.ToString(valorAnual);

                }
                else
                {
                    temp.Gastos = Valor;
                    TotalGastos += Convert.ToInt32(Valor);
                }                
            }
            
            //ListaTPresupuesto.Add(temp);
            ItemsList.Add(temp);
            //ItemsList = new ObservableCollection<TPresupuesto>(ListaTPresupuesto);            
        }

        public async Task GenerarReporte()
        {
            PopUp PopUpView = new PopUp();
            if (TotalIngresos > TotalGastos)
            {
                ((MessageViewModel)PopUpView.BindingContext).Image = "bien.png";
                ((MessageViewModel)PopUpView.BindingContext).Message = "Tus ingresos están por encima de tus gastos, vas muy bien," +
                    " eso significa que puedes ahorrar o darte un gusto de vez en cuando, pero con moderación, también podrías ahorrar lo suficiente" +
                    " para poder invertirlo en lo que tu quieras.";
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }
            else if(TotalIngresos < TotalGastos){
                ((MessageViewModel)PopUpView.BindingContext).Image = "rodri.png";
                ((MessageViewModel)PopUpView.BindingContext).Message = "Tus gastos están por encima de tus ingresos, eso es una mala noticia," +
                    " recuerda que si gastas mas de lo que ganas no podrás ahorrar para cumplir tus metas, así que ponte las pilas y revisa tus gastos " +
                    " para poder gastar un poco menos. También podrías buscar alternativas de ingresos para poder nivelarte con tus gastos.";
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }
            
        }
        public async Task IrAPresupuesto() //Función que se activa al dar click en el boton de atrás
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
