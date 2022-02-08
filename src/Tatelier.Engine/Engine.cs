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
    /// <summary>
    /// エンジン
    /// </summary>
    public class Engine : INowTime
    {
        SceneControl sceneControl = new SceneControl();
        public ISceneControl SceneControl => sceneControl;


        public ImageLoadControl ImageLoadControl { get; private set; } = new ImageLoadControl();

        public SoundLoadControl SoundLoadControl { get; private set; } = new SoundLoadControl();

        public FontLoadControl FontLoadControl { get; private set; } = new FontLoadControl();

        public CoroutineControl CoroutineControl { get; private set; } = new CoroutineControl();

        public IEngineFunctionModule FunctionModule { get; private set; }

        public int NowMillisec { get; private set; } = 0;

        public long NowMicrosec { get; private set; } = 0;

        int INowTime.Millisec => NowMillisec;

        long INowTime.Microsec => NowMicrosec;

        public void Start<TStartScene>(ISupervision supervision)
            where TStartScene : IScene, new ()
        {
            if(supervision.FunctionModule == null)
            {
                // error
                return;
            }

            FunctionModule = supervision.FunctionModule;

            SceneControl.Start<TStartScene>();

            ImageLoadControl.Start(FunctionModule);
            FontLoadControl.Start(FunctionModule);

            supervision.BeforeModuleStart();
            FunctionModule.ModuleStart();
            supervision.AfterModuleStart();
        }

        public void Run()
        {
            while(FunctionModule.ProcessMessage() == 0)
            {
                FunctionModule.ClearDrawScreen();

                NowMicrosec = 0;
                NowMillisec = (int)(NowMicrosec / 1000);

                sceneControl.Update();
                sceneControl.Draw();

                FunctionModule.ScreenFlip();
            }

            CoroutineControl.Update();
        }

        public void Finish()
        {

        }
    }
}
