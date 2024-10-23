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
        Label1 = New System.Windows.Forms.Label()
        Label_mail = New System.Windows.Forms.Label()
        LabelDEMO = New System.Windows.Forms.Label()
        SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        ButtonOk = New System.Windows.Forms.Button()
        ButtonANN = New System.Windows.Forms.Button()
        ProgressBar1 = New System.Windows.Forms.ProgressBar()
        GroupBox1.SuspendLayout()
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
        GroupBox1.Text = "Importa PSET"
        ' 
        ' ButtonPDF
        ' 
        ButtonPDF.Location = New System.Drawing.Point(9, 38)
        ButtonPDF.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonPDF.Name = "ButtonPDF"
        ButtonPDF.Size = New System.Drawing.Size(825, 33)
        ButtonPDF.TabIndex = 14
        ButtonPDF.Text = "Seleziona File Excel..."
        ButtonPDF.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        Label1.Location = New System.Drawing.Point(23, 138)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(137, 20)
        Label1.TabIndex = 26
        Label1.Text = "Stefano CARTA"
        Label1.TextAlign = Drawing.ContentAlignment.BottomLeft
        ' 
        ' Label_mail
        ' 
        Label_mail.AutoSize = True
        Label_mail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        Label_mail.Location = New System.Drawing.Point(204, 138)
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
        LabelDEMO.Location = New System.Drawing.Point(23, 179)
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
        ButtonOk.Text = "Elabora"
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
        ButtonANN.Text = "Annulla"
        ButtonANN.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New System.Drawing.Point(23, 218)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New System.Drawing.Size(825, 38)
        ProgressBar1.TabIndex = 29
        ' 
        ' _00_FRM_OpenFile
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(876, 278)
        Controls.Add(ProgressBar1)
        Controls.Add(LabelDEMO)
        Controls.Add(ButtonANN)
        Controls.Add(Label_mail)
        Controls.Add(ButtonOk)
        Controls.Add(Label1)
        Controls.Add(GroupBox1)
        Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        MaximumSize = New System.Drawing.Size(892, 317)
        Name = "_00_FRM_OpenFile"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label_mail As System.Windows.Forms.Label
    Friend WithEvents LabelDEMO As System.Windows.Forms.Label
    Friend WithEvents ButtonPDF As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonANN As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
