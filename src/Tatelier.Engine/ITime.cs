﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    interface INowTime
    {
        int Millisec { get; }

        long Microsec { get; }
    }
}
