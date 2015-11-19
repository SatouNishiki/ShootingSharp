using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;
using ShootingSharp.sound;

namespace ShootingSharp.core
{
    public static class Effecter
    {
        private static TextureLoader loader = TextureLoader.GetInstance();
        private static SoundLoader sLoader = SoundLoader.GetInstance();

       /// <summary>
        /// 指定名の画像をつかって左上(x1, y1)から右下(x2+1, y2+1)にかけてカットイン描画します
       /// </summary>
       /// <param name="name"></param>
       /// <param name="x1"></param>
       /// <param name="y1"></param>
       /// <param name="x2"></param>
       /// <param name="y2"></param>
       /// <param name="wait"></param>
        public static void CutIn(string name, int x1, int y1, int x2, int y2, int wait)
        {
            DX.DrawExtendGraph(
                        x1,
                        y1,
                        x2 + 1,
                        y2 + 1,
                        TextureLoader.GetInstance().Textures[name],
                        DX.TRUE
                        );

            DX.ScreenFlip();

            sLoader.PlayEffect("cutin.mp3");

            DX.WaitTimer(wait);
        }

        /// <summary>
        /// 指定名の画像群をx,yを起点としてmarginだけスペースを空けてタイプタイター的に描画します
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="margin"></param>
        /// <param name="names"></param>
        public static void TypeWrite(int x, int y,int margin,  params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["click.mp3"], DX.DX_PLAYTYPE_BACK);
                
                DX.DrawGraph(x + margin * i, y, loader.Textures[names[i]], DX.TRUE);

                DX.ScreenFlip();
                DX.WaitTimer(150);
            }
        }

        /// <summary>
        /// 指定名の画像群をx,yを起点としてmarginだけスペースを空けて描画します
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="margin"></param>
        /// <param name="name"></param>
        public static void DrawContinuityGraph(int x, int y, int margin, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                DX.DrawGraph(x + margin * i, y, loader.Textures[names[i]], DX.TRUE);
            }
        }

        /// <summary>
        /// 画像を点滅させて描画します
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="textureName"></param>
        /// <param name="soundName"></param>
        /// <param name="count"></param>
        /// <param name="speed"></param>
        public static void DrawFlashingGraph(int x, int y, int x2, int y2, string textureName, string soundName, int count, int speed)
        {
            for (int c = 0; c < count; c++)
            {

                SoundLoader.GetInstance().PlayEffect(soundName);

                // フェードイン処理
                for (int i = 0; i < 255; i += speed)
                {
                    // 描画輝度をセット
                    DX.SetDrawBright(i, i, i);

                    // グラフィックを描画
                    DX.DrawExtendGraph(
                        x,
                        y,
                        x2,
                        y2,
                        TextureLoader.GetInstance().Textures[textureName],
                        DX.TRUE);

                    DX.ScreenFlip();
                }

                // フェードアウト処理
                for (int i = 0; i < 255; i += speed)
                {
                    // 描画輝度をセット
                    DX.SetDrawBright(255 - i, 255 - i, 255 - i);

                    // グラフィックを描画
                    DX.DrawExtendGraph(
                        x,
                        y,
                        x2,
                        y2,
                        TextureLoader.GetInstance().Textures[textureName],
                        DX.TRUE);

                    DX.ScreenFlip();
                }
            }
            DX.SetDrawBright(255, 255, 255);
        }
    }
}
