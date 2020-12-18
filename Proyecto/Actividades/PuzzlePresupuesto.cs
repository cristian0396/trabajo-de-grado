using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;

namespace Proyecto.Actividades
{
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego;
        PaletaPrincipal paleta, paleta2, paleta3, paleta4;
        CCSprite paleta5, paleta6, paleta7, paleta8;
        private int ControlPaletas;
        private bool hasGameEnded;

        public PuzzlePresupuesto(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px
            var contentSearchPaths = new List<string>() { "Images" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            ControlPaletas = 0;
            InicializarCapas();
            CrearFondo();
            CrearPaletas();
            CreateTouchListener();
            Schedule(Activity);
        }

        private void InicializarCapas()
        {
            CapaDeFondo = new CCLayer();
            this.AddLayer(CapaDeFondo);

            CapaDeJuego = new CCLayer();
            this.AddLayer(CapaDeJuego);
        }
        private void CrearFondo()
        {
            var background = new CCSprite("greenBG.jpg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.Scale = 1.2f;
            CapaDeFondo.AddChild(background);
        }

        private void CrearPaletas()
        {
            paleta = new PaletaPrincipal();
            paleta.PositionX = 300;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);

            paleta2 = new PaletaPrincipal();
            paleta2.PositionX = 300;
            paleta2.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta2.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta2);

            paleta3 = new PaletaPrincipal();
            paleta3.PositionX = 300;
            paleta3.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta3.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta3);


            paleta4 = new PaletaPrincipal();
            paleta4.PositionX = 300;
            paleta4.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta4.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta4);

            paleta5 = new CCSprite("piezaIzquierda.jpeg");
            paleta5.Scale = 0.25f;
            paleta5.IsAntialiased = false;
            paleta5.PositionX = 100;
            paleta5.PositionY = 400;
            CapaDeJuego.AddChild(paleta5);

            paleta6 = new CCSprite("piezaIzquierda.jpeg");
            paleta6.Scale = 0.25f;
            paleta6.IsAntialiased = false;
            paleta6.PositionX = 100;
            paleta6.PositionY = 290;
            CapaDeJuego.AddChild(paleta6);

            paleta7 = new CCSprite("piezaIzquierda.jpeg");
            paleta7.Scale = 0.25f;
            paleta7.IsAntialiased = false;
            paleta7.PositionX = 100;
            paleta7.PositionY = 180;
            CapaDeJuego.AddChild(paleta7);

            paleta8 = new CCSprite("piezaIzquierda.jpeg");
            paleta8.Scale = 0.25f;
            paleta8.IsAntialiased = false;
            paleta8.PositionX = 100;
            paleta8.PositionY = 70;
            CapaDeJuego.AddChild(paleta8);

        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            //touchListener.OnTouchesBegan = HandleTouchesBegan;
            CapaDeJuego.AddEventListener(touchListener);
        }
        /*
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (hasGameEnded)
            {
                var newScene = new TitleScene(GameController.GameView);
                GameController.GoToScene(newScene);
            }
        }
        */
        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            // we only care about the first touch:
            var locationOnScreen = touches[0].Location;
            switch (ControlPaletas)
            {
                case 0:
                    paleta.HandleInput(locationOnScreen);
                    break;
                case 1:
                    paleta2.HandleInput(locationOnScreen);
                    break;
                case 2:
                    paleta3.HandleInput(locationOnScreen);
                    break;
                case 3:
                    paleta4.HandleInput(locationOnScreen);
                    break;
            }
        }

        private void Activity(float frameTimeInSeconds)
        {
            if (hasGameEnded == false)
            {
                switch (ControlPaletas)
                {
                    case 0:
                        paleta.Activity(frameTimeInSeconds);
                        break;
                    case 1:
                        paleta2.Activity(frameTimeInSeconds);
                        break;
                    case 2:
                        paleta3.Activity(frameTimeInSeconds);
                        break;
                    case 3:
                        paleta4.Activity(frameTimeInSeconds);
                        break;
                }
                EjecutarColision();
            }
        }

        private void EjecutarColision()
        {
            PaletaPrincipal piezaActual;
            switch (ControlPaletas)
            {
                case 0:
                    piezaActual = paleta;
                    break;
                case 1:
                    piezaActual = paleta2;
                    break;
                case 2:
                    piezaActual = paleta3;
                    break;
                case 3:
                    piezaActual = paleta4;
                    break;
                default:
                    piezaActual = paleta;
                    break;
            }
            PiezaVsPieza(piezaActual);
           
        }

        private void ubicarPieza(int x, int y)
        {
            CCSprite nuevaPieza = new CCSprite("piezaIzquierda.jpeg");
            nuevaPieza.Scale = 0.25f;
            nuevaPieza.IsAntialiased = false;
            nuevaPieza.PositionX = x;
            nuevaPieza.PositionY = y;
            CapaDeJuego.AddChild(nuevaPieza);
        }
        private void PiezaVsPieza(PaletaPrincipal pieza)
        {
            if (pieza.PositionX > 100 && pieza.PositionX < 200 && pieza.PositionY < 430 && pieza.PositionY > 370)
            {               
                pieza.RemoveFromParent();
                ubicarPieza(250, 400);
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 200 && pieza.PositionY < 320 && pieza.PositionY > 260)
            {
                pieza.RemoveFromParent();
                ubicarPieza(250, 290);
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 200 && pieza.PositionY < 210 && pieza.PositionY > 150)
            {
                pieza.RemoveFromParent();
                ubicarPieza(250, 180);
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 200 && pieza.PositionY < 100 && pieza.PositionY > 40)
            {
                pieza.RemoveFromParent();
                ubicarPieza(250, 70);
                ControlPaletas++;
            }
        }
    }
}
