using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.ai
{
    /// <summary>
    /// なにもしません
    /// </summary>
    public class NoneAI : AITask
    {
         public NoneAI(entity.Entity entity, int priority, int frame)
            : base(entity, priority, frame)
        {

        }


        protected override void RunMethod()
        {
        }
    }
}
