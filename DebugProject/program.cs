using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp;
using ShootingSharp.core;
using ShootingSharp.scene;

namespace DebugProject
{
    static class program
    {
        [STAThread]
        static void Main()
        {
            Logger logger = Logger.GetInstance();

            //ログ出力デリゲートにコンソール出力のラムダ式をリンク
            logger.OnWriteLog = (string log) =>
            {
                Console.WriteLine(log);
            };

            SSGame game = SSGame.GetInstance();
            


            TitleSceneBase titleScene = new TitleSceneBase();

           
            titleScene.AddScene("フェアリーダンス", typeof(FairyDance));
            
            game.StartScene = titleScene;

            game.Run();
        }
    }
}
