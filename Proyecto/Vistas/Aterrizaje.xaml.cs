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
                new Menu{ Page= new MainPage(),MenuTitle="My Profile",  MenuDetail="Mi perfil",icon="hombre.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Messages",  MenuDetail="Mensajes",icon="message.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Contacts",  MenuDetail="Contactos",icon="contacts.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Settings",  MenuDetail="Configuración",icon="administracion.png"}
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