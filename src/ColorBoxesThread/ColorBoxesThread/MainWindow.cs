using System;
using Gtk;
using System.Threading;

namespace ColorBoxesThread
{
	public class MainWindow : Gtk.Window
	{
		ColorBoxesCanvas canvas = null;
		Thread worker = null;
		VBox mainLayout = new VBox();
		HBox controlButtonsLayout = new HBox();
		Label lblStatusBar = new Label("Ready");
		Button[] controlButtons = { 
			new Button("gtk-media-play"),
			new Button("gtk-media-stop")
		};

		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			this.Title = "Sample GTK# thread example";
			this.SetDefaultSize(343, 311);
			this.DeleteEvent += new DeleteEventHandler(OnWindowDelete);
			this.BorderWidth = 6;
			canvas = new ColorBoxesCanvas();
			//Adjustment the controls
			mainLayout.BorderWidth = 1;
			mainLayout.Spacing = 6;

			controlButtonsLayout.Spacing = 3;
			controlButtonsLayout.BorderWidth = 1;
			controlButtonsLayout.Homogeneous = true;

			for (var i = 0; i < controlButtons.Length;i++)
			{
				controlButtons[i].CanFocus = true;
				controlButtons[i].UseStock = true;
			}
			//Setting the control panel

			controlButtons[0].Clicked += new EventHandler(Run); 
			controlButtons[1].Clicked += new EventHandler(Abort);

			//Adding the button panel
			foreach (var button in controlButtons)
			{
				controlButtonsLayout.Add(button);
			}
			//Adding to the layout
			mainLayout.Add(canvas);
			mainLayout.Add(controlButtonsLayout);
			mainLayout.Add(lblStatusBar);
			this.Add(mainLayout);
			this.ShowAll();
		}

		protected void OnWindowDelete(object o, DeleteEventArgs args)
		{
			if (worker.IsAlive)
				worker.Abort();
			Application.Quit();
		}

		protected void Run(object sender,System.EventArgs e)
		{
			worker = new Thread(canvas.Repaint);
			canvas.KeepRunning = true;
			worker.Start();
			controlButtons[0].Visible = false;
			controlButtons[1].Visible = true;
			lblStatusBar.Text = "Running thread ID: " + Thread.CurrentThread.ManagedThreadId
				+ " at " + DateTime.Now.ToLongTimeString();
		}

		protected void Abort(object sender, System.EventArgs e)
		{
			if (worker.IsAlive)
			{
				worker.Abort();
				controlButtons[0].Visible = true;
				controlButtons[1].Visible = false;
			}
			lblStatusBar.Text = "Stopping thread ID: " + Thread.CurrentThread.ManagedThreadId
				+ " at " + DateTime.Now.ToLongTimeString();
		}
	}
}

