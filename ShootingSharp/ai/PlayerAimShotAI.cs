using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.task;

namespace ShootingSharp.ai
{
    public class PlayerAimShotAI<T> : ShotAITask<T> where T : Shot
    {
        public PlayerAimShotAI(entity.Entity entity, int priority, int frame, int meta)
            : base(entity, priority, frame, meta)
        {

        }

        public override Shot GetShot()
        {
         //   return new Shot.Builder(typeof(T)).Position(entity.GetPosition()).Type(Shot.ShotType.Aim).MetaData(this.shotMetaData).Target(SSTaskFactory.PlayerUpdateTask.Player.GetPosition()).Build();
            return new AimShotBuilder<T>(entity, shotMetaData, SSTaskFactory.PlayerUpdateTask.Player.GetPosition()).CreateShot();
        }
    }

}
