using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.texture;
using ShootingSharp.entity.shot;
using ShootingSharp.task;
using ShootingSharp.sound;
using ShootingSharp.interfaces;
using ShootingSharp.entity.bom;
using ShootingSharp.core;

namespace ShootingSharp.entity
{

    public abstract class EntityPlayer : LivingAnimationEntity, IHasBom
    {
        public enum MainShotType
        {
            Normal, Three, Five
        }

        public enum SubShotType
        {
            None, Side
        }

        private const int powerMax = 20;

        protected int power;

        protected TextureLoader textureLoader;

        private int shotCount;

        protected int shotInterval;

        private bool isDeathTime;

        private int deathCount;

        private int noInteractTime;

        private int tempSpeed;

        protected MainShotType mainShotType;
        protected SubShotType subShotType;

        private int fiveShotCount;

       
        private int bomCount;

        public EntityPlayer()
            : base()
        {

            this.textureLoader = TextureLoader.GetInstance();
            this.logger.Debug(this.GetType().ToString() + " is create");

            this.Life = 3;
            this.MaxLife = 3;

            this.bomCount = 1;

            this.position.PosX = SSGame.GetInstance().GetBattleWindowSize().Width / 2;
            this.position.PosY = SSGame.GetInstance().GetBattleWindowSize().Height - 100;

            this.textureLoader.LoadSprite("ora.png", 512 / 20, 256 / 20, 1, 20, 20);

            this.mainShotType = MainShotType.Normal;
            this.subShotType = SubShotType.None;

            this.friendCode = "player";
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


        public override void Move()
        {
            if (this.isDeathTime)
            {
                this.position.PosY--;

                this.deathCount++;


                if (this.deathCount > 96)
                {
                    foreach (var item in SSTaskFactory.ShotUpdateTask.ShotList)
                    {
                        item.Life = 0;
                    }
                }

                if (this.deathCount > 100)
                {
                    this.isDeathTime = false;
                    this.deathCount = 0;
                    this.noInteractTime = 40;
                }

                return;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_LSHIFT) == 1 || DX.CheckHitKey(DX.KEY_INPUT_RSHIFT) == 1)
            {
                this.tempSpeed = this.moveSpeed;
                this.moveSpeed /= 2;
            }

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

            if (this.tempSpeed != 0)
            {
                this.moveSpeed = tempSpeed;
            }

            tempSpeed = 0;
        }

        private bool CanRightMove()
        {
            return this.position.PosX < SSGame.GetInstance().GetBattleWindowSize().Width - 10;
        }

        private bool CanLeftMove()
        {
            return this.position.PosX > 10;
        }

        private bool CanUpMove()
        {
            return this.position.PosY > 10;
        }

        private bool CanDownMove()
        {
            return this.position.PosY < SSGame.GetInstance().GetBattleWindowSize().Height - 10;
        }

        public override void DoAction()
        {
            if (this.isDeathTime) return;

            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
            {
                if (shotCount >= this.shotInterval)
                {
                    Shot s = this.GetShot();
                    //味方に設定
                    s.SetFriendCode(this.friendCode);
                    this.InteractManager.AddInteractObject(s);
                    SSTaskFactory.ShotMoveTask.ShotList.Add(s);
                    SSTaskFactory.ShotDrawTask.ShotList.Add(s);
                    SSTaskFactory.ShotUpdateTask.ShotList.Add(s);
                    this.shotCount = 0;
                    this.PlayShotSound();

                    if (this.mainShotType == MainShotType.Three || this.mainShotType == MainShotType.Five)
                    {
                        Shot s2 = this.GetThreeShot(100.0D);
                        //味方に設定
                        s2.SetFriendCode(this.friendCode);
                        this.InteractManager.AddInteractObject(s2);
                        SSTaskFactory.ShotMoveTask.ShotList.Add(s2);
                        SSTaskFactory.ShotDrawTask.ShotList.Add(s2);
                        SSTaskFactory.ShotUpdateTask.ShotList.Add(s2);

                        Shot s3 = this.GetThreeShot(80.0D);
                        //味方に設定
                        s3.SetFriendCode(this.friendCode);
                        this.InteractManager.AddInteractObject(s3);
                        SSTaskFactory.ShotMoveTask.ShotList.Add(s3);
                        SSTaskFactory.ShotDrawTask.ShotList.Add(s3);
                        SSTaskFactory.ShotUpdateTask.ShotList.Add(s3);
                    }

                    if (this.mainShotType == MainShotType.Five)
                    {
                        if (this.fiveShotCount % 2 == 0)
                        {
                            Shot s4 = this.GetFiveShot(110.0D);
                            //味方に設定
                            s4.SetFriendCode(this.friendCode);
                            this.InteractManager.AddInteractObject(s4);
                            SSTaskFactory.ShotMoveTask.ShotList.Add(s4);
                            SSTaskFactory.ShotDrawTask.ShotList.Add(s4);
                            SSTaskFactory.ShotUpdateTask.ShotList.Add(s4);

                            Shot s5 = this.GetFiveShot(70.0D);
                            //味方に設定
                            s5.SetFriendCode(this.friendCode);
                            this.InteractManager.AddInteractObject(s5);
                            SSTaskFactory.ShotMoveTask.ShotList.Add(s5);
                            SSTaskFactory.ShotDrawTask.ShotList.Add(s5);
                            SSTaskFactory.ShotUpdateTask.ShotList.Add(s5);
                        }

                        this.fiveShotCount++;
                    }
                    /*
                    if (this.subShotType == SubShotType.Side)
                    {
                        Shot s2 = this.GetSubShot(80.0D);
                        //味方に設定
                        s2.SetFriendCode(this.friendCode);
                        this.InteractManager.AddInteractObject(s2);
                        SSTaskFactory.ShotMoveTask.ShotList.Add(s2);
                        SSTaskFactory.ShotDrawTask.ShotList.Add(s2);
                        SSTaskFactory.ShotUpdateTask.ShotList.Add(s2);
                        this.shotCount = 0;

                        Shot s3 = this.GetSubShot(20.0D);
                        //味方に設定
                        s3.SetFriendCode(this.friendCode);
                        this.InteractManager.AddInteractObject(s3);
                        SSTaskFactory.ShotMoveTask.ShotList.Add(s3);
                        SSTaskFactory.ShotDrawTask.ShotList.Add(s3);
                        SSTaskFactory.ShotUpdateTask.ShotList.Add(s3);
                        this.shotCount = 0;
                    }*/
                }
            }
            if (this.shotCount < this.shotInterval)
                this.shotCount++;

            if (DX.CheckHitKey(DX.KEY_INPUT_L) == 1)
            {
                if (this.bomCount > 0)
                    this.Bom();
            }
        }

