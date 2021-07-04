using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
    public class LivesText : CCNode
	{
		CCLabel label;

		CCDrawNode background;

		public int Lives
		{
			set
			{
				label.Text = "Lives: " + value;
			}
		}

		public LivesText()
		{
			background = new CCDrawNode();

			const int width = 115;
			const int height = 27;

			background.DrawRect(new CCRect(-5, -height,
				width, height),
				new CCColor4B(100, 100, 100));

			this.AddChild(background);


			label = new CCLabel("Lives: 9999", "Arial", 20, CCLabelFormat.SystemFont);
			label.AnchorPoint = new CCPoint(0, 1);
			this.AddChild(label);
		}
	}
}
