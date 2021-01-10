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
        #endregion

        public App()
        {
            InitializeComponent();
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
