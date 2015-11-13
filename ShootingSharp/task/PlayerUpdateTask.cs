using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity;

namespace ShootingSharp.task
{
    public class PlayerUpdateTask : ITask
    {
        public EntityPlayer Player { get; set; }

        public void Run()
        {
            if (this.Player != null)
                Player.OnUpdate();
        }
    }
}
