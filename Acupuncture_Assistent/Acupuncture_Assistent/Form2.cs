using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acupuncture_Assistent
{
    public partial class Form2 : Form
    {
        int image_index = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            string target = textBox1.Text;
            int find = 0, k = 0;
            string[] related_acu = new string[1];
            foreach(var d in Global.data)
            {
                foreach(string s in d.feature)
                {
                    if(s.Equals(target))
                    {
                        related_acu[k++] = d.acupuncture;
                        Array.Resize(ref related_acu, related_acu.Length + 1);
                        find = 1;
                    }
                }
            }
            Array.Resize(ref related_acu, related_acu.Length - 1);
            if (find == 1)
            {
                foreach (string t in related_acu)
                {
                    textBox2.AppendText(t + "\n");
                }
            }
            else
                MessageBox.Show("Not found!!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.Clear();
            if (e.KeyChar == 108 || e.KeyChar == 13)
            {
                string target = textBox1.Text;
                int find = 0, k = 0;
                string[] related_acu = new string[1];
                foreach (var d in Global.data)
                {
                    foreach (string s in d.feature)
                    {
                        if (s.Equals(target))
                        {
                            related_acu[k++] = d.acupuncture;
                            Array.Resize(ref related_acu, related_acu.Length + 1);
                            find = 1;
                        }
                    }
                }
                Array.Resize(ref related_acu, related_acu.Length - 1);
                if (find == 1)
                {
                    foreach (string t in related_acu)
                    {
                        textBox2.AppendText(t + "\n");
                    }
                }
                else
                    MessageBox.Show("Not found!!");
            }
            else if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"0.jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (image_index > 0)
                image_index--;
            else
                image_index = 7;
            pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (image_index < 7)
                image_index++;
            else
                image_index = 0;
            pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Point p = new Point(e.X, e.Y);
            //textBox2.AppendText(p.ToString()+"\n");
        }
    }
}
