using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPDM.Interop.epdm;

namespace PdmMigrateFromExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private IEdmVault5 vault1 = null;
        private IEdmVault8 vault = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private IEdmVault8 LoginVault()
        {
            try
            {
                IEdmVault5 vault1 = new EdmVault5();
                IEdmVault8 vault = (IEdmVault8)vault1;
                vault.LoginAuto("CDI Controlled Documents", 32);
                return vault;

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return this.vault;
        }

        private void btnMigrateData_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ExcelInterop excelInterop = new ExcelInterop();
            string wbPath = @"C:\Users\jevans\Desktop\Harrison\harrisonDrawingMigration.xlsx";
            List<PartData> parts = excelInterop.GetPartData(wbPath);


            listView1.Columns.Add("Drawing Number", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Title", 100, HorizontalAlignment.Left);

            
            listView1.BeginUpdate();
            //ListViewItem lvi = new ListViewItem(new string[] { });

            foreach (PartData part in parts)
            {
                //listView1.Items.Add(new ListViewItem(new string[] { part.Number, part.Title }));
                ListViewItem newItem = new ListViewItem(part.Number);
                newItem.SubItems.Add(part.Title);                
                listView1.Items.Add(newItem);
                AddFileToVault(part);
            }
            //pictureBox1.Visible = false;
            listView1.EndUpdate();
            MessageBox.Show("Done");

            this.vault = LoginVault();
        }

        public void AddFileToVault(PartData part)
        {
            if(this.vault == null)
            {
                this.vault = LoginVault();
            }

            IEdmFolder8 folder = (IEdmFolder8)vault.GetFolderFromPath(part.DestFolderName);
            IEdmFile5 file = null;

            int fileId = 0;
            int addFileStatus;
            IEdmEnumeratorVariable8 EnumVarObj = default(IEdmEnumeratorVariable8);
            string ext = Path.GetExtension(part.LocalPath);
            string newPath = part.Number + ext;

            fileId = folder.AddFile2(32, part.LocalPath, out addFileStatus, newPath);

            file = (IEdmFile5)vault.GetObject(EdmObjectType.EdmObject_File, fileId);

            EnumVarObj = (IEdmEnumeratorVariable8)file.GetEnumeratorVariable();
            

            EnumVarObj.SetVar("number", "", part.Number);
            EnumVarObj.SetVar("Part Numbers", "", part.PartNumbers);
            EnumVarObj.SetVar("Revision", "", part.Revision);
            //EnumVarObj.SetVar("nextrevision", "", part.Revision);

            EnumVarObj.SetVar("Title", "", part.Title);
            EnumVarObj.SetVar("Material", "", part.Material);
            EnumVarObj.SetVar("Document Type", "", part.DocType);
            EnumVarObj.SetVar("Drawn By", "", part.DrawnBy);

            EnumVarObj.CloseFile(false);

            file.UnlockFile(32, "File added by Jamey Evans through API.  Initial Harrison addition");
            IEdmFile10 file10 = (IEdmFile10)file;
            file10.ChangeState2("Waiting for Approval", folder.ID, "State changed through API.", 32, bsPasswd:"admin");
            file10.ChangeState2("Approved for Production", folder.ID, "State changed through API.", 32, bsPasswd:"admin");








        }
    }

    
}
