using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;

namespace ShootingSharp.ai
{
    public abstract class AIMicroThread
    {
        /// <summary>
        /// 優先度 小さいほうから実行される
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// マイクロスレッドの実行
        /// </summary>
        /// <param name="entity">実行者</param>
        /// <returns></returns>
        public abstract IEnumerator<object> Run(Entity entity);
    }
}
