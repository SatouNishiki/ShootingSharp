using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;
using DxLibDLL;
using ShootingSharp.sound;
using ShootingSharp.texture;
using ShootingSharp.core;
using ShootingSharp.task;

namespace ShootingSharp.scene
{
    public class ResultSceneBase : IScene
    {
        public enum ResultType
        {
            Clear, Lose
        }

        private bool flag = true;
        private TextureLoader loader = TextureLoader.GetInstance();
        private SSGame game = SSGame.GetInstance();
        private ResultType type;

        public ResultSceneBase(ResultType type)
        {
            this.type = type;
        }

        public bool Run()
        {

            if (this.type == ResultType.Clear)
            {

                if (flag)
                {
                    //初回は音鳴らす
                    SoundLoader.GetInstance().PlayLoopSound("clear.mp3");
                    
                }

                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 128);

                DX.DrawExtendGraph(0, 0, game.GetWindowSize().Width, game.GetWindowSize().Height, loader.Textures["result_back.jpg"], DX.TRUE);

                DX.DrawGraph(game.GetWindowSize().Width - 600, -50, loader.Textures["result_char.png"], DX.TRUE);

                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);


                DX.DrawStringFToHandle(10, 10, "Result", DX.GetColor(0, 0, 255), FontProvider.GetSisterFontHandle(30, 13));


                DX.DrawStringFToHandle(10, 50,  "    Kill Score   " + SSTaskFactory.InfoDrawTask.KillScore.ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20,15));

                DX.DrawStringFToHandle(10, 80, "  + Time Score   " + SSTaskFactory.InfoDrawTask.TimeScore.ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 15));

                DX.DrawStringFToHandle(10, 110, "  =  All Score   " + (SSTaskFactory.InfoDrawTask.TimeScore + SSTaskFactory.InfoDrawTask.KillScore).ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 15));


                if (flag)
                {
                    Effecter.TypeWrite(100, 200, 80, "C.png", "L.png", "E.png", "A.png", "R.png", "!.png", "!.png");
                }
                else
                {
                    Effecter.DrawContinuityGraph(10, 200, 80, "C.png", "L.png", "E.png", "A.png", "R.png", "!.png", "!.png");
                }


                if (flag)
                {

                    DX.ScreenFlip();
                    DX.WaitTimer(2000);
                    flag = false;
                }
                
                DX.DrawStringFToHandle(200, 400, "なにかキーを押してください", DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 13));
            }
            else
            {
                if (flag)
                {
                    //初回は音鳴らす
                    SoundLoader.GetInstance().PlayLoopSound("lose.mp3");
                }

                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 128);

                DX.DrawExtendGraph(0, 0, game.GetWindowSize().Width, game.GetWindowSize().Height, loader.Textures["result_back.jpg"], DX.TRUE);

                DX.DrawGraph(game.GetWindowSize().Width - 600, -50, loader.Textures["result_char2.png"], DX.TRUE);

                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);



                DX.DrawStringFToHandle(10, 10, "Result", DX.GetColor(0, 0, 255), FontProvider.GetSisterFontHandle(30, 13));


                DX.DrawStringFToHandle(10, 50, "    Kill Score   " + SSTaskFactory.InfoDrawTask.KillScore.ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 15));

                DX.DrawStringFToHandle(10, 80, "  + Time Score   " + SSTaskFactory.InfoDrawTask.TimeScore.ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 15));

                DX.DrawStringFToHandle(10, 110, "  =  All Score   " + (SSTaskFactory.InfoDrawTask.TimeScore + SSTaskFactory.InfoDrawTask.KillScore).ToString(), DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 15));

                if (flag)
                {
                    Effecter.TypeWrite(100, 200, 80, "L.png", "O.png", "S.png", "E.png", "dot.png", "dot.png", "dot.png");
                }
                else
                {
                    Effecter.DrawContinuityGraph(100, 200, 80, "L.png", "O.png", "S.png", "E.png", "dot.png", "dot.png", "dot.png");
                }
                    
                
                if (flag)
                {
                    DX.ScreenFlip();
                    DX.WaitTimer(2000);
                    flag = false;
                }

                DX.DrawStringFToHandle(200, 400, "なにかキーを押してください", DX.GetColor(255, 0, 0), FontProvider.GetSisterFontHandle(20, 13));
            }
            return !this.IsFinished();
        }

        public bool ExistNextScene()
        {
            return true;
        }

        public bool IsFinished()
        {
            //何かキーが押されたら終わる
            if (DX.CheckHitKeyAll() != 0)
            {
                SoundLoader.GetInstance().StopSount("clear.mp3");
                SoundLoader.GetInstance().StopSount("lose.mp3");
                return true;
            }
            else
                return false;
        }

        public IScene GetNextScene()
        {
            return this.GetTitleScene();
        }

        public virtual TitleSceneBase GetTitleScene()
        {
            return new TitleSceneBase();
        }
    }
}
