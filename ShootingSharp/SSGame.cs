using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp
{
    /// <summary>
    /// ライブラリのコアクラス
    /// </summary>
    public class SSGame
    {
        private static SSGame instance;

        private SSGame()
        {

        }

        public void Run()
        {
            //プロセスループ
            while (DX.ProcessMessage() == 0)
            {
                //内部処理
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

                if (DX.DxLib_Init() == -1)
                {
                    return null;
                }
            }

            return instance;
        }
    }
}
