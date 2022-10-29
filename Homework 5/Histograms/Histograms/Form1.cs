namespace Histograms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int throws = 1;
        Bitmap bmp;
        Bitmap bmp2;
        Graphics g;
        Graphics g2;
        Random r = new Random();
        Pen p = new Pen(Brushes.Gray);
        Pen p2 = new Pen(Brushes.Red);
        Pen p3 = new Pen(Brushes.Green);
        Pen p4 = new Pen(Brushes.Blue);

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.label2.Text = this.trackBar2.Value.ToString();
            throws = this.trackBar2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.TranslateTransform(0, bmp.Height);
            g.ScaleTransform(1, -1);

            bmp2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            g2 = Graphics.FromImage(bmp2);
            g2.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = throws;
            double maxY = throws;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            Dictionary<Point, int> Vertical_Throws = new Dictionary<Point, int>();
            Dictionary<Point, int> Horizontal_Throws = new Dictionary<Point, int>();
            Dictionary<int, int> Histograms = new Dictionary<int, int>();
            for (int j = 0; j < throws; j++)
            {
                int uniform = r.Next(1,7);
                bool inside_dict = false;
                foreach (KeyValuePair<int,int> pair in Histograms)
                {
                    if (uniform == pair.Key)
                    {
                        inside_dict = true;
                        Histograms[pair.Key] += 1;
                        break;
                    }
                }
                if (inside_dict == false)
                {
                    Histograms[uniform] = 1;
                }
            }
            int i = 0;
            foreach (KeyValuePair<int,int> pair in Histograms)
            {

                int X1 = FromXRealToXVirtual(i, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                int Y1 = FromYRealToYVirtual(pair.Value, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);

                int X2 = FromXRealToXVirtual(i, minX, maxX, VirtualWindow2.Left, VirtualWindow2.Width);
                int Y2 = FromYRealToYVirtual(pair.Value, minY, maxY, VirtualWindow2.Top, VirtualWindow2.Height);


                Point point1 = new Point(X1,Y1);
                Point point2 = new Point(Y2, X2);

                Vertical_Throws[point1] = pair.Value;
                Horizontal_Throws[point2] = pair.Value;

                i += 30;
            }
            foreach (KeyValuePair<Point, int> histogram in Vertical_Throws)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow.Height)
                {
                    Y = VirtualWindow.Height;
                }
                Rectangle istogramma = new Rectangle(histogram.Key.X, VirtualWindow.Left, 10, Y);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);

            }
            foreach (KeyValuePair<Point, int> histogram in Horizontal_Throws)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, histogram.Key.Y, Y, 10);
                g2.FillRectangle(Brushes.Red, istogramma);
                g2.DrawRectangle(p, istogramma);

            }
            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
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
            int res = (int)((int)top + H - H * (Y - minY) / (maxY - minY));
            return res;
        }
    }
}