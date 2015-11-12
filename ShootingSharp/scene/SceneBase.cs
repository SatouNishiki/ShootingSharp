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
        protected List<IInteracter> interacters;

        /// <summary>
        /// シーンに存在するオブジェクトをすべて格納するリスト
        /// </summary>
        protected List<IUpdateable> sceneObjects;

        public SceneBase()
        {
            this.interacters = new List<IInteracter>();
            this.sceneObjects = new List<IUpdateable>();
        }

        public bool Run()
        {
            if (this.Update != null)
            {
                this.Update();
            }

            if (this.ExistNextScene())
            {
                //TODO: 遷移処理
            }

            return !this.IsFinished();
        }

        public abstract bool ExistNextScene();

        public abstract bool IsFinished();

        public abstract IScene GetNextScene();

        public event Action Update;

        /// <summary>
        /// あたり判定を有するオブジェクトとして登録します
        /// </summary>
        /// <param name="interact"></param>
        public void AddInteractObject(IInteracter interact)
        {
            this.interacters.Add(interact);
            interact.InteractManager = this;
        }

        public void AddSceneObject(IUpdateable sceneObject)
        {
            this.sceneObjects.Add(sceneObject);
            this.Update += sceneObject.OnUpdate;
        }


        public IInteracter GetInteractObject(IInteracter interact)
        {
            IInteracter interactor = null;

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
