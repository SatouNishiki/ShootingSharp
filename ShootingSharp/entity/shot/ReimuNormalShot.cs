using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class ReimuNormalShot : PlayerCircleShot
    {
        /*
        public ReimuNormalShot(Builder builder)
            : base(builder)
        {
            this.textureLoader.LoadSprite("reimu_shot.png", 4, 4, 9, 130, 130);
            this.MoveSpeed = -10;
            this.textureSize = new System.Drawing.Size(20, 20);
        }*/

        public ReimuNormalShot()
            : base()
        {
            this.textureLoader.LoadSprite("reimu_shot.png", 4, 4, 9, 130, 130);
            this.MoveSpeed = -10;
            this.textureSize = new System.Drawing.Size(20, 20);
        }

        public override string GetTextureName()
        {
            return "reimu_shot.png8";
        }
        
        public override int GetRadius()
        {
            return 20;
        }
        
    }
}
