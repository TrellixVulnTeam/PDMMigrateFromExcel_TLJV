/*
 * PDFCreator COM Interface tests for C#
 * Part of the PDFCreator application
 * License: GPL
 * Homepage: http://www.pdfforge.org/pdfcreator
 * .Net Framework: 4.0
 * Version: 1.0.0.0
 * Created: June, 16. 2015
 * Modified: June, 16. 2015
 * Author: Sazan Hoti
 * Comments: This project demonstrates the use of the COM Interface of PDFCreator.
             There are 5 different kinds of usage presented.
             Further usage presentation is only available in the JavaScript directory.
 * Note: When executed in release mode paths have to be modified.
 */

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using pdfforge.PDFCreator.UI.ComWrapper;

//IMPORTANT: Add a reference to the PDFCreator.ComWrapper.dll

namespace COM_TestForm
{
    public partial class Form1 : Form
    {
        private bool _isTypeInitialized;

        public Form1()
        {
            InitializeComponent();
        }

        private Queue CreateQueue()
        {
            // This needs to be done once to make the ComWrapper work reliably.
            if (!_isTypeInitialized)
            {
                Type queueType = Type.GetTypeFromProgID("PDFCreator.JobQueue");
                Activator.CreateInstance(queueType);
                _isTypeInitialized = true;
            }

            return new Queue();
        }

