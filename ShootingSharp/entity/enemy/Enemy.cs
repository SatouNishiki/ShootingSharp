using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;

namespace ShootingSharp.entity.enemy
{
    public abstract class Enemy : EntityLiving
    {
        /// <summary>
        /// このエネミーが出現するまでのフレーム数
        /// </summary>
        protected int popCount;

        public Enemy() : base()
        {

        }

        public override void OnInteract(Entity entity)
        {
            //プレイヤーだったら
            if (entity is EntityPlayer)
            {
                //とりあえずこっちは何もしない(ボスが体当たりで死んだらアレなので)
                return;
            }

            //ライフを1減らす
            this.Life--;
        }

        
        public void SetPopCount(int count)
        {
            this.popCount = count;
        }

        public int GetPopCount()
        {
            return this.popCount;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(this.position.PosX > SSGame.GetInstance().GetWindowSize().Width
                || this.position.PosY > SSGame.GetInstance().GetWindowSize().Height)
            {
                this.Life = 0;
            }
        }
    }
}
