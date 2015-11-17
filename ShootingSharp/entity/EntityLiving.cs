using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.position;

namespace ShootingSharp.entity
{
    /// <summary>
    /// 生きてます
    /// </summary>
    public abstract class EntityLiving : Entity, IHasHealth
    {
        /// <summary>
        /// ヒットポイント
        /// </summary>
        public int Life { get; set; }

        public int MaxLife { get; set; }

        public EntityLiving() : base()
        {
            this.Life = 1;
            this.MaxLife = this.Life;
        }

        /// <summary>
        /// 生きてる?
        /// </summary>
        /// <returns></returns>
        public override bool IsLiving()
        {
            return this.Life > 0;
        }

        public abstract void OnDeath();

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!this.IsLiving())
            {
                this.OnDeath();
            }
            
        }

    }
}
