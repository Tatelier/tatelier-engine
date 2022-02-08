using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine;

namespace Tatelier.Sample
{
    class Sample
        : SceneBase
    {
        public override IEnumerator GetStartIterator()
        {
            var imageLoadControl = Program.Engine.ImageLoadControl;
            var fontLoadControl = Program.Engine.FontLoadControl;

            var font = fontLoadControl.Create("NS");
            imageLoadControl.Load("LiteDB.xml");

            imageLoadControl.IsDebug = true;
            imageLoadControl.Load("LiteDB.xml");
            imageLoadControl.OutputLog();

            yield break;
        }
    }
}
