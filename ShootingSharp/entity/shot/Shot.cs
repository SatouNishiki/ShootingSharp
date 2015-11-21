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
using ShootingSharp.entity.item;

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

        protected readonly Type[] noCollitionTypes = { typeof(Item), typeof(Shot) };

        /// <summary>
        /// ショットのタイプ
        /// </summary>
        public ShotType Type { get; set; }

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

        protected collid.ColliderBase collider;

        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        public Shot(SSPosition position)
            : base()
        {
            Init();
           
            this.Type = ShotType.Normal;
            
            this.position.PosX = position.PosX;
            this.position.PosY = position.PosY;
        }

        /// <summary>
        /// 方向弾を生成
        /// </summary>
        /// <param name="position">発射位置</param>
        /// <param name="theta">角度</param>
        public Shot(SSPosition position, double theta)
            : base()
        {
            Init();

            this.Type = ShotType.Direction;

            this.theta = theta * Math.PI / 180.0D;
            this.position.PosX = position.PosX;
            this.position.PosY = position.PosY;

        }


        /// <summary>
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public Shot(IHasSSPosition shooter)
            : base()
        {
            Init();

            this.Type = ShotType.Normal;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;
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

            Init();
            this.theta = theta * Math.PI / 180.0D;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;
        }

        public Shot(IHasSSPosition shooter, SSPosition target)
            : base()
        {
            Init();
            this.Type = ShotType.Aim;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;

            this.target = new SSPosition();
            this.target.PosX = target.PosX;
            this.target.PosY = target.PosY;

            this.theta = Math.Atan2(target.PosY - this.position.PosY, target.PosX - this.position.PosX);
        }

        protected virtual void Init()
        {
            this.outOfWindowDeleteEnable = true;
            this.moveSpeed = 5;
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            
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

        public override void OnInteract(collid.CollitionInfo info)
        {
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
        {
            if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
                || this.position.PosX < 0
                || this.position.PosY < 0)
            {
                return;
            }
            
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

        public override collid.ColliderBase GetCollider()
        {
            return this.collider;
        }

        public override void DoAction()
        {
            
        }

        public override void OnDeath()
        {
            
        }
    }
}
