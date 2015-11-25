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
        public EnemyCircleShot() : base() { }

          /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        public EnemyCircleShot(SSPosition position)
            : base(position)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="theta">角度</param>
        public EnemyCircleShot(SSPosition position, double theta)
            : base(position , theta)
        {

        }


        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public EnemyCircleShot(IHasSSPosition shooter)
            : base(shooter)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        /// <param name="theta">角度</param>
        public EnemyCircleShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
        }

        public EnemyCircleShot(IHasSSPosition shooter, SSPosition target)
            : base(shooter, target)
        {
        }

        public EnemyCircleShot(IHasSSPosition shooter, IHasSSPosition target)
            : base(shooter, target)
        {

        }
        */

        public EnemyCircleShot(Builder builder)
            : base(builder)
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
