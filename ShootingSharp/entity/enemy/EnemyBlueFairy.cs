using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.task;

namespace ShootingSharp.entity.enemy
{
    public class EnemyBlueFairy : Enemy
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

        private int shotCount = 100;

        public EnemyBlueFairy()
            : base()
        {
            this.loader.LoadSprite("blue_fairy.png", 6, 2, 12, 49, 50);
            this.MoveSpeed = 3;
            this.textureSize = new System.Drawing.Size(30, 30);
        }

        public override string GetUpTextureName()
        {
            return "blue_fairy.png2";
        }

        public override string GetDownTextureName()
        {
            return "blue_fairy.png8";
        }

        public override string GetLeftTextureName()
        {
            return "blue_fairy.png7";
        }

        public override string GetCenterTextureName()
        {
            return "blue_fairy.png0";
        }

        public override string GetRightTextureName()
        {
            return "blue_fairy.png5";
        }

        public override string GetRightUpTextureName()
        {
            return "blue_fairy.png11";
        }

        public override string GetRightDownTextureName()
        {
            return "blue_fairy.png10";
        }

        public override string GetLeftUpTextureName()
        {
            return "blue_fairy.png0";
        }

        public override string GetLeftDownTextureName()
        {
            return "blue_fairy.png1";
        }



        public override void Move()
        {
            if (this.MovingType == 0)
            {
                this.position.PosX += this.MoveSpeed - 1;
                this.position.PosY += this.MoveSpeed - 1;
                this.MoveType = MoveTypeEnum.RightDown;

            }
            else if (this.MovingType == 1)
            {
                this.position.PosX -= this.MoveSpeed - 1;
                this.position.PosY += this.MoveSpeed - 1;
            }
           
                this.moveCount++;
        }

        public override void DoAction()
        {
            if (this.ActionType == 0)
            {
                if (this.shotCount > 100)
                {

                    if (this.position.PosX > SSTaskFactory.PlayerUpdateTask.Player.GetPosition().PosX - 10 &&
                        this.position.PosX < SSTaskFactory.PlayerUpdateTask.Player.GetPosition().PosX + 10 &&
                        this.position.PosY <= SSTaskFactory.PlayerUpdateTask.Player.GetPosition().PosY)
                    {
                       // Shot s3 = new Shot.Builder(typeof(BlueFairyShot)).Position(this.position).Theta(60).Build();
                        Shot s3 = new DirectionShotBuilder<BlueFairyShot>(this, 0, 60).CreateShot();
                        this.Scene.AddShot(s3);

                        Shot s4 = new DirectionShotBuilder<BlueFairyShot>(this, 0, 90).CreateShot();
                        this.Scene.AddShot(s4);

                        Shot s2 = new DirectionShotBuilder<BlueFairyShot>(this, 0, 75).CreateShot();
                        this.Scene.AddShot(s2);

                        this.shotCount = 0;
                    }

                }

                this.shotCount++;
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
            return new item.ItemBigPower();

        }
    }
    
}
