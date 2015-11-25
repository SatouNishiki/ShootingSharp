using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.bom
{
    public class ReimuBom : Bom
    {
        public ReimuBom()
            : base()
        {
            this.textureSize = new System.Drawing.Size(30, 30);
        }

        public override string GetTextureName()
        {
            return "reimu_bom2.png";
        }

    }
}
