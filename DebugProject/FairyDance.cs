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
        private Size windowSize = new Size(SSGame.GetInstance().GetWindowSize().Width, SSGame.GetInstance().GetWindowSize().Height);

        public FairyDance()
            : base()
        {
            Random rnd = new Random();

            EntityReimu p = new EntityReimu();
            this.AddInteractObject(p);
            this.AddPlayer(p);

/*
            for (int i = 0; i < 80; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition(rnd.Next(this.windowSize.Width), 1));
                enemy.MovingType = 2;
                enemy.ActionType = 3;

                enemy.SetPopCount(i * 40 + 20);
                this.AddEnemy(enemy);
            }
           
            for (int i = 0; i < 50; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition(rnd.Next(this.windowSize.Width), 1));
                enemy.MovingType = 3;

                enemy.ActionType = 3;

                enemy.SetPopCount(i * 30 + 20 + 20 * 50);
                this.AddEnemy(enemy);
            }
            */

            this.AddBoss(new BossRumia());

            SSTaskFactory.BackGroundImageTask.ImageHandle = TextureLoader.GetInstance().Textures["back_2.jpg"];
            SSTaskFactory.BackGroundImageTask.ScrollSpeed = 2;
            SSTaskFactory.BackGroundImageTask.ImageY = 800;

         
            this.soundName = "ready.mp3";
        }


    }
}
