using CocosSharp;
using Proyecto.Actividades.Geometria;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
    public class PaletaPrincipal : CCNode
    {
        const float width = 59;
        const float height = 9;
        const float speedAtMaxRotation = 200;
        const float maxRotation = 50; // in degrees

        CCPoint desiredLocation;
        public CCPoint Velocity;

        CCSprite ficha;
        public Polygon Polygon
        {
            get;
            private set;
        }
        float rotation;
        public new float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                Polygon.Rotation = rotation;
                base.Rotation = rotation;
            }
        }

        public PaletaPrincipal()
        {
            crearFichas();
            CreateCollision();
        }

        private void crearFichas()
        {
            ficha = new CCSprite("pieza.png");
            ficha.Scale = 0.2f;
            ficha.IsAntialiased = false;
            // offset it so the monkey's paddle lines up with the collision:
            ficha.PositionY = -24;
            this.AddChild(ficha);
        }

        public void HandleInput(CCPoint touchPoint)
        {
            desiredLocation = touchPoint;
        }
        private void CreateCollision()
        {
            Polygon = Polygon.CreateRectangle(width, height);
            this.AddChild(Polygon);
        }
        public void Activity(float frameTimeInSeconds)
        {
            // This code will cause the cursor to lag behind the touch point.
            // Increasing this value reduces how far the paddle lags
            // behind the player's finger.
            const float velocityCoefficient = 4;

            // Get the velocity from current location and touch location
            Velocity = (desiredLocation - this.Position) * velocityCoefficient;

            this.Position += Velocity * frameTimeInSeconds;

            float ratio = Velocity.X / speedAtMaxRotation;
            if (ratio > 1) ratio = 1;
            if (ratio < -1) ratio = -1;

            this.Rotation = ratio * maxRotation;            
        }

        internal void SetDesiredPositionToCurrentPosition()
        {
            desiredLocation.X = this.PositionX;
            desiredLocation.Y = this.PositionY;
        }
    }
}
