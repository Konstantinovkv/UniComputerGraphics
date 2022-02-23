using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private Shape selection;
		public Shape Selection {
			get { return selection; }
			set { selection = value; }
		}

		/// <summary>
		/// Избрани елементи.
		/// </summary>
		private List<Shape> multipleSelection = new List<Shape>();
		public List<Shape> MultipleSelection
		{
			get { return multipleSelection; }
			set { multipleSelection = value; }
		}

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}

		#endregion

		private Color defaultFillColor = Color.White;
		public Color DefaultFillColor
		{
			get { return defaultFillColor; }
			set { defaultFillColor = value; }
		}
		private Color defaultStrokeColor = Color.Black;

		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = defaultFillColor;
			rect.StrokeColor = defaultStrokeColor;
			rect.StrokeWidth = 1;

			ShapeList.Add(rect);
		}

		/// <summary>
		/// Добавя примитив - елипса на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));
			ellipse.FillColor = defaultFillColor;
			ellipse.StrokeColor = defaultStrokeColor;
			ellipse.StrokeWidth = 1;

			ShapeList.Add(ellipse);
		}

		public void AddNewGroup()
		{
			GroupShape group = new GroupShape();

			foreach (Shape item in MultipleSelection)
			{
				ShapeList.Remove(item);
			}
			group.SubShape = MultipleSelection;
			ShapeList.Add(group);
		}

		/// <summary>
		/// Добавя примитив - елипса на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			LineShape line = new LineShape(new Rectangle(x, y, 100, 200));
			line.FillColor = defaultFillColor;
			line.StrokeWidth = 1;
			line.StrokeColor = defaultStrokeColor;

			ShapeList.Add(line);
		}

		public void AddRandomPoint()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			PointShape point = new PointShape(new Rectangle(x, y, 100, 200));
			point.FillColor = defaultFillColor;
			point.StrokeColor = defaultStrokeColor;
			point.StrokeWidth = 1;

			ShapeList.Add(point);
		}

		private int lastSelection;
		public int LastSelection
		{
			get { return lastSelection; }
			set { lastSelection = value; }
		}
		private int savedSelection;
		public int SavedSelection
		{
			get { return savedSelection; }
			set { savedSelection = value; }
		}
		private bool isSelected = false;

		private bool isMultipleSelection = false;
		public bool IsMultipleSelection
		{
			get { return isMultipleSelection; }
			set { isMultipleSelection = value; }
		}

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for (int i = ShapeList.Count - 1; i >= 0; i--)
			{
				if (ShapeList[i].Contains(point))
				{
					if (ShapeList[i] is GroupShape)
					{
						foreach (Shape item in ShapeList[i].SubShape)
							item.FillColor = Color.Red;
					}
					else
					{
						ShapeList[i].FillColor = Color.Red;
					}
					if (isSelected == true && lastSelection != i && !isMultipleSelection)
					{
						if(lastSelection > ShapeList.Count - 1)
                        {
							lastSelection = savedSelection;
                        }
						if (ShapeList[lastSelection] is GroupShape)
						{
							foreach (Shape item in ShapeList[lastSelection].SubShape)
							{
								if (item.ChangeColor == Color.Empty)
								{
									item.FillColor = defaultFillColor;
								}
								else
								{
									item.FillColor = item.ChangeColor;

								}
							}
						}
						else
						{
							if (ShapeList[lastSelection].ChangeColor == Color.Empty)
							{
								ShapeList[lastSelection].FillColor = defaultFillColor;
							}
							else
							{
								ShapeList[lastSelection].FillColor = ShapeList[lastSelection].ChangeColor;

							}
						}
					}
					isSelected = true;
					lastSelection = i;

					return ShapeList[i];
				}
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (selection is GroupShape)
			{
				foreach (Shape item in selection.SubShape)
					item.Location = new PointF(item.Location.X + p.X - lastLocation.X, item.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
				return;
			}
			if (selection != null) {
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
				return;
			}
			if (multipleSelection.Count != 0)
            {
                foreach (Shape item in MultipleSelection)
                    item.Location = new PointF(item.Location.X + p.X - lastLocation.X, item.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
				return;
			}
        }
	}
}
