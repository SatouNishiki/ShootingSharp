using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShootingSharp.position;

namespace ShootingSharp.interfaces
{
    /// <summary>
    /// 描画可能なオブジェクト
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// 画像の名前
        /// </summary>
        /// <returns></returns>
        string GetTextureName();

        /// <summary>
        /// 画像のサイズ
        /// </summary>
        /// <returns></returns>
        Size GetTextureSize();

        /// <summary>
        /// 画像の描画開始場所
        /// </summary>
        /// <returns></returns>
        SSPosition GetTexturePosition();

        /// <summary>
        /// 描画します
        /// </summary>
        void Draw();
    }
}
