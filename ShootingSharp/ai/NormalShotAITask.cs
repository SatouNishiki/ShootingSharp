using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;

namespace ShootingSharp.ai
{
    public class NormalShotAITask<T> : ShotAITask<T> where T : Shot
    {
        public NormalShotAITask(entity.Entity entity, int priority, int frame, int meta)
            : base(entity, priority, frame, meta)
        {

        }

        public override Shot GetShot()
        {
            return new Shot.Builder(typeof(T)).Position(entity.GetPosition()).Type(Shot.ShotType.Normal).MetaData(this.shotMetaData).Build();
        }
    }
}
