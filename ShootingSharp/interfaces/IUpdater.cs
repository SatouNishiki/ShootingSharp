using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 監視される側
    /// </summary>
    public interface IUpdater
    {
        /// <summary>
        /// アップデートイベントの発生
        /// </summary>
        event Action Update;

    }
}
