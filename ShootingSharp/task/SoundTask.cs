using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.task
{
    public class SoundTask : interfaces.ITask
    {
        public int SoundHandle { get; set; }

        public int PlayType { get; set; }

        private bool flag = true;

        public SoundTask()
        {
            this.SoundHandle = 0;
            this.PlayType = DxLibDLL.DX.DX_PLAYTYPE_LOOP;
        }

        public void Run()
        {
            if (flag)
            {
                if (this.SoundHandle == 0)
                {
                    core.Logger.GetInstance().Error("on " + this.GetType().ToString() + " 's sound handle is not input");
                    flag = false;
                }
            }

            DxLibDLL.DX.PlaySoundMem(this.SoundHandle, this.PlayType);
        }
    }
}
