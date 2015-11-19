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

        public Enemy() : base()
        {
            this.loader = TextureLoader.GetInstance();
            this.score = 10;
            this.KilledByPlayer += this.OnKilled;
            this.friendCode = "enemy";
        }

        public void OnPop()
        {
            this.InteractManager.AddInteractObject(this);
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
            ShootingSharp.position.SSPosition pos = new position.SSPosition();

            pos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            pos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return pos;
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

                this.InteractManager.AddInteractObject(i);
                SSTaskFactory.ItemDrawTask.ItemList.Add(i);
                SSTaskFactory.ItemMoveTask.ItemList.Add(i);
                SSTaskFactory.ItemUpdateTask.ItemList.Add(i);
            }
        }
        
    }
}
