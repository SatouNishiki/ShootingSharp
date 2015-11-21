using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;
using ShootingSharp.core;
using ShootingSharp.ai;

namespace ShootingSharp.entity
{
    /// <summary>
    /// 動くもの全般
    /// </summary>
    public abstract class Entity : IAction, IInteracter, IMoveable, IUpdateable, IDrawable
    {
       
        /// <summary>
        /// 自分の位置
        /// </summary>
        protected SSPosition position;

        /// <summary>
        /// 移動速度
        /// </summary>
        protected int moveSpeed;

        protected Logger logger;

        public scene.ShootingSceneBase Scene { get; set; }

        protected List<AIMicroThread> MoveAI;
        protected List<AIMicroThread> ActionAI;

        public Entity()
        {
            this.position = new position.SSPosition();
            this.logger = Logger.GetInstance();
            this.ActionAI = new List<AIMicroThread>();
            this.MoveAI = new List<AIMicroThread>();
        }
        /// <summary>
        /// 中心位置
        /// </summary>
        /// <returns></returns>
        public SSPosition GetPosition()
        {
            return this.position;
        }

        public void SetPosition(SSPosition pos)
        {
            this.position = pos;
        }

        /// <summary>
        /// 画像の名前
        /// </summary>
        /// <returns></returns>
        public abstract string GetTextureName();
        
        /// <summary>
        /// 画像サイズ
        /// </summary>
        /// <returns></returns>
        public abstract System.Drawing.Size GetTextureSize();

        /// <summary>
        /// 画像の表示場所
        /// </summary>
        /// <returns></returns>
        public abstract SSPosition GetTexturePosition();

        /// <summary>
        /// 表示するよ
        /// </summary>
        public abstract void Draw();


        /// <summary>
        /// アップデートのとき
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// 動く
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// ショットとか
        /// </summary>
        public abstract void DoAction();


        public abstract bool IsLiving();

        public abstract void OnInteract(collid.CollitionInfo info);

        public abstract collid.ColliderBase GetCollider();
    }
}
