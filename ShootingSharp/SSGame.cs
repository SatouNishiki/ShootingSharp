using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.interfaces;
using System.Drawing;
using ShootingSharp.core;

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

        private FPSController fps;

        private SSGame()
        {
            this.SceneNameSpace = new List<string>();
            this.fps = new FPSController(60.0f, 800);
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

                fps.All();
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

                DX.SetWindowText("ShootingSharp");

                DX.ChangeWindowMode(DX.TRUE);

                DX.SetGraphMode(800, 600, 32);

                DX.SetAlwaysRunFlag(DX.TRUE);

                if (DX.DxLib_Init() == -1)
                {
                    return null;
                }

                DX.ChangeFontType(DX.DX_FONTTYPE_ANTIALIASING_8X8);
                
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