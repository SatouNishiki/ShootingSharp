using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.scene;
using ShootingSharp.entity.player;
using ShootingSharp.task;
using ShootingSharp.position;
using ShootingSharp.entity.enemy;
using ShootingSharp;
using ShootingSharp.texture;
using ShootingSharp.sound;
using System.Drawing;
using ShootingSharp.entity.boss;

namespace DebugProject
{
    public class FairyDance : ShootingSceneBase
    {
        

        public FairyDance()
            : base()
        {
            EntityReimu p = new EntityReimu();
            this.AddPlayer(p);


            for (int i = 0; i < 80; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition((int)Math.Round(Math.Abs(Math.Sin(i) * (this.windowSize.Width - 400))) + 200, 1));
                enemy.MovingType = 2;
                enemy.ActionType = 3;

                enemy.SetPopCount(i * 30 + 20);
                this.AddEnemy(enemy);
            }
           
            for (int i = 0; i < 50; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition((int)Math.Round(Math.Abs(Math.Cos(i) * (this.windowSize.Width - 400))) + 200, 1));
                enemy.MovingType = 3;

                enemy.ActionType = 3;

                enemy.SetPopCount(i * 20 + 20 + 20 * 50);
                this.AddEnemy(enemy);
            }/*


            for (int i = 0; i < 30; i++)
            {
                EnemyBlueFairy enemy = new EnemyBlueFairy();
                enemy.SetPosition(new SSPosition(1, 1));
                enemy.MovingType = 0;

                enemy.ActionType = 0;

                enemy.SetPopCount(i * 40 + 20);
                this.AddEnemy(enemy);
            }
            */

            this.AddBoss(new BossRumia());
            
            SSTaskFactory.BackGroundImageTask.ImageHandle = TextureLoader.GetInstance().Textures["back_3.jpg"];
            SSTaskFactory.BackGroundImageTask.ScrollSpeed = 2;
            SSTaskFactory.BackGroundImageTask.ImageY = 768;
         
            this.soundName = "ready.mp3";
        }


    }
}
