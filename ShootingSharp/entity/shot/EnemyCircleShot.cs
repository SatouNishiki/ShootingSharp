using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public abstract class EnemyCircleShot : CircleShot
    {
        /*
        public EnemyCircleShot(Builder builder)
            : base(builder)
        {

        }*/

        public EnemyCircleShot()
            : base()
        {

        }
        protected override void Init()
        {
            base.Init();
            this.collider.AddNoCollitionTypes(typeof(enemy.Enemy));
            this.collider.AddNoCollitionTypes(typeof(boss.Boss));
        }
    }
}
