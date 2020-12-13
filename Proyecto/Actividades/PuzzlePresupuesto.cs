using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;

namespace Proyecto.Actividades
{
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer _layer;
        CCLayer CapaDeFondo;

        public PuzzlePresupuesto(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px
            var contentSearchPaths = new List<string>() { "Images" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            InicializarCapas();
            CrearFondo();
            _layer = new CCLayer();           
        }

        private void InicializarCapas()
        {
            CapaDeFondo = new CCLayer();
            this.AddLayer(CapaDeFondo);
        }
        private void CrearFondo()
        {
            var background = new CCSprite("greenBG.jpg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            CapaDeFondo.AddChild(background);
        }
        public void DrawParticle()
        {
            /*
            var explosion = new CCParticleExplosion(CCPoint.Zero)
            {
                TotalParticles = 10,
                StartColor = new CCColor4F(CCColor3B.White),
                EndColor = new CCColor4F(CCColor3B.Black),
                Position = new CCPoint(point.X / App.Density, App.Height - point.Y / App.Density)
            };
            */
            var label = new CCLabel("Tap to begin", "Arial", 30, CCLabelFormat.SystemFont);
            label.PositionX = _layer.ContentSize.Width / 2.0f;
            label.PositionY = _layer.ContentSize.Height / 2.0f;
            label.Color = CCColor3B.White;
            _layer.AddChild(label);
        }
    }
}
