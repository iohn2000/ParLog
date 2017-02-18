using System;
using System.Collections.Generic;
using System.IO;

namespace ParLog
{
    public class FileInReader : IInputManager
    {
        private string WildCard;

        public FileInReader(string wildCard)
        {
            this.WildCard = wildCard;
        }

        /// <summary>
        /// Gets all files that match the wildcard.
        /// Bases on current directoy (".")
        /// </summary>
        /// <returns>List<string> with file names.</returns>
        /// <param name="wildCard">Wild card.</param>
        public List<string> GetAllFilenamesWildcard()
        {
            string[] files = Directory.GetFiles(".", this.WildCard);
            return new List<string>(files);
        }

        public IEnumerable<string> GetLinesOfContent(string fname)
        {
            return File.ReadLines(fname);
        }

    }
}

