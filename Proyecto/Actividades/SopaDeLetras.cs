using CocosSharp;
using Proyecto.Actividades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades
{
    public class SopaDeLetras : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego, hudLayer;
        CCGameView GameView;
        CCDrawNode node;
        int score = 0;
        bool hasGameEnded;
        ScoreText scoreText;
        public SopaDeLetras(CCGameView gameView) : base(gameView)
        {
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            node = new CCDrawNode();
            //CCAudioEngine.SharedEngine.PlayBackgroundMusic("fondoBouncingBall", true);
            InicializarCapas();
            CrearFondo();
            CreateHud();
            CrearSopa();
            CreateTouchListener();
            //Schedule(Activity);
        }

        private void InicializarCapas()
        {
            CapaDeFondo = new CCLayer();
            this.AddLayer(CapaDeFondo);

            CapaDeJuego = new CCLayer();
            this.AddLayer(CapaDeJuego);

            hudLayer = new CCLayer();
            this.AddLayer(hudLayer);
        }
        private void CrearFondo()
        {
            var background = new CCSprite("fondosopa.jpg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.ContentSize = new CCSize(App.Width, App.Height);
            CapaDeFondo.AddChild(background);
        }       
        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = hudLayer.ContentSize.Width - (hudLayer.ContentSize.Width - 20);
            scoreText.PositionY = 40;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);
        }
        private void CrearSopa()
        {
            var sopa = new CCSprite("sopauno.png");
            sopa.IsAntialiased = false;
            sopa.Scale = 0.7f;
            sopa.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f); 
            sopa.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f);
            CapaDeJuego.AddChild(sopa);
        }
        private void CreateTouchListener()
        { //función que crea un evento para cuando el usuario toque la pantalla
            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesMoved = HandleTouchesMoved,
                OnTouchesBegan = HandleTouchesBegan
            };
            CapaDeJuego.AddEventListener(touchListener);
        }
        private void DrawParticle(CCPoint point)
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
            var position = new CCPoint(point.X / App.Density, App.Height - point.Y / App.Density);
            node.Color = CCColor3B.Red;
            node.Opacity = 1;
            node.DrawRect(new CCRect(position.X - 30, position.Y + 16, 30, 16));            
            CapaDeJuego.AddChild(node);
        }
        private void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            var locationOnScreen = touches[0].LocationOnScreen;
            DrawParticle(locationOnScreen);            
        }
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {   /*
            if (hasGameEnded)
            {
                var newScene = new SopaDeLetras(GameView);
                GameView.Director.ReplaceScene(newScene);
            }
            */
            var locationOnScreen = arg1[0].LocationOnScreen;
            DrawParticle(locationOnScreen);
        }
        private void Activity(float frameTimeInSeconds)
        { //función que controla toda la actividad en el juego, se ejecuta por medio de Schedule()
            if (hasGameEnded == false)
            {
               
            }
        }
        private void EndGame()
        {
            CCAudioEngine.SharedEngine.StopBackgroundMusic();
            hasGameEnded = true;
            var drawNode = new CCDrawNode();
            drawNode.DrawRect(
                new CCRect(0, 0, 2000, 2000),
                new CCColor4B(0, 0, 0, 160));
            hudLayer.Children.Add(drawNode);
            //posicionar score
            var endGameLabel = new CCLabel("Game Over\nFinal Score:" + " " + score,
                "Arial", 40, CCLabelFormat.SystemFont);
            endGameLabel.HorizontalAlignment = CCTextAlignment.Center;
            endGameLabel.Color = CCColor3B.White;
            endGameLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            endGameLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
            endGameLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
            hudLayer.Children.Add(endGameLabel);
        }
    }
}
