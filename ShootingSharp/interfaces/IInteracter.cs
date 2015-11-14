using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;

namespace ShootingSharp.interfaces
{
    public enum SharpType
    {
        Circle, Square
    }

    public interface IInteracter : IHasSSPosition
    {
        /// <summary>
        /// 衝突判定管理クラス
        /// </summary>
        IInteractManager InteractManager { get; set; }

        /// <summary>
        /// 引数の相手とぶつかってるかどうか
        /// </summary>
        /// <param name="obj">相手</param>
        /// <returns></returns>
        bool IsInteract(IInteracter obj);

        /// <summary>
        /// 当たったときの処理
        /// </summary>
        void OnInteract(entity.Entity entity);

        /// <summary>
        /// 円の半径を返す
        /// </summary>
        /// <returns></returns>
        int GetRadius();

        /// <summary>
        /// 長方形の四隅を返す
        /// </summary>
        /// <returns></returns>
        SquareSSPositon GetSquarePosition();

        /// <summary>
        /// 形状タイプを返す
        /// </summary>
        /// <returns></returns>
        SharpType GetSharpType();

        /// <summary>
        /// フレンドコード(この文字列が同じなら味方)を取得する
        /// </summary>
        /// <returns></returns>
        string GetFriendCode();

        /// <summary>
        /// フレンドコードを設定
        /// </summary>
        void SetFriendCode(string code);
    }
}
