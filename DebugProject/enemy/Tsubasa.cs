using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;
using ShootingSharp.ai;
using DebugProject.ai;
using ShootingSharp.task;

namespace DebugProject.enemy
{
    public class Tsubasa : Enemy
    {
        public Tsubasa()
            : base()
        {
            this.AIEnabled = true;
            this.MoveSpeed = 2;

            this.AddMoveAI(new RightDownMoveAI(this, 0, 1000));

            this.AddActionAI(new CircleShotAI(this, 1, 1, SSTaskFactory.PlayerUpdateTask.Player, 0));

            this.AddActionAI(new NoneAI(this, 0, 30));

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
            return "tsubasa_left.png";
        }

        public override string GetLeftTextureName()
        {
            return "tsubasa_left.png";
        }

        public override string GetLeftUpTextureName()
        {
            return "tsubasa_left.png";
        }

        public override string GetRightDownTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetRightTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetRightUpTextureName()
        {
            throw new NotImplementedException();
        }

        public override string GetUpTextureName()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(32, 32);
        }

        public override void SetMoveType()
        {
            this.MoveType = MoveTypeEnum.Left;
        }

    }
}
