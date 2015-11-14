using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.task
{
    public class EnemyMoveTask : interfaces.ITask
    {
        public List<Enemy> EnemyList { get; set; }

        public EnemyMoveTask()
        {
            this.EnemyList = new List<Enemy>();
        }

        public void Run()
        {
            foreach (var e in this.EnemyList)
            {
                e.Move();
            }
        }
    }
}
