using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ShootingSharp.core
{
    public static class FileUtility
    {
        /// <summary>
        /// 指定の名前のディレクトリを実行フォルダ以下から探して絶対パスを返します
        /// なければ作成して絶対パスを返します
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindTextureDirectory(string name)
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
