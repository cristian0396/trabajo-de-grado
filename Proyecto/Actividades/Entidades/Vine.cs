using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
	public class Vine : CCNode
	{
		public Vine()
		{
			const int numberOfVinesToAdd = 20;

			for (int i = 0; i < numberOfVinesToAdd; i++)
			{
				var graphic = new CCSprite("vine.png");
				graphic.AnchorPoint = new CCPoint(.5f, 0);
				graphic.PositionY = i * graphic.ContentSize.Height;
				this.AddChild(graphic);
			}
		}
	}
}
