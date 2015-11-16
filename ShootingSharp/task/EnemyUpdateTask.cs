﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.task
{
    public class EnemyUpdateTask : interfaces.ITask
    {
        public List<Enemy> EnemyList { get; set; }

        public EnemyUpdateTask()
        {
            this.EnemyList = new List<Enemy>();
        }

        public void Run()
        {
            List<Enemy> list = new List<Enemy>();

            foreach (var s in this.EnemyList)
            {
                s.OnUpdate();

                if (!s.IsLiving())
                {
                    list.Add(s);
                }
            }

            //死んでるやつを削除
            this.EnemyList.RemoveAll(shot => list.IndexOf(shot) >= 0);
        }
    }
}