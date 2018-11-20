// TestPage2PDFAsync.js script
// Part of PDFCreator
// License: GPL
// Homepage: http://www.pdfforge.org/pdfcreator
// Version: 1.0.0.0
// Date: July, 28. 2014
// Author: Sazan Hoti
// Comments: This script shows how to get the names of all printer devices of PDFCreator
//			 using the COM interface 


var PDFCreator = new ActiveXObject("PDFCreator.PDFCreatorObj");

var printers = PDFCreator.GetPDFCreatorPrinters();
var i = 0;
var allPrinters = "";

if(PDFCreator.IsInstanceRunning)
{
WScript.Echo(PDFCreator.IsInstanceRunning);
}

while(i < printers.Count)
{
    allPrinters += "\n" + printers.GetPrinterByIndex(i);
	i++;
}

WScript.Echo(allPrinters);


