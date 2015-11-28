using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.shot
{
    public class DirectionShotBuilder<T> : ShotBuilder<T> where T : Shot
    {
        private position.SSPosition pos;
        private int meta;
        private double theta;

        public DirectionShotBuilder(position.SSPosition initPos, int metaData, double theta)
        {
            this.pos = initPos;
            this.meta = metaData;
            this.theta = theta;
        }

        public DirectionShotBuilder(interfaces.IHasSSPosition shooter, int metaData, double theta)
        {
            this.pos = shooter.GetPosition();
            this.meta = metaData;
            this.theta = theta;
        }


        protected override void SetBuilder(T shot)
        {
            shot.SetPosition(pos);
            shot.Type = Shot.ShotType.Direction;
            shot.SetMetaData(meta);
            shot.Theta = theta * Math.PI / 180.0D;

            shot.MoveX = (int)Math.Round((double)shot.MoveSpeed * Math.Cos(shot.Theta));
            shot.MoveY = (int)Math.Round((double)shot.MoveSpeed * Math.Sin(shot.Theta));
        }
    }
}
