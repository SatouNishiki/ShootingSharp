using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.ai;
using ShootingSharp.interfaces;
using ShootingSharp.position;
using ShootingSharp.entity;
using DebugProject.shot;

namespace DebugProject.ai
{
    public class CircleShotAI : AITask
    {
        private double theta { get; set; }
        private int color;
        private SSPosition target;
        private IHasSSPosition target2;

        public CircleShotAI(Entity entity, int priority, int frame, double theta, int color)
            : base(entity, priority, frame)
        {
            this.theta = theta;
            this.color = color;
        }

        public CircleShotAI(Entity entity, int priority, int frame, SSPosition target, int color)
            : base(entity, priority, frame)
        {
            this.target = target;
            this.color = color;
        }

        public CircleShotAI(Entity entity, int priority, int frame, IHasSSPosition target, int color)
            : base(entity, priority, frame)
        {
            this.target2 = target;
            this.color = color;
        }

        protected override void RunMethod()
        {
            if (this.target == null && this.target2 == null)
                entity.Scene.AddShot(new CirclesShot(entity, this.theta, this.color));
            else if (this.target == null)
                entity.Scene.AddShot(new CirclesShot(entity, ShootingSharp.task.SSTaskFactory.PlayerUpdateTask.Player, this.color));
            else
                entity.Scene.AddShot(new CirclesShot(entity, this.target, this.color));
        }
    }
}
