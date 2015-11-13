using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class PlayerDrawTask : interfaces.ITask
    {
        public entity.EntityPlayer Player { get; set; }

        public void Run()
        {
            if (Player != null)
            {
                Player.Draw();
            }
        }
    }
}
