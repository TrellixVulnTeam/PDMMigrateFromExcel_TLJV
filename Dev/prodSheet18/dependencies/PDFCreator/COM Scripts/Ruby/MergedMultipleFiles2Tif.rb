=begin
 PDFCreator COM Interface test for Ruby
 Part of the PDFCreator application
 License: GPL
 Homepage: http://www.pdfforge.org/pdfcreator
 Ruby Version: 2.1.6
 Version: 1.0.0.0
 Created: June, 16. 2015
 Modified: June, 16. 2015 
 Author: Sazan Hoti
 Comments: This project demonstrates the use of the COM Interface of PDFCreator.
           This script converts 3 windows testpage to one .tif file.
 Note: More usage examples then in the Ruby directory can be found in the JavaScript directory only.
=end

require 'win32ole'
require 'tk'

ShellObj = WIN32OLE.new("Shell.Application")
PDFCreatorQueue = WIN32OLE.new("PDFCreator.JobQueue")
scriptDir = File.expand_path(File.dirname(__FILE__))
fullPath = scriptDir + "/Results/TestPages_Merged2Tif.pdf " 

Tk.messageBox('message' =>  "Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

Tk.messageBox('message' =>  "Printing 3 windows testpages...")
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)

Tk.messageBox('message' =>  "Waiting for the 3 jobs to arrive at the queue...")
if (!PDFCreatorQueue.WaitForJobs(3,15))
    Tk.messageBox('message' =>  "The 3 print jobs did not reach the queue within 15 seconds")
else 
    Tk.messageBox('message' =>  "Currently there are " + (PDFCreatorQueue.Count).to_s + " job(s) in the queue")
    Tk.messageBox('message' =>  "Merging all available jobs now")
    PDFCreatorQueue.MergeAllJobs
    
    Tk.messageBox('message' =>  "Now there are " + (PDFCreatorQueue.Count).to_s + " job(s) in the queue")
    Tk.messageBox('message' =>  "Getting job instance")
    printJob = PDFCreatorQueue.NextJob
    
    printJob.SetProfileByGuid("DefaultGuid")
    
    Tk.messageBox('message' =>  "Setting OutputFormat to tif")
    printJob.SetProfileSetting("OutputFormat", "Tif")
    
    Tk.messageBox('message' => "Converting under ""DefaultGuid"" conversion profile but with .Tif as output format")
    printJob.ConvertTo(fullPath)
    
    if (!printJob.IsFinished || !printJob.IsSuccessful)
		Tk.messageBox('message' =>  "Could not convert the file: " + fullPath)
	else
		Tk.messageBox('message' =>  "Job finished successfully")
    end
end
Tk.messageBox('message' =>  "Releasing the object")
PDFCreatorQueue.ReleaseCom