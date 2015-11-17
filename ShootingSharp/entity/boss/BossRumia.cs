using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShootingSharp.entity.shot;
using ShootingSharp.task;

namespace ShootingSharp.entity.boss
{
    public class BossRumia : Boss
    {
        private bool moveDirection;
        private Size windowSize;
        private int actionCount;

        public BossRumia() : base()
        {
            this.windowSize = SSGame.GetInstance().GetBattleWindowSize();
            this.Life = 100;
            this.MaxLife = Life;
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

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(300, 300);
        }

        public override void Move()
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

        public override void DoAction()
        {
            if (actionCount % 20 == 0)
            {
                Shot s = new BlackCircleShot(this, SSTaskFactory.PlayerUpdateTask.Player.GetPosition());
                s.SetFriendCode(this.friendCode);
                this.InteractManager.AddInteractObject(s);
                SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                SSTaskFactory.ShotUpdateTask.ShotList.Add(s);
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
    }
}
