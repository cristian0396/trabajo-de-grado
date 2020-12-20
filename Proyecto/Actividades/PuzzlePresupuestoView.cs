using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyecto.Actividades
{ //Clase que hace de controlador de las escenas del juego actividad1 Puzzle - Division entre escenas y componentes
    //Aqui se hace uso de la libreria CocosSharp que esta basada en el motor de videojuegos Cocos-2d
    public class PuzzlePresupuestoView : ContentView
    {
        PuzzlePresupuesto _scene;

        public PuzzlePresupuestoView()
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
                _scene = new PuzzlePresupuesto(ccGView); //inicializar PuzzlePresupuesto
                ccGView.RunWithScene(_scene); //ejecutar escena
            }
        }
    }    
}
