using Proyecto.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    public partial class App : Application
    {
        #region Properties
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Density { get; set; }
        #endregion

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
