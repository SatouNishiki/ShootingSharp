using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.position
{
    /// <summary>
    /// 長方形の4隅の座標をまとめたクラス
    /// </summary>
    public class SquareSSPositon
    {/*
        public SSPosition LeftTop { get; set; }

        public SSPosition LeftBottom { get; set; }

        public SSPosition RightTop { get; set; }

        public SSPosition RightBottom { get; set; }*/

        public readonly int LeftTop = 0;
        public readonly int LeftBottom = 1;
        public readonly int RightTop = 2;
        public readonly int RightBottom = 3;

        public SSPosition[] SquarePosition { get; set; }


        public SquareSSPositon()
        {
            this.SquarePosition = new SSPosition[4];
        }
    }
}
