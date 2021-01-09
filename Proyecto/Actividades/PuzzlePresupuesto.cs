using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;

namespace Proyecto.Actividades
{ //Clase principal que abstrae el juego de puzzle presupuesto
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego;
        PaletaPrincipal paleta;
        CCSprite paleta5, paleta6, paleta7, paleta8, paleta9, paleta10;
        private int ControlPaletas; //variable usada para saber que ficha es la que el usuario esta manejando actualmente
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
            var background = new CCSprite("fondopuzzle.jpg");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.ContentSize = new CCSize(App.Width, App.Height);
            CapaDeFondo.AddChild(background);
        }

        private void CrearPaletas()
        { //función que inicializa todas las fichas del puzzle en las respectivas ubicaciones
            paleta = new PaletaPrincipal();
            paleta.crearFichas("r1.png");
            paleta.PositionX = 300;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);

            paleta5 = new CCSprite("p6.png"); //tercer ficha izquierda más arriba
            paleta5.Scale = 0.4f;
            paleta5.IsAntialiased = false;
            paleta5.PositionX = 100;
            paleta5.PositionY = 400;
            CapaDeJuego.AddChild(paleta5);

            paleta6 = new CCSprite("p1.png"); //cuarta ficha izquierda más arriba
            paleta6.Scale = 0.4f;
            paleta6.IsAntialiased = false;
            paleta6.PositionX = 100;
            paleta6.PositionY = 290;
            CapaDeJuego.AddChild(paleta6);

            paleta7 = new CCSprite("p3.png");
            paleta7.Scale = 0.4f;
            paleta7.IsAntialiased = false;
            paleta7.PositionX = 100;
            paleta7.PositionY = 180;
            CapaDeJuego.AddChild(paleta7);

            paleta8 = new CCSprite("p2.png"); 
            paleta8.Scale = 0.4f;
            paleta8.IsAntialiased = false;
            paleta8.PositionX = 100;
            paleta8.PositionY = 70;
            CapaDeJuego.AddChild(paleta8);

            paleta9 = new CCSprite("p4.png");  //segunda ficha izquierda más arriba
            paleta9.Scale = 0.4f;
            paleta9.IsAntialiased = false;
            paleta9.PositionX = 100;
            paleta9.PositionY = 510;
            CapaDeJuego.AddChild(paleta9);

            paleta10 = new CCSprite("p5.png"); //ficha izquierda más arriba
            paleta10.Scale = 0.4f;
            paleta10.IsAntialiased = false;
            paleta10.PositionX = 100;
            paleta10.PositionY = 620;
            CapaDeJuego.AddChild(paleta10);

        }

        private void CreateTouchListener()
        { //función que crea un evento para cuando el usuario toque la pantalla
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

        void CrearNuevaFicha(string dir)
        {
            paleta = new PaletaPrincipal();
            paleta.crearFichas(dir);
            paleta.PositionX = 300;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);            
        }
        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
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
                EjecutarColision();
            }
        }

        private void EjecutarColision()
        { //función que da manejo a las colisiones del juego            
            PiezaVsPieza(paleta);
           
        }

        private void ubicarPieza(string dir, int x, int y)
        { //función que le entra las coordenadas en que la ficha estatica creada debe ubicarse
            CCSprite nuevaPieza = new CCSprite(dir);
            nuevaPieza.Scale = 0.4f;
            nuevaPieza.IsAntialiased = false;
            nuevaPieza.PositionX = x;
            nuevaPieza.PositionY = y;
            CapaDeJuego.AddChild(nuevaPieza);
        }
        private void PiezaVsPieza(PaletaPrincipal pieza)
        { //función que determina unos rangos en que la pieza usada puede entrar para luego generar otra ficha estatica que se une con la ficha concepto
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 650 && pieza.PositionY > 590)
            {
                pieza.RemoveFromParent();
                ubicarPieza("r5.png", 230, 620);
                CrearNuevaFicha("r6.png");
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 540 && pieza.PositionY > 480)
            {
                pieza.RemoveFromParent();
                ubicarPieza("r4.png", 230, 510);
                CrearNuevaFicha("r5.png");
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 430 && pieza.PositionY > 370)
            {               
                pieza.RemoveFromParent();
                ubicarPieza("r6.png", 230, 400);
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 320 && pieza.PositionY > 260)
            {
                pieza.RemoveFromParent();
                ubicarPieza("r1.png", 230, 290);
                CrearNuevaFicha("r2.png");
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 210 && pieza.PositionY > 150)
            {
                pieza.RemoveFromParent();
                ubicarPieza("r3.png", 230, 180);
                CrearNuevaFicha("r4.png");
                ControlPaletas++;
            }
            if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 100 && pieza.PositionY > 40)
            {
                pieza.RemoveFromParent();
                ubicarPieza("r2.png", 230, 70);
                CrearNuevaFicha("r3.png");
                ControlPaletas++;
            }
        }
    }
}
