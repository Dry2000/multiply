using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multiply
{
    public partial class Form2 : Form
    {
        PictureBox[,] dots = new System.Windows.Forms.PictureBox[100, 100];//りんごを格納する配列
        Button[] split = new Button[100];//分割線を表示するためのボタンを格納する配列
        PictureBox splitLine = new PictureBox();//分割する線
        Label[] number = new Label[100];//りんご上部に表示する数字を格納する配列
        int x, y, splitWidth, splitPos;/*x,y:入力された式のかけられる数、かける数 
        splitWidth:かけられる数の数から算出した分割する線を表示する間隔
        splitPos:分割線を表示するために押されたボタンが左から何番目の箇所かを示す
        */
       
        public Form2()
        {
            InitializeComponent();
            this.Text = "かけざんをわけてみよう";//formのタイトルを設定
            this.WindowState = FormWindowState.Maximized;//画面を最大化
            label3.Font = new Font(label3.Font.Name,40,label3.Font.Style); 
            label3.Text = "左の四角にかける数を、右の四角にかけられる数をいれてね";
            label3.Location = new Point(button1.Location.X + 120, 23);//各工程で指示を出すためのlabelの初期設定
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int tmp = 0, tmp2 = 0;
            if (Int32.TryParse(textBox1.Text, out tmp) && Int32.TryParse(textBox1.Text, out tmp2))//テキストボックスに入力された値が整数であることを確かめ、tmp,tmp2に代入
            {
                if (textBox1.Text == "" || textBox2.Text == "")//入力がない時は上のif文では弾けないからここで弾く                
                {
                    return;
                }
                tmp = int.Parse(textBox1.Text);
                tmp2 = int.Parse(textBox2.Text);//if文のところで代入してるのになんで再代入してるんだこれ
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
            splitPos = ((((Button)sender).Left+10) / splitWidth);//押されたボタンが左から何番目かを示す
            replaceApple();//りんごを青リンゴと赤りんごの２つの色に変化させるもの
            splitLine.BackgroundImageLayout = ImageLayout.Stretch;
           splitLine.BackgroundImage = multiply.Properties.Resources.line;
            splitLine.BackColor = Color.Transparent;
            splitLine.Location = ((Button)sender).Location;
            splitLine.Size = new Size(((Button)sender).Width, this.Height - 70);
            label1.Visible = true;
            label2.Visible = true;//各領域上に領域のリンゴの数を求める式を表示する
            this.Controls.Add(splitLine);
            splitLine.BringToFront();
            label3.Text = x.ToString()+"×"+y.ToString()+"="+(x*y).ToString() ;
            label3.Font = new Font("DefaultFont", 50);
            label3.Location = new Point(button1.Location.X + 120, 23);
            label1.Text = x.ToString() + "×" + splitPos.ToString() + "=" + (x * splitPos).ToString() ;
            label1.Font = new Font("DefaultFont", 50);
            label1.Location = new Point(splitPos*splitWidth/3, 150);//領域の上部に表示されるように座標を調整
            if ((y - splitPos) * splitWidth > 600)//分割箇所によっては式が画面外に出てしまうので調節
            {
                label2.Location = new Point(splitPos * splitWidth + (y - splitPos) * splitWidth / 3, 150);
            }
            else
            {
                label2.Location = new Point(this.Width-400, 150);
            }
            label2.Text =  x.ToString() + "×" + (y - splitPos).ToString() + "=" + (x * (y - splitPos)).ToString();
            label2.Font = new Font("DefaultFont", 50);
            
            this.Controls.Add(label3);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3Click(object sender, EventArgs e)//タイトルへ戻るのボタンが押された時に起動
        {
            this.Visible = false;

            //タイトル画面を表示
            Form1 f1 = new Form1();
            f1.Show();
        }
        private void replaceApple()//りんごを分割領域によって赤リンゴと青リンゴの２つの色に分ける
        {
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < splitPos; j++)
                {
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;//分割領域の左側は赤りんご
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = splitPos; j < y; j++)
                {
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.greenapple;//分割領域の右側は青リンゴ
                }
            }
        }
        private void removeThings()//初期化する時に呼び出す
        {
            label1.Visible = false;
            label2.Visible = false;
            this.Controls.Remove(splitLine);
            label3.Text = "左の四角にかける数を、右の四角にかけられる数をいれてね";
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
            int widthDistance = this.Width / b;//リンゴの左右の間隔
            splitWidth = this.Width / (b+1);//分割する線を表示する時の間隔            
            removeThings();//初期化

            label3.Text = "リンゴの近くにある三角のボタンをおしてね";
            for (int i = 0; i < a; i++)

            {
                for (int j = 0; j < b; j++)
                {
                    if( i == 0)
                    {
                        number[j] = new Label();//りんごが左から何番目かを示すラベル
                        number[j].Location = new Point(widthDistance * j+ (widthDistance-10) / 2, 230);
                        number[j].Size = new System.Drawing.Size(40, 20);
                        number[j].Text = (j + 1).ToString();
                        number[j].Font = new Font("DefaultFont", 10);
                        this.Controls.Add(number[j]);
                    }
                    if (i == 0 && j != b - 1)
                    {
                        //split[j].Dispose();
                        split[j] = new Button();//分割する時にクリックするボタン
                        //this.Controls.Remove(split[j]);        
                        split[j].Location = new Point(widthDistance *j+ widthDistance-10, 230);
                        split[j].Size = new System.Drawing.Size(20, 20);
                        split[j].BackgroundImageLayout = ImageLayout.Zoom;
                        split[j].BackgroundImage = multiply.Properties.Resources.arrow;
                        split[j].Click += new EventHandler(splitClick);
                        this.Controls.Add(split[j]);
                    }
                    dots[i, j] = new PictureBox();//表示するりんご
                    //   this.Controls.Remove(dots[i,j]);
                    dots[i, j].Location = new Point(widthDistance * j, i * heightDistance + 250);
                    dots[i, j].Size = new System.Drawing.Size(widthDistance - 10, heightDistance - 10);
                    dots[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;

                    this.Controls.Add(dots[i, j]);
                }
            }
        }
    }
}
