using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 衝突判定管理クラス
    /// </summary>
    public interface IInteractManager
    {
       void AddInteractObject(IInteracter interact);

        /// <summary>
        /// ぶつかってる対象を返す
        /// </summary>
        /// <param name="interact">あたり判定をする自身のインスタンス</param>
        /// <returns>ぶつかってる=ぶつかってる対象 ぶつかってない=null</returns>
        IInteracter GetInteractObject(IInteracter interact);

    }
}