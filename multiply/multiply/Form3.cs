using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multiply
{
    public partial class Form3 : Form
    {
        PictureBox[,] dots = new System.Windows.Forms.PictureBox[100, 100];//りんごを格納する配列
        Button[] split = new Button[100];//分割線を表示するためのボタンを格納する配列
        PictureBox splitLine = new PictureBox();//分割する線
        int x, y, splitWidth, splitPos,former,latter;
        /*x,y:入力された式のかけられる数、かける数 
        splitWidth:かけられる数の数から算出した分割する線を表示する間隔
        splitPos:分割線を表示するために押されたボタンが左から何番目の箇所かを示す
        former,latter:以前の仕様でかけられる数かける数を保持しておくために使用
        */

        Label[] number = new Label[100];//りんご上部に表示する数字を格納する配列
        PictureBox check = new PictureBox();//正答が誤答がを表示する
        Timer timer = new Timer();//表示時間を決める
        SoundPlayer sp;//正答、誤答時にならすSE
        public Form3()
        {
            InitializeComponent();
            this.Text = "いろんな式を分けてみよう";//formのタイトルを設定
            this.WindowState = FormWindowState.Maximized;//画面を最大化
            hideThings();//この時点で表示されないものを隠す
            label6.Location = new Point(button1.Location.X + 300, 23);
            label7.Hide();
            label8.Hide();
            textBox9.Hide();
            textBox10.Hide();
            textBox11.Location = new Point(label6.Location.X + 250, 23);
            button2.Location = new Point(textBox11.Location.X + 150, 0);
            
        }
        private void button1_Click(object sender, EventArgs e)//"計算する!"のボタンが押された時に起動
        {
            int tmp = 0, tmp2 = 0;
            if (Int32.TryParse(textBox1.Text, out tmp) && Int32.TryParse(textBox1.Text, out tmp2))//テキストボックスに入力された値が整数であることを確かめ、tmp,tmp2に代入
            {
                if(textBox1.Text == "" || textBox2.Text == "")//入力がない時は上のif文では弾けないからここで弾く 
                {
                    return;
                }
                tmp = int.Parse(textBox1.Text);
                tmp2 = int.Parse(textBox2.Text);
                former = tmp;
                latter = tmp2;
                y = tmp2; x = tmp;//かける数、かけられる数の代入
                createDots(tmp, tmp2);//リンゴを表示するための関数を呼び出す

            }
            else
            {
                return;
            }
        }
        
        private void splitClick(object sender, EventArgs e)//分割するためのボタンが押された時に起動する
        {
            splitPos = ((((Button)sender).Left+10) / splitWidth);//分割するための線を表示するための座標を取得
            splitLine.BackgroundImageLayout = ImageLayout.Stretch;
            splitLine.BackgroundImage = multiply.Properties.Resources.line;            
            splitLine.Location = ((Button)sender).Location;
            this.Controls.Add(splitLine);
            splitLine.Size = new Size(((Button)sender).Width, this.Height - 70);
            splitLine.BringToFront();
            replaceApple();//りんごを青リンゴと赤りんごの２つの色に変化させるもの
            showThings();//各領域上に領域のリンゴの数を求める式を入力するためのテキストボックスなどを表示する
            textBox3.Location = new Point(splitPos * splitWidth / 3, 150);//領域の上部に表示されるように座標を調整
            label2.Location = new Point(textBox3.Location.X + 60, 150);
            textBox4.Location = new Point(label2.Location.X + 70, 150);
            label3.Location = new Point(textBox4.Location.X + 75, 150);
            textBox5.Location = new Point(label3.Location.X + 50, 150);
            if ((y - splitPos) * splitWidth > 600)//分割箇所によっては式が画面外に出てしまうので調節
            {
                textBox6.Location = new Point(splitPos * splitWidth + (y - splitPos) * splitWidth / 3, 150);
            }
            else
            {
                textBox6.Location = new Point(this.Width-500, 150);
            }
           
            label4.Location = new Point(textBox6.Location.X + 60, 150);
            textBox7.Location = new Point(label4.Location.X + 70, 150);
            label5.Location = new Point(textBox7.Location.X + 75, 150);
            textBox8.Location = new Point(label5.Location.X + 50, 150);
        }
        private void hideThings()//画面に表示されないように隠す
        {
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            textBox7.Hide();
            textBox8.Hide();
            textBox11.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
        }
        private void showThings()//画面に表示させる
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox11.Text = "";
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            textBox6.Show();
            textBox7.Show();
            textBox8.Show();
            textBox11.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
        }
        private void replaceApple()//りんごを分割領域によって赤リンゴと青リンゴの２つの色に分ける
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < splitPos; j++)
                {
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = splitPos; j < y; j++)
                {
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.greenapple;
                }
            }
        }
        private void button2Click(object sender, EventArgs e)//"答え合わせをする"ボタンが押された時に起動
        {

            if(textBox11.Text != (x * y).ToString())//最終的な答えが一致していない                       
            {
                tellFalse();
                return;
            }
            if(textBox5.Text != (x*splitPos).ToString())//赤リンゴの数と一致しない
            {
                tellFalse();
                return;
            }
            if ( textBox8.Text != (x * (y - splitPos)).ToString())//青リンゴの数と一致しない
            {
                tellFalse();
                return;
            }
            sp = new SoundPlayer(Properties.Resources.correct1);
            sp.Play();
            showAnswer(true);

        }
        private void tellFalse()//誤答の時に呼び出される関数
        {
            sp = new SoundPlayer(Properties.Resources.miss);
            sp.Play();
            showAnswer(false);
        }
        private void showAnswer(Boolean n)//正答か誤答かを示す図を選択して表示する。nがtrueの時正答
        {
            check.BackgroundImageLayout = ImageLayout.Stretch;
            if (n)
            {
                check.BackgroundImage = multiply.Properties.Resources.correctframe;
            }
            else
            {
                check.BackgroundImage = multiply.Properties.Resources.incorrectframe;
            }

            // splitLine.BackColor = Color.Black;
            check.Location = new Point(this.Width / 5, this.Height / 4);
            this.Controls.Add(check);
            check.Size = new Size(1200, 400);

            check.BringToFront();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(erase);//5000msの後削除させる
            timer.Enabled = true;
        }
        private void erase(object sender, EventArgs e)
        {
            this.Controls.Remove(check);
            timer.Enabled = false;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3Click(object sender, EventArgs e)//タイトルへ戻るのボタンが押された時に起動
        {
            this.Visible = false;

            //タイトル画面を表示
            Form1 f1 = new Form1();
            f1.Show();
        }
        private void removeThings()//入力されたもの以外を初期化する時に呼び出す
        {
            hideThings();
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            this.Controls.Remove(splitLine);
           // label3.Text = "";
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i == 0)
                    {

                        this.Controls.Remove(number[j]);
                    }
                    if (i == 0 && j != 100 - 1)
                    {

                        this.Controls.Remove(split[j]);
                    }
                    this.Controls.Remove(dots[i, j]);
                }
            }
        }
        private void createDots(int a, int b)
        {
            int heightDistance = (this.Height - 270) / a;//りんごの上下の間隔
            int widthDistance = this.Width / b;//りんごの左右の間隔
            splitWidth = this.Width / b;//分割する線を表示する時の間隔   
            removeThings();//初期化
            label6.Text = former.ToString() + "×" + latter.ToString() + "=";
            label6.Show();
            label6.SendToBack();
            for (int i = 0; i < a; i++)
       
            {
                for (int j = 0; j < b; j++)
                {
                    if (i == 0)
                    {
                        number[j] = new Label();
                        number[j].Location = new Point(widthDistance * j + (widthDistance - 10) / 2, 230);
                        number[j].Size = new System.Drawing.Size(40, 20);
                        number[j].Text = (j + 1).ToString();
                        number[j].Font = new Font("DefaultFont", 10);
                        this.Controls.Add(number[j]);
                    }
                    if (i == 0 && j != b - 1)
                    {
                        split[j] = new Button(); 
                        split[j].Location = new Point(widthDistance * (j+1)-10, 230);
                        split[j].Size = new System.Drawing.Size(20, 20);
                        split[j].BackgroundImageLayout = ImageLayout.Zoom;
                        split[j].BackgroundImage = multiply.Properties.Resources.arrow;
                        split[j].Click += new EventHandler(splitClick);
                        this.Controls.Add(split[j]); 
                    }
                    dots[i, j] = new PictureBox();
                    dots[i, j].Location = new Point(widthDistance * j, i * heightDistance + 250);
                    dots[i, j].Size = new System.Drawing.Size(widthDistance-10, heightDistance-10);
                    dots[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;

                    this.Controls.Add(dots[i, j]);
                }
            }
        }
    }
}

