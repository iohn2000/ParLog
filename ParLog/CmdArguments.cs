using System;

namespace ParLog
{
    public class CmdArguments
    {
        public string FileWildCard
        {
            get;
            set;
        }

        public string SearchPattern
        {
            get;
            set;
        }

        public string StartOfLinePattern
        {
            get;
            set;
        }

        public bool ShowPerformance { get; set; }
    }
}

