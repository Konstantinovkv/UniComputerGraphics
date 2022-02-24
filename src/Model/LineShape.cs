using System;
using System.Drawing;

namespace Draw
{
	public class LineShape : Shape
	{
		#region Constructor

		public LineShape(RectangleF line) : base(line)
		{
		}

		public LineShape(LineShape lineS) : base(lineS)
		{
		}

		#endregion

		public override bool Contains(PointF point)
		{
			Point start = new Point((int)this.Location.X, (int)this.Location.Y);
			Point end = Point.Add(start, new Size(250, 0));
			if ((point.X < start.X && point.X < end.X) || (point.X > start.X && point.X > end.X) ||
				(point.Y < start.Y && point.Y < end.Y) || (point.Y > start.Y && point.Y > end.Y))
				return false;
			float dy = end.Y - start.Y;
			float dx = end.X - start.X;
			float Z = dy * point.X - dx * point.Y + start.Y * end.X - start.X * end.Y;
			float N = dy * dy + dx * dx;
			float dist = (float)(Math.Abs(Z) / Math.Sqrt(N));
			return dist < 250 / 2f;
		}

		public override void DrawSelf(Graphics grfx)
		{
			Point point1 = new Point((int)this.Location.X, (int)this.Location.Y);
			Point point2 = Point.Add(point1, new Size(250, 0));
			using (Pen pen = new Pen(StrokeColor, StrokeWidth))
				grfx.DrawLine(pen, point1, point2);
		}
	}
}
