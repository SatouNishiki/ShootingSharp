using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// ボムを打てる
    /// </summary>
    public interface IHasBom
    {
        /// <summary>
        /// ボム打てる?
        /// </summary>
        /// <returns></returns>
        bool CanShot();

        /// <summary>
        /// ボム打つ
        /// </summary>
        void Bom();
    }
}
