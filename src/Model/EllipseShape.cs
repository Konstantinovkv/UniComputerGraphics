using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът елипса е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class EllipseShape : Shape
	{
		#region Constructor

		public EllipseShape(RectangleF ell) : base(ell)
		{
		}

		public EllipseShape(EllipseShape ellipse) : base(ellipse)
		{
		}

		#endregion

		
		public override bool Contains(PointF point)
		{
			PointF center = new PointF(
				  this.Location.X + (this.Width / 2),
				  this.Location.Y + (this.Height / 2));

			double _xRadius = this.Width / 2;
			double _yRadius = this.Height / 2;


			if (_xRadius <= 0.0 || _yRadius <= 0.0)
				return false;
			/* This is a more general form of the circle equation
             *
             * X^2/a^2 + Y^2/b^2 <= 1
             */

			PointF normalized = new PointF(point.X - center.X,
										 point.Y - center.Y);

			return ((double)(normalized.X * normalized.X)
					 / (_xRadius * _xRadius)) + ((double)(normalized.Y * normalized.Y) / (_yRadius * _yRadius))
				<= 1.0;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}
