// * ----------------------------------------------------------------------------
// * Author: Ben Baker
// * Website: headsoft.com.au
// * E-Mail: benbaker@headsoft.com.au
// * Copyright (C) 2015 Headsoft. All Rights Reserved.
// * ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MAMECompiler64
{
	public class TextProgressBar : ProgressBar
	{
		public TextProgressBar()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle clientRect = ClientRectangle;
			Graphics graphics = e.Graphics;

			ProgressBarRenderer.DrawHorizontalBar(graphics, clientRect);

			clientRect.Inflate(-3, -3);

			if (Value > 0)
			{
				Rectangle clipRect = new Rectangle(clientRect.X, clientRect.Y, (int)Math.Round(((float)Value / Maximum) * clientRect.Width), clientRect.Height);
				ProgressBarRenderer.DrawHorizontalChunks(graphics, clipRect);
			}

			using (Font font = new Font(FontFamily.GenericSansSerif, 10))
			{
				SizeF textSize = graphics.MeasureString(Message, font);
				Point location = new Point((int)(Width / 2 - textSize.Width / 2), (int)(Height / 2 - textSize.Height / 2));

				graphics.DrawString(Message, font, Brushes.Black, location);
			}
		}

		public string Message { get; set; }
	}
}
