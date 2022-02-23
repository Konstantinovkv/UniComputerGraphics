using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
			if (toolStripButton6.Checked)
			{
				if (dialogProcessor.ContainsPoint(e.Location) != null && !dialogProcessor.ContainsPoint(e.Location).Targeted)
				{
					dialogProcessor.MultipleSelection.Add(dialogProcessor.ContainsPoint(e.Location));
				}
				if (dialogProcessor.MultipleSelection.Count != 0 && dialogProcessor.ContainsPoint(e.Location) != null)
					{
						dialogProcessor.ContainsPoint(e.Location).Targeted = true;
						statusBar.Items[0].Text = "Последно действие: Селекция на примитиви";
						dialogProcessor.IsDragging = true;
						dialogProcessor.LastLocation = e.Location;
						viewPort.Invalidate();
					}
				
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void DrawEllipseSpeedButtonClick(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomEllipse();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

        private void DrawLineSpeedButtonClick(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomLine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

			viewPort.Invalidate();
		}

        private void DrawPointSpeedButtonClick(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomPoint();

			statusBar.Items[0].Text = "Последно действие: Рисуване на точка";

			viewPort.Invalidate();
		}

		private void makeGroup(object sender, EventArgs e)
		{
			dialogProcessor.AddNewGroup();

			statusBar.Items[0].Text = "Последно действие: Групиране на елементи.";

			viewPort.Invalidate();
		}

		private void colorDialog(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				if (dialogProcessor.Selection != null)
				{
					try
					{
						if (dialogProcessor.Selection is GroupShape)
						{
							foreach (Shape item in dialogProcessor.Selection.SubShape)
							{
								item.FillColor = colorDialog1.Color;
								item.ChangeColor = colorDialog1.Color;
							}
						}
						else
						{
							dialogProcessor.Selection.FillColor = colorDialog1.Color;
							dialogProcessor.Selection.ChangeColor = colorDialog1.Color;
						}
					}
					catch (NullReferenceException)
					{
						Console.WriteLine("Nothing to fill.");
					}
					viewPort.Invalidate();
				}
            }
        }

        private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

		}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
			if (dialogProcessor.Selection != null)
            {
				try
				{
					dialogProcessor.Selection.StrokeWidth = (int)numericUpDown1.Value;
					viewPort.Invalidate();
				}
				catch (NullReferenceException)
				{
					Console.WriteLine("Nothing to fill.");
				}
			}

		}


		private void strokeColorDialog(object sender, EventArgs e)
        {
			if (colorDialog2.ShowDialog() == DialogResult.OK)
			{
				if (dialogProcessor.Selection != null)
				{
					try
					{
						dialogProcessor.Selection.StrokeColor = colorDialog2.Color;
					}
					catch (NullReferenceException)
					{
						Console.WriteLine("Nothing to fill.");
					}
					viewPort.Invalidate();
				}
			}
		}

		private void toolStripButton6_Click(object sender, EventArgs e)
		{
			dialogProcessor.SavedSelection = dialogProcessor.LastSelection;
			pickUpSpeedButton.Checked = false;
			dialogProcessor.IsMultipleSelection = true;
			if (dialogProcessor.Selection != null)
			{
				if (dialogProcessor.Selection.ChangeColor == Color.Empty)
				{
					dialogProcessor.Selection.FillColor = dialogProcessor.DefaultFillColor;
				}
				else
				{
					dialogProcessor.Selection.FillColor = dialogProcessor.Selection.ChangeColor;
				}
			}
			dialogProcessor.Selection = null;
			viewPort.Invalidate();
		}

        private void pickUpSpeedButton_Click(object sender, EventArgs e)
        {
			toolStripButton6.Checked = false;
			dialogProcessor.IsMultipleSelection = false;
			if (dialogProcessor.MultipleSelection.Count != 0)
			{
				foreach (Shape item in dialogProcessor.MultipleSelection) { 
					if (item.ChangeColor == Color.Empty)
					{
						item.FillColor = dialogProcessor.DefaultFillColor;
					}
					else
					{
						item.FillColor = dialogProcessor.Selection.ChangeColor;
					}
					item.Targeted = false;
				}
			}
			dialogProcessor.MultipleSelection = new List<Shape>();
			viewPort.Invalidate();
		}

        
    }
}
