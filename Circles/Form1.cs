using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace Circles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            LoadForm temp = new LoadForm();
            temp.ShowDialog();
            //Thread.Sleep(3000);
            temp.Close();
            InitializeComponent();
            gr = CreateGraphics();
            gr.Clear(Color.White);
        }
        private Circle circle;
        private Color backgroundColor = Color.White;
        private Color color = Color.Black;
        static Graphics gr;
        private int sizeofround = 100;
        

        private void drawButton_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < sizeofround; i++)
            {
                circle = new Circle(ClientSize.Width, ClientSize.Height, i);
                circle.X = 50;
                circle.Y = 50;

                circle.Draw(gr, color);
                if (checkBox1.Checked)
                {
                    Thread.Sleep(10);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gr.Clear(backgroundColor);
        }

        private void drawOnClick(object sender, MouseEventArgs e)
        {
            for(int i = 0;i < sizeofround; i++)
            {
                circle = new Circle(ClientSize.Width, ClientSize.Height, i);
                circle.X = e.X - 450;
                circle.Y = e.Y - 250;
                

                circle.Draw(gr, color);
                
                if (checkBox1.Checked)
                {
                    Thread.Sleep(10);
                }
            }
        }

        private void изменитьЦветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                backgroundColor = dialog.Color;
            }
        }

        private void поменятьЦветToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color;
            }
        }

        private void изменитьДиаметрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeForm sizeform = new SizeForm();
            sizeform.ShowDialog();
            sizeofround = sizeform.sizeOfPenAttr;
        }

        private void сохранитьВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "text files(*.txt;*.docx)|*.txt;*.docx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                        FileUtil.PutInWordFile(openFileDialog1.FileName, new string[] { " cirlce x: " + circle.X, ", circle y: " + circle.Y, ", radius: " + sizeofround , ", " + color.ToString()});                        
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void сохранитьВExcelфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "text files(*.xls;*.xlsx)|*.xlsx;*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileUtil.PutInExcelFile(openFileDialog1.FileName, new string[] { " cirlce x: " + circle.X, ", circle y: " + circle.Y, ", radius: " + sizeofround, ", " + color.ToString() });

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
