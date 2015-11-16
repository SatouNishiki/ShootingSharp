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

namespace ShootingSharp.scene
{
    public abstract class ShootingSceneBase : IShootingScene, IInteractManager
    {
        
        /// <summary>
        /// あたり判定を行うオブジェクトを全て格納するリスト
        /// </summary>
        protected List<Entity> interacters;

        protected UpdateTaskManager updateTask = SSTaskFactory.UpdateTask;
        protected DrawTaskManager drawTask = SSTaskFactory.DrawTask;
        protected MoveTaskManager moveTask = SSTaskFactory.MoveTask;
        protected ActionTaskManager actionTask = SSTaskFactory.ActionTask;
        protected EnemyPopTask popTask = SSTaskFactory.EnemyPopTask;
        protected BackGroundImageTask backimageTask = SSTaskFactory.BackGroundImageTask;
        protected InfoDrawTask infoDrawTask = SSTaskFactory.InfoDrawTask;

        private List<Entity> temp = new List<Entity>();

        protected string soundName = string.Empty;

        private bool flag = true;

        protected TextureLoader loader = TextureLoader.GetInstance();

        protected ResultSceneBase.ResultType Type;

        public ShootingSceneBase()
        {
            this.interacters = new List<Entity>();
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
            this.actionTask.Run();
            this.updateTask.Run();
            this.moveTask.Run();
            this.drawTask.Run();
            this.infoDrawTask.Run();

            foreach (var item in this.interacters)
            {
                if (!item.IsLiving())
                {
                    temp.Add(item);
                }
            }

            interacters.RemoveAll(e => temp.IndexOf(e) >= 0);
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
            if (SSTaskFactory.EnemyUpdateTask.EnemyList.Count == 0 && SSTaskFactory.EnemyPopTask.EnemyList.Count == 0)
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
                string outStr = "CLEAR!!";

                for (int i = 0; i < outStr.Length; i++)
                {
                    DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["click.mp3"], DX.DX_PLAYTYPE_BACK);

                    switch (i)
                    {
                        case (0):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["C.png"], DX.TRUE);
                            break;

                        case (1):
                            DX.DrawGraph(100 + i * 90, 100, loader.Textures["L.png"], DX.TRUE);
                            break;

                        case (2):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["E.png"], DX.TRUE);
                            break;

                        case (3):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["A.png"], DX.TRUE);
                            break;

                        case (4):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["R.png"], DX.TRUE);
                            break;

                        case (5):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["!.png"], DX.TRUE);
                            break;

                        case (6):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["!.png"], DX.TRUE);
                            break;
                    }

                    DX.ScreenFlip();
                    DX.WaitTimer(150);
                }

            }
            else
            {
                DX.WaitTimer(200);
                string outStr = "LOSE...";

                for (int i = 0; i < outStr.Length; i++)
                {
                    DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["click.mp3"], DX.DX_PLAYTYPE_BACK);

                    switch (i)
                    {
                        case (0):
                            DX.DrawGraph(100 + i * 90, 100, loader.Textures["L.png"], DX.TRUE);
                            break;

                        case (1):
                            DX.DrawGraph(100 + i * 90, 100, loader.Textures["O.png"], DX.TRUE);
                            break;

                        case (2):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["S.png"], DX.TRUE);
                            break;

                        case (3):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["E.png"], DX.TRUE);
                            break;

                        case (4):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["dot.png"], DX.TRUE);
                            break;

                        case (5):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["dot.png"], DX.TRUE);
                            break;

                        case (6):
                            DX.DrawGraph(100 + i * 80, 100, loader.Textures["dot.png"], DX.TRUE);
                            break;
                    }

                    DX.ScreenFlip();
                    DX.WaitTimer(150);
                }
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
            this.interacters.Add(interact);
            interact.InteractManager = this;
        }

        public Entity GetInteractObject(Entity interact)
        {
            Entity interactor = null;

            //引数のオブジェクトのあたり判定チェックに全オブジェクトをチェックさせる
            foreach (var i in interacters)
            {
                //自分自身だった場合は飛ばす
                if (i == interact)
                    continue;

                if (interact.IsInteract(i))
                {
                    interactor = i;
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
        }


        public void AddEnemy(entity.enemy.Enemy enemy)
        {
            SSTaskFactory.EnemyPopTask.EnemyList.Add(enemy);
        }


        public string GetName()
        {
            return this.GetType().FullName;
        }
    }
}
