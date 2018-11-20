// TestPage2PDF.js script
// Part of PDFCreator
// License: GPL
// Homepage: http://www.pdfforge.org/pdfcreator
// Windows Scripting Host version: 5.6
// Version: 1.0.0.0
// Date: July, 28. 2014
// Author: Sazan Hoti
// Comments: This script demonstrating the conversion  of a printable test file in a jpeg-file using 
//           the COM interface of PDFCreator and additionally adjusting the jpeg settings.



var objFSO = new ActiveXObject("Scripting.FileSystemObject");
var objShell = new ActiveXObject("Shell.Application");

var Scriptname = objFSO.GetFileName(WScript.ScriptFullname);

if (WScript.Version < 5.6)
{
 WScript.Echo("You need the \"Windows Scripting Host version 5.6\" or greater!");
 WScript.Quit();
}

try
{
var PDFCreatorQueue = new ActiveXObject("PDFCreator.JobQueue");

WScript.Echo("Initializing PDFCreator queue...");
PDFCreatorQueue.Initialize();

 var fullPath = objFSO.GetParentFolderName(WScript.ScriptFullname) + "\\TestPage.jpg";
WScript.Echo("Setting up target path to: " + fullPath);
 
WScript.Echo("Printing one windows test page...");
 objShell.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n \"PDFCreator\"", "", "open", 1);


WScript.Echo("Waiting for the job to arrive at the queue...");
if(!PDFCreatorQueue.WaitForJob(10))
{   
	WScript.Echo("The print job did not reach the queue within " + 10 + " seconds"); 
}
else
{
	WScript.Echo("Currently there are " + PDFCreatorQueue.Count + " job(s) in the queue");
	WScript.Echo("Getting job instance");
	var job = PDFCreatorQueue.NextJob;
	job.SetProfileByGuid("JpegGuid");	//Notice that we are converting under JpegGuid.				
	
	WScript.Echo("Applying jpeg settings...");
	/* The SetProfileSettings method allows us to change the JpegSettings of the job*/
	//We want 24 bit colors for our converted file
	job.SetProfileSetting("JpegSettings.Color", "Color24Bit"); 
	//We want the best quality possible for the converted file
	job.SetProfileSetting("JpegSettings.Quality", "100"); 
	
	WScript.Echo("Converting under \"JpegGuid\" conversion profile");
	job.ConvertTo(fullPath);
	
	if(!job.IsFinished || !job.IsSuccessful)
	{
		WScript.Echo("Could not convert the file: " + fullPath);
	}
	else
	{
		WScript.Echo("Job finished successfully");
	}
}
WScript.Echo("Releasing the object");
PDFCreatorQueue.ReleaseCom();
}

catch(e)
{
	WScript.Echo(e.message);
	PDFCreatorQueue.ReleaseCom();
}