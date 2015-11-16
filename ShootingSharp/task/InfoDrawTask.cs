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

        public int Score { get; set; }

        private Size windowSize;

        string scoreStr = "Score";
        string playerStr = "Life";

        public InfoDrawTask()
        {
            this.windowSize = new Size(SSGame.GetInstance().GetBattleWindowSize().Width + 100, SSGame.GetInstance().GetBattleWindowSize().Height);
            TextureLoader.GetInstance().LoadSprite("player_helth.png", 4, 4, 4, 512 / 4, 512 / 4);
        }


        public void Run()
        {

         
            DX.DrawGraph(this.windowSize.Width - 100, 0, TextureLoader.GetInstance().Textures["score.jpg"], DX.TRUE);

            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 200, this.scoreStr, (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));
            DX.DrawStringToHandle(this.windowSize.Width - 90, this.windowSize.Height - 170, this.Score.ToString(), (uint)DX.GetColor(5, 10, 255), FontProvider.GetSisterFontHandle(25, 9));

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
        }

        public void OnEnemyKilled(int score)
        {
            this.Score += score;
        }
    }
}
