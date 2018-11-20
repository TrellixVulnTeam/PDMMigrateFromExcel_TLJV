Imports Microsoft.Office.Interop
Imports EPDM.Interop.epdm
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports eDrawings.Interop.EModelViewControl
Imports System.Runtime.InteropServices
Imports System
Imports System.Diagnostics

Public Class Form1
    Dim vault As IEdmVault5

    Dim objApp As Excel.Application
    Dim objBook As Excel._Workbook
    Dim partNo As String
    Dim search As IEdmSearch5



    Dim swModelDoc As ModelDoc2

    Dim swApp As SldWorks = New SldWorks




    Private Sub PrimaryButton_Click(sender As Object, e As EventArgs) Handles primaryButton.Click
        'Call CreateNewWorkbook()
        Call PdmSub()
        MsgBox("Done")
        'Close()
    End Sub

    Sub CreateNewWorkbook()
        Dim objBooks As Excel.Workbooks
        Dim objSheets As Excel.Sheets
        Dim objSheet As Excel._Worksheet
        Dim range As Excel.Range

        objApp = New Excel.Application()
        objBooks = objApp.Workbooks
        objBook = objBooks.Add
        objSheets = objBook.Worksheets
        objSheet = objSheets(1)

        range = objSheet.Range("A1")
        range.Value = "Test"

        'Return control to user
        objApp.Visible = True
        objApp.UserControl = True

        'Clean up
        range = Nothing
        objSheet = Nothing
        objSheets = Nothing
        objBooks = Nothing
    End Sub
    Sub CreateVaultLogin()
        vault = New EdmVault5
        vault.LoginAuto("Cdi Controlled Documents", Me.Handle.ToInt32)
    End Sub

    Sub PdmSub()
        vault = New EdmVault5
        vault.LoginAuto("Cdi Controlled Documents", Me.Handle.ToInt32)

        Dim vault7 As IEdmVault7 = Nothing
        vault7 = DirectCast(vault, IEdmVault7)

        'Dim search As IEdmSearch5  made global
        Dim colResults As New Dictionary(Of Integer, IEdmSearchResult5)
        Dim file As IEdmFile5

        'PartNo = "705-%"
        If partNo = vbNullString Then
            MsgBox("No part number detected")
            Exit Sub
        End If
        search = vault.CreateSearch
        search.AddVariable("Number", "%" & PartNo)
        'search.State = "Approved for Production"
        search.FileName = "%.slddrw%"
        search.FindHistoricStates = False
        Dim srchResult As IEdmSearchResult5
        srchResult = search.GetFirstResult

        While Not srchResult Is Nothing
            'Debug.Print(srchResult.Name & " : " & srchResult.Path & " : " & srchResult.ID)
            If Not colResults.ContainsKey(srchResult.ID) Then
                colResults.Add(srchResult.ID, srchResult)
            End If
            srchResult = search.GetNextResult
        End While

        Dim arrValidPaths As New List(Of String)

        For Each kvp As KeyValuePair(Of Integer, IEdmSearchResult5) In colResults
            Debug.Print(colResults(kvp.Key).Name)
            arrValidPaths.Add(colResults(kvp.Key).Path)

            Dim bg As IEdmBatchGet
            Dim GetDirectory() As EdmSelItem
            ReDim GetDirectory(0)
            bg = vault.CreateUtility(EdmUtility.EdmUtil_BatchGet)
            GetDirectory(0).mlDocID = colResults(kvp.Key).ID
            GetDirectory(0).mlProjID = colResults(kvp.Key).ParentFolderID

            'if you get an "E_INVALIDARG" error make sure "Embed Interop Types" is set to false in EPDM.Interop.epdm reference
            bg.AddSelection(vault, GetDirectory)
            bg.CreateTree(0, EdmGetCmdFlags.Egcf_Nothing)
            bg.GetFiles(0, Nothing)
            'If Not bg.WaitForExit(60) Then
            'Debug.Print("Timed Out")
            'End If


        Next kvp

        For Each validPath In arrValidPaths
            OpenFile2(validPath)
            Debug.Print(validPath)
        Next
    End Sub
    Sub OpenFile(filePath As String)
        'Opens file with SOlidworks
        Dim errors As Integer
        Dim warnings As Integer
        swModelDoc = swApp.OpenDoc6(filePath, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_ReadOnly, "", errors, warnings)
        swApp.CreateNewWindow()

    End Sub

    Sub OpenFileEDwg(filePath As String)
        Dim eModelView As EModelViewApp = New EModelViewApp
        'Dim eModelControl As EModelViewControl = New EModelViewControl
        'eModelControl.OpenDoc(filePath, False, False, True, "")
        eModelView.OpenFile(filePath, "User", "Password", 3)
    End Sub
    Function OpenFile2(DocName As String) As Long
        'Opens file with default viewer
        'Dim proc As New System.Diagnostics.Process()
        Process.Start("C:\Program Files\SOLIDWORKS Corp\eDrawings\eDrawings.exe" & DocName)
        'proc = Process.Start(DocName, "C:\Program Files\SOLIDWORKS Corp\eDrawings\eDrawings.exe")
    End Function



    Private Sub PartNumberTextBox_TextChanged(sender As Object, e As EventArgs) Handles partNumberTextBox.TextChanged
        partNo = partNumberTextBox.Text
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class




