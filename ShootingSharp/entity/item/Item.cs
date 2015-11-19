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

        public Item()
            : base()
        {
            this.loader = TextureLoader.GetInstance();
            this.moveSpeed = 2;
        }

        public override void OnDeath()
        {
            //なにもなし
        }

        public override void OnInteract(Entity entity)
        {
            if (entity is EntityPlayer)
            {
                this.AddItemEffect(((EntityPlayer)entity));
                this.Life = 0;
            }
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

        public override void Move()
        {
            this.position.PosY += this.moveSpeed;
        }

        public override void DoAction()
        {
            
        }

        public override int GetRadius()
        {
            return this.GetTextureSize().Width > this.GetTextureSize().Height ? this.GetTextureSize().Height : this.GetTextureSize().Width;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        /// <summary>
        /// 引数のプレイヤーに効果を与える
        /// </summary>
        /// <param name="player"></param>
        public abstract void AddItemEffect(EntityPlayer player);
    }
}
