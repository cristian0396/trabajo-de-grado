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
                        Titulo = "Presupuesto",
                        Contenido = "Es la cantidad de plata calculada para hacer frente a los gastos generales de la vida cotidiana, de un viaje u otros. Es decir que es una estimación anticipada de ingresos y gastos que habrán de producirse en un período determinado.",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "importantPresu.png",
                        Titulo = "Importancia",
                        Contenido = "Para la toma de decisiones, por ejemplo, la puesta en marcha de un negocio o el inicio de un proyecto; permite identificar, determinar y gestionar los recursos que se emplearan para el cumplimiento de metas planeadas, de forma que sea óptimo y eficiente.",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "IdeaPresu.png",
                        Titulo = "Ejemplo",
                        Contenido = "A continuación, un ejemplo de un presupuesto inicial, donde hay un pequeño margen para el ahorro y por otro lado se describen ingresos y gastos, lo que es el ideal en todo presupuesto. ",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "PresupuestoInicial.PNG",
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
                        Titulo = "Gastos",
                        Contenido = "Es la utilización o consumo de un bien o servicio a cambio de una contraprestación, se suele realizar mediante una cantidad saliente de dinero. También se denomina egreso.",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "money.png",
                        Titulo = "Importancia",
                        Contenido = "Es importante conocer los gastos mensuales ya que permite obtener información relevante sobre la rentabilidad y el desempeño de las actividades personales. Además, ayuda también en la planificación  y en la toma de decisiones sobre inversiones futuras.",
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
