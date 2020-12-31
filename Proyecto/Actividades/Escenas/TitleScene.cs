using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Escenas
{
    public class TitleScene : CCScene
    {
        CCLayer layer;
        CCGameView GameView;

        public TitleScene(CCGameView gameView) : base(gameView)
        {
            GameView = gameView;
            layer = new CCLayer();
            this.AddLayer(layer);

            CreateText();

            CreateTouchListener();

        }

        private void CreateText()
        {
            var label = new CCLabel("Tap to begin", "Arial", 30, CCLabelFormat.SystemFont);
            label.PositionX = layer.ContentSize.Width / 2.0f;
            label.PositionY = layer.ContentSize.Height / 2.0f;
            label.Color = CCColor3B.White;

            layer.AddChild(label);
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            layer.AddEventListener(touchListener);
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            var newScene = new FruitFalls(GameView);
            GameView.Director.ReplaceScene(newScene);
        }
    }
}
