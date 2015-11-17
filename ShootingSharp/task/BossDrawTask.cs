using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class BossDrawTask : interfaces.ITask
    {
        public List<entity.boss.Boss> BossList { get; set; }

        public BossDrawTask()
        {
            this.BossList = new List<entity.boss.Boss>();
        }

        public void Run()
        {
            List<entity.boss.Boss> list = new List<entity.boss.Boss>();

            foreach (var s in this.BossList)
            {
                s.Draw();

                if (!s.IsLiving())
                    list.Add(s);
            }

            //死んでるやつを削除
            this.BossList.RemoveAll(shot => list.IndexOf(shot) >= 0);
        }
    }
}
