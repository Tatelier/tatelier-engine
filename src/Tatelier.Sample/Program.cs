using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Sample
{
    class Program : Engine.ISupervision
    {
        public static Program Singleton = new Program();
        public static Engine.Engine Engine { get; private set; }

        public Action OnBeforeDxInit { get; set; }
        public Action OnAfterDxInit { get; set; }

        public IEngineFunctionModule FunctionModule { get; } = new Engine.Stub.EngineFunctionModule();

        void main(string[] args)
        {
            Engine = new Engine.Engine();

            Engine.Start<Sample>(this);

            Engine.Run();

            Engine.Finish();
        }

        static void Main(string[] args)
        {
            Singleton.main(args);
        }

        public void BeforeModuleStart()
        {
            FunctionModule.WindowMode = true;
            FunctionModule.SetGraphMode(512, 384, 32);
        }

        public void AfterModuleStart()
        {

        }
    }
}
