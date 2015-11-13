using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.task
{
    public class PlayerMoveTask : ITask
    {
        public entity.EntityPlayer Player { get; set; }

        public void Run()
        {
            if (Player != null)
            {
                Player.Move();
            }
        }
    }
}
