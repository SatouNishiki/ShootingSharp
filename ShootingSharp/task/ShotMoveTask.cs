using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity.shot;
using ShootingSharp.entity;

namespace ShootingSharp.task
{
    public class ShotMoveTask : ITask
    {
        public List<Shot> ShotList { get; private set; }

        public ShotMoveTask()
        {
            this.ShotList = new List<Shot>();
        }

        public void Run()
        {

            for (int i = 0; i < this.ShotList.Count; i++)
            {
                this.ShotList[i].Move();
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
