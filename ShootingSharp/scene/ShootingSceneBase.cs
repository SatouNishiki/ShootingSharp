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
using System.Drawing;
using System.Diagnostics; 

namespace ShootingSharp.scene
{
    public abstract class ShootingSceneBase : IShootingScene
    {
      

        protected UpdateTaskManager updateTask = SSTaskFactory.UpdateTask;
        protected DrawTaskManager drawTask = SSTaskFactory.DrawTask;
        protected MoveTaskManager moveTask = SSTaskFactory.MoveTask;
        protected ActionTaskManager actionTask = SSTaskFactory.ActionTask;
        protected EnemyPopTask popTask = SSTaskFactory.EnemyPopTask;
        protected BackGroundImageTask backimageTask = SSTaskFactory.BackGroundImageTask;
        protected InfoDrawTask infoDrawTask = SSTaskFactory.InfoDrawTask;
        protected BossPopTask bossPopTask = SSTaskFactory.BossPopTask;
        protected CollitionTask collitionTask = SSTaskFactory.CollitionTask;


        protected string soundName = string.Empty;

        private bool flag = true;

        protected TextureLoader loader = TextureLoader.GetInstance();

        protected ResultSceneBase.ResultType Type;

        protected Size windowSize = new Size(SSGame.GetInstance().GetWindowSize().Width, SSGame.GetInstance().GetWindowSize().Height);

        public ShootingSceneBase()
        {
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
            this.moveTask.Run();
            this.updateTask.Run();
            this.actionTask.Run();
            this.collitionTask.Run();
            this.drawTask.Run();
            this.infoDrawTask.Run();
            

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
            if (SSTaskFactory.EnemyUpdateTask.EnemyList.Count == 0 && SSTaskFactory.EnemyPopTask.GetRemainEnemys() == 0 && SSTaskFactory.BossPopTask.BossList.Count == 0)
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
            SoundLoader.GetInstance().StopAllSound();
            movie.MovieManager.GetInstance().StopMovie();

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
        

        public void AddPlayer(EntityPlayer player)
        {
            SSTaskFactory.PlayerUpdateTask.Player = player;
            SSTaskFactory.PlayerDrawTask.Player = player;
            SSTaskFactory.PlayerMoveTask.Player = player;
            SSTaskFactory.PlayerActionTask.Player = player;
            SSTaskFactory.InfoDrawTask.Player = player;
            SSTaskFactory.CollitionTask.AddInteractors(player);
            player.Scene = this;
        }


        public void AddEnemy(entity.enemy.Enemy enemy)
        {
            enemy.Scene = this;
            SSTaskFactory.EnemyPopTask.AddEnemyList(enemy);
        }

        public void PopEnemy(entity.enemy.Enemy enemy)
        {
            SSTaskFactory.EnemyActionTask.EnemyList.Add(enemy);
            SSTaskFactory.EnemyDrawTask.EnemyList.Add(enemy);
            SSTaskFactory.EnemyMoveTask.EnemyList.Add(enemy);
            SSTaskFactory.EnemyUpdateTask.EnemyList.Add(enemy);
            SSTaskFactory.CollitionTask.AddInteractors(enemy);
        }


        public string GetName()
        {
            return this.GetType().FullName;
        }


        public void AddBoss(entity.boss.Boss boss)
        {
            boss.Scene = this;
            SSTaskFactory.BossPopTask.BossList.Add(boss);
        }

        public void PopBoss(entity.boss.Boss boss)
        {
            SSTaskFactory.BossActionTask.BossList.Add(boss);
            SSTaskFactory.BossDrawTask.BossList.Add(boss);
            SSTaskFactory.BossMoveTask.BossList.Add(boss);
            SSTaskFactory.CollitionTask.AddInteractors(boss);
        }

        public void AddBom(entity.bom.Bom bom)
        {
            bom.Scene = this;
            SSTaskFactory.BomUpdateTask.BomList.Add(bom);
            SSTaskFactory.BomDrawTask.BomList.Add(bom);
            SSTaskFactory.CollitionTask.AddInteractors(bom);
        }

        public void AddShot(Shot shot)
        {
            shot.Scene = this;
            SSTaskFactory.ShotMoveTask.AddShot(shot);
            SSTaskFactory.ShotDrawTask.AddShot(shot);
            SSTaskFactory.ShotUpdateTask.AddShot(shot);
            SSTaskFactory.CollitionTask.AddInteractors(shot);
        }


        public void AddItem(entity.item.Item item)
        {
            item.Scene = this;
            SSTaskFactory.ItemDrawTask.ItemList.Add(item);
            SSTaskFactory.ItemMoveTask.ItemList.Add(item);
            SSTaskFactory.ItemUpdateTask.ItemList.Add(item);
            SSTaskFactory.CollitionTask.AddInteractors(item);
        }
    }
}
