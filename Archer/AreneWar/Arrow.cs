using AreneWar.Controllers;
using AreneWar.Entites;
using AreneWar.Models;
using System.Drawing;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreneWar
{
    public class Arrow
    {
        public int posaX = 9999;
        public int posaY = 9999;
        public int arrows = 10;
        public Image spriteSheet;
        public bool isShoot = false;
        public bool arrowinAir = false;
        public bool endShoot = false;
        public int CorrectArrow;
        public int ArrowDX;
        public int ArrowDY;
        public int ArrowRX;
        public int ArrowRY;
        public Arrow(int posaX, int posaY, Image spriteSheet)
        {
            this.posaX = posaX;
            this.posaY = posaY;
            this.spriteSheet = spriteSheet;
        }
        public void MoveA()
        {
            switch (CorrectArrow)
            {
                case 2:
                    posaX += 50;
                    if (posaX > 928) { endShoot = true; }
                    break;
                case 5:
                    posaX -= 50;
                    if (posaX < 0) { endShoot = true; }
                    break;
                case 6:
                    posaX += 50;
                    posaY -= 50;
                    if (posaX > 928 & posaY < 0) { endShoot = true; }
                    break;
                case 7:
                    posaX += 50;
                    posaY += 50;
                    if (posaX > 928 & posaY > 928) { endShoot = true; }
                    break;
                case 8:
                    posaX -= 50;
                    posaY -= 50;
                    if (posaX < 0 & posaY < 0) { endShoot = true; }
                    break;
                case 9:
                    posaX -= 50;
                    posaY += 50;
                    if (posaX < 0 & posaY >928) { endShoot = true; }
                    break;
            }
        }
        public void Shoot(Graphics g)
        {
            if (isShoot)
            {
                g.DrawImage(spriteSheet, new Rectangle(new Point(posaX, posaY + 24), new Size(ArrowRX*2, ArrowRY*2)), ArrowDX, ArrowDY, ArrowRX, ArrowRY, GraphicsUnit.Pixel);
            }
        }
        public void SetDir(int CorrectArrow2)
        {
            CorrectArrow = CorrectArrow2;
            switch (CorrectArrow2)
            {
                case 2:
                    ArrowDX = 0;
                    ArrowDY = 474;
                    ArrowRX = 24;
                    ArrowRY = 3;
                    break;
                case 5:
                    ArrowDX = 0;
                    ArrowDY = 478;
                    ArrowRX = 24;
                    ArrowRY = 3;
                    break;
                case 6:
                    ArrowDX = 27;
                    ArrowDY = 483;
                    ArrowRX = 16;
                    ArrowRY = 16;
                    break;
                case 7:
                    ArrowDX = 27;
                    ArrowDY = 500;
                    ArrowRX = 16;
                    ArrowRY = 16;
                    break;
                case 8:
                    ArrowDX = 10;
                    ArrowDY = 483;
                    ArrowRX = 16;
                    ArrowRY = 16;
                    break;
                case 9:
                    ArrowDX = 10;
                    ArrowDY = 500;
                    ArrowRX = 16;
                    ArrowRY = 16;
                    break;
            }

        }
    }
}
