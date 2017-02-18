using System;
using System.Collections.Generic;

namespace ParLog
{
    public interface IInputManager
    {
        List<string> GetAllFilenamesWildcard();
        IEnumerable<string> GetLinesOfContent(string fname);
    }
}

