using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;

namespace ShootingSharp.entity.shot
{
    public class BlackCircleShot : EnemyCircleShot
    {
        /*
        public BlackCircleShot(Builder builder)
            : base(builder)
        {
            this.textureLoader.LoadDerivationGraph(167, 62, 30, 30, "shot.png");
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(12, 12);
        }*/

        public BlackCircleShot()
            : base()
        {
            this.textureLoader.LoadDerivationGraph(167, 62, 30, 30, "shot.png");
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(12, 12);
        }

        public override string GetTextureName()
        {
            return "shot.png167+62";
        }
       
        public override int GetRadius()
        {
            return 1;
        }

    }
}
