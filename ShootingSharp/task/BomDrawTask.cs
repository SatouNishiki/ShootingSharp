using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class BomDrawTask : interfaces.ITask
    {
        public List<entity.bom.Bom> BomList { get; set; }

        public BomDrawTask()
        {
            this.BomList = new List<entity.bom.Bom>();
        }

        public void Run()
        {
            foreach (var item in this.BomList)
            {
                item.Draw();
            }

            this.BomList.RemoveAll(b => !b.IsLiving());
        }
    }
}
