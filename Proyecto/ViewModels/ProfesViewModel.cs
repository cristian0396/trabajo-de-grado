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
    public class ProfesViewModel : ViewModelBase
    {
        public List<string> Sources { get; set; }
        public List<int> Alturas { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public string Opcion { get; set; }

        public ProfesViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
            NewCommand = new Command(async () => await NewPopUp(), () => true);
        }
        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task NewPopUp()
        {
            await PopupNavigation.Instance.PopAsync();
            Profes PopUpView1 = new Profes();
            List<int> Heights = new List<int>();
            List<string> Sources = new List<string>();
            switch (Opcion)
            {
                case "2":
                    Sources.Add("tablero01.png");
                    Sources.Add("juan.png");
                    Heights.Add(270);
                    Heights.Add(270);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "3";
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;

                case "3":
                    Sources.Add("tablero02.png");
                    Sources.Add("juanca.png");
                    Heights.Add(270);
                    Heights.Add(270);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "4";
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "4":
                    Sources.Add("tablero03.png");
                    Sources.Add("juanca1.png");
                    Heights.Add(270);
                    Heights.Add(270);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "5";
                    await PopupNavigation.Instance.PushAsync(PopUpView1);
                    break;
                case "5":
                    Sources.Add("tablero04.png");
                    Sources.Add("juan1.png");
                    Heights.Add(270);
                    Heights.Add(270);
                    ((ProfesViewModel)PopUpView1.BindingContext).Sources = Sources;
                    ((ProfesViewModel)PopUpView1.BindingContext).Alturas = Heights;
                    ((ProfesViewModel)PopUpView1.BindingContext).Opcion = "6";
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
                    await PopupNavigation.Instance.PushAsync(PopUpView);
                    break;
            }
        }
    }
}
