using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace ParLog
{
	public class ParLogLib
	{
		// '04 Feb 2017 15:02:50,778 - this is a start of line
        private IInputManager ContentInputManager;
		private Regex StartOfLogEntryRegex;
        private Regex SearchTermRegex;
        private bool ShowPerformance = false;
        private Stopwatch stopwatch = new Stopwatch();

        public ParLogLib(IInputManager contentInputManager, CmdArguments args)
        {
            this.ContentInputManager = contentInputManager;
            this.StartOfLogEntryRegex = new Regex(args.StartOfLinePattern, RegexOptions.IgnoreCase);
            this.SearchTermRegex = new Regex (args.SearchPattern, RegexOptions.IgnoreCase);
            this.ShowPerformance = args.ShowPerformance;
        }

		public void Parse()
		{
            if (this.ShowPerformance)
            {
                stopwatch.Start();
            }

            foreach (string file in this.ContentInputManager.GetAllFilenamesWildcard())
			{
                bool matchingMode = false;

                Match mStart, mSearchTermMatch;
                foreach (string line in this.ContentInputManager.GetLinesOfContent(file)) 
				{
                    if (line == null) continue;
                    
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
            if (this.ShowPerformance)
            {
                stopwatch.Stop();
                Console.WriteLine("Time Elapsed: {0} ms", stopwatch.Elapsed.Milliseconds);
            }
		}

    

	}
}

