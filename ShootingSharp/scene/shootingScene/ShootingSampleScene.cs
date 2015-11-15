using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.enemy;
using ShootingSharp.position;
using ShootingSharp.entity.player;
using ShootingSharp.task;
using ShootingSharp.texture;
using DxLibDLL;
using ShootingSharp.sound;
using ShootingSharp.scene;

namespace ShootingSharp.scene.shootingScene
{
    public class ShootingSampleScene : ShootingSceneBase
    {
        public ShootingSampleScene() : base()
        {
            EntityReimu p = new EntityReimu();
            p.SetPosition(new SSPosition(SSGame.GetInstance().GetWindowSize().Width / 2, SSGame.GetInstance().GetWindowSize().Height - 10));
            this.AddInteractObject(p);
            this.AddPlayer(p);

            SampleEnemy enemy = new SampleEnemy();
            enemy.InteractManager = this;
            enemy.SetPopCount(100);
            this.AddEnemy(enemy);

            SSTaskFactory.BackGroundImageTask.ImageHandle = TextureLoader.GetInstance().Textures["back_1.jpg"];
            SSTaskFactory.BackGroundImageTask.ScrollSpeed = 2;
            SSTaskFactory.BackGroundImageTask.ImageY = 898;


            DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["ginen.mp3"], DX.DX_PLAYTYPE_LOOP);
            
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
