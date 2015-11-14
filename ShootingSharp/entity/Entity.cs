﻿using System;
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

        /// <summary>
        /// 当たってる？
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool IsInteract(IInteracter obj)
        {

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
        public abstract void OnInteract();

        /// <summary>
        /// 中心位置
        /// </summary>
        /// <returns></returns>
        public SSPosition GetPosition()
        {
            return this.position;
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

        

        public Entity()
        {
            this.position = new position.SSPosition();
            this.logger = Logger.GetInstance();
        }

        /// <summary>
        /// アップデートのとき
        /// </summary>
        public virtual void OnUpdate()
        {
            //何かとぶつかってたら
            if (this.InteractManager.GetInteractObject(this) != null)
            {
                this.OnInteract();
            }
        }

    }
}
