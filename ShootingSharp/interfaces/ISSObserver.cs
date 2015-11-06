using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// Observer
    /// </summary>
    public interface ISSObserver
    {
        /// <summary>
        /// アップデートイベントの発生
        /// </summary>
        event Action Update;

    }
}
