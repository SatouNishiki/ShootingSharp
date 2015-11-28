using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class RedFairyShot : EnemyCircleShot
    {/*
        public RedFairyShot(Builder builder)
            : base(builder)
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(20, 20);
        }
        */

        public RedFairyShot()
            : base()
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(20, 20);
        }

        public override string GetTextureName()
        {
            return "fairy_shot.png0";
        }

        public override int GetRadius()
        {
            return 3;
        }

        
    }
}
