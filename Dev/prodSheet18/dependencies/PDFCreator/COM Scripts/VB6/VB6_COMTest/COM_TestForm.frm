VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   1935
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   6735
   LinkTopic       =   "Form1"
   ScaleHeight     =   1935
   ScaleWidth      =   6735
   StartUpPosition =   3  'Windows-Standard
   Begin VB.CommandButton backgroundPage_btn 
      Caption         =   "BackgroundPage"
      Height          =   615
      Index           =   1
      Left            =   4560
      TabIndex        =   4
      Top             =   1080
      Width           =   1935
   End
   Begin VB.CommandButton coverPage_btn 
      Caption         =   "CoverPage"
      Height          =   615
      Index           =   1
      Left            =   2400
      TabIndex        =   3
      Top             =   1080
      Width           =   1935
   End
   Begin VB.CommandButton mergedFiles_btn 
      Caption         =   "MergeMultipleFiles2Tif"
      Height          =   615
      Index           =   1
      Left            =   240
      TabIndex        =   2
      Top             =   1080
      Width           =   1935
   End
   Begin VB.CommandButton jpegSettings_btn 
      Caption         =   "JpegSettings"
      Height          =   615
      Index           =   1
      Left            =   3480
      TabIndex        =   1
      Top             =   240
      Width           =   1935
   End
   Begin VB.CommandButton testPage_btn 
      Caption         =   "TestPage2PDF"
      Height          =   615
      Index           =   0
      Left            =   1320
      TabIndex        =   0
      Top             =   240
      Width           =   1935
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'PDFCreator COM Interface tests for VB6
'Part of the PDFCreator application
'License: GPL
'Homepage: www.pdfforge.org/pdfcreator
'Version: 1.0.0.0
'Created: June, 16. 2015
'Modified: June, 16. 2015
'Author: Sazan Hoti
'Comments: This project demonstrates the use of the PDFCreator COM Interface.
'          There are 5 different kinds of usage presented.
'          Further usage presentation is only available in the JavaScript directory
'Note: In order to execute, first create an executable and execute this one finally.

Private Sub backgroundPage_btn_Click(Index As Integer)
Dim PDFCreatorQueue As Queue
Dim PrintJob As PrintJob
Dim ShellObj As Object
Dim fullPath As String

Set PDFCreatorQueue = CreateObject("PDFCreator.JobQueue")
fullPath = App.Path
fullPath = fullPath & "\Results\TestPage_WithBackground.pdf"
Set ShellObj = CreateObject("Shell.Application")


On Error GoTo ErrorHandler

