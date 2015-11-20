﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;

namespace ShootingSharp.entity.shot
{
    public class BlackCircleShot : CircleShot
    {
        public BlackCircleShot(interfaces.IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.textureLoader.LoadDerivationGraph(167, 62, 30, 30, "shot.png");
            this.moveSpeed = 3;
        }

        public BlackCircleShot(interfaces.IHasSSPosition shooter, position.SSPosition target)
            : base(shooter, target)
        {
            this.textureLoader.LoadDerivationGraph(167, 62, 30, 30, "shot.png");
            this.moveSpeed = 3;
        }

        public override void OnDeath()
        {
            //
        }

        public override string GetTextureName()
        {
            return "shot.png167+62";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(12, 12);
        }

        public override void DoAction()
        {
            throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 1;
        }

    }
}
