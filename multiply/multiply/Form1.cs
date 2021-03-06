using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace multiply
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Text = "タイトル";
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            //左中段のボタンに対応した機能を実行するフォームを表示
            Form2 f2 = new Form2();
            f2.Show();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
 
            this.Visible = false;

            //左下段のボタンに対応した機能を実行するフォームを表示
            Form3 f3 = new Form3();
            f3.Show();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
   
            this.Visible = false;

            //右中段のボタンに対応した機能を実行するフォームを表示
            Form4 f4 = new Form4();
            f4.Show();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            
            this.Visible = false;

            //右上段のボタンに対応した機能を実行するフォームを表示
            Form5 f5 = new Form5();
            f5.Show();
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            
            this.Visible = false;

            //右下段のボタンに対応した機能を実行するフォームを表示
            Form6 f6 = new Form6();
            f6.Show();
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            
            this.Visible = false;

            //左上段のボタンに対応した機能を実行するフォームを表示
            Form7 f7 = new Form7();
            f7.Show();
        }
    }
    }

