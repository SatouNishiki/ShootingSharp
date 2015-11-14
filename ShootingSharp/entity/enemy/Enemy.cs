using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;

namespace ShootingSharp.entity.enemy
{
    public abstract class Enemy : EntityLiving
    {
        /// <summary>
        /// このエネミーが出現するまでのフレーム数
        /// </summary>
        protected int popCount;


        public override void OnInteract()
        {
            throw new NotImplementedException();
        }

        public override string GetTextureName()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Size GetTextureSize()
        {
            throw new NotImplementedException();
        }

        public override position.SSPosition GetTexturePosition()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public void SetPopCount(int count)
        {
            this.popCount = count;
        }
    }
}
