using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;

namespace ShootingSharp.ai
{
    public class DirectionShotAI<T> : ShotAITask<T> where T : Shot
    {
        private double theta;

        public DirectionShotAI(entity.Entity entity, int priority, int frame, int meta, double theta)
            : base(entity, priority, frame, meta)
        {
            this.theta = theta;
        }

        public override Shot GetShot()
        {
            return new Shot.Builder(typeof(T)).Position(entity.GetPosition()).Type(Shot.ShotType.Direction).MetaData(this.shotMetaData).Theta(this.theta).Build();
        }
    }
}
