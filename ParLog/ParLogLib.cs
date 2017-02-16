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
        private bool ShowPerformance = false;

        public ParLogLib(IFileManager fMgr, CmdArguments args/*string startOfLogEntryPattern, string searchPattern*/)
        {
            this.fManager = fMgr;
            this.StartOfLogEntryRegex = new Regex(args.StartOfLinePattern, RegexOptions.IgnoreCase);
            this.SearchTermRegex = new Regex (args.SearchPattern, RegexOptions.IgnoreCase);
            this.ShowPerformance = args.ShowPerformance;
        }

		public void Parse()
		{
            foreach (string file in this.fManager.GetAllFilenamesWildcard())
			{
                bool matchingMode = false;

                Match mStart, mSearchTermMatch;
                foreach (string line in this.fManager.GetLinesOfFile(file)) 
				{
                   
					mStart = StartOfLogEntryRegex.Match (line);	
					mSearchTermMatch = SearchTermRegex.Match(line);

					if (mStart.Success && mSearchTermMatch.Success) 
					{
						// new log entry found	
						Console.WriteLine (line);
						matchingMode = true;
					} 
					else if (mStart.Success && !mSearchTermMatch.Success)
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

