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

                if (SSTaskFactory.EnemyUpdateTask.EnemyList.Count == 0 && SSTaskFactory.EnemyPopTask.EnemyList.Count == 0)
                {
                    runCount++;

                }
                if (runCount > 200)
                {

                    runCount = 0;
                   
                    SoundLoader.GetInstance().PlayEffect("alert.mp3");

                    // フェードイン処理
                    for (int i = 0; i < 255; i += 5)
                    {
                        // 描画輝度をセット
                        DX.SetDrawBright(i, i, i);

                        // グラフィックを描画
                        DX.DrawExtendGraph(
                            100,
                            100,
                            SSGame.GetInstance().GetBattleWindowSize().Width,
                            SSGame.GetInstance().GetBattleWindowSize().Height,
                            TextureLoader.GetInstance().Textures["encount_boss.png"],
                            DX.TRUE);
                        
                        DX.ScreenFlip();
                    }

                    // フェードアウト処理
                    for (int i = 0; i < 255; i += 5)
                    {
                        // 描画輝度をセット
                        DX.SetDrawBright(255 - i, 255 - i, 255 - i);
		
                        // グラフィックを描画
                        DX.DrawExtendGraph(
                            100,
                            100,
                            SSGame.GetInstance().GetBattleWindowSize().Width,
                            SSGame.GetInstance().GetBattleWindowSize().Height,
                            TextureLoader.GetInstance().Textures["encount_boss.png"],
                            DX.TRUE);

                        DX.ScreenFlip();
                    }

                    SoundLoader.GetInstance().PlayEffect("alert.mp3");

                    // フェードイン処理
                    for (int i = 0; i < 255; i += 5)
                    {
                        // 描画輝度をセット
                        DX.SetDrawBright(i, i, i);

                        // グラフィックを描画
                        DX.DrawExtendGraph(
                            100,
                            100,
                            SSGame.GetInstance().GetBattleWindowSize().Width,
                            SSGame.GetInstance().GetBattleWindowSize().Height,
                            TextureLoader.GetInstance().Textures["encount_boss.png"],
                            DX.TRUE);

                        DX.ScreenFlip();
                    }

                    // フェードアウト処理
                    for (int i = 0; i < 255; i += 5)
                    {
                        // 描画輝度をセット
                        DX.SetDrawBright(255 - i, 255 - i, 255 - i);

                        // グラフィックを描画
                        DX.DrawExtendGraph(
                            100,
                            100,
                            SSGame.GetInstance().GetBattleWindowSize().Width,
                            SSGame.GetInstance().GetBattleWindowSize().Height,
                            TextureLoader.GetInstance().Textures["encount_boss.png"],
                            DX.TRUE);

                        DX.ScreenFlip();
                    }

                    DX.SetDrawBright(255, 255, 255);

                    DX.DrawExtendGraph(
                        0,
                        0,
                        SSGame.GetInstance().GetWindowSize().Width,
                        SSGame.GetInstance().GetWindowSize().Height,
                        TextureLoader.GetInstance().Textures[BossList[0].GetCutinTextureName()],
                        DX.TRUE
                        );

                    DX.ScreenFlip();

                    DX.WaitTimer(2000);

                    foreach (var item in this.BossList)
                    {
                        SSTaskFactory.BossActionTask.BossList.Add(item);
                        SSTaskFactory.BossDrawTask.BossList.Add(item);
                        SSTaskFactory.BossMoveTask.BossList.Add(item);
                        SSTaskFactory.BossUpdateTask.BossList.Add(item);
                        SSTaskFactory.InfoDrawTask.BossName += item.GetName();
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
