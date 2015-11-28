using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.scene;
using ShootingSharp.entity.player;
using DebugProject.enemy;
using ShootingSharp.position;
using ShootingSharp.task;

namespace DebugProject
{
    public class Bakemono : ShootingSceneBase
    {
        public Bakemono()
            : base()
        {
            EntityReimu p = new EntityReimu();
            this.AddPlayer(p);

            for (int i = 0; i < 50; i++)
            {
                Mayoi enemy = new Mayoi();
             
                enemy.SetPosition(new SSPosition(this.windowSize.Width / 2 + (i % 20) * 10, 0));
                enemy.SetType(0);

                enemy.SetPopCount(i * 20 + 20);
                this.AddEnemy(enemy);

            }

            for (int i = 0; i < 150; i++)
            {
                Mayoi enemy = new Mayoi();

                enemy.SetPosition(new SSPosition(this.windowSize.Width / 2 + (i % 20) * 10, 0));
                enemy.SetType(0);

                enemy.SetPopCount(i * 50 + 20 + 50 * 20);
                this.AddEnemy(enemy);

            }

            for (int i = 0; i < 200; i++)
            {
                Tsubasa t = new Tsubasa();
                t.SetPosition(new SSPosition(20 + (i % 20) * 10, 10));

                t.SetPopCount(i * 30);
                this.AddEnemy(t);

            }

            for (int i = 0; i < 200; i++)
            {
                Tsukihi t = new Tsukihi();
                t.SetPosition(new SSPosition(this.windowSize.Width / 2 + (i % 20) * 30, 10));

                t.SetPopCount(50 * 20 + 20 + 30 * 20 + i * 30);
                this.AddEnemy(t);

            }

            SSTaskFactory.BackGroundImageTask.Mode = BackGroundImageTask.BackGroundMode.Movie;
            SSTaskFactory.BackGroundImageTask.MovieName = "bakemono.avi";
            SSTaskFactory.BackGroundImageTask.MovieExtend = 1;
            SSTaskFactory.BackGroundImageTask.BannarName = "bakemono_bannar.png";
        }
    }
}
