using CocosSharp;
using Proyecto.Actividades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades
{
    public class Millonario : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego, hudLayer;
        CCGameView GameView;
        bool hasGameEnded, inConfirmation = false;
        ScoreText scoreText;
        int score = 0;
        CCSprite respuesta1, respuesta2, respuesta3, respuesta4, preguntaActual;
        CCPoint currentLocation;
        string currentAnswer = "", currentQuestion = "";
        public Millonario(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px            
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("millonario");
            InicializarCapas();            
            CrearFondo();            
            CrearPregunta("pregunta1.png");
            CreateHud();
            CrearPaletas("respuesta1a.png", "respuesta1b.png", "respuesta1c.png", "respuesta1d.png");
            CreateTouchListener();            
            Schedule(Activity);
        }

        private void GanarPuntos(string sourceEffect)
        {
            CCAudioEngine.SharedEngine.PlayEffect(sourceEffect);
            score += 10;
            scoreText.Score = score;
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
            var background = new CCSprite("fondo_actividad2.png");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.ContentSize = new CCSize(App.Width, App.Height);
            CapaDeFondo.AddChild(background);
        }

        private void CrearPregunta(string dir)
        {
            float x = CapaDeJuego.ContentSize.Width / 2.0f, y = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f);
            preguntaActual = new CCSprite(dir);
            preguntaActual.IsAntialiased = false;
            preguntaActual.Scale = 0.4f;
            preguntaActual.PositionX = x;
            preguntaActual.PositionY = y;
            CapaDeJuego.AddChild(preguntaActual);
        }
        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = hudLayer.ContentSize.Width - (hudLayer.ContentSize.Width - 20);
            scoreText.PositionY = hudLayer.ContentSize.Height - 20;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);
        }

        private void CreateTouchListener()
        { //función que crea un evento para cuando el usuario toque la pantalla
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = HandleTouchesEnded;
            //touchListener.OnTouchesBegan = HandleTouchesBegan;
            CapaDeJuego.AddEventListener(touchListener);
        }

        private void HandleTouchesEnded(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        { //función que se encarga de darle manejo al uso de las fichas en el juego
            // we only care about the first touch:
            currentLocation = touches[0].Location;     
        }
        private void LimpiarEscenario()
        {
            preguntaActual.RemoveFromParent();
            respuesta1.RemoveFromParent();
            respuesta2.RemoveFromParent();
            respuesta3.RemoveFromParent();
            respuesta4.RemoveFromParent();            
        }

        private void SiguienteNivel()
        {
            LimpiarEscenario();            
            switch(currentQuestion)
            {
                case "pregunta2":
                    CrearPregunta("pregunta2.png");
                    CrearPaletas("respuesta2a.png", "respuesta2b.png", "respuesta2c.png", "respuesta2d.png");
                    break;
            }

        }

        private void VerificarCorrectitud(string pregunta, string respuesta)
        {
            var greenColor = new CCColor3B(0, 255, 0);
            switch (pregunta)
            {
                case "pregunta1":
                    if(respuesta == "respuesta4") //opcion D
                    {
                        respuesta4.Color = greenColor;
                        GanarPuntos("correctAnswer");
                        currentQuestion = "pregunta2";
                        SiguienteNivel();
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
            }
        }
        private void ConfirmarEleccion()
        {
            var transparente = new CCColor3B(255, 255, 255);
            if (inConfirmation)
            {
                switch (currentAnswer)
                {
                    case "respuesta2":
                        if (respuesta2.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud("pregunta1", "respuesta2");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if(respuesta1.BoundingBox.ContainsPoint(currentLocation) || respuesta3.BoundingBox.ContainsPoint(currentLocation) || respuesta4.BoundingBox.ContainsPoint(currentLocation)){
                            respuesta2.Color = transparente;
                            inConfirmation = false;
                        }
                        break;
                    case "respuesta1":
                        if (respuesta1.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud("pregunta1", "respuesta1");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if (respuesta2.BoundingBox.ContainsPoint(currentLocation) || respuesta3.BoundingBox.ContainsPoint(currentLocation) || respuesta4.BoundingBox.ContainsPoint(currentLocation))
                        {                            
                            respuesta1.Color = transparente;
                            inConfirmation = false;
                        }
                        break;
                    case "respuesta3":
                        if (respuesta3.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud("pregunta1", "respuesta3");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if (respuesta1.BoundingBox.ContainsPoint(currentLocation) || respuesta2.BoundingBox.ContainsPoint(currentLocation) || respuesta4.BoundingBox.ContainsPoint(currentLocation))
                        {                            
                            respuesta3.Color = transparente;
                            inConfirmation = false;
                        }
                        break;
                    case "respuesta4":
                        if (respuesta4.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud("pregunta1", "respuesta4");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if(respuesta1.BoundingBox.ContainsPoint(currentLocation) || respuesta2.BoundingBox.ContainsPoint(currentLocation) || respuesta3.BoundingBox.ContainsPoint(currentLocation))
                        {
                            respuesta4.Color = transparente;
                            inConfirmation = false;
                        }
                        break;
                }                
            }         
        }
        private void SeleccionarRespuesta()
        {
            var orangeColor = new CCColor3B(255, 119, 0);
            if (!inConfirmation)
            {
                if (respuesta2.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta2.Color = orangeColor;
                    currentAnswer = "respuesta2";                    
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                }
                if (respuesta1.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta1.Color = orangeColor;
                    currentAnswer = "respuesta1";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                }
                if (respuesta3.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta3.Color = orangeColor;
                    currentAnswer = "respuesta3";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                }
                if (respuesta4.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta4.Color = orangeColor;
                    currentAnswer = "respuesta4";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                }                
            }                     
        }
        private void Activity(float frameTimeInSeconds)
        { //función que controla toda la actividad en el juego, se ejecuta por medio de Schedule()
            if (hasGameEnded == false)
            {
                SeleccionarRespuesta();
                ConfirmarEleccion();
            }
        }

        private void CrearPaletas(string a, string b, string c, string d)
        {
            float widthCenter = (CapaDeJuego.ContentSize.Width / 2.0f);
            respuesta2 = new CCSprite(a); //primera ficha más arriba
            respuesta2.IsAntialiased = false;
            respuesta2.Scale = 0.9f;
            respuesta2.PositionX = widthCenter;
            respuesta2.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f);
            CapaDeJuego.AddChild(respuesta2);

            respuesta1 = new CCSprite(b); 
            respuesta1.IsAntialiased = false;
            respuesta1.Scale = 0.9f;
            respuesta1.PositionX = widthCenter;
            respuesta1.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f);
            CapaDeJuego.AddChild(respuesta1);

            respuesta3 = new CCSprite(c); 
            respuesta3.IsAntialiased = false;
            respuesta3.Scale = 0.9f;
            respuesta3.PositionX = widthCenter;
            respuesta3.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 11.0f);
            CapaDeJuego.AddChild(respuesta3);

            respuesta4 = new CCSprite(d); //ultima ficha más abajo
            respuesta4.IsAntialiased = false;
            respuesta4.Scale = 0.9f;
            respuesta4.PositionX = widthCenter;
            respuesta4.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 3.0f);
            CapaDeJuego.AddChild(respuesta4);
        }

        private void EndGame()
        {
            hasGameEnded = true;

            // dim the background:
            var drawNode = new CCDrawNode();
            drawNode.DrawRect(
                new CCRect(0, 0, 2000, 2000),
                new CCColor4B(0, 0, 0, 160));
            hudLayer.Children.Add(drawNode);

            var endGameLabel = new CCLabel("Game Over\nFinal Score:" + score,
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
