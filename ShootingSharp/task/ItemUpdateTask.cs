using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class ItemUpdateTask : interfaces.ITask
    {
        public List<entity.item.Item> ItemList { get; set; }

        public ItemUpdateTask()
        {
            this.ItemList = new List<entity.item.Item>();
        }

        public void Run()
        {
            foreach (var item in this.ItemList)
            {
                item.OnUpdate();
            }

            this.ItemList.RemoveAll(i => !i.IsLiving());
        }
    }
}
