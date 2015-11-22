using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.ai;
using ShootingSharp.entity;
using DebugProject.shot;
using ShootingSharp.position;

namespace DebugProject.ai
{
    public class StarsShotAI : AITask
    {
        private double theta { get; set; }
        private int color;
        private SSPosition target;

        public StarsShotAI(Entity entity, int priority, int frame, double theta, int color)
            : base(entity, priority, frame)
        {
            this.theta = theta;
            this.color = color;
        }

        public StarsShotAI(Entity entity, int priority, int frame, SSPosition target, int color)
            : base(entity, priority, frame)
        {
            this.target = target;
            this.color = color;
        }

        protected override void RunMethod()
        {
            if (this.target == null)
                entity.Scene.AddShot(new StarsShot(entity, this.theta, this.color));
            else
                entity.Scene.AddShot(new StarsShot(entity, this.target, this.color));
        }
    }
}
