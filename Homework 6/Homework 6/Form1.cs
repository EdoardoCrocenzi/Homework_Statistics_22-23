using Microsoft.VisualBasic.FileIO;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Homework_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        Bitmap bmp2;
        Graphics g;
        Graphics g2;
        Random r = new Random();
        Pen p = new Pen(Brushes.Gray);
        Pen p2 = new Pen(Brushes.Red);
        Pen p3 = new Pen(Brushes.Green);
        Pen p4 = new Pen(Brushes.Blue);

        int TrialsCount = 100;
        int Trajectories = 100;


        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            bmp2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            g2 = Graphics.FromImage(bmp2);
            g2.Clear(Color.White);

            double minX = 0;
            double minY = 0;

            double maxX = Trajectories;
            double maxY;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);

            Rectangle VirtualWindow2 = new Rectangle(20, 20, bmp2.Width - 40, bmp2.Height - 40);
            g2.DrawRectangle(p, VirtualWindow2);

            List<double> Avg = new List<double>();
            List<double> Variance = new List<double>();
            List<double> Population = new List<double>();
            for (int y = 0; y < Trajectories; y++)
            {
                Population = new List<double>();
                List<double> Weight = new List<double>();
                using (TextFieldParser parser = new TextFieldParser(@"../../../../weight-height.csv"))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.ReadFields();
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        double weight = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        Population.Add(weight);
                        double x = r.NextDouble();
                        if (x <= 0.5)
                        {
                            Weight.Add(weight);
                            
                        }
                    }
                }
                double avg = Math.Round(Weight.Average(), 2);
                double variance = 0;
                variance += Math.Pow(Weight[y] - avg, 2);
                Avg.Add(avg);
                Variance.Add(Math.Round((variance / TrialsCount), 2));
            }
            double maxAVG = Avg.Max();
            double maxVARIANCE = Variance.Max();
            

            double minAVG = Avg.Min();
            double minVARIANCE = Variance.Min();

            double deltaAVG = maxAVG - minAVG;
            double deltaVARIANCE = maxVARIANCE - minVARIANCE;
            double intAVG = deltaAVG / 10;
            double intVARIANCE = deltaVARIANCE / 10;

            Dictionary<double, int> intervalsAVG = new Dictionary<double, int>();
            Dictionary<double, int> intervalsVARIANCE = new Dictionary<double, int>();

            double barabozzo = minAVG;
            double barabozzo2 = minVARIANCE;
            for (int x = 0; x < 10; x++)
            {
                intervalsAVG[barabozzo] = 0;
                intervalsVARIANCE[barabozzo2] = 0;

                barabozzo += intAVG;
                barabozzo2 += intVARIANCE;
            }

            foreach (double value in Avg)
            {
                bool interval = false;
                foreach (KeyValuePair<double, int> pair in intervalsAVG)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= intAVG)
                    {
                        intervalsAVG[pair.Key] += 5;
                        interval = true;
                        break;
                    }
                }
            }

            foreach (double value in Variance)
            {
                bool interval = false;
                foreach (KeyValuePair<double, int> pair in intervalsVARIANCE)
                {
                    double v = Math.Abs(value - pair.Key);
                    if (v <= intVARIANCE)
                    {
                        intervalsVARIANCE[pair.Key] += 5;
                        interval = true;
                        break;
                    }
                }

            }

            int z = (int)VirtualWindow.Height / 10;
            int intervallo = (int)VirtualWindow.Height / 10;
            foreach (KeyValuePair<double, int> histogram in intervalsAVG)
            {
                int Y = histogram.Value;
                if (Y > VirtualWindow.Width)
                {
                    Y = VirtualWindow.Width;
                }
                int yAvg = FromYRealToYVirtual(z, 0, 10 * intervallo, VirtualWindow.Top, VirtualWindow.Height);
                z += intervallo;
                Rectangle istogramma = new Rectangle(VirtualWindow.Left, yAvg, Y, intervallo);
                g.FillRectangle(Brushes.Red, istogramma);
                g.DrawRectangle(p, istogramma);

            }
            int q = (int)VirtualWindow2.Height / 10;
            int interv = (int)VirtualWindow2.Height / 10;

            foreach (KeyValuePair<double, int> histogram in intervalsVARIANCE)
            {
                int Y = histogram.Value;
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

            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
            this.richTextBox3.AppendText("SAMPLE_AVG, POPULATION_AVG: " + Avg.Average().ToString() + ", " + Population.Average().ToString() +"\n");
            this.richTextBox3.ScrollToCaret();
            List<double> Pop_Var = new List<double>();
            for (int a = 0; a < Population.Count(); a++)
            {
                double variance = Math.Pow(Population[a] - Population.Average(), 2);
                Pop_Var.Add((variance / Population.Count()));
            }
            this.richTextBox3.AppendText("SAMPLE_VARIANCE, POPULATION_VARIANCE: " + Variance.Average().ToString() + ", " + Pop_Var.Average().ToString() + "\n");
            this.richTextBox3.ScrollToCaret();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Trajectories = (int)this.numericUpDown1.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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