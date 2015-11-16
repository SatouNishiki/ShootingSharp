using System;
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
            List<Enemy> temp = new List<Enemy>();

            foreach (var e in this.EnemyList)
            {
                //出現フレーム数になったら
                if ((UInt64)e.GetPopCount() == this.count)
                {
                    //各タスクに登録(=出現)
                    SSTaskFactory.EnemyActionTask.EnemyList.Add(e);
                    SSTaskFactory.EnemyDrawTask.EnemyList.Add(e);
                    SSTaskFactory.EnemyMoveTask.EnemyList.Add(e);
                    SSTaskFactory.EnemyUpdateTask.EnemyList.Add(e);
                    e.KilledByPlayer += SSTaskFactory.InfoDrawTask.OnEnemyKilled;
                    temp.Add(e);
                }
            }

            this.EnemyList.RemoveAll(e => temp.IndexOf(e) >= 0);

            this.count++;
        }
    }
}