MsgBox ("Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

MsgBox ("Printing windows test page...")
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1

If Not PDFCreatorQueue.WaitForJob(10) Then
    MsgBox ("The job didn't arrive at the queue within 10 seconds")
Else
    MsgBox ("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Getting job instance ")
    Set PrintJob = PDFCreatorQueue.NextJob
    
    PrintJob.SetProfileByGuid ("DefaultGuid")
    
    MsgBox ("Applying background page settings...")
    PrintJob.SetProfileSetting "BackgroundPage.Enabled", "True"
    PrintJob.SetProfileSetting "BackgroundPage.Repetition", "RepeatAllPages"
    PrintJob.SetProfileSetting "BackgroundPage.File", App.Path & "\FilesForTests\BackgroundPage.pdf"
    
    MsgBox ("Converting under DefaultGuid")
    PrintJob.ConvertTo (fullPath)
    
    If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
        MsgBox ("Could not convert the file: " & fullPath)
    Else
        MsgBox ("Job finished successfully.")
    End If
End If

MsgBox ("Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
Exit Sub

ErrorHandler:
MsgBox ("An error occured during the process: " & Err.Description & ". Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
End Sub

Private Sub coverPage_btn_Click(Index As Integer)
Dim PDFCreatorQueue As Queue
Dim PrintJob As PrintJob
Dim ShellObj As Object
Dim fullPath As String

Set PDFCreatorQueue = CreateObject("PDFCreator.JobQueue")
fullPath = App.Path
fullPath = fullPath & "\Results\TestPage_WithCover.pdf"
Set ShellObj = CreateObject("Shell.Application")


On Error GoTo ErrorHandler

MsgBox ("Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

MsgBox ("Printing windows test page...")
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1

If Not PDFCreatorQueue.WaitForJob(10) Then
    MsgBox ("The job didn't arrive at the queue within 10 seconds")
Else
    MsgBox ("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Getting job instance ")
    Set PrintJob = PDFCreatorQueue.NextJob
    
    PrintJob.SetProfileByGuid ("DefaultGuid")
    
    PrintJob.SetProfileSetting "CoverPage.Enabled", "True"
    PrintJob.SetProfileSetting "CoverPage.File", App.Path & "\FilesForTests\CoverPage.pdf"
    
    MsgBox ("Converting under DefaultGuid")
    PrintJob.ConvertTo (fullPath)
    
    If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
        MsgBox ("Could not convert the file: " & fullPath)
    Else
        MsgBox ("Job finished successfully.")
    End If
End If

MsgBox ("Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
Exit Sub

ErrorHandler:
MsgBox ("An error occured during the process: " & Err.Description & ". Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
End Sub

Private Sub jpegSettings_btn_Click(Index As Integer)
Dim PDFCreatorQueue As Queue
Dim PrintJob As PrintJob
Dim ShellObj As Object
Dim fullPath As String

Set PDFCreatorQueue = CreateObject("PDFCreator.JobQueue")
fullPath = App.Path
fullPath = fullPath & "\Results\TestPage_2Jpeg.pdf"
Set ShellObj = CreateObject("Shell.Application")


On Error GoTo ErrorHandler

MsgBox ("Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

MsgBox ("Printing windows test page...")
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1

If Not PDFCreatorQueue.WaitForJob(10) Then
    MsgBox ("The job didn't arrive at the queue within 10 seconds")
Else
    MsgBox ("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Getting job instance ")
    Set PrintJob = PDFCreatorQueue.NextJob
    
    PrintJob.SetProfileByGuid ("JpegGuid")
    
    'The SetProfileSetting method allows us to change the settings of a job.
    'We want 24 Bit colors for this job.
    PrintJob.SetProfileSetting "JpegSettings.Color", "Color24Bit"
    PrintJob.SetProfileSetting "JpegSettings.Quality", "100"
    
    MsgBox ("Converting under JpegGuid")
    PrintJob.ConvertTo (fullPath)
    
    If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
        MsgBox ("Could not convert the file: " & fullPath)
    Else
        MsgBox ("Job finished successfully.")
    End If
End If

MsgBox ("Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
Exit Sub

ErrorHandler:
MsgBox ("An error occured during the process: " & Err.Description & ". Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
End Sub

Private Sub mergedFiles_btn_Click(Index As Integer)
Dim PDFCreatorQueue As Queue
Dim PrintJob As PrintJob
Dim ShellObj As Object
Dim fullPath As String

Set PDFCreatorQueue = CreateObject("PDFCreator.JobQueue")
fullPath = App.Path
fullPath = fullPath & "\Results\TestPages_Merged2Tif.pdf"
Set ShellObj = CreateObject("Shell.Application")


On Error GoTo ErrorHandler

MsgBox ("Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

MsgBox ("Printing 3 windows test pages...")
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1

If Not PDFCreatorQueue.WaitForJobs(3, 15) Then
    MsgBox ("The job didn't arrive at the queue within 10 seconds")
Else
    MsgBox ("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Merging all available jobs.")
    PDFCreatorQueue.MergeAllJobs
    
    
    MsgBox ("Now there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Getting job instance ")
    Set PrintJob = PDFCreatorQueue.NextJob
    
    PrintJob.SetProfileByGuid ("DefaultGuid")
    
    MsgBox ("Setting output format to .tif")
    PrintJob.SetProfileSetting "OutputFormat", "Tif"
    
    MsgBox ("Converting under DefaultGuid")
    PrintJob.ConvertTo (fullPath)
    
    If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
        MsgBox ("Could not convert the file: " & fullPath)
    Else
        MsgBox ("Job finished successfully.")
    End If
End If

MsgBox ("Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
Exit Sub

ErrorHandler:
MsgBox ("An error occured during the process: " & Err.Description & ". Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
End Sub

Private Sub testPage_btn_Click(Index As Integer)
Dim PDFCreatorQueue As Queue
Dim PrintJob As PrintJob
Dim ShellObj As Object
Dim fullPath As String

Set PDFCreatorQueue = CreateObject("PDFCreator.JobQueue")
assemblyDir = App.Path
fullPath = assemblyDir & "\Results\TestPage_2Pdf.pdf"
Set ShellObj = CreateObject("Shell.Application")


On Error GoTo ErrorHandler

MsgBox ("Initializing PDFCreator queue...")
PDFCreatorQueue.Initialize

'Of course, you can print whatever you'd like to (make use of the PDFCreatorObj obj)
'But in order to keep the code small and clear we will only print the windows test page
MsgBox ("Printing windows test page...")
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1

If Not PDFCreatorQueue.WaitForJob(10) Then
    MsgBox ("The job didn't arrive at the queue within 10 seconds")
Else
    MsgBox ("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
    MsgBox ("Getting job instance ")
    Set PrintJob = PDFCreatorQueue.NextJob
    
    PrintJob.SetProfileByGuid ("DefaultGuid")
    
    MsgBox ("Converting under DefaultGuid")
    PrintJob.ConvertTo (fullPath)
    
    If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
        MsgBox ("Could not convert the file: " & fullPath)
    Else
        MsgBox ("Job finished successfully.")
    End If
End If

MsgBox ("Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
Exit Sub

ErrorHandler:
MsgBox ("An error occured during the process: " & Err.Description & ". Releasing the queue object.")
PDFCreatorQueue.ReleaseCom
End Sub


