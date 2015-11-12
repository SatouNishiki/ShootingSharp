using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// アップデートされる側
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Update時に呼び出される処理
        /// </summary>
        void OnUpdate();   
    }
}
