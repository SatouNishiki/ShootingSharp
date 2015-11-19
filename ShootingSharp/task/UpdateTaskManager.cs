using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.task
{
    /// <summary>
    /// 全アップデート実行
    /// </summary>
    public class UpdateTaskManager : TaskManager
    {
        public UpdateTaskManager()
        {
            this.AddTask(SSTaskFactory.PlayerUpdateTask);
            this.AddTask(SSTaskFactory.ShotUpdateTask);
            this.AddTask(SSTaskFactory.EnemyUpdateTask);
            this.AddTask(SSTaskFactory.BossUpdateTask);
            this.AddTask(SSTaskFactory.ItemUpdateTask);
        }

    }
}
