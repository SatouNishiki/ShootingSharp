using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class ItemDrawTask : interfaces.ITask
    {
        public List<entity.item.Item> ItemList { get; set; }

        public ItemDrawTask()
        {
            this.ItemList = new List<entity.item.Item>();
        }

        public void Run()
        {
            foreach (var item in this.ItemList)
            {
                item.Draw();
            }

            this.ItemList.RemoveAll(i => !i.IsLiving());
        }
    }
}
