using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public interface ISceneControl
    {
        void Start<TStartScene>()
            where TStartScene : IScene, new();

        /// <summary>
        /// シーンを登録する
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="scene"></param>
        void Regist(float layer, IScene scene);

        void Unregist(IScene scene);

        int LoadScene<TScene>(out TScene scene) where TScene : IScene, new();
    }
}
