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
        public int MoveSpeed { get; protected set; }

        protected Logger logger;

        protected System.Drawing.Size textureSize;

        public scene.ShootingSceneBase Scene { get; set; }

        private List<AITask> MoveAI;
        private List<AITask> ActionAI;


        private int moveAICount;
        private int actionAICount;

        private bool isLiving;

        public bool AIEnabled { get; set; }

        public Action<Entity> OnDispose;

        public Entity()
        {
            this.position = new position.SSPosition();
            this.logger = Logger.GetInstance();
            this.ActionAI = new List<AITask>();
            this.MoveAI = new List<AITask>();
            this.AIEnabled = true;
            this.MoveSpeed = 1;
            this.isLiving = true;
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
        public System.Drawing.Size GetTextureSize()
        {
            return this.textureSize;
        }

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
        public virtual void OnUpdate()
        {

            if (this.position.PosX < 0 || this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                || this.position.PosY < 0 || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height)
                this.isLiving = false;
        }

        /// <summary>
        /// 動く
        /// </summary>
        public virtual void Move()
        {
            if (this.AIEnabled)
            {

                if (this.MoveAI.Count == 0)
                    return;

                if (!this.MoveAI[moveAICount].Run())
                {
                    this.moveAICount++;

                    if (this.moveAICount >= this.MoveAI.Count)
                    {
                        this.moveAICount = 0;
                    }

                    this.Move();
                }
            }
        }

        /// <summary>
        /// ショットとか
        /// </summary>
        public virtual void DoAction()
        {
            if (this.AIEnabled)
            {

                if (this.ActionAI.Count == 0)
                    return;

                if (!this.ActionAI[actionAICount].Run())
                {
                    this.actionAICount++;

                    if (this.actionAICount >= this.ActionAI.Count)
                    {
                        this.actionAICount = 0;
                    }

                    this.DoAction();
                }
            }
        }


        public virtual bool IsLiving()
        {
            return this.isLiving;
        }

        public abstract void OnInteract(collid.CollitionInfo info);

        public abstract collid.ColliderBase GetCollider();

        public void AddMoveAI(AITask ai)
        {
            this.MoveAI.Add(ai);

            var quary = from s in this.MoveAI
                        orderby s.priority
                        select s;

            this.MoveAI = quary.ToList();
        }

        public void AddActionAI(AITask ai)
        {
            this.ActionAI.Add(ai);

            var quary = from s in this.ActionAI
                        orderby s.priority
                        select s;

            this.ActionAI = quary.ToList();
        }

    }
}
