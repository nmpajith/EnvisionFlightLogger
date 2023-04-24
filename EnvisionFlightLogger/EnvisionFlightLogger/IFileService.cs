using System;
using System.Collections.Generic;
using System.Text;

namespace EnvisionFlightLogger
{
    public interface IFileService
    {
        byte[] LoadFile(string filePath);
    }
}
