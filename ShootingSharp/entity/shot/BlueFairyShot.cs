using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class BlueFairyShot : EnemyCircleShot
    {
        /*
        public BlueFairyShot(Builder builder)
            : base(builder)
        {
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(20, 20);
        }*/

        public BlueFairyShot()
            : base()
        {
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(20, 20);
        }

        public override string GetTextureName()
        {
            if (this.metaData == 1)
                return "dark_blue_fairy_shot.png";
            else
                return "blue_fairy_shot.png";
        }

        public override int GetRadius()
        {
            return 3;
        }

    }
}
