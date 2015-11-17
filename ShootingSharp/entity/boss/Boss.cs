using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;

namespace ShootingSharp.entity.boss
{
    public abstract class Boss : LivingAnimationEntity
    {
        protected TextureLoader loader;

        public Boss() : base()
        {
            loader = TextureLoader.GetInstance();
            this.position.PosX = SSGame.GetInstance().GetBattleWindowSize().Width / 2;
            this.position.PosY = 100;
        }

        public void OnPop()
        {
            this.InteractManager.AddInteractObject(this);
        }


        public override void OnInteract(Entity entity)
        {
            if (entity is EntityPlayer)
            {
                return;
            }

            this.Life--;
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

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public abstract string GetCutinTextureName();

        public abstract string GetName();
    }
}
