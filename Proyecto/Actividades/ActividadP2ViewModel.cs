using Proyecto.Actividades.Entidades;
using Proyecto.ViewModels;
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
        public string Descripcion { get; set; }
        public bool ActivarPicker { get; set; }
        public IngresoGastos Tema { get; set; }
        public Descripciones DescEscogido { get; set; }
        public ICommand BotonAtrasCommand { get; set; }
        public ICommand ItemSeleccionadoCommand { get; set; }
        public string Valor { get; set; }
        public List<IngresoGastos> IngreGastos { get; set; }
        public List<Descripciones> ListaDescrip { get; set; }
        public List<TPresupuesto> ListaTPresupuesto { get; set; }
        private ObservableCollection<TPresupuesto> itemsList; 
        public ICommand AgregarCommand { get; set; }        
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
            ActivarPicker = false;

            IngreGastos = new List<IngresoGastos>()
            {
                new IngresoGastos(){ ID = 1, Nombre = "Ingresos"},
                new IngresoGastos(){ ID = 2, Nombre = "Gastos"}           
            };
            
            ListaTPresupuesto = new List<TPresupuesto>();
            ItemsList = new ObservableCollection<TPresupuesto>();
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            AgregarCommand = new Command(AgregarItemLista);
            BotonAtrasCommand = new Command(async () => await IrAPresupuesto(), () => true);
            ItemSeleccionadoCommand = new Command(async () => await HabilitarPickerDescripcion(), () => true);
        }

        public async Task HabilitarPickerDescripcion()
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
            temp.Descripcion = Descripcion;
            if(Tema.Nombre == "Ingresos")
            {
                temp.Ingresos = Valor;
            }
            else
            {
                temp.Gastos = Valor;
            }            
            //ListaTPresupuesto.Add(temp);
            ItemsList.Add(temp);
            //ItemsList = new ObservableCollection<TPresupuesto>(ListaTPresupuesto);            
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
    }
}
