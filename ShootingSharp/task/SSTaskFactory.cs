using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.task
{
    public class SSTaskFactory
    {
        public static BackGroundImageTask BackGroundImageTask = TaskCreator<BackGroundImageTask>.Create();

        public static BossActionTask BossActionTask = TaskCreator<BossActionTask>.Create();

        public static BossDrawTask BossDrawTask = TaskCreator<BossDrawTask>.Create();

        public static BossMoveTask BossMoveTask = TaskCreator<BossMoveTask>.Create();

        public static BossUpdateTask BossUpdateTask = TaskCreator<BossUpdateTask>.Create();

        public static BossPopTask BossPopTask = TaskCreator<BossPopTask>.Create();

        public static PlayerUpdateTask PlayerUpdateTask = TaskCreator<PlayerUpdateTask>.Create();

        public static PlayerDrawTask PlayerDrawTask = TaskCreator<PlayerDrawTask>.Create();

        public static PlayerMoveTask PlayerMoveTask = TaskCreator<PlayerMoveTask>.Create();

        public static PlayerActionTask PlayerActionTask = TaskCreator<PlayerActionTask>.Create();

        public static ShotMoveTask ShotMoveTask = TaskCreator<ShotMoveTask>.Create();

        public static ShotDrawTask ShotDrawTask = TaskCreator<ShotDrawTask>.Create();

        public static ShotUpdateTask ShotUpdateTask = TaskCreator<ShotUpdateTask>.Create();

        public static EnemyActionTask EnemyActionTask = TaskCreator<EnemyActionTask>.Create();

        public static EnemyDrawTask EnemyDrawTask = TaskCreator<EnemyDrawTask>.Create();

        public static EnemyMoveTask EnemyMoveTask = TaskCreator<EnemyMoveTask>.Create();

        public static EnemyUpdateTask EnemyUpdateTask = TaskCreator<EnemyUpdateTask>.Create();

        public static EnemyPopTask EnemyPopTask = TaskCreator<EnemyPopTask>.Create();

        public static MoveTaskManager MoveTask = TaskCreator<MoveTaskManager>.Create();

        public static DrawTaskManager DrawTask = TaskCreator<DrawTaskManager>.Create();

        public static UpdateTaskManager UpdateTask = TaskCreator<UpdateTaskManager>.Create();

        public static ActionTaskManager ActionTask = TaskCreator<ActionTaskManager>.Create();

        public static InfoDrawTask InfoDrawTask = TaskCreator<InfoDrawTask>.Create();

        /// <summary>
        /// 全てのタスクを初期化します
        /// </summary>
        public static void Init()
        {
            BackGroundImageTask = TaskCreator<BackGroundImageTask>.Create();

            BossActionTask = TaskCreator<BossActionTask>.Create();

            BossDrawTask = TaskCreator<BossDrawTask>.Create();

            BossMoveTask = TaskCreator<BossMoveTask>.Create();

            BossUpdateTask = TaskCreator<BossUpdateTask>.Create();

            BossPopTask = TaskCreator<BossPopTask>.Create();

            PlayerUpdateTask = TaskCreator<PlayerUpdateTask>.Create();

            PlayerDrawTask = TaskCreator<PlayerDrawTask>.Create();

            PlayerMoveTask = TaskCreator<PlayerMoveTask>.Create();

            PlayerActionTask = TaskCreator<PlayerActionTask>.Create();

            ShotMoveTask = TaskCreator<ShotMoveTask>.Create();

            ShotDrawTask = TaskCreator<ShotDrawTask>.Create();

            ShotUpdateTask = TaskCreator<ShotUpdateTask>.Create();

            EnemyActionTask = TaskCreator<EnemyActionTask>.Create();

            EnemyDrawTask = TaskCreator<EnemyDrawTask>.Create();

            EnemyMoveTask = TaskCreator<EnemyMoveTask>.Create();

            EnemyUpdateTask = TaskCreator<EnemyUpdateTask>.Create();

            EnemyPopTask = TaskCreator<EnemyPopTask>.Create();

            MoveTask = TaskCreator<MoveTaskManager>.Create();

            DrawTask = TaskCreator<DrawTaskManager>.Create();

            UpdateTask = TaskCreator<UpdateTaskManager>.Create();

            ActionTask = TaskCreator<ActionTaskManager>.Create();

            InfoDrawTask = TaskCreator<InfoDrawTask>.Create();
        }

    }
}
