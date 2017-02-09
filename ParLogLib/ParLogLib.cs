using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ParLog
{
	public class ParLogLib
	{
		// '04 Feb 2017 15:02:50,778 - this is a start of line
        private IFileManager fManager;
		private Regex StartOfLogEntryRegex;
        private Regex SearchTermRegex;

        public ParLogLib(IFileManager fMgr, string startOfLogEntryPattern, string searchPattern)
        {
            this.fManager = fMgr;
            this.StartOfLogEntryRegex = new Regex(startOfLogEntryPattern, RegexOptions.IgnoreCase);
            this.SearchTermRegex = new Regex (searchPattern, RegexOptions.IgnoreCase);
        }

		public void Parse()
		{
            foreach (string file in this.fManager.GetAllFilenamesWildcard())
			{
                bool matchingMode = false;

				Match mStart, mWorkflowMatch;
                foreach (string line in this.fManager.GetLinesOfFile(file)) 
				{
                   
					mStart = StartOfLogEntryRegex.Match (line);	
					mWorkflowMatch = SearchTermRegex.Match(line);

					if (mStart.Success && mWorkflowMatch.Success) 
					{
						// new log entry found	
						Console.WriteLine (line);
						matchingMode = true;
					} 
					else if (mStart.Success && !mWorkflowMatch.Success)
					{
						matchingMode = false;
					}


					if (!mStart.Success) 
					{
						if (matchingMode)
							Console.WriteLine (line);
					}
				}
			}
		}

    

	}
}

