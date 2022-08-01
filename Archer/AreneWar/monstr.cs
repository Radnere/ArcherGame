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
    public class monstr
    {
        public int posX;
        public int posY;
        public int dirX;
        public int dirY;
        public int HP;
        public int Spawnsite;
        public int flip;
        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;
        public bool dead = true;
        public Image spriteSheet;
        public monstr(int HP,  Image spriteSheet)
        {
            this.HP = HP;
            this.spriteSheet = spriteSheet;
            currentAnimation = 533;
            currentFrame = 0;
            currentLimit = 8;
            flip = 1;
        }
       
        public void PlayAnimation(Graphics g)
        {
            Random rnd = new Random();
            if (dead == false)
            {
                if (flip == -1) currentAnimation = 533; else currentAnimation = 489;
                if (currentFrame < currentLimit - 1)
                    currentFrame++;
                else currentFrame = 0;
                g.DrawImage(spriteSheet, new Rectangle(new Point(posX, posY), new Size(34, 64)), 46 + (23 * currentFrame), currentAnimation, 23, 43, GraphicsUnit.Pixel);
            }
            else {
                Spawnsite = rnd.Next(0, 3); 
              switch (Spawnsite)
                {
                    case 0:
                        {
                            posX = rnd.Next(-50, 1000);
                            posY = -50;
                            break;
                        }
                    case 1:
                        {
                            posX = 1000;
                            posY = rnd.Next(-50, 1000);
                            break;
                        }
                    case 2:
                        {
                            posX = rnd.Next(-50, 1000);
                            posY = 1000;
                            break;
                        }
                    case 3:
                        {
                            posX = -50;
                            posY = rnd.Next(-50, 1000);
                            break;
                        }
                }
                dead = false;
                HP = 30;
            }
           
        }

    }

}
