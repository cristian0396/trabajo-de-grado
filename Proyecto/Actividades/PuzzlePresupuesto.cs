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

        private bool hasGameEnded;

        public PuzzlePresupuesto(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px
            var contentSearchPaths = new List<string>() { "Images" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            InicializarCapas();
            CrearFondo();
            CrearPaleta();
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
            CapaDeFondo.AddChild(background);
        }
        
        private void CrearPaleta()
        {
            paleta = new PaletaPrincipal();
            paleta.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);

            paleta2 = new PaletaPrincipal();
            paleta2.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta2.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
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

            paleta.HandleInput(locationOnScreen);
        }

        private void Activity(float frameTimeInSeconds)
        {
            if (hasGameEnded == false)
            {
                paleta.Activity(frameTimeInSeconds);
            }
        }
    }
}
