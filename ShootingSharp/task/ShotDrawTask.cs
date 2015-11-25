﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.entity;

namespace ShootingSharp.task
{
    public class ShotDrawTask : interfaces.ITask
    {
        public List<Shot> ShotList { get; private set; }

        public ShotDrawTask()
        {
            this.ShotList = new List<entity.shot.Shot>();
        }

        public void Run()
        {
            for (int i = 0; i < this.ShotList.Count; i++)
            {
                this.ShotList[i].Draw();

            }
        }

        public void AddShot(Shot shot)
        {
            shot.OnDispose += this.DeathAt;
            this.ShotList.Add(shot);
        }

        private void DeathAt(Entity entity)
        {
            this.ShotList.Remove((Shot)entity);
        }
    }
}
