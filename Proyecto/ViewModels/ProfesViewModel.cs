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
                        texto = "Despues de hacer el presupuesto es recomendable analizar cada una de las variables que se han identificado y hacer los ajustes que crea necesarios.";
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
                        texto = "Dé prioridad a los gastos personales que afectan su tranquilidad para mantener bien sus finanzas y que parten de un compromiso, muchas veces contractual, como el pago del arriendo, pago de la cuota de su vivienda" +
                            "y pago de los servicios públicos. ";
                        break;
                    case "13":
                        texto = "Estime cuál es el costo requerido para sus productos alimenticios y tenga en cuenta que su gasto debe enfocarlo a lo básico en materia alimentaria." +
                            "Si desea comprar productos que no son básicos no pierda de vista que por no ser prioritarios los puede sacar de su canasta cualquier momento sin mayores preocupaciones.";
                        break;
                    case "14":
                        texto = "Y por último te aconsejamos que intentes con probar con productos parecidos a menor precio, así como comprar en las plazas de mercado. Realice siempre el ejercicio de comparar y no se vaya" +
                            "por la primera alternativa que se le presente.";
                        break;
                    //lección #2 Plan de gastos
                    case "15":
                        texto = "El flujo de caja nos sirve para determinar la capacidad que tiene, en este caso, la persona de generar plata, y asi poder determinar si la persona tiene la capacidad de hacer frente" +
                            "a sus obligaciones actuales, y, de haber obligaciones proyectadas, poder prever dichas obligaciones.";
                        break;
                    case "16":
                        texto = "Teniendo en cuenta la capacidad de la persona de generar ingresos(plata), en segunda instancia, el flujo de caja nos servirá para tomar decisiones. La decisión de invertir, de endeudarse" +
                            "o de bajar los gastos o cualquier otra, dependiendo de nuestra capacidad comercial o capacidad de generar ingresos(plata)";
                        break;
                    case "17":
                        texto = "El flujo de caja nos permite observar el comportamiento de nuestras finanzas personales, lo cual nos dará la capacidad de formular estrategias y poder tomar decisiones saludables para asegurar un futuro financiero estable.";
                        break;
                    case "18":
                        texto = "El proceso de elaboración de un flujo de caja es sencillo, pero debemos definir para que lo vamos a realizar, así la información nos sea realmente útil, debe ser información fidedigna, real y confiable." +
                            "A continuación los pasos recomendados";
                        break;
                    case "19":
                        texto = "Primero, Definir el periodo a realizar el flujo" +
                            "Segundo, Determinar el saldo inicial" +
                            "Tercero, Determinar todas las salidas o egresos de fondos" +
                            "Cuarto, Determinar todas las salidas o egresos de fondos" +
                            "Quinto, La suma de los ingresos (menos) la suma de los egresos se obtiene el saldo del periodo." +
                            "A continuación los pasos recomendados";
                        break;
                    case "20":
                        texto = "Para tener en cuenta: " +
                            "Si el saldo es positivo, lo tengo en cuenta para el siguiente mes o ciclo." +
                            "Si el saldo es negativo significa que se deben disminuir los gastos o consigo prestamos para ese mes.";
                        break;
                    //lección #1 Ahorro
                    case "21":
                        texto = "El ahorro es una labor que requiere propósito, organización y constancia, estos tres factores son importantes para tener exito al momento de iniciar cualquier tipo de ahorro," +
                            "sin ellos el camino será dificil de recorrer e incierto para algunas personas.";
                        break;
                    case "22":
                        texto = "Propósito" +
                            "Si no tienes un propósito claro, no vas a ahorrar de manera constante o te lo puedes gastar todo en una sola salida de compras, por eso es importante definir un proposito o varios," +
                            "además, establecer los limites de ese proposito y como lo vas a cumplir.";
                        break;
                    case "23":
                        texto = "Organización" +
                            "Ya sea que planees tus ahorros de forma semanal, quincenal o mensual, debes estar consciente de que hablamos de un plan a largo plazo." +
                            "De lo contrario te desmotivas, dejas de ahorrar o te gastas lo poco que has guardado.";
                        break;
                    case "24":
                        texto = "Constancia" +
                            "No se puede parar por ningún motivo, aunque hayas cumplido tu objetivo inicial. Si empiezas ahorrando 10.000, luego puedes ahorrar 15000 y asi nos vamos acostumbrando al hábito" +
                            "de ahorrar cada vez más siempre que sea posible.";
                        break;
                    case "25":
                        texto = "El ahorro no necesariamente tiene que ser para una emergencia. Claro que un propósito totalmente válido puede ser un viaje o cualquier lujo que quieras darte." +
                            "De nada vale trabajar tant duro si no te puedes dar un gusto de vez en cuando.";
                        break;
                    //lección #2 Ahorro
                    case "26":
                        texto = "Al parecer no hay una fórmula mágica pero si hay opciones para ahorrar que se adaptan a las realidades de cada persona:" +
                            "Lo primero que debemos hacer es diferenciar nuestras necesidades de nuestros deseos, algo fundamental es tener la respuesta a estas preguntas:" +
                            "¿Realmente lo necesito?" +
                            "¿Puedo o debo comprarmelo?";
                        break;
                    case "27":
                        texto = "No solo hablamos de ese último iPhone para estar a la moda, sino gastos cotidianos que nos hacen disminuir nuestro capital, o gastos hormiga, como ese café despues del almuerzo" +
                            "en la cafeteria de la oficina: 1000 por 5 dias a la semana por 4 semanas al mes por 12 meses al año es igual a 240.000 (como mínimo)" +
                            "Ojo: no hablamos de prohibirnos cosas, sino de tener claro cuales son nuestras prioridades y tener autocontrol.";
                        break;
                    case "28":
                        texto = "Existen opciones de apoyo que te pueden servir para comenzar a ahorrar, por ejemplo una cuenta de ahorros, pero toca conocer/investigar sobre qué productor" +
                            "bancario es más apropiado para uno: " +
                            "Cuentas de ahorro" +
                            "Plazos fijos a corto plazo" +
                            "Seguros de vida con plan de ahorro";
                        break;
                    case "29":
                        texto = "Pague sus cuentas o ahorro programado a tiempo. Si no pagas tus cuentas antes de la fecha límite, no te engañes, que no estas ahorrando" +
                            "Esto te evita penalidades y moras, además que construyes tus referencias de crédito y evitas reportes en las centrales de riesgo.";
                        break;
                    case "30":
                        texto = "Las tarjetas de crédito, que al parecer mucha gente las ve como un pago adicional del salario y ahí es donde está el error y empieza el efecto de bola de nieve" +
                            "Ahora, las tarjetas de crédito no son malas. Al contrario, este es un producto que bien administrado le puedes sacar provecho y te puede ayudar a construir historial crediticio.";
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
            else if (Actividad == 4)
            {
                await Shell.Current.GoToAsync("ActividadGastos2");
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
                    PopUpView1 = new Profes("16", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 4, opcion: "17");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "17":
                    Sources.Add("leccion2_punto3.png");
                    Sources.Add("mujer8.png");
                    PopUpView1 = new Profes("17", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 4, opcion: "18");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "18":
                    Sources.Add("leccion2_punto4.png");
                    Sources.Add("mujer4.png");
                    PopUpView1 = new Profes("18", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 4, opcion: "19");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "19":
                    Sources.Add("leccion2_punto5.png");
                    Sources.Add("mujer5.png");
                    PopUpView1 = new Profes("19", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 4, opcion: "20");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "20":
                    Sources.Add("leccion2_punto6.png");
                    Sources.Add("mujer1.png");
                    PopUpView1 = new Profes("20", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 4, opcion: "21");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "21":
                    await Shell.Current.GoToAsync("ActividadGastos2");
                    break;
                //casos de la lección #1 modulo Ahorro
                case "22":
                    Sources.Add("punto2ahorro.png");
                    Sources.Add("munecos2.png");
                    PopUpView1 = new Profes("22", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "23");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "23":
                    Sources.Add("punto3ahorro.png");
                    Sources.Add("munecos3.png");
                    PopUpView1 = new Profes("23", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "24");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "24":
                    Sources.Add("punto4ahorro.png");
                    Sources.Add("munecos4.png");
                    PopUpView1 = new Profes("24", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "25");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "25":
                    Sources.Add("punto5ahorro.png");
                    Sources.Add("munecos5.png");
                    PopUpView1 = new Profes("25", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "26");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "26":
                    await Shell.Current.GoToAsync("ActividadGastos1");
                    break;
                //casos de la lección #2 modulo Ahorro
                case "27":
                    Sources.Add("punto7ahorro.png");
                    Sources.Add("munecos7.png");
                    PopUpView1 = new Profes("27", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "28");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "28":
                    Sources.Add("punto8ahorro.png");
                    Sources.Add("munecos8.png");
                    PopUpView1 = new Profes("28", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "29");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "29":
                    Sources.Add("punto9ahorro.png");
                    Sources.Add("munecos9.png");
                    PopUpView1 = new Profes("29", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "30");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "30":
                    Sources.Add("punto10ahorro.png");
                    Sources.Add("munecos10.png");
                    PopUpView1 = new Profes("30", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "31");
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "31":
                    await Shell.Current.GoToAsync("ActividadGastos1");
                    break;
            }
        }
    }
}
