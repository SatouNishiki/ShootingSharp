using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;

namespace ShootingSharp.ai
{
    public class SinShotAI<T> : AITask where T : Shot,new()
    {

        public SinShotAI(entity.Entity entity, int priority, int frame)
            : base(entity, priority, frame)
        {
        }

        protected override void RunMethod()
        {
            T shot = new T();
            shot.SinShotTheta = 30;
            shot.Type = Shot.ShotType.Sin;
            shot.SetMoveSpeed(3);
            shot.SetPosition(new position.SSPosition(entity.GetPosition().PosX, entity.GetPosition().PosY));
            entity.Scene.AddShot(shot);
        }
    }
}
