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
    public partial class Form3 : Form
    {
        int image_index = 0;
        int check_star = 0;
        Label l = new Label();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string target = textBox1.Text;
            int find = 0;
            foreach (var d in Global.data)
            {
                if (d.acupuncture.Equals(target))
                {
                    find = 1;
                    textBox2.Clear();
                    foreach (string s in d.feature)
                    {
                        textBox2.AppendText(s + "\n");
                    }
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < Global.pos[i].GetUpperBound(0) + 1; j++)
                        {
                            if (Global.pos[i][j].acu.Equals(target))
                            {
                                check_star = 1;
                                l.Location = new Point(Global.pos[i][j].x - 25, Global.pos[i][j].y - 25);
                                pictureBox1.Controls.Add(l);
                                image_index = i;
                                pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
                            }
                        }
                    break;
                }
            }
            if (find == 0)
                MessageBox.Show("Not found!!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 108 || e.KeyChar == 13)
            {
                string target = textBox1.Text;
                int find = 0;
                foreach (var d in Global.data)
                {
                    if (d.acupuncture.Equals(target))
                    {
                        find = 1;
                        textBox2.Clear();
                        foreach (string s in d.feature)
                        {
                            textBox2.AppendText(s + "\n");
                        }
                        for(int i = 0; i < 8; i++)
                            for(int j=0;j< Global.pos[i].GetUpperBound(0) + 1;j++)
                            {
                                if(Global.pos[i][j].acu.Equals(target))
                                {
                                    check_star = 1;
                                    l.Location = new Point(Global.pos[i][j].x - 25, Global.pos[i][j].y - 25);
                                    pictureBox1.Controls.Add(l);
                                    image_index = i;
                                    pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
                                }
                            }
                        break;
                    }
                }
                if (find == 0)
                    MessageBox.Show("Not found!!");
            }
            else if(e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (image_index > 0)
                image_index--;
            else
                image_index = 7;
            pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
            if (check_star == 1)
            { 
                pictureBox1.Controls.Remove(l);
                check_star = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (image_index < 7)
                image_index++;
            else
                image_index = 0;
            pictureBox1.Image = Image.FromFile(@image_index + ".jpg");
            if (check_star == 1)
            {
                pictureBox1.Controls.Remove(l);
                check_star = 0;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            textBox2.AppendText(p.ToString()+"\n");
            string find_acu = null;
            for(int i=0;i<8;i++)
            {
                if (image_index == i)
                {
                    int[] dis = new int[Global.pos[i].GetUpperBound(0) + 1];
                    for (int j = 0; j < Global.pos[i].GetUpperBound(0) + 1; j++)
                    {
                        dis[j] = (Global.pos[image_index][j].x - e.X) * (Global.pos[image_index][j].x - e.X) + (Global.pos[image_index][j].y - e.Y) * (Global.pos[image_index][j].y - e.Y);
                    }
                    for (int k = 0; k < Global.pos[i].GetUpperBound(0) + 1; k++)
                    {
                        if (dis.Min() == dis[k] && dis[k] < 50)
                        {
                            check_star = 1;
                            l.Location = new Point(Global.pos[i][k].x - 25, Global.pos[i][k].y - 25);
                            pictureBox1.Controls.Add(l);
                            find_acu = Global.pos[image_index][k].acu;
                        }
                    }
                }
            }
            
            textBox1.Text = find_acu;
            foreach (var d in Global.data)
            {
                if (d.acupuncture.Equals(find_acu))
                {
                    textBox2.Clear();
                    foreach (string s in d.feature)
                    {
                        textBox2.AppendText(s + "\n");
                    }
                    break;
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            l.Text = "★";
            l.Font = new Font("Bold", 30);
            l.Size = new Size(40,40);
            l.ForeColor = Color.Red;
            l.BackColor = Color.Transparent;
            pictureBox1.Image = Image.FromFile(@"0.jpg");
        }
    }
}
