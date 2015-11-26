using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.core
{
    /// <summary>
    /// 高速な計算処理を提供する
    /// </summary>
    public static class SSMath
    {
        private static Dictionary<int, double> cosDic;

        public static void Init()
        {
            cosDic = new Dictionary<int, double>();

            for (int i = 0; i < 90; i++)
            {
                cosDic.Add(i, Math.Cos(i * Math.PI / 2 / 90));
            }
        }

        public static double Cos(double theta)
        {
            double sign = 1;

            if (theta < 0)
                theta = -theta;

            theta /= Math.PI;

            theta = (theta - (int)(theta / 2) * 2);

            if (theta > 1)
                theta = 2 - theta;

            if (theta > 0.5)
            {
                theta = 1 - theta;
                sign = -1;
            }

            int i = (int)(theta * 2 * 90);
            double d;

            try
            {
                d = sign * cosDic[i];
            }
            catch (KeyNotFoundException)
            {
                d = Math.Cos(theta);
            }

            return d;
        }

        public static double Sin(double theta)
        {
            return Cos(theta - Math.PI / 2);
        }
    }
}
