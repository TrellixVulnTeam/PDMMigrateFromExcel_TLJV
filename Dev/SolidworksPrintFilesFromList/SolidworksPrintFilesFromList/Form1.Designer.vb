<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.primaryButton = New System.Windows.Forms.Button()
        Me.partNumberTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.resultFileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewFilePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewFileState = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'primaryButton
        '
        Me.primaryButton.Location = New System.Drawing.Point(236, 144)
        Me.primaryButton.Name = "primaryButton"
        Me.primaryButton.Size = New System.Drawing.Size(342, 139)
        Me.primaryButton.TabIndex = 0
        Me.primaryButton.Text = "Start Program"
        Me.primaryButton.UseVisualStyleBackColor = True
        '
        'partNumberTextBox
        '
        Me.partNumberTextBox.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.partNumberTextBox.Location = New System.Drawing.Point(197, 28)
        Me.partNumberTextBox.Name = "partNumberTextBox"
        Me.partNumberTextBox.Size = New System.Drawing.Size(277, 22)
        Me.partNumberTextBox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Enter Part Number Variable"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.resultFileName, Me.listViewFilePath, Me.listViewFileState})
        Me.ListView1.Location = New System.Drawing.Point(181, 314)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(513, 114)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'resultFileName
        '
        Me.resultFileName.Text = "File Name"
        '
        'listViewFilePath
        '
        Me.listViewFilePath.Text = "File Path"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(588, 74)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 55)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.partNumberTextBox)
        Me.Controls.Add(Me.primaryButton)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents primaryButton As Button
    Friend WithEvents partNumberTextBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents resultFileName As ColumnHeader
    Friend WithEvents listViewFilePath As ColumnHeader
    Friend WithEvents listViewFileState As ColumnHeader
    Friend WithEvents Button1 As Button
End Class
