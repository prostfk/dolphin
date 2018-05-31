
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
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
            temp.Close();
            InitializeComponent();
            gr = CreateGraphics();
            circle = new Circle(ClientSize.Width, ClientSize.Height);
            //bitmap = new Bitmap(Width, Height);
            //gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);
            //backTread = new Thread(drawByDeafault);
            circlesList = new List<Circle>();
            fullCirclesList = new List<Circle>();
            
        }
        private List<Circle> circlesList, fullCirclesList;
        private Circle circle;
        private Color backgroundColor = Color.White;
        private Color color = Color.Black;
        static Graphics gr;
        private Bitmap bitmap;
        private int sizeofround = 100;
        private Thread thread, backTread;

        private void drawByDeafault()
        {
            
            while (true)
            {
                Random random = new Random((int)DateTime.Now.Ticks);

                lock (backTread)
                {
                    draw(random.Next(-500, 500), random.Next(-500, 500));
                    Thread.Sleep(1);
                }
            }
        }

        private  void drawButton_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                    thread.Join();
                    
                }
            }
            thread = new Thread(drawAll);
            thread.Start();
        }

        private void drawAll()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int x, y;

            for (int i = -20; i < 10; i++)
            {
                x = random.Next(-500, 500);
                y = random.Next(-500, 500);
                draw(x, y);
                circlesList.Add(circle);
                fullCirclesList.Add(circle);
            }

        }

        private void draw(int x, int y)
        {
            
            for (int i = 0; i < sizeofround; i++)
            {
                circle = new Circle(ClientSize.Width, ClientSize.Height, i);
                circle.X = x;
                circle.Y = y;
                circle.Draw(gr, color);
                fullCirclesList.Add(circle);
                if (checkBox1.Checked)
                {
                    Thread.Sleep(10);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            circlesList = new List<Circle>();
            fullCirclesList = new List<Circle>();
            lock (gr)
            {
                gr.Clear(backgroundColor);
            }
        }

        private void drawFromConfigFile(Circle fileCircle)
        {
            circle = new Circle(ClientSize.Width, ClientSize.Height, fileCircle.Diameter);
            circle.CloneParams(fileCircle);
            circle.Draw(gr, fileCircle.PenColor);
        }

        private void drawOnClick(object sender, MouseEventArgs e)
        {
           
            for(int i = 0;i < sizeofround; i++)
            {
                circle = new Circle(ClientSize.Width, ClientSize.Height, i);
                circle.X = e.X - 450;
                circle.Y = e.Y - 250;
                circle.Draw(gr, color);
                fullCirclesList.Add(circle);
                if (checkBox1.Checked)
                {
                    Thread.Sleep(10);
                }
            }

            circlesList.Add(circle);
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
            if (circlesList.Count == 0)
            {
                MessageBox.Show("Ваш холст пустой. Нечего добавить в файл");
                return;
            }
            var str = new List<string>();
            foreach(var i in circlesList)
            {
                str.Add(i.ToString());
            }
            FileUtil.PutInWordFile(str.ToArray());

        }

        private void сохранитьВExcelфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (circlesList.Count == 0)
            {
                MessageBox.Show("Ваш холст пустой. Нечего добавить в файл");
                return;
            }
            var list = new List<string[]>();
            for(int i = 0; i < circlesList.Count; i++)
            {
                list.Add(circlesList[i].StringForExcel());
            }
            FileUtil.PutInExcelFile(list);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm form = new HelpForm();
            form.ShowDialog();
        }

        private void savePic()
        {
            

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "All filies (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(Width, Height);

                gr.DrawImage(bitmap, this.Width, this.Height);
                bitmap.Save(dialog.FileName);
            }
        }

        private void сохранитьКартинкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePic();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("helper_Help.exe");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //drawByDeafault();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                thread.Abort();
                thread.Join();
            }catch (Exception) { }
            try
            {
                backTread.Abort();
                backTread.Join();
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "All filies (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var templist = new List<Circle>();
                var crls = circle.DeserializeObjects(dialog.FileName);
                templist.AddRange(crls);
                foreach(var i in templist)
                {
                    drawFromConfigFile(i);

                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "All filies (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                circle.SerializeObjects(dialog.FileName, fullCirclesList.ToArray());

            }
            

        }
    }
}
