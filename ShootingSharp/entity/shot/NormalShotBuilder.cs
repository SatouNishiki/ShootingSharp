using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.shot
{
    public class NormalShotBuilder<T> : ShotBuilder<T> where T : Shot
    {
        private position.SSPosition pos;
        private int meta;

        public NormalShotBuilder(position.SSPosition initPos, int metaData)
        {
            this.pos = initPos;
            this.meta = metaData;
        }

        public NormalShotBuilder(interfaces.IHasSSPosition shooter, int metaData)
        {
            this.pos = shooter.GetPosition();
            this.meta = metaData;
        }

        protected override void SetBuilder(T shot)
        {
            shot.SetPosition(pos);
            shot.Type = Shot.ShotType.Normal;
            shot.SetMetaData(meta);
        }
    }
}
