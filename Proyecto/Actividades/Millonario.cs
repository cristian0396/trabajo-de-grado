using CocosSharp;
using Proyecto.Actividades.Entidades;
using Proyecto.ViewModels;
using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Actividades
{
    public class Millonario : CCScene
    {
        CCLayer CapaDeFondo, CapaDeJuego, hudLayer;
        CCGameView GameView;
        CCSprite respuesta1, respuesta2, respuesta3, respuesta4, preguntaActual, ayuda5050, ayudaAmigo, ayudaPublico;
        CCPoint currentLocation;
        ScoreText scoreText;
        bool hasGameEnded, inConfirmation, ayuda5050Usada, ayudaPublicoUsada, ayudaAmigoUsada;        
        int score = 0, contador = 0;
        int[] randoms;
        string currentAnswer, currentQuestion;
        List<string> ListaPreguntas;
        string[][] MatrizRespuestas;
        public Millonario(CCGameView gameView) : base(gameView)
        {
            //Width device: 411px Height: 683px            
            GameView = gameView;
            var contentSearchPaths = new List<string>() { "Images", "Sounds" };
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("millonario", true);            
            randoms = new int[10];
            randoms = CrearRandoms();
            InicializarPreguntasYRespuestas();
            string primeraPregunta = ObtenerPreguntaAzar(randoms[contador]);
            currentQuestion = primeraPregunta;           
            InicializarCapas();            
            CrearFondo();            
            CrearPregunta(primeraPregunta);
            CreateHud();
            CrearPaletas(MatrizRespuestas[randoms[contador]][0], MatrizRespuestas[randoms[contador]][1], MatrizRespuestas[randoms[contador]][2], MatrizRespuestas[randoms[contador]][3]);
            CrearAyudas("5050.png", "votacionPublico.png", "llamadaAmigo.png");
            CreateTouchListener();            
            Schedule(Activity);
        }
        private int[] CrearRandoms()
        {
            int cantidadPreguntas = 10;
            Random generator = new Random();
            int[] magicNumbers = new int[10];
            for (int i=0; i<10; i++)
            {
                magicNumbers[i] = generator.Next(cantidadPreguntas);
                cantidadPreguntas--;
            }             
            return magicNumbers;
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
        private void InicializarPreguntasYRespuestas()
        {
            ListaPreguntas = new List<string> { "pregunta1.png", "pregunta2.png", "pregunta3.png", "pregunta4.png", "pregunta5.png", "pregunta6.png", "pregunta7.png", "pregunta8.png", "pregunta9.png", "pregunta10.png" };
            MatrizRespuestas = new string[][] { new string []{ "respuesta1a.png", "respuesta1b.png", "respuesta1c.png", "respuesta1d.png" },
            new string []{ "respuesta2a.png", "respuesta2b.png", "respuesta2c.png", "respuesta2d.png" }, new string []{ "respuesta3a.png", "respuesta3b.png", "respuesta3c.png", "respuesta3d.png" },
            new string []{ "respuesta4a.png", "respuesta4b.png", "respuesta4c.png", "respuesta4d.png" },new string []{ "respuesta5a.png", "respuesta5b.png", "respuesta5c.png", "respuesta5d.png" },
            new string []{ "respuesta6a.png", "respuesta6b.png", "respuesta6c.png", "respuesta6d.png" },new string []{ "respuesta7a.png", "respuesta7b.png", "respuesta7c.png", "respuesta7d.png" },
            new string []{ "respuesta8a.png", "respuesta8b.png", "respuesta8c.png", "respuesta8d.png" },new string []{ "respuesta9a.png", "respuesta9b.png", "respuesta9c.png", "respuesta9d.png" },
            new string []{ "respuesta10a.png", "respuesta10b.png", "respuesta10c.png", "respuesta10d.png" }};
        }
        private string ObtenerPreguntaAzar(int magic)
        {
            string pregunta = ListaPreguntas[magic];
            ListaPreguntas.RemoveAt(magic);
            return pregunta;
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
        private void CrearAyudas(string srcAyuda5050, string srcAyudaPublico, string srcAyudaAmigo)
        {
            float height = (CapaDeJuego.ContentSize.Height / 2.0f) - (CapaDeJuego.ContentSize.Height / 2.6f);
            float xPosition5050 = (CapaDeJuego.ContentSize.Width / 2.0f) - (CapaDeJuego.ContentSize.Width / 3.0f);
            float xPositionPublic = (CapaDeJuego.ContentSize.Width / 2.0f);
            float xPositionFriend = (CapaDeJuego.ContentSize.Width / 2.0f) + (CapaDeJuego.ContentSize.Width / 3.0f);

            ayuda5050 = new CCSprite(srcAyuda5050);
            ayuda5050.IsAntialiased = false;
            ayuda5050.Scale = 0.75f;
            ayuda5050.PositionX = xPosition5050;
            ayuda5050.PositionY = height;
            CapaDeJuego.AddChild(ayuda5050);

            ayudaPublico = new CCSprite(srcAyudaPublico);
            ayudaPublico.IsAntialiased = false;
            ayudaPublico.Scale = 0.75f;
            ayudaPublico.PositionX = xPositionPublic;
            ayudaPublico.PositionY = height;
            CapaDeJuego.AddChild(ayudaPublico);

            ayudaAmigo = new CCSprite(srcAyudaAmigo);
            ayudaAmigo.IsAntialiased = false;
            ayudaAmigo.Scale = 0.75f;
            ayudaAmigo.PositionX = xPositionFriend;
            ayudaAmigo.PositionY = height;
            CapaDeJuego.AddChild(ayudaAmigo);
        }
        private void CreateTouchListener()
        { //función que crea un evento para cuando el usuario toque la pantalla
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = HandleTouchesEnded;
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            CapaDeJuego.AddEventListener(touchListener);
        }
        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (hasGameEnded)
            {
                var newScene = new Millonario(GameView);
                GameView.Director.ReplaceScene(newScene);
            }
        }
        private void HandleTouchesEnded(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        { //función que se encarga de darle manejo al uso de las fichas en el juego
            // we only care about the first touch:
            currentLocation = touches[0].Location;     
        }
        private void GanarPuntos(string sourceEffect)
        {
            CCAudioEngine.SharedEngine.PlayEffect(sourceEffect);
            score += 10;
            scoreText.Score = score;
        }
        private void LimpiarEscenario()
        {
            preguntaActual.RemoveFromParent();
            respuesta1.RemoveFromParent();
            respuesta2.RemoveFromParent();
            respuesta3.RemoveFromParent();
            respuesta4.RemoveFromParent();
            MatrizRespuestas = MatrizRespuestas.Where((elemento, i) => i != randoms[contador]).ToArray();
            contador++;
        }
        private void CrearCheck()
        {
            float y = CapaDeJuego.ContentSize.Height - 30, x;
            CCSprite correct = new CCSprite("check2.png");
            correct.Scale = 0.5f;
            correct.PositionY = y;
            correct.IsAntialiased = false;
            int count = contador - 1;
            switch (count)
            {
                case 0:
                    x = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width - 275);
                    correct.PositionX = x;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 1:
                    x = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width - 300);
                    correct.PositionX = x;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 2:
                    x = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width - 325);
                    correct.PositionX = x;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 3:
                    x = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width - 350);
                    correct.PositionX = x;
                    CapaDeJuego.AddChild(correct);
                    break;
                case 4:
                    x = CapaDeJuego.ContentSize.Width - (CapaDeJuego.ContentSize.Width - 375);
                    correct.PositionX = x;
                    CapaDeJuego.AddChild(correct);
                    break;
            }          
        }
        private void SiguienteNivel()
        {            
            LimpiarEscenario();
            if (contador > 4)
            {
                EndGame();
            }
            int numeroEscogido = randoms[contador];
            string siguientePregunta = ObtenerPreguntaAzar(numeroEscogido);
            currentQuestion = siguientePregunta;
            inConfirmation = false;
            CrearPregunta(siguientePregunta);
            CrearPaletas(MatrizRespuestas[randoms[contador]][0], MatrizRespuestas[randoms[contador]][1], MatrizRespuestas[randoms[contador]][2], MatrizRespuestas[randoms[contador]][3]);
        }
        private void VerificarCorrectitud(string pregunta, string respuesta)
        {            
            string question = pregunta.Substring(0, pregunta.Length - 4);
            switch (question)
            {
                case "pregunta1":
                    if(respuesta == "respuesta4") //opcion D
                    {
                        GanarPuntos("correctAnswer");                                                
                        SiguienteNivel();                        
                        CrearCheck();
                    } else {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta2":
                    if (respuesta == "respuesta3") //opcion C
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();                        
                        CrearCheck();
                    } else {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta3":
                    if (respuesta == "respuesta2") //opcion B
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();                        
                        CrearCheck();
                    } else {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta4":
                    if (respuesta == "respuesta1") //opcion D
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();                        
                        CrearCheck();
                    } else {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta5":
                    if (respuesta == "respuesta4") //opcion D
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();                        
                        CrearCheck();
                    } else {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta6":
                    if (respuesta == "respuesta2") 
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();
                        CrearCheck();
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta7":
                    if (respuesta == "respuesta4") 
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();
                        CrearCheck();
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta8":
                    if (respuesta == "respuesta3")
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();
                        CrearCheck();
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta9":
                    if (respuesta == "respuesta1")
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();
                        CrearCheck();
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("wrong");
                        EndGame();
                    }
                    break;
                case "pregunta10":
                    if (respuesta == "respuesta4")
                    {
                        GanarPuntos("correctAnswer");
                        SiguienteNivel();
                        CrearCheck();
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
                    case "respuesta1":
                        if (respuesta1.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud(currentQuestion, "respuesta1");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if (respuesta2.BoundingBox.ContainsPoint(currentLocation) || respuesta3.BoundingBox.ContainsPoint(currentLocation) || respuesta4.BoundingBox.ContainsPoint(currentLocation))
                        {
                            respuesta1.Color = transparente;
                            inConfirmation = false;
                        }
                        break;
                    case "respuesta2":
                        if (respuesta2.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud(currentQuestion, "respuesta2");
                            currentLocation.X = 0;
                            currentLocation.Y = 0;
                        }
                        else if(respuesta1.BoundingBox.ContainsPoint(currentLocation) || respuesta3.BoundingBox.ContainsPoint(currentLocation) || respuesta4.BoundingBox.ContainsPoint(currentLocation)){
                            respuesta2.Color = transparente;
                            inConfirmation = false;
                        }
                        break;                    
                    case "respuesta3":
                        if (respuesta3.BoundingBox.ContainsPoint(currentLocation))
                        {
                            VerificarCorrectitud(currentQuestion, "respuesta3");
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
                            VerificarCorrectitud(currentQuestion, "respuesta4");
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
                if (respuesta1.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta1.Color = orangeColor;
                    currentAnswer = "respuesta1";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                    CCAudioEngine.SharedEngine.PlayEffect("seleccionRespuesta");
                }
                if (respuesta2.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta2.Color = orangeColor;
                    currentAnswer = "respuesta2";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                    CCAudioEngine.SharedEngine.PlayEffect("seleccionRespuesta");
                }
                if (respuesta3.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta3.Color = orangeColor;
                    currentAnswer = "respuesta3";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                    CCAudioEngine.SharedEngine.PlayEffect("seleccionRespuesta");
                }
                if (respuesta4.BoundingBox.ContainsPoint(currentLocation))
                {
                    respuesta4.Color = orangeColor;
                    currentAnswer = "respuesta4";
                    currentLocation.X = 0;
                    currentLocation.Y = 0;
                    inConfirmation = true;
                    CCAudioEngine.SharedEngine.PlayEffect("seleccionRespuesta");
                }                
            }                     
        }
        private void EjecutarAyudaPublico()
        {
            var orangeColor = new CCColor3B(255, 119, 0);
            CCAudioEngine.SharedEngine.PlayEffect("seleccionRespuesta");
            string question = currentQuestion.Substring(0, currentQuestion.Length - 4);
            switch (question)
            {
                case "pregunta1":
                    respuesta4.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta4";
                    break;
                case "pregunta2":
                    respuesta3.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta3";
                    break;
                case "pregunta3":
                    respuesta2.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta2";
                    break;
                case "pregunta4":
                    respuesta1.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta1";
                    break;
                case "pregunta5":
                    respuesta4.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta4";
                    break;
                case "pregunta6":
                    respuesta2.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta2";
                    break;
                case "pregunta7":
                    respuesta4.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta4";
                    break;
                case "pregunta8":
                    respuesta3.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta3";
                    break;
                case "pregunta9":
                    respuesta1.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta1";
                    break;
                case "pregunta10":
                    respuesta4.Color = orangeColor;
                    inConfirmation = true;
                    currentAnswer = "respuesta4";
                    break;
            }
        }
        private void EjecutarAyuda5050()
        {
            string question = currentQuestion.Substring(0, currentQuestion.Length - 4);
            switch (question)
            {
                case "pregunta1":
                    respuesta2.RemoveFromParent();
                    respuesta3.RemoveFromParent();
                    break;
                case "pregunta2":
                    respuesta1.RemoveFromParent();
                    respuesta2.RemoveFromParent();
                    break;
                case "pregunta3":
                    respuesta1.RemoveFromParent();
                    respuesta4.RemoveFromParent();
                    break;
                case "pregunta4":
                    respuesta2.RemoveFromParent();
                    respuesta3.RemoveFromParent();
                    break;
                case "pregunta5":
                    respuesta2.RemoveFromParent();
                    respuesta3.RemoveFromParent();
                    break;
                case "pregunta6":
                    respuesta1.RemoveFromParent();
                    respuesta4.RemoveFromParent();
                    break;
                case "pregunta7":
                    respuesta1.RemoveFromParent();
                    respuesta3.RemoveFromParent();
                    break;
                case "pregunta8":
                    respuesta2.RemoveFromParent();
                    respuesta4.RemoveFromParent();
                    break;
                case "pregunta9":
                    respuesta2.RemoveFromParent();
                    break;
                case "pregunta10":
                    respuesta1.RemoveFromParent();
                    respuesta3.RemoveFromParent();
                    break;
            }
        }
        private async Task EjecutarAyudaAmigo()
        {
            Profes PopUpView1 = new Profes("", false); ;
            List<int> Heights = new List<int>() { 240, 260 };
            List<string> Sources = new List<string>() { "mujer8.png" };
            string question = currentQuestion.Substring(0, currentQuestion.Length - 4);
            switch (question)
            {
                case "pregunta1":
                    Sources.Insert(0, "llamada1.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta2":
                    Sources.Insert(0, "llamada2.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta3":
                    Sources.Insert(0, "llamada3.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta4":
                    Sources.Insert(0, "llamada4.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta5":
                    Sources.Insert(0, "llamada5.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta6":
                    Sources.Insert(0, "llamada6.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta7":
                    Sources.Insert(0, "llamada7.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta8":
                    Sources.Insert(0, "llamada8.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta9":
                    Sources.Insert(0, "llamada9.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
                case "pregunta10":
                    Sources.Insert(0, "llamada10.png");
                    ((ProfesViewModel)PopUpView1.BindingContext).InitializeFields(_popUp: ((ProfesViewModel)PopUpView1.BindingContext), sources: Sources, alturas: Heights, deshabilitarOmitir: true, deshabilitarVoz: true, deshabilitarLabelVoz: true);
                    break;
            }            
            await PopupNavigation.Instance.PushAsync(PopUpView1);
        }
        private void EscogerAyuda()
        {
            if (ayuda5050.BoundingBox.ContainsPoint(currentLocation) && !ayuda5050Usada)
            {
                EjecutarAyuda5050();
                ayuda5050.RemoveFromParent();
                ayuda5050Usada = true;
            }
            if (ayudaPublico.BoundingBox.ContainsPoint(currentLocation) && !ayudaPublicoUsada)
            {
                EjecutarAyudaPublico();
                ayudaPublico.RemoveFromParent();
                ayudaPublicoUsada = true;
            }
            if (ayudaAmigo.BoundingBox.ContainsPoint(currentLocation) && !ayudaAmigoUsada)
            {
                EjecutarAyudaAmigo();
                ayudaAmigo.RemoveFromParent();
                ayudaAmigoUsada = true;
            }
        }
        private void Activity(float frameTimeInSeconds)
        { //función que controla toda la actividad en el juego, se ejecuta por medio de Schedule()
            if (hasGameEnded == false)
            {
                SeleccionarRespuesta();
                ConfirmarEleccion();
                EscogerAyuda();
            }
        }
        private void CrearPaletas(string a, string b, string c, string d)
        {
            float widthCenter = (CapaDeJuego.ContentSize.Width / 2.0f);
            respuesta1 = new CCSprite(a); //primera ficha más arriba
            respuesta1.IsAntialiased = false;
            respuesta1.Scale = 0.9f;
            respuesta1.PositionX = widthCenter;
            respuesta1.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f) + (CapaDeJuego.ContentSize.Height / 9.4f);
            CapaDeJuego.AddChild(respuesta1);

            respuesta2 = new CCSprite(b); 
            respuesta2.IsAntialiased = false;
            respuesta2.Scale = 0.9f;
            respuesta2.PositionX = widthCenter;
            respuesta2.PositionY = (CapaDeJuego.ContentSize.Height / 2.0f);
            CapaDeJuego.AddChild(respuesta2);

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
            CCAudioEngine.SharedEngine.StopBackgroundMusic();
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
