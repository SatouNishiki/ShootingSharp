using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp;
using ShootingSharp.core;

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
            game.StartScene = new ShootingSharp.scene.ShootingSampleScene();
            

            game.Run();
        }
    }
}
