using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public interface IScene
    {
        IEnumerator GetStartIterator();

        int PreStart();

        void Update();

        void Draw();

        IEnumerator GetFinishIterator();
    }
}
