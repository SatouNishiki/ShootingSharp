using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.sound;

namespace ShootingSharp.entity.item
{
    public class ItemBigPower : Item
    {
        public ItemBigPower()
            : base()
        {
         
        }

        public override void AddItemEffect(EntityPlayer player)
        {
            player.AddPower(8);

            SoundLoader.GetInstance().PlayEffect("super_up.mp3");
        }

        public override string GetTextureName()
        {
            return "big_power.png";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(30, 30);
        }
    }
}
