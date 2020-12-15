using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;

namespace Proyecto.Actividades
{
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer _layer;
        CCLayer CapaDeFondo;
        CCLayer CapaDeJuego;
        PaletaPrincipal paleta;

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

            //_layer = new CCLayer();           
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
        /*
        public void DrawParticle()
        {
            
            var explosion = new CCParticleExplosion(CCPoint.Zero)
            {
                TotalParticles = 10,
                StartColor = new CCColor4F(CCColor3B.White),
                EndColor = new CCColor4F(CCColor3B.Black),
                Position = new CCPoint(point.X / App.Density, App.Height - point.Y / App.Density)
            };
            
             var label = new CCLabel("Tap to begin", "Arial", 30, CCLabelFormat.SystemFont);
            label.PositionX = _layer.ContentSize.Width / 2.0f;
            label.PositionY = _layer.ContentSize.Height / 2.0f;
            label.Color = CCColor3B.White;
            _layer.AddChild(label);
        }
        */

        private void CrearPaleta()
        {
            paleta = new PaletaPrincipal();
            paleta.PositionX = CapaDeJuego.ContentSize.Width / 2.0f;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;

            paleta.SetDesiredPositionToCurrentPosition();

            CapaDeJuego.AddChild(paleta);
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
