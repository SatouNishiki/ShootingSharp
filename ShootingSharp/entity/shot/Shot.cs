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
        /// シューターとy座標をどれだけずらしてショットするか
        /// </summary>
        public int MarginToShooter { get; set; }

        /// <summary>
        /// ショットのタイプ
        /// </summary>
        public ShotType Type { get; set; }

        /// <summary>
        /// 弾が消えるまでの時間
        /// </summary>
        protected int deleteTime;

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
        /// 通常弾を生成
        /// </summary>
        /// <param name="shooter">射手</param>
        public Shot(IHasSSPosition shooter)
            : base()
        {
            this.Type = ShotType.Normal;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY + this.MarginToShooter;

            this.deleteTime = this.GetDeleteTime();
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

            this.theta = theta;
            this.moveSpeed = 5;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY + this.MarginToShooter;

            this.deleteTime = this.GetDeleteTime();
        }


        
        public override void OnUpdate()
        {
            
            if (this.deleteTime <= this.updateCount)
            {
                this.Life = 0;
                this.updateCount = 0;
            }

            if (this.updateCount <= this.deleteTime)
                this.updateCount++;
     
            base.OnUpdate();
        }

        public override void Move()
        {
            this.GetMove();

            this.position.PosX += this.moveX;
            this.position.PosY -= this.moveY;
        }

        protected virtual void GetMove()
        {
            if (this.Type == ShotType.Normal)
            {
                this.moveX = 0;
                this.moveY = this.moveSpeed;
            }

            if (this.Type == ShotType.Direction)
            {
                this.moveX = this.moveSpeed * (int)Math.Cos(theta);
                this.moveY = this.moveSpeed * (int)Math.Sin(theta);
            }
        }

        public override void OnInteract(Entity entity)
        {
            if (entity is Shot)
                return;

            //ぶつかったら消える
            this.Life = 0;

        }

        //消えるまでの時間
        protected virtual int GetDeleteTime()
        {/*
            int a = SSGame.GetInstance().GetWindowSize().Width / this.moveSpeed;
            int b = SSGame.GetInstance().GetWindowSize().Height / this.moveSpeed;

            if (a < b)
                return a;
            else
                return b;
           */

            return SSGame.GetInstance().GetBattleWindowSize().Height / this.moveSpeed;
        }

        public override position.SSPosition GetTexturePosition()
        {

            ShootingSharp.position.SSPosition pos = new position.SSPosition();

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
    }
}
