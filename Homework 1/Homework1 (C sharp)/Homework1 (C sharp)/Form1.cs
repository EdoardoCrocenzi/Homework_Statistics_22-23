namespace Homework1__C_sharp_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "Hello to everyone, this is my first program in VB.net to understand how this language works";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.ForeColor = Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.Yellow;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.richTextBox1.Text = "Congratulation, you've learned about handlers in VB.net";
            }
            else
            {
                this.richTextBox1.Text = "You unchecked the checkbox, so you are not so confident with handlers. Try the button above to understand better";
            }
        }
    }
}