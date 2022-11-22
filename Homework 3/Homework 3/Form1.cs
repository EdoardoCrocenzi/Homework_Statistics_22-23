using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Homework_3
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> univariateData = new Dictionary<int, int>();
            int index = 4;
            this.richTextBox1.AppendText("Lets calculate the univariate distribution of the packets sent: \n");
            this.richTextBox1.ScrollToCaret();
            using (TextFieldParser parser = new TextFieldParser(@"../../../../wireshark_dataset.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    int key = Int32.Parse(fields[index]);
                    if (univariateData.ContainsKey(key))
                    {
                        univariateData[key]++;
                    }
                    else
                    {
                       univariateData.Add(key, 1);
                    }
                }
            }
            foreach (KeyValuePair<int, int> el in univariateData)
            {
                int pacchetti = el.Value;
                int key = el.Key;
                this.richTextBox1.AppendText(key.ToString() + ": " + pacchetti.ToString() + "\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> univariateData = new Dictionary<int, int>();
            int index = 5;
            this.richTextBox1.AppendText("Lets calculate the univariate distribution of the bytes sent A: \n");
            this.richTextBox1.ScrollToCaret();
            using (TextFieldParser parser = new TextFieldParser(@"../../../../wireshark_dataset.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    int key = Int32.Parse(fields[index]);
                    if (univariateData.ContainsKey(key))
                    {
                        univariateData[key]++;
                    }
                    else
                    {
                        univariateData.Add(key, 1);
                    }
                }
            }
            foreach (KeyValuePair<int, int> el in univariateData)
            {
                int pacchetti = el.Value;
                int key = el.Key;
                this.richTextBox1.AppendText(key.ToString() + ": " + pacchetti.ToString() + "\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }
    }
}
