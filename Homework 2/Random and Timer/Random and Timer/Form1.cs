namespace Random_and_Timer
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        int value = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "Hi player, welcome to even and odd's game. The rules are very simple: every time you have to click the \"Random Value\" button to generate a random value. Then you have 3 second to guess if its an odds or even number. When you are ready, click the button.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            value = rand.Next(1,100);
            this.richTextBox1.Text = value.ToString();
            this.timer1.Start();
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = "I'm sorry, are you a human or a snail?";
            this.timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(value % 2 == 0)
            {
                this.richTextBox1.Text = "Great job!";
            }
            else
            {
                this.richTextBox1.Text = "No, that is not correct";
            }
            this.timer1.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (value % 2 != 0)
            {
                this.richTextBox1.Text = "Great job!";
            }
            else
            {
                this.richTextBox1.Text = "No, that is not correct";
            }
            this.timer1.Stop();
        }
    }
}