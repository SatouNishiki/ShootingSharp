using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class MoveTaskManager : TaskManager
    {
        public MoveTaskManager()
        {
            this.AddTask(SSTaskFactory.PlayerMoveTask);
            this.AddTask(SSTaskFactory.ShotMoveTask);
            this.AddTask(SSTaskFactory.EnemyMoveTask);
            this.AddTask(SSTaskFactory.BossMoveTask);
            this.AddTask(SSTaskFactory.ItemMoveTask);
        }
    }
}
