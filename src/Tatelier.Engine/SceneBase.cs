using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public class SceneBase : IScene
    {
        public virtual void Update()
        {
        }
        public virtual void Draw()
        {
        }

        public virtual IEnumerator GetStartIterator()
        {
            yield break;
        }

        public virtual IEnumerator GetFinishIterator()
        {
            yield break;
        }

        public virtual int PreStart()
        {
            return 0;
        }
    }
}