        protected abstract Shot GetShot();

        /// <summary>
        /// 3-way弾の時でてくる弾です
        /// </summary>
        /// <returns></returns>
        protected abstract Shot GetThreeShot(double theta);

        /// <summary>
        /// 5-way弾の時でてくる弾です
        /// </summary>
        /// <returns></returns>
        protected abstract Shot GetFiveShot(double theta);

        /// <summary>
        /// SubShotで出てくる弾です
        /// </summary>
        /// <returns></returns>
        protected abstract Shot GetSubShot(double theta);

        public override void OnInteract(Entity entity)
        {
            if (entity is item.Item)
                return;

            if (!this.isDeathTime)
            {
                this.Life--;

                this.mainShotType = MainShotType.Normal;
                this.subShotType = SubShotType.None;
                this.power = 0;
                this.bomCount = 1;

                this.isDeathTime = true;

                DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["death.mp3"], DX.DX_PLAYTYPE_BACK);

                this.position.PosX = SSGame.GetInstance().GetBattleWindowSize().Width / 2;
                this.position.PosY = SSGame.GetInstance().GetBattleWindowSize().Height;

            }
        }


        /// <summary>
        /// テクスチャ左上座標
        /// </summary>
        /// <returns></returns>
        public override position.SSPosition GetTexturePosition()
        {
            ShootingSharp.position.SSPosition pos = new position.SSPosition();

            pos.PosX = this.position.PosX - this.GetTextureSize().Width / 2;
            pos.PosY = this.position.PosY - this.GetTextureSize().Height / 2;

            return pos;
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

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public override void Draw()
        {
            if (this.isDeathTime || this.noInteractTime > 0)
            {

                DX.DrawExtendGraph(
                    this.GetTexturePosition().PosX - 45,
                    this.GetTexturePosition().PosY - 45,
                    this.GetTexturePosition().PosX + this.GetTextureSize().Width + 51,
                    this.GetTexturePosition().PosY + this.GetTextureSize().Height + 51,
                    this.textureLoader.Textures["ora.png0"],
                    DX.TRUE);
            }

            DX.DrawExtendGraph(
                this.GetTexturePosition().PosX,
                this.GetTexturePosition().PosY,
                this.GetTexturePosition().PosX + this.GetTextureSize().Width + 1,
                this.GetTexturePosition().PosY + this.GetTextureSize().Height + 1,
                this.textureLoader.Textures[this.GetTextureName()],
                DX.TRUE);
        }

        protected virtual void PlayShotSound()
        {
            DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["default_shot.mp3"], DX.DX_PLAYTYPE_BACK);
        }

        public override void OnDeath()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();


            if (this.noInteractTime > 0)
            {
                this.noInteractTime--;
                return;
            }
        }

        public void AddMainShot()
        {
            if (this.mainShotType == MainShotType.Normal)
            {
                this.mainShotType = MainShotType.Three;
            }
            else if (this.mainShotType == MainShotType.Three)
            {
                this.mainShotType = MainShotType.Five;
            }
        }

        public void AddSubShot()
        {
            if (this.subShotType == SubShotType.None)
            {
                this.subShotType = SubShotType.Side;
            }
        }

        public void AddPower(int power)
        {
            this.power += power;

            if (this.power >= powerMax)
            {
                if (this.mainShotType != MainShotType.Five)
                {
                    this.AddMainShot();
                }
                else
                {
                    this.power = powerMax;
                    SSTaskFactory.InfoDrawTask.ItemScore += power * 100;
                }
            }
        }

        public int GetPower()
        {
            return this.power;
        }

        public bool CanShot()
        {
            throw new NotImplementedException();
        }

        public void Bom()
        {
            Bom s = this.GetBom();
            s.SetPosition(new position.SSPosition(this.position.PosX, this.position.PosY));
            s.SetFriendCode(this.friendCode);
            this.InteractManager.AddInteractObject(s);
            SSTaskFactory.BomUpdateTask.BomList.Add(s);
            SSTaskFactory.BomDrawTask.BomList.Add(s);

            this.bomCount--;

            Effecter.CutIn(this.GetCutInTextureName(), 0, 0, SSGame.GetInstance().GetWindowSize().Height, SSGame.GetInstance().GetWindowSize().Height, 1000);
        }

        protected abstract Bom GetBom();

        public int GetBomCount()
        {
            return this.bomCount;
        }

        public abstract string GetCutInTextureName();
    }
}
