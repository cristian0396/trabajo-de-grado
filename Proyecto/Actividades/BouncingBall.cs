using CocosSharp;
using Proyecto.Actividades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
// musica de fondo por: https://patrickdearteaga.com
namespace Proyecto.Actividades
{
    public class BouncingBall : CCScene
    {
        CCSprite paddle, ball, martian;        
        CCLayer CapaDeFondo, CapaDeJuego, hudLayer;
        CCGameView GameView;
        int score = 0, lives = 3, level = 0;
        float velocidadX, velocidadY = 200;
        const float gravedad = 140;
        float minXVelocity = -200;
        float maxXVelocity = 200;
        bool hasGameEnded, martianFlag;
        ScoreText scoreText;
        LivesText livesLabel;
        CCLabel levelLabel;
        public BouncingBall(CCGameView gameView) : base(gameView)
        {
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("fondoBouncingBall", true);
            InicializarCapas();
            CrearFondo();
            CreateHud();
            CreateLivesLabel();
            CreateTouchListener();
            IniciarPaleta();
            IniciarBola();
            SubirNivel();
            Schedule(Activity);
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
            var background = new CCSprite("bouncingBg.png");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.ContentSize = new CCSize(App.Width, App.Height);
            CapaDeFondo.AddChild(background);
        }
        private void IniciarPaleta()
        {
            paddle = new CCSprite("plataforma.png");
            paddle.Scale = 0.3f;
            paddle.IsAntialiased = false;
            paddle.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f);
            paddle.PositionY = 100;
            CapaDeJuego.AddChild(paddle);
        }
        private void IniciarBola()
        {
            ball = new CCSprite("bolita.png");
            ball.Scale = 0.1f;
            ball.IsAntialiased = false;
            ball.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f);
            ball.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f);
            CapaDeJuego.AddChild(ball);
        }
        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = hudLayer.ContentSize.Width - (hudLayer.ContentSize.Width - 20);
            scoreText.PositionY = 40;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);
        }
        private void CreateLivesLabel()
        {
            livesLabel = new LivesText();
            livesLabel.PositionX = hudLayer.ContentSize.Width -130;
            livesLabel.PositionY = 40;
            livesLabel.Lives = 3;
            hudLayer.AddChild(livesLabel);
        }
        private void CreateTouchListener()
        { //función que crea un evento para cuando el usuario toque la pantalla
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            CapaDeJuego.AddEventListener(touchListener);
        }
        private void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                var locationOnScreen = touches[0].Location;
                paddle.PositionX = locationOnScreen.X;
            }            
        }
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (hasGameEnded)
            {
                var newScene = new BouncingBall(GameView);
                GameView.Director.ReplaceScene(newScene);
            }
        }
        private void VerificarPelotaAlsuelo()
        {
            // falló el usuario, la pelota cae en el suelo
            if (ball.PositionY < 0)
            {
                lives -= 1;
                livesLabel.Lives = lives;
                if (lives == 0)
                {
                    EndGame();
                }
                else
                {
                    ball.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f);
                    ball.PositionY = CapaDeJuego.ContentSize.Height;
                }
            }
        }
        private void SubirNivel()
        {
            levelLabel = new CCLabel("Nivel: " + level, "Arial", 20, CCLabelFormat.SystemFont);
            levelLabel.Color = CCColor3B.White;
            levelLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
            levelLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
            hudLayer.Children.Add(levelLabel);
            level += 1;
            minXVelocity -= 100;
            maxXVelocity += 100;
            velocidadY += 100;
        }
        private void VerificarColisiones()
        {
            bool ballOverlapsPaddle = ball.BoundingBoxTransformedToParent.IntersectsRect(paddle.BoundingBoxTransformedToParent);
            bool isMovignDownward = velocidadY < 0;
            if (ballOverlapsPaddle && isMovignDownward)
            {
                CCAudioEngine.SharedEngine.PlayEffect("boing");
                velocidadY *= -1;
                velocidadX = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);
                score += 1;
                scoreText.Score = score;
                if (score % 5 == 0)
                {
                    SubirNivel();
                    if (!martianFlag)
                    {
                        CrearMarciano();
                        martianFlag = true;
                    }                    
                } else
                {
                    levelLabel.Visible = false;
                }
            }
        }
        private void VerificarLimitesLaterales()
        {
            float ballRight = ball.BoundingBoxTransformedToParent.MaxX;
            float ballLeft = ball.BoundingBoxTransformedToParent.MinX;
            float screenRight = CapaDeJuego.ContentSize.Width;
            float screenLeft = 0;

            bool shouldReflectXVelocity = (ballRight > screenRight && velocidadX > 0) || (ballLeft < screenLeft && velocidadX < 0);

            if (shouldReflectXVelocity)
            {
                velocidadX *= -1;
            }
        }
        private void VerificarLimiteSuperior()
        {
            float ballTop = ball.BoundingBoxTransformedToParent.MaxY;
            float screenTop = CapaDeJuego.ContentSize.Height;

            if (ballTop > screenTop && velocidadY > 0)
            {
                velocidadY *= -1;
            }
        }        
        private void CrearMarciano()
        {
            float posY = CCRandom.GetRandomFloat(CapaDeJuego.ContentSize.Height / 2.0f, CapaDeJuego.ContentSize.Height - 20);
            float posX = CCRandom.GetRandomFloat(20, CapaDeJuego.ContentSize.Width-20);
            martian = new CCSprite("naveEspacial.png");
            martian.Scale = 0.3f;
            martian.IsAntialiased = false;
            martian.PositionX = posX;
            martian.PositionY = posY;
            CapaDeJuego.AddChild(martian);            
        }
        private void VerificarColisionConMarciano()
        {
            bool ballOverlapsMartian = ball.BoundingBoxTransformedToParent.IntersectsRect(martian.BoundingBoxTransformedToParent);
            if (ballOverlapsMartian)
            {
                martian.RemoveFromParent();
                score += 3;
                scoreText.Score = score;
                martianFlag = false;
            }            
        }
        private void Activity(float frameTimeInSeconds)
        { //función que controla toda la actividad en el juego, se ejecuta por medio de Schedule()
            if (hasGameEnded == false)
            {               
                //movimiento de la pelota
                velocidadY += frameTimeInSeconds * -gravedad;
                ball.PositionX += velocidadX * frameTimeInSeconds;
                ball.PositionY += velocidadY * frameTimeInSeconds;

                VerificarPelotaAlsuelo();
                VerificarColisiones();
                VerificarLimitesLaterales();
                VerificarLimiteSuperior();
                if (martianFlag)
                {
                    VerificarColisionConMarciano();
                }
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
