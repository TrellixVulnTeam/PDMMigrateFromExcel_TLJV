Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.Marshal
Imports SldWorks
Imports SwConst
Imports SwCommands
Imports EdmLib

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Call main()
    End Sub


    Sub main()
        Dim swApp As SldWorks.SldWorks
        Dim swModel As SldWorks.ModelDoc2
        Dim swSelMgr As SldWorks.SelectionMgr
        Dim swSelObj As Object
        Dim swFeat As SldWorks.Feature
        Dim swEnt As SldWorks.Entity
        Dim swBody As SldWorks.Body2
        Dim swSelComp As SldWorks.Component2
        Dim swSelModel As SldWorks.ModelDoc2
        Dim nSelType As Long
        Dim sFeatName As String
        Dim bRet As Boolean
        ' Disables Visual Basic's implicit error on QueryInterface
        On Error Resume Next
        swApp = CreateObject("SldWorks.Application")
        swModel = swApp.ActiveDoc
        swSelMgr = swModel.SelectionManager
        ' Could either have a feature or entity selected
        ' Do not try to get entity directly
        ' from feature because feature might be Nothing
        ' Instead, use ISelectionMgr
        swFeat = swSelMgr.GetSelectedObject5(1)
        swEnt = swSelMgr.GetSelectedObject5(1)
        swBody = swSelMgr.GetSelectedObject5(1)
        swSelObj = swSelMgr.GetSelectedObject5(1)
        swSelComp = swSelMgr.GetSelectedObjectsComponent2(1)
        Debug.Print("Selected Type      = " & swSelMgr.GetSelectedObjectType2(1))
        If Not swFeat Is Nothing Then
            Debug.Print("Feature            = " + swFeat.Name + " [" + swFeat.GetTypeName + "]")
        End If
        If Not swBody Is Nothing Then
            Debug.Print("  Body selected")
        End If
        If swFeat Is Nothing And swEnt Is Nothing And swBody Is Nothing And Not swSelObj Is Nothing Then
            Debug.Print("  Unknown object")
        End If
        ' Could not get component from ISelectionMgr,
        ' so try and get component through IEntity
        If swSelComp Is Nothing Then
            swSelComp = swEnt.GetComponent
        End If
        If Not swSelComp Is Nothing Then
            swSelModel = swSelComp.GetModelDoc
            Debug.Print("Component Name     = " + swSelComp.Name2)
            Debug.Print("Model Path         = " + swSelModel.GetPathName)
        End If

    End Sub
End Class
