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
    
    public partial class Form6 : Form
    {
        PictureBox[,] dots = new System.Windows.Forms.PictureBox[100, 100];
        Button[] split = new Button[100];
        PictureBox splitLine = new PictureBox();
        int x, y,z, splitWidth, splitPos, former, latter;
        bool buttonCliked = false;
        Label[] number = new Label[100];
        PictureBox check = new PictureBox();
        Timer timer = new Timer();
        SoundPlayer sp;
        public PictureBox Image;
        char symbol; //入力された+,-を格納する
        public Form6()
        {
            InitializeComponent();
            this.Text = "かっこのついたかけざん";
            this.WindowState = FormWindowState.Maximized;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = textBox1.Text;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                return;
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                if (!char.IsNumber(tmp[i]))//文字列のi番目が数字でない時(すなわち記号が入力された時)
                {
                    if(i == 1 && tmp.Length == 4)//2+12のような1桁の数(+,-)2桁の数が入力された時
                    {
                        x = int.Parse(tmp[0].ToString());//最初の数字
                        y = int.Parse(tmp.Substring(2, 2));//i=2,3の文字列を連結して１つの数として変換
                        symbol = tmp[1];//記号を代入
                        //button1.Text = y.ToString();//これなに?
                    }
                    else if (i == 2 && tmp.Length == 4)//12+2のような2桁の数(+,-)1桁の数が入力された時
                    {
                        x = int.Parse(tmp[3].ToString());//i=3の時の文字を数字に変換
                        y = int.Parse(tmp.Substring(0, 2));//i=0,1の時の文字列を連結して１つの数として変換
                        symbol = tmp[2];//記号を代入
                    }
                    else if (i == 1 && tmp.Length == 3)//2+2のような1桁の数(+,-)1桁の数が入力された時
                    {
                        y = int.Parse(tmp[0].ToString());//i=0の時の文字を数字に変換
                        symbol = tmp[1];//記号を代入
                        x = int.Parse(tmp[2].ToString());//i=2の時の文字を数字に変換
                    }
                }
            }
         //   label1.Text = y.ToString() + symbol + x.ToString();
         if(symbol == '+')//記号が+の時
            {
                createDots(x, y);
            }
         else if (symbol == '-')//記号が-の時
            {
                if (y-x<=0)//かっこの内部の値が負になる時
                {
                    label4.Text = "かっこの中の式が今のままだとけいさんできません";//小学生は負の数の概念を学習していないので弾く
                    return;
                }
                createMinusDots(y, x);
            }   
        }
        private void button3Click(object sender, EventArgs e)
        {
            this.Visible = false;

            //タイトル画面を表示
            Form1 f1 = new Form1();
            f1.Show();
        }
        private void removeThings()
        {
            for (int i = 0; i < 100; i++)
            {
                this.Controls.Remove(number[i]);
                for (int j = 0; j < 100; j++)
                {
                    this.Controls.Remove(dots[i, j]);
                }
            }
        }
        private void createDots(int a, int b)
        {
            z = int.Parse(textBox2.Text);
            int heightDistance = (this.Height - 270) / z;
            int widthDistance = this.Width / (a + b);
          //  splitWidth = this.Width / (a + b);
            removeThings();
         //   label6.Text = former.ToString() + "×" + latter.ToString() + "=";
         //   label6.Show();
            for (int i = 0; i < a+b; i++)

            {
                for (int j = 0; j < z; j++)
                {
                    if (j == 0)
                    {
                        number[i] = new Label();
                        number[i].Location = new Point(widthDistance * i + (widthDistance - 10) / 2, 230);
                        number[i].Size = new System.Drawing.Size(40, 20);
                        if(i+1 > a)//xの値を超えた時
                        {
                            number[i].Text = (i + 1 - a).ToString();//もういちど1から加算する
                        }
                        else
                        {
                            number[i].Text = (i + 1).ToString();
                        }
                       
                        number[i].Font = new Font("DefaultFont", 10);
                        this.Controls.Add(number[i]);
                    }

                    dots[i, j] = new PictureBox();
                    //   this.Controls.Remove(dots[i,j]);
                    dots[i, j].Location = new Point(widthDistance * i, j * heightDistance + 250);
                    dots[i, j].Size = new System.Drawing.Size(widthDistance - 10, heightDistance - 10);
                    dots[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    if (i < a)//iがxの値の範疇
                    {
                        dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;//赤りんごを表示
                    }
                    else if (i >= a)//iがyの値の範疇
                    {
                        dots[i, j].BackgroundImage = multiply.Properties.Resources.greenapple;//青リンゴを表示
                    }
                    //    dots[i, j].BackColor = Color.Transparent;

                    this.Controls.Add(dots[i, j]);
                }
            }
        }
        private void createMinusDots(int a, int b)
        {
            z = int.Parse(textBox2.Text);
            int heightDistance = (this.Height - 270) / z;
            int widthDistance = this.Width / a ;
            splitWidth = this.Width / a ;
               removeThings();
            //   label6.Text = former.ToString() + "×" + latter.ToString() + "=";
            //   label6.Show();
            for (int i = 0; i < a; i++)

            {
                for (int j = 0; j < z; j++)
                {
                    if (j == 0)
                    {
                        number[i] = new Label();
                        number[i].Location = new Point(widthDistance * i + (widthDistance - 10) / 2, 230);
                        number[i].Size = new System.Drawing.Size(40, 20);
                        number[i].Text = (i + 1).ToString();
                        number[i].Font = new Font("DefaultFont", 10);
                        this.Controls.Add(number[i]);
                       
                    }

                    dots[i, j] = new PictureBox();
                    //   this.Controls.Remove(dots[i,j]);
                    dots[i, j].Location = new Point(widthDistance * i, j * heightDistance + 250);
                    dots[i, j].Size = new System.Drawing.Size(widthDistance - 10, heightDistance - 10);
                    dots[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    if (i < a-b)//iがy-xの値の範疇の時
                    {
                        dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;//赤リンゴを表示
                    }
                    else if (i >= a-b)//iがy-xの値の範疇でない時
                    {
                        dots[i, j].BackgroundImage = multiply.Properties.Resources.greenapple;//青リンゴを表示
                    }
                    //    dots[i, j].BackColor = Color.Transparent;

                    this.Controls.Add(dots[i, j]);
                }
            }
        }
    }
}

