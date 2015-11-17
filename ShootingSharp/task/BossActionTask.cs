using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity.boss;

namespace ShootingSharp.task
{
    public class BossActionTask : ITask
    {
        public List<Boss> BossList { get; set; }

        public BossActionTask()
        {
            this.BossList = new List<Boss>();
        }

        public void Run()
        {
            List<Boss> list = new List<Boss>();

            foreach (var s in this.BossList)
            {
                s.DoAction();

                if (!s.IsLiving())
                    list.Add(s);
            }

            //死んでるやつを削除
            this.BossList.RemoveAll(shot => list.IndexOf(shot) >= 0);
        }
    }
}
