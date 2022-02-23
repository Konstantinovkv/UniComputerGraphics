using System;
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
		public virtual Color DefaultFillColor
		{
			get { return defaultFillColor; }
			set { defaultFillColor = value; }
		}

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
			rect.StrokeColor = Color.Green;
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
			ellipse.StrokeColor = Color.Green;
			ellipse.StrokeWidth = 1;

			ShapeList.Add(ellipse);
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
			line.StrokeColor = Color.Black;

			ShapeList.Add(line);
		}

		public void AddRandomPoint()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			PointShape point = new PointShape(new Rectangle(x, y, 100, 200));
			point.FillColor = defaultFillColor;
			point.StrokeWidth = 1;

			ShapeList.Add(point);
		}

		private int lastSelection;
		public virtual int LastSelection
		{
			get { return lastSelection; }
			set { lastSelection = value; }
		}
		private bool isSelected = false;
		public virtual bool IsSelected
		{
			get { return isSelected; }
			set { isSelected = value; }
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
					ShapeList[i].FillColor = Color.Red;
					if (isSelected == true && lastSelection != i)
					{
						if(ShapeList[lastSelection].ChangeColor == null) { 
							ShapeList[lastSelection].FillColor = defaultFillColor;
						}
						else
                        {
							ShapeList[lastSelection].FillColor = ShapeList[lastSelection].ChangeColor;

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
			if (selection != null) {
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
			}
		}
	}
}
