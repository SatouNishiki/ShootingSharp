using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;
using ShootingSharp.ai;
using DebugProject.ai;
using ShootingSharp.task;
using DebugProject.shot;

namespace DebugProject.enemy
{
    public class Mayoi : Enemy
    {
        private int type;

        public Mayoi()
            : base()
        {
            this.AIEnabled = true;
            this.MoveSpeed = 1;
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
            return "mayoi_left.png";
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
            return "mayoi_right.png";
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
            if (this.type == 0)
            {
                this.MoveType = MoveTypeEnum.Right;
            }
            else
            {
                this.MoveType = MoveTypeEnum.Left;
            }
        }

        public void SetType(int t)
        {
            this.type = t;

            if (this.type == 0)
            {
                this.AddMoveAI(new LeftDownMoveAI(this, 0, 120));
                this.AddMoveAI(new NoneAI(this, 1, 100));
                this.AddMoveAI(new LeftMoveAI(this, 2, 1000));


                for (int i = 0; i < 36 ; i ++)
                {
                 //   this.AddActionAI(new StarsShotAI(this, 1 + 3 * i, 1, i * 30, i % 6));
                //    this.AddActionAI(new SmallStarsShotAI(this, 2 + 3 * i, 1, SSTaskFactory.PlayerUpdateTask.Player.GetPosition(), i % 4));

                    this.AddActionAI(new DirectionShotAI<StarsShot>(this, 1 + 3 * i, 1, i % 6, i * 30));
                    this.AddActionAI(new PlayerAimShotAI<SmallStarsShot>(this, 2 + 3 * i, 1, i % 4));
                    this.AddActionAI(new NoneAI(this, 3 + 3 * i, 5));
               
                }

                this.AddActionAI(new NoneAI(this, 2 + 2 * 360, 1000));

            }
            else
            {
                this.AddMoveAI(new RightDownMoveAI(this, 0, 100));
            }
        }
    }
}
