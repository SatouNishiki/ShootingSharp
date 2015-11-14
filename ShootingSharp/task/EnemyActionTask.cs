using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.task
{
    public class EnemyActionTask : interfaces.ITask
    {
        public List<Enemy> EnemyList { get; set; }

        public EnemyActionTask()
        {
            this.EnemyList = new List<Enemy>();
        }

        public void Run()
        {
            foreach (var e in this.EnemyList)
            {
                e.DoAction();
            }
        }
    }
}
