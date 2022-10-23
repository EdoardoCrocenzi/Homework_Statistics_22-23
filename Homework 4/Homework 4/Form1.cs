namespace Homework_4
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Random r = new Random();
        Pen p = new Pen(Brushes.Gray);
        Pen p2 = new Pen(Brushes.Red);
        Pen p3 = new Pen(Brushes.Green);
        Pen p4 = new Pen(Brushes.Blue);
        int absolute_frequency = 0;
        int absolute_tentatives = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            this.richTextBox1.Clear();


            int TrialsCount = 100;
            int Trajectories = 30;

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
            for(int i = 1; i <= Trajectories; i++)
            {
                List<Point> Points = new List<Point>();
                int success = 0;
                int unsuccess = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    int uniform = r.Next(2);
                    if (uniform == 0)
                    {
                        success++;
                        absolute_frequency++;
                        absolute_tentatives++;
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + " Absolute frequency: " + absolute_frequency.ToString() + "/" + absolute_tentatives.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    else
                    {
                        unsuccess++;
                        absolute_tentatives++;
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + " Absolute frequency: " + absolute_frequency.ToString() + "/" + absolute_tentatives.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    int xDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yDevice = FromYRealToYVirtual(success, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Points.Add(new Point(xDevice, yDevice));
                }
                g.DrawLines(p2, Points.ToArray());
                int avg = (int)absolute_frequency / i;
                double normalized =  success / Math.Sqrt(TrialsCount);
                int x = FromXRealToXVirtual(TrialsCount + 1, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                int y = FromYRealToYVirtual(avg, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                g.DrawLine(p4, new Point(20, 20), new Point(x, y));
                x = FromXRealToXVirtual(TrialsCount + 2, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                y = FromYRealToYVirtual(normalized, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                g.DrawLine(p3, new Point(20, 20), new Point(x, y));
            }
            this.pictureBox1.Image = bmp;
        }
        private int FromXRealToXVirtual(double X, double minX, double maxX, int left, int W)
        {
            if ((maxX - minX) == 0)
            {
                return 0;
            }
            int res = (int)((int)left + W * (X - minX) / (maxX - minX));
            return res;
        }
        private int FromYRealToYVirtual(double Y, double minY, double maxY, int top, int H)
        {
            if ((maxY - minY) == 0)
            {
                return 0;
            }
            int res = (int)((int)top + H * (Y - minY) / (maxY - minY));
            return res;
        }
    }
}