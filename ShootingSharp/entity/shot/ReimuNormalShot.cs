using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class ReimuNormalShot : Shot
    {
        public ReimuNormalShot(IHasSSPosition shooter) : base(shooter) 
        {
            this.textureLoader.LoadSprite("reimu_shot.png", 4, 4, 9, 130, 130);
            this.moveSpeed = -10;
        }

        public ReimuNormalShot(IHasSSPosition shooter, double theta) : base(shooter, theta)
        {
            this.textureLoader.LoadSprite("reimu_shot.png", 4, 4, 9, 130, 130);
            this.moveSpeed = -10;
        }

        public override void OnDeath()
        {
            //なにもしない
        }

        public override string GetTextureName()
        {
            return "reimu_shot.png8";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(20, 20);
        }
        
        public override void DoAction()
        {
            throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 20;
        }

    }
}
