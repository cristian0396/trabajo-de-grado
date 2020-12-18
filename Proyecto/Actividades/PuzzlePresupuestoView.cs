using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class PuzzlePresupuestoView : ContentView
    {
        PuzzlePresupuesto _scene;

        public PuzzlePresupuestoView()
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
                ccGView.DesignResolution = new CCSizeI(App.Width, App.Height);
                _scene = new PuzzlePresupuesto(ccGView);
                ccGView.RunWithScene(_scene);
            }
        }
    }    
}
