using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.movie;

namespace ShootingSharp.task
{
    public class BackGroundImageTask : interfaces.ITask
    {
        public enum BackGroundMode
        {
            Image, Movie
        }

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

        /// <summary>
        /// 動画の拡大率
        /// </summary>
        public int MovieExtend { get; set; }

        public string MovieName { get; set; }

        private int posY;

        private bool flag = true;

        public BackGroundMode Mode { get; set; }

        /// <summary>
        /// 動画の上のスペースに出てくる画像(800 * 100で描画されます)
        /// </summary>
        public string BannarName { get; set; }

        public BackGroundImageTask()
        {
            this.ImageHandle = this.ScrollSpeed = this.ImageY = 0;
            this.Mode = BackGroundMode.Image;
        }

        public void Run()
        {
            if (this.Mode == BackGroundMode.Image)
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
            else
            {
                DX.DrawGraph(0, 0, texture.TextureLoader.GetInstance().Textures[this.BannarName], DX.TRUE);

                if (!MovieManager.GetInstance().PlayMovie(this.MovieName, this.MovieExtend))
                {
                    //動画が終わったらエネミーをすべて消す
                    SSTaskFactory.EnemyPopTask.Clear();
                }
            }
        }
    }
}
