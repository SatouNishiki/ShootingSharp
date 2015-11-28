using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;
using ShootingSharp.position;

namespace DebugProject.shot
{
    public class CirclesShot : EnemyCircleShot
    {
       

        public CirclesShot(Builder builder)
            : base(builder)
        {
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(30, 30);
       
        }
        
        public override int GetRadius()
        {
            return 2;
        }

        public override string GetTextureName()
        {
            if (this.metaData < 3)
            {
                return "circles_shot" + this.metaData.ToString() + ".png";
            }
            else
            {
                return "circles_shot2.png";
            }
        }
     
    }
}
