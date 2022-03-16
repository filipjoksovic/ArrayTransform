using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArrayTransorm
{
    public partial class Form1 : Form
    {
        double[] parsedArray;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                description.Text = "Mapiranjem niza ogranicavamo vrednosti svih elemenata niza u raspon od 0 do 1.";
                if (parsedArray != null && !listBox1.Items.Contains("Standardna devijacija"))
                {
                    double[] mappedArray = Magic.MapArray(parsedArray, 0, 1);
                    listBox1.Items.Add("Mapiran niz");
                    foreach (double val in mappedArray)
                    {
                        listBox1.Items.Add(val.ToString());
                    }
                }
            }
            if (comboBox1.SelectedIndex == 1 && !listBox1.Items.Contains("Standardna devijacija"))
            {
                description.Text = "Ovom opcijom svaki podatak niza se transformise oduzimanjem srednje vrednosti niza, a zatim se deli standardnom devijacijom niza.";
                if (parsedArray != null && !listBox1.Items.Contains("Standardna devijacija"))
                {
                    double standardDeviation = Magic.CalculateStandardDeviation(parsedArray);
                    listBox1.Items.Add("Standardna devijacija");
                    listBox1.Items.Add(standardDeviation.ToString());
                }
            }
            groupBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Tekstualni fajl (*.txt)|*.txt";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;

                    var fileStream = openFileDialog1.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        String[] parsedContent = Magic.ParseFile(fileContent);
                        listBox1.Items.Clear();
                        listBox1.Items.AddRange(parsedContent.ToArray());
                        parsedArray = Magic.ToArray(parsedContent);
                        if (comboBox1.SelectedIndex == 0)
                        {
                            double[] mappedArray = Magic.MapArray(parsedArray, 0, 1);
                            listBox1.Items.Add("Mapiran niz");
                            foreach (double val in mappedArray)
                            {
                                listBox1.Items.Add(val.ToString());
                            }
                        }
                        if (comboBox1.SelectedIndex == 1)
                        {
                            double standardDeviation = Magic.CalculateStandardDeviation(parsedArray);
                            listBox1.Items.Add("Standardna devijacija");
                            listBox1.Items.Add(standardDeviation.ToString());
                        }
                    }
                }
                groupBox2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "CSV file (*.csv)|*.csv";
            saveFileDialog1.Title = "Eksportovanje podataka";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")

            {
                List<String> content = new List<String>();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    content.Add(listBox1.Items[i].ToString());
                }
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.WriteLine(Magic.JoinArray(parsedArray));
                writer.Flush();
                writer.WriteLine(Magic.JoinArray(Magic.MapArray(parsedArray, 0, 1)));
                writer.WriteLine(Magic.CalculateStandardDeviation(parsedArray));
                writer.Dispose();
                writer.Close();
                MessageBox.Show("Podaci uspesno eksportovani");
            }
            else {
                MessageBox.Show("Doslo je do greske prilikom eksportovanja podataka");
            }

        }
    }
}
