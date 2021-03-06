﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.task
{
    public class DrawTaskManager : TaskManager
    {
        public DrawTaskManager()
        {
            this.AddTask(SSTaskFactory.PlayerDrawTask);
            this.AddTask(SSTaskFactory.ShotDrawTask);
            this.AddTask(SSTaskFactory.EnemyDrawTask);
            this.AddTask(SSTaskFactory.BossDrawTask);
            this.AddTask(SSTaskFactory.ItemDrawTask);
            this.AddTask(SSTaskFactory.BomDrawTask);
        }

    }
}
