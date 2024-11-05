<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _00_FRM_OpenFile
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        ButtonPDF = New System.Windows.Forms.Button()
        Label_mail = New System.Windows.Forms.Label()
        LabelDEMO = New System.Windows.Forms.Label()
        SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        ButtonOk = New System.Windows.Forms.Button()
        ButtonANN = New System.Windows.Forms.Button()
        ProgressBar1 = New System.Windows.Forms.ProgressBar()
        ListBoxErrori = New System.Windows.Forms.ListBox()
        GroupBox2 = New System.Windows.Forms.GroupBox()
        TextBox1 = New System.Windows.Forms.TextBox()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(ButtonPDF)
        GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.818182F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        GroupBox1.Location = New System.Drawing.Point(14, 14)
        GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        GroupBox1.Size = New System.Drawing.Size(846, 96)
        GroupBox1.TabIndex = 16
        GroupBox1.TabStop = False
        GroupBox1.Text = "PSET Import"
        ' 
        ' ButtonPDF
        ' 
        ButtonPDF.Location = New System.Drawing.Point(9, 38)
        ButtonPDF.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonPDF.Name = "ButtonPDF"
        ButtonPDF.Size = New System.Drawing.Size(825, 33)
        ButtonPDF.TabIndex = 14
        ButtonPDF.Text = "Select Excel file..."
        ButtonPDF.UseVisualStyleBackColor = True
        ' 
        ' Label_mail
        ' 
        Label_mail.AutoSize = True
        Label_mail.BackColor = Drawing.Color.DimGray
        Label_mail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        Label_mail.ForeColor = Drawing.Color.White
        Label_mail.Location = New System.Drawing.Point(233, 139)
        Label_mail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label_mail.Name = "Label_mail"
        Label_mail.Size = New System.Drawing.Size(233, 20)
        Label_mail.TabIndex = 27
        Label_mail.Text = "stefano.carta74@gmail.com"
        Label_mail.TextAlign = Drawing.ContentAlignment.BottomLeft
        ' 
        ' LabelDEMO
        ' 
        LabelDEMO.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        LabelDEMO.ForeColor = Drawing.Color.Red
        LabelDEMO.Location = New System.Drawing.Point(14, 170)
        LabelDEMO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        LabelDEMO.Name = "LabelDEMO"
        LabelDEMO.Size = New System.Drawing.Size(706, 23)
        LabelDEMO.TabIndex = 28
        LabelDEMO.Text = "DEMO 15gg - solo 5 file alla volta - 4 Esecuzioni giornaliere"
        LabelDEMO.TextAlign = Drawing.ContentAlignment.BottomLeft
        LabelDEMO.Visible = False
        ' 
        ' ButtonOk
        ' 
        ButtonOk.BackColor = Drawing.Color.LightGreen
        ButtonOk.Enabled = False
        ButtonOk.Location = New System.Drawing.Point(525, 134)
        ButtonOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonOk.Name = "ButtonOk"
        ButtonOk.Size = New System.Drawing.Size(181, 33)
        ButtonOk.TabIndex = 18
        ButtonOk.Text = "Go..."
        ButtonOk.UseVisualStyleBackColor = False
        ButtonOk.Visible = False
        ' 
        ' ButtonANN
        ' 
        ButtonANN.Location = New System.Drawing.Point(723, 134)
        ButtonANN.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonANN.Name = "ButtonANN"
        ButtonANN.Size = New System.Drawing.Size(125, 33)
        ButtonANN.TabIndex = 19
        ButtonANN.Text = "Cancell"
        ButtonANN.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New System.Drawing.Point(23, 196)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New System.Drawing.Size(825, 38)
        ProgressBar1.TabIndex = 29
        ' 
        ' ListBoxErrori
        ' 
        ListBoxErrori.FormattingEnabled = True
        ListBoxErrori.Location = New System.Drawing.Point(6, 25)
        ListBoxErrori.Name = "ListBoxErrori"
        ListBoxErrori.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        ListBoxErrori.Size = New System.Drawing.Size(813, 308)
        ListBoxErrori.TabIndex = 30
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ListBoxErrori)
        GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        GroupBox2.Location = New System.Drawing.Point(23, 240)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New System.Drawing.Size(825, 345)
        GroupBox2.TabIndex = 31
        GroupBox2.TabStop = False
        GroupBox2.Text = "Duplicate Parameters in the Excel File...Data was copied to clipboard"
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Drawing.Color.DimGray
        TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TextBox1.ForeColor = Drawing.Color.White
        TextBox1.Location = New System.Drawing.Point(12, 134)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(495, 29)
        TextBox1.TabIndex = 32
        TextBox1.Text = "STEFANO CARTA"
        ' 
        ' _00_FRM_OpenFile
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(876, 597)
        Controls.Add(Label_mail)
        Controls.Add(TextBox1)
        Controls.Add(GroupBox2)
        Controls.Add(ProgressBar1)
        Controls.Add(LabelDEMO)
        Controls.Add(ButtonANN)
        Controls.Add(ButtonOk)
        Controls.Add(GroupBox1)
        Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        MaximumSize = New System.Drawing.Size(892, 636)
        MinimumSize = New System.Drawing.Size(892, 636)
        Name = "_00_FRM_OpenFile"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        GroupBox1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label_mail As System.Windows.Forms.Label
    Friend WithEvents LabelDEMO As System.Windows.Forms.Label
    Friend WithEvents ButtonPDF As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonANN As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ListBoxErrori As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