        private void testPage_btn_Click(object sender, EventArgs e)
        {
            var jobQueue = CreateQueue();

            var assemblyDir = Assembly.GetExecutingAssembly().Location;
            var resultsDir = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\Results");
            Directory.CreateDirectory(resultsDir);
            var convertedFilePath = Path.Combine(resultsDir, "TestPage_2PDF.pdf");

            try
            {
                MessageBox.Show("Initializing the job queue");
                jobQueue.Initialize();

                MessageBox.Show("Printing windows test page...");
                PrintWindowsTestPage();

                if (!jobQueue.WaitForJob(10))
                {
                    MessageBox.Show("The job didn't arrive within 10 seconds");
                }
                else
                {
                    MessageBox.Show("Currently there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Getting job instance");
                    var printJob = jobQueue.NextJob;

                    printJob.SetProfileByGuid("DefaultGuid");

                    MessageBox.Show("Converting under DefaultGuid");
                    printJob.ConvertTo(convertedFilePath);

                    if (!printJob.IsFinished || !printJob.IsSuccessful)
                    {
                        MessageBox.Show("Could not convert: ");
                    }
                    else
                    {
                        MessageBox.Show("The conversion was succesful!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occured: " + err.Message);
            }
            finally
            {
                MessageBox.Show("Releasing the queue object");
                jobQueue.ReleaseCom();
            }
        }

        private void PrintWindowsTestPage()
        {
            Type shellObj = Type.GetTypeFromProgID("Shell.Application");
            dynamic shellInst = Activator.CreateInstance(shellObj);

            shellInst.ShellExecute("RUNDLL32.exe", "PRINTUI,PrintUIEntry /k /n \"PDFCreator\"", "", "open", 1);
        }

        /**
         * This method will convert to jpg using late binding, i.e. no reference to pdfcreator.exe is necessary for the below method.
         */

        private void jpegSettings_btn_Click(object sender, EventArgs e)
        {
            var jobQueue = CreateQueue();

            //The rest is still the same as with early binding
            var assemblyDir = Assembly.GetExecutingAssembly().Location;
            var resultsDir = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\Results");
            Directory.CreateDirectory(resultsDir);
            var convertedFilePath = Path.Combine(resultsDir, "TestPage_2Jpeg.jpg");

            try
            {
                MessageBox.Show("Initializing the job queue");
                jobQueue.Initialize();

                MessageBox.Show("Printing windows test page...");
                PrintWindowsTestPage();

                if (!jobQueue.WaitForJob(10))
                {
                    MessageBox.Show("The job didn't arrive within 10 seconds");
                }
                else
                {
                    MessageBox.Show("Currently there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Getting job instance");
                    var printJob = jobQueue.NextJob;

                    printJob.SetProfileByGuid("JpegGuid");

                    MessageBox.Show("Applying jpeg settings");
                    printJob.SetProfileSetting("JpegSettings.Color", "Color24Bit");
                    printJob.SetProfileSetting("JpegSettings.Quality", "100");

                    MessageBox.Show("Converting under JpegGuid");
                    printJob.ConvertTo(convertedFilePath);

                    if (!printJob.IsFinished || !printJob.IsSuccessful)
                    {
                        MessageBox.Show("Could not convert: " + convertedFilePath);
                    }
                    else
                    {
                        MessageBox.Show("The conversion was succesful!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occured: " + err.Message);
            }
            finally
            {
                MessageBox.Show("Releasing the queue object");
                jobQueue.ReleaseCom();
            }
        }

        private void mergedFiles_btn_Click(object sender, EventArgs e)
        {
            var jobQueue = CreateQueue();

            var assemblyDir = Assembly.GetExecutingAssembly().Location;
            var resultsDir = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\Results");
            Directory.CreateDirectory(resultsDir);
            var fullPath = Path.Combine(resultsDir, "TestPage_Merged2Tif.pdf");

            try
            {
                MessageBox.Show("Initializing the job queue");
                jobQueue.Initialize();

                MessageBox.Show("Printing 3 windows test pages...");
                PrintWindowsTestPage();
                PrintWindowsTestPage();
                PrintWindowsTestPage();

                if (!jobQueue.WaitForJobs(3, 15))
                {
                    MessageBox.Show("The jobs didn't arrive within 15 seconds");
                }
                else
                {
                    MessageBox.Show("Currently there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Merging all available jobs");
                    jobQueue.MergeAllJobs();

                    MessageBox.Show("Now there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Getting job instance");
                    var printJob = jobQueue.NextJob;

                    printJob.SetProfileByGuid("DefaultGuid");

                    //Change the output format of the current conversion profile
                    //to .tif - this holds only for this job
                    printJob.SetProfileSetting("OutputFormat", "Tif");

                    MessageBox.Show("Converting under DefaultGuid but with .tif as output format");
                    printJob.ConvertTo(fullPath);

                    if (!printJob.IsFinished || !printJob.IsSuccessful)
                    {
                        MessageBox.Show("Could not convert: ");
                    }
                    else
                    {
                        MessageBox.Show("The conversion was succesful!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occured: " + err.Message);
            }
            finally
            {
                MessageBox.Show("Releasing the queue object");
                jobQueue.ReleaseCom();
            }
        }

        private void coverPage_btn_Click(object sender, EventArgs e)
        {
            var jobQueue = CreateQueue();

            var assemblyDir = Assembly.GetExecutingAssembly().Location;
            var resultsDir = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\Results");
            Directory.CreateDirectory(resultsDir);
            var coverPagePath = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\FilesForTests\\CoverPage.pdf");
            var convertedFilePath = Path.Combine(resultsDir, "TestPage_WithCover.pdf");

            try
            {
                MessageBox.Show("Initializing the job queue");
                jobQueue.Initialize();

                MessageBox.Show("Printing windows test page...");
                PrintWindowsTestPage();

                if (!jobQueue.WaitForJob(10))
                {
                    MessageBox.Show("The job didn't arrive within 10 seconds");
                }
                else
                {
                    MessageBox.Show("Currently there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Getting job instance");
                    var printJob = jobQueue.NextJob;

                    printJob.SetProfileByGuid("DefaultGuid");

                    MessageBox.Show("Applying cover page settings");
                    printJob.SetProfileSetting("CoverPage.Enabled", "True");
                    printJob.SetProfileSetting("CoverPage.File", coverPagePath);

                    MessageBox.Show("Converting under DefaultGuid");
                    printJob.ConvertTo(convertedFilePath);

                    if (!printJob.IsFinished || !printJob.IsSuccessful)
                    {
                        MessageBox.Show("Could not convert: ");
                    }
                    else
                    {
                        MessageBox.Show("The conversion was succesful!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occured: " + err.Message);
            }
            finally
            {
                MessageBox.Show("Releasing the queue object");
                jobQueue.ReleaseCom();
            }
        }

        private void backgroundPage_btn_Click(object sender, EventArgs e)
        {
            var jobQueue = CreateQueue();

            var assemblyDir = Assembly.GetExecutingAssembly().Location;
            var resultsDir = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\Results");
            Directory.CreateDirectory(resultsDir);
            var bkgroundPagePath = assemblyDir.Replace("\\bin\\Debug\\COM_TestForm.exe", "\\FilesForTests\\BackgroundPage.pdf");
            var fullPath = Path.Combine(resultsDir, "TestPage_WithBackground.pdf");

            try
            {
                MessageBox.Show("Initializing the job queue");
                jobQueue.Initialize();

                MessageBox.Show("Printing windows test page...");
                PrintWindowsTestPage();

                if (!jobQueue.WaitForJob(10))
                {
                    MessageBox.Show("The job didn't arrive within 10 seconds");
                }
                else
                {
                    MessageBox.Show("Currently there are " + jobQueue.Count + " job(s) in the queue");
                    MessageBox.Show("Getting job instance");
                    var printJob = jobQueue.NextJob;

                    printJob.SetProfileByGuid("DefaultGuid");

                    MessageBox.Show("Applying background page settings");
                    printJob.SetProfileSetting("BackgroundPage.Enabled", "True");
                    printJob.SetProfileSetting("BackgroundPage.Repetition", "RepeatAllPages");
                    printJob.SetProfileSetting("BackgroundPage.File", bkgroundPagePath);

                    MessageBox.Show("Converting under DefaultGuid");
                    printJob.ConvertTo(fullPath);

                    if (!printJob.IsFinished || !printJob.IsSuccessful)
                    {
                        MessageBox.Show("Could not convert: ");
                    }
                    else
                    {
                        MessageBox.Show("The conversion was succesful!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occured: " + err.Message);
            }
            finally
            {
                MessageBox.Show("Releasing the queue object");
                jobQueue.ReleaseCom();
            }
        }
    }
}
