using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;

namespace ShootingSharp.entity.item
{
    public abstract class Item : EntityLiving
    {
        protected TextureLoader loader;
        protected collid.SquareCollider collider;

        public Item()
            : base()
        {
            this.loader = TextureLoader.GetInstance();
            this.MoveSpeed = 2;

            this.collider = new collid.SquareCollider(this.GetType(), null);
            this.collider.SquarePosition =
                new position.SquareSSPositon(
                    new position.SSPosition(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY),
                    new position.SSPosition(this.GetTexturePosition().PosX,  this.GetTexturePosition().PosY + this.GetTextureSize().Height),
                    new position.SSPosition(this.GetTexturePosition().PosX + this.GetTextureSize().Width, this.GetTexturePosition().PosY),
                    new position.SSPosition(this.GetTexturePosition().PosX + this.GetTextureSize().Width, this.GetTexturePosition().PosY + this.GetTextureSize().Height));
        }

        public override void OnDeath()
        {
            //なにもなし
        }

        public override void OnInteract(collid.CollitionInfo info)
        {
            if (typeof(EntityPlayer).IsAssignableFrom(info.CollitionObjectType))
            {
                this.AddItemEffect(((EntityPlayer)info.CollitionInteractor));
                this.Life = 0;
            }
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

        public override void Move()
        {
            this.position.PosY += this.MoveSpeed;
        }

        public override void DoAction()
        {
            
        }
/*
        public virtual int GetRadius()
        {
            return this.GetTextureSize().Width > this.GetTextureSize().Height ? this.GetTextureSize().Width : this.GetTextureSize().Height;
        }
        */
        /// <summary>
        /// 引数のプレイヤーに効果を与える
        /// </summary>
        /// <param name="player"></param>
        public abstract void AddItemEffect(EntityPlayer player);

        public override collid.ColliderBase GetCollider()
        {
            return this.collider;
        }
    }
}
