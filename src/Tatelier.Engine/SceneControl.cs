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
    public class SceneControl : ISceneControl
    {
        CoroutineControl coroutineControl;

        public void Start<TScene>()
            where TScene : IScene, new()
        {
            coroutineControl = new CoroutineControl();

            var instance = new TScene();

            coroutineControl.StartCoroutine(instance.GetStartIterator());
        }

        public int LoadScene<TScene>(out TScene scene)
            where TScene : IScene, new()
        {
            scene = new TScene();

            int ret = scene.PreStart();

            if (ret != 0)
            {
                return ret;
            }

            return 0;
        }

        public void Update()
        {
            coroutineControl.Update();
        }

        public void Draw()
        {
        }

        public void Regist(float layer, IScene scene)
        {
        }

        public void Unregist(IScene scene)
        {
            throw new NotImplementedException();
        }
    }
}
