using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class RedFairyShot : Shot
    {

        public RedFairyShot(IHasSSPosition shooter) : base(shooter) 
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.moveSpeed = -10;
        }

        public RedFairyShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.moveSpeed = -10;
        }

        public override void OnDeath()
        {
            //なにもしない
        }

        public override string GetTextureName()
        {
            return "fairy_shot.png0";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(20, 20);
        }

        public override void DoAction()
        {
          //  throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 10;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return SharpType.Circle;
        }

        public override void OnUpdate()
        {
            if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
                || this.position.PosX < 0
                || this.position.PosY < 0)
            {
                this.Life = 0;
            }


            base.OnUpdate();
        }
        
    }
}
