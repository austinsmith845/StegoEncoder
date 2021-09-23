using System;
using System.Collections.Generic;
using System.Text;

namespace StegoEncoder
{
    public interface IEncodeCommand
    {
        byte[] Execute();
    }
}
