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
using System.Windows;
using ShootingSharp.task;

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
            Normal, Direction, Aim, Homing, Sin
        }

        protected readonly Type[] noCollitionTypes = { typeof(Item), typeof(Shot) };

        /// <summary>
        /// ショットのタイプ
        /// </summary>
        public ShotType Type { get; set; }

        public double SinShotTheta { get; set; }

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

        private Vector vec2;

        protected int metaData;

        public class Builder 
        {
            public SSPosition initPos { get; private set; }
            public Shot.ShotType type { get; private set; }
            public double theta { get; private set; }
            public SSPosition target { get; private set; }
            public int moveX { get; private set; }
            public int moveY { get; private set; }
            public int metaData { get; private set; }
            private Type baseType;

            public Builder(Type baseType)
            {
                this.baseType = baseType;
            }

            public Builder Position(SSPosition pos)
            {
                this.initPos = pos;
                return this;
            }

            public Builder Type(Shot.ShotType type)
            {
                this.type = type;
                return this;
            }

            public Builder Theta(double theta)
            {
                this.theta = theta * Math.PI / 180.0D;
                return this;
            }

            public Builder Target(SSPosition target)
            {
                this.target = target;
                return this;
            }

            public Builder MetaData(int meta)
            {
                this.metaData = meta;
                return this;
            }

            public Shot Build()
            {
                if (typeof(Shot).IsAssignableFrom(this.baseType))
                {
                    return (Shot)Activator.CreateInstance(this.baseType, this);
                }

                else
                {
                    return null;
                }
            }

        }

        public Shot(Builder builder) : base()
        {
            Init();
            this.Type = builder.type;
            this.position = new SSPosition(builder.initPos.PosX, builder.initPos.PosY);
            this.theta = builder.theta;
            this.target = builder.target;
            this.metaData = builder.metaData;

            if (this.target != null)
            {
                this.theta = Math.Atan2(target.PosY - this.position.PosY, target.PosX - this.position.PosX);

                this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(theta));
                this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(theta));
            }
            else if (this.theta != 0.0D)
            {
                this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(theta));
                this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(theta));
            }
        }
        /*
        public Shot()
        {
            Init();

            this.Type = ShotType.Normal;
            this.position.PosX = SSTaskFactory.PlayerUpdateTask.Player.GetPosition().PosX;
            this.position.PosY = SSTaskFactory.PlayerUpdateTask.Player.GetPosition().PosY;

        }

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

            this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(theta));
            this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(theta));

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

            this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(theta));
            this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(theta));
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

            this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(theta));
            this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(theta));
        }

        /// <summary>
        /// 誘導弾作成
        /// </summary>
        /// <param name="shooter"></param>
        /// <param name="target"></param>
        public Shot(IHasSSPosition shooter, IHasSSPosition target)
            : base()
        {
            Init();
            this.Type = ShotType.Homing;
            this.position.PosX = shooter.GetPosition().PosX;
            this.position.PosY = shooter.GetPosition().PosY;
           
            this.target = target.GetPosition();
        }
    */
        protected virtual void Init()
        {
            this.outOfWindowDeleteEnable = true;
            this.MoveSpeed = 5;
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

            this.updateCount++;

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
                this.moveY = this.MoveSpeed;
            }

            else if (this.Type == ShotType.Direction)
            {

                if (this.moveX == 0 && this.moveY == 0)
                {
                    this.moveY = this.MoveSpeed;
                }
            }
            else if (this.Type == ShotType.Aim)
            {
                

                if (this.moveX == 0 && this.moveY == 0)
                {
                    this.moveY = this.MoveSpeed;
                }
            }
            else if (this.Type == ShotType.Homing)
            {
                Vector vec1 = new Vector(target.PosX - this.position.PosX, target.PosY - this.position.PosY);
                vec1.Normalize();
                vec1 *= vec1.Length;
                Vector outVec = vec1 + vec2;
                outVec.Normalize();
                outVec *= this.MoveSpeed;
                vec2 = outVec;

                this.moveX = (int)Math.Round(vec2.X);
           
                this.moveY = this.MoveSpeed;

            
                if (this.moveX == 0 && this.moveY == 0)
                {
                    this.moveY = this.MoveSpeed;
                }
            }
            else if (this.Type == ShotType.Sin)
            {
             //   this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Sin(this.SinShotTheta));
             //   this.moveY = this.MoveSpeed;

               // this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(this.moveX * Math.PI / 180.0D)) + 1;
           //     this.moveY = (int)Math.Round((double)this.MoveSpeed * Math.Sin(this.moveY * Math.PI / 180.0D)) + 1;

                this.moveX = (int)Math.Round((double)this.MoveSpeed * Math.Cos(this.position.PosX));
                this.moveY = this.MoveSpeed;
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
            this.MoveSpeed = speed;
        }

        public int GetMoveSpeed()
        {
            return this.MoveSpeed;
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

        public void SetMetaData(int meta)
        {
            this.metaData = meta;
        }
    }
}
