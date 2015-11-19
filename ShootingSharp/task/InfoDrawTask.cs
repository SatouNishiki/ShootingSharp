using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity;
using DxLibDLL;
using System.Drawing;
using ShootingSharp.texture;
using ShootingSharp.core;

namespace ShootingSharp.task
{
    /// <summary>
    /// プレイヤーの体力とかスコアとか書くためのタスク
    /// </summary>
    public class InfoDrawTask : ITask
    {
        public EntityPlayer Player { get; set; }

        public int KillScore { get; set; }

        public int TimeScore { get; set; }

        public int ItemScore { get; set; }

        public int BossHPPercent { get; set; }

        public int Score
        { get { return this.KillScore + this.ItemScore + this.TimeScore; } }

        public string BossName { get; set; }

        private Size windowSize;

        private int drawCount;
        private int count;

        string scoreStr = "Score";
        string playerStr = "Life";
        string powerStr = "Power";

        public InfoDrawTask()
        {
            this.BossName = string.Empty;
            this.windowSize = new Size(SSGame.GetInstance().GetBattleWindowSize().Width + 100, SSGame.GetInstance().GetBattleWindowSize().Height);
            TextureLoader.GetInstance().LoadSprite("player_helth.png", 4, 4, 4, 512 / 4, 512 / 4);
          
            this.drawCount = 0;
            this.count = 0;
        }


        public void Run()
        {
            this.TimeScore++;
         
            DX.DrawGraph(this.windowSize.Width - 100, 0, TextureLoader.GetInstance().Textures["score.jpg"], DX.TRUE);

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 500, this.BossName, (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(15, 12));

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 450, "ボスHP", (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));

            DX.DrawBox(this.windowSize.Width - 95, this.windowSize.Height - 404, this.windowSize.Width - 5, this.windowSize.Height - 385, DX.GetColor(255, 255, 255), DX.FALSE);
            DX.DrawBox(this.windowSize.Width - 90, this.windowSize.Height - 402, this.windowSize.Width - 90 + this.BossHPPercent / 10 * 8, this.windowSize.Height - 387, DX.GetColor(0, 225, 0), DX.TRUE);

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 360, "ボム", (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));

            for (int i = 0; i < Player.GetBomCount(); i++)
            {
                DX.DrawExtendGraph(
                    this.windowSize.Width - 95 + i * 40,
                    this.windowSize.Height - 334,
                    this.windowSize.Width - 61 + i * 40,
                    this.windowSize.Height - 300,
                    TextureLoader.GetInstance().Textures["bom_count" + (drawCount % 6).ToString() + ".png" ],
                    DX.TRUE);
            }

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 300, this.scoreStr, (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));
            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 270, this.Score.ToString(), (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 200, this.powerStr, (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));
            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 170, Player.GetPower().ToString(), (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 100, this.playerStr, (uint)DX.GetColor(255, 5, 5), FontProvider.GetSisterFontHandle(25, 9));
        


            for (int i = 0; i < Player.Life; i++)
            {
                DX.DrawExtendGraph(
                    this.windowSize.Width - 95 + i * 20,
                    this.windowSize.Height - 60,
                    this.windowSize.Width - 55 + i * 20,
                    this.windowSize.Height - 20,
                    TextureLoader.GetInstance().Textures["player_helth.png3"],
                    DX.TRUE);
            }

            if (count % 2 == 0)
                drawCount++;

            count++;
        }

        public void OnEnemyKilled(int score)
        {
            this.KillScore += score;
        }
    }
}
