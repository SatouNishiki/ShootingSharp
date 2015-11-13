using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.core
{
    /// <summary>
    /// ログ出力クラス
    /// </summary>
    public class Logger : ILogger
    {
        private static Logger instance;

        private Logger()
        {

        }

        public static Logger GetInstance()
        {
            if (instance == null) instance = new Logger();

            return instance;
        }

        public WriteLogDelegate OnWriteLog { get; set; }

        public void Debug(string log)
        {
            if (this.OnWriteLog != null)
            {
                this.OnWriteLog("[Debug]" + this.GetHourHeader() + log);
            }
        }

        public void Error(string log)
        {
            if (this.OnWriteLog != null)
            {
                this.OnWriteLog("[Error]" + this.GetHourHeader() + log);
            }
        }

        public void Fatal(string log)
        {
            if (this.OnWriteLog != null)
            {
                this.OnWriteLog("[Fatal]" + this.GetHourHeader() + log);
            }
        }

        private string GetHourHeader()
        {
            return "[" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":"
                + DateTime.Now.Minute + ":" + DateTime.Now.Second + "]";
        }
    }
}
