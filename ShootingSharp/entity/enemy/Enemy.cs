using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;
using ShootingSharp.texture;
using DxLibDLL;
using ShootingSharp.task;

namespace ShootingSharp.entity.enemy
{
    public abstract class Enemy : LivingAnimationEntity
    {
        /// <summary>
        /// このエネミーが出現するまでのフレーム数
        /// </summary>
        protected int popCount;

        protected TextureLoader loader;

        /// <summary>
        /// こいつが倒された時のスコア
        /// </summary>
        protected int score;

        public Action<int> KilledByPlayer;

        protected collid.CircleCollider collider;


        public Enemy() : base()
        {
            this.loader = TextureLoader.GetInstance();
            this.score = 10;
            this.KilledByPlayer += this.OnKilled;
            this.collider = new collid.CircleCollider(this.GetType(), typeof(Enemy), typeof(item.Item));
            this.collider.Radius = this.GetRadius();
          
        }

        public void OnPop()
        {
            this.Scene.PopEnemy(this);
        }

        public override void OnInteract(collid.CollitionInfo info)
        {
            if (typeof(EntityPlayer).IsAssignableFrom(info.CollitionObjectType))
            {
                //とりあえずこっちは何もしない(体当たりで死んだらアレなので)
                return;
            }

            //ライフを1減らす
            this.Life--;

            if (!this.IsLiving())
            {
                if (this.KilledByPlayer != null)
                {
                    this.KilledByPlayer(this.score);
                }

                
            }
        }

        
        public void SetPopCount(int count)
        {
            this.popCount = count;
        }

        public int GetPopCount()
        {
            return this.popCount;
        }


        public override position.SSPosition GetTexturePosition()
        {

            tempPos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            tempPos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return tempPos;
        }

        public override void Draw()
        {

            DX.DrawExtendGraph(
                this.GetTexturePosition().PosX,
                this.GetTexturePosition().PosY,
                this.GetTexturePosition().PosX + this.GetTextureSize().Width + 1,
                this.GetTexturePosition().PosY + this.GetTextureSize().Height + 1,
                this.loader.Textures[this.GetTextureName()],
                DX.TRUE);
        }

        public override void OnDeath()
        {
           
        }

        protected virtual item.Item GetDropItem()
        {
            return new item.ItemSmallPower();
        }

        private void OnKilled(int score)
        {
            Random rnd = new Random();
            int r = rnd.Next(100);
           
            if (r < 30)
            {
                item.Item i = this.GetDropItem();

                if (i == null)
                    return;

                i.SetPosition(this.position);
                this.Scene.AddItem(i);
            }
        }

        public override collid.ColliderBase GetCollider()
        {
            return this.collider;
        }

        public abstract int GetRadius();

    }
}
