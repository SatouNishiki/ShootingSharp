using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 位置情報をもったオブジェクト
    /// </summary>
    public interface IHasSSPosition
    {
        /// <summary>
        /// 位置
        /// </summary>
        /// <returns></returns>
        SSPosition GetPosition();
    }
}
