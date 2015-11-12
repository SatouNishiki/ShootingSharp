using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// シーンを表すインターフェース
    /// </summary>
    public interface IScene : IUpdater
    {
        /// <summary>
        /// フレーム毎の処理
        /// </summary>
        /// <returns>終了フラグ</returns>
        bool Run();

        /// <summary>
        /// 次のシーンが存在するかどうか
        /// </summary>
        /// <returns></returns>
        bool ExistNextScene();

        /// <summary>
        /// このシーンが終了しているかどうか
        /// </summary>
        /// <returns></returns>
        bool IsFinished();

        /// <summary>
        /// 次のシーンを返す
        /// </summary>
        /// <returns></returns>
        IScene GetNextScene();

    }
}
