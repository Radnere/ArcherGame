using AreneWar.Controllers;
using AreneWar.Entites;
using AreneWar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AreneWar.Save;

namespace AreneWar
{
    public partial class Form1 : Form
    {
        public Image archerSheet;
        public Image arrowSheet;
        public Arrow arrow1;
        public monstr zombie;
        public Entity player;
        GameHistory game = new GameHistory();
        public int interval = 60;
        public int mouseX;
        public int mouseY;
        public double tg30 = 0.577350269189625;
        public double cos30 = 0.93969262078;
        public double finishX;
        public double finishY;
        public bool poop = true;
        public Form1()
        {
            InitializeComponent();

            timer1.Interval = interval;
            timer1.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Init();
        }

        public void OnKeyUp(object sender ,KeyEventArgs e)
        {
            interval = 60;
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = 0;
                    break;
                case Keys.S:
                    player.dirY = 0;
                    break;
                case Keys.A:
                    player.dirX = 0;
                    break;
                case Keys.D:
                    player.dirX = 0;
                    break;
            }

            if (player.dirX == 0 && player.dirY == 0)
            {
                player.isMoving = false;
                if(player.flip == 1)
                player.SetAnimationConfiguration(0);
                else player.SetAnimationConfiguration(3);
            }
            //player.dirX = 0;
            //player.dirY = 0;
            //player.isMoving = false;
            //player.SetAnimationConfiguration(0);
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = -10;
                    player.isMoving = true;
                    if (player.flip == 1)
                        player.SetAnimationConfiguration(1);
                    else player.SetAnimationConfiguration(4);
                    break;
                case Keys.S:
                    player.dirY = 10;
                    player.isMoving = true;
                    if (player.flip == 1)
                        player.SetAnimationConfiguration(1);
                    else player.SetAnimationConfiguration(4);
                    break;
                case Keys.A:
                    player.dirX = -10;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(4);
                    player.flip = -1;
                    break;
                case Keys.D:
                    player.dirX = 10;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    player.flip = 1;
                    break;
            }
        }
        public void Init()
        {
            MapController.Init();

            this.Width = MapController.GetWidth();
            this.Height = MapController.GetHeight();

            archerSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(),"Sprites\\archer.png"));
            player = new Entity(448, 448, 100, 20, Hero.idleFrames, Hero.runFrames, Hero.attackFrames, Hero.deathFrames, archerSheet);
            zombie = new monstr(30, archerSheet);
            arrow1 = new Arrow(player.posX, player.posY, archerSheet);
            timer1.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            //PhysicsController.IsCollide(player);
            if (!PhysicsController.IsCollide(player, new Point(player.dirX, player.dirY)))
            {
                if (player.isMoving)
                    player.Move();
            }
            if (player.isShoot)
            {
                arrow1.arrowinAir = true;
                if(poop)
                {
                arrow1.posaX = player.posX;
                arrow1.posaY = player.posY;
                    poop = false;
                }
                arrow1.isShoot = true;
                arrow1.MoveA();
                if (arrow1.endShoot)
                { 
                  player.isShoot = false;
                  arrow1.endShoot = false;
                  arrow1.isShoot = false;
                  arrow1.arrowinAir = false;
                  poop = true;
                }
            }
            if (zombie.posX > player.posX)
            {
                zombie.posX -= 3;
                zombie.flip = 1;
            }
            else if(zombie.dirX < player.posX)
            {
                zombie.posX += 3;
                zombie.flip = -1;
            }
            if (zombie.posY > player.posY )
            {
                zombie.posY -= 3;
            }
            else if (zombie.posY < player.posY)
            {
                zombie.posY += 3;
            }
            if (arrow1.CorrectArrow == 2 || arrow1.CorrectArrow == 5)
            {
                if (arrow1.endShoot == false)
                {
                    if (zombie.posX < arrow1.posaX & arrow1.posaX < zombie.posX + 23 & zombie.posY < arrow1.posaY & arrow1.posaY < zombie.posY + 43 ||
                zombie.posX < arrow1.posaX + 32 & arrow1.posaX + 32 < zombie.posX + 23 & zombie.posY < arrow1.posaY + 3 & arrow1.posaY + 3 < zombie.posY + 43 ||
                zombie.posX < arrow1.posaX + 32 & arrow1.posaX + 32 < zombie.posX + 23 & zombie.posY < arrow1.posaY & arrow1.posaY < zombie.posY + 43 ||
                zombie.posX < arrow1.posaX & arrow1.posaX < zombie.posX + 23 & zombie.posY < arrow1.posaY + 3 & arrow1.posaY + 3 < zombie.posY + 43)
                {
                    zombie.HP -= 10;
                    arrow1.endShoot = true;
                    if (zombie.HP <= 0) { zombie.dead = true; player.SCORE += 1; }
                }
                    }
            }
            else if ((arrow1.CorrectArrow == 6 || arrow1.CorrectArrow == 7 || arrow1.CorrectArrow == 8 || arrow1.CorrectArrow == 9))
            {
                if (arrow1.endShoot == false)
                {
                    if (zombie.posX < arrow1.posaX & arrow1.posaX < zombie.posX + 23 & zombie.posY < arrow1.posaY & arrow1.posaY < zombie.posY + 43 ||
                   zombie.posX < arrow1.posaX + 32 & arrow1.posaX + 32 < zombie.posX + 23 & zombie.posY < arrow1.posaY + 32 & arrow1.posaY + 32 < zombie.posY + 43 ||
                   zombie.posX < arrow1.posaX + 32 & arrow1.posaX + 32 < zombie.posX + 23 & zombie.posY < arrow1.posaY & arrow1.posaY < zombie.posY + 43 ||
                   zombie.posX < arrow1.posaX & arrow1.posaX < zombie.posX + 23 & zombie.posY < arrow1.posaY + 32 & arrow1.posaY + 32 < zombie.posY + 43)
                    {
                        zombie.HP -= 10;
                        arrow1.endShoot = true;
                        if (zombie.HP <= 0) { zombie.dead = true; player.SCORE += 1; }
                    }
                }
            }

                Invalidate();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Graphics m = e.Graphics;
            MapController.DrawMap(g);
            Graphics a = e.Graphics;
            player.PlayAnimation(g);
            arrow1.Shoot(a);
            zombie.PlayAnimation(m);
            label1.Text = player.arrows.ToString();
            label2.Text = mouseX.ToString();
            label3.Text = mouseY.ToString();
            label4.Text = (player.posX + 18).ToString();
            label5.Text = (player.posY + 21).ToString();
            label8.Text = (player.SCORE).ToString();
        }

        public void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (arrow1.arrowinAir == false)
            {
                interval = interval - interval / 2;
                player.dirX = 0;
                player.dirY = 0;
                int centrX = player.posX + 18;
                int centrY = player.posY + 21;
                player.isMoving = false;
                if (mouseX < centrX) // left 
                {
                    finishY = Math.Sqrt(Math.Pow(((centrX - mouseX) / cos30), 2) - Math.Pow((centrX - mouseX), 2));
                    if (mouseY < centrY)
                    {
                        if (centrY - finishY < mouseY) { player.SetAnimationConfiguration(5); arrow1.SetDir(5); }
                        else { player.SetAnimationConfiguration(8); arrow1.SetDir(8); }
                    }
                    else
                    {
                        if (centrY + finishY > mouseY) { player.SetAnimationConfiguration(5); arrow1.SetDir(5); }
                        else { player.SetAnimationConfiguration(9); arrow1.SetDir(9); }
                    }
                }
                else
                {
                    finishY = Math.Sqrt(Math.Pow(((mouseX - centrX) / cos30), 2) - Math.Pow((mouseX - centrX), 2));
                    if (mouseY < centrY)
                    {
                        if (centrY - finishY < mouseY) { player.SetAnimationConfiguration(2); arrow1.SetDir(2); }
                        else
                        {
                            player.SetAnimationConfiguration(6);
                            arrow1.SetDir(6);
                        }
                    }
                    else
                    {
                        if (centrY + finishY > mouseY) { player.SetAnimationConfiguration(2); arrow1.SetDir(2); }
                        else { player.SetAnimationConfiguration(7); arrow1.SetDir(7); }
                    }
                }
            } 
        }

        private void сохранитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            game.History.Push(player.SaveState());
        }

        private void загрузитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            zombie.dead = true;
            if (game.History.Count > 0)
            {
                player.RestoreState(game.History.Pop());

            }
            else if (game.History.Count == 0)
            { player = new Entity(448, 448, 100, 10, Hero.idleFrames, Hero.runFrames, Hero.attackFrames, Hero.deathFrames, archerSheet); }
        }
    }

}
