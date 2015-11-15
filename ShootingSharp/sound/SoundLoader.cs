using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DxLibDLL;

namespace ShootingSharp.sound
{
    public class SoundLoader
    {
        private const string DefaultSoundDirectoryName = "sound";


        private static SoundLoader instance;

        private core.Logger logger = core.Logger.GetInstance();

        /// <summary>
        /// 音声格納ハッシュマップ
        /// </summary>
        public Dictionary<string, int> Sounds { get; set; }

        private SoundLoader()
        {
            this.Sounds = new Dictionary<string, int>();

            this.LoadAllSounds();
        }

        public static SoundLoader GetInstance()
        {
            if (instance == null) instance = new SoundLoader();

            return instance;
        }

        public void LoadAllSounds()
        {
            this.Sounds.Clear();
            string path = this.FindTextureDirectory(DefaultSoundDirectoryName);

            //全部のファイルのフルパスが格納されている
            string[] files = Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);

            foreach (var s in files)
            {
                string name = s.Substring(path.Length + 1, s.Length - path.Length - 1);
                //keyはファイルのフルパスからディレクトリ名と\\を除いたもの(=ファイル名のみ)にする
                this.Sounds.Add(name, DX.LoadSoundMem(s));

                logger.Debug(name + " is load");
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
    }
}
