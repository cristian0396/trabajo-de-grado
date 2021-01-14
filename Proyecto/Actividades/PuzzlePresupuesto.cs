﻿using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Proyecto.Actividades.Entidades;
using Proyecto.Actividades.Escenas;

namespace Proyecto.Actividades
{ //Clase principal que abstrae el juego de puzzle presupuesto
    public class PuzzlePresupuesto : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego;
        CCGameView GameView;
        PaletaPrincipal paleta;
        CCSprite paleta5, paleta6, paleta7, paleta8, paleta9, paleta10, correct;        
        private int ControlPaletas; //variable usada para saber que ficha es la que el usuario esta manejando actualmente
        int score = 0;
        private bool hasGameEnded;
        ScoreText scoreText;
        CCLayer hudLayer;

        public PuzzlePresupuesto(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            ControlPaletas = 0;
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
            paleta = new PaletaPrincipal();
            paleta.crearFichas("r1.png");
            paleta.PositionX = 300;
            paleta.PositionY = CapaDeJuego.ContentSize.Height / 2.0f;
            paleta.SetDesiredPositionToCurrentPosition();
            CapaDeJuego.AddChild(paleta);

            paleta5 = new CCSprite("p6.png"); //tercer ficha izquierda más arriba
            paleta5.Scale = 0.4f;
            paleta5.IsAntialiased = false;
            paleta5.PositionX = 80;
            paleta5.PositionY = 440;
            CapaDeJuego.AddChild(paleta5);

            paleta6 = new CCSprite("p1.png"); //cuarta ficha izquierda más arriba
            paleta6.Scale = 0.4f;
            paleta6.IsAntialiased = false;
            paleta6.PositionX = 80;
            paleta6.PositionY = 320;
            CapaDeJuego.AddChild(paleta6);

            paleta7 = new CCSprite("p3.png");
            paleta7.Scale = 0.4f;
            paleta7.IsAntialiased = false;
            paleta7.PositionX = 80;
            paleta7.PositionY = 200;
            CapaDeJuego.AddChild(paleta7);

            paleta8 = new CCSprite("p2.png"); 
            paleta8.Scale = 0.4f;
            paleta8.IsAntialiased = false;
            paleta8.PositionX = 80;
            paleta8.PositionY = 80;
            CapaDeJuego.AddChild(paleta8);

            paleta9 = new CCSprite("p4.png");  //segunda ficha izquierda más arriba
            paleta9.Scale = 0.4f;
            paleta9.IsAntialiased = false;
            paleta9.PositionX = 80;
            paleta9.PositionY = 560;
            CapaDeJuego.AddChild(paleta9);

            //paleta10 = new CCSprite("p5.png");        //ficha izquierda más arriba
            //paleta10.Scale = 0.4f;
            //paleta10.IsAntialiased = false;
            //paleta10.PositionX = 100;
            //paleta10.PositionY = 620;
            //CapaDeJuego.AddChild(paleta10);
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

        private void Checkear(int x, int y)
        {
            correct = new CCSprite("check.png");
            correct.Scale = 0.07f;
            correct.IsAntialiased = false;
            correct.PositionX = x;
            correct.PositionY = y;
            CapaDeJuego.AddChild(correct);
        }

        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = 20;
            scoreText.PositionY = hudLayer.ContentSize.Height - 20;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);
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

            var endGameLabel = new CCLabel("Game Over\nFinal Score:" + " " + score,
                "Arial", 40, CCLabelFormat.SystemFont);
            endGameLabel.HorizontalAlignment = CCTextAlignment.Center;
            endGameLabel.Color = CCColor3B.White;
            endGameLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            endGameLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
            endGameLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
            hudLayer.Children.Add(endGameLabel);
        }
        private void PiezaVsPieza(PaletaPrincipal pieza)
        { //función que determina unos rangos en que la pieza usada puede entrar para luego generar otra ficha estatica que se une con la ficha concepto            
            
            //if (pieza.PositionX > 100 && pieza.PositionX < 250 && pieza.PositionY < 650 && pieza.PositionY > 590 )
            //{               
            //    if (ControlPaletas == 4)
            //    {
            //        score += 10;
            //        scoreText.Score = score;
            //        pieza.RemoveFromParent();
            //        ubicarPieza("r5.png", 230, 620);
            //        CrearNuevaFicha("r6.png");
            //        Checkear(360, 620);
            //        ControlPaletas++;
            //    }
            //    else
            //    {
            //        pieza.RemoveFromParent();
            //        EscogerNuevaFicha();                   
            //    }                
            //}
            if (pieza.PositionX > 60 && pieza.PositionX < 210 && pieza.PositionY < 560 && pieza.PositionY > 500)
            {
                
                if (ControlPaletas == 3)
                {
                    score += 10;
                    scoreText.Score = score;
                    pieza.RemoveFromParent();
                    ubicarPieza("r4.png", 210, 560);
                    CrearNuevaFicha("r5.png");
                    Checkear(360, 560);
                    ControlPaletas++;
                }
                else
                {
                    if(score > 0)
                    {
                        score -= 5;
                        scoreText.Score = score;
                    }                    
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > 60 && pieza.PositionX < 210 && pieza.PositionY < 450 && pieza.PositionY > 390)
            {
                if (ControlPaletas == 4)
                {
                    score += 10;
                    scoreText.Score = score;
                    pieza.RemoveFromParent();
                    ubicarPieza("r6.png", 210, 440);
                    Checkear(360, 440);
                    ControlPaletas++;
                    EndGame();
                }
                else
                {
                    if (score > 0)
                    {
                        score -= 5;
                        scoreText.Score = score;
                    }
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > 60 && pieza.PositionX < 210 && pieza.PositionY < 340 && pieza.PositionY > 280)
            {
                if (ControlPaletas == 0)
                {
                    score += 10;
                    scoreText.Score = score;
                    pieza.RemoveFromParent();
                    ubicarPieza("r1.png", 210, 320);
                    CrearNuevaFicha("r2.png");
                    Checkear(360, 320);
                    ControlPaletas++;
                }
                else
                {
                    if (score > 0)
                    {
                        score -= 5;
                        scoreText.Score = score;
                    }
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > 60 && pieza.PositionX < 210 && pieza.PositionY < 230 && pieza.PositionY > 170)
            {
                if (ControlPaletas == 2)
                {
                    score += 10;
                    scoreText.Score = score;
                    pieza.RemoveFromParent();
                    ubicarPieza("r3.png", 210, 200);
                    CrearNuevaFicha("r4.png");
                    Checkear(360, 200);
                    ControlPaletas++;
                }
                else
                {
                    if (score > 0)
                    {
                        score -= 5;
                        scoreText.Score = score;
                    }
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
            if (pieza.PositionX > 60 && pieza.PositionX < 210 && pieza.PositionY < 120 && pieza.PositionY > 60)
            {
                if (ControlPaletas == 1)
                {
                    score += 10;
                    scoreText.Score = score;
                    pieza.RemoveFromParent();
                    ubicarPieza("r2.png", 210, 80);
                    CrearNuevaFicha("r3.png");
                    Checkear(360, 80);
                    ControlPaletas++;
                }
                else
                {
                    if (score > 0)
                    {
                        score -= 5;
                        scoreText.Score = score;
                    }
                    pieza.RemoveFromParent();
                    EscogerNuevaFicha();
                }                
            }
        }
    }
}
