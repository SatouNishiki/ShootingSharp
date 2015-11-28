using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.shot
{
    public class SinShotBuilder<T> : ShotBuilder<T> where T : Shot
    {
        private int r;
        private int l;
        private int meta;
        private position.SSPosition pos;

        public SinShotBuilder(position.SSPosition initPos, int metaData, int radius, int loopSpeed)
        {
            this.pos = initPos;
            this.l = loopSpeed;
            this.meta = metaData;
            this.r = radius;
        }

        public SinShotBuilder(interfaces.IHasSSPosition shooter, int metaData, int radius, int loopSpeed)
        {
            this.pos = shooter.GetPosition();
            this.l = loopSpeed;
            this.meta = metaData;
            this.r = radius;
        }

        protected override void SetBuilder(T shot)
        {
            shot.SetPosition(pos);
            shot.Type = Shot.ShotType.Sin;
            shot.SetMetaData(meta);
            shot.SinRadius = r;
            shot.LoopSpeed = l;
        }
    }
}
