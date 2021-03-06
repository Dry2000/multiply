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
    public partial class Form7 : Form
    {
        PictureBox[,] dots = new System.Windows.Forms.PictureBox[100, 100];
        Button[] split = new Button[100];
        PictureBox splitLine = new PictureBox();
        Label[] number = new Label[100];
        Label[] verticalNumber = new Label[100];//かけられる数を数えるために使う数字のラベル
        int x, y, splitWidth, splitPos, former, latter,counter=0,widthDistance,heightDistance;
        //counter:"かけるすうをふやす"ボタンが押された回数
        public Form7()
        {
            InitializeComponent();
            this.Text = "かけざんのいみをもういちど見てみよう";
            this.WindowState = FormWindowState.Maximized;
            label3.Font = new Font(label3.Font.Name, 30, label3.Font.Style);
            label3.Text = "左の四角にかけられる数をいれて、よういするをおしてみてね";
            button1.Location = new Point(label4.Location.X + 300, 0);
            button2.Location = new Point(button1.Location.X + 200, 0);
            label3.Location = new Point(button2.Location.X + 150, 25);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int tmp = 0;
            if (Int32.TryParse(textBox1.Text, out tmp))
            {
                tmp = int.Parse(textBox1.Text);
                
                x = tmp;
                createDots(tmp, 15);            
            }
            else
            {
                return;
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
                    this.Controls.Remove(dots[14, i]);
                
            }
           // label3.Text = "左の四角にかけられる数をいれて、じゅんびをするをおしてみてね";
            for (int i = 0; i < former; i++)
            {
                this.Controls.Remove(verticalNumber[i]);
                for (int j = 0; j < 16; j++)
                {
                    if (i == 0)
                    {
                       
                        this.Controls.Remove(number[j]);
                    }
                    this.Controls.Remove(dots[j, i]);
                    
                }
            }
        }
        private void showDots(object sender, EventArgs e)
        {
            
            label4.Text = "×"+(counter+1).ToString()+"="+(x*(counter+1)).ToString();
            
            number[counter] = new Label();
            number[counter].Location = new Point(widthDistance * counter +50+ (widthDistance - 10) / 2, 180);
            number[counter].Size = new System.Drawing.Size(40, 20);
            number[counter].Text = (counter + 1).ToString();
            number[counter].Font = new Font("DefaultFont", 10);
            this.Controls.Add(number[counter]);//一行分かけられる数が増えたのでそこが何番目かを表示する
            for (int j = 0; j < x; j++)//一行分リンゴを追加で表示する
                {
                    if (counter == 0)
                    {
                    verticalNumber[j] = new Label();
                    verticalNumber[j].Location = new Point(10 , j*heightDistance+200+(heightDistance-10)/2);
                    verticalNumber[j].Size = new System.Drawing.Size(40, 20);
                    verticalNumber[j].Text = (j + 1).ToString();
                    verticalNumber[j].Font = new Font("DefaultFont", 10);
                    this.Controls.Add(verticalNumber[j]);
                    verticalNumber[counter].BringToFront();
                }

                    dots[counter, j] = new PictureBox();
                    //   this.Controls.Remove(dots[i,j]);
                    dots[counter, j].Location = new Point(widthDistance * counter+50, j * heightDistance + 200);
                    dots[counter, j].Size = new System.Drawing.Size(widthDistance - 10, heightDistance - 10);
                    dots[counter, j].BackgroundImageLayout = ImageLayout.Zoom;
                    dots[counter, j].BackgroundImage = multiply.Properties.Resources.apple;
                Console.WriteLine(counter.ToString() + "," + j.ToString());
                    this.Controls.Add(dots[counter, j]);
                }
            if (counter >= 14)//15回押されたなら終了する
            {
                label3.Text = "つぎはほかの数字も入れてみよう";
                return;
            }
            counter++;
        }
        private void createDots(int a, int b)
        {
            label3.Text = "かける数をふやすをおしてみてね";
            label4.Text = "× ?";
            heightDistance = (this.Height - 220) / a;
            widthDistance = (this.Width-50) / b;
            splitWidth = this.Width / (b + 1);
            counter = 0;
            removeThings();

         
        }
    }
}

