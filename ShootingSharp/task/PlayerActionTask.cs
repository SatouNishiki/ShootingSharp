using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class PlayerActionTask : interfaces.ITask
    {
        public entity.EntityPlayer Player { get; set; }

        public void Run()
        {
            if (this.Player != null)
            {
                this.Player.DoAction();
            }
        }
    }
}
