using Microsoft.VisualBasic.FileIO;
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

            List<double> Weight = new List<double>();
            List<double> Avg = new List<double>();
            List<double> Variance = new List<double>();
            for (int y = 0; y < Trajectories; y++)
            {
                using (TextFieldParser parser = new TextFieldParser(@"../../../../weight-height.csv"))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.ReadFields();
                    while (!parser.EndOfData)
                    {
                        double x = r.NextDouble();
                        if (x <= 0.5)
                        {
                            string[] fields = parser.ReadFields();
                            double weight = double.Parse(fields[2], CultureInfo.InvariantCulture);
                            Weight.Add(weight);
                        }
                        else
                        {
                            string[] fields = parser.ReadFields();
                        }
                    }
                }
                double avg = Weight.Average();
                double variance = 0;
                for (int x = 0; x < TrialsCount; x++)
                {
                    variance += Math.Pow(Weight[x] - avg, 2);
                }
                Avg.Add(avg);
                Variance.Add(variance/TrialsCount);
            }
            double maxAVG = Avg.Max();
            double maxVARIANCE = Variance.Max();

            double minAVG = Avg.Min();
            double minVARIANCE = Variance.Min();


            List<Point> Draw_Avg = new List<Point>();
            List<Point> Draw_Var = new List<Point>();
            for (int z = 0; z < Trajectories; z++)
            {
                int xAvg = FromXRealToXVirtual(z, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                int yAvg = FromYRealToYVirtual(Avg[z], minAVG, maxAVG, VirtualWindow.Top, VirtualWindow.Height);

                Draw_Avg.Add(new Point(xAvg, yAvg));

                int xVar = FromXRealToXVirtual(z, minX, maxX, VirtualWindow2.Left, VirtualWindow2.Width);
                int yVar = FromYRealToYVirtual(Variance[z], minVARIANCE, maxVARIANCE, VirtualWindow2.Top, VirtualWindow2.Height);

                Draw_Var.Add(new Point(xVar, yVar));
            }
            g.DrawLines(p2, Draw_Avg.ToArray());
            g2.DrawLines(p3, Draw_Var.ToArray());
            this.pictureBox1.Image = bmp;
            this.pictureBox2.Image = bmp2;
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