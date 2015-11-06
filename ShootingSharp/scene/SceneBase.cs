using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.scene
{
    public abstract class SceneBase : IScene, IInteractManager
    {
        /// <summary>
        /// あたり判定を行うオブジェクトを全て格納するリスト
        /// </summary>
        protected List<IInteract> interacters;

        public void Run()
        {
            if (this.Update != null)
            {
                this.Update();
            }
        }

        public abstract bool ExistNextScene();

        public abstract bool IsFinished();

        public abstract IScene GetNextScene();

        public event Action Update;

        public void AddInteractObject(IInteract interact)
        {
            this.interacters.Add(interact);
        }

        public IInteract GetInteractObject(IInteract interact)
        {
            IInteract interactor = null;

            //引数のオブジェクトのあたり判定チェックに全オブジェクトをチェックさせる
            foreach (var i in interacters)
            {
                if (interact.IsInteract(i))
                {
                    interactor = i;
                }
            }

            return interactor;
        }
    }
}
