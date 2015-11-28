using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.core;
using ShootingSharp.sound;
using ShootingSharp.texture;

namespace ShootingSharp.task
{
    public class BossPopTask : interfaces.ITask
    {

        private int runCount;
        public List<entity.boss.Boss> BossList { get; set; }

        private bool bossPoped;


        public BossPopTask()
        {
            this.runCount = 0;
            this.bossPoped = true;
            this.BossList = new List<entity.boss.Boss>();
        }

        public void Run()
        {
            if (bossPoped)
            {

                if (SSTaskFactory.EnemyUpdateTask.EnemyList.Count == 0 && SSTaskFactory.EnemyPopTask.GetRemainEnemys() == 0)
                {
                    runCount++;

                }
                if (runCount > 200)
                {

                    SoundLoader.GetInstance().StopAllSound();
                    movie.MovieManager.GetInstance().StopMovie();

                    runCount = 0;

                    Effecter.DrawFlashingGraph(
                        100,
                        100,
                        SSGame.GetInstance().GetBattleWindowSize().Width,
                        SSGame.GetInstance().GetBattleWindowSize().Height,
                        "encount_boss.png",
                        "alert.mp3",
                        2,
                        5);

                    Effecter.CutIn(BossList[0].GetCutinTextureName(), 0, 0, SSGame.GetInstance().GetWindowSize().Height, SSGame.GetInstance().GetWindowSize().Height, 2000);

                    SoundLoader.GetInstance().PlayLoopSound(this.BossList[0].GetMusicName());

                    foreach (var item in this.BossList)
                    {
                        SSTaskFactory.InfoDrawTask.BossName += item.GetName();
                        item.OnPop();
                    }

                    this.bossPoped = false;
                }

            }

            List<entity.boss.Boss> temp = new List<entity.boss.Boss>();

            foreach (var item in this.BossList)
            {
                if (!item.IsLiving())
                {
                    temp.Add(item);
                }
            }

            this.BossList.RemoveAll(e => temp.IndexOf(e) >= 0);

        }
    }
}
