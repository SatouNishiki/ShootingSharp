using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;
using ShootingSharp.entity;

namespace ShootingSharp.entity.shot
{
    public abstract class Shot : EntityLiving
    {
        /// <summary>
        /// 移動方向タイプ
        /// </summary>
        public enum MoveDirection
        {
            Up, Right
        }

        /// <summary>
        /// 追尾弾レベルの上限
        /// </summary>
        protected const int homingMaxLevel = 10;

        protected int moveX;
        protected int moveY;

        protected bool homingEnabled;
        protected bool tryToMoveEnabled;

        protected int homingLevel;
        protected int deleteTime;
        protected int updateCount;

        /// <summary>
        /// 何もしなかった場合に動く方向
        /// </summary>
        protected MoveDirection baseMoveDirection;

        /// <summary>
        /// 追尾弾の対象
        /// </summary>
        protected IHasSSPosition homingTarget;

        /// <summary>
        /// 位置指定弾の対象
        /// </summary>
        protected SSPosition tryToMovePoint;

        /// <summary>
        /// 追尾弾のインターバルを計算するカウンタ
        /// </summary>
        private int homingCount;

        public Shot(IHasSSPosition shooter) : base()
        {
            this.baseMoveDirection = MoveDirection.Up;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;
        }

        public void SetHomingLevel(int level, IHasSSPosition target)
        {
            this.homingLevel = homingMaxLevel - level;
            this.homingEnabled = true;
            this.homingTarget = target;
        }

        public void SetTryToMove(SSPosition target)
        {
            this.tryToMovePoint = target;
            this.tryToMoveEnabled = true;
        }

        public override void Move()
        {
            this.SetNextMove();

            this.position.PosX += moveX;
            this.position.PosY -= moveY;
        }


        public override void OnInteract()
        {
            //ぶつかったら消える
            this.Life = 0;
        }

        protected virtual void SetNextMove()
        {
            this.moveX = 0;
            this.moveY = 0;

            if(this.baseMoveDirection == MoveDirection.Up)
            {
                this.moveY = this.moveSpeed;
            }
            else
            {
                this.moveX = this.moveSpeed;
            }

            if (this.homingEnabled)
            {
                if (this.homingLevel <= this.homingCount)
                {
                    this.TryToMove(this.homingTarget.GetPosition());
                    homingCount = 0;
                }
                else
                {
                    homingCount++;
                }

                return;
            }

            if (tryToMoveEnabled)
            {
                TryToMove(this.tryToMovePoint);
                this.tryToMoveEnabled = false;
            }
        }

        /// <summary>
        /// 目的地に移動しようとしてみるs
        /// </summary>
        /// <param name="position"></param>
        private void TryToMove(SSPosition position)
        {
            if (this.baseMoveDirection == MoveDirection.Up)
            {
                if (this.position.PosX != position.PosX)
                {
                    if (this.position.PosX > position.PosX)
                    {
                        this.moveX--;
                    }
                    else
                    {
                        this.moveX++;
                    }
                }
            }

            if (this.baseMoveDirection == MoveDirection.Right)
            {
                if (this.position.PosY != position.PosY)
                {
                    if (this.position.PosY > position.PosY)
                    {
                        this.moveY--;
                    }
                    else
                    {
                        this.moveY++;
                    }
                }
            }
        }

        public override void OnUpdate()
        {

            if (this.deleteTime <= this.updateCount)
            {
                this.Life = 0;
                this.updateCount = 0;
            }

            this.updateCount++;


            base.OnUpdate();
        }


        public override void OnDeath()
        {
            this.logger.Debug(this.GetType() + " is deleted");
        }
    }
}
