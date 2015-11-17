using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class ActionTaskManager : TaskManager
    {
        public ActionTaskManager()
        {
            this.AddTask(SSTaskFactory.PlayerActionTask);
            this.AddTask(SSTaskFactory.EnemyActionTask);
            this.AddTask(SSTaskFactory.BossActionTask);
        }
    }
}
