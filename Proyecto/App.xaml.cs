using Proyecto.Servicios.Navigation;
using Proyecto.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    public partial class App : Application
    {
        #region Properties
        public static int Width { get; set; } //Ancho del dispositivo Android - se genera desde proyecto Android (usado en las actividades)
        public static int Height { get; set; } //Altura del dispositivo Android - se genera desde proyecto Android (usado en las actividades)
        public static int Density { get; set; } //Densidad del dispositivo Android - se genera desde proyecto Android (usado en las actividades)
        
        static NavigationService navigationService;
        #endregion

        #region Getters & Setters
        public static NavigationService NavigationService
        {
            get
            {
                if (navigationService == null)
                {
                    navigationService = new NavigationService();
                }
                return navigationService;
            }
        }
        #endregion Getters & Setters

        public App()
        {
            InitializeComponent();
            //MainPage = new Login();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
