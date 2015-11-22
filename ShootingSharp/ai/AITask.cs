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
    public abstract class AITask
    {
        /// <summary>
        /// 優先度 小さいほうから実行される
        /// </summary>
        public int priority { get; private set; }
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
        public AITask(Entity entity, int priority, int frame)
        {
            this.player = SSTaskFactory.PlayerUpdateTask.Player;
            this.entity = entity;
            this.priority = priority;
            this.frame = frame;
        }


        /// <summary>
        /// AIをコンストラタで指定したフレーム数だけ繰り返し実行します
        /// </summary>
        /// <returns>falseで実行終了</returns>
        public bool Run()
        {

            if (this.frame <= this.frameCount)
            {
                this.frameCount = 0;
                return false;
            }
            else
            {
                this.RunMethod();
                this.frameCount++;
                return true;

            }

        }

        protected abstract void RunMethod();
    }
}
