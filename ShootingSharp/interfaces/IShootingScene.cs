using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity;
using ShootingSharp.entity.enemy;

namespace ShootingSharp.interfaces
{
    public interface IShootingScene : IScene
    {
        /// <summary>
        /// プレイヤーの追加
        /// </summary>
        /// <param name="player"></param>
        void AddPlayer(EntityPlayer player);

        /// <summary>
        /// 敵の追加
        /// </summary>
        /// <param name="enemy"></param>
        void AddEnemy(Enemy enemy);

        string GetName();
    }
}
