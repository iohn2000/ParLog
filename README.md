# ParLog
parses log files filter for certain string in log file entry

A tool to filter log files for log entries that contain a specific (regex) pattern.
Works also with multiline log entries.
e.g.: show only log entries from a certain workflow instance.
Writes the result to standard output. Use '>' to redirect into a file

example : ParLog -f=*.log -p=WOIN_afa2348jfs3489
