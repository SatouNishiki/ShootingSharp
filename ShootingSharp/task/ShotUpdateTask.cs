using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;

namespace ShootingSharp.task
{
    public class ShotUpdateTask : interfaces.ITask
    {
        public List<Shot> ShotList { get; set; }

        public ShotUpdateTask()
        {
            this.ShotList = new List<entity.shot.Shot>();
        }

        public void Run()
        {
            foreach (var s in this.ShotList)
            {

                s.OnUpdate();

            }

            //死んでるやつを削除
            this.ShotList.RemoveAll(shot => !shot.IsLiving());
        }
    }
}
