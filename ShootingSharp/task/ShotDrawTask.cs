using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;

namespace ShootingSharp.task
{
    public class ShotDrawTask : interfaces.ITask
    {
        public List<Shot> ShotList { get; set; }

        public ShotDrawTask()
        {
            this.ShotList = new List<entity.shot.Shot>();
        }

        public void Run()
        {
            List<Shot> list = new List<Shot>();

            foreach (var s in this.ShotList)
            {
                s.Draw();

                if (!s.IsLiving())
                    list.Add(s);
            }

            //死んでるやつを削除
            this.ShotList.RemoveAll(shot => list.IndexOf(shot) >= 0);
        }
    }
}
