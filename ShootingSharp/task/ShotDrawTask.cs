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
            foreach (var s in this.ShotList)
            {
                s.Draw();

            }

            //死んでるやつを削除
            this.ShotList.RemoveAll(shot => !shot.IsLiving());
        }
    }
}
