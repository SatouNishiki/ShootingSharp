﻿using System;
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

        public Bom() : base()
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
        {
            DX.DrawRotaGraph(this.position.PosX, this.position.PosY, (double)this.drawCount / 3.0D, (double)this.drawCount / 10.0D,
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
            int a = 0;

            if (this.GetTextureSize().Width < this.GetTextureSize().Height)
            {
                a = this.GetTextureSize().Height;
            }
            else
            {
                a = this.GetTextureSize().Width;
            }

            return (int)Math.Round((a * ((double)this.drawCount / 3.0D)));
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SSTaskFactory.ShotDrawTask.ShotList.ForEach(s => s.Life = 0);
            SSTaskFactory.ShotMoveTask.ShotList.ForEach(s => s.Life = 0);
            SSTaskFactory.ShotUpdateTask.ShotList.ForEach(s => s.Life = 0);

            if (this.GetRadius() > SSGame.GetInstance().GetBattleWindowSize().Width)
            {
                this.Life = 0;
            }
        }
    }
}
