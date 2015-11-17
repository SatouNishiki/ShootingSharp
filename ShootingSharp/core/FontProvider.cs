using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.core
{
    public static class FontProvider
    {
        private static System.Drawing.Text.PrivateFontCollection fontCollection = new System.Drawing.Text.PrivateFontCollection();

        private static Dictionary<int, int> dic = new Dictionary<int, int>();

        /// <summary>
        /// 妹フォントを取得します
        /// </summary>
        /// <param name="size">大きさ</param>
        /// <param name="thick">太さ</param>
        /// <returns></returns>
        public static int GetSisterFontHandle(int size, int thick)
        {
            if (!dic.ContainsKey(size))
            {
                string path = FileUtility.FindTextureDirectory("font");

                fontCollection.AddFontFile(path + "\\" + "SistersFS.ttf");

                dic.Add(size, DX.CreateFontToHandle("妹フォント標準", size, thick, DX.DX_FONTTYPE_NORMAL));
            }
            return dic[size];
        }

    }
}
