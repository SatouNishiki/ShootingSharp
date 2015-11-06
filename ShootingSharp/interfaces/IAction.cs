using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// Update時にアクション(ショットなど)を行う
    /// </summary>
    interface IAction
    {
        void DoAction();
    }
}
