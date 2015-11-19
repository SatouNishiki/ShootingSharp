using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class ItemMoveTask : interfaces.ITask
    {
        public List<entity.item.Item> ItemList { get; set; }

        public ItemMoveTask()
        {
            this.ItemList = new List<entity.item.Item>();
        }

        public void Run()
        {
            foreach (var item in this.ItemList)
            {
                item.Move();
            }

            this.ItemList.RemoveAll(i => !i.IsLiving());
        }
    }
}
