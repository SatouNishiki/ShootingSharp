using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public abstract class PlayerCircleShot : CircleShot
    {/*
          /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        public PlayerCircleShot(SSPosition position)
            : base(position)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="theta">角度</param>
        public PlayerCircleShot(SSPosition position, double theta)
            : base(position , theta)
        {

        }


        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public PlayerCircleShot(IHasSSPosition shooter)
            : base(shooter)
        {
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        /// <param name="theta">角度</param>
        public PlayerCircleShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
        }

        public PlayerCircleShot(IHasSSPosition shooter, SSPosition target)
            : base(shooter, target)
        {
        }
        */

        public PlayerCircleShot(Builder builder)
            : base(builder)
        {

        }
        protected override void Init()
        {
            base.Init();
            this.collider.AddNoCollitionTypes(typeof(entity.EntityPlayer), typeof(bom.Bom));
        }
    }
}
