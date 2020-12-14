using Proyecto.Modelos;
using Proyecto.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;

namespace Proyecto.ViewModels
{
    [AddINotifyPropertyChangedInterface]
   
    public class IntroPresupuestoViewModel : ViewModelBase
    {
        public ObservableCollection<IntroduccionModel> Introduccion { get; set; }
        public int PosicionIndice { get; set; }
        public string IDModulo { get; set; }
        public string TextoBotonSiguiente
        {
            get
            {
                if (PosicionIndice ==  Introduccion.Count - 1)
                    return "HECHO";
                return "SIGUIENTE";
            }
        }
        public ICommand ComandoSiguiente { get; set; }
        public ICommand ComandoOmitir { get; set; }

        public IntroPresupuestoViewModel(string Modulo)
        {
            IDModulo = Modulo;
            if (IDModulo == "1")
            {
                Introduccion = new ObservableCollection<IntroduccionModel>
                {
                    new IntroduccionModel()
                    {
                        DirImagen = "ledger.png",
                        Titulo = "Modulo de Presupuesto",
                        Contenido = "Es la cantidad de plata calculada para hacer frente a los gastos generales de la vida cotidiana, de un viaje u otros. Es decir que es una estimación anticipada de ingresos y gastos que habrán de producirse en un período determinado",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "importantPresu.png",
                        Titulo = "Importancia",
                        Contenido = "Esta es la corta descripcion de la importancia del presupuesto",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "IdeaPresu.png",
                        Titulo = "Ejemplos",
                        Contenido = "Aqui hay un ejemplo",
                        CarouselItem = new RecorridoItemPage()
                    }
                };
            }
            else if(IDModulo == "2")
            {
                Introduccion = new ObservableCollection<IntroduccionModel>
                {
                    new IntroduccionModel()
                    {
                        DirImagen = "ejemplo.png",
                        Titulo = "Modulo de Plan de Gastos",
                        Contenido = "Esta es la corta definición de presupuesto",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "money.png",
                        Titulo = "Importancia",
                        Contenido = "Esta es la corta descripcion de la importancia del presupuesto",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "financeMedal.png",
                        Titulo = "Ejemplos",
                        Contenido = "Aqui hay un ejemplo",
                        CarouselItem = new RecorridoItemPage()
                    }
                };
            }
            else
            {
                Introduccion = new ObservableCollection<IntroduccionModel>
                {
                    new IntroduccionModel()
                    {
                        DirImagen = "moneydef.png",
                        Titulo = "Modulo de Plan de Gastos",
                        Contenido = "Esta es la corta definición de Ahorro",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "safeIn.png",
                        Titulo = "Importancia",
                        Contenido = "Esta es la corta descripcion de la importancia del presupuesto",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "bankBuilding.png",
                        Titulo = "Ejemplos",
                        Contenido = "Aqui hay un ejemplo",
                        CarouselItem = new RecorridoItemPage()
                    }
                };

            }
                                 
            
            foreach(var intro in Introduccion)
            {
                intro.CarouselItem.BindingContext = intro;

            }

            ComandoSiguiente = new Command(Siguiente);
            ComandoOmitir = new Command(async () => await Omitir(), () => true);
        }

        private void Siguiente(object obj)
        {
            if (PosicionIndice < Introduccion.Count - 1)
            {
                PosicionIndice++;
            }
            else
            {
                Omitir();
            }
        }

        private async Task  Omitir(){
            if(IDModulo == "1")
            {
                await Shell.Current.GoToAsync(nameof(Presupuesto));
            }
            else if(IDModulo == "2")
            {
                await Shell.Current.GoToAsync(nameof(PlanGastos));
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(Ahorro));
            }            
        }
    }
}
