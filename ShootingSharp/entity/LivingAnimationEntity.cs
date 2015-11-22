using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;

namespace ShootingSharp.entity
{
    /// <summary>
    /// 生きてて動きのアニメーションするタイプの生物
    /// </summary>
    public abstract class LivingAnimationEntity : EntityLiving
    {
        /// <summary>
        /// アニメーションのタイプ(今どっちに動いてるか)
        /// </summary>
        public enum MoveTypeEnum
        {
            Up, Down, Left, Center, Right, RightUp, RightDown, LeftUp, LeftDown
        }

        /// <summary>
        /// 今どっちに動いてるか
        /// </summary>
        public MoveTypeEnum MoveType { get; set; }

        private SSPosition beforePosition;

        public LivingAnimationEntity()
            : base()
        {
            this.MoveType = MoveTypeEnum.Center;
        }

        /* 上下左右中央それぞれの場合のテクスチャの名前を返す */

        public abstract string GetUpTextureName();

        public abstract string GetDownTextureName();

        public abstract string GetLeftTextureName();

        public abstract string GetCenterTextureName();

        public abstract string GetRightTextureName();

        public abstract string GetRightUpTextureName();

        public abstract string GetRightDownTextureName();

        public abstract string GetLeftUpTextureName();

        public abstract string GetLeftDownTextureName();


        /// <summary>
        /// 移動タイプを決定する
        /// </summary>
        public virtual void SetMoveType()
        {
            if (beforePosition == null)
            {
                this.beforePosition = new SSPosition(this.position.PosX, this.position.PosY);
                return;
            }

            if (this.position.PosX == this.beforePosition.PosX && this.beforePosition.PosY == this.position.PosY)
            {
                this.MoveType = MoveTypeEnum.Center;
                return;
            }

            if (this.position.PosX == this.beforePosition.PosX)
            {
                if (this.position.PosY > this.beforePosition.PosY)
                {
                    this.MoveType = MoveTypeEnum.Down;
                }
                else
                {
                    this.MoveType = MoveTypeEnum.Up;
                }
                return;
            }

            if (this.position.PosX > this.beforePosition.PosX)
            {
                if (this.beforePosition.PosY == this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.Right;
                    return;
                }

                if (this.beforePosition.PosY > this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.RightUp;
                    return;
                }

                if (this.beforePosition.PosY < this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.RightDown;
                    return;
                }
            }

            if (this.position.PosX < this.beforePosition.PosX)
            {
                if (this.beforePosition.PosY == this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.Left;
                    return;
                }

                if (this.beforePosition.PosY > this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.LeftUp;
                    return;
                }

                if (this.beforePosition.PosY < this.position.PosY)
                {
                    this.MoveType = MoveTypeEnum.LeftDown;
                    return;
                }
            }
        }

        /// <summary>
        /// AnimationTypeに応じた画像を返す
        /// </summary>
        /// <returns></returns>
        public override string GetTextureName()
        {
            string name;

            switch (this.MoveType)
            {
                case(MoveTypeEnum.Up):
                    name = this.GetUpTextureName();
                    break;

                case(MoveTypeEnum.Down):
                    name = this.GetDownTextureName();
                    break;

                case(MoveTypeEnum.Left):
                    name = this.GetLeftTextureName();
                    break;

                case(MoveTypeEnum.Center):
                    name = this.GetCenterTextureName();
                    break;

                case(MoveTypeEnum.Right):
                    name = this.GetRightTextureName();
                    break;

                case(MoveTypeEnum.RightUp):
                    name = this.GetRightUpTextureName();
                    break;

                case(MoveTypeEnum.RightDown):
                    name = this.GetRightDownTextureName();
                    break;

                case (MoveTypeEnum.LeftUp):
                    name = this.GetLeftUpTextureName();
                    break;

                case (MoveTypeEnum.LeftDown):
                    name = this.GetLeftDownTextureName();
                    break;

                default:
                    name = string.Empty;
                    break;
            }

            return name;
        }

        public override void OnUpdate()
        {
            this.SetMoveType();
            base.OnUpdate();
        }
    }
}
