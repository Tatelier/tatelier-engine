using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
	public interface ILogWindow
	{
		void AddLast(string message, uint color = 0xFFFFFF);
	}
}
