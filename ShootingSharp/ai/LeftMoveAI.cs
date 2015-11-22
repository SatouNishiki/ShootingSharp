using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.ai
{
    public class LeftMoveAI : AITask
    {
        public LeftMoveAI(entity.Entity entity, int priority, int frame)
            : base(entity, priority, frame)
        {

        }


        protected override void RunMethod()
        {
            this.entity.GetPosition().PosX -= this.entity.MoveSpeed;
        }
    }
}
