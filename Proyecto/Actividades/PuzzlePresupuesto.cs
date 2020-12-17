using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;

namespace Proyecto.Actividades
{
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer CapaDeFondo;
        CCLayer CapaDeJuego;
        PaletaPrincipal paleta;
        PaletaPrincipal paleta2;
        PaletaPrincipal paleta3;
        PaletaPrincipal paleta4;
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
            var background = new CCSprite("back5.jpeg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.Scale = 1.2f;
            CapaDeFondo.AddChild(background);
        }
                
        private void CrearPaletas()
        {
            paleta = new PaletaPrincipal();
            paleta.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);

            paleta2 = new PaletaPrincipal();
            paleta2.PositionX = 100;
            paleta2.PositionY = 450;
            paleta2.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta2);

            paleta3 = new PaletaPrincipal();
            paleta3.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta3.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta3);


            paleta4 = new PaletaPrincipal();
            paleta4.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta4.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta4);
                       
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
                default:
                    paleta.HandleInput(locationOnScreen);
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
                    default:
                        paleta.Activity(frameTimeInSeconds);
                        break;
                }               
                EjecutarColision();
            }
        }
        
        private void EjecutarColision()
        {
            PaletaPrincipal piezaActual;
            for (int i = 3; i > -1; i--)
            {
                switch(i){
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
        }
        
        private void PiezaVsPieza(PaletaPrincipal pieza)
        {
            var fichaActual = paleta2;
            bool estaDentro = false;
            if(pieza.PositionX > 100 && pieza.PositionX < 200 && pieza.PositionY < 500 && pieza.PositionY > 400)
            {
                estaDentro = true;
                pieza.RemoveFromParent();
            }

            if (estaDentro)
            {
                pieza.PositionX = 200;
                pieza.PositionY = 450;
                paleta.SetDesiredPositionToCurrentPosition();
                ControlPaletas++;
                ControlPaletas++;
            }            
        }        
    }
}
