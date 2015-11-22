﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;
using ShootingSharp.position;
using DxLibDLL;

namespace DebugProject.shot
{
    public class SmallStarsShot : EnemyCircleShot
    {
        private int colorType;

        private double gTheta;

        public SmallStarsShot(IHasSSPosition shooter, double theta, int color)
            : base(shooter, theta)
        {
            this.MoveSpeed = 3;
            this.colorType = color;
        }

        public SmallStarsShot(IHasSSPosition shooter, SSPosition target, int color)
            : base(shooter, target)
        {
            this.MoveSpeed = 3;
            this.colorType = color;
        }


        public override int GetRadius()
        {
            return 3;
        }

        public override string GetTextureName()
        {
            if (this.colorType < 3)
            {
                return "stars" + this.colorType.ToString() + ".png";
            }
            else
            {
                return "stars5.png";
            }
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(10, 10);
        }

        public override void Draw()
        {
            DX.DrawRotaGraph(this.GetTexturePosition().PosX, this.GetTexturePosition().PosY, 1.0D, (Math.PI / 180.0D) * this.gTheta, this.textureLoader.Textures[this.GetTextureName()], DX.TRUE);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            this.gTheta += 2.5D;
        }
    }
}
