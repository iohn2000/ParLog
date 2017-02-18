using System;
using ParLog;
using System.IO;
using Fclp;

namespace ParLog
{
	public class MainClass
	{
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

            p.Setup(arg => arg.ShowPerformance).As("performance").WithDescription("only show performance statistics").SetDefault(false);

			p.SetupHelp("?", "help")
				.Callback(text => 
					{
                        Console.WriteLine (Helper.HelpText());
						Environment.Exit(0); 
					}
				);
 
			var result = p.Parse(args);
			if (result.HasErrors == false)
			{
                IInputManager InputManager = null;
                if (Console.IsInputRedirected)
                {
                    InputManager = new ConsoleInReader();
                }
                else
                {
                    InputManager = new FileInReader(p.Object.FileWildCard);    
                }

                ParLogLib llib = new ParLogLib(InputManager, p.Object);
				llib.Parse();
			}
			else
			{
				Console.WriteLine("error in cmd line parameters.");
			}
		}
	}
}
