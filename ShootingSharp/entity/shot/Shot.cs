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
using ShootingSharp.core;

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
        public int MoveX { get; set; }

        /// <summary>
        /// yの移動量
        /// </summary>
        public int MoveY { get; set; }

        /// <summary>
        /// 弾の角度
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// 画面外に出た弾がtrueで消えるようになる(デフォ=true)
        /// </summary>
        protected bool outOfWindowDeleteEnable;

        /// <summary>
        /// Aim,Homing時のターゲット座標
        /// </summary>
        public SSPosition Target { get; set; }

        protected collid.ColliderBase collider;

        private Vector vec2;

        protected int metaData;

        public int SinRadius { get; set; }
        public int LoopSpeed { get; set; }
        private double loopCount;

        public Shot()
        {
            Init();
        }
      
    
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

            this.position.PosX += this.MoveX;
            this.position.PosY += this.MoveY;
        }

        protected virtual void GetMove()
        {
            if (this.Type == ShotType.Normal)
            {
                this.MoveX = 0;
                this.MoveY = this.MoveSpeed;
            }
                /*
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
            }*/
            else if (this.Type == ShotType.Homing)
            {
                Vector vec1 = new Vector(Target.PosX - this.position.PosX, Target.PosY - this.position.PosY);
                vec1.Normalize();
                vec1 *= vec1.Length;
                Vector outVec = vec1 + vec2;
                outVec.Normalize();
                outVec *= this.MoveSpeed;
                vec2 = outVec;

                this.MoveX = (int)Math.Round(vec2.X);
           
                this.MoveY = this.MoveSpeed;

            
                if (this.MoveX == 0 && this.MoveY == 0)
                {
                    this.MoveY = this.MoveSpeed;
                }
            }
            else if (this.Type == ShotType.Sin)
            {


                this.MoveX = SinRadius * (int)Math.Round(Math.Cos(this.loopCount));
                this.MoveY = SinRadius * (int)Math.Round(Math.Sin(this.loopCount)) + 1;

                this.loopCount += 1.0D / this.LoopSpeed;
                
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

            tempPos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            tempPos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return tempPos;
        }

        public override void Draw()
        {
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
