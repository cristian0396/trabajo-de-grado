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
        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand VozCommand { get; set; }
        public string Opcion { get; set; }
        public int Actividad { get; set; }
        public string OpcionInicial { get; set; }
        public ICommand OmitirCommand { get; set; }
        public CancellationTokenSource _speakButtonCancellationTokenSource { get; set; }
        public bool SwitchVoz { get; set; }
        public ProfesViewModel(string opcionMusica, bool vozEncendida)
        {
            SwitchVoz = vozEncendida;
            OpcionInicial = opcionMusica;
            _speakButtonCancellationTokenSource = new CancellationTokenSource();
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
            OmitirCommand = new Command(async () => await OmitirIntro(), () => true);
            VozCommand = new Command(async () => await ActivarTextoAVoz(), () => true);
            ActivarTextoAVoz();
        }

        private async Task ActivarTextoAVoz()
        {
            string texto;
            if (SwitchVoz)
            {
                switch (OpcionInicial)
                {
                    case "1":
                        texto = " Presupuesto" +
                            "Para desarrollar un presupuesto se recomienda que se cumpla con los siguientes consejos: ";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "2":
                        texto = "Fijar un objetivo concreto o meta a cumplir en un periodo limitado de tiempo y que sea alcanzable para cada persona." +
                            " Tener presente y muy claro el ingreso que recibe. " +
                            "Determinar los costos y gastos u obligaciones fijas que se tienen.";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "3":
                        texto = "Priorizar las obligaciones, teniendo en cuenta que las más importantes tienen que ser subsanadas primero. " +
                            "Determinar los gastos variables que se puedan presentar. ";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "4":
                        texto = "Los gastos totales no deben superar el 90% de los ingresos recibidos. " +
                            "Escoger un valor o porcentaje el cual cree que se podría ahorrar después de haber descontado todas las obligaciones.";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "5":
                        texto = "Es fundamental que los gastos pasados y la deuda personal se tengan en cuenta cuando se crea este tipo de presupuesto, " +
                            "ya que implican una salida de plata. También los ingresos que reciben forman parte fundamental del presupuesto y cualquier " +
                            "ingreso obtenido implica una entrada de plata. ";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "6":
                        texto = "Despues de hacer el presupuesto es recomendable mirar y analizar cada una de las variables que se han identificado y hacer los ajustes que crea pertinentes.";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "7":
                        texto = "Una vez hayas puesto en marcha las recomendaciones, está disponible un documento guia.";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "8":
                        texto = "Futuro Financiero " +
                            " Sé inteligente con la plata. Empieza a cuidarla para que el dia en que la necesites esté ahi y no tengas que pasar por el suplicio" +
                            " de pedir prestado, esto afecta tu futuro financiero. Además, comienza a tomar en serio las inversiones de cualquier tipo. ";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                    case "9":
                        texto = "Evita los gastos impulsivos. Una vez que te acostumbras a evitar gastos impulsivos generaras el hábito de la moderación, esto es esencial" +
                            " a la hora de fijar una meta y poder cumplirla sin ningún contratiempo. ";
                        await TextToSpeech.SpeakAsync(texto, _speakButtonCancellationTokenSource.Token);
                        break;
                }
            }
        }
        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task OmitirIntro()
        {
            await Close();
            _speakButtonCancellationTokenSource?.Cancel();
            if (Actividad == 1)
            {
                await Shell.Current.GoToAsync("ActividadP1");
                PopUp PopUpView = new PopUp();
                ((MessageViewModel)PopUpView.BindingContext).Titulo = "Instrucciones";
                ((MessageViewModel)PopUpView.BindingContext).Message = "1. Relaciona conceptos, encaja la ficha derecha con alguna del lado izquierdo.\n" +
                    "\n" +
                    "2. Si encajas la ficha donde no es, pierdes 5 puntos.\n" +
                    "\n" +
                    "3. Si la encajas correctamente ganas 10 puntos.\n" +
                    "\n" +
                    "4. Cada vez que encajes una pieza, aparece otra en la esquina superior derecha.\n" +
                    "\n" +
                    "5. Los puntos que obtengas, se guardaran automaticamente.\n";
                ((MessageViewModel)PopUpView.BindingContext).ActivarConsejos = false;
                await PopupNavigation.Instance.PushAsync(PopUpView);
            }
            else
            {
                await Shell.Current.GoToAsync("ActividadP2");
            }            
        }

        public async Task NewPopUp()
        {
            await PopupNavigation.Instance.PopAsync();
            _speakButtonCancellationTokenSource?.Cancel();
            Profes PopUpView1;
            List<int> Heights = new List<int>();
            List<string> Sources = new List<string>();
            switch (Opcion)
            {
                case "2":
                    Sources.Add("tablero01.png");
                    Sources.Add("juan.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("2", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "3";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 1;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;

                case "3":
                    Sources.Add("tablero02.png");
                    Sources.Add("juanca.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("3", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "4";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 1;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "4":
                    Sources.Add("tablero03.png");
                    Sources.Add("juanca1.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("4", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "5";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 1;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "5":
                    Sources.Add("tablero04.png");
                    Sources.Add("juan1.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("5", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "6";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 1;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "6":
                    await Shell.Current.GoToAsync("ActividadP1");
                    PopUp PopUpView = new PopUp();
                    ((MessageViewModel)PopUpView.BindingContext).Titulo = "Instrucciones";
                    ((MessageViewModel)PopUpView.BindingContext).Message = "1. Relaciona conceptos, encaja la ficha derecha con alguna del lado izquierdo.\n" +
                        "\n" +
                        "2. Si encajas la ficha donde no es, pierdes 5 puntos.\n" +
                        "\n" +
                        "3. Si la encajas correctamente ganas 10 puntos.\n" +
                        "\n" +
                        "4. Cada vez que encajes una pieza, aparece otra en la esquina superior derecha.\n" +
                        "\n" +
                        "5. Los puntos que obtengas, se guardaran automaticamente.\n";
                    ((MessageViewModel)PopUpView.BindingContext).ActivarConsejos = false;
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
                //casos de la lección #2
                case "7":
                    Sources.Add("tablero11.png");
                    Sources.Add("mono2.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("7", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "8";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 2;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "8":
                    Sources.Add("tablero12.png");
                    Sources.Add("juan2.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("8", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "9";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 2;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "9":
                    Sources.Add("tablero13.png");
                    Sources.Add("juan3.png");
                    Heights.Add(240);
                    Heights.Add(260);
                    PopUpView1 = new Profes("9", SwitchVoz);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "10";
                    ((ProfesViewModel)PopUpView1.BindingContext).Actividad = 2;
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "10":
                    await Shell.Current.GoToAsync("ActividadP2");
                    break;
            }
        }
    }
}
