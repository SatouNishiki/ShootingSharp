﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.task
{
    public class EnemyDrawTask : interfaces.ITask
    {
        public List<Enemy> EnemyList { get; set; }

        public EnemyDrawTask()
        {
            this.EnemyList = new List<Enemy>();
        }

        public void Run()
        {
            foreach (var s in this.EnemyList)
            {
                s.Draw();
            }

            this.EnemyList.RemoveAll(enemy => !enemy.IsLiving());
        }
    }
}
