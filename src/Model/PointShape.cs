using System;
using System.Drawing;

namespace Draw
{
	public class PointShape : Shape
	{
		#region Constructor

		public PointShape(RectangleF point) : base(point)
		{
		}

		public PointShape(PointShape pointS) : base(pointS)
		{
		}

		#endregion

		public override bool Contains(PointF point)
		{
			Point start = new Point((int)this.Location.X, (int)this.Location.Y);
			if ((point.X < start.X) || (point.X > start.X+2) ||
				(point.Y < start.Y) || (point.Y > start.Y+2))
				return false;
			return true;
		}

		public override void DrawSelf(Graphics grfx)
		{
			Brush aBrush = (Brush)Brushes.Black;
			grfx.FillRectangle(aBrush, this.Location.X, this.Location.Y, 3, 3);
		}
	}
}
