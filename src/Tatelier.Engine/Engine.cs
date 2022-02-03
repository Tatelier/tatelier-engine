using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Engine
{
    interface A
    {
        int DrawGraph(int x, int y);
    }
    /// <summary>
    /// エンジン
    /// </summary>
    public class Engine : INowTime
    {
        public SceneControl SceneControl { get; private set; } = new SceneControl();

        public ImageLoadControl ImageLoadControl { get; private set; } = new ImageLoadControl();

        public SoundLoadControl SoundLoadControl { get; private set; } = new SoundLoadControl();

        public CoroutineControl CoroutineControl { get; private set; } = new CoroutineControl();

        public IEngineFunctionModule FunctionModule { get; private set; }

        public int NowMillisec { get; private set; } = 0;

        public long NowMicrosec { get; private set; } = 0;

        int INowTime.Millisec => NowMillisec;

        long INowTime.Microsec => NowMicrosec;

        public void Start<IScene>(ISupervision supervision)
        {
            if (supervision.FunctionModule != null)
            {
                FunctionModule = supervision.FunctionModule;
            }
            else
            {
                FunctionModule = new Stub.EngineFunctionModule();
            }

            ImageLoadControl.Start(FunctionModule);

            ImageLoadControl.Load("LiteDB.xml");
            ImageLoadControl.Load("LiteDB.xml");

            supervision.BeforeInit();
            FunctionModule.Init();
            supervision.AfterInit();
        }

        public void Run()
        {
            while(FunctionModule.ProcessMessage() == 0)
            {
                FunctionModule.ClearDrawScreen();

                FunctionModule.ScreenFlip();
            }

            CoroutineControl.Update();
            NowMicrosec = 0;
            NowMillisec = (int)(NowMicrosec / 1000);
        }

        public void Finish()
        {

        }
    }
}
