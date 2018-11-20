// TestPage2PDF.js script
// Part of PDFCreator
// License: GPL
// Homepage: http://www.pdfforge.org/pdfcreator
// Windows Scripting Host version: 5.6
// Version: 1.0.0.0
// Date: July, 28. 2014
// Author: Sazan Hoti
// Comments: This script demonstrating the conversion  of a printable test file in a pdf-file using 
//           the COM interface of PDFCreator and additionally sending it via the standard E-Mail Client.


var objFSO = new ActiveXObject("Scripting.FileSystemObject");
var objShell = new ActiveXObject("Shell.Application");

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

 var fullPath = objFSO.GetParentFolderName(WScript.ScriptFullname) + "\\TestPage.pdf";
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
	job.SetProfileByGuid("DefaultGuid");						
	
	WScript.Echo("Applying e-mail client settings...");
	/* The SetProfileSettings method allows us to tell the job that it should be send after conversion via the default e-mail client */
	//Since we want to send an e-mail via default client, we have to enable the action first
	job.SetProfileSetting("EmailClientSettings.Enabled", "true");
	//Setting up subject of e-mail	
	job.SetProfileSetting("EmailClientSettings.Subject", "Test Mail"); 
	//Setting up a e-mail message
	job.SetProfileSetting("EmailClientSettings.Content", "Message to recipient of this e-mail."); 
	//Setting up the recipients: Several recipients are splitted by a semicolon
	job.SetProfileSetting("EmailClientSettings.Recipients", "info@someone.com;me@mywebsite.com"); 
	
	WScript.Echo("Converting under \"DefaultGuid\" conversion profile");
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