using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RubberBand1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private Bitmap bitmap, bm;
		private bool MenBarInit = false;
		private bool drag = false;
		private int DrawMode = 1;
		private int x0, y0, x, y;
		private int cx, cy;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ToolTip toolTip1;
		private Graphics gB;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Silver;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.button3,
																				 this.button2,
																				 this.button1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(488, 30);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.FloralWhite;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.ImageIndex = 2;
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new System.Drawing.Point(70, 2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(25, 25);
			this.button3.TabIndex = 0;
			this.toolTip1.SetToolTip(this.button3, "Draw a Rectangle");
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FloralWhite;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.ImageIndex = 1;
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new System.Drawing.Point(43, 2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(25, 25);
			this.button2.TabIndex = 0;
			this.toolTip1.SetToolTip(this.button2, "Draw an Ellipse");
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FloralWhite;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ImageIndex = 0;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(16, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(25, 25);
			this.button1.TabIndex = 0;
			this.toolTip1.SetToolTip(this.button1, "Draw a Line");
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(488, 268);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Name = "Form1";
			this.Text = "Form1";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			Size size = SystemInformation.PrimaryMonitorMaximizedWindowSize;
			bitmap = new Bitmap(size.Width, size.Height);
			gB = Graphics.FromImage(bitmap);
			Color bckColor = this.BackColor;
			gB.Clear(bckColor);
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			x0 = p.X;
			y0 = p.Y;
			drag = true;
		}

		private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			x= p.X;
			y = p.Y;
			cx = x - x0;
			cy = y - y0;
			if (drag)
			{
				Invalidate();
            }
		}

		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			cx = x - x0;
			cy = y - y0;
			Pen pen = new Pen(Color.Blue);
			switch (DrawMode)
			{
				case 1:
				{
					gB.DrawLine(pen, x0, y0, x, y);
					break;
				}
				case 2:
				{
					gB.DrawEllipse(pen, x0, y0, cx, cy);
					break;
				}
				case 3:
				{
					gB.DrawRectangle(pen, x0, y0, cx, cy);
					break;
				}
			}
			RefreshBackground();
			drag = false;
			pen.Dispose();
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen pen = new Pen(Color.Blue);
			if (drag)
			{
				switch (DrawMode)
				{
					case 1:
					{
						g.DrawLine(pen, x0, y0, x, y);
						break;
					}
					case 2:
					{
						g.DrawEllipse(pen, x0, y0, cx, cy);
						break;
					}
					case 3:
					{
						g.DrawRectangle(pen, x0, y0, cx, cy);
						break;
					}
				}
			}

			pen.Dispose();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			button1.ForeColor = Color.Red;
			button2.ForeColor = Color.Black;
			button3.ForeColor = Color.Black;
			DrawMode = 1;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			button2.ForeColor = Color.Red;
			button1.ForeColor = Color.Black;
			button3.ForeColor = Color.Black;
			DrawMode = 2;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			button3.ForeColor = Color.Red;
			button1.ForeColor = Color.Black;
			button2.ForeColor = Color.Black;
			DrawMode = 3;
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (MenBarInit == false)
			{
				button1.ForeColor = Color.Red;
				button1.Focus();
			}
			MenBarInit = true;
		}

		private void Form1_SizeChanged(object sender, System.EventArgs e)
		{
			RefreshBackground();
		}

		private void RefreshBackground()
		{
			Size sz = this.Size;
			Rectangle rt = new Rectangle(0, 0, sz.Width, sz.Height);
			bm = bitmap.Clone(rt, bitmap.PixelFormat);
			BackgroundImage = bm;
		}
	}
}
