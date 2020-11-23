using Proyecto.Servicios.Navegacion;
using Proyecto.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    public partial class App : Application
    {
        #region Properties
        static NavegationService navigationService;
        public static MasterDetailPage MasterD { get; set; }

        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Density { get; set; }
        #endregion

        #region Getters & Setters
        public static NavegationService NavigationService
        {
            get
            {
                if (navigationService == null)
                {
                    navigationService = new NavegationService();
                }
                return navigationService;
            }
        }
        #endregion Getters & Setters
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new Aterrizaje());
            //MainPage = new MainPage();
            MainPage = new ActividadP1();
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
