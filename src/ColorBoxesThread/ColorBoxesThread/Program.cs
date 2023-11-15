using System;
using Gtk;

namespace ColorBoxesThread
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = new MainWindow();
			win.Show();
			Console.WriteLine("GTK# Clock example running...");
			Application.Run();
		}
	}
}
