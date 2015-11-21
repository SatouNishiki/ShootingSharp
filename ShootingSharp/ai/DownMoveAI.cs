using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.ai
{
    public class DownMoveAI : AIMicroThread
    {
        public DownMoveAI(entity.Entity entity, int priority, int frame)
            : base(entity, priority, frame)
        {

        }

        protected override IEnumerator<object> MicroThread()
        {
            entity.GetPosition().PosY++;

            yield return null;
        }
    }
}
