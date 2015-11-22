﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.ai
{
    public class DownMoveAI : AITask
    {
        public DownMoveAI(entity.Entity entity, int priority, int frame)
            : base(entity, priority, frame)
        {

        }

        protected override void RunMethod()
        {
            entity.GetPosition().PosY += entity.MoveSpeed;

        }
    }
}
