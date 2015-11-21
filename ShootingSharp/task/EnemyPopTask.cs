﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.task
{
    public class EnemyPopTask : ITask
    {
        private UInt64 count;

        public List<Enemy> EnemyList { get; set; }

        public EnemyPopTask()
        {
            this.count = 0;
            this.EnemyList = new List<Enemy>();
        }

        public void Run()
        {
            foreach (var e in this.EnemyList)
            {
                //出現フレーム数になったら
                if ((UInt64)e.GetPopCount() == this.count)
                {
                    e.KilledByPlayer += SSTaskFactory.InfoDrawTask.OnEnemyKilled;
                    e.OnPop();
                }
            }

            this.EnemyList.RemoveAll(enemy => !enemy.IsLiving());

            this.count++;
        }
    }
}
