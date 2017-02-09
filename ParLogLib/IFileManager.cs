using System;
using System.Collections.Generic;

namespace ParLog
{
    public interface IFileManager
    {
        List<string> GetAllFilenamesWildcard();
        IEnumerable<string> GetLinesOfFile(string fname);
    }
}

