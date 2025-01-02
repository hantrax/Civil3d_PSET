<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_EsportaFile
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
        Button1 = New System.Windows.Forms.Button()
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        ButtonOK = New System.Windows.Forms.Button()
        ButtonCancell = New System.Windows.Forms.Button()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        Label_mail = New System.Windows.Forms.Label()
        TextBox1 = New System.Windows.Forms.TextBox()
        ProgressBar1 = New System.Windows.Forms.ProgressBar()
        LabelDEMO = New System.Windows.Forms.Label()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New System.Drawing.Point(6, 22)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(764, 37)
        Button1.TabIndex = 0
        Button1.Text = "Select Excel Target File...."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' ButtonOK
        ' 
        ButtonOK.BackColor = Drawing.Color.LightGreen
        ButtonOK.Enabled = False
        ButtonOK.Location = New System.Drawing.Point(524, 102)
        ButtonOK.Name = "ButtonOK"
        ButtonOK.Size = New System.Drawing.Size(138, 33)
        ButtonOK.TabIndex = 1
        ButtonOK.Text = "GO..."
        ButtonOK.UseVisualStyleBackColor = False
        ButtonOK.Visible = False
        ' 
        ' ButtonCancell
        ' 
        ButtonCancell.Location = New System.Drawing.Point(668, 103)
        ButtonCancell.Name = "ButtonCancell"
        ButtonCancell.Size = New System.Drawing.Size(114, 33)
        ButtonCancell.TabIndex = 2
        ButtonCancell.Text = "Cancell"
        ButtonCancell.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Location = New System.Drawing.Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(776, 74)
        GroupBox1.TabIndex = 3
        GroupBox1.TabStop = False
        GroupBox1.Text = "Export Parameters"
        ' 
        ' Label_mail
        ' 
        Label_mail.AutoSize = True
        Label_mail.BackColor = Drawing.Color.DimGray
        Label_mail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        Label_mail.ForeColor = Drawing.Color.White
        Label_mail.Location = New System.Drawing.Point(243, 107)
        Label_mail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label_mail.Name = "Label_mail"
        Label_mail.Size = New System.Drawing.Size(233, 20)
        Label_mail.TabIndex = 33
        Label_mail.Text = "stefano.carta74@gmail.com"
        Label_mail.TextAlign = Drawing.ContentAlignment.MiddleRight
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Drawing.Color.DimGray
        TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TextBox1.ForeColor = Drawing.Color.White
        TextBox1.Location = New System.Drawing.Point(21, 103)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(473, 29)
        TextBox1.TabIndex = 34
        TextBox1.Text = "STEFANO CARTA"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New System.Drawing.Point(18, 169)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New System.Drawing.Size(764, 38)
        ProgressBar1.TabIndex = 35
        ' 
        ' LabelDEMO
        ' 
        LabelDEMO.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.78182F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        LabelDEMO.ForeColor = Drawing.Color.Red
        LabelDEMO.Location = New System.Drawing.Point(21, 142)
        LabelDEMO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        LabelDEMO.Name = "LabelDEMO"
        LabelDEMO.Size = New System.Drawing.Size(706, 23)
        LabelDEMO.TabIndex = 36
        LabelDEMO.Text = "DEMO 15gg - solo 5 file alla volta - 4 Esecuzioni giornaliere"
        LabelDEMO.TextAlign = Drawing.ContentAlignment.BottomLeft
        LabelDEMO.Visible = False
        ' 
        ' Form_EsportaFile
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(800, 216)
        Controls.Add(LabelDEMO)
        Controls.Add(ProgressBar1)
        Controls.Add(Label_mail)
        Controls.Add(TextBox1)
        Controls.Add(GroupBox1)
        Controls.Add(ButtonCancell)
        Controls.Add(ButtonOK)
        MaximumSize = New System.Drawing.Size(816, 255)
        MinimumSize = New System.Drawing.Size(816, 255)
        Name = "Form_EsportaFile"
        Text = "Form1"
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancell As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label_mail As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents LabelDEMO As System.Windows.Forms.Label
End Class
