using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.position;

namespace ShootingSharp.entity.shot
{
    public class RGBStoneShot : EnemyCircleShot
    {
        public enum ColorType
        {
            Red, Green, Blue, Purple
        }

        private int moveCount;
        private int initPosX;
        private ColorType color;

        public RGBStoneShot(Builder buider)
            : base(buider)
        {
            this.MoveSpeed = 1;
            this.initPosX = position.PosX;
            this.color = ColorType.Red;
            this.textureSize = new System.Drawing.Size(15, 20);
        }
        
        public RGBStoneShot(Builder builder, ColorType color)
            : base(builder)
        {
            this.MoveSpeed = 1;
            this.initPosX = position.PosX;
            this.color = color;
            this.textureSize = new System.Drawing.Size(15, 20);
        }

        public override string GetTextureName()
        {
            switch (this.color)
            {
                case(ColorType.Red):
                    return "red_stone.png";

                case(ColorType.Blue):
                     return "blue_stone.png";

                case (ColorType.Green):
                     return "green_stone.png";

                default:
                     return "purple_stone.png";
               
            }
            
        }
        public override int GetRadius()
        {
            return 1;
        }

        public override void Move()
        {
            this.position.PosX = (int)((this.initPosX + Math.Round(Math.Sin(this.moveCount / 12.0D) * this.MoveSpeed * 12)));
            this.position.PosY += this.MoveSpeed;


            this.moveCount++;
        }
    }
}
