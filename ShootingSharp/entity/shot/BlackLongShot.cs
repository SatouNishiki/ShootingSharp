using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.entity.shot
{
    public class BlackLongShot : Shot
    {
        private int drawCount;
        private int gTheta;

        public BlackLongShot(interfaces.IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.moveSpeed = 2;
        }

        public BlackLongShot(interfaces.IHasSSPosition shooter, position.SSPosition target)
            : base(shooter, target)
        {
            this.moveSpeed = 2;
        }

        public override void OnDeath()
        {
           
        }

        public override string GetTextureName()
        {
            return "black_long_shot.png";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(5, 10);
        }

        public override void DoAction()
        {
        }

        public override int GetRadius()
        {
            return 1;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public override void Draw()
        {
            if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
                || this.position.PosX < 0
                || this.position.PosY < 0)
            {
                return;
            }
            

            DX.DrawRotaGraph(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY, 2.0D, (Math.PI / 180.0D) * this.gTheta, this.textureLoader.Textures[this.GetTextureName()], DX.TRUE);

            
            this.drawCount++;
            this.gTheta+=2;
        }
    }
}
