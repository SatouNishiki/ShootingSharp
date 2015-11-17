﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class RedFairyShot : Shot
    {

        public RedFairyShot(IHasSSPosition shooter) : base(shooter) 
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.moveSpeed = 4;
        }

        public RedFairyShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.moveSpeed = 4;
        }

        public RedFairyShot(IHasSSPosition shooter, position.SSPosition target)
            : base(shooter, target)
        {
            this.textureLoader.LoadSprite("fairy_shot.png", 512 / 20, 256 / 20, 8, 20, 20);
            this.moveSpeed = 4;
        }

        public override void OnDeath()
        {
            //なにもしない
        }

        public override string GetTextureName()
        {
            return "fairy_shot.png0";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(20, 20);
        }

        public override void DoAction()
        {
          //  throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 5;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return SharpType.Circle;
        }

     
        
    }
}
