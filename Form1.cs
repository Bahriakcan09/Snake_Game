using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            create(300,300);
            yem_ozellik();
            bait_create();
            timer1.Start();
        }
        int count = 0,boyut = 30,yon = 0,x,y,score=0;
        Label[] lbl=new Label[200];
        Label yem=new Label();
        Random random = new Random();
        Rectangle rect1, rect2;
        public void create(int LocationX,int LocationY)
        {            
            Label a =new Label()
            {
                Name = "label"+count,
                Size = new Size(boyut,boyut),
                Location = new Point(LocationX,LocationY),
                BackColor = Color.Red
            };
            this.panel3.Controls.Add(a);
            lbl[count] = a;
        }
        void yem_ozellik()
        {
            yem.Name = "label";
            yem.Size = new Size(boyut, boyut);
            yem.BackColor = Color.Yellow;
        }
        public void bait_create()
        {
            do
            {            
                x = random.Next(0,20)*30;
                y = random.Next(0, 20)*30;
                rect1 = new Rectangle(x, y, 20, 20);
                rect2 = new Rectangle(lbl[0].Location.X, lbl[0].Location.Y, 20, 20);
            } while (rect1.IntersectsWith(rect2));

            yem.Location = new Point(x, y);

            this.panel3.Controls.Add(yem);
        }
        public void yuru()
        {
            for (int i = count; i > 0; i--)
            {
                lbl[i].Location = lbl[i-1].Location;
            }

            if (yon == 1)
            {
                lbl[0].Location = new Point(lbl[0].Location.X, lbl[0].Location.Y - boyut);
            }
            else if(yon == 2)
            {
                lbl[0].Location = new Point(lbl[0].Location.X + boyut, lbl[0].Location.Y);
            }
            else if (yon == 3)
            {
                lbl[0].Location = new Point(lbl[0].Location.X , lbl[0].Location.Y + boyut);
            }
            else if (yon == 4)
            {
                lbl[0].Location = new Point(lbl[0].Location.X-boyut, lbl[0].Location.Y );
            }
            else
            {

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (yon != 3 && (e.KeyCode == Keys.Up || e.KeyCode == Keys.W))
            {
                yon = 1;
            }
            else if (yon != 4 && (e.KeyCode == Keys.Right || e.KeyCode == Keys.D))
            {
                yon = 2;
            }
            else if (yon != 1 && (e.KeyCode == Keys.Down || e.KeyCode == Keys.S))
            {
                yon = 3;
            }
            else if (yon != 2 && (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)) 
            {
                yon = 4;
            }
        }
        public void durum()
        {
           Size size = new Size(20,20); 
           Rectangle aaa = new Rectangle(x, y, 20, 20);
           Rectangle bbb = new Rectangle(lbl[0].Location,size);
            Rectangle p = new Rectangle(panel3.Location.X, panel3.Location.Y - boyut * 2, panel3.Width, panel3.Height - 20);            
            if (!bbb.IntersectsWith(p))
            {
                end();
            }
            if (aaa.IntersectsWith(bbb))
            {
                panel3.Controls.Remove(yem);
                yılan_buyu();
                score += 10;
                scoreLabel.Text = "SCORE : " + score.ToString();
                bait_create();
            }

        }
        private void yılan_buyu()
        {
            int a=yem.Location.X, b=yem.Location.Y;
            count++;
            create(a,b);
        }
        public void end()
        {
            timer1.Stop();
            DialogResult a = MessageBox.Show("Your score : "+ score +"\nDo you want to play again", "GAME OVER",MessageBoxButtons.YesNo);
            if(a==DialogResult.Yes)
            {
                count = 0;
                yon = 0;
                score = 0;
                scoreLabel.Text= "SCORE : "+score.ToString();
                panel3.Controls.Clear();
                create(300, 300);
                yem_ozellik();
                bait_create();
                timer1.Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            yuru();
            durum();
        }
    }
}
