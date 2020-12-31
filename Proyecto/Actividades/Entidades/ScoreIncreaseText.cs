using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
	public class ScoreIncreaseText : CCNode
	{
		CCLabel label;

		public ScoreIncreaseText()
		{
			label = new CCLabel("+1", "Arial", 20, CCLabelFormat.SystemFont);

			this.AddChild(label);
		}
	}
}
