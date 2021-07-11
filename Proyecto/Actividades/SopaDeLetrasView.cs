using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class SopaDeLetrasView : ContentView
    {
        SopaDeLetras _scene;

        public SopaDeLetrasView()
        { 
            var sharpView = new CocosSharpView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ViewCreated = HandleViewCreated
            };

            Content = sharpView;
        }
        private void HandleViewCreated(object sender, EventArgs e)
        {
            var ccGView = sender as CCGameView;

            if (ccGView != null)
            {
                ccGView.DesignResolution = new CCSizeI(App.Width, App.Height); //utilizar el ancho y alto que retorna el dispositivo Android
                _scene = new SopaDeLetras(ccGView); 
                ccGView.RunWithScene(_scene); //ejecutar escena
            }
        }
    }
}
