using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.texture;
using ShootingSharp.sound;

namespace ShootingSharp.entity.item
{
    public class ItemSmallPower : Item
    {
        public ItemSmallPower()
            : base()
        {
            this.textureSize = new System.Drawing.Size(30, 30);
        }

        public override void AddItemEffect(EntityPlayer player)
        {
            player.AddPower(1);

            SoundLoader.GetInstance().PlayEffect("up.mp3");
        }

        public override string GetTextureName()
        {
            return "small_power.png";
        }
      
    }
}
