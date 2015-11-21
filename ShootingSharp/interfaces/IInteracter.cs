using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.collid;

namespace ShootingSharp.interfaces
{
    public interface IInteracter : IHasSSPosition
    {
        /// <summary>
        /// 当たったときの処理
        /// </summary>
        void OnInteract(CollitionInfo info);
       
        ColliderBase GetCollider();

        bool IsLiving();
    }
}
