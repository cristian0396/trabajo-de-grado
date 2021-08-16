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
        CCDrawNode node, node2;
        CCSprite sopa;
        int random, score = 0, contInversion = 0, contAhorro = 0, contFinanzas = 0, contPlata = 0, contEsfuerzo = 0, contMeta = 0;
        bool hasGameEnded;
        ScoreText scoreText;
        public SopaDeLetras(CCGameView gameView) : base(gameView)
        {
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            node = new CCDrawNode();
            node2 = new CCDrawNode();
            random = CreateRandom();
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("SopaBg", true);
            InicializarCapas();
            CrearFondo();
            CreateHud();
            CrearSopa();
            CreateTouchListener();
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

            CCSprite palabras;
            if (random < 1)
            {
                palabras = new CCSprite("palabrasuno.png");
            }
            else
            {
                palabras = new CCSprite("palabrasdos.png");
            }

            palabras.AnchorPoint = new CCPoint(0, 0);
            palabras.IsAntialiased = false;
            palabras.Scale = 0.6f;
            palabras.PositionX = (CapaDeJuego.ContentSize.Width / 5.5f);
            palabras.PositionY = CapaDeJuego.ContentSize.Height / 1.25f;
            CapaDeFondo.AddChild(palabras);
        }
        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = hudLayer.ContentSize.Width - (hudLayer.ContentSize.Width - 20);
            scoreText.PositionY = 40;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);
        }
        private int CreateRandom()
        {
            Random generator = new Random();
            return generator.Next(2);
        }
        private void CrearSopa()
        {
            if (random < 1)
            {
                sopa = new CCSprite("sopauno.png");
            }
            else
            {
                sopa = new CCSprite("sopatres.png");
            }
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
        private void DrawParticle(CCPoint point, int width, int height)
        {
            node.Color = CCColor3B.Red;
            node.DrawRect(new CCRect(point.X, point.Y, width, height)); //dibuja X hacia derecha y Y de abajo hacia arriba           
            CapaDeJuego.AddChild(node);
        }
        private void DrawPoint(CCPoint point, int width, int height)
        {
            node2.Color = CCColor3B.Red;
            node2.DrawRect(new CCRect(point.X + 450, point.Y + 100, width, height));
            node2.RotationX = -49.5f;
            CapaDeJuego.AddChild(node2);
        }
        private void RevisarCondicionDeParada()
        {
            CCAudioEngine.SharedEngine.PlayEffect("good");
            score += 10;
            scoreText.Score = score;
            if (random < 1)
            {
                if (score >= 60)
                {
                    EndGame();
                }
            }
            else
            {
                if (score >= 50)
                {
                    EndGame();
                }
            }
        }
        private void CrearCheck(int count)
        {
            CCSprite correct = new CCSprite("check2.png");
            correct.Scale = 0.5f;
            correct.IsAntialiased = false;
            switch (count)
            {
                case 0:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 6.2f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.225f;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 1:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 1.95f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.1f;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 2:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 6.2f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.1f;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 3:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 1.95f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.225f;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 4:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 6.2f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.16f;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 5:
                    correct.PositionX = CapaDeJuego.ContentSize.Width / 1.95f;
                    correct.PositionY = CapaDeJuego.ContentSize.Height / 1.16f;
                    CapaDeJuego.AddChild(correct);
                    break;
            }
            RevisarCondicionDeParada();
        }
        private void CheckLocation1(CCPoint point)
        {
            // x -> izquierda a derecha va aumentando, Y -> de abajo hacia arriba va aumentando          
            float limIzqInversion = (CapaDeJuego.ContentSize.Width / 7.0f), limDerInversion = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 7.0f), limSuperiorInversion = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 4.5f), limInferiorInversion = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 5.3f);
            float limIzqAhorro = (CapaDeJuego.ContentSize.Width / 7.3f), limDerAhorro = CapaDeJuego.ContentSize.Width / 6.7f, limSuperiorAhorro = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 18.0f), limInferiorAhorro = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.6f);
            float limIzqFinanzas = (CapaDeJuego.ContentSize.Width / 1.6f), limDerFinanzas = CapaDeJuego.ContentSize.Width / 1.4f, limSuperiorFinanzas = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 7.0f), limInferiorFinanzas = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.6f);
            float limIzqPlata = (CapaDeJuego.ContentSize.Width / 2.2f), limDerPlata = CapaDeJuego.ContentSize.Width / 1.9f, limSuperiorPlata = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 18.0f), limInferiorPlata = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 5.8f);
            float limIzqEsfuerzo = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 5.5f), limDerEsfuerzo = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 8.0f), limSuperiorEsfuerzo = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 7.0f), limInferiorEsfuerzo = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.6f);
            float limIzqMeta1 = (CapaDeJuego.ContentSize.Width / 4.6f), limDerMeta1 = CapaDeJuego.ContentSize.Width / 4.0f, limSuperiorMeta1 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 12.5f), limInferiorMeta1 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 20.5f);
            float limIzqMeta2 = (CapaDeJuego.ContentSize.Width / 2.2f), limDerMeta2 = CapaDeJuego.ContentSize.Width / 1.9f, limSuperiorMeta2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 18.0f), limInferiorMeta2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 10.0f);

            if (point.X > limIzqInversion && point.X < limDerInversion && point.Y < limSuperiorInversion && point.Y > limInferiorInversion)
            {
                contInversion += 1;
                switch (contInversion)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqInversion, limInferiorInversion + 14), 70, 4);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqInversion + 70, limInferiorInversion + 14), 70, 4);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqInversion + 140, limInferiorInversion + 14), 80, 4);
                        break;
                    case 4:
                        DrawParticle(new CCPoint(limIzqInversion + 220, limInferiorInversion + 14), 80, 4);
                        CrearCheck(0);
                        break;
                }
            }
            if (point.X > limIzqAhorro && point.X < limDerAhorro && point.Y < limSuperiorAhorro && point.Y > limInferiorAhorro)
            {
                contAhorro += 1;
                switch (contAhorro)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqAhorro + 5, limInferiorAhorro), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqAhorro + 5, limInferiorAhorro + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqAhorro + 5, limInferiorAhorro + 140), 4, 30);
                        CrearCheck(1);
                        break;
                }
            }
            if (point.X > limIzqFinanzas && point.X < limDerFinanzas && point.Y < limSuperiorFinanzas && point.Y > limInferiorFinanzas)
            {
                contFinanzas += 1;
                switch (contFinanzas)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqFinanzas + 16, limInferiorFinanzas), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqFinanzas + 16, limInferiorFinanzas + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqFinanzas + 16, limInferiorFinanzas + 140), 4, 92);
                        CrearCheck(2);
                        break;
                }
            }
            if (point.X > limIzqPlata && point.X < limDerPlata && point.Y < limSuperiorPlata && point.Y > limInferiorPlata)
            {
                contPlata += 1;
                switch (contPlata)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqPlata + 16, limInferiorPlata), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqPlata + 16, limInferiorPlata + 70), 4, 70);
                        CrearCheck(3);
                        break;
                }
            }
            if (point.X > limIzqEsfuerzo && point.X < limDerEsfuerzo && point.Y < limSuperiorEsfuerzo && point.Y > limInferiorEsfuerzo)
            {
                contEsfuerzo += 1;
                switch (contEsfuerzo)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqEsfuerzo + 11, limInferiorEsfuerzo), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqEsfuerzo + 11, limInferiorEsfuerzo + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqEsfuerzo + 11, limInferiorEsfuerzo + 140), 4, 92);
                        CrearCheck(4);
                        break;
                }
            }
            if ((point.X > limIzqMeta1 && point.X < limDerMeta1 && point.Y < limSuperiorMeta1 && point.Y > limInferiorMeta1) || (point.X > limIzqMeta2 && point.X < limDerMeta2 && point.Y < limSuperiorMeta2 && point.Y > limInferiorMeta2))
            {
                contMeta += 1;
                switch (contMeta)
                {
                    case 1:
                        DrawPoint(new CCPoint(limIzqMeta1 + 5, limInferiorMeta2 + 70), 4, 70);
                        break;
                    case 2:
                        DrawPoint(new CCPoint(limIzqMeta1 + 5, limInferiorMeta2 + 140), 4, 90);
                        CrearCheck(5);
                        break;
                }
            }
        }
        private void CheckLocation2(CCPoint point)
        {
            // x -> izquierda a derecha va aumentando, Y -> de abajo hacia arriba va aumentando          
            float limIzqConstancia = (CapaDeJuego.ContentSize.Width / 7.3f), limDerConstancia = CapaDeJuego.ContentSize.Width / 6.7f, limSuperiorConstancia = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 4.0f), limInferiorConstancia = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.3f);
            float limIzqCredito = (CapaDeJuego.ContentSize.Width / 4.6f), limDerCredito = CapaDeJuego.ContentSize.Width / 4.0f, limSuperiorCredito = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 6.5f), limInferiorCredito = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.6f);
            float limIzqPlanear = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 5.5f), limDerPlanear = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 8.0f), limSuperiorPlanear = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 6.0f), limInferiorPlanear = (CapaDeJuego.ContentSize.Height / 2.7f);
            float limIzqDeuda = (CapaDeJuego.ContentSize.Width / 1.4f), limDerDeuda = CapaDeJuego.ContentSize.Width / 1.2f, limSuperiorDeuda = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 7.0f), limInferiorDeuda = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 12.0f);
            float limIzqCompras = (CapaDeJuego.ContentSize.Width / 3.3f), limDerCompras = CapaDeJuego.ContentSize.Width / 2.7f, limSuperiorCompras = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 11.0f), limInferiorCompras = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 18.0f);
            float limIzqCompras2 = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 5.5f), limDerCompras2 = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 8.0f), limSuperiorCompras2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 5.0f), limInferiorCompras2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.6f);
            float limIzqCompras3 = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 2.2f), limDerCompras3 = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width / 2.6f), limSuperiorCompras3 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 15.0f), limInferiorCompras3 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 14.0f);


            if (point.X > limIzqConstancia && point.X < limDerConstancia && point.Y < limSuperiorConstancia && point.Y > limInferiorConstancia)
            {
                contInversion += 1;
                switch (contInversion)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqConstancia + 5, limInferiorConstancia + 14), 4, 90);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqConstancia + 5, limInferiorConstancia + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqConstancia + 5, limInferiorConstancia + 140), 4, 70);
                        break;
                    case 4:
                        DrawParticle(new CCPoint(limIzqConstancia + 5, limInferiorConstancia + 210), 4, 90);
                        CrearCheck(2);
                        break;
                }
            }
            if (point.X > limIzqCredito && point.X < limDerCredito && point.Y < limSuperiorCredito && point.Y > limInferiorCredito)
            {
                contAhorro += 1;
                switch (contAhorro)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqCredito + 5, limInferiorCredito), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqCredito + 5, limInferiorCredito + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqCredito + 5, limInferiorCredito + 140), 4, 60);
                        CrearCheck(0);
                        break;
                }
            }
            if (point.X > limIzqPlanear && point.X < limDerPlanear && point.Y < limSuperiorPlanear && point.Y > limInferiorPlanear)
            {
                contFinanzas += 1;
                switch (contFinanzas)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqPlanear + 10, limInferiorPlanear), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqPlanear + 10, limInferiorPlanear + 70), 4, 70);
                        break;
                    case 3:
                        DrawParticle(new CCPoint(limIzqPlanear + 10, limInferiorPlanear + 140), 4, 70);
                        CrearCheck(1);
                        break;
                }
            }
            if (point.X > limIzqDeuda && point.X < limDerDeuda && point.Y < limSuperiorDeuda && point.Y > limInferiorDeuda)
            {
                contPlata += 1;
                switch (contPlata)
                {
                    case 1:
                        DrawParticle(new CCPoint(limIzqDeuda + 16, limInferiorDeuda), 4, 70);
                        break;
                    case 2:
                        DrawParticle(new CCPoint(limIzqDeuda + 16, limInferiorDeuda + 70), 4, 70);
                        CrearCheck(5);
                        break;
                }
            }
            if ((point.X > limIzqCompras && point.X < limDerCompras && point.Y < limSuperiorCompras && point.Y > limInferiorCompras) || (point.X > limIzqCompras2 && point.X < limDerCompras2 && point.Y < limSuperiorCompras2 && point.Y > limInferiorCompras2) || (point.X > limIzqCompras3 && point.X < limDerCompras3 && point.Y < limSuperiorCompras3 && point.Y > limInferiorCompras3))
            {
                contEsfuerzo += 1;
                switch (contEsfuerzo)
                {
                    case 1:
                        DrawPoint(new CCPoint(limIzqCompras + 11, limInferiorCompras2), 4, 70);
                        break;
                    case 2:
                        DrawPoint(new CCPoint(limIzqCompras + 11, limInferiorCompras2 + 70), 4, 70);
                        break;
                    case 3:
                        DrawPoint(new CCPoint(limIzqCompras + 11, limInferiorCompras2 + 140), 4, 170);
                        CrearCheck(4);
                        break;
                }
            }
        }
        private void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            var locationOnScreen = touches[0].Location;
            if (random < 1)
            {
                CheckLocation1(locationOnScreen);
            }
            else
            {
                CheckLocation2(locationOnScreen);
            }

        }
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (hasGameEnded)
            {
                var newScene = new SopaDeLetras(GameView);
                GameView.Director.ReplaceScene(newScene);
            }
            var locationOnScreen = arg1[0].Location;
            if (random < 1)
            {
                CheckLocation1(locationOnScreen);
            }
            else
            {
                CheckLocation2(locationOnScreen);
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