using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyecto.Actividades
{
    public class ControladorFruit : ContentView
    {
        FruitFalls _scene;

        public ControladorFruit()
        { //Ajustar la vista al contentView e inicializar la clase PuzzlePresupuesto que contiene el juego como tal
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
                _scene = new FruitFalls(ccGView); //inicializar PuzzlePresupuesto
                ccGView.RunWithScene(_scene); //ejecutar escena
            }
        }
    }
}
