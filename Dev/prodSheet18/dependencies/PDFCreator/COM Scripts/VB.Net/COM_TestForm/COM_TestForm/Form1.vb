'PDFCreator COM Interface tests for C#
'Part of the PDFCreator application
'License: GPL
'Homepage: http://www.pdfforge.org/pdfcreator
'.Net Framework: 4.0
'Version: 1.0.0.0
'Created: June, 16. 2015
'Modified: June, 16. 2015
'Author: Sazan Hoti
'Comments: This project demonstrates the use of the COM Interface of PDFCreator.
'           There are 5 different kinds of usage presented.
'           Further usage presentation is only available in the JavaScript directory.
'Note: When executed in release mode paths have to be modified.

' IMPORTANT: Add a reference to the pdfcreator.exe
Imports System.IO
Imports pdfforge.PDFCreator.UI.ComWrapper

Public Class Form1

    Private Sub testPage_btn_Click(sender As Object, e As EventArgs) Handles testPage_btn.Click
        Dim PDFCreatorQueue As Queue = New Queue
        Dim job As Object
        Dim fullPath As String

        Try

            fullPath = Path.Combine(Path.GetTempPath, Path.GetTempFileName)
            MsgBox("Initializing PDFCreator queue...")
            PDFCreatorQueue.Initialize()

            MsgBox("Printing windows test page...")
            printFile()

            If Not PDFCreatorQueue.WaitForJob(10) Then
                MsgBox("The print job did not reach the queue within " & " 10 seconds")
            Else
                MsgBox("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Getting job instance")
                job = PDFCreatorQueue.NextJob

                job.SetProfileByGuid("DefaultGuid")

                MsgBox("Converting under ""DefaultGuid"" conversion profile")
                job.ConvertTo(fullPath)

                If (Not job.IsFinished Or Not job.IsSuccessful) Then
                    MsgBox("Could not convert the file: " & fullPath)
                Else
                    MsgBox("Job finished successfully")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Original error: " & Ex.Message)
        Finally
            MsgBox("Releasing the queue object")
            PDFCreatorQueue.ReleaseCom()
        End Try

    End Sub

    Private Sub jpegSettings_btn_Click(sender As Object, e As EventArgs) Handles jpegSettings_btn.Click
        Dim oJobQueue As Type = Type.GetTypeFromProgID("PDFCreator.JobQueue")
        Dim PDFCreatorQueue As Object = Activator.CreateInstance(oJobQueue)
        Dim PrintJob As Object
        Dim fullPath As String

        Try
            fullPath = Path.Combine(Path.GetTempPath, Path.GetTempFileName)

            MsgBox("Initializing PDFCreator queue...")
            PDFCreatorQueue.Initialize()

            MsgBox("Printing windows test page...")
            printFile()

            If Not PDFCreatorQueue.WaitForJob(10) Then
                MsgBox("The print job did not reach the queue within " & " 10 seconds")
            Else
                MsgBox("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Getting job instance")
                PrintJob = PDFCreatorQueue.NextJob

                PrintJob.SetProfileByGuid("JpegGuid") 'Notice that we changed the profile

                'The SetProfileSettings method allows us to change the JpegSettings of the job
                'We want 24 bit colors for our converted file
                PrintJob.SetProfileSetting("JpegSettings.Color", "Color24Bit")
                PrintJob.SetProfileSetting("JpegSettings.Quality", "100")

                MsgBox("Converting under ""JpegGuid"" conversion profile")
                PrintJob.ConvertTo(fullPath)

                If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
                    MsgBox("Could not convert the file: " & fullPath)
                Else
                    MsgBox("Job finished successfully")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Original error: " & Ex.Message)
        Finally
            MsgBox("Releasing the queue object")
            PDFCreatorQueue.ReleaseCom()
        End Try

    End Sub

    Private Sub mergedFiles_btn_Click(sender As Object, e As EventArgs) Handles mergedFiles_btn.Click
        Dim oJobQueue As Type = Type.GetTypeFromProgID("PDFCreator.JobQueue")
        Dim PDFCreatorQueue As Object = Activator.CreateInstance(oJobQueue)
        Dim PrintJob As Object
        Dim fullPath As String

        Try

            fullPath = Path.Combine(Path.GetTempPath, Path.GetTempFileName)

            MsgBox("Initializing PDFCreator queue...")
            PDFCreatorQueue.Initialize()

            MsgBox("Printing 3 windows test pages...")
            printFile()
            printFile()
            printFile()

            If Not PDFCreatorQueue.WaitForJobs(3, 15) Then
                MsgBox("The print jobs did not reach the queue within 15 seconds")
            Else
                MsgBox("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Merging all available jobs")
                PDFCreatorQueue.MergeAllJobs()

                MsgBox("Now there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Getting job instance")
                PrintJob = PDFCreatorQueue.NextJob

                PrintJob.SetProfileByGuid("DefaultGuid")

                'We change the output format of the DefaultGuid to Tif only for these jobs
                MsgBox("Setting OutputFormat to tif")
                PrintJob.SetProfileSetting("OutputFormat", "Tif")

                MsgBox("Converting under ""DefaultGuid"" conversion profile but with .Tif as output format")
                PrintJob.ConvertTo(fullPath)

                If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
                    MsgBox("Could not convert the file: " & fullPath)
                Else
                    MsgBox("Job finished successfully")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Original error: " & Ex.Message)
        Finally
            MsgBox("Releasing the queue object")
            PDFCreatorQueue.ReleaseCom()
        End Try
        ' End If
    End Sub

    Private Sub coverPage_btn_Click(sender As Object, e As EventArgs) Handles coverPage_btn.Click
        Dim oJobQueue As Type = Type.GetTypeFromProgID("PDFCreator.JobQueue")
        Dim PDFCreatorQueue As Object = Activator.CreateInstance(oJobQueue)
        Dim PrintJob As Object
        Dim assemblyDir, fullPath As String

        Try
            assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            assemblyDir = assemblyDir.Replace("\bin\Debug", "\Results")
            fullPath = Path.Combine(assemblyDir, "TestPage_WithBackground.pdf")

            MsgBox("Initializing PDFCreator queue...")
            PDFCreatorQueue.Initialize()

            MsgBox("Printing windows test page...")
            printFile()

            If Not PDFCreatorQueue.WaitForJob(10) Then
                MsgBox("The print job did not reach the queue within " & " 10 seconds")
            Else
                MsgBox("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Getting job instance")
                PrintJob = PDFCreatorQueue.NextJob

                PrintJob.SetProfileByGuid("DefaultGuid")

                MsgBox("Applying cover page settings...")

                PrintJob.SetProfileSetting("CoverPage.Enabled", "true")
                PrintJob.SetProfileSetting("CoverPage.File", Path.Combine(assemblyDir, "\\FilesForTests\\CoverPage.pdf"))

                MsgBox("Converting under ""DefaultGuid"" conversion profile")
                PrintJob.ConvertTo(fullPath)

                If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
                    MsgBox("Could not convert the file: " & fullPath)
                Else
                    MsgBox("Job finished successfully")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Original error: " & Ex.Message)
        Finally
            MsgBox("Releasing the queue object")
            PDFCreatorQueue.ReleaseCom()
        End Try
    End Sub

    Private Sub backgroundPage_btn_Click(sender As Object, e As EventArgs) Handles backgroundPage_btn.Click
        Dim oJobQueue As Type = Type.GetTypeFromProgID("PDFCreator.JobQueue")
        Dim PDFCreatorQueue As Object = Activator.CreateInstance(oJobQueue)
        Dim PrintJob As Object
        Dim assemblyDir, fullPath As String

        Try
            assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            assemblyDir = assemblyDir.Replace("\bin\Debug", "\Results")
            fullPath = Path.Combine(assemblyDir, "TestPage_WithBackground.pdf")

            MsgBox("Initializing PDFCreator queue...")
            PDFCreatorQueue.Initialize()

            MsgBox("Printing windows test page...")
            printFile()

            If Not PDFCreatorQueue.WaitForJob(10) Then
                MsgBox("The print job did not reach the queue within " & " 10 seconds")
            Else
                MsgBox("Currently there are " & PDFCreatorQueue.Count & " job(s) in the queue")
                MsgBox("Getting job instance")
                PrintJob = PDFCreatorQueue.NextJob

                PrintJob.SetProfileByGuid("DefaultGuid")

                MsgBox("Applying background page settings...")

                PrintJob.SetProfileSetting("BackgroundPage.Enabled", "true")
                PrintJob.SetProfileSetting("BackgroundPage.Repetition", "RepeatAllPages")
                PrintJob.SetProfileSetting("BackgroundPage.File", Path.Combine(assemblyDir, "\\FilesForTests\\BackgroundPage.pdf"))

                MsgBox("Converting under ""DefaultGuid"" conversion profile")
                PrintJob.ConvertTo(fullPath)

                If (Not PrintJob.IsFinished Or Not PrintJob.IsSuccessful) Then
                    MsgBox("Could not convert the file: " & fullPath)
                Else
                    MsgBox("Job finished successfully")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("Original error: " & Ex.Message)
        Finally
            MsgBox("Releasing the queue object")
            PDFCreatorQueue.ReleaseCom()
        End Try
    End Sub

    'In order to keep the code small and clear, we'll only print the windows test page.
    'But you could also use the PDFCreatorObj object to print any file that you want.
    'Just select your file, pass the complete file path to PDFCreatorObj.PrintFile(filePath)
    'and it will be ready for conversion.
    Private Sub printFile()
        Dim ShellObj As Object

        ShellObj = CreateObject("Shell.Application")
        ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""PDFCreator""", "", "open", 1)
    End Sub

End Class
