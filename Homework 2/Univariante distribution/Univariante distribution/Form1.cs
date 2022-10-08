using Microsoft.VisualBasic.FileIO;

namespace Univariante_distribution
{
    public partial class Form1 : Form
    {
        int[] array1 = { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
        int[] array2 = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.AppendText("Lets calculate the univariate distribution of the students' age: \n");
            this.richTextBox1.ScrollToCaret();
            using (TextFieldParser parser = new TextFieldParser("C:/Users/Edoar/Desktop/Università/MAGISTRALE/Primo Anno/Primo Semestre/Statistics/Homework 2/Statistics_students_dataset_22_23 - Foglio1.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] header = parser.ReadFields();
                int index = 0;
                for(int i = 0; i < header.Length; i++)
                {
                    if (header[i] == "Age")
                    {
                        index = i;
                    }
                }
                    while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    for(int i = 0; i < array1.Length; i++)
                    {
                        if (array1[i] == Int32.Parse(fields[index]))
                        {
                            array2[i]++;
                        }
                    }
                }
            }
            for(int i = 0; i < array1.Length; i++)
            {
                int eta = array1[i];
                int numero = array2[i];
                this.richTextBox1.AppendText(eta.ToString() + ": " + numero.ToString() + "\n");
            }
        }
    }
}