using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public abstract class CircleShot : Shot
    {/*
        public CircleShot()
            : base()
        {

        }

         /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        public CircleShot(SSPosition position)
            : base(position)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="theta">角度</param>
        public CircleShot(SSPosition position, double theta)
            : base(position , theta)
        {

        }


        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public CircleShot(IHasSSPosition shooter)
            : base(shooter)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        /// <param name="theta">角度</param>
        public CircleShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
        }

        public CircleShot(IHasSSPosition shooter, SSPosition target)
            : base(shooter, target)
        {
        }

        public CircleShot(IHasSSPosition shooter, IHasSSPosition target)
            : base(shooter, target)
        {

        }*/

        public CircleShot(Builder builder)
            : base(builder)
        {

        }

        protected override void Init()
        {
            base.Init();

            collid.CircleCollider c = new collid.CircleCollider(this.GetType(), null);
            foreach (var n in noCollitionTypes)
            {
                c.AddNoCollitionTypes(n);
            }
            c.Radius = this.GetRadius();
            this.collider = c;
        }

        public abstract int GetRadius();
    }
}
