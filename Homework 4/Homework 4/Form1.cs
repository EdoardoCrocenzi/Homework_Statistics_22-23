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
            int Trajectories = 10;

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
            for(int i = 1; i <= Trajectories; i++)
            {
                List<Point> Absolute_Frequency = new List<Point>();
                int success = 0;
                int unsuccess = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    int uniform = r.Next(2);
                    if (uniform == 0)
                    {
                        success++;
                        double abs_freq = success * TrialsCount / (j + 1);
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + " Absolute Frequency: " + abs_freq.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    else
                    {
                        unsuccess++;
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    int xAbsoluteDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yAbsoluteDevice = FromYRealToYVirtual(success, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Absolute_Frequency.Add(new Point(xAbsoluteDevice, yAbsoluteDevice));
                }
                g.DrawLines(p2, Absolute_Frequency.ToArray());
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

        private void button2_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            this.richTextBox1.Clear();


            int TrialsCount = 100;
            int Trajectories = 10;

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
            for (int i = 1; i <= Trajectories; i++)
            {
                List<Point> Relative_Frequency = new List<Point>();
                int success = 0;
                int unsuccess = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    int uniform = r.Next(2);
                    if (uniform == 0)
                    {
                        success++;
                        double rel_freq = success *  TrialsCount/ (j + 1);
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + " Relative Frequency: " + rel_freq.ToString() +  "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    else
                    {
                        unsuccess++;
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    double Y = success * TrialsCount / (j + 1);
                    int xRelativeDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yRelativeDevice = FromYRealToYVirtual(Y, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Relative_Frequency.Add(new Point(xRelativeDevice, yRelativeDevice));

                }
                g.DrawLines(p3, Relative_Frequency.ToArray());
            }
            this.pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            this.richTextBox1.Clear();


            int TrialsCount = 100;
            int Trajectories = 10;

            double minX = 0;
            double minY = 0;

            double maxX = TrialsCount;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, bmp.Width - 40, bmp.Height - 40);
            g.DrawRectangle(p, VirtualWindow);
            for (int i = 1; i <= Trajectories; i++)
            {
                List<Point> Normalized_Frequency = new List<Point>();
                int success = 0;
                int unsuccess = 0;
                for (int j = 0; j < TrialsCount; j++)
                {
                    int uniform = r.Next(2);
                    if (uniform == 0)
                    {
                        success++;
                        double norm_freq = success * TrialsCount / (Math.Sqrt(j + 1));
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + "Normalized Frequency: " + norm_freq.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }
                    else
                    {
                        unsuccess++;
                        this.richTextBox1.AppendText("Success: " + success.ToString() + " Unsuccess: " + unsuccess.ToString() + "\n");
                        this.richTextBox1.ScrollToCaret();
                    }

                    double nY = success * Math.Sqrt(TrialsCount) / (Math.Sqrt(j + 1));
                    int xNormalizedDevice = FromXRealToXVirtual(j, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    int yNormalizedeDevice = FromYRealToYVirtual(nY, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Normalized_Frequency.Add(new Point(xNormalizedDevice, yNormalizedeDevice));

                }
                g.DrawLines(p4, Normalized_Frequency.ToArray());
            }
            this.pictureBox1.Image = bmp;
        }
    }
}
