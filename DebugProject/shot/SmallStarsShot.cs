using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;
using ShootingSharp.position;
using DxLibDLL;

namespace DebugProject.shot
{
    public class SmallStarsShot : EnemyCircleShot
    {
     //   private int colorType;

        private double gTheta;
     

        public SmallStarsShot(Builder builder)
            : base(builder)
        {
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(10, 10);
        //    this.colorType = builder.metaData;
        }

        public override int GetRadius()
        {
            return 1;
        }

        public override string GetTextureName()
        {
            if (this.metaData < 3)
            {
                return "stars_shot" + this.metaData.ToString() + ".png";
            }
            else
            {
                return "stars_shot5.png";
            }
        }
        
        public override void Draw()
        {
            DX.DrawRotaGraph(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY, 0.7D, (Math.PI / 180.0D) * this.gTheta, this.textureLoader.Textures[this.GetTextureName()], DX.TRUE);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            this.gTheta += 2.5D;
        }
    }
}
