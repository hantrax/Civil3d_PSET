<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ImportaFile
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label_mail = New System.Windows.Forms.Label()
        TextBox1 = New System.Windows.Forms.TextBox()
        LabelDEMO = New System.Windows.Forms.Label()
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        ButtonPDF = New System.Windows.Forms.Button()
        ProgressBar1 = New System.Windows.Forms.ProgressBar()
        ButtonANN = New System.Windows.Forms.Button()
        ButtonOk = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' Label_mail
        ' 
        Label_mail.AutoSize = True
        Label_mail.BackColor = Drawing.Color.DimGray
        Label_mail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        Label_mail.ForeColor = Drawing.Color.White
        Label_mail.Location = New System.Drawing.Point(249, 113)
        Label_mail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label_mail.Name = "Label_mail"
        Label_mail.Size = New System.Drawing.Size(233, 20)
        Label_mail.TabIndex = 33
        Label_mail.Text = "stefano.carta74@gmail.com"
        Label_mail.TextAlign = Drawing.ContentAlignment.BottomLeft
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Drawing.Color.DimGray
        TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TextBox1.ForeColor = Drawing.Color.White
        TextBox1.Location = New System.Drawing.Point(28, 108)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(495, 29)
        TextBox1.TabIndex = 35
        TextBox1.Text = "STEFANO CARTA"
        ' 
        ' LabelDEMO
        ' 
        LabelDEMO.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        LabelDEMO.ForeColor = Drawing.Color.Red
        LabelDEMO.Location = New System.Drawing.Point(30, 144)
        LabelDEMO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        LabelDEMO.Name = "LabelDEMO"
        LabelDEMO.Size = New System.Drawing.Size(706, 23)
        LabelDEMO.TabIndex = 34
        LabelDEMO.Text = "DEMO 15gg - solo 5 file alla volta - 4 Esecuzioni giornaliere"
        LabelDEMO.TextAlign = Drawing.ContentAlignment.BottomLeft
        LabelDEMO.Visible = False
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' ButtonPDF
        ' 
        ButtonPDF.Location = New System.Drawing.Point(22, 49)
        ButtonPDF.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonPDF.Name = "ButtonPDF"
        ButtonPDF.Size = New System.Drawing.Size(825, 33)
        ButtonPDF.TabIndex = 36
        ButtonPDF.Text = "Select Excel file..."
        ButtonPDF.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New System.Drawing.Point(28, 170)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New System.Drawing.Size(825, 38)
        ProgressBar1.TabIndex = 39
        ' 
        ' ButtonANN
        ' 
        ButtonANN.Location = New System.Drawing.Point(728, 108)
        ButtonANN.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonANN.Name = "ButtonANN"
        ButtonANN.Size = New System.Drawing.Size(125, 33)
        ButtonANN.TabIndex = 38
        ButtonANN.Text = "Cancell"
        ButtonANN.UseVisualStyleBackColor = True
        ' 
        ' ButtonOk
        ' 
        ButtonOk.BackColor = Drawing.Color.LightGreen
        ButtonOk.Enabled = False
        ButtonOk.Location = New System.Drawing.Point(530, 108)
        ButtonOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        ButtonOk.Name = "ButtonOk"
        ButtonOk.Size = New System.Drawing.Size(181, 33)
        ButtonOk.TabIndex = 37
        ButtonOk.Text = "Go..."
        ButtonOk.UseVisualStyleBackColor = False
        ButtonOk.Visible = False
        ' 
        ' Form_ImportaFile
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(860, 219)
        Controls.Add(ButtonPDF)
        Controls.Add(ProgressBar1)
        Controls.Add(ButtonANN)
        Controls.Add(ButtonOk)
        Controls.Add(Label_mail)
        Controls.Add(TextBox1)
        Controls.Add(LabelDEMO)
        Name = "Form_ImportaFile"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label_mail As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents LabelDEMO As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonPDF As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonANN As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
End Class
