using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;
using ShootingSharp.ai;
using ShootingSharp.entity.shot;

namespace DebugProject.enemy
{
    public class Tsukihi : Enemy
    {
        public Tsukihi()
            : base()
        {
            this.AIEnabled = true;
            this.MoveSpeed = 2;

            this.AddMoveAI(new LeftDownMoveAI(this, 0, 1000));

            this.AddActionAI(new SinShotAI<RedFairyShot>(this, 0, 1, 0));

            this.AddActionAI(new NoneAI(this, 0, 30));

            this.textureSize = new System.Drawing.Size(32, 32);
        }

        public override int GetRadius()
        {
            return 20;
        }

        public override string GetCenterTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetDownTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetLeftDownTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetLeftTextureName()
        {
            return "tsukihi_left.png";
        }

        public override string GetLeftUpTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetRightDownTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetRightTextureName()
        {
            return "tsukihi_right.png";
        }

        public override string GetRightUpTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetUpTextureName()
        {
            throw new NotImplementedException();
        }
        /*
        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(32, 32);
        }*/

        public override void SetMoveType()
        {
            this.MoveType = MoveTypeEnum.Right;
        }
    }
}
