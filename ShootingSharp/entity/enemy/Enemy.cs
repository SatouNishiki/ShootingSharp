﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;
using ShootingSharp.texture;
using DxLibDLL;

namespace ShootingSharp.entity.enemy
{
    public abstract class Enemy : LivingAnimationEntity
    {
        /// <summary>
        /// このエネミーが出現するまでのフレーム数
        /// </summary>
        protected int popCount;

        protected TextureLoader loader;

        public Enemy() : base()
        {
            this.loader = TextureLoader.GetInstance();
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
    }
}
