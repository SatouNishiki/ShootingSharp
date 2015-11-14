using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity
{
    /// <summary>
    /// あたり判定の計算クラス
    /// </summary>
    public static class InteractCalculator
    {
        /// <summary>
        /// 二次元ベクトルを表す内部クラス
        /// </summary>
        private class Vector2
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        /// <summary>
        /// 点が円の中にあるかどうか
        /// </summary>
        /// <param name="oppentPos"></param>
        /// <param name="myPos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private static bool PointCircleInteractHelper(SSPosition oppentPos, SSPosition myPos, int radius)
        {
            int dx = oppentPos.PosX - myPos.PosX;
            int dy = oppentPos.PosY - myPos.PosY;

            if (dx * dx + dy * dy < radius * radius)
                return true;
            else
                return false;
        }

        /// <summary>
        /// diff ← ベクトル p - q
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static Vector2 Vector2DiffHelper(Vector2 p, Vector2 q)
        {
            Vector2 diff = new Vector2();
            diff.x = p.x - q.x;
            diff.y = p.y - q.y;
            return diff;
        }

        /// <summary>
        /// ベクトル p と q の内積
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static double Vector2InnerProductHelper(Vector2 p, Vector2 q)
        {
            return p.x * q.x + p.y * q.y;
        }

        /// <summary>
        /// ベクトル p と q の外積
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static double Vector2OuterProductHelper(Vector2 p, Vector2 q)
        {
            return p.x * q.y - p.y * q.x;
        }

        /// <summary>
        /// 角度θを返す
        /// </summary>
        /// <param name="squarePosition"></param>
        /// <param name="circlePosition"></param>
        /// <returns></returns>
        private static double GetThetaHelper(SSPosition pos0, SSPosition pos1, SSPosition circlePosition)
        {

            /* ベクトル C→P と C→Q のなす角θおよび回転方向を求める．*/
            Vector2 c, p, q; /* 入力データ */
            Vector2 cp;      /* ベクトル C→P */
            Vector2 cq;      /* ベクトル C→Q */
            double s;          /* 外積：(C→P) × (C→Q) */
            double t;          /* 内積：(C→P) ・ (C→Q) */
            double theta;      /* θ (ラジアン) */

            c = p = q = new Vector2();

            /* c，p，q を所望の値に設定する．*/
            c.x = pos0.PosX;
            c.y = pos0.PosY;
            p.x = pos1.PosX;
            p.y = pos1.PosY;
            q.x = circlePosition.PosX;
            q.y = circlePosition.PosY;

            /* 回転方向および角度θを計算する．*/
            cp = Vector2DiffHelper(p, c);          /* cp ← p - c   */
            cq = Vector2DiffHelper(q, c);          /* cq ← q - c   */
            s = Vector2OuterProductHelper(cp, cq); /* s ← cp × cq */
            t = Vector2InnerProductHelper(cp, cq); /* t ← cp ・ cq */
            theta = Math.Atan2(s, t);

            return theta;
        }

        /// <summary>
        /// 点と線分との距離を求める
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static double GetDistanceHelper(double x, double y, double x1, double y1, double x2, double y2)
        {
            double dx, dy, a, b, t, tx, ty;
            double distance;
            dx = (x2 - x1); dy = (y2 - y1);
            a = dx * dx + dy * dy;
            b = dx * (x1 - x) + dy * (y1 - y);
            t = -b / a;
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            tx = x1 + dx * t;
            ty = y1 + dy * t;
            distance = Math.Sqrt((x - tx) * (x - tx) + (y - ty) * (y - ty));
            return distance;
        }

        /// <summary>
        /// 円形あたり判定と長方形あたり判定を持つオブジェクトどうしのあたり判定を行う
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="square"></param>
        /// <returns></returns>
        public static bool IsInteractCircleSquare(IInteracter circle, IInteracter square)
        {
            //円の中に長方形の４点のうちどれかがあるかどうか判定
            for (int i = 0; i < 4; i++)
            {
                if (PointCircleInteractHelper(square.GetSquarePosition().SquarePosition[i], circle.GetPosition(), circle.GetRadius()))
                {
                    return true;
                }
            }

            //ここまで


            //長方形の中に物体が入り込んでいるかどうかを判定判定
            double theta = GetThetaHelper(square.GetSquarePosition().SquarePosition[0], square.GetSquarePosition().SquarePosition[1], circle.GetPosition());//3点の成す角1
            double theta2 = GetThetaHelper(square.GetSquarePosition().SquarePosition[2], square.GetSquarePosition().SquarePosition[3], circle.GetPosition());//3点の成す角2

            if (0 <= theta && theta <= Math.PI / 2 && 0 <= theta2 && theta2 <= Math.PI / 2)
                return true;

            //ここまで


            //線分と点との距離を求める

            for (int i = 0; i < 4; i++)
            {
                double d = GetDistanceHelper(
                    circle.GetPosition().PosX,
                    circle.GetPosition().PosY,
                    square.GetSquarePosition().SquarePosition[i].PosX,
                    square.GetSquarePosition().SquarePosition[i].PosY,
                    square.GetSquarePosition().SquarePosition[(i + 1) % 4].PosX,
                    square.GetSquarePosition().SquarePosition[(i + 1) % 4].PosY);

                if (d < circle.GetRadius())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 短形Cと傾いた短形Bについて傾きのない近似短形D及びAをつくり衝突判定を行う
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool IsInteractSquareSquare(IInteracter obj1, IInteracter obj2)
        {
            /*

            //短形Cの幅
            int w2 = Math.Abs(obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().LeftTop].PosX - obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().RightTop].PosX);
            
            //短形Cの高さ
            int h2 = Math.Abs(obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().LeftTop].PosY - obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().LeftBottom].PosY);

            //短形Cの中心座標X
            int cx2 = obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().LeftTop].PosX + (w2 / 2);

            //短形Cの中心座標Y
            int cy2 = obj1.GetSquarePosition().SquarePosition[obj1.GetSquarePosition().LeftTop].PosY + (h2 / 2);

            */

            throw new NotImplementedException();

            //TODO:回転短形同士のあたり判定は実装していません

        }

        public static bool IsInteractCircleCircle(IInteracter obj1, IInteracter obj2)
        {
            //円と円のあたり判定

            int x0 = obj2.GetPosition().PosX;
            int x1 = obj1.GetPosition().PosX;

            int y0 = obj2.GetPosition().PosY;
            int y1 = obj1.GetPosition().PosY;

            int d = obj1.GetRadius() + obj2.GetRadius();

            return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < d * d;
        }
    }
}
