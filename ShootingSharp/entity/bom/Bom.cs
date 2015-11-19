using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;

namespace ShootingSharp.entity.bom
{
    public abstract class Bom : EntityLiving
    {
        protected TextureLoader loader;
        private int drawCount;

        public Bom()
        {
            this.loader = TextureLoader.GetInstance();
        }

        public override void OnDeath()
        {
            
        }

        public override void OnInteract(Entity entity)
        {
            //なにもしない(貫通)
        }

      
        public override position.SSPosition GetTexturePosition()
        {
            ShootingSharp.position.SSPosition pos = new position.SSPosition();

            pos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            pos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return pos;
        }

        public override void Draw()
        {/*
            DX.DrawExtendGraph(
                 this.GetTexturePosition().PosX,
                 this.GetTexturePosition().PosY,
                 this.GetTexturePosition().PosX + this.GetTextureSize().Width + 1,
                 this.GetTexturePosition().PosY + this.GetTextureSize().Height + 1,
                 this.loader.Textures[this.GetTextureName()],
                 DX.TRUE);
          */

            DX.DrawRotaGraph(this.position.PosX, this.position.PosY, this.drawCount, (double)this.drawCount / 4.0D,
                this.loader.Textures[this.GetTextureName()], DX.TRUE);

            this.drawCount++;

        }

        public override void Move()
        {
          //移動しない
        }

        public override void DoAction()
        {

        }

        public override int GetRadius()
        {
            return this.GetTextureSize().Width > this.GetTextureSize().Height ? this.GetTextureSize().Width : this.GetTextureSize().Height;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }
    }
}
