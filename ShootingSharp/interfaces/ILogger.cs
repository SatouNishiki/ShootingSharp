using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.interfaces
{


    /// <summary>
    /// ログ出力時に呼び出されるデリゲート
    /// </summary>
    /// <param name="log"></param>
    public delegate void WriteLogDelegate(string log);

    /// <summary>
    /// ログ出力用インターフェース
    /// </summary>
    public interface ILogger
    {
    }
}
