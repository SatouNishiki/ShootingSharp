using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;
using ShootingSharp.core;

namespace ShootingSharp.entity
{
    /// <summary>
    /// 動くもの全般
    /// </summary>
    public abstract class Entity : IAction, IInteracter, IMoveable, IUpdateable, IDrawable
    {
        /// <summary>
        /// フレンドコード
        /// </summary>
        protected string friendCode;
       
        /// <summary>
        /// 自分の位置
        /// </summary>
        protected SSPosition position;

        /// <summary>
        /// 移動速度
        /// </summary>
        protected int moveSpeed;

        protected Logger logger;


        /// <summary>
        /// 衝突判定管理クラス
        /// </summary>
        public IInteractManager InteractManager { get; set; }

        public Entity()
        {
            this.position = new position.SSPosition();
            this.logger = Logger.GetInstance();
            this.friendCode = this.GetType().FullName;
        }

       
        /// <summary>
        /// 当たってる？
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool IsInteract(IInteracter obj)
        {
            //フレンドコードが同じなら味方であると判定してfalseを返す
            if (this.GetFriendCode() == obj.GetFriendCode())
                return false;
            
            if (this.position.PosX > SSGame.GetInstance().GetWindowSize().Width
                 || this.position.PosY > SSGame.GetInstance().GetWindowSize().Height
                 || this.position.PosX < 0
                 || this.position.PosY < 0)
                return false;

            if (obj.GetPosition().PosX > SSGame.GetInstance().GetWindowSize().Width
                || obj.GetPosition().PosY > SSGame.GetInstance().GetWindowSize().Height
                || obj.GetPosition().PosX < 0
                || obj.GetPosition().PosY < 0)
                return false;
            
            if (this.GetSharpType() == SharpType.Circle)
            {

                if (obj.GetSharpType() == SharpType.Circle)
                {
                    return InteractCalculator.IsInteractCircleCircle(this, obj);
                }
                else
                {
                    return InteractCalculator.IsInteractCircleSquare(this, obj);
                }
            }
            else
            {
                if (obj.GetSharpType() == SharpType.Circle)
                {
                    return InteractCalculator.IsInteractCircleSquare(obj, this);
                }
                else
                {
                    return InteractCalculator.IsInteractSquareSquare(this, obj);
                }
            }
            
        }

        
        /// <summary>
        /// 当たったとき
        /// </summary>
        public abstract void OnInteract(Entity entity);

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
        public virtual void OnUpdate()
        {
            if (!(this.position.PosX > SSGame.GetInstance().GetWindowSize().Width
                 || this.position.PosY > SSGame.GetInstance().GetWindowSize().Height
                 || this.position.PosX < 0
                 || this.position.PosY < 0))
            {
                Entity entity = this.InteractManager.GetInteractObject(this);
                //何かとぶつかってたら
                if (entity != null)
                {
                    this.OnInteract(entity);
                    entity.OnInteract(this);
                }
            }
        }

        public virtual string GetFriendCode()
        {
            return this.friendCode;
        }


        public void SetFriendCode(string code)
        {
            this.friendCode = code;
        }

        /// <summary>
        /// 動く
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// ショットとか
        /// </summary>
        public abstract void DoAction();

        /// <summary>
        /// 半径返す
        /// </summary>
        /// <returns></returns>
        public abstract int GetRadius();

        public abstract bool IsLiving();

        /// <summary>
        /// 長方形の四隅を返す
        /// </summary>
        /// <returns></returns>
        public abstract SquareSSPositon GetSquarePosition();

        /// <summary>
        /// 形状タイプを返す
        /// </summary>
        /// <returns></returns>
        public abstract SharpType GetSharpType();

    }
}
