using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DxLibDLL;
using ShootingSharp.core;

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
            string path = FileUtility.FindTextureDirectory(DefaultSoundDirectoryName);

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


        public void PlayLoopSound(string name)
        {
            DX.PlaySoundMem(this.Sounds[name], DX.DX_PLAYTYPE_LOOP);
        }

        public void StopSount(string name)
        {
            DX.StopSoundMem(this.Sounds[name]);
        }
    }
}
