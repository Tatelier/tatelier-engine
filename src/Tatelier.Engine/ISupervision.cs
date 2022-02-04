using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Engine
{
    public interface ISupervision
    {
        IEngineFunctionModule FunctionModule { get; }

        void AfterModuleStart();
        void BeforeModuleStart();
    }
}
