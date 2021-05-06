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
using Rg.Plugins.Popup.Services;

namespace Proyecto.ViewModels
{
    [AddINotifyPropertyChangedInterface] //Usado por la libreria Foody que permite la propagación de contenido INotifyPropertyChanged
   
    public class IntroPresupuestoViewModel : ViewModelBase
    {
        public ObservableCollection<IntroduccionModel> Introduccion { get; set; }
        public int PosicionIndice { get; set; }
        public string IDModulo { get; set; }
        public PopUpUniversal PopUpView { get; set; }
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
            PopUpView = new PopUpUniversal();
            IDModulo = Modulo;
            EscogerIntroduccion();            
        }

        //Funcion donde se recibe el idModulo y con respecto a el se muestran los diferentes iconos, definiciones y ejemplos respectivos
        private void EscogerIntroduccion()
        {
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
            else if (IDModulo == "2")
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
                        Titulo = "Ejemplo",
                        Contenido = "A continuación, se mostrará un ejemplo real de los gastos generales de un colombiano promedio según el Dane en el 2015",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "gastoEnColombia.jpg",
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
                        Titulo = "Ahorro",
                        Contenido = "Ahorrar es una forma inteligente de guardar dinero (lograr un patrimonio) y alcanzar tus metas. Todos podemos hacerlo, sin importar si ganamos mucho o poco, cualquier aporte sirve al momento de ahorrar",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "safeIn.png",
                        Titulo = "Importancia",
                        Contenido = "Es importante ahorrar en el presente para afrontar las necesidades o imprevistos de un futuro financiero incierto, estar preparados para cualquier emergencia o gastos inesperados, el ahorro también puede invertirse para poder cumplir tus metas a futuro. ",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "bankBuilding.png",
                        Titulo = "Ejemplos",
                        Contenido = "A continuación, encontrarás un ejemplo de ahorro personal orientado a obtener un bien que dependiendo de cada persona puede ser por lujo o por necesidad.",
                        CarouselItem = new RecorridoItemPage()
                    },
                    new IntroduccionModel()
                    {
                        DirImagen = "tablahorro.png",
                        CarouselItem = new RecorridoItemPage()
                    }
                };
            }
            foreach (var intro in Introduccion)
            {
                intro.CarouselItem.BindingContext = intro;
            }
            ComandoSiguiente = new Command(Siguiente);
            ComandoOmitir = new Command(async () => await Omitir(), () => true);
        }
        private void MostrarFlechas()
        {
            List<int> Heights;
            string message = "";
            switch (IDModulo)
            {
                case "1":
                    Heights = new List<int>() { 135, 220 };
                    message = "Este es el Total Ingresos, aquí encontraras todos los ingresos recibidos" + " y se calcula sumando todos los aportes en este caso: tu sueldo y el aporte familiar.";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 90, escala: 1, source: "flecha1.png", alturas: Heights, opcion: "3", gridColumn: 1, message: message);
                    break;
                case "2":
                    Heights = new List<int>() { 230, 220 };
                    message = "La vivienda es por lo general, el gasto fijo mensual más alto, y uno muy relevante por lo que es necesarios colocarlo en la lista de prioridades";
                    ((PopUpViewModel)PopUpView.BindingContext).InitializeFields(_popUp: ((PopUpViewModel)PopUpView.BindingContext), rotacion: 90, escala: 0.7f, source: "flecha1.png", alturas: Heights, opcion: "7", gridColumn: 1, message: message);
                    break;
            }
            PopupNavigation.Instance.PushAsync(PopUpView);
        }
        private void Siguiente(object obj) //Función que permite actualizar el indice en que se esta en el carrouselView, en caso de que se este en la ultima se cambia la función del boton a "hecho"
        {
            if (PosicionIndice < Introduccion.Count - 1)
            {
                PosicionIndice++;
                if (PosicionIndice == Introduccion.Count - 1)
                {
                    MostrarFlechas();                   
                }                
            }
            else
            {
                Omitir();
            }
        }

        private async Task  Omitir(){ //Función que dirije al usuario al respectivo modulo
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
