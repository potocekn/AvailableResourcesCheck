@rem !! This file applies to Windows, adjust it to your platform if necessary
@rem !! Please, make sure that path to the C# compiler (csc.exe) is included in your Windows' PATH environment variable
@rem !! If it is not, uncomment the following line and include the correct path to the C# compiler in it
@rem !! path=C:\WINDOWS\Microsoft.NET\Framework\v4.0;%path%

@rem !! Uncomment the following line to recompile the "DotNetWikiBot.dll" library
@rem csc /t:library /debug:full /o- DotNetWikiBot.cs

@rem !! If you use Page.ReviseInMSWord() function in your code, uncomment the following command
@rem !! to recompile the "DotNetWikiBot.dll" library with Microsoft Word interoperability enabled,
@rem !! and make sure you've installed the proper PIA and the path to the PIA below is correct
@rem csc /t:library /debug:full /o- DotNetWikiBot.cs /reference:"C:\Program Files\PIAs for MS Office XP\Microsoft.Office.Interop.Word.dll" /define:MS_WORD_INTEROP

@rem !! Exclude "/debug:full /o-" options from the following line to optimize bot performance after having it debugged
csc BotScript.cs /debug:full /o- /reference:DotNetWikiBot.dll
BotScript.exe
@pause