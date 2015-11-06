using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.position;

namespace ShootingSharp.entity
{
    public class EntityLiving : Entity, IHasHealth
    {
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
        /// 当たってる？
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract bool IsInteract(IInteract obj);

        /// <summary>
        /// 当たったとき
        /// </summary>
        public abstract void OnInteract();

        /// <summary>
        /// 位置
        /// </summary>
        /// <returns></returns>
        public abstract SSPosition GetPosition();

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

        public bool IsLiving()
        {
            throw new NotImplementedException();
        }

        public void OnDeath()
        {
            throw new NotImplementedException();
        }
    }
}
