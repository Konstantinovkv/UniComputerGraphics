using System;
using System.Drawing;

namespace Draw
{


//Форма за изпита
	public class ComplexShape : Shape
	{
		#region Constructor

		public ComplexShape(RectangleF compl) : base(compl)
		{
		}

		public ComplexShape(ComplexShape complex) : base(complex)
		{
		}

		#endregion

		
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				return true;
			else
				return false;
		}

		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawRectangle(new Pen(StrokeColor, StrokeWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			Point point1 = new Point((int)this.Location.X, (int)this.Location.Y + 50);
			Point point2 = Point.Add(point1, new Size(200, 0));
			using (Pen pen = new Pen(StrokeColor, StrokeWidth))
				grfx.DrawLine(pen, point1, point2);

			Point point3 = new Point((int)this.Location.X + 100, (int)this.Location.Y + 50);
			Point point4 = Point.Add(point1, new Size(100, 50));
			using (Pen pen2 = new Pen(StrokeColor, StrokeWidth))
				grfx.DrawLine(pen2, point3, point4);

		}
	}
}
