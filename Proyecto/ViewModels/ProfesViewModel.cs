using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class ProfesViewModel : ViewModelBase
    {
        public List<string> Sources { get; set; }
        public List<int> Alturas { get; set; }       
        public string Opcion { get; set; }
        public int Actividad { get; set; }
        public string OpcionInicial { get; set; }
        public CancellationTokenSource _speakButtonCancellationTokenSource { get; set; }
        public bool SwitchVoz { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand VozCommand { get; set; }
        public ICommand OmitirCommand { get; set; }
        public bool BotonOmitir { get; set; }
        public bool BotonDeVoz { get; set; }
        public bool LabelVoz { get; set; }
        public ProfesViewModel(string opcionMusica, bool vozEncendida)
        {
            SwitchVoz = vozEncendida;
            OpcionInicial = opcionMusica;
            _speakButtonCancellationTokenSource = new CancellationTokenSource();
            InicializarComandos();
            ActivarTextoAVoz();
        }
        public void InicializarComandos()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
            OmitirCommand = new Command(async () => await OmitirIntro(), () => true);
            VozCommand = new Command(async () => await ActivarTextoAVoz(), () => true);
        }
        private async Task ActivarTextoAVoz()
        {
            string texto = "";
            if (SwitchVoz)
            {
                switch (OpcionInicial)
                {
                    case "1":
                        texto = " Presupuesto" + "Para desarrollar un presupuesto se recomienda que se cumpla con los siguientes consejos: ";                        
                        break;
                    case "2":
                        texto = "Fijar un objetivo concreto o meta a cumplir en un periodo limitado de tiempo y que sea alcanzable para cada persona." +
                            " Tener presente y muy claro el ingreso que recibe. " + "Determinar los costos y gastos u obligaciones fijas que se tienen.";
                        break;
                    case "3":
                        texto = "Priorizar las obligaciones, teniendo en cuenta que las más importantes tienen que ser subsanadas primero. " +
                            "Determinar los gastos variables que se puedan presentar. ";
                        break;
                    case "4":
                        texto = "Los gastos totales no deben superar el 90% de los ingresos recibidos. " + "Escoger un valor o porcentaje el cual cree que se podría ahorrar después de haber descontado todas las obligaciones.";
                        break;
                    case "5":
                        texto = "Es fundamental que los gastos pasados y la deuda personal se tengan en cuenta cuando se crea este tipo de presupuesto, " +
                            "ya que implican una salida de plata. También los ingresos que reciben forman parte fundamental del presupuesto y cualquier " + "ingreso obtenido implica una entrada de plata. ";
                        break;
                    //lección #2 Presupuesto
                    case "6":
                        texto = "Despues de hacer el presupuesto es recomendable mirar y analizar cada una de las variables que se han identificado y hacer los ajustes que crea pertinentes.";
                        break;
                    case "7":
                        texto = "Una vez hayas puesto en marcha las recomendaciones, está disponible un documento guia.";
                        break;
                    case "8":
                        texto = "Futuro Financiero " + " Sé inteligente con la plata. Empieza a cuidarla para que el dia en que la necesites esté ahi y no tengas que pasar por el suplicio" +
                            " de pedir prestado, esto afecta tu futuro financiero. Además, comienza a tomar en serio las inversiones de cualquier tipo. ";
                        break;
                    case "9":
                        texto = "Evita los gastos impulsivos. Una vez que te acostumbras a evitar gastos impulsivos generaras el hábito de la moderación, esto es esencial" +
                            " a la hora de fijar una meta y poder cumplirla sin ningún contratiempo. ";
                        break;
                    //lección #1 Plan de gastos
                    case "10":
                        texto = "El primer paso para potenciar el manejo de los gastos es establecer los gastos prioritarios y seguirlos fielmente, como la rentas, servicios, matrículas, comida, entre otros." +
                            "Estos gastos deben ir reflejados en tu presupuesto mensual como gastos fijos";
                        break;
                    case "11":
                        texto = "Empiece por presupuestar el pago de su salud, pensión y ARL que le permitan gozar de tranquilidad para vivir su dia a dia. Si es empleado con seguridad, tales aportes son los primero" +
                            "que le descuentan cuando le pagan su salario, pero si es independiente póngalos en su lista de prioridades.";
                        break;
                    case "12":
                        texto = "Dé prioridad a los gastos personales que afectan su tranquilidad para sobrevivir y que parten de un compromiso, muchas veces contractual, como el pago del arriendo, pago de la cuota de su vivienda" +
                            "y pago de los servicios públicos. ";
                        break;
                    case "13":
                        texto = "Estime cuál es el costo requerido para sus productos alimenticios y tenga en cuenta que su gasto debe enfocarlo a lo básico en materia alimentaria." +
                            "Si desea comprar productos que no son básicos no pierda de vista que por no ser prioritarios los puede sacar de su canasta cualquier momento sin mayores preocupaciones.";
                        break;
                    case "14":
                        texto = "Y por último te aconsejamos que intentes con probar con productos sustitutos a menor precio, así como comprar en las plazas de mercado. Realice siempre el ejercicio de comparar y no se vaya" +
                            "por la primera alternativa que se le presente.";
                        break;
                }
                await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
            }
        }
        public async Task Close()
        {
            _speakButtonCancellationTokenSource?.Cancel();
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task OmitirIntro()
        {
            await Close();            
            if (Actividad == 1)
            {
                await Shell.Current.GoToAsync("ActividadP1");
                PopUp PopUpView = new PopUp();
                await ((MessageViewModel)PopUpView.BindingContext).ActivarVentanaActividadP1(PopUpView);
            }
            else if(Actividad == 2)
            {
                await Shell.Current.GoToAsync("ActividadP2");
            }
            else if (Actividad == 3)
            {
                await Shell.Current.GoToAsync("ActividadGastos1");
            }
        }
        public void InitializeFields(ProfesViewModel _popUp, List<string> sources = default(List<string>), List<int> alturas = default(List<int>), int actividad = default(int), string opcion = default(string), bool deshabilitarOmitir = default(bool), bool deshabilitarVoz = default(bool), bool deshabilitarLabelVoz = default(bool))
        {
            _popUp.Sources = sources;
            _popUp.Alturas = alturas;
            _popUp.Opcion = opcion;
            _popUp.Actividad = actividad;
            _popUp.BotonOmitir = !deshabilitarOmitir;
            _popUp.BotonDeVoz = !deshabilitarVoz;
            _popUp.LabelVoz = !deshabilitarLabelVoz;
        }
        public async Task NewPopUp()
        {
            await Close();
            Profes PopUpView1;
            List<int> Heights = new List<int>() { 240, 260};
            List<string> Sources = new List<string>();
            string mensaje;
            switch (Opcion)
            {
                case "2":
                    Sources.Add("tablero01.png");
                    Sources.Add("juan.png");
                    PopUpView1 = new Profes("2", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 1, opcion: "3");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "3":
                    Sources.Add("tablero02.png");
                    Sources.Add("juanca.png");
                    PopUpView1 = new Profes("3", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 1, opcion: "4");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "4":
                    Sources.Add("tablero03.png");
                    Sources.Add("juanca1.png");
                    PopUpView1 = new Profes("4", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 1, opcion: "5");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "5":
                    Sources.Add("tablero04.png");
                    Sources.Add("juan1.png");
                    PopUpView1 = new Profes("5", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 1, opcion: "6");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "6":
                    await Shell.Current.GoToAsync("ActividadP1");
                    PopUp PopUpView = new PopUp();
                    await ((MessageViewModel)PopUpView.BindingContext).ActivarVentanaActividadP1(PopUpView);
                    break;
                //casos de la lección #2
                case "7":
                    Sources.Add("tablero11.png");
                    Sources.Add("mono2.png");
                    PopUpView1 = new Profes("7", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 2, opcion: "8");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "8":
                    Sources.Add("tablero12.png");
                    Sources.Add("juan2.png");
                    PopUpView1 = new Profes("8", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 2, opcion: "9");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "9":
                    Sources.Add("tablero13.png");
                    Sources.Add("juan3.png");
                    PopUpView1 = new Profes("9", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 2, opcion: "10");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "10":
                    await Shell.Current.GoToAsync("ActividadP2");
                    break;
                //casos de la lección #1 modulo Plan de gastos
                case "11":
                    Sources.Add("punto2.png");
                    Sources.Add("mujer3.png");
                    PopUpView1 = new Profes("11", SwitchVoz); 
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "12");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "12":
                    Sources.Add("punto3.png");
                    Sources.Add("mujer4.png");
                    PopUpView1 = new Profes("12", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "13");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "13":
                    Sources.Add("punto4.png");
                    Sources.Add("mujer7.png");
                    PopUpView1 = new Profes("13", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "14");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "14":
                    Sources.Add("punto5.png");
                    Sources.Add("mujer8.png");
                    PopUpView1 = new Profes("14", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "15");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "15":
                    await Shell.Current.GoToAsync("ActividadGastos1");
                    break;
                //casos de la lección #2 modulo Plan de gastos
                case "16":
                    Sources.Add("leccion2_punto2.png");
                    Sources.Add("mujer7.png");
                    PopUpView1 = new Profes("11", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "17");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "17":
                    Sources.Add("leccion2_punto3.png");
                    Sources.Add("mujer8.png");
                    PopUpView1 = new Profes("12", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "18");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "18":
                    Sources.Add("leccion2_punto4.png");
                    Sources.Add("mujer4.png");
                    PopUpView1 = new Profes("13", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "19");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "19":
                    Sources.Add("leccion2_punto5.png");
                    Sources.Add("mujer5.png");
                    PopUpView1 = new Profes("14", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "20");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
            }
        }
    }
}
