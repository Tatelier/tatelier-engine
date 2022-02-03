using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    /// <summary>
    /// シーン管理
    /// </summary>
    public class SceneControl
    {
        CoroutineControl coroutineControl;
        
        
        public void Start<TScene>()
            where TScene : IScene, new()
        {
            coroutineControl = new CoroutineControl();

            var instance = new TScene();

            coroutineControl.StartCoroutine(instance.GetStartIterator());
        }

        public void Update()
        {
            coroutineControl.Update();
        }

        public void Draw()
        {
        }
    }
}
