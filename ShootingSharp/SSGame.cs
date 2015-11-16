using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.interfaces;
using System.Drawing;

namespace ShootingSharp
{
    /// <summary>
    /// ライブラリのコアクラス
    /// </summary>
    public class SSGame
    {
        private static SSGame instance;

        public IScene StartScene { get; set; }

        /// <summary>
        /// シーンが格納されたネームスペースを設定
        /// </summary>
        public List<string> SceneNameSpace { get; set; }

        private IScene nowScene;

        private SSGame()
        {
            this.SceneNameSpace = new List<string>();
        }

        public void Run()
        {
          
            nowScene = StartScene;

            //プロセスループ
            while (DX.ProcessMessage() == 0)
            {
                DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                DX.ClearDrawScreen();

                //TODO:内部処理
                if (!this.nowScene.Run())
                {
                    //今のシーンの処理が終わったら

                    if (this.nowScene.ExistNextScene())
                    {
                        this.nowScene = this.nowScene.GetNextScene();
                    }
                    else
                    {
                        break;
                    }
                }

                DX.ScreenFlip();
                
            }

            DX.DxLib_End();
        }

        public static SSGame GetInstance()
        {
            if (instance == null)
            {
                instance = new SSGame();

                //以下DxLibの初期化処理

                DX.ChangeWindowMode(DX.TRUE);

                DX.SetGraphMode(800, 600, 32);

                DX.SetAlwaysRunFlag(DX.TRUE);

                if (DX.DxLib_Init() == -1)
                {
                    return null;
                }
            }

            return instance;
        }

        /// <summary>
        /// バトルのときのプレイヤー移動可能ウィンドウサイズを取得
        /// </summary>
        /// <returns></returns>
        public Size GetBattleWindowSize()
        {
            int x = 0;
            int y = 0;
            DX.GetWindowSize(out x, out y);

            return new Size(x - 100, y);
        }

        /// <summary>
        /// 標準ウインドウサイズを取得
        /// </summary>
        /// <returns></returns>
        public Size GetWindowSize()
        {
            int x = 0;
            int y = 0;
            DX.GetWindowSize(out x, out y);

            return new Size(x, y);
        }
    }
}