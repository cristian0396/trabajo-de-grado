using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Geometria
{
	public class Circle : CCNode
	{
		public float Radius
		{
			get;
			set;
		}

		public Circle()
		{
		}

		public bool IsPointInside(CCPoint point)
		{
			var absolutePosition = this.PositionWorldspace;

			return (point.X - absolutePosition.X) * (point.X - absolutePosition.X) +
				(point.Y - absolutePosition.Y) * (point.Y - absolutePosition.Y) <
				Radius * Radius;
		}
	}
}
