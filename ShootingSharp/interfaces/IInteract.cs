using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;
using ShootingSharp.position;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 円形あたり判定を持つオブジェクトを表す
    /// </summary>
    public interface IInteract : IHasSSPosition
    {
        /// <summary>
        /// 円の半径を返す
        /// </summary>
        /// <returns></returns>
        int GetRadius();

        /// <summary>
        /// 引数の相手とぶつかってるかどうか
        /// </summary>
        /// <param name="obj">相手</param>
        /// <returns></returns>
        bool IsInteract(IInteract obj);

        /// <summary>
        /// 当たったときの処理
        /// </summary>
        void OnInteract();
    }
}
