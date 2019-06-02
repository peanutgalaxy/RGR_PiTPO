using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

 namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int p, buzz = 0, tick = 0;
        int w, h;
        int sw = 100, sh = 68;
        private PictureBox mainpic;
        private object[] tokens;
        private System.Collections.Hashtable tokenstate;
        PictureBox tokenpic1;
        PictureBox tokenpic2;
        string imgstr;

        private void Setup()
        {
            if (textBox1.Text == "5")
            {
                this.Width = 530;
                this.Height = 430;
                tabControl1.Width = 530;
                this.tabControl1.Height= 430;
            }
            if (textBox1.Text == "7")
            {
                this.Width = 730;
                this.Height = 570;
                this.tabControl1.Width = 730;
                this.tabControl1.Height = 570;
            }
            if (textBox1.Text == "9")
            {
                this.Width = 930;
                this.Height = 700;
                this.tabControl1.Width = 930;
                this.tabControl1.Height = 700;
            }
        }

        private void button1_Click(object sender, EventArgs e) => Begin();


        public void Begin()
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                //   MessageBox.Show("Уровень сложности и картинка будут подобраны компьютером");
                textBox1.Text = "5";
                textBox2.Text = "C:\\Users\\Мария\\Pictures\\картинки\\волшебство.jpg";

            }
            Begin2(textBox1.Text, textBox2.Text);
        }
           public int Begin2(string text1, string text2) {  
                try
                {
                    if (Convert.ToInt32(text1) >= 5 && Convert.ToInt32(text1) <= 9)
                    {
                      //  this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        p = Convert.ToInt32(text1);
                        w = sw * p;
                        h = sh * p;
                        tokens = new object[p * p];
                        tokenstate = new System.Collections.Hashtable((p * p));
                        imgstr = text2;
                        Setup();
                        Createtokens();
                        Gettokenpictures();
                        Solvepicutres();
                        tabControl1.SelectTab(tabPage2);
                        Shufflepicutres();
                    return 1;
                    }
                    else
                    {
                        MessageBox.Show("Сложность должна быть в отрезке от 5 до 9");
                        return 2;
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при обработке введенных данных. Проверьте их корректность");
                    return 3;
                
            }
        }

        private void button2_Click(object sender, EventArgs e) => Operation();
        

        public bool Operation()
        {
            try {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "JPG|*.jpg";
                ofd.Multiselect = false;
                ofd.ShowDialog();
                textBox2.Text = ofd.FileName;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (buzz == 0)
            {
                buzz = 1;
                tokenpic1 = (PictureBox)sender;
                tokenpic1.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                tokenpic2 = (PictureBox)sender;
                Swaptag(ref tokenpic1, ref tokenpic2);
                buzz = 0;
                Checksolve();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgstr == null)
            {
                MessageBox.Show("Пожалуйста, начните игру.");
            }
            else
            {
                MessageBox.Show("Цельная картинка покажется на 5 секунд");
                this.tabControl1.SelectTab(tabPage3);
                pictureBox1.Image = Image.FromFile(imgstr);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Width = w;
                pictureBox1.Height = h;
                timer1.Start();
                tick = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick > 5)
            {
                timer1.Stop();
                pictureBox1.Image = null;
                this.tabControl1.SelectTab(tabPage2);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "5";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = "7";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = "9";
            }
        }
        private void Createtokens()
        {
            int index = 0;

            /*Create Tokens*/
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(sw, sh);
                    if (index == ((p * p) - 1))
                    {
                        pic.BorderStyle = BorderStyle.None;
                        pic.BackColor = Color.Transparent;
                    }
                    else
                    {
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.BackColor = Color.Transparent;
                    }
                    pic.Name = string.Format("token{0}", index);
                    pic.Click += new EventHandler(pictureBox1_Click);
                    pic.Tag = index;
                    tokens[index] = pic;
                    index++;
                }
            }
        }
        private void Checksolve()
        {
            bool lose = false;
            for (int i = 0; i < (p * p); i++)
            {
                PictureBox tokenpic = (PictureBox)tokens[i];
                if (Convert.ToInt32(tokenstate[i]) != i)
                    lose = true;
            }
            if (lose == false)
                MessageBox.Show("Вы выиграли!");
        }

    
        private void Swaptag(ref PictureBox A, ref PictureBox B)
        {
            object keeptag = null;
            keeptag = tokenstate[A.Tag];
            tokenstate[A.Tag] = tokenstate[B.Tag];
            tokenstate[B.Tag] = keeptag;
            PictureBox pb = new PictureBox();
            pb.Location = A.Location;
            A.Location = B.Location;
            B.Location = pb.Location;
            A.BorderStyle = BorderStyle.FixedSingle;
            B.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Gettokenpictures()
        {
            mainpic = new PictureBox();
            mainpic.Size = new Size(w, h);
            mainpic.Location = new Point(0, 0);
            Image img = Image.FromFile(imgstr);
            Bitmap bm = new Bitmap(img, w, h);
            mainpic.Image = bm;
            mainpic.SizeMode = PictureBoxSizeMode.StretchImage;
            int top = 0;
            int left = 0;
            int k = 0;
            Bitmap bmp = new Bitmap(mainpic.Image);
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    PictureBox tokenpic = (PictureBox)tokens[k];
                    tokenpic.Image = bmp.Clone(new Rectangle(left, top, sw, sh), System.Drawing.Imaging.PixelFormat.DontCare);
                    left += sw;
                    k++;
                }
                left = 0;
                top += sh;
            }
        }

        private void Solvepicutres()
        {
            tokenstate.Clear();
            this.tabPage2.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rnd = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable((p * p));
            for (int i = 0; i < (p * p); i++)
            {
                PictureBox pic = (PictureBox)tokens[i];
                pic.Location = new Point(left, top);
                tabPage2.Controls.Add(pic);
                tokenstate.Add(pic.Tag, i);
                gen.Add(i, i);
                left += sw;
                if ((i + 1) % p == 0)
                {
                    left = 0;
                    top += sh;
                }
            }
        }

        private void Shufflepicutres()
        {
            tokenstate.Clear();
            this.tabPage2.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rnd = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable((p * p));
            for (int i = 0; i < (p * p); i++)
            {

                int newtoken = rnd.Next(0, (p * p));
                while (gen.Contains(newtoken))
                {
                    newtoken = rnd.Next(0, (p * p));
                }
                PictureBox pic = (PictureBox)tokens[newtoken];
                pic.Location = new Point(left, top);
                this.tabPage2.Controls.Add(pic);
                tokenstate.Add(pic.Tag, i);
                gen.Add(newtoken, newtoken);
                left += sw;
                if ((i + 1) % p == 0)
                {
                    left = 0;
                    top += sh;
                }
            }
        }

    }
}