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
        protected collid.CircleCollider collider;

        public Boss() : base()
        {
            loader = TextureLoader.GetInstance();
            this.position.PosX = SSGame.GetInstance().GetBattleWindowSize().Width / 2;
            this.position.PosY = 100;
            this.collider = new collid.CircleCollider(this.GetType(), typeof(EntityPlayer));
            this.collider.Radius = this.GetRadius();
            
        }

        public void OnPop()
        {
            this.Scene.PopBoss(this);
        }


        public override void OnInteract(collid.CollitionInfo info)
        {
            if (typeof(EntityPlayer).IsAssignableFrom(info.CollitionObjectType))
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

        public abstract string GetCutinTextureName();

        public abstract string GetName();

        public abstract string GetMusicName();

        public abstract int GetRadius();

        public override collid.ColliderBase GetCollider()
        {
            return this.collider;
        }
    }
}
