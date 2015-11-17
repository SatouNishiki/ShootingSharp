using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.task;

namespace ShootingSharp.entity.enemy
{
    public class EnemyRedFairy : Enemy
    {
        private int moveCount;

        /// <summary>
        /// 移動タイプ
        /// </summary>
        public int MovingType { get; set; }

        /// <summary>
        /// アクションタイプ
        /// </summary>
        public int ActionType { get; set; }

        private const int shotCount = 10;

        public EnemyRedFairy()
            : base()
        {
            this.loader.LoadSprite("fairy.png", 8, 8, 64, 32, 32);
            this.moveSpeed = 3;
        }

        public override string GetUpTextureName()
        {
            return "fairy.png2";
        }

        public override string GetDownTextureName()
        {
            return "fairy.png1";
        }

        public override string GetLeftTextureName()
        {
            return "fairy.png10";
        }

        public override string GetCenterTextureName()
        {
            return "fairy.png0";
        }

        public override string GetRightTextureName()
        {
            return "fairy.png15";
        }

        public override string GetRightUpTextureName()
        {
            return "fairy.png12";
        }

        public override string GetRightDownTextureName()
        {
            return "fairy.png13";
        }

        public override string GetLeftUpTextureName()
        {
            return "fairy.png9";
        }

        public override string GetLeftDownTextureName()
        {
            return "fairy.png8";
        }

        public override void SetMoveType()
        {
            
        }

        
        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(50, 50);
        }


        public override void Move()
        {
            if (this.MovingType == 0)
            {
                if (moveCount < 50)
                {
                    this.position.PosY += this.moveSpeed;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX -= this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }

            }
            else if (this.MovingType == 1)
            {
                if (moveCount < 50)
                {
                    this.position.PosY += this.moveSpeed;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX += this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }
            }
            else if(this.MovingType == 2)
            {
                if (moveCount < 50)
                {
                    this.position.PosX += this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX -= this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }
            }
            else
            {
                if (moveCount < 50)
                {
                    this.position.PosX -= this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX += this.moveSpeed - 1;
                    this.position.PosY += this.moveSpeed - 1;
                }
            }
                this.moveCount++;
        }

        public override void DoAction()
        {
            if (this.ActionType == 0)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this);
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }
            else if (this.ActionType == 1)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this, 70.0D);
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }
            else if (this.ActionType == 2)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this, 110.0D);
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }
            else if (this.ActionType == 3)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }
            else if(this.ActionType == 4)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this, 110.0D);
                    s.SetMoveSpeed(2);
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }
            else if (this.ActionType == 5)
            {
                if (this.moveCount % shotCount == 0)
                {
                    Shot s = new RedFairyShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                    s.SetMoveSpeed(2);
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);

                }
            }

        }

        public override int GetRadius()
        {
            return 16;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public override void OnUpdate()
        {
            if (this.position.PosX > SSGame.GetInstance().GetBattleWindowSize().Width
                 || this.position.PosY > SSGame.GetInstance().GetBattleWindowSize().Height
                 || this.position.PosX < 0
                 || this.position.PosY < 0)
            {
                this.Life = 0;
                return;
            }

            base.OnUpdate();
        }
    }
}
