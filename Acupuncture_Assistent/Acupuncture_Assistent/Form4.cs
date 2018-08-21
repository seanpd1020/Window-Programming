using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using SpeechLib;
using System.Threading;

namespace Acupuncture_Assistent
{
    public partial class Form4 : Form
    {
        int playing = 0;
        int score = 0;
        int question_index = 0;
        int image_index = 0;
        int result = 0;
        Random r = new Random();
        SpVoiceClass voice = new SpVoiceClass();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pictureBox1.BringToFront();
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);//Item(0)中文女聲
            voice.Volume = 100;
            voice.Speak("即將開始救援黃秋儀", SpeechVoiceSpeakFlags.SVSFDefault);
            timer1.Interval = 2000;
            timer1.Start();
            playing = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (image_index > 0)
                image_index--;
            else
                image_index = 7;
            pictureBox3.Image = Image.FromFile(@image_index + ".jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (image_index < 7)
                image_index++;
            else
                image_index = 0;
            pictureBox3.Image = Image.FromFile(@image_index + ".jpg");
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            string find_acu = null;
            for (int i = 0; i < 8; i++)
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
                            find_acu = Global.pos[image_index][k].acu;
                            break;
                        }
                        else
                            find_acu = "notfound";
                    }
                }
            }
            textBox1.AppendText(find_acu + "\n");
            int check = 0;
            foreach (var d in Global.data)
            {
                if (d.acupuncture.Equals(find_acu))
                {
                    foreach (string s in d.feature)
                    {
                        textBox1.AppendText(s + "\n");
                        if(label1.Text.Equals(s))
                        {
                            check = 1;
                        }
                    }
                    break;
                }
            }

            if (question_index <= 10 && find_acu != "notfound")
            {
                int k = r.Next(3);
                if (check == 1)
                {
                    pictureBox1.Image = Image.FromFile("a" + (r.Next(3) + 1).ToString() + ".gif");
                    pictureBox1.Location = new Point(0, 0);
                    pictureBox1.Size = new Size(900, 700);
                    score += 10;
                    if(k == 0)
                        voice.Speak("舒服舒服好舒服", SpeechVoiceSpeakFlags.SVSFDefault);
                    else if(k==1)
                        voice.Speak("對，就是這裡阿阿", SpeechVoiceSpeakFlags.SVSFDefault);
                    else if(k==2)
                        voice.Speak("專業的九四不一樣", SpeechVoiceSpeakFlags.SVSFDefault);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("b" + (r.Next(3) + 1).ToString() + ".gif");
                    pictureBox1.Location = new Point(0, 0);
                    pictureBox1.Size = new Size(900, 700);
                    
                    if(k==0)
                        voice.Speak("安捏母湯喔", SpeechVoiceSpeakFlags.SVSFDefault);
                    else if(k==1)
                        voice.Speak("加油好嗎", SpeechVoiceSpeakFlags.SVSFDefault);
                    else if(k==2)
                        voice.Speak("我對你很失望", SpeechVoiceSpeakFlags.SVSFDefault);
                }
                label3.Text = "Score : " + score;
                button3.Visible = true;
                button1.Visible = false;
                button2.Visible = false;
                pictureBox3.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (playing == 0)
                playing = 1;
            if (button3.Text != "Next")
                button3.Text = "Next";
            if (playing == 1)
            {
                if (question_index < 10)
                {
                    button3.Visible = false;
                    button1.Visible = true;
                    button2.Visible = true;
                    pictureBox3.Visible = true;
                    label2.Text = "Question " + ++question_index;
                    pictureBox1.Location = new Point(-11, 520);
                    pictureBox1.Size = new Size(350, 270);
                    pictureBox1.Image = Image.FromFile("不舒服.gif");
                    label1.Text = Global.question[r.Next(826)];
                    voice.Speak(label1.Text, SpeechVoiceSpeakFlags.SVSFDefault);
                }
                else if (question_index == 10)
                {
                    question_index--;
                    label2.Text = "Question " + ++question_index;
                    playing = 0;
                    voice.Speak("遊戲結束了喔", SpeechVoiceSpeakFlags.SVSFDefault);
                    if (score <= 30)
                    {
                        pictureBox1.Image = Image.FromFile("八字不合.jpg");
                        result = 1;
                    }
                    else if (score <= 70)
                    {
                        pictureBox1.Image = Image.FromFile("是我太傻了.jpg"); result = 2;
                        result = 2;
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile("美好回憶.jpg"); result = 3;
                        result = 3;
                    }
                    Thread.Sleep(3000);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (question_index == 0 && playing == 1)
            {
                label1.Text = Global.question[r.Next(826)];
                voice.Speak(label1.Text, SpeechVoiceSpeakFlags.SVSFDefault);
                label2.Text = "Question " + ++question_index;
            }
            if (result != 0)
            {
                if (result == 1)
                {
                    voice.Speak("我們八字不合，給我滾！", SpeechVoiceSpeakFlags.SVSFDefault);
                }
                else if (result == 2)
                {
                    voice.Speak("是我太傻了才會相信你...", SpeechVoiceSpeakFlags.SVSFDefault);
                }
                else if (result == 3)
                {
                    voice.Speak("恩加搞，你讓我留下了美好的回憶", SpeechVoiceSpeakFlags.SVSFDefault);
                }
                result = 0;
                score = 0;
                label3.Text = "Score : " + score;
                question_index = 0;
                button3.Text = "Play Again";
            }
        }
    }
}
