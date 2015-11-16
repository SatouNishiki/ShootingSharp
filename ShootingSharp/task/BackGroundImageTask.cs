using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.task
{
    public class BackGroundImageTask : interfaces.ITask
    {
        /// <summary>
        /// 画像のイメージハンドル
        /// </summary>
        public int ImageHandle { get; set; }

        /// <summary>
        /// スクロール速度
        /// </summary>
        public int ScrollSpeed { get; set; }

        /// <summary>
        /// 画像の縦幅
        /// </summary>
        public int ImageY { get; set; }

        private int posY;

        private bool flag = true;

        public BackGroundImageTask()
        {
            this.ImageHandle = this.ScrollSpeed = this.ImageY = 0;
        }

        public void Run()
        {
            if (flag)
            {
                //データが足りなければ
                if (this.ImageHandle == 0 || this.ScrollSpeed == 0 || this.ImageY == 0)
                {
                    core.Logger.GetInstance().Error("on " + this.GetType().ToString() + " 's data not imput");
                    flag = false;
                    return;
                }
            }

            if (this.ImageY < SSGame.GetInstance().GetBattleWindowSize().Height)
            {
                DX.DrawExtendGraph(0, posY, SSGame.GetInstance().GetBattleWindowSize().Width, SSGame.GetInstance().GetBattleWindowSize().Height, this.ImageHandle, DX.TRUE);

                DX.DrawExtendGraph(0, this.posY - this.ImageY, SSGame.GetInstance().GetBattleWindowSize().Width, SSGame.GetInstance().GetBattleWindowSize().Height, this.ImageHandle, DX.TRUE);
            }
            else
            {
                DX.DrawGraph(0, posY, this.ImageHandle, DX.TRUE);

                DX.DrawGraph(0, this.posY - this.ImageY, this.ImageHandle, DX.TRUE);
            }

            this.posY += this.ScrollSpeed;

            if (this.posY == this.ImageY)
            {
                this.posY = 0;
            }
        }
    }
}
