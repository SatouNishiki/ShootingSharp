using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;
using ShootingSharp.texture;
using DxLibDLL;
using ShootingSharp.position;

namespace DebugProject.shot
{
    public class StarsShot : EnemyCircleShot
    {
     //   public int ColorType { get; set; }

        private double gTheta;
        /*
        public StarsShot(IHasSSPosition shooter, double theta, int color)
            : base(shooter, theta)
        {
            this.MoveSpeed = 3;
            this.ColorType = color;
        }

        public StarsShot(IHasSSPosition shooter, SSPosition target, int color)
            : base(shooter, target)
        {
            this.MoveSpeed = 3;
            this.ColorType = color;
        }
        */

        public StarsShot(Builder builder)
            : base(builder)
        {
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(30, 30);
          //  this.ColorType = builder.metaData;
        }
        public override string GetTextureName()
        {
            if (this.metaData < 6)
            {
                return "stars" + this.metaData.ToString() + ".png" ;
            }
            else
            {
                return "stars0.png";
            }
        }/*

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(30, 30);
        }*/

        public override int GetRadius()
        {
            return 2;
        }

        public override void Draw()
        {
            DX.DrawRotaGraph(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY, 1.0D, (Math.PI / 180.0D) * this.gTheta, this.textureLoader.Textures[this.GetTextureName()], DX.TRUE);
        
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            this.gTheta += 1.5;
        }
    }
}
