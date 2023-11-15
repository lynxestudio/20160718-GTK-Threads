using System;
using Gtk;
using System.Drawing;

namespace ColorBoxesThread
{
	public class ColorBoxesCanvas : DrawingArea
	{
		public bool KeepRunning { set; get; }
		int sleepTime = 100;
		int red = 0,green = 0,blue = 0;
		Random random1 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
		Random random2 = null;
		Random random3 = null;


		public ColorBoxesCanvas()
		{
			random2 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
			SetSizeRequest(341,266);
			this.ExposeEvent += new ExposeEventHandler(ExposeHandler);
		}

		public void Repaint()
		{
			Console.WriteLine("Thread running...");
			while (KeepRunning)
			{
				this.ExposeEvent += new ExposeEventHandler(ExposeHandler);
				this.QueueDrawArea(0, 0, this.Allocation.Width, this.Allocation.Height);
				System.Threading.Thread.Sleep(sleepTime);
			}
		}

		protected void ExposeHandler(object o,ExposeEventArgs args)
		{
			random3 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
			Gdk.EventExpose ev = args.Event;
			Gdk.Window window = ev.Window;
			using (Graphics g = Gtk.DotNet.Graphics.FromDrawable(window))
			{
				SolidBrush backBrush = new SolidBrush(Color.White);
				g.FillRectangle(backBrush,0,0,this.Allocation.Width,this.Allocation.Height);
				for (int j = 30; j < Allocation.Height - 15; j += 30)
				{
					for (int i = 5; i < Allocation.Width - 15; i += 30)
					{
						red = GetValue(random1);
						green = GetValue(random2);
						blue = GetValue(random3);
						SolidBrush foreBrush = new SolidBrush(Color.FromArgb(red,green,blue));
						g.FillRectangle(foreBrush,i, j, 25, 25);
						Pen forePen = new Pen(Color.Black);
						g.DrawRectangle(forePen,i - 1, j - 1, 25, 25);
					}

				}
			}
		}

		static int GetValue(Random r)
		{
			byte[] values = new byte[6];
			r.NextBytes(values);
			int index = new Random().Next(0, 6);
			int face = values[index];
			return face;
		}
	}
}

