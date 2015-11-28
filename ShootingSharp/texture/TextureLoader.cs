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
using ShootingSharp.core;

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

        private core.Logger logger = core.Logger.GetInstance();

        private CompositeTextureProvider com;
       
        private TextureLoader()
        {
            this.Textures = new Dictionary<string, int>();

            com = new CompositeTextureProvider();
       
            this.LoadAllTexture();
            
        }

        public static TextureLoader GetInstance()
        {
            if (instance == null)
                instance = new TextureLoader();

            return instance;
        }


        /// <summary>
        /// 現在のテクスチャ保存データをすべて消去し、新しく全ての画像ファイルを読み込みます
        /// </summary>
        private void LoadAllTexture()
        {
            this.Textures.Clear();

            string path = FileUtility.FindTextureDirectory(DefaultTextureDirectoryName);

            //全部のファイルのフルパスが格納されている
            string[] files = Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);


            foreach (var s in files)
            {
                int handle = com.GetImgHandle(Path.GetFileName(s));

                if (handle > 0)
                {
                    this.Textures.Add(Path.GetFileName(s), handle);
                    logger.Debug(Path.GetFileName(s) + " is hyspeed load");
                }
                else
                {
                    this.Textures.Add(Path.GetFileName(s), DX.LoadGraph(s));
                    logger.Debug(Path.GetFileName(s) + " is default load");
                }

                
            }

        }

        /// <summary>
        /// 分割画像を読み込みます
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="xNum">xの分割数</param>
        /// <param name="yNum">yの分割数</param>
        /// <param name="allNum">分割総数</param>
        /// <param name="xSize">分割後のxのサイズ</param>
        /// <param name="ySize">分割後のyのサイズ</param>
        public void LoadSprite(string fileName, int xNum, int yNum, int allNum, int xSize, int ySize)
        {
            //同じファイルがロード済みなら
            if (this.Textures.ContainsKey(fileName + "0"))
            {
                return;
            }

            string path = FileUtility.FindTextureDirectory(DefaultTextureDirectoryName);
       

            int[] buf = com.GetDivGraph(fileName, xNum, yNum, allNum, xSize, ySize);

            if (buf == null)
            {
                buf = new int[allNum];
                DX.LoadDivGraph(path + "\\" + fileName, allNum, xNum, yNum, xSize, ySize, out buf[0]);
            }

            int count = 0;

            foreach (var s in buf)
            {
                this.Textures.Add(fileName + count.ToString(), buf[count]);
                count++;

            }
        }
        
        /// <summary>
        /// 画像を指定の場所から指定の大きさだけ切り取って別の画像としてハンドルを取得、格納します
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <param name="name"></param>
        public void LoadDerivationGraph(int posX, int posY, int sizeX, int sizeY, string name)
        {
            if (!this.Textures.ContainsKey(name + posX.ToString() + "+" + posY.ToString()))
            {
                int handle = com.GetDerivationGraph(167, 62, 30, 30, name);

                if (handle > 0)
                    this.Textures.Add(name + posX.ToString() + "+" + posY.ToString(), handle);
                else
                    this.Textures.Add(name + posX.ToString() + "+" + posY.ToString(), DX.DerivationGraph(167, 62, 30, 30, Textures[name]));
            }
        }
        
       
      
    }
}
