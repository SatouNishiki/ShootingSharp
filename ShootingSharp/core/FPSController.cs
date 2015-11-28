using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.core
{
    public class FPSController
    {
        int frame_time; //1フレームの時間(ミリ秒)
        int wait_time; //待ち時間
        int last_time, now_time; //最後に取得した時間と，今の時間
        float count; //フレームのカウント
        float fps; //表示するしたFPS値
        int update_time; //表示値を更新する時間
        int last_update; //最後に表示値を更新した時間
        //描画関係の変数
        int disp_x, disp_y;
        

        //コンストラクタ
        public FPSController(float RefreshRate, int UpdateTime)
        {
            Init(RefreshRate, UpdateTime);
        }
        public FPSController()
        {
            Init(60.0f, 800);
        }
        //待ち時間の計算
        private void Wait()
        {
            now_time = DX.GetNowCount();
            wait_time = frame_time - (now_time - last_time);
            if (wait_time > 0)
            { //待ち時間があれば
                DX.WaitTimer(wait_time); //指定時間待つ
            }
            last_time = DX.GetNowCount();
        }
        //FPS値の計算
        public float Get()
        {
            count += 1.0f;
            if (update_time < (last_time - last_update))
            { //アップデート時間になっていれば
                fps = count / (float)(last_time - last_update) * 1000.0f; //FPS値の計算
                last_update = last_time;
                count = 0.0f;
            }
            return (fps);
        }
        //描画処理
        public void Disp()
        {
           //  DX.DrawStringF( disp_x , disp_y , disp_color , "fps:%0.1f" , fps );
            DX.SetWindowText("FPS:" + ((int)Math.Round(fps)).ToString());
        }
        //処理をまとめたもの
        public float All()
        {
            Get();
            Wait();
            Disp();
            return (fps);
        }
        //描画設定の変更
        public void SetDisp(int x, int y)
        {
            disp_x = x;
            disp_y = y;
            // disp_color = color;
        }

        //初期化
        private void Init(float RefreshRate, int UpdateTime)
        {
            frame_time = (int)(1000.0f / RefreshRate); //1フレームの時間の計算
            update_time = UpdateTime;
            wait_time = 0;
            last_time = now_time = 0;
            count = 0.0f;
            fps = 0.0f;
            last_update = 0;
            //描画関係
            disp_x = 0;
            disp_y = 0;
        }
    }
}
