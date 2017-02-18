using System;
using System.Collections.Generic;

namespace ParLog
{
    public class ConsoleInReader : IInputManager
    {
        public ConsoleInReader()
        {
        }

        public List<string> GetAllFilenamesWildcard()
        {
            return new List<string>(){ "consoleInputStream" };
        }

        public IEnumerable<string> GetLinesOfContent(string fname)
        {
            string line = "x";

            while( ! string.IsNullOrEmpty(line) ) 
            {
                line = Console.In.ReadLine();
                yield return line;
            }
        }
    }
}

