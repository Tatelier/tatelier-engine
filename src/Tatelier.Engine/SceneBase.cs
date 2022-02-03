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
        protected void Regist(float layer)
        {

        }

        public void Update()
        {
        }
        public void Draw()
        {
        }

        public IEnumerator GetStartIterator()
        {
            yield break;
        }

        public IEnumerator GetFinishIterator()
        {
            yield break;
        }
    }
}
