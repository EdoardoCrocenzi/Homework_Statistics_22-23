namespace Empirical_Distribution
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap bmp2;
        Bitmap bmp3;
        Graphics g;
        Graphics g2;
        Graphics g3;
        Random r = new Random();
        Pen p = new Pen(Brushes.Gray);
        Pen p2 = new Pen(Brushes.Red);
        Pen p3 = new Pen(Brushes.Black);

        double maxRay = 50;
        double points = 1000;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            bmp2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            g2 = Graphics.FromImage(bmp2);
            g2.Clear(Color.White);

            bmp3 = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            g3 = Graphics.FromImage(bmp3);
            g3.Clear(Color.White);

            double minX = -50;
            double minY = -50;

            double maxX = 50;
            double maxY = 50;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            Rectangle VirtualWindow3 = new Rectangle(20, 20, bmp3.Width - 40, bmp3.Height - 40);
            g3.DrawRectangle(p, VirtualWindow3);

            List<double> puntiX = new List<double>();
            List<double> puntiY = new List<double>();

            for (int i = 0; i < points; i++)
            {
                double X = r.NextDouble() * maxRay;
                double Y = r.Next(0,360);

                double x = X * Math.Cos(Y);
                double y = X * Math.Sin(Y);

                int xDevice = FromXRealToXVirtual(x, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                int yDevice = FromYRealToYVirtual(y, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);

                Brush black = (Brush)Brushes.Black;
                Rectangle rettangolo = new Rectangle(xDevice, yDevice, 1, 1);
                g.DrawRectangle(p3, rettangolo);
                g.FillRectangle(black, xDevice, yDevice, 1, 1);

                puntiX.Add(xDevice);
                puntiY.Add(yDevice);

            }

            Dictionary<double, int> HistogramsX = new Dictionary<double, int>();
            Dictionary<double, int> HistogramsY = new Dictionary<double, int>();

            double deltaX = puntiX.Max() - puntiX.Min();
            double deltaY = puntiY.Max() - puntiY.Min();

            double interX = deltaX / 10;
            double interY = deltaY / 10;

            double barabozzoX = puntiX.Min();
            double barabozzoY = puntiY.Min();
            for (int i = 0; i < 10; i++)
            {
                HistogramsX[barabozzoX] = 0;
                HistogramsY[barabozzoY] = 0;
                barabozzoX += interX;
                barabozzoY += interY;
            }

            foreach (double value in puntiX)
            {
                bool interval = false;
                foreach (KeyValuePair<double, int> pair in HistogramsX)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= interX)
                    {
                        HistogramsX[pair.Key] += 2;
                        interval = true;
                        break;
                    }
                }
            }

            foreach (double value in puntiY)
            {
                bool interval = false;
                foreach (KeyValuePair<double, int> pair in HistogramsY)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= interY)
                    {
                        HistogramsY[pair.Key] += 2;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow2.Height / 10;
            int interv = (int)VirtualWindow2.Height / 10;

            foreach (KeyValuePair<double, int> histogram in HistogramsX)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                int yVariance = FromYRealToYVirtual(q, 0, 10 * interv, VirtualWindow2.Top, VirtualWindow2.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, yVariance, Y, interv);
                g2.FillRectangle(Brushes.Red, istogramma);
                g2.DrawRectangle(p, istogramma);

            }

            int qY = (int)VirtualWindow3.Height / 10;
            int intervY = (int)VirtualWindow3.Height / 10;

            foreach (KeyValuePair<double, int> histogram in HistogramsY)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                int yVariance = FromYRealToYVirtual(qY, 0, 10 * intervY, VirtualWindow2.Top, VirtualWindow2.Height);
                qY += intervY;
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, yVariance, Y, intervY);
                g3.FillRectangle(Brushes.Red, istogramma);
                g3.DrawRectangle(p, istogramma);

            }

            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
            this.pictureBox3.Image = bmp3;
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            maxRay = (int) numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            points = (int) numericUpDown2.Value;
        }
    }
}