using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace Csv_parse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TextFieldParser parser = new TextFieldParser("C:/Users/Edoar/Desktop/Università/MAGISTRALE/Primo Anno/Primo Semestre/Statistics/Homework 2/Statistics_students_dataset_22_23 - Foglio1.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach(string field in fields)
                    {
                        this.richTextBox1.AppendText(field + " ");
                    }
                    this.richTextBox1.AppendText("\n");
                    this.richTextBox1.ScrollToCaret();
                }   
            }
            
        }
    }
}