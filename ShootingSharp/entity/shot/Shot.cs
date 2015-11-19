using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;
using ShootingSharp.entity;
using ShootingSharp.texture;
using DxLibDLL;

namespace ShootingSharp.entity.shot
{
    public abstract class Shot : EntityLiving
    {
        /// <summary>
        /// ショットのタイプ
        /// Nomal = まっすぐ進むだけ
        /// Direction = 角度を指定してその方向に進む弾
        /// Aim = Targetを指定してその位置に向かう弾
        /// Homing = 慣性の大きさ(ホーミングの強力さ)を指定して敵をある程度追尾する弾
        /// </summary>
        public enum ShotType
        {
            Normal, Direction, Aim, Homing
        }

        /// <summary>
        /// ショットのタイプ
        /// </summary>
        public ShotType Type { get; set; }

        /// <summary>
        /// 弾が消えるまでの時間
        /// </summary>
     //   protected int deleteTime;

        /// <summary>
        /// アップデートのたびに呼ばれるカウンタ
        /// </summary>
        protected int updateCount;

        protected TextureLoader textureLoader = TextureLoader.GetInstance();

        /// <summary>
        /// xの移動量
        /// </summary>
        protected int moveX;

        /// <summary>
        /// yの移動量
        /// </summary>
        protected int moveY;

        /// <summary>
        /// 弾の角度
        /// </summary>
        protected double theta;

        /// <summary>
        /// 画面外に出た弾がtrueで消えるようになる(デフォ=true)
        /// </summary>
        protected bool outOfWindowDeleteEnable;

        /// <summary>
        /// Aim,Homing時のターゲット座標
        /// </summary>
        protected SSPosition target;

        private ShootingSharp.position.SSPosition pos = new position.SSPosition();

        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        public Shot(SSPosition position)
            : base()
        {
            this.outOfWindowDeleteEnable = true;
            this.Type = ShotType.Normal;
            this.moveSpeed = 5;
            this.position.PosX = position.PosX;
            this.position.PosY = position.PosY;

          //  this.deleteTime = this.GetDeleteTime();
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="theta">角度</param>
        public Shot(SSPosition position, double theta)
            : base()
        {
            this.outOfWindowDeleteEnable = true;
            this.Type = ShotType.Direction;

            this.theta = theta * Math.PI / 180.0D;
            this.moveSpeed = 5;
            this.position.PosX = position.PosX;
            this.position.PosY = position.PosY;

          //  this.deleteTime = this.GetDeleteTime();
        }


        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public Shot(IHasSSPosition shooter)
            : base()
        {
            this.outOfWindowDeleteEnable = true;
            this.Type = ShotType.Normal;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;

          //  this.deleteTime = this.GetDeleteTime();
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        /// <param name="theta">角度</param>
        public Shot(IHasSSPosition shooter, double theta)
            : base()
        {
            this.Type = ShotType.Direction;
            outOfWindowDeleteEnable = true;
            this.theta = theta * Math.PI / 180.0D;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;

           // this.deleteTime = this.GetDeleteTime();
        }

        public Shot(IHasSSPosition shooter, SSPosition target)
            : base()
        {
            this.Type = ShotType.Aim;
            outOfWindowDeleteEnable = true;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;

           // this.deleteTime = this.GetDeleteTime();

            this.target = new SSPosition();
            this.target.PosX = target.PosX;
            this.target.PosY = target.PosY;

            this.theta = Math.Atan2(target.PosY - this.position.PosY, target.PosX - this.position.PosX);
        }
        
        public override void OnUpdate()
        {
           // base.OnUpdate();

            if (!this.IsLiving())
            {
                this.OnDeath();
            }

            if (this.outOfWindowDeleteEnable)
            {
                if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
               || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
               || this.position.PosX < 0
               || this.position.PosY < 0)
                {
                    this.Life = 0;
                }
            }
/*
            if (this.deleteTime != -1)
            {
                if (this.deleteTime <= this.updateCount)
                {
                    this.Life = 0;
                    this.updateCount = 0;
                }

                if (this.updateCount <= this.deleteTime)
                    this.updateCount++;
            }*/
        }

        public override void Move()
        {
            this.GetMove();

            this.position.PosX += this.moveX;
            this.position.PosY += this.moveY;
        }

        protected virtual void GetMove()
        {
            if (this.Type == ShotType.Normal)
            {
                this.moveX = 0;
                this.moveY = this.moveSpeed;
            }

            else if (this.Type == ShotType.Direction)
            {

                this.moveX = (int)Math.Round((double)this.moveSpeed * Math.Cos(theta));
                this.moveY = (int)Math.Round((double)this.moveSpeed * Math.Sin(theta));

                if (this.moveX == 0 && this.moveY == 0)
                {
                    this.moveY = this.moveSpeed;
                }
            }
            else if (this.Type == ShotType.Aim)
            {
                this.moveX = (int)Math.Round((double)this.moveSpeed * Math.Cos(theta));
                this.moveY = (int)Math.Round((double)this.moveSpeed * Math.Sin(theta));

                if (this.moveX == 0 && this.moveY == 0)
                {
                    this.moveY = this.moveSpeed;
                }
            }
        }

        public override void OnInteract(Entity entity)
        {
            if (entity is Shot || entity is item.Item)
                return;


            //ぶつかったら消える
            this.Life = 0;

        }

        //消えるまでの時間
        protected virtual int GetDeleteTime()
        {
            return -1;
        }

        public override position.SSPosition GetTexturePosition()
        {

            pos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            pos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return pos;
        }

        public override void Draw()
        {/*
            if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
                || this.position.PosX < 0
                || this.position.PosY < 0)
            {
                return;
            }
            */
            DX.DrawExtendGraph(
               this.GetTexturePosition().PosX,
               this.GetTexturePosition().PosY,
               this.GetTexturePosition().PosX + this.GetTextureSize().Width + 1,
               this.GetTexturePosition().PosY + this.GetTextureSize().Height + 1,
               this.textureLoader.Textures[this.GetTextureName()],
               DX.TRUE);
        }

        public void SetMoveSpeed(int speed)
        {
            this.moveSpeed = speed;
        }

        public int GetMoveSpeed()
        {
            return this.moveSpeed;
        }
        
        
    }
}
