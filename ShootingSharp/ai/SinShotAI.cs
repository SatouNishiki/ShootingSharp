using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;

namespace ShootingSharp.ai
{
    public class SinShotAI<T> : ShotAITask<T> where T : Shot
    {

        public SinShotAI(entity.Entity entity, int priority, int frame, int meta)
            : base(entity, priority, frame, meta)
        {
        }

        public override Shot GetShot()
        {
            return new Shot.Builder(typeof(T)).Position(entity.GetPosition()).Type(Shot.ShotType.Sin).CircleRadius(7).LoopSpeed(10).Build();
        }
    }
}
