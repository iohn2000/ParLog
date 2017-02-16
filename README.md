# ParLog
A tool to filter log files for log entries that contain a specific (regex) pattern.
Works also with multiline log entries.
Writes the result to standard output. 
Use '>' to redirect into a file

default startPattern is : ^[0-9]{2} [\w]{3} [0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3}
this corresponds to date format: e.g.: 04 Feb 2017 15:02:50,778

	f:wildcard		a file name or wildcard for multiple files
	p:pattern		the regex pattern to filter the file(s)
	s:startPattern		regex pattern to define when a new log entry starts

example : ParLog -f=*.log -p=findMe
