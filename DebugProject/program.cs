using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp;
using ShootingSharp.core;
using ShootingSharp.scene;
using ShootingSharp.scene.shootingScene;

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
            

           // game.StartScene = new ShootingSharp.scene.shootingScene.ShootingSampleScene();

            TitleSceneBase titleScene = new TitleSceneBase();

            titleScene.AddScene("サンプルシーン1", typeof(ShootingSampleScene));
            titleScene.AddScene("サンプルシーン2", typeof(ShootingSampleScene));
            titleScene.AddScene("サンプルシーン3", typeof(ShootingSampleScene));
            titleScene.AddScene("フェアリーダンス", typeof(FairyDance));
            
            game.StartScene = titleScene;

            game.Run();
        }
    }
}
