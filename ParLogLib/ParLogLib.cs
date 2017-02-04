using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ParLog
{
	public class ParLogLib
	{
		// '04 Feb 2017 15:02:50,778 - this is a start of line

		private string LogFileName = "";
		private Regex StartOfLogEntryRegex;
		private Regex workflowRegex;

		public ParLogLib (string logFile, string startOfLogEntry, string workflowPattern)
		{
			this.LogFileName = logFile;
			this.StartOfLogEntryRegex = new Regex(startOfLogEntry, RegexOptions.IgnoreCase);
			this.workflowRegex = new Regex (workflowPattern, RegexOptions.IgnoreCase);
		}


		public ParLogLib(CmdArguments args)
		{
			this.LogFileName = args.FileWildCard;
			this.StartOfLogEntryRegex = new Regex(args.StartOfLinePattern, RegexOptions.IgnoreCase);
			this.workflowRegex = new Regex (args.SearchPattern, RegexOptions.IgnoreCase);
		}

		public void Parse()
		{
			string[] files = Directory.GetFiles(".", this.LogFileName);
			foreach (string file in files)
			{
				bool matchingMode = true;

				Match mStart, mWorkflowMatch;
				foreach (string line in File.ReadLines(file)) 
				{

					mStart = StartOfLogEntryRegex.Match (line);	
					mWorkflowMatch = workflowRegex.Match(line);

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

