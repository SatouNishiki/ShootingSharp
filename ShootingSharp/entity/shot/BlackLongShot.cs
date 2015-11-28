using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.entity.shot
{
    public class BlackLongShot : EnemyCircleShot
    {
        private int drawCount;
        private int gTheta;

        /*
        public BlackLongShot(Builder builder)
            : base(builder)
        {
            this.MoveSpeed = 2;
            this.textureSize = new System.Drawing.Size(5, 10);
        }

         */

        public BlackLongShot()
            : base()
        {
            this.MoveSpeed = 2;
            this.textureSize = new System.Drawing.Size(5, 10);
        }

        public override string GetTextureName()
        {
            return "black_long_shot.png";
        }
        
        public override int GetRadius()
        {
            return 1;
        }


        public override void Draw()
        {

            DX.DrawRotaGraph(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY, 2.0D, (Math.PI / 180.0D) * this.gTheta, this.textureLoader.Textures[this.GetTextureName()], DX.TRUE);

            this.drawCount++;
            this.gTheta+=2;
        }
    }
}
