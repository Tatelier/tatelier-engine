using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    class SoundGroup
    {
        SoundLoadControl soundLoadControl;

        List<int> handles = new List<int>();

        public int Volume { get; set; }

        public int Load(string filePath)
        {
            int handle = soundLoadControl.Load(filePath);

            if(handle == -1)
            {
                return -1;
            }

            handles.Add(handle);

            return handle;
        }

        public void Update()
        {
            if (false)
            {
                foreach(var item in handles)
                {
                    // 音量すべて調整
                }
            }
        }

        public SoundGroup(SoundLoadControl soundLoadControl)
        {
            this.soundLoadControl = soundLoadControl;
        }
    }
}
