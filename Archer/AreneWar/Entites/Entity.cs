using AreneWar.Controllers;
using System.Drawing;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AreneWar.Save;

namespace AreneWar.Entites
{
    public class Entity
    {
        public int posX;
        public int posY;

        public int dirX;
        public int dirY;
        public int HP;
        public int SCORE;
        public int arrows;
        public bool isMoving;
        public bool isShoot = false;

        public int flip;

        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;

        public int idleFrames;
        public int runFrames;
        public int attackFrames;
        public int deathFrames;

        public int size;

        public Image spriteSheet;
        public Entity(int posX,int posY,int HP,int arrows, int idleFrames,int runFrames, int attackFrames,int deathFrames,Image spriteSheet)
        {
            this.posX = posX;
            this.posY = posY;
            this.HP = HP;
            SCORE = 0;
            this.arrows = arrows;
            this.idleFrames = idleFrames;
            this.runFrames = runFrames;
            this.attackFrames = attackFrames;
            this.deathFrames = deathFrames;
            this.spriteSheet = spriteSheet;
            size = 55;
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = idleFrames;
            flip = 1;
        }

        public void Move()
        {
            posX += dirX;
            posY += dirY;
        }

        public void PlayAnimation(Graphics g)
        {
            
                if (currentAnimation == 2 || currentAnimation == 5 || currentAnimation == 6 || currentAnimation == 7 || currentAnimation == 8 || currentAnimation == 9)
                {
                if (arrows > 0)
                {
                    if (currentFrame < currentLimit - 1)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        isShoot = true;
                        currentFrame = 0;
                        arrows--;
                        if (flip == -1) SetAnimationConfiguration(3); else SetAnimationConfiguration(0);
                    }
                    g.DrawImage(spriteSheet, new Rectangle(new Point(posX, posY), new Size(size, size)), 37f * currentFrame, 43 * currentAnimation, 37, 43, GraphicsUnit.Pixel);
                }
                else if (flip == -1) SetAnimationConfiguration(3); else SetAnimationConfiguration(0);
                g.DrawImage(spriteSheet, new Rectangle(new Point(posX, posY), new Size(size, size)), 37f * currentFrame, 43 * currentAnimation, 37, 43, GraphicsUnit.Pixel);
            } 
            else
                {
                    if (currentFrame < currentLimit - 1)
                        currentFrame++;
                    else currentFrame = 0;
                    g.DrawImage(spriteSheet, new Rectangle(new Point(posX, posY), new Size(size, size)), 37f * currentFrame, 43 * currentAnimation, 37, 43, GraphicsUnit.Pixel);
                }
        }
         
        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;
            
            switch (currentAnimation)
            {
                case 0:
                    currentLimit = idleFrames;
                    break;
                case 1:
                    currentLimit = runFrames;
                    break;
                case 2:
                    currentLimit = attackFrames;
                    break;
                case 3:
                    currentLimit = idleFrames;
                    break;
                case 4:
                    currentLimit = runFrames;
                    break;
                case 5:
                    currentLimit = attackFrames;
                    break;
                case 6:
                    currentLimit = attackFrames;
                    break;
                case 7:
                    currentLimit = attackFrames;
                    break;
                case 8:
                    currentLimit = attackFrames;
                    break;
                case 9:
                    currentLimit = attackFrames;
                    break;
                case 10:
                    currentLimit = deathFrames;
                    break;
            }
        }
        // сохранение состояния
        public HeroMemento SaveState()
        {
            return new HeroMemento(arrows, HP, posX, posY);
        }

        // восстановление состояния
        public void RestoreState(HeroMemento memento)
        {
            this.arrows = memento.arrows;
            this.HP = memento.HP;
            this.posX = memento.posX;
            this.posY = memento.posY;
        }
    }
}
