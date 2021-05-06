using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class FondoLeccionesViewModel : ViewModelBase
    {
        public ICommand PopUpCommand { get; set; }
        public PopUp PopUpView { get; set; }
        public string SourceImg { get; set; }
        public string ActivityOption { get; set; }

        public FondoLeccionesViewModel(string sourceImg, string activityOption) //constructor
        {
            SourceImg = sourceImg;
            ActivityOption = activityOption;
            PopUpView = new PopUp();
            InicializarComandos();            
            ActivarPopUps();
        }

        public void InicializarComandos()
        {
            PopUpCommand = new Command(async () => await ActivarPopUps(), () => true);
        }

        public async Task ActivarPopUps()
        {
            List<int> Heights = new List<int>() { 240,260 };
            List<string> Sources = new List<string>();
            Profes PopUpView2;
            switch (ActivityOption)
            {   //Lecciones modulo presupuesto:
                case "1":                    
                    Sources.Add("tablero0.png");
                    Sources.Add("juan1.png");
                    PopUpView2 = new Profes("1", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView2.BindingContext), sources: Sources, alturas: Heights, actividad: 1, opcion: "2");
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
                case "2":
                    Sources.Add("tablero10.png");
                    Sources.Add("mono3.png");
                    PopUpView2 = new Profes("6", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView2.BindingContext), sources: Sources, alturas: Heights, actividad: 2, opcion: "7");
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
                //Lecciones modulo plan de gastos:
                case "3":
                    Sources.Add("punto1.png");
                    Sources.Add("mujer1.png");
                    PopUpView2 = new Profes("10", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView2.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "11");
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
                case "4":
                    Sources.Add("leccion2_punto1.png");
                    Sources.Add("mujerDosPuntoDos.png");
                    PopUpView2 = new Profes("10", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView2.BindingContext), sources: Sources, alturas: Heights, actividad: 3, opcion: "16");
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
            }            
        }
    }
}
