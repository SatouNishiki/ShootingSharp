using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;
using ShootingSharp.task;

namespace ShootingSharp.ai
{
    /// <summary>
    /// AIのひな形クラス
    /// </summary>
    public abstract class AIMicroThread
    {
        /// <summary>
        /// 優先度 小さいほうから実行される
        /// </summary>
        protected int priority;
        protected EntityPlayer player;
        protected Entity entity;
        protected int frame;

        /// <summary>
        /// 現在のフレーム数
        /// </summary>
        protected int frameCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity">実行者</param>
        /// <param name="priority">優先度</param>
        /// <param name="frame">実行フレーム数</param>
        public AIMicroThread(Entity entity, int priority, int frame)
        {
            this.player = SSTaskFactory.PlayerUpdateTask.Player;
            this.entity = entity;
            this.priority = priority;
            this.frame = frame;
        }


        /// <summary>
        /// マイクロスレッドをコンストラタで指定したフレーム数だけ繰り返し実行します
        /// </summary>
        /// <returns>falseで実行終了</returns>
        public bool Run()
        {
            this.MicroThread().Reset();

            if (this.frame <= this.frameCount)
            {
                return false;
            }
            else
            {
                this.MicroThread().MoveNext();
                this.frameCount++;
                return true;

            }

        }

        protected abstract IEnumerator<object> MicroThread();
    }
}
