using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class BossUpdateTask : interfaces.ITask
    {
         public List<entity.boss.Boss> BossList { get; set; }
         private int runCount;

         public BossUpdateTask()
        {
            this.BossList = new List<entity.boss.Boss>();
        }

        public void Run()
        {
            List<entity.boss.Boss> list = new List<entity.boss.Boss>();

            foreach (var s in this.BossList)
            {
                s.OnUpdate();

                if (!s.IsLiving())
                    list.Add(s);
            }

            //死んでるやつを削除
            this.BossList.RemoveAll(shot => list.IndexOf(shot) >= 0);

            if (this.BossList.Count > 0)
            {
                if (runCount * 10 <= (this.BossList[0].Life * 100) / this.BossList[0].MaxLife)
                {
                    SSTaskFactory.InfoDrawTask.BossHPPercent = runCount * 10;
                    this.runCount++;
                }

                else
                    SSTaskFactory.InfoDrawTask.BossHPPercent = (this.BossList[0].Life * 100) / this.BossList[0].MaxLife;
            }
        }
    }
}
