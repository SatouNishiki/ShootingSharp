using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity;
using ShootingSharp.task;

namespace ShootingSharp.scene
{
    public abstract class SceneBase : IScene, IInteractManager
    {
        /// <summary>
        /// あたり判定を行うオブジェクトを全て格納するリスト
        /// </summary>
        protected List<Entity> interacters;

        protected UpdateTaskManager updateTask = SSTaskFactory.UpdateTask;
        protected DrawTaskManager drawTask = SSTaskFactory.DrawTask;
        protected MoveTaskManager moveTask = SSTaskFactory.MoveTask;
        protected ActionTaskManager actionTask = SSTaskFactory.ActionTask;

        public SceneBase()
        {
            this.interacters = new List<Entity>();
        }

        public bool Run()
        {
            this.actionTask.Run();
            this.updateTask.Run();
            this.moveTask.Run();
            this.drawTask.Run();

            if (this.ExistNextScene())
            {
                //TODO: 遷移処理
            }

            return !this.IsFinished();
        }

        public abstract bool ExistNextScene();

        public abstract bool IsFinished();

        public abstract IScene GetNextScene();


        /// <summary>
        /// あたり判定を有するオブジェクトとして登録します
        /// </summary>
        /// <param name="interact"></param>
        public void AddInteractObject(Entity interact)
        {
            this.interacters.Add(interact);
            interact.InteractManager = this;
        }

        public Entity GetInteractObject(Entity interact)
        {
            Entity interactor = null;

            //引数のオブジェクトのあたり判定チェックに全オブジェクトをチェックさせる
            foreach (var i in interacters)
            {
                //自分自身だった場合は飛ばす
                if (i == interact)
                    continue;

                if (interact.IsInteract(i))
                {
                    interactor = i;
                }
            }

            return interactor;
        }

        

        public void AddPlayer(EntityPlayer player)
        {
            SSTaskFactory.PlayerUpdateTask.Player = player;
            SSTaskFactory.PlayerDrawTask.Player = player;
            SSTaskFactory.PlayerMoveTask.Player = player;
            SSTaskFactory.PlayerActionTask.Player = player;
        }
    }
}
