using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using System.Reflection;
using DxLibDLL;
using System.IO;

namespace ShootingSharp.scene
{
    public class TitleSceneBase : IScene
    {
        /// <summary>
        /// タイトル画面に最大何個まで一緒にシーン名を表示させるか
        /// </summary>
        protected const int SceneItemsCount = 10;

        /// <summary>
        /// 背景画像のハンドル
        /// </summary>
        protected int backgroundImageHandle;

        protected int selectedIndex;

        protected int runCount;

        protected int interval;

        private bool finishFlag;

        private IShootingScene nextScene;

        /// <summary>
        /// 表示名,型名
        /// </summary>
        protected Dictionary<string, Type> SceneList { get; set; }

        public TitleSceneBase()
        {
          //  this.ShootingSceneList = new List<Type>();
            this.SceneList = new Dictionary<string, Type>();
            this.interval = 5;
       //     this.LoadAllScene();
        }

        public virtual bool Run()
        {

            this.DrawTitle();

            if (this.runCount >= this.interval)
            {

                if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                {
                    if (this.selectedIndex < this.SceneList.Count - 1)
                        this.selectedIndex++;
                }

                if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                {
                    if (this.selectedIndex > 0)
                    {
                        this.selectedIndex--;
                    }
                }

                if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
                {
                    Type[] ts = new Type[this.SceneList.Values.Count];
                    this.SceneList.Values.CopyTo(ts, 0);

                    this.nextScene = (IShootingScene)Activator.CreateInstance(ts[this.selectedIndex]);

                    this.finishFlag = true;
                }

                this.runCount = 0;
            }

            if (this.interval > this.runCount)
                this.runCount++;

            return !this.IsFinished();
        }

        protected virtual void DrawTitle()
        {
            if (this.selectedIndex >= this.SceneList.Count)
                return;

            /*
            for (int i = 0; i < SceneList.Count; i++)
            {
                if (i >= SceneItemsCount)
                    break;

                if (i != selectedIndex)
                    DX.DrawString(10, i * 10 + 10, this.SceneList[i].ToString(), DX.GetColor(100, 0, 0));
                else
                    DX.DrawString(10, i * 10 + 10, this.ShootingSceneList[i].ToString(), DX.GetColor(255, 0, 0));

            }*/

            int i = 0;

            foreach (var item in this.SceneList)
            {
                if (i >= SceneItemsCount)
                    break;

                if (i == selectedIndex)
                {
                    DX.DrawString(10, i * 15 + 10, item.Key, DX.GetColor(255, 0, 0));
                }
                else
                {
                    DX.DrawString(10, i * 15 + 10, item.Key, DX.GetColor(100, 0, 0));
                }

                i++;
            }
        }

        public bool ExistNextScene()
        {
            return true;
        }

        public bool IsFinished()
        {
            return this.finishFlag;
        }

        public IScene GetNextScene()
        {
            return this.nextScene;
        }

        public void AddScene(string name, Type type)
        {
            if ((typeof(IShootingScene)).IsAssignableFrom(type))
            {
                if (!this.SceneList.ContainsKey(name))
                {
                    this.SceneList.Add(name, type);
                }
            }
        }
      
    }
}
