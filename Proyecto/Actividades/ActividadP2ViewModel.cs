using Proyecto.Actividades.Entidades;
using Proyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class ActividadP2ViewModel : ViewModelBase
    {
        public string Descripcion { get; set; }
        public string Ingresos { get; set; }
        public string Gastos { get; set; }

        private List<TPresupuesto> listaTPresupuesto;
        public ObservableCollection<TPresupuesto> ItemsList { get; set; } 

        private TPresupuesto itemPresupuesto;
        public ICommand AgregarCommand { get; set; }
        private TPresupuesto ItemPresupuesto
        {
            get { return itemPresupuesto; }
            set
            {
                itemPresupuesto = value;
                OnPropertyChanged();
            }
        }
        private List<TPresupuesto> ListaTPresupuesto
        {
            get { return listaTPresupuesto; }
            set
            {
                listaTPresupuesto = value;
                OnPropertyChanged();
            }
        }
        public ActividadP2ViewModel()
        {
            listaTPresupuesto = new List<TPresupuesto>();
            ItemsList = new ObservableCollection<TPresupuesto>();
            itemPresupuesto = new TPresupuesto();
            InicializarComandos();
        }

        public void InicializarComandos()
        {
            AgregarCommand = new Command(AgregarItemLista);
        }
        public void AgregarItemLista()
        {
            ItemPresupuesto.Descripcion = Descripcion;
            ItemPresupuesto.Ingresos = Ingresos;
            ItemPresupuesto.Gastos = Gastos;
            ListaTPresupuesto.Add(ItemPresupuesto);
            ItemsList.Add(ItemPresupuesto);
            //ItemsList = new ObservableCollection<TPresupuesto>(ListaTPresupuesto);
        }
    }
}
