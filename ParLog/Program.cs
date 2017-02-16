using System;
using ParLog;
using System.IO;
using Fclp;

namespace ParLog
{
	class MainClass
	{
		// '04 Feb 2017 15:02:50,778 - this is a start of line

		// call : ParsLog -f "filename or wildcards" -p "regex pattern to search for"

		public static void Main (string[] args)
		{
			
			string defaultStartPattern = @"^[0-9]{2} [\w]{3} [0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3}";
			var p = new FluentCommandLineParser<CmdArguments>();

			p.Setup(arg => arg.FileWildCard)
				.As('f', "wildcard")
				.WithDescription("a file name or wildcard for multiple files")
				.SetDefault("test.log");
				//.Required(); 

			p.Setup(arg => arg.SearchPattern)
				.As('p', "pattern")
				.WithDescription("the regex pattern to filter the file(s)")
				//.Required();
				.SetDefault("031c0b7cc17a4c5eb7ea12deeac56cd2");

			p.Setup(arg => arg.StartOfLinePattern)
				.As('s', "startPattern")
				.WithDescription("regex pattern to define when a new log entry starts")
				.SetDefault(defaultStartPattern); 

            p.Setup(arg => arg.ShowPerformance).As("--performance").WithDescription("only show performance statistics").SetDefault(false);

			p.SetupHelp("?", "help")
				.Callback(text => 
					{
						Console.WriteLine ("");
						Console.WriteLine ("A tool to filter log files for log entries that contain a specific (regex) pattern.");
						Console.WriteLine ("Works also with multiline log entries.");
						Console.WriteLine ("e.g.: show only log entries from a certain workflow instance.");
						Console.WriteLine ("Writes the result to standard output. Use '>' to redirect into a file");
						Console.WriteLine ("");
						Console.WriteLine ("default startPattern is : " + defaultStartPattern);
						Console.WriteLine ("this corresponds to date format: e.g.: 04 Feb 2017 15:02:50,778");
						Console.WriteLine (text);
						Console.WriteLine ("example : ParLog -f=*.log -p=WOIN_afa2348jfs3489");
						Environment.Exit(0); 
					}
				);
 
			
			var result = p.Parse(args);

			if (result.HasErrors == false)
			{
                IFileManager fm = new FileManager(p.Object.FileWildCard);
                ParLogLib llib = new ParLogLib(fm, p.Object);
				llib.Parse();
			}
			else
			{
				Console.WriteLine("error in cmd line parameters.");
			}
		}
	}
}
