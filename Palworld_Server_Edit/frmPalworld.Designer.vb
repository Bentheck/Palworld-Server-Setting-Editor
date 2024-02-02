<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        txtServLoc = New TextBox()
        lblServLoc = New Label()
        btnLoadServLoc = New Button()
        rtbServInfo = New RichTextBox()
        SuspendLayout()
        ' 
        ' txtServLoc
        ' 
        txtServLoc.Location = New Point(259, 22)
        txtServLoc.Name = "txtServLoc"
        txtServLoc.Size = New Size(472, 23)
        txtServLoc.TabIndex = 0
        txtServLoc.Text = "C:\Program Files (x86)\Steam\steamapps\common\PalServer"
        ' 
        ' lblServLoc
        ' 
        lblServLoc.AutoSize = True
        lblServLoc.BackColor = SystemColors.ControlDark
        lblServLoc.BorderStyle = BorderStyle.Fixed3D
        lblServLoc.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblServLoc.Location = New Point(3, 22)
        lblServLoc.Name = "lblServLoc"
        lblServLoc.Size = New Size(250, 23)
        lblServLoc.TabIndex = 1
        lblServLoc.Text = "Server Location(PalServer Folder) :"
        ' 
        ' btnLoadServLoc
        ' 
        btnLoadServLoc.Font = New Font("Segoe UI", 9F)
        btnLoadServLoc.Location = New Point(737, 21)
        btnLoadServLoc.Name = "btnLoadServLoc"
        btnLoadServLoc.Size = New Size(75, 24)
        btnLoadServLoc.TabIndex = 2
        btnLoadServLoc.Text = "Load"
        btnLoadServLoc.TextAlign = ContentAlignment.TopCenter
        btnLoadServLoc.UseVisualStyleBackColor = True
        ' 
        ' rtbServInfo
        ' 
        rtbServInfo.Location = New Point(28, 73)
        rtbServInfo.Name = "rtbServInfo"
        rtbServInfo.Size = New Size(293, 242)
        rtbServInfo.TabIndex = 3
        rtbServInfo.Text = ""
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveBorder
        ClientSize = New Size(818, 639)
        Controls.Add(rtbServInfo)
        Controls.Add(btnLoadServLoc)
        Controls.Add(lblServLoc)
        Controls.Add(txtServLoc)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtServLoc As TextBox
    Friend WithEvents lblServLoc As Label
    Friend WithEvents btnLoadServLoc As Button
    Friend WithEvents rtbServInfo As RichTextBox

End Class
