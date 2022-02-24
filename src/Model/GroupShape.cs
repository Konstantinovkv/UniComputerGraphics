using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът група комбинира набор от селектирани форми.
	/// </summary>
	public class GroupShape : Shape
	{
		#region Constructor

		public GroupShape() : base()
		{
		}

		public GroupShape(RectangleF group) : base(group)
		{
		}

		public GroupShape(GroupShape groupS) : base(groupS)
		{
		}

		#endregion

		


		public override bool Contains(PointF point)
		{
			foreach (Shape item in SubShape)
			{
				if (item.Contains(point))
				{
					return true;
				}
			}
			return false;
		}

		
		public override void DrawSelf(Graphics grfx)
		{
			foreach (Shape item in SubShape)
			{
				item.DrawSelf(grfx);
			}
		}
	}
}
