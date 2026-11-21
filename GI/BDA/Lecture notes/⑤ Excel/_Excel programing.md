[[Excel DNA with Examples]] – Microsoft recommends this technology for developing **custom Excel add-ins**. We can create custom functions in C#, and, with a little more effort, even build our own Ribbon interface.

[[Excel VSTO]] – This is Microsoft's older technology for creating custom functions as Excel add-ins. It is no longer actively developed, but it is still supported. Many companies still have add-ins based on this technology that are actively used in production.

Excel COM automation in CSharp]] – This approach allows us to open Excel from a C# application and then **control it remotely** to create and save an Excel workbook. The downside is that it only works on Windows and requires Microsoft Office to be installed on the machine.

[[BDA/Lecture notes/⑤ Excel/ClosedXML]] – There are numerous libraries available online that allow us to **read and create Excel files** without having Microsoft Excel installed. ClosedXML is the library we typically use. Its main advantage is that it is cross-platform.

[[Spreadsheet history]] – A brief overview of the history of spreadsheet applications.

## If you only need diagrams

If your goal is simply to generate charts, using Excel might be overkill. You may want to give the following library a try:

https://scottplot.net/