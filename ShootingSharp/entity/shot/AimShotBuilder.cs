using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.shot
{
    public class AimShotBuilder<T> : ShotBuilder<T> where T : Shot
    {
        private position.SSPosition pos;
        private int meta;
        private position.SSPosition target;

        public AimShotBuilder(position.SSPosition initPos, int metaData, position.SSPosition target)
        {
            this.pos = initPos;
            this.meta = metaData;
            this.target = target;
        }

        public AimShotBuilder(interfaces.IHasSSPosition shooter, int metaData, position.SSPosition target)
        {
            this.pos = shooter.GetPosition();
            this.meta = metaData;
            this.target = target;
        }

        protected override void SetBuilder(T shot)
        {
            shot.SetPosition(pos);
            shot.Type = Shot.ShotType.Aim;
            shot.SetMetaData(meta);
            shot.Target = target;

            shot.Theta = Math.Atan2(target.PosY - pos.PosY, target.PosX - pos.PosX);

            shot.MoveX = (int)Math.Round((double)shot.MoveSpeed * Math.Cos(shot.Theta));
            shot.MoveY = (int)Math.Round((double)shot.MoveSpeed * Math.Sin(shot.Theta));
        }
    }
}
