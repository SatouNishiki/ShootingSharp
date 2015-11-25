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
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(30, 30);
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

        /*
        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(30, 30);
        }
        */

        public override void Move()
        {
            if (this.MovingType == 0)
            {
                if (moveCount < 50)
                {
                    this.position.PosY += this.MoveSpeed;
                    this.MoveType = MoveTypeEnum.Down;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX -= this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.LeftDown;
                }

            }
            else if (this.MovingType == 1)
            {
                if (moveCount < 50)
                {
                    this.position.PosY += this.MoveSpeed;
                    this.MoveType = MoveTypeEnum.Down;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX += this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.RightDown;
                }
            }
            else if(this.MovingType == 2)
            {
                if (moveCount < 50)
                {
                    this.position.PosX += this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.RightDown;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX -= this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.LeftDown;
                }
            }
            else
            {
                if (moveCount < 50)
                {
                    this.position.PosX -= this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.LeftDown;
                }
                else if (moveCount > 60)
                {
                    this.position.PosX += this.MoveSpeed - 1;
                    this.position.PosY += this.MoveSpeed - 1;
                    this.MoveType = MoveTypeEnum.RightDown;
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
                  //  Shot s = new RedFairyShot(this);
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Build();
                    this.Scene.AddShot(s);

                }
            }
            else if (this.ActionType == 1)
            {
                if (this.moveCount % shotCount == 0)
                {
                //    Shot s = new RedFairyShot(this, 70.0D);
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Theta(70.0D).Build();
                    this.Scene.AddShot(s);

                }
            }
            else if (this.ActionType == 2)
            {
                if (this.moveCount % shotCount == 0)
                {
                 //   Shot s = new RedFairyShot(this, 110.0D);
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Theta(110.0D).Build();
                    this.Scene.AddShot(s);

                }
            }
            else if (this.ActionType == 3)
            {
                if (this.moveCount % shotCount == 0)
                {
              //      Shot s = new RedFairyShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Target(SSTaskFactory.PlayerUpdateTask.Player.GetPosition()).Build();
                    this.Scene.AddShot(s);
                }
            }
            else if(this.ActionType == 4)
            {
                if (this.moveCount % shotCount == 0)
                {
                //    Shot s = new RedFairyShot(this, 110.0D);
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Theta(110.0D).Build();
                    s.SetMoveSpeed(2);
                    this.Scene.AddShot(s);

                }
            }
            else if (this.ActionType == 5)
            {
                if (this.moveCount % shotCount == 0)
                {
                //    Shot s = new RedFairyShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                    Shot s = new Shot.Builder(typeof(RedFairyShot)).Position(this.position).Target(SSTaskFactory.PlayerUpdateTask.Player.GetPosition()).Build();
                    s.SetMoveSpeed(2);
                    this.Scene.AddShot(s);

                }
            }

        }

        public override int GetRadius()
        {
            return 16;
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

        protected override item.Item GetDropItem()
        {
            Random rnd = new Random();
            int r = rnd.Next(100);

            if (r < 10)
            {
                return new item.ItemBigPower();
            }
            else
            {
                return new item.ItemSmallPower();
            }
        }
    }
}
