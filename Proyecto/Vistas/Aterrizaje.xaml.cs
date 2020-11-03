using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aterrizaje : MasterDetailPage
    {
        public Aterrizaje()
        {
            InitializeComponent();
            MyMenu();
        }

        public void MyMenu()
        {
            Detail = new NavigationPage(new MainPage());
            List<Menu> menu = new List<Menu>
            {
                new Menu{ Page= new MainPage(),MenuTitle="Mi Perfil",  MenuDetail="Mi perfil",icon="hombre.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Evaluaciones",  MenuDetail="Evaluaciones",icon="evaluacion.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Configuración",  MenuDetail="Configuración",icon="administracion.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Ayuda",  MenuDetail="Ayuda",icon="pregunta.png"}
            };
            ListMenu.ItemsSource = menu;
        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(menu.Page);
            }
        }
        public class Menu
        {
            public string MenuTitle
            {
                get;
                set;
            }
            public string MenuDetail
            {
                get;
                set;
            }

            public ImageSource icon
            {
                get;
                set;
            }

            public Page Page
            {
                get;
                set;
            }
        }
    }
}