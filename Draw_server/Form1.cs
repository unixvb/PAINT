using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace Draw_server
{
    public partial class Form1 : Form
    {

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr dc);

        public double clientHeight;
        public double clientWidht;
        public double kHeight;
        public double kWidth;

        private int x0, y0, x, y;
        private int cx, cy;

        public Bitmap bitmap;
        public EndPoint clientEndPoint;
        public Graphics graphics;

        private Socket serverSocket;

        private byte[] dataStream = new byte[1024];

        private delegate void UpdateStatusDelegate(string status);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                clientHeight = 500;
                clientWidht = 700;
                kHeight = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height) / clientHeight;
                kWidth = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width) / clientWidht;
                bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                graphics = Graphics.FromImage(bitmap);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint server = new IPEndPoint(IPAddress.Any, 30000);
                serverSocket.Bind(server);
                IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)clients;
                serverSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(ReceiveData), epSender);
                lblStatus.Text = "Listening";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error";
                MessageBox.Show("Load Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReceiveData(IAsyncResult asyncResult)
        {
            try
            {
                Packet receivedData = new Packet(this.dataStream);
                IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)clients;
                serverSocket.EndReceiveFrom(asyncResult, ref epSender);
                switch (receivedData.ChatDataIdentifier)
                {
                    case DataIdentifier.Message:
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
                        MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(receivedData.ChatMessage));
                        Data draw = (Data)js.ReadObject(ms);

                        x = Convert.ToInt32(draw.X * kWidth);
                        y = Convert.ToInt32(draw.Y * kHeight);
                        x0 = Convert.ToInt32(draw.X0 * kWidth);
                        y0 = Convert.ToInt32(draw.Y0 * kHeight);
                        cx = Convert.ToInt32(draw.Cx * kWidth);
                        cy = Convert.ToInt32(draw.Cy * kHeight);

                        Pen pen = new Pen(Color.Blue);
                        pen.Width = draw.PenSize;
                        pen.Color = draw.Color;
                        switch (draw.DrawMode)
                        {
                            case 1:
                                graphics.DrawLine(pen, x0, y0, x, y);
                                break;
                            case 2:
                                graphics.DrawEllipse(pen, x0, y0, cx, cy);
                                break;
                            case 3:
                                if (cx >= 0 && cy >= 0)
                                    graphics.DrawRectangle(pen, x0, y0, cx, cy);
                                else if (cx >= 0 && cy <= 0)
                                    graphics.DrawRectangle(pen, x0, y0 + cy, cx, -1 * cy);
                                else if (cx <= 0 && cy >= 0)
                                    graphics.DrawRectangle(pen, x0 + cx, y0, -1 * cx, cy);
                                else if (cx <= 0 && cy <= 0)
                                    graphics.DrawRectangle(pen, x0 + cx, y0 + cy, -1 * cx, -1 * cy);
                                break;
                        }
                        IntPtr desktopDC = GetDC(IntPtr.Zero);
                        Graphics g = Graphics.FromHdc(desktopDC);
                        g.DrawImage(bitmap, 0, 0);
                        g.Dispose();
                        ReleaseDC(desktopDC);
                        ms.Close();
                        break;
                }
                serverSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(this.ReceiveData), epSender);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ReceiveData Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
