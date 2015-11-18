using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using ShootingSharp.entity;
using ShootingSharp.task;
using ShootingSharp.sound;
using DxLibDLL;
using ShootingSharp.core;
using ShootingSharp.texture;
using ShootingSharp.entity.shot;
using ShootingSharp.entity.player;

namespace ShootingSharp.scene
{
    public abstract class ShootingSceneBase : IShootingScene, IInteractManager
    {
        
        /// <summary>
        /// あたり判定を行うオブジェクトを全て格納するリスト
        /// </summary>
        protected List<Entity> ohtherInteracters;

        protected List<Shot> shotInteractors;

        protected UpdateTaskManager updateTask = SSTaskFactory.UpdateTask;
        protected DrawTaskManager drawTask = SSTaskFactory.DrawTask;
        protected MoveTaskManager moveTask = SSTaskFactory.MoveTask;
        protected ActionTaskManager actionTask = SSTaskFactory.ActionTask;
        protected EnemyPopTask popTask = SSTaskFactory.EnemyPopTask;
        protected BackGroundImageTask backimageTask = SSTaskFactory.BackGroundImageTask;
        protected InfoDrawTask infoDrawTask = SSTaskFactory.InfoDrawTask;
        protected BossPopTask bossPopTask = SSTaskFactory.BossPopTask;

        private List<Entity> temp = new List<Entity>();

        protected string soundName = string.Empty;

        private bool flag = true;

        protected TextureLoader loader = TextureLoader.GetInstance();

        protected ResultSceneBase.ResultType Type;

        public ShootingSceneBase()
        {
            this.ohtherInteracters = new List<Entity>();
            this.shotInteractors = new List<Shot>();
            this.Type = ResultSceneBase.ResultType.Clear;

        }

        public bool Run()
        {
            if (flag)
            {
                if (soundName != string.Empty)
                    SoundLoader.GetInstance().PlayLoopSound(soundName);

                flag = false;
            }

            this.backimageTask.Run();
            this.popTask.Run();
            this.bossPopTask.Run();
            this.actionTask.Run();
            this.updateTask.Run();
            this.moveTask.Run();
            this.drawTask.Run();
            this.infoDrawTask.Run();


            foreach (var item in this.ohtherInteracters)
            {
                if (!item.IsLiving())
                {
                    temp.Add(item);
                }
            }

            ohtherInteracters.RemoveAll(e => temp.IndexOf(e) >= 0);
            temp.Clear();

            if (this.IsFinished())
            {
                this.OnFinished();
            }

            return !this.IsFinished();
        }

        public bool ExistNextScene()
        {
            return true;
        }

        public virtual bool IsFinished()
        {
            if (SSTaskFactory.EnemyUpdateTask.EnemyList.Count == 0 && SSTaskFactory.EnemyPopTask.EnemyList.Count == 0 && SSTaskFactory.BossPopTask.BossList.Count == 0)
            {
                this.Type = ResultSceneBase.ResultType.Clear;
                return true;
            }
            else if(!SSTaskFactory.PlayerUpdateTask.Player.IsLiving())
            {
                this.Type = ResultSceneBase.ResultType.Lose;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual void OnFinished()
        {
            if (soundName != string.Empty)
                SoundLoader.GetInstance().StopSount(soundName);

            if (this.Type == ResultSceneBase.ResultType.Clear)
            {
                DX.WaitTimer(200);

                Effecter.TypeWrite(100, 100, 80, "C.png", "L.png", "E.png", "A.png", "R.png", "!.png", "!.png");
            }
            else
            {
                DX.WaitTimer(200);

                Effecter.TypeWrite(100, 100, 80, "L.png", "O.png", "S.png", "E.png", "dot.png", "dot.png", "dot.png");
            }
            DX.DrawStringFToHandle(200, 300, "なにかキーを押してください", DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 13));
            DX.ScreenFlip();
            DX.WaitKey();
        }

        public IScene GetNextScene()
        {
            return this.GetResultScene();
        }

        public virtual ResultSceneBase GetResultScene()
        {
            return new ResultSceneBase(this.Type);
        }

        /// <summary>
        /// あたり判定を有するオブジェクトとして登録します
        /// </summary>
        /// <param name="interact"></param>
        public void AddInteractObject(Entity interact)
        {
            if (interact is Shot)
            {
                this.shotInteractors.Add((Shot)interact);
            }
            else
            {
                this.ohtherInteracters.Add(interact);
            }
            interact.InteractManager = this;
        }


        public Entity GetInteractObject(Entity interact)
        {
            if (!interact.IsLiving())
                return null;

            Entity interactor = null;

            if (interact is Shot)
            {
                
                //引数のオブジェクトのあたり判定チェックに全オブジェクトをチェックさせる
                foreach (var i in ohtherInteracters)
                {
                    if (i.IsLiving() && interact.IsInteract(i))
                    {
                        interactor = i;
                    }
                }
            }
            else
            {
                //引数のオブジェクトのあたり判定チェックに全オブジェクトをチェックさせる
                foreach (var i in shotInteractors)
                {
                    if (i.IsLiving() && interact.IsInteract(i))
                    {
                        interactor = i;
                    }
                }
            }

            return interactor;
        }

        

        public void AddPlayer(EntityPlayer player)
        {
            SSTaskFactory.PlayerUpdateTask.Player = player;
            SSTaskFactory.PlayerDrawTask.Player = player;
            SSTaskFactory.PlayerMoveTask.Player = player;
            SSTaskFactory.PlayerActionTask.Player = player;
            SSTaskFactory.InfoDrawTask.Player = player;
            player.InteractManager = this;
        }


        public void AddEnemy(entity.enemy.Enemy enemy)
        {
            enemy.InteractManager = this;
            SSTaskFactory.EnemyPopTask.EnemyList.Add(enemy);
        }


        public string GetName()
        {
            return this.GetType().FullName;
        }


        public void AddBoss(entity.boss.Boss boss)
        {

            boss.InteractManager = this;
            SSTaskFactory.BossPopTask.BossList.Add(boss);
        }
    }
}
