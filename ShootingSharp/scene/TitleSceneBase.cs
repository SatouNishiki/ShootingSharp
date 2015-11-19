using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using System.Reflection;
using DxLibDLL;
using System.IO;
using ShootingSharp.task;
using ShootingSharp.sound;
using ShootingSharp.texture;
using ShootingSharp.core;

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
        protected static Dictionary<string, Type> SceneList = new Dictionary<string, Type>();

        private SoundLoader sloader;

        private bool flag = true;

        private TextureLoader tLoader;

        private bool flag2 = false;
        
        public TitleSceneBase()
        {
          
            this.interval = 5;
            sloader = SoundLoader.GetInstance();
            tLoader = TextureLoader.GetInstance();
           
        }

        public virtual bool Run()
        {
            if (flag)
            {
                sloader.PlayLoopSound("title.mp3");

            }

            if (DX.CheckHitKeyAll() == 0)
                flag2 = true;

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 128);
            DX.DrawExtendGraph(0, 0, SSGame.GetInstance().GetWindowSize().Width, SSGame.GetInstance().GetWindowSize().Height, tLoader.Textures["title.jpg"], DX.TRUE);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);

            this.DrawTitle();

            if (flag)
            {
                DX.WaitTimer(200);
                flag = false;
            }

            if ((DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1 && DX.CheckHitKey(DX.KEY_INPUT_S) == 0 && DX.CheckHitKey(DX.KEY_INPUT_W) == 0) || this.runCount >= this.interval)
            {

                if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                {

                    if (this.selectedIndex < SceneList.Count - 1)
                    {
                        this.selectedIndex++;
                        DX.PlaySoundMem(sloader.Sounds["select.mp3"], DX.DX_PLAYTYPE_BACK);
                    }
                   
                }

                if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                {

                    if (this.selectedIndex > 0)
                    {
                        this.selectedIndex--;
                        DX.PlaySoundMem(sloader.Sounds["select.mp3"], DX.DX_PLAYTYPE_BACK);
                    }


                }

                if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
                {
                    if (flag2)
                    {
                        DX.PlaySoundMem(sloader.Sounds["enter.mp3"], DX.DX_PLAYTYPE_BACK);

                        Type[] ts = new Type[SceneList.Values.Count];
                        SceneList.Values.CopyTo(ts, 0);

                        SSTaskFactory.Init();

                        this.nextScene = (IShootingScene)Activator.CreateInstance(ts[this.selectedIndex]);

                        this.finishFlag = true;

                        sloader.StopSound("title.mp3");
                    }
                }

                this.runCount = 0;
            }

            if (this.interval > this.runCount)
                this.runCount++;

            return !this.IsFinished();
        }

        protected virtual void DrawTitle()
        {
            if (this.selectedIndex >= SceneList.Count)
                return;

            int i = 0;

            DX.DrawStringToHandle(50, 30, "ステージ選択", DX.GetColor(60, 255, 140), FontProvider.GetSisterFontHandle(35, 20));

            foreach (var item in SceneList)
            {
                if (i >= SceneItemsCount)
                    break;

                if (i == selectedIndex)
                {
                    DX.DrawStringToHandle(50, i * 35 + 100, item.Key, DX.GetColor(60, 255, 140), FontProvider.GetSisterFontHandle(20, 10));
                }
                else
                {
                    DX.DrawStringToHandle(50, i * 35 + 100, item.Key, DX.GetColor(60, 180, 140), FontProvider.GetSisterFontHandle(20, 10));
                }

                i++;
            }

            DX.DrawTriangle(10, selectedIndex * 35 + 100, 10, selectedIndex * 35 + 120, 30, selectedIndex * 35 + 110, DX.GetColor(255, 0, 0), DX.TRUE);
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
                if (!SceneList.ContainsKey(name))
                {
                    SceneList.Add(name, type);
                }
            }
        }
      
    }
}
