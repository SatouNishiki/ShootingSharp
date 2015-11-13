using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.scene
{
    public class SampleScene : SceneBase
    {
        public SampleScene() : base()
        {
            entity.EntityPlayer p = new entity.EntityPlayer();
            this.AddInteractObject(p);
            this.AddPlayer(p);
        }

        public override bool ExistNextScene()
        {
            return false;
        }

        public override bool IsFinished()
        {
            return false;
        }

        public override interfaces.IScene GetNextScene()
        {
            throw new NotImplementedException();
        }
    }
}
