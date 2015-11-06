using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 生きてるオブジェクト
    /// </summary>
    public interface IHasHealth
    {
        /// <summary>
        /// 生きてる?
        /// </summary>
        /// <returns></returns>
        bool IsLiving();

        /// <summary>
        /// 死んだとき
        /// </summary>
        void OnDeath();
    }
}
