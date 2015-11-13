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
        public static PlayerUpdateTask PlayerUpdateTask = TaskCreator<PlayerUpdateTask>.Create();

        public static PlayerDrawTask PlayerDrawTask = TaskCreator<PlayerDrawTask>.Create();

        public static PlayerMoveTask PlayerMoveTask = TaskCreator<PlayerMoveTask>.Create();

        public static ShotMoveTask ShotMoveTask = TaskCreator<ShotMoveTask>.Create();

        public static ShotDrawTask ShotDrawTask = TaskCreator<ShotDrawTask>.Create();

        public static ShotUpdateTask ShotUpdateTask = TaskCreator<ShotUpdateTask>.Create();

        public static PlayerActionTask PlayerActionTask = TaskCreator<PlayerActionTask>.Create();

        public static MoveTaskManager MoveTask = TaskCreator<MoveTaskManager>.Create();

        public static DrawTaskManager DrawTask = TaskCreator<DrawTaskManager>.Create();

        public static UpdateTaskManager UpdateTask = TaskCreator<UpdateTaskManager>.Create();

        public static ActionTaskManager ActionTask = TaskCreator<ActionTaskManager>.Create();

    }
}
