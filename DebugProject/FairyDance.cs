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

namespace DebugProject
{
    public class FairyDance : ShootingSceneBase
    {

        public FairyDance()
            : base()
        {
            EntityReimu p = new EntityReimu();
            p.SetPosition(new SSPosition(SSGame.GetInstance().GetWindowSize().Width / 2, SSGame.GetInstance().GetWindowSize().Height - 10));
            this.AddInteractObject(p);
            this.AddPlayer(p);


            for (int i = 0; i < 30; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition(i * 10 + 10, 1));
                enemy.Type = 0;
                enemy.InteractManager = this;
                enemy.SetPopCount(i * 20 + 20);
                this.AddEnemy(enemy);
            }

            for (int i = 0; i < 30; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition(i * -10 + 700, 1));
                enemy.Type = 1;
                enemy.InteractManager = this;
                enemy.SetPopCount(i * 20 + 20);
                this.AddEnemy(enemy);
            }

            for (int i = 0; i < 10; i++)
            {
                EnemyRedFairy enemy = new EnemyRedFairy();
                enemy.SetPosition(new SSPosition(SSGame.GetInstance().GetWindowSize().Width / 2, 10));
                enemy.Type = 2;
                enemy.InteractManager = this;
                enemy.SetPopCount(i * 20 + 20);
                this.AddEnemy(enemy);

            }

            SSTaskFactory.BackGroundImageTask.ImageHandle = TextureLoader.GetInstance().Textures["back_2.jpg"];
            SSTaskFactory.BackGroundImageTask.ScrollSpeed = 2;
            SSTaskFactory.BackGroundImageTask.ImageY = 800;

         //   SoundLoader.GetInstance().PlayLoopSound("ready.mp3");
            this.soundName = "ready.mp3";
        }


    }
}
