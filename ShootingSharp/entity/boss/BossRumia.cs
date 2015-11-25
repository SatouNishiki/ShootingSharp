using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShootingSharp.entity.shot;
using ShootingSharp.task;
using ShootingSharp.position;

namespace ShootingSharp.entity.boss
{
    public class BossRumia : Boss
    {
        private bool moveDirection;
        private Size windowSize;
        private int actionCount;
        private bool canMove;

        public BossRumia() : base()
        {
            this.windowSize = SSGame.GetInstance().GetBattleWindowSize();
            this.Life = 1000;
            this.MaxLife = Life;
            this.canMove = true;
            this.textureSize = new System.Drawing.Size(150, 150);
        }

        public override string GetUpTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetDownTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetLeftTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetCenterTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetRightTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetRightUpTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetRightDownTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetLeftUpTextureName()
        {
            return "boss_rumia.png";
        }

        public override string GetLeftDownTextureName()
        {
            return "boss_rumia.png";
        }

        public override void SetMoveType()
        {
            this.MoveType = MoveTypeEnum.Center;
        }

        public override void OnDeath()
        {
            //なにもしない
        }
        /*
        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(150, 150);
        }
        */
        public override void Move()
        {
            if (this.canMove)
            {

                if (!this.moveDirection)
                {
                    this.position.PosX--;
                }
                else
                {
                    this.position.PosX++;
                }

                if (this.position.PosX == 50)
                {
                    this.moveDirection = true;
                }

                if (this.position.PosX == this.windowSize.Width - 50)
                {
                    this.moveDirection = false;
                }
            }
        }

        public override void DoAction()
        {
            if (actionCount % 200 >= 0 && this.actionCount % 200 <= 100)
            {
                this.canMove = false;

                if ((this.actionCount % 100) % 13 == 0)
                {
                    for (int i = 30; i < 151; i++)
                    {
                        if (i % 10 == 0)
                        {
                         //   Shot s = new BlackLongShot(this, (double)i);
                            Shot s = new Shot.Builder(typeof(BlackLongShot)).Position(this.position).Theta((double)i).Build();
                            this.Scene.AddShot(s);
                        }
                    }
                }

                
            }

            if (!this.canMove && this.actionCount % 200 > 100)
            {
                this.canMove = true;
            }


            if (actionCount % 10 == 0)
            {
                //Shot s = new BlackCircleShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                Shot s = new Shot.Builder(typeof(BlackCircleShot)).Position(this.position).Target(SSTaskFactory.PlayerUpdateTask.Player.GetPosition()).Build();
                this.Scene.AddShot(s);
            }

            if (actionCount % 90 == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    //Shot s = new RGBStoneShot(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30), RGBStoneShot.ColorType.Red);
             //       Shot s = new Shot.Builder(typeof(RGBStoneShot)).Position(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30)). RGBStoneShot.ColorType.Red)
                    Shot s = new RGBStoneShot(new Shot.Builder(typeof(RGBStoneShot)).Position(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30)), RGBStoneShot.ColorType.Red);
                    this.Scene.AddShot(s);
                }
            }

            if (actionCount % 90 == 30)
            {
                for (int i = 0; i < 10; i++)
                {
                  //  Shot s = new RGBStoneShot(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30), RGBStoneShot.ColorType.Blue);
                    Shot s = new RGBStoneShot(new Shot.Builder(typeof(RGBStoneShot)).Position(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30)), RGBStoneShot.ColorType.Blue);
                    this.Scene.AddShot(s);
                }
            }

            if (actionCount % 90 == 60)
            {
                for (int i = 0; i < 10; i++)
                {
                //    Shot s = new RGBStoneShot(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30), RGBStoneShot.ColorType.Green);
                    Shot s = new RGBStoneShot(new Shot.Builder(typeof(RGBStoneShot)).Position(new SSPosition(this.position.PosX - 150 + i * 30, this.position.PosY + 30)), RGBStoneShot.ColorType.Green);
                    this.Scene.AddShot(s);
                }
            }

            this.actionCount++;
        }

        public override int GetRadius()
        {
            return 30;
        }

        public override string GetCutinTextureName()
        {
            return "rumia_cutin.png";
        }

        public override string GetName()
        {
            return "ルーミア";
        }


        public override string GetMusicName()
        {
            return "boss1.mp3";
        }
    }
}
