=begin comment
 PDFCreator COM Interface test for Perl
 Part of the PDFCreator application
 License: GPL
 Homepage: http://www.pdfforge.org/pdfcreator
 Perl Version: 5.2
 Version: 1.0.0.0
 Created: June, 16. 2015
 Modified: June, 16. 2015 
 Author: Sazan Hoti
 Comments: This project demonstrates the use of the COM Interface of PDFCreator.
           This script converts 3 windows testpage to one .tif file.
 Note: More usage examples then in the Perl directory can be found in the JavaScript directory only.
=cut

use strict;
use Cwd;
use Cwd 'abs_path';
use Win32::OLE;
use Tkx;        #IMPORTANT: YOU MIGHT NEED TO INSTALL THIS MODULE EXPLICITLY

my $ShellObj = Win32::OLE->new("Shell.Application");
my $PDFCreatorQueue = Win32::OLE->new("PDFCreator.JobQueue", "Quit");
my $scriptDir = cwd();
my $fullPath = $scriptDir . "/Results/TestPages_Merged2Tif.pdf";

Tkx::tk___messageBox(-message => "Initializing PDFCreator queue...");
$PDFCreatorQueue->Initialize();

Tkx::tk___messageBox(-message => "Printing 3 windows testpages");
$ShellObj->ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n \"PDFCreator\"", "", "open", 1);
$ShellObj->ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n \"PDFCreator\"", "", "open", 1);
$ShellObj->ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n \"PDFCreator\"", "", "open", 1);

Tkx::tk___messageBox(-message => "Waiting for the job to arrive at the queue...");
if (!($PDFCreatorQueue)->WaitForJobs(3,15)){
    Tkx::tk___messageBox(-message => "The print jobs did not reach the queue within 15 seconds");
    }else{
    my $txt = join "", "Currently there are ", $PDFCreatorQueue->Count(), " job(s) in the queue";
    Tkx::tk___messageBox(-message => $txt);
    Tkx::tk___messageBox(-message => "Merging all available jobs now");
    $PDFCreatorQueue->MergeAllJobs();
    
    Tkx::tk___messageBox(-message => "Now there are " . $PDFCreatorQueue->Count());
    Tkx::tk___messageBox(-message => "Getting job instance");
    my $printJob = $PDFCreatorQueue->NextJob();
    $printJob->SetProfileByGuid("DefaultGuid");
    
    Tkx::tk___messageBox(-message => "Setting OutputFormat to tif");
    $printJob->SetProfileSetting("OutputFormat", "Tif" );
    
    Tkx::tk___messageBox(-message => "Converting under \"DefaultGuid\" conversion profile but with .Tif as output format");
    $printJob->ConvertTo($fullPath);
    
    if (!($printJob->IsFinished) || !($printJob->IsSuccessful)){
		Tkx::tk___messageBox(-message => "Could not convert the file: " . $fullPath);
	}else{
        Tkx::tk___messageBox(-message => "Job finished successfully");
    }
}
Tkx::tk___messageBox(-message => "Releasing the object");
$PDFCreatorQueue->ReleaseCom();