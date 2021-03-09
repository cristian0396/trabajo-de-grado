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
            InicializarComandos();
            PopUpView = new PopUp();
            ActivarPopUps();
        }

        public void InicializarComandos()
        {
            PopUpCommand = new Command(async () => await ActivarPopUps(), () => true);
        }

        public async Task ActivarPopUps()
        {
            List<int> Heights = new List<int>();
            List<string> Sources = new List<string>();
            Profes PopUpView2;
            switch (ActivityOption)
            {
                case "1":
                    Heights.Add(240);
                    Heights.Add(260);
                    Sources.Add("tablero0.png");
                    Sources.Add("juan1.png");
                    PopUpView2 = new Profes("1", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView2.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView2.BindingContext).Opcion = "2";
                    ((ProfesViewModel)PopUpView2.BindingContext).Actividad = 1;
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
                case "2":
                    Heights.Add(240);
                    Heights.Add(260);
                    Sources.Add("tablero10.png");
                    Sources.Add("mono3.png");
                    PopUpView2 = new Profes("6", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView2.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView2.BindingContext).Opcion = "7";
                    ((ProfesViewModel)PopUpView2.BindingContext).Actividad = 2;
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
                case "3":
                    Heights.Add(240);
                    Heights.Add(260);
                    Sources.Add("punto1.png");
                    Sources.Add("mujer1.png");
                    PopUpView2 = new Profes("9", false);
                    ((ProfesViewModel)PopUpView2.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView2.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView2.BindingContext).Opcion = "11";
                    ((ProfesViewModel)PopUpView2.BindingContext).Actividad = 2;
                    await PopupNavigation.Instance.PushAsync(PopUpView2);
                    break;
            }
            
        }
    }
}
