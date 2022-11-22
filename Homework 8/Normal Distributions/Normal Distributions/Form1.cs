using System.Diagnostics.Metrics;

namespace Normal_Distributions
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Random r = new Random();
        Pen p = new Pen(Brushes.Gray);
        Pen p2 = new Pen(Brushes.Red);

        int TrialsCount = 10000;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            double maxValue = 0;
            double minValue = 368468754;

            List<double> Punti = new List<double>();


            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
            for (int j = 0; j < TrialsCount; j++)
                {
                    double X = r.NextDouble() * 2 - 1;
                    double Y = r.NextDouble() * 2 - 1;

                    double S = Math.Pow(X, 2) + Math.Pow(Y, 2);

                    while (S > 1 || S < 0)
                    {
                        X = r.NextDouble() * 2 - 1;
                        Y = r.NextDouble() * 2 - 1;

                        S = Math.Pow(X, 2) + Math.Pow(Y, 2);
                    }
                    double T = Math.Sqrt((-2 * Math.Log(S)) / S);
                    double Z1 = T * X;
                    double Z2 = T * Y;

                    Punti.Add(Z1);
                }
            double delta = Punti.Max() - Punti.Min();
            double inter = delta / 200;
            Dictionary<double, double> Histogram = new Dictionary<double, double>();
            double barabozzo = Punti.Min();

            for (int i = 0; i < 200; i++)
            {
                Histogram[barabozzo] = 0;
                barabozzo += inter;
            }

            foreach (double z1 in Punti)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Histogram)
                {
                    double v = Math.Abs(z1 - pair.Key);
                    if (v <= inter)
                    {
                        Histogram[pair.Key] += 1;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int) VirtualWindow.Height / 200;
            int interv = (int) VirtualWindow.Height / 200;

            foreach(KeyValuePair<double, double> histogram in Histogram)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow.Width) {
                    Y = VirtualWindow.Width;
                }
                int y = FromYRealToYVirtual(q, 0, 200 * interv, VirtualWindow.Top, VirtualWindow.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, y, Y, interv);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);
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
            int res = (int)((int)top + H - H * (Y - minY) / (maxY - minY));
            return res;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label2.Text = this.trackBar1.Value.ToString();
            TrialsCount = this.trackBar1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            double maxValue = 0;
            double minValue = 368468754;

            List<double> Punti = new List<double>();


            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
                for (int j = 0; j < TrialsCount; j++)
                {
                    double X = r.NextDouble() * 2 - 1;
                    double Y = r.NextDouble() * 2 - 1;

                    double S = Math.Pow(X, 2) + Math.Pow(Y, 2);

                    while (S > 1 || S < 0)
                    {
                        X = r.NextDouble() * 2 - 1;
                        Y = r.NextDouble() * 2 - 1;

                        S = Math.Pow(X, 2) + Math.Pow(Y, 2);
                    }
                    double T = Math.Sqrt((-2 * Math.Log(S)) / S);
                    double Z1 = T * X;
                    double Z2 = T * Y;

                    Punti.Add(Math.Pow(Z1,2));
                }
            double delta = Punti.Max() - Punti.Min();
            double inter = delta / 200;

            Dictionary<double, double> Histogram = new Dictionary<double, double>();
            double barabozzo = Punti.Min();

            for (int i = 0; i < 200; i++)
            {
                Histogram[barabozzo] = 0;
                barabozzo += inter;
            }

            foreach (double z1 in Punti)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Histogram)
                {
                    double v = Math.Abs(z1 - pair.Key);
                    if (v <= inter)
                    {
                        Histogram[pair.Key] += 1;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow.Height / 200;
            int interv = (int)VirtualWindow.Height / 200;

            foreach (KeyValuePair<double, double> histogram in Histogram)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow.Width)
                {
                    Y = VirtualWindow.Width;
                }
                int y = FromYRealToYVirtual(q, 0, 200 * interv, VirtualWindow.Top, VirtualWindow.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, y, Y, interv);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);
            }
            this.pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            List<double> Punti = new List<double>();


            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
                for (int j = 0; j < TrialsCount; j++)
                {
                    double X = r.NextDouble() * 2 - 1;
                    double Y = r.NextDouble() * 2 - 1;

                    double S = Math.Pow(X, 2) + Math.Pow(Y, 2);

                    while (S > 1 || S < 0)
                    {
                        X = r.NextDouble() * 2 - 1;
                        Y = r.NextDouble() * 2 - 1;

                        S = Math.Pow(X, 2) + Math.Pow(Y, 2);
                    }
                    double T = Math.Sqrt((-2 * Math.Log(S)) / S);
                    double Z1 = T * X;
                    double Z2 = T * Y;

                    Punti.Add(Z1/Math.Pow(Z2, 2));
                }
            Punti.Sort();
            List<double> Points = new List<double>();
            for (int x = 9; x < Punti.Count()-9; x++)
            {
                Points.Add(Punti[x]);
            }
            double delta = Math.Abs(Points.Max() - Points.Min());
            double inter = delta / 200;


            Dictionary<double, double> Histogram = new Dictionary<double, double>();
            double barabozzo = Points.Min();

            for (int i = 0; i < 200; i++)
            {
                Histogram[barabozzo] = 0;
                barabozzo += inter;
            }

            foreach (double z1 in Points)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Histogram)
                {
                    double v = Math.Abs(z1 - pair.Key);
                    if (v <= inter)
                    {
                        Histogram[pair.Key] += 1;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow.Height / 200;
            int interv = (int)VirtualWindow.Height / 200;

            foreach (KeyValuePair<double, double> histogram in Histogram)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow.Width)
                {
                    Y = VirtualWindow.Width;
                }
                int y = FromYRealToYVirtual(q, 0, 200 * interv, VirtualWindow.Top, VirtualWindow.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, y, Y, interv);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);
            }
            this.pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            List<double> Punti = new List<double>();


            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
                for (int j = 0; j < TrialsCount; j++)
                {
                    double X = r.NextDouble() * 2 - 1;
                    double Y = r.NextDouble() * 2 - 1;

                    double S = Math.Pow(X, 2) + Math.Pow(Y, 2);

                    while (S > 1 || S < 0)
                    {
                        X = r.NextDouble() * 2 - 1;
                        Y = r.NextDouble() * 2 - 1;

                        S = Math.Pow(X, 2) + Math.Pow(Y, 2);
                    }
                    double T = Math.Sqrt((-2 * Math.Log(S)) / S);
                    double Z1 = T * X;
                    double Z2 = T * Y;

                    Punti.Add(Math.Pow(Z1,2) / Math.Pow(Z2, 2));
                }
            Punti.Sort();
            List<double> Points = new List<double>();
            for (int x = 9; x < Punti.Count() - 9; x++)
            {
                Points.Add(Punti[x]);
            }
            double delta = Math.Abs(Points.Max() - Points.Min());
            double inter = delta / 200;


            Dictionary<double, double> Histogram = new Dictionary<double, double>();
            double barabozzo = Points.Min();

            for (int i = 0; i < 200; i++)
            {
                Histogram[barabozzo] = 0;
                barabozzo += inter;
            }

            foreach (double z1 in Points)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Histogram)
                {
                    double v = Math.Abs(z1 - pair.Key);
                    if (v <= inter)
                    {
                        Histogram[pair.Key] += 1;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow.Height / 200;
            int interv = (int)VirtualWindow.Height / 200;

            foreach (KeyValuePair<double, double> histogram in Histogram)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow.Width)
                {
                    Y = VirtualWindow.Width;
                }
                int y = FromYRealToYVirtual(q, 0, 200 * interv, VirtualWindow.Top, VirtualWindow.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, y, Y, interv);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);
            }
            this.pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            List<double> Punti = new List<double>();


            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
                for (int j = 0; j < TrialsCount; j++)
                {
                    double X = r.NextDouble() * 2 - 1;
                    double Y = r.NextDouble() * 2 - 1;

                    double S = Math.Pow(X, 2) + Math.Pow(Y, 2);

                    while (S > 1 || S < 0)
                    {
                        X = r.NextDouble() * 2 - 1;
                        Y = r.NextDouble() * 2 - 1;

                        S = Math.Pow(X, 2) + Math.Pow(Y, 2);
                    }
                    double T = Math.Sqrt((-2 * Math.Log(S)) / S);
                    double Z1 = T * X;
                    double Z2 = T * Y;

                    Punti.Add(Z1 / Z2);
                }
            Punti.Sort();
            List<double> Points = new List<double>();
            for (int x = 9; x < Punti.Count() - 9; x++)
            {
                Points.Add(Punti[x]);
            }
            double delta = Math.Abs(Points.Max() - Points.Min());
            double inter = delta / 200;


            Dictionary<double, double> Histogram = new Dictionary<double, double>();
            double barabozzo = Points.Min();

            for (int i = 0; i < 200; i++)
            {
                Histogram[barabozzo] = 0;
                barabozzo += inter;
            }

            foreach (double z1 in Points)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Histogram)
                {
                    double v = Math.Abs(z1 - pair.Key);
                    if (v <= inter)
                    {
                        Histogram[pair.Key] += 1;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow.Height / 200;
            int interv = (int)VirtualWindow.Height / 200;

            foreach (KeyValuePair<double, double> histogram in Histogram)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow.Width)
                {
                    Y = VirtualWindow.Width;
                }
                int y = FromYRealToYVirtual(q, 0, 200 * interv, VirtualWindow.Top, VirtualWindow.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, y, Y, interv);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);
            }
            this.pictureBox1.Image = bmp;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}