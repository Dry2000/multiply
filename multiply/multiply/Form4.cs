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
//面倒になったので、大きな差異が存在しない場合form2、3と同じところについてコメントアウトはしない
namespace multiply
{
    public partial class Form4 : Form
    {
        PictureBox[,] dots = new System.Windows.Forms.PictureBox[100, 100];
        Button[] split = new Button[100];
        Button[] horizonalSplit = new Button[100];
        PictureBox splitLine = new PictureBox();
        PictureBox horizonalSplitLine = new PictureBox();
        int x, y, splitWidth, splitPos,horizonalSplitWidth,horizonalSplitPos,former,latter;
        /*x,y:入力された式のかけられる数、かける数 
        splitWidth:かけられる数の数から算出した分割する線を表示する間隔
        splitPos:分割線を表示するために押されたボタンが左から何番目の箇所かを示す
        former,latter:以前の仕様でかけられる数かける数を保持しておくために使用
        horizonal... : 上下にも分割可能にしようとした名残、色分けなどの素材の入手が面倒だったので削除
        */
        bool buttonCliked = false;//分割ボタンが押された時にtrue、なんのために実装したか覚えてないし、これを条件にもつif文もないので削除可能?
        Label[] number = new Label[100];
        PictureBox check = new PictureBox();
        Timer timer = new Timer();
        SoundPlayer sp;
        public Form4()
        {
            InitializeComponent();
            this.Text = "むずかしいれんしゅうもんだい";
            this.WindowState = FormWindowState.Maximized;
            createFormula();
            hideThings();
            label2.Text = "×";
            button1.Location = new Point(label1.Location.X + 400, 0);
            label6.Location = new Point(button1.Location.X + 200, 23);
            textBox11.Location = new Point(label6.Location.X + 250, 23);
            button2.Location = new Point(textBox11.Location.X + 150, 0);
            button4.Location = new Point(button2.Location.X + 150, 0);
            label7.Hide();
            label8.Hide();
            textBox9.Hide();
            textBox10.Hide();
        }
        private void createFormula()
        {
            Random r1 = new System.Random();//乱数生成器
            int tmp = r1.Next(11, 20);//11~19の乱数生成
            int tmp2 = r1.Next(11, 20);
            former = tmp;
            latter = tmp2;
            label1.Text = tmp.ToString() + " × " + tmp2.ToString() + " = ?";//問題式をlabel1に表示させる
            x = tmp;
            y = tmp2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            createDots(x, y);
        }
        private void button4_Click(object sender, EventArgs e)//"つぎのもんだいへ"ボタンを押された時に起動
        {
            createFormula();//式を再生成
            buttonCliked = false;
            removeThings();//初期化
        }
        private void splitClick(object sender, EventArgs e)
        {
            splitPos = ((((Button)sender).Left+10) / splitWidth);
            splitLine.BackgroundImageLayout = ImageLayout.Stretch;
            splitLine.BackgroundImage = multiply.Properties.Resources.line;
            splitLine.Location = ((Button)sender).Location;
            this.Controls.Add(splitLine);
            splitLine.Size = new Size(((Button)sender).Width, this.Height - 70);
            splitLine.BringToFront();
            buttonCliked = true;
            replaceApple();
            showThings();
            textBox3.Location = new Point(splitPos * splitWidth / 3, 150);
            label2.Location = new Point(textBox3.Location.X + 60, 150);
            textBox4.Location = new Point(label2.Location.X + 70, 150);
            label3.Location = new Point(textBox4.Location.X + 75, 150);
            textBox5.Location = new Point(label3.Location.X + 50, 150);
            if ((y - splitPos) * splitWidth > 600)
            {
                textBox6.Location = new Point(splitPos * splitWidth + (y - splitPos) * splitWidth / 3, 150);
            }
            else
            {
                textBox6.Location = new Point(this.Width - 500, 150);
            }
            label4.Location = new Point(textBox6.Location.X + 60, 150);
            textBox7.Location = new Point(label4.Location.X + 70, 150);
            label5.Location = new Point(textBox7.Location.X + 75, 150);
            textBox8.Location = new Point(label5.Location.X + 50, 150);
        }
        private void horizonalSplitClick(object sender, EventArgs e)//上下にも分割できるようにしようとした名残
        {
            int tmp = (((Button)sender).Top / horizonalSplitWidth);
            splitLine.BackgroundImageLayout = ImageLayout.Stretch;
            splitLine.BackgroundImage = multiply.Properties.Resources.verticalLine;
            splitLine.Location = ((Button)sender).Location;
            this.Controls.Add(splitLine);
            splitLine.Size = new Size(this.Width, ((Button)sender).Height);
            splitLine.BringToFront();
            buttonCliked = true;
            
        }
        private void hideThings()
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
        private void showThings()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            textBox6.Show();
            textBox7.Show();
            textBox8.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
        }
        private void replaceApple()
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
        private void button2Click(object sender, EventArgs e)
        {
            if (textBox11.Text != (x * y).ToString())
            {
                tellFalse();
                return;
            }
            if (textBox5.Text != (x * splitPos).ToString())
            {
                tellFalse();
                return;
            }
            if (textBox8.Text != (x * (y - splitPos)).ToString())
            {
                tellFalse();
                return;
            }
            sp = new SoundPlayer(Properties.Resources.correct1);
            sp.Play();
            showAnswer(true);

        }
        private void tellFalse()
        {

            sp = new SoundPlayer(Properties.Resources.miss);
            sp.Play();
            showAnswer(false);
        }
        private void showAnswer(Boolean n)
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
           
            check.Location = new Point(this.Width/5,this.Height/4);//この位置調整環境によってだいぶ変化するから実際はもっと相対的に設置しないといけない
            this.Controls.Add(check);
            check.Size = new Size(1200, 400);

            check.BringToFront();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(erase);
            timer.Enabled = true;
        }
        private void erase(object sender,EventArgs e)
        {
            this.Controls.Remove(check);
            timer.Enabled = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";        
            hideThings();
            this.Controls.Remove(splitLine);
            this.Controls.Remove(check);
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
                    if (j == 0 && i != 100 - 1)
                    {

                        this.Controls.Remove(horizonalSplit[i]);
                    }
                    this.Controls.Remove(dots[i, j]);
                }
            }
        }
        private void createDots(int a, int b)
        {
            int heightDistance = (this.Height - 270) / a;
            int widthDistance = this.Width / b;
            splitWidth = this.Width / b;
            horizonalSplitWidth = (this.Height - 270) / a;//上下の分割線を表示する間隔
            removeThings();
            label6.Text = x.ToString() + "×" + y.ToString() + "=";
            label6.Show();
            textBox11.Text = "";
            textBox11.Show();
            textBox11.BringToFront();
            label6.SendToBack();
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (i == 0)
                    {
                        number[j] = new Label();
                        number[j].Location = new Point(widthDistance * j + (widthDistance - 10) / 2,230);
                        number[j].Size = new System.Drawing.Size(40, 20);
                        number[j].Text = (j + 1).ToString();
                        number[j].Font = new Font("DefaultFont", 10);
                        this.Controls.Add(number[j]);
                    }
                    if (i == 0 && j != b - 1)
                    {
                        //split[j].Dispose();
                        split[j] = new Button();
                        //this.Controls.Remove(split[j]);        
                        split[j].Location = new Point(widthDistance * (j + 1)+20, 230);
                        split[j].Size = new System.Drawing.Size(20, 20);
                        split[j].BackgroundImageLayout = ImageLayout.Zoom;
                        split[j].BackgroundImage = multiply.Properties.Resources.arrow;
                        split[j].Click += new EventHandler(splitClick);
                        this.Controls.Add(split[j]);
                    }
                    if(j == 0 && i != a - 1)
                    {
                        horizonalSplit[i] = new Button();
                        //this.Controls.Remove(split[j]);        
                        horizonalSplit[i].Location = new Point(0, heightDistance * (i + 1)+230);
                        horizonalSplit[i].Size = new System.Drawing.Size(20, 20);
                        horizonalSplit[i].BackgroundImageLayout = ImageLayout.Zoom;
                        horizonalSplit[i].BackgroundImage = multiply.Properties.Resources.arrow;
                        horizonalSplit[i].Click += new EventHandler(horizonalSplitClick);
                     //   this.Controls.Add(horizonalSplit[i]);
                    }
                    dots[i, j] = new PictureBox();
                    //   this.Controls.Remove(dots[i,j]);
                    dots[i, j].Location = new Point(widthDistance * j+30, i * heightDistance + 250);
                    dots[i, j].Size = new System.Drawing.Size(widthDistance - 10, heightDistance - 10);
                    dots[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    dots[i, j].BackgroundImage = multiply.Properties.Resources.apple;

                    this.Controls.Add(dots[i, j]);
                }
            }
        }
    }
}
