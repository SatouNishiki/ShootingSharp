using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;
using ShootingSharp.task;

namespace ShootingSharp.entity.bom
{
    public abstract class Bom : EntityLiving
    {
        protected TextureLoader loader;
        private int drawCount;
        protected collid.CircleCollider collider;
        private collid.CollitionInfo info;

        public Bom() : base()
        {
            this.loader = TextureLoader.GetInstance();
            this.collider = new collid.CircleCollider(this.GetType(), typeof(EntityPlayer), typeof(item.Item));
            this.collider.Radius = this.GetRadius();
            this.info = new collid.CollitionInfo();
            info.CollitionObjectType = this.GetType();
            info.CollitionInteractor = this;
        }

        public override void OnDeath()
        {
            
        }

        public override void OnInteract(collid.CollitionInfo info)
        {
            //なにもしない(貫通)
        }

      
        public override position.SSPosition GetTexturePosition()
        {

            tempPos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            tempPos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return tempPos;
        }

        public override void Draw()
        {
            DX.DrawRotaGraph(this.position.PosX, this.position.PosY, (double)this.drawCount / 2.0D, (double)this.drawCount / 10.0D,
                this.loader.Textures[this.GetTextureName()], DX.TRUE);

            this.drawCount++;

         //   this.collider.Radius = this.GetRadius();

        }

        public override void Move()
        {
          //移動しない
        }

        public override void DoAction()
        {

        }

        public virtual int GetRadius()
        {
            int a = 0;

            if (this.GetTextureSize().Width < this.GetTextureSize().Height)
            {
                a = this.GetTextureSize().Height;
            }
            else
            {
                a = this.GetTextureSize().Width;
            }

            return (int)Math.Round((a * ((double)this.drawCount / 2.0D)));
        }


        public override void OnUpdate()
        {
            base.OnUpdate();

            SSTaskFactory.ShotDrawTask.ShotList.ForEach(s => s.Life = 0);
            SSTaskFactory.ShotMoveTask.ShotList.ForEach(s => s.Life = 0);
            SSTaskFactory.ShotUpdateTask.ShotList.ForEach(s => s.Life = 0);

            SSTaskFactory.EnemyUpdateTask.EnemyList.ForEach(s => s.OnInteract(info));


            if (this.GetRadius() > SSGame.GetInstance().GetBattleWindowSize().Width)
            {
                this.Life = 0;
            }
        }

        public override collid.ColliderBase GetCollider()
        {
            return this.collider;
        }
    }
}
