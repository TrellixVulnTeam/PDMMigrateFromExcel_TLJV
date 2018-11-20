'''
 PDFCreator COM Interface test for Python
 Part of the PDFCreator application
 License: GPL
 Homepage: http://www.pdfforge.org/pdfcreator
 Python Version: 3.4
 Version: 1.0.0.0
 Created: June, 16. 2015
 Modified: June, 16. 2015 
 Author: Sazan Hoti
 Comments: This project demonstrates the use of the COM Interface of PDFCreator.
           This script converts 3 windows testpage to one .tif file.
 Note: More usage examples then in the Python directory can be found in the JavaScript directory only.
'''


#for COM support
import os
import win32com.client as w32c
import ctypes
import tkinter.messagebox as mbox #IMPORTANT: CHECK IF tkinter MODULE IST INSTALLED! Command: python -m tkinter
    
ShellObj = w32c.Dispatch("Shell.Application")
PDFCreatorQueue = w32c.Dispatch("PDFCreator.JobQueue")
scriptDir = os.path.dirname(os.path.realpath(__file__))
fullPath = scriptDir + "\Results\TestPages_Merged2Tif.pdf" 

mbox.showinfo("","Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize()

mbox.showinfo("","Printing 3 windows testpages...")
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)

mbox.showinfo("","Waiting for the 3 jobs to arrive at the queue...")
if(not PDFCreatorQueue.WaitForJobs(3, 15)):
    mbox.showinfo("","The print jobs did not reach the queue within 15 seconds")
else:
    mbox.showinfo("","Currently there are " + str(PDFCreatorQueue.Count) + " job(s) in the queue")
    mbox.showinfo("","Merging all available jobs now")
    PDFCreatorQueue.MergeAllJobs()
    
    mbox.showinfo("","Now there are " + str(PDFCreatorQueue.Count) + " job(s) in the queue")
    mbox.showinfo("","Getting job instance")
    printJob = PDFCreatorQueue.NextJob
    
    printJob.SetProfileByGuid("DefaultGuid")
    
    mbox.showinfo("","Setting OutputFormat to tif")
    printJob.SetProfileSetting("OutputFormat", "Tif") 
    
    mbox.showinfo("","Converting under ""DefaultGuid"" conversion profile but with .Tif as output format")
    printJob.ConvertTo(fullPath)
    
    if(not printJob.IsFinished or not printJob.IsSuccessful):
	    mbox.showinfo("","Could not convert the file: " + fullPath)
    else:
	    mbox.showinfo("","Job finished successfully")
mbox.showinfo("","Releasing the object")
PDFCreatorQueue.ReleaseCom()