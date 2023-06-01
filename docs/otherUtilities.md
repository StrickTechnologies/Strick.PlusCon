## Other Utilities
### Virtual Terminal
If you see things that look like this:
```
?[38;2;255;0;0mHello world!?[39m
```
when you are expecting something with colors, you will need to enable virtual terminal mode. This will typically happen if you are running a console application from Windows Explorer, the Windows Command Prompt, or Windows PowerShell.

One solution is to run the application from [Windows Terminal](https://apps.microsoft.com/store/detail/windows-terminal/9N0DX20HK701) (a free download). Another is to add a call to `ConsoleUtilities.EnableVirtualTerminal` in your application ahead of where you want to use escape sequences to format console output.
