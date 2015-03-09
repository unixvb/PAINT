using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Client
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        // DRAWING DATA
		private System.ComponentModel.IContainer components;
		private Bitmap bitmap, bm;
		private bool MenBarInit = false;
		private bool drag = false;
		private int DrawMode = 1;
        private Color color;
		private int x0, y0, x, y;
		private int cx, cy;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ToolTip toolTip1;
        private Button button6;
        private TrackBar trackBar1;
        private Label label1;
        private TextBox txtServerIP;
        private Button btnConnect;
		private Graphics gB;
        
        // CONNECTING DATA
        private Socket clientSocket;
        private EndPoint epServer;
        private byte[] dataStream = new byte[1024];
        private Button button4;
    
        private delegate void DisplayMessageDelegate(string message);
        private DisplayMessageDelegate displayMessageDelegate = null;


		public Form1()
		{
			InitializeComponent();

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
		}


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

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Silver;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.txtServerIP);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 26);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(87, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Color";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(407, 4);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(184, 20);
            this.txtServerIP.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(597, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(308, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "1";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(198, 0);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Red;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ImageList = this.imageList1;
            this.button6.Location = new System.Drawing.Point(171, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(21, 21);
            this.button6.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button6, "Draw a Rectangle");
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FloralWhite;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ImageIndex = 2;
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(57, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 21);
            this.button3.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button3, "Draw a Rectangle");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FloralWhite;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImageIndex = 1;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(30, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(21, 21);
            this.button2.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button2, "Draw an Ellipse");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FloralWhite;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 21);
            this.button1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button1, "Draw a Line");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Draw";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

		}

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
            button4.ForeColor = Color.Black;
            color = Color.Red;

            this.displayMessageDelegate = new DisplayMessageDelegate(this.DisplayMessage);
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
            pen.Width = trackBar1.Value;
            pen.Color = color;
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
                    if (cx >= 0 && cy >= 0)
                        gB.DrawRectangle(pen, x0, y0, cx, cy);
                    else if (cx >= 0 && cy <= 0)
                        gB.DrawRectangle(pen, x0, y0 + cy, cx, -1 * cy);
                    else if (cx <= 0 && cy >= 0)
                        gB.DrawRectangle(pen, x0 + cx, y0, -1 * cx, cy);
                    else if (cx <= 0 && cy <= 0)
                        gB.DrawRectangle(pen, x0 + cx, y0 + cy, -1 * cx, -1 * cy);
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
            pen.Width = trackBar1.Value;
			if (drag)
            {
                pen.Color = color;
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
                        if (cx >= 0 && cy >= 0)
                            g.DrawRectangle(pen, x0, y0, cx, cy);
                        else if (cx >= 0 && cy <= 0)
                            g.DrawRectangle(pen, x0, y0 + cy, cx, -1 * cy);
                        else if (cx <= 0 && cy >= 0)
                            g.DrawRectangle(pen, x0 + cx, y0, -1 * cx, cy);
                        else if (cx <= 0 && cy <= 0)
                            g.DrawRectangle(pen, x0 + cx, y0 + cy, -1 * cx, -1 * cy);
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
            //using (Form form = new Form())
            //{
            //    form.StartPosition = FormStartPosition.CenterScreen;
            //    form.Size = bm.Size;
            //
            //    PictureBox pb = new PictureBox();
            //    pb.Dock = DockStyle.Fill;
            //    pb.Image = bm;
            //
            //    form.Controls.Add(pb);
            //    form.ShowDialog();
            //}
            Send();
		}

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(trackBar1.Value);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Packet sendData = new Packet();
                sendData.ChatMessage = null;
                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverIP = IPAddress.Parse(txtServerIP.Text.Trim());
                IPEndPoint server = new IPEndPoint(serverIP, 30000);
                epServer = (EndPoint)server;
                byte[] data = sendData.GetDataStream();
                clientSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Send()
        {
            try
            {
                Data data = new Data()
                {
                    DrawMode = DrawMode,
                    Color = color,
                    X0 = x0,
                    Y0 = y0,
                    X = x,
                    Y = y,
                    Cx = cx,
                    Cy = cy,
                    PenSize = trackBar1.Value
                };
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, data);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);

                Packet sendData = new Packet();
                sendData.ChatMessage = sr.ReadToEnd();
                sendData.ChatDataIdentifier = DataIdentifier.Message;
                byte[] byteData = sendData.GetDataStream();
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);

                sr.Close();
                ms.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Error: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendData(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Data: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DisplayMessage(string messge)
        {
            //rtxtConversation.Text += messge + Environment.NewLine;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = color;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color = colorDlg.Color;
                button6.BackColor = color;
            }
        }
	}
}
