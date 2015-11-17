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

                    for (int i = 0; i < 7; i++)
                    {
                        DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["click.mp3"], DX.DX_PLAYTYPE_BACK);

                        switch (i)
                        {
                            case (0):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["C.png"], DX.TRUE);
                                break;

                            case (1):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["L.png"], DX.TRUE);
                                break;

                            case (2):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["E.png"], DX.TRUE);
                                break;

                            case (3):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["A.png"], DX.TRUE);
                                break;

                            case (4):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["R.png"], DX.TRUE);
                                break;

                            case (5):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["!.png"], DX.TRUE);
                                break;

                            case (6):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["!.png"], DX.TRUE);
                                break;
                        }

                        DX.ScreenFlip();
                        DX.WaitTimer(150);
                    }


                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        switch (i)
                        {
                            case (0):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["C.png"], DX.TRUE);
                                break;

                            case (1):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["L.png"], DX.TRUE);
                                break;

                            case (2):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["E.png"], DX.TRUE);
                                break;

                            case (3):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["A.png"], DX.TRUE);
                                break;

                            case (4):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["R.png"], DX.TRUE);
                                break;

                            case (5):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["!.png"], DX.TRUE);
                                break;

                            case (6):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["!.png"], DX.TRUE);
                                break;
                        }

                    }

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

                    for (int i = 0; i < 7; i++)
                    {
                        DX.PlaySoundMem(SoundLoader.GetInstance().Sounds["click.mp3"], DX.DX_PLAYTYPE_BACK);

                        switch (i)
                        {
                            case (0):
                                DX.DrawGraph(10 + i * 90, 200, loader.Textures["L.png"], DX.TRUE);
                                break;

                            case (1):
                                DX.DrawGraph(10 + i * 90, 200, loader.Textures["O.png"], DX.TRUE);
                                break;

                            case (2):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["S.png"], DX.TRUE);
                                break;

                            case (3):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["E.png"], DX.TRUE);
                                break;

                            case (4):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;

                            case (5):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;

                            case (6):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;
                        }

                        DX.ScreenFlip();
                        DX.WaitTimer(150);
                    }
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        switch (i)
                        {
                            case (0):
                                DX.DrawGraph(10 + i * 90, 200, loader.Textures["L.png"], DX.TRUE);
                                break;

                            case (1):
                                DX.DrawGraph(10 + i * 90, 200, loader.Textures["O.png"], DX.TRUE);
                                break;

                            case (2):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["S.png"], DX.TRUE);
                                break;

                            case (3):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["E.png"], DX.TRUE);
                                break;

                            case (4):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;

                            case (5):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;

                            case (6):
                                DX.DrawGraph(10 + i * 80, 200, loader.Textures["dot.png"], DX.TRUE);
                                break;
                        }
                    }
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
