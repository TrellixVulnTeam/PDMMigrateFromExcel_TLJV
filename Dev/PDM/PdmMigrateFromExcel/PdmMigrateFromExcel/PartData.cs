using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdmMigrateFromExcel
{
    public class PartData
    {
        public string LocalPath { get; set; }

        public string DestFolderName { get; set; }

        public string Number { get; set; }

        public string PartNumbers { get; set; }

        public string Revision { get; set; }

        public string Title { get; set; }

        public string Material { get; set; }

        public string DocType { get; set; }

        public string DrawnBy { get; set; }
    }
}
