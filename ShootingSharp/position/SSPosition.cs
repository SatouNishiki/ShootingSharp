using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.position
{
    public class SSPosition
    {
        private int posX;
        private int posY;

        public SSPosition()
        {
            this.posX = 0;
            this.posY = 0;
        }

        public SSPosition(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        /// <summary>
        /// 画面左上から数えたX座標
        /// </summary>
        public int PosX
        {
            get { return this.posX; }
            set { this.posX = value; }
        }

        /// <summary>
        /// 画面左上から数えたY座標
        /// </summary>
        public int PosY
        {
            get { return this.posY; }
            set { this.posY = value; }
        }
        
    }
}
