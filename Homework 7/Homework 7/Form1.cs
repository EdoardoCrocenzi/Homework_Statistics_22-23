namespace Homework_7
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
        Pen p3 = new Pen(Brushes.Green);
        Pen p4 = new Pen(Brushes.Blue);

        double lambda = 1;
        double intervals = 1;
        int TrialsCount = 100;
        int Trajectories = 10;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, MouseEventArgs e)
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

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            Rectangle VirtualWindow3 = new Rectangle(20, 20, bmp3.Width - 40, bmp3.Height - 40);
            g3.DrawRectangle(p, VirtualWindow3);

            List<Point> Final_Point = new List<Point>();
            List<double> Distance = new List<double>();
            double Bernoulli = lambda / intervals;
            double max_distance = 0;
            double min_distance = 500000;
            for (int i = 1; i <= Trajectories; i++)
            {
                int distance = 0;
                int old_distance = 0;
                List<Point> Absolute_Frequency = new List<Point>();
                List<Point> Distance_Points = new List<Point>();
                int success = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    double uniform = r.NextDouble();
                    if (uniform <= Bernoulli)
                    {
                        success++;
                        old_distance = distance;
                        Distance.Add(distance);
                        if (max_distance < distance)
                        {
                            max_distance = distance;
                        }
                        if (min_distance > distance)
                        {
                            min_distance = distance;
                        }
                        distance = 0;
                    }
                    else
                    {
                        distance++;
                    }
                    int xAbsoluteDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yAbsoluteDevice = FromYRealToYVirtual(success, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);

                    int yDistance = FromYRealToYVirtual(old_distance, minY, maxY, VirtualWindow3.Top, VirtualWindow3.Height);
                    Distance_Points.Add(new Point(xAbsoluteDevice, yDistance));
                    old_distance = 0; 
                    Absolute_Frequency.Add(new Point(xAbsoluteDevice, yAbsoluteDevice));
                    if (j == TrialsCount - 1)
                    {
                        Final_Point.Add(new Point(xAbsoluteDevice, yAbsoluteDevice));
                    }
                }
                g.DrawLines(p2, Absolute_Frequency.ToArray());
            }
            double delta = max_distance - min_distance;
            double inter = delta / 10;

            Dictionary<double, double> Distance_Intervals = new Dictionary<double, double>();
            double barabozzo = min_distance;

            for (int i = 0; i < 10; i++)
            {
                Distance_Intervals[i] = barabozzo;
                barabozzo += inter;
            }

            foreach (double value in Distance)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Distance_Intervals)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= inter)
                    {
                        Distance_Intervals[pair.Key] += 2;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow3.Height / 10;
            int interv = (int)VirtualWindow3.Height / 10;

            foreach (KeyValuePair<double, double> histogram in Distance_Intervals)
            {
                int Y = (int) histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                int yVariance = FromYRealToYVirtual(q, 0, 10 * interv, VirtualWindow2.Top, VirtualWindow2.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, yVariance, Y, interv);
                g3.FillRectangle(Brushes.Red, istogramma);
                g3.DrawRectangle(p, istogramma);

            }
            this.pictureBox3.Image = bmp3;

            Dictionary<Point, int> Histograms = new Dictionary<Point, int>();

            foreach (Point punto in Final_Point)
            {
                bool interval = false;
                foreach (KeyValuePair<Point, int> pair in Histograms)
                {
                    int v = Math.Abs(punto.Y - pair.Key.Y);
                    if (v <= 5)
                    {
                        Histograms[pair.Key] += 5;
                        interval = true;
                        break;
                    }
                }
                if (interval == false)
                {
                    Histograms.Add(punto, 5);
                }

            }
            foreach (KeyValuePair<Point, int> histogram in Histograms)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, histogram.Key.Y, Y, 5);
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

        private void button2_Click(object sender, EventArgs e)
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

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            Rectangle VirtualWindow3 = new Rectangle(20, 20, bmp3.Width - 40, bmp3.Height - 40);
            g3.DrawRectangle(p, VirtualWindow3);

            List<Point> Final_Point = new List<Point>();
            List<double> Distance = new List<double>();
            double Bernoulli = lambda / intervals;
            double max_distance = 0;
            double min_distance = 500000;
            for (int i = 1; i <= Trajectories; i++)
            {
                int distance = 0;
                int old_distance = 0;
                List<Point> Distance_Points = new List<Point>();
                List<Point> Relative_Frequency = new List<Point>();
                int success = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    double uniform = r.NextDouble();
                    if (uniform <= Bernoulli)
                    {
                        success++;
                        old_distance = distance;
                        Distance.Add(distance);
                        if (max_distance < distance)
                        {
                            max_distance = distance;
                        }
                        if (min_distance > distance)
                        {
                            min_distance = distance;
                        }
                        distance = 0;
                    }
                    else
                    {
                        distance++;
                    }
                    double Y = success * TrialsCount / (j + 1);
                    int xRelativeDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yRelativeDevice = FromYRealToYVirtual(Y, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    int yDistance = FromYRealToYVirtual(old_distance, minY, maxY, VirtualWindow3.Top, VirtualWindow3.Height);
                    Relative_Frequency.Add(new Point(xRelativeDevice, yRelativeDevice));

                    Distance_Points.Add(new Point(xRelativeDevice, yDistance));
                    old_distance = 0;

                    if (j == TrialsCount - 1)
                    {
                        Final_Point.Add(new Point(xRelativeDevice, yRelativeDevice));
                    }
                }
                g.DrawLines(p3, Relative_Frequency.ToArray());
            }
            Dictionary<Point, int> Histograms = new Dictionary<Point, int>();

            double delta = max_distance - min_distance;
            double inter = delta / 10;

            Dictionary<double, double> Distance_Intervals = new Dictionary<double, double>();
            double barabozzo = min_distance;

            for (int i = 0; i < 10; i++)
            {
                Distance_Intervals[i] = barabozzo;
                barabozzo += inter;
            }

            foreach (double value in Distance)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Distance_Intervals)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= inter)
                    {
                        Distance_Intervals[pair.Key] += 2;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow3.Height / 10;
            int interv = (int)VirtualWindow3.Height / 10;

            foreach (KeyValuePair<double, double> histogram in Distance_Intervals)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                int yVariance = FromYRealToYVirtual(q, 0, 10 * interv, VirtualWindow2.Top, VirtualWindow2.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, yVariance, Y, interv);
                g3.FillRectangle(Brushes.Red, istogramma);
                g3.DrawRectangle(p, istogramma);

            }
            this.pictureBox3.Image = bmp3;

            foreach (Point punto in Final_Point)
            {
                bool interval = false;
                foreach (KeyValuePair<Point, int> pair in Histograms)
                {
                    int v = Math.Abs(punto.Y - pair.Key.Y);
                    if (v <= 5)
                    {
                        Histograms[pair.Key] += 5;
                        interval = true;
                        break;
                    }
                }
                if (interval == false)
                {
                    Histograms.Add(punto, 5);
                }

            }
            foreach (KeyValuePair<Point, int> histogram in Histograms)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, histogram.Key.Y, Y, 5);
                g2.FillRectangle(Brushes.Green, istogramma);
                g2.DrawRectangle(p, istogramma);

            }
            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
        }

        private void button3_Click(object sender, EventArgs e)
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

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            Rectangle VirtualWindow3 = new Rectangle(20, 20, bmp3.Width - 40, bmp3.Height - 40);
            g3.DrawRectangle(p, VirtualWindow3);

            List<Point> Final_Point = new List<Point>();
            List<double> Distance = new List<double>();
            double Bernoulli = lambda / intervals;
            double max_distance = 0;
            double min_distance = 500000;
            for (int i = 1; i <= Trajectories; i++)
            {
                int distance = 0;
                int old_distance = 0;
                List<Point> Distance_Points = new List<Point>();
                List<Point> Normalized_Frequency = new List<Point>();
                int success = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    double uniform = r.NextDouble();
                    if (uniform <= Bernoulli)
                    {
                        success++;
                        old_distance = distance;
                        Distance.Add(distance);
                        if (max_distance < distance)
                        {
                            max_distance = distance;
                        }
                        if (min_distance > distance)
                        {
                            min_distance = distance;
                        }
                        distance = 0;
                    }
                    else
                    {
                        distance++;
                    }
                    double nY = success * Math.Sqrt(TrialsCount) / (Math.Sqrt(j + 1));
                    int xNormalizedDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yNormalizedDevice = FromYRealToYVirtual(nY, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Normalized_Frequency.Add(new Point(xNormalizedDevice, yNormalizedDevice));

                    int yDistance = FromYRealToYVirtual(old_distance, minY, maxY, VirtualWindow3.Top, VirtualWindow3.Height);

                    Distance_Points.Add(new Point(xNormalizedDevice, yDistance));
                    old_distance = 0;

                    if (j == TrialsCount - 1)
                    {
                        Final_Point.Add(new Point(xNormalizedDevice, yNormalizedDevice));
                    }
                }
                g.DrawLines(p4, Normalized_Frequency.ToArray());
            }
            Dictionary<Point, int> Histograms = new Dictionary<Point, int>();

            double delta = max_distance - min_distance;
            double inter = delta / 10;

            Dictionary<double, double> Distance_Intervals = new Dictionary<double, double>();
            double barabozzo = min_distance;

            for (int i = 0; i < 10; i++)
            {
                Distance_Intervals[i] = barabozzo;
                barabozzo += inter;
            }

            foreach (double value in Distance)
            {
                bool interval = false;
                foreach (KeyValuePair<double, double> pair in Distance_Intervals)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= inter)
                    {
                        Distance_Intervals[pair.Key] += 2;
                        interval = true;
                        break;
                    }
                }
            }

            int q = (int)VirtualWindow3.Height / 10;
            int interv = (int)VirtualWindow3.Height / 10;

            foreach (KeyValuePair<double, double> histogram in Distance_Intervals)
            {
                int Y = (int)histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                int yVariance = FromYRealToYVirtual(q, 0, 10 * interv, VirtualWindow2.Top, VirtualWindow2.Height);
                q += interv;
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, yVariance, Y, interv);
                g3.FillRectangle(Brushes.Red, istogramma);
                g3.DrawRectangle(p, istogramma);

            }
            this.pictureBox3.Image = bmp3;

            foreach (Point punto in Final_Point)
            {
                bool interval = false;
                foreach (KeyValuePair<Point, int> pair in Histograms)
                {
                    int v = Math.Abs(punto.Y - pair.Key.Y);
                    if (v <= 5)
                    {
                        Histograms[pair.Key] += 5;
                        interval = true;
                        break;
                    }
                }
                if (interval == false)
                {
                    Histograms.Add(punto, 5);
                }

            }
            foreach (KeyValuePair<Point, int> histogram in Histograms)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow2.Width)
                {
                    Y = VirtualWindow2.Width;
                }
                Rectangle istogramma = new Rectangle(VirtualWindow2.Left, histogram.Key.Y, Y, 5);
                g2.FillRectangle(Brushes.Blue, istogramma);
                g2.DrawRectangle(p, istogramma);

            }
            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            intervals = (double) this.numericUpDown2.Value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lambda = (double) this.numericUpDown1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label4.Text = this.trackBar1.Value.ToString();
            TrialsCount = this.trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.label6.Text = this.trackBar2.Value.ToString();
            Trajectories = this.trackBar2.Value;
        }
    }
}