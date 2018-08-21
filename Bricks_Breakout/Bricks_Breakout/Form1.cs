using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace Bricks_Breakout
{
    public partial class Form1 : Form
    {
        public double sec = 0;
        public int min = 0;
        public int speed_left = 6;
        public int speed_top = 8;
        public int points = 0;
        public int play_or_stop = 0;
        public int index_of_brick_type = 1;
        public int check_starting = 0;
        PictureBox[] pic = new PictureBox[25];
        int[] pos_x = new int[25];
        SoundPlayer backmusic = new SoundPlayer("background.wav");
        SoundPlayer lose = new SoundPlayer("lose.wav");
        SoundPlayer win = new SoundPlayer("win.wav");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            backmusic.PlayLooping();

            timer1.Enabled = false;

            player.Top = playground.Bottom - playground.Bottom / 8; 

            pic[0] = pictureBox1; pic[1] = pictureBox2; pic[2] = pictureBox3;
            pic[3] = pictureBox4; pic[4] = pictureBox5; pic[5] = pictureBox6;
            pic[6] = pictureBox7; pic[7] = pictureBox8; pic[8] = pictureBox9;
            pic[9] = pictureBox10; pic[10] = pictureBox11; pic[11] = pictureBox12;
            pic[12] = pictureBox13; pic[13] = pictureBox14; pic[14] = pictureBox15;
            pic[15] = pictureBox16; pic[16] = pictureBox17; pic[17] = pictureBox18;
            pic[18] = pictureBox19; pic[19] = pictureBox20; pic[20] = pictureBox21;
            pic[21] = pictureBox22; pic[22] = pictureBox23; pic[23] = pictureBox24;
            pic[24] = pictureBox25;
            for(int i=0;i<25;i++)
            {
                pos_x[i] = pic[i].Left;
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            brickbreakout();
            check_if_game_over();

            ball.Left = ball.Left + speed_left;
            ball.Top = ball.Top - speed_top;
            if(ball.Bottom>=player.Top&&ball.Bottom<=player.Bottom&&ball.Left>=player.Left&&ball.Right<=player.Right)
            {
                speed_top = -speed_top;

                Random r = new Random();
                //換背景,球的顏色
                playground.BackColor = Color.FromArgb(r.Next(175, 255), r.Next(175, 255), r.Next(175, 255));
                ball.BackColor = Color.FromArgb(r.Next(1, 150), r.Next(1, 150), r.Next(1, 150));
            }
            if(ball.Left<=playground.Left)
            {
                speed_left = -speed_left;
            }
            else if(ball.Right>=playground.Right)
            {
                speed_left = -speed_left;
            }
            else if(ball.Top<=(playground.Top+10))
            {
                speed_top = -speed_top;
            }
            else if(ball.Bottom>=playground.Bottom)
            {
                restart.Visible = true;
                timer1.Enabled = false;
                lose.Play();
                check_starting = 0;
            }
            sec = sec + 0.1;
            if (sec >= 60)
            {
                sec = 0;
                min++;
            }
            time.Text = min + ":" + (int)sec;
        }
  
        
        private void brickbreakout()
        {
            for(int i=0;i<25;i++)
            {
                if (ball.Left == -pic[i].Width*2) continue;
                if ((ball.Left + ball.Width / 2) >= pic[i].Left && (ball.Left + ball.Width / 2) <= pic[i].Right && (ball.Top + ball.Height / 2) >= pic[i].Top && (ball.Top + ball.Height / 2) <= (pic[i].Top + pic[i].Height))
                {
                    player.Width = player.Width + 5;
                    pic[i].Left = -pic[i].Width*2;
                    points = points + 10;
                    point.Text = points + "";

                    if (ball.Top <= (pic[i].Top + pic[i].Height) && (ball.Top + ball.Height) >= (pic[i].Top + pic[i].Height))
                    {
                        speed_top = -speed_top;
                    }
                    else if ((ball.Top + ball.Height) >= pic[i].Top && ball.Top <= pic[i].Top)
                    {
                        speed_top = -speed_top;
                    }
                    else if (ball.Right >= pic[i].Left && ball.Left <= pic[i].Left)
                    {
                        speed_left = -speed_left;
                    }
                    else if (ball.Left <= pic[i].Right && ball.Right >= pic[i].Right)
                    {
                        speed_left = -speed_left;
                    }
                    else
                    {
                        speed_top = -speed_top;
                    }
                    break;
                }
            }
            
        }

        private void check_if_game_over()
        {
            int ch = 0;
            for(int i=0;i<25;i++)
            {
                if (pic[i].Left == (-pic[i].Width*2))
                    ch++;
            }
            if(ch==25)
            {
                check_starting = 0;
                win.Play();
                timer1.Enabled = false;
                restart.Visible = true;
                point.Text = (int)(points - min * 60 - sec * 1)+"";
                MessageBox.Show("Game Over!!\n\nYou Win (:3/=/_\n\nTime Used : "+min+":"+(int)sec+"\n\nScore : "+(int)(points-min*60-sec*1));
            }
        }

        private void playground_MouseDown(object sender, MouseEventArgs e)
        {
            if (check_starting == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    player.Top = player.Top + 5;
                    player.Top = player.Top + 5;
                    player.Top = player.Top + 5;
                    player.Top = player.Top + 5;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    timer1.Enabled = true;
                }
            }
        }

        private void playground_MouseUp(object sender, MouseEventArgs e)
        {
            if (check_starting == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    player.Top = player.Top - 5;
                    player.Top = player.Top - 5;
                    player.Top = player.Top - 5;
                    player.Top = player.Top - 5;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    ;
                }
            }
        }

        private void playground_MouseMove(object sender, MouseEventArgs e)
        {
            player.Left = e.X - player.Width / 2;
        }

        private void fast_KeyDown(object sender, KeyEventArgs e)
        {
            if (start.Visible == false)
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                else if (e.KeyCode == Keys.F5)
                {
                    player.Width = 150;
                    slow.Enabled = true;
                    medium.Enabled = true;
                    fast.Enabled = true;
                    backmusic.PlayLooping();
                    ball.Top = 450;
                    ball.Left = 100;
                    sec = 0;
                    min = 0;
                    points = 0;
                    point.Text = points + "";
                    timer1.Enabled = false;
                    start.Visible = true;
                    playground.BackColor = Color.Cyan;
                    restart.Visible = false;
                    colorbar.Visible = true;
                    for (int i = 0; i < 25; i++)
                    {
                        pic[i].Left = pos_x[i];
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (play_or_stop == 0)
                    {
                        pause.Text = "Pauseing...";
                        play_or_stop = 1;
                        timer1.Enabled = false;
                    }
                    else
                    {
                        pause.Text = "Press space to Pause the game :3";
                        play_or_stop = 0;
                        timer1.Enabled = true;
                    }
                }
            }
            if (e.KeyCode == Keys.N)
            {
                if (index_of_brick_type < 7) index_of_brick_type++;
                else index_of_brick_type = 1;
                for (int i = 0; i < 25; i++)
                {
                    pic[i].Image = Image.FromFile("brick" + index_of_brick_type + ".jpg");
                }
            }
            if (e.KeyCode == Keys.S && start.Visible == true)
            {
                check_starting = 1;
                slow.Enabled = false;
                medium.Enabled = false;
                fast.Enabled = false;
                start.Visible = false;
                colorbar.Visible = false;
                if (slow.Checked == true)
                {
                    speed_left = 4;
                    speed_top = 6;
                }
                else if (medium.Checked == true)
                {
                    speed_left = 8;
                    speed_top = 10;
                }
                else if (fast.Checked == true)
                {
                    speed_left = 12;
                    speed_top = 14;
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.BackColor = Color.FromArgb(Math.Abs(colorbar.Value-80), Math.Abs(colorbar.Value-10), Math.Abs(colorbar.Value-200));
        }

        private void colorbar_KeyDown(object sender, KeyEventArgs e)
        {
            if (start.Visible == false)
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close(); 
                else if (e.KeyCode == Keys.F5)
                {
                    player.Width = 150;
                    backmusic.PlayLooping();
                    slow.Enabled = true;
                    medium.Enabled = true;
                    fast.Enabled = true;
                    ball.Top = 450;
                    ball.Left = 100;
                    sec = 0;
                    min = 0;
                    points = 0;
                    point.Text = points + "";
                    timer1.Enabled = false;
                    start.Visible = true;
                    playground.BackColor = Color.Cyan;
                    restart.Visible = false;
                    colorbar.Visible = true;
                    for (int i = 0; i < 25; i++)
                    {
                        pic[i].Left = pos_x[i];
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (play_or_stop == 0)
                    {
                        pause.Text = "Pauseing...";
                        play_or_stop = 1;
                        timer1.Enabled = false;
                    }
                    else
                    {
                        pause.Text = "Press space to Pause the game :3";
                        play_or_stop = 0;
                        timer1.Enabled = true;
                    }
                }
            }
            if (e.KeyCode == Keys.N)
            {
                if (index_of_brick_type < 7) index_of_brick_type++;
                else index_of_brick_type = 1;
                for (int i = 0; i < 25; i++)
                {
                    pic[i].Image = Image.FromFile("brick" + index_of_brick_type + ".jpg");
                }
            }
            if (e.KeyCode == Keys.S && start.Visible == true)
            {
                check_starting = 1;
                slow.Enabled = false;
                medium.Enabled = false;
                fast.Enabled = false;
                start.Visible = false;
                colorbar.Visible = false;
                if (slow.Checked == true)
                {
                    speed_left = 4;
                    speed_top = 6;
                }
                else if (medium.Checked == true)
                {
                    speed_left = 8;
                    speed_top = 10;
                }
                else if (fast.Checked == true)
                {
                    speed_left = 12;
                    speed_top = 14;
                }
            }
        }

    }
}
