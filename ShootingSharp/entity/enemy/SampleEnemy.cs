using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.entity.enemy
{
    public class SampleEnemy : Enemy
    {
        public SampleEnemy()
            : base()
        {
            //初期位置を適当に設定
            this.position.PosX = 100;
            this.position.PosY = 0;
        }

        public override void OnDeath()
        {
            //なにもしない
        }

        public override void Move()
        {
            this.position.PosY += 2;
        }

        public override void DoAction()
        {
            //なにもしない
        }

        public override int GetRadius()
        {
            return 30;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return interfaces.SharpType.Circle;
        }

        public override string GetTextureName()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Size GetTextureSize()
        {
            throw new NotImplementedException();
        }

        public override position.SSPosition GetTexturePosition()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            DX.DrawCircle(this.position.PosX, this.position.PosY, this.GetRadius(), DX.GetColor(0, 255, 0));
        }
    }
}
