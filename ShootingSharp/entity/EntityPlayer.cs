using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;
using ShootingSharp.entity.shot;
using ShootingSharp.task;

namespace ShootingSharp.entity
{
    public class EntityPlayer : LivingAnimationEntity
    {
        private TextureLoader textureLoader;

        private int shotCount;

        protected int shotInterval;

        

        public EntityPlayer() : base()
        {
            this.moveSpeed = 6;
            this.shotInterval = 5;
            this.textureLoader = TextureLoader.GetInstance();
            this.logger.Debug(this.GetType().ToString() + " is create");
        }

        /// <summary>
        /// 移動係数を返す
        /// </summary>
        /// <returns></returns>
        private float GetMoveCoefficient()
        {
            float moveCoefficient;

            //ななめ移動なら
            if (this.MoveType == MoveTypeEnum.RightUp || this.MoveType == MoveTypeEnum.RightDown 
                || this.MoveType == MoveTypeEnum.LeftUp || this.MoveType == MoveTypeEnum.LeftDown)
            {
                moveCoefficient = 0.71f;
            }
            else
            {
                moveCoefficient = 1.0f;
            }

            return moveCoefficient;
        }

        public override string GetUpTextureName()
        {
            return "player.png";
        }

        public override string GetDownTextureName()
        {
            return "player.png";
        }

        public override string GetLeftTextureName()
        {
            return "player.png";
        }

        public override string GetCenterTextureName()
        {
            return "player.png";
        }

        public override string GetRightTextureName()
        {
            return "player.png";
        }

        public override string GetRightUpTextureName()
        {
            return "player.png";
        }

        public override string GetRightDownTextureName()
        {
            return "player.png";
        }

        public override string GetLeftUpTextureName()
        {
            return "player.png";
        }

        public override string GetLeftDownTextureName()
        {
            return "player.png";
        }

        public override void OnDeath()
        {
        //    throw new NotImplementedException();
        }
        

        public override void Move()
        {

            if (this.MoveType == MoveTypeEnum.Right)
            {
                if (this.CanRightMove())
                    this.position.PosX += (int)(this.moveSpeed * this.GetMoveCoefficient());
            }

            if (this.MoveType == MoveTypeEnum.Left)
            {
                if (this.CanLeftMove())
                    this.position.PosX -= (int)(this.moveSpeed * this.GetMoveCoefficient());
            }

            if (this.MoveType == MoveTypeEnum.Up)
            {
                if (this.CanUpMove())
                    this.position.PosY -= (int)(this.moveSpeed * this.GetMoveCoefficient());
            }

            if (this.MoveType == MoveTypeEnum.Down)
            {
                if (this.CanDownMove())
                    this.position.PosY += (int)(this.moveSpeed * this.GetMoveCoefficient());
            }

            if (this.MoveType == MoveTypeEnum.RightUp)
            {
                if (this.CanRightMove() && this.CanUpMove())
                {
                    this.position.PosX += (int)(this.moveSpeed * this.GetMoveCoefficient());
                    this.position.PosY -= (int)(this.moveSpeed * this.GetMoveCoefficient());
                }
            }

            if (this.MoveType == MoveTypeEnum.RightDown)
            {
                if (this.CanRightMove() && this.CanDownMove())
                {
                    this.position.PosX += (int)(this.moveSpeed * this.GetMoveCoefficient());
                    this.position.PosY += (int)(this.moveSpeed * this.GetMoveCoefficient());
                }
            }

            if (this.MoveType == MoveTypeEnum.LeftUp)
            {
                if (this.CanLeftMove() && this.CanUpMove())
                {
                    this.position.PosX -= (int)(this.moveSpeed * this.GetMoveCoefficient());
                    this.position.PosY -= (int)(this.moveSpeed * this.GetMoveCoefficient());
                }
            }

            if (this.MoveType == MoveTypeEnum.LeftDown)
            {
                if (this.CanLeftMove() && this.CanDownMove())
                {
                    this.position.PosX -= (int)(this.moveSpeed * this.GetMoveCoefficient());
                    this.position.PosY += (int)(this.moveSpeed * this.GetMoveCoefficient());
                }
            }
        }

        private bool CanRightMove()
        {
            return this.position.PosX < SSGame.GetInstance().GetWindowSize().Width;
        }

        private bool CanLeftMove()
        {
            return this.position.PosX > 0;
        }

        private bool CanUpMove()
        {
            return this.position.PosY > 0;
        }

        private bool CanDownMove()
        {
            return this.position.PosY < SSGame.GetInstance().GetWindowSize().Height;
        }

        public override void DoAction()
        {
            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
            {
                if (shotCount >= this.shotInterval)
                {
                    Shot s = new NormalShot(this);
                    s.InteractManager = this.InteractManager;
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);
                    this.shotCount = 0;
                }
            }
            if (this.shotCount < this.shotInterval)
                this.shotCount++;
        }

        public override int GetRadius()
        {
            return 10;
        }

        public override bool IsInteract(interfaces.IInteracter obj)
        {
        //    throw new NotImplementedException();
            return false;
        }

        public override void OnInteract()
        {
            throw new NotImplementedException();
        }


        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(50, 50);
        }

        public override position.SSPosition GetTexturePosition()
        {
            return this.position;
        }
         
        public override void Draw()
        {
            DX.DrawCircle(this.position.PosX, this.position.PosY,this.GetRadius() , DX.GetColor(255, 00, 0));
        }

        /// <summary>
        /// 移動タイプ決めるよ
        /// </summary>
        public override void SetMoveType()
        {
            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                {
                    this.MoveType = MoveTypeEnum.LeftUp;
                }
                else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                {
                    this.MoveType = MoveTypeEnum.LeftDown;
                }
                else
                {
                    this.MoveType = MoveTypeEnum.Left;
                }
            }
            else
            {

                if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
                {
                    if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                    {
                        this.MoveType = MoveTypeEnum.RightUp;
                    }
                    else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                    {
                        this.MoveType = MoveTypeEnum.RightDown;
                    }
                    else
                    {
                        this.MoveType = MoveTypeEnum.Right;
                    }
                }
                else
                {

                    if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                    {
                        if (DX.CheckHitKey(DX.KEY_INPUT_A) != 1)
                        {
                            if (DX.CheckHitKey(DX.KEY_INPUT_S) != 1)
                            {
                                this.MoveType = MoveTypeEnum.Up;
                            }
                        }
                    }
                    else
                    {

                        if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                        {
                            if (DX.CheckHitKey(DX.KEY_INPUT_A) != 1)
                            {
                                if (DX.CheckHitKey(DX.KEY_INPUT_D) != 1)
                                {
                                    this.MoveType = MoveTypeEnum.Down;
                                }
                            }
                        }
                        else
                        {
                            this.MoveType = MoveTypeEnum.Center;
                        }
                    }
                }
            }
        }
    }
}
