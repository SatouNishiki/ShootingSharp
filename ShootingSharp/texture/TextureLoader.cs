using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DxLibDLL;

namespace ShootingSharp.texture
{
    public class TextureLoader
    {
        private const string DefaultTextureDirectoryName = "texture";

        private static TextureLoader instance;

        /// <summary>
        /// グラフィックハンドルを格納するハッシュマップ
        /// </summary>
        public Dictionary<string, int> Textures { get; set; }

        private TextureLoader()
        {
            this.Textures = new Dictionary<string, int>();

            this.GetAllTextures();
        }

        public static TextureLoader GetInstance()
        {
            if (instance == null)
                instance = new TextureLoader();

            return instance;
        }

        /// <summary>
        /// 全てのテクスチャを取得します
        /// </summary>
        public void GetAllTextures()
        {
            Dictionary<string, Bitmap> dic = this.ReadAllTextureForBitmap();

            foreach (var s in dic.Keys)
            {
                this.Textures.Add(s, this.GetGraphicsHandle(dic[s]));
            }
        }


        /// <summary>
        /// 指定の名前のディレクトリを実行フォルダ以下から探して絶対パスを返します
        /// なければ作成して絶対パスを返します
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string FindTextureDirectory(string name)
        {
            string appPath = Application.StartupPath;

            if (!Directory.Exists(appPath + "\\" + name))
            {
                Directory.CreateDirectory(appPath + "\\" + name);
            }

            return appPath + "\\" + name;
        }

        /// <summary>
        /// 全ての画像ファイルをBitmap形式で読み込みます
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Bitmap> ReadAllTextureForBitmap()
        {
            string path = this.FindTextureDirectory(DefaultTextureDirectoryName);

            //全部のファイルのフルパスが格納されている
            string[] files = Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);

            Dictionary<string, Bitmap> dic = new Dictionary<string, Bitmap>();

            foreach (var s in files)
            {
                //keyはファイルのフルパスからディレクトリ名と\\を除いたもの(=ファイル名のみ)にする
                dic.Add(s.Substring(0, path.Length + 2), new Bitmap(s));
            }

            return dic;
        }

        /// <summary>
        /// Bitmapからグラフィックハンドルを取得します
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private int GetGraphicsHandle(Bitmap bmp)
        {
            int handle = -1;

            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);
                byte[] buff = ms.ToArray();

                unsafe
                {
                    fixed (byte* p = buff)
                    {
                        DX.SetDrawValidGraphCreateFlag(DX.TRUE);
                        DX.SetDrawValidAlphaChannelGraphCreateFlag(DX.TRUE);

                        handle = DX.CreateGraphFromMem(p, buff.Length);

                        DX.SetDrawValidGraphCreateFlag(DX.FALSE);
                        DX.SetDrawValidAlphaChannelGraphCreateFlag(DX.FALSE);
                    }
                }
            }

            return handle;
        }
    }
}
