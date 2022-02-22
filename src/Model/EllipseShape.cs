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

		public EllipseShape(ElipseF ell) : base(ell)
		{
		}

		public EllipseShape(ElipseShape ellipse) : base(ellipse)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към елипсата.
		/// В случая на елипса този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащата елипса на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащата елипса.
				// В случая на елипса - директно връщаме true
				return true;
			else
				// Ако не е в обхващащата елипса, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillEllipse(new SolidBrush(FillColor), Ellipse.X, Ellipse.Y, Ellipse.Width, Ellipse.Height);
			grfx.DrawEllipse(Pens.Black, Ellipse.X, Ellipse.Y, Ellipse.Width, Ellipse.Height);

		}
	}
}
