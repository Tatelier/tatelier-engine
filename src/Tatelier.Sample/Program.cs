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

        Engine.Engine engine;

        public Action OnBeforeDxInit { get; set; }
        public Action OnAfterDxInit { get; set; }

        public IEngineFunctionModule FunctionModule => null;

        void main(string[] args)
        {
            engine = new Tatelier.Engine.Engine();


            engine.Start<Engine.SceneBase>(this);

            engine.Run();

            engine.Finish();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.main(args);
        }

        public void BeforeInit()
        {
        }

        public void AfterInit()
        {

        }
    }
}
