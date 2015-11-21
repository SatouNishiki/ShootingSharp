using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.collid
{
    public class CollitionInfo
    {
        /// <summary>
        /// 当たった相手のタイプ
        /// </summary>
        public Type CollitionObjectType { get; set; }

        /// <summary>
        /// 当たった相手のオブジェクト
        /// </summary>
        public IInteracter CollitionInteractor { get; set; }
    }
}
