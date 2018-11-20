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
           This script converts a windows testpage while adding a cover to it.
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
coverPage = scriptDir + "\FilesForTests\CoverPage.pdf"
fullPath = scriptDir + "\Results\TestPage_WithCover.pdf" 

mbox.showinfo("","Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize()

mbox.showinfo("","Printing a windows testpage")
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)

mbox.showinfo("","Waiting for the job to arrive at the queue...")
if(not PDFCreatorQueue.WaitForJob(10)):
    mbox.showinfo("","The print job did not reach the queue within 10 seconds")
else:
    mbox.showinfo("","Currently there are " + str(PDFCreatorQueue.Count) + " job(s) in the queue")
    mbox.showinfo("","Getting job instance")
    printJob = PDFCreatorQueue.NextJob
    
    printJob.SetProfileByGuid("DefaultGuid") #Notice that we are converting under JpegGuid
    
    printJob.SetProfileByGuid("DefaultGuid")
    
    mbox.showinfo("","Applying cover page settings...")
    #The SetProfileSettings method allows us to tell the job that it should add a cover to the first page
	#Since we want to add a cover page, we have to enable it first
    
    printJob.SetProfileSetting("CoverPage.Enabled", "true")
    printJob.SetProfileSetting("CoverPage.File", coverPage)
    
    #Notice that the first parameter here says that we want to cover a file and the second specifies the file. It is very important that
	#the file is in pdf-Format otherwise exceptions will occur
    
    mbox.showinfo("","Converting under ""DefaultGuid"" conversion profile")
    printJob.ConvertTo(fullPath)
    
    if(not printJob.IsFinished or not printJob.IsSuccessful):
	    mbox.showinfo("","Could not convert the file: " + fullPath)
    else:
	    mbox.showinfo("","Job finished successfully")
mbox.showinfo("","Releasing the object")
PDFCreatorQueue.ReleaseCom()