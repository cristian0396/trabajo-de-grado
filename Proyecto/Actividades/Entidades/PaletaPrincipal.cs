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

        CCSprite ficha1;
        CCSprite ficha2;
        CCSprite ficha3;
        CCSprite ficha4;
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
            ficha1 = new CCSprite("piezaIzq.png");
            //ficha2 = new CCSprite("piezaIzq.png");
            //ficha3 = new CCSprite("piezaIzq.png");
            //ficha4 = new CCSprite("piezaIzq.png");

            ficha1.IsAntialiased = false;
            //ficha2.IsAntialiased = false;
            //ficha3.IsAntialiased = false;
            //ficha4.IsAntialiased = false;
            // offset it so the monkey's paddle lines up with the collision:
            ficha1.PositionY = -24;
            this.AddChild(ficha1);
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

            // This value adds a slight amount of rotation
            // to the vine. Having a small amount of tilt looks nice.
            
            //float vineAngle = this.Velocity.X / 100.0f;

            //leftVine.Rotation = -this.Rotation + vineAngle;
            //rightVine.Rotation = -this.Rotation + vineAngle;
            
        }

        internal void SetDesiredPositionToCurrentPosition()
        {
            desiredLocation.X = this.PositionX;
            desiredLocation.Y = this.PositionY;
        }
    }
}
