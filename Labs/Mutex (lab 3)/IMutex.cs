using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex
{
    public interface IMutex
    {
        void Lock();
        bool Unlock();
    }
}
