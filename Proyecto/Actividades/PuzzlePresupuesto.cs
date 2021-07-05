using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;
using Proyecto.Actividades.Escenas;
using Xamarin.Forms;

namespace Proyecto.Actividades
{ //Clase principal que abstrae el juego de puzzle presupuesto
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego, hudLayer;
        CCGameView GameView;
        PaletaPrincipal paleta;
        CCSprite paleta5, paleta6, paleta7, paleta8, paleta9, correct;        
        int ControlPaletas, score = 0; //variable usada para saber que ficha es la que el usuario esta manejando actualmente
        bool hasGameEnded;
        ScoreText scoreText;

        public PuzzlePresupuesto(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px            
            GameView = gameView;
            ControlPaletas = 0;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("Ambiente", true);            
            InicializarCapas();            
            CrearFondo();
            CrearPaletas();
            CreateTouchListener();
            CreateHud();
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
            var background = new CCSprite("fondopuzzle.jpg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.ContentSize = new CCSize(App.Width, App.Height);
            CapaDeFondo.AddChild(background);
        }

        private void CrearPaletas()
        { //función que inicializa todas las fichas del puzzle en las respectivas ubicaciones
            //ficha movible
            paleta = new PaletaPrincipal();
            paleta.crearFichas("r1.png");
            paleta.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) + (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 2.34f);
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);
            //inicio fichas estaticas
            paleta9 = new CCSprite("p4.png");  //primera ficha izquierda más arriba
            paleta9.Scale = 0.4f;
            paleta9.IsAntialiased = false;
            paleta9.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta9.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f);
            CapaDeJuego.AddChild(paleta9);

            paleta5 = new CCSprite("p6.png"); //segunda ficha izquierda más arriba
            paleta5.Scale = 0.4f;
            paleta5.IsAntialiased = false;
            paleta5.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta5.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f);
            CapaDeJuego.AddChild(paleta5);
            
            paleta6 = new CCSprite("p1.png"); //tercera ficha izquierda más arriba
            paleta6.Scale = 0.4f;
            paleta6.IsAntialiased = false;
            paleta6.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta6.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 9.0f); 
            CapaDeJuego.AddChild(paleta6);
            
            paleta7 = new CCSprite("p3.png"); //cuarta ficha izquierda más arriba
            paleta7.Scale = 0.4f;
            paleta7.IsAntialiased = false;
            paleta7.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta7.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.1f); 
            CapaDeJuego.AddChild(paleta7);
             
            paleta8 = new CCSprite("p2.png");  //quinta ficha izquierda más arriba
            paleta8.Scale = 0.4f;
            paleta8.IsAntialiased = false;
            paleta8.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta8.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.38f);
            CapaDeJuego.AddChild(paleta8);            
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
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            CapaDeJuego.AddEventListener(touchListener);
        }        
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (hasGameEnded)
            {
                var newScene = new PuzzlePresupuesto(GameView);
                GameView.Director.ReplaceScene(newScene);
            }
        }        
        private void CrearNuevaFicha(string dir)
        {
            paleta = new PaletaPrincipal();
            paleta.crearFichas(dir);
            paleta.PositionX = (CapaDeJuego.ContentSize.Width / 2.0f) + (CapaDeJuego.ContentSize.Width / 4.0f);
            paleta.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 2.34f);
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);            
        }
        private void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        { //función que se encarga de darle manejo al uso de las fichas en el juego
            // we only care about the first touch:
            var locationOnScreen = touches[0].Location;
            paleta.HandleInput(locationOnScreen);        
        }
        private void Activity(float frameTimeInSeconds)
        { //función que controla toda la actividad en el juego, se ejecuta por medio de Schedule()
            if (hasGameEnded == false)
            {
                paleta.Activity(frameTimeInSeconds);
                PiezaVsPieza(paleta); //manejar las colisiones del juego
            }
        }
        private void ubicarPieza(string dir, float x, float y)
        { //función que le entra las coordenadas en que la ficha estatica creada debe ubicarse
            CCSprite nuevaPieza = new CCSprite(dir);
            nuevaPieza.Scale = 0.4f;
            nuevaPieza.IsAntialiased = false;
            nuevaPieza.PositionX = x;
            nuevaPieza.PositionY = y;
            CapaDeJuego.AddChild(nuevaPieza);
        }
        private void EscogerNuevaFicha()
        {
            switch (ControlPaletas)
            {
                case 0:
                    CrearNuevaFicha("r1.png");
                    break;
                case 1:
                    CrearNuevaFicha("r2.png");
                    break;
                case 2:
                    CrearNuevaFicha("r3.png");
                    break;
                case 3:
                    CrearNuevaFicha("r4.png");
                    break;
                case 4:
                    CrearNuevaFicha("r6.png");
                    break;                    
            }
        }
        private void Checkear(float x, float y)
        {
            correct = new CCSprite("check.png");
            correct.Scale = 0.07f;
            correct.IsAntialiased = false;
            correct.PositionX = x;
            correct.PositionY = y;
            CapaDeJuego.AddChild(correct);
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
        private void GanarPuntos(string sourceEffect)
        {
            CCAudioEngine.SharedEngine.PlayEffect(sourceEffect);
            score += 10;
            scoreText.Score = score;
            ControlPaletas++;
        }

        private void PerderPuntos(string sourceEffect)
        {
            CCAudioEngine.SharedEngine.PlayEffect(sourceEffect);
            if (score > 0)
            {
                score -= 5;
                scoreText.Score = score;
            }
        }
        private void PiezaVsPieza(PaletaPrincipal pieza)
        { //función que determina unos rangos en que la pieza movible puede entrar para luego generar otra ficha estatica que se une con la ficha concepto   
            float xCentro = (CapaDeJuego.ContentSize.Width / 4.0f); // ubicación centro de las fichas izquierdas
            float xCentroD = (CapaDeJuego.ContentSize.Width / 4.0f) + 240, x = CapaDeJuego.ContentSize.Width / 4.0f + 130, y;
            float limSuperiorF4 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f) + 48, limInferiorF4 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f) - 48;
            float limSuperiorF6 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f) + 48, limInferiorF6 = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f) - 48;
            float limSuperiorF1 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 9.0f) + 48, limInferiorF1 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 9.0f) - 48;
            float limSuperiorF3 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.1f) + 48, limInferiorF3 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.1f) - 48;
            float limSuperiorF2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.38f) + 48, limInferiorF2 = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.38f) - 48;
            
            if (pieza.PositionX > xCentro && pieza.PositionX < xCentro + 130 && pieza.PositionY < limSuperiorF4 && pieza.PositionY > limInferiorF4)
            {                
                if (ControlPaletas == 3)
                {                    
                    y = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f);
                    GanarPuntos("good");                   
                    pieza.RemoveFromParent();
                    ubicarPieza("r4.png", x, y);                    
                    CrearNuevaFicha("r6.png");
                    Checkear(xCentroD, (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 3.54f));                    
                }
                else
                {
                    PerderPuntos("bad");                  
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > xCentro && pieza.PositionX < xCentro + 130 && pieza.PositionY < limSuperiorF6 && pieza.PositionY > limInferiorF6)
            {
                if (ControlPaletas == 4)
                {
                    y = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f);
                    GanarPuntos("good");
                    pieza.RemoveFromParent();
                    ubicarPieza("r6.png", x, y);                    
                    Checkear(xCentroD, (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f));
                    EndGame();
                }
                else
                {
                    PerderPuntos("bad");
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > xCentro && pieza.PositionX < xCentro + 130 && pieza.PositionY < limSuperiorF1 && pieza.PositionY > limInferiorF1)
            {
                if (ControlPaletas == 0)
                {
                    y = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 9.0f);
                    GanarPuntos("good");
                    pieza.RemoveFromParent();
                    ubicarPieza("r1.png", x, y);                    
                    CrearNuevaFicha("r2.png");
                    Checkear(xCentroD, (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Width / 9.0f));
                }
                else
                {
                    PerderPuntos("bad");
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > xCentro && pieza.PositionX < xCentro + 130 && pieza.PositionY < limSuperiorF3 && pieza.PositionY > limInferiorF3)
            {
                if (ControlPaletas == 2)
                {
                    y = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.1f);
                    GanarPuntos("good");
                    pieza.RemoveFromParent();
                    ubicarPieza("r3.png", x, y);                    
                    CrearNuevaFicha("r4.png");
                    Checkear(xCentroD, (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 4.1f));
                }
                else
                {
                    PerderPuntos("bad");
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > xCentro && pieza.PositionX < xCentro + 130 && pieza.PositionY < limSuperiorF2 && pieza.PositionY > limInferiorF2)
            {
                if (ControlPaletas == 1)
                {
                    y = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.38f);
                    GanarPuntos("good");
                    pieza.RemoveFromParent();
                    ubicarPieza("r2.png", x, y);                    
                    CrearNuevaFicha("r3.png");
                    Checkear(xCentroD, (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.38f));                    
                }
                else
                {
                    PerderPuntos("bad");
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
        }
    }
}
