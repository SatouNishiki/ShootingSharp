﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.entity.shot;

namespace ShootingSharp.entity.player
{
    public class EntityReimu : EntityPlayer
    {
        private const int size = 35;

        public EntityReimu()
            : base()
        {
            this.MoveSpeed = 6;
            this.shotInterval = 5;

            int loadTextureSize = 1024 / 21;
            this.textureLoader.LoadSprite("reimu.png", 21, 3, 19, loadTextureSize, loadTextureSize);
            this.textureSize = new System.Drawing.Size(size, size);
        }

        public override string GetUpTextureName()
        {
            return "reimu.png10";
        }

        public override string GetDownTextureName()
        {
            return "reimu.png8";
        }

        public override string GetLeftTextureName()
        {
            return "reimu.png0";
        }

        public override string GetCenterTextureName()
        {
            return "reimu.png9";
        }

        public override string GetRightTextureName()
        {
            return "reimu.png18";
        }

        public override string GetRightUpTextureName()
        {
            return "reimu.png13";
        }

        public override string GetRightDownTextureName()
        {
            return "reimu.png11";
        }

        public override string GetLeftUpTextureName()
        {
            return "reimu.png5";
        }

        public override string GetLeftDownTextureName()
        {
            return "reimu.png7";
        }

        /*
        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(textureSize, textureSize);
        }
        */
        public override int GetRadius()
        {
           //return this.GetTextureSize().Height > this.GetTextureSize().Width ? this.GetTextureSize().Width / 4 : this.GetTextureSize().Height / 4;
            return 1;
        }

        protected override shot.Shot GetShot()
        {
          //  return new ReimuNormalShot(this);
        //    return new Shot.Builder(typeof(ReimuNormalShot)).Position(this.position).Build();
            return new NormalShotBuilder<ReimuNormalShot>(this, 0).CreateShot();
        }

        protected override Shot GetThreeShot(double theta)
        {
          //  return new ReimuNormalShot(this, theta);
            //return new Shot.Builder(typeof(ReimuNormalShot)).Position(this.position).Theta(theta).Build();
            return new DirectionShotBuilder<ReimuNormalShot>(this, 0, theta).CreateShot();
        }

        protected override Shot GetFiveShot(double theta)
        {
          //  return new ReimuNormalShot(this, theta);
          //  return new Shot.Builder(typeof(ReimuNormalShot)).Position(this.position).Theta(theta).Build();
            return new DirectionShotBuilder<ReimuNormalShot>(this, 0, theta).CreateShot();
        }

        protected override Shot GetSubShot(double theta)
        {
          //  return new ReimuNormalShot(new position.SSPosition(), theta);
            throw new NotImplementedException();
        }

        protected override bom.Bom GetBom()
        {
            return new entity.bom.ReimuBom();
        }

        public override string GetCutInTextureName()
        {
            return "cutin_reimu.png";
        }
    }
}
