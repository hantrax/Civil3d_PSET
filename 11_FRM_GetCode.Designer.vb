<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRM_Get_Code
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
        Label1 = New System.Windows.Forms.Label()
        TextBox1 = New System.Windows.Forms.TextBox()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New System.Drawing.Point(246, 160)
        Button1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(88, 27)
        Button1.TabIndex = 5
        Button1.Text = "Ok"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(14, 18)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(432, 15)
        Label1.TabIndex = 4
        Label1.Text = "Inviare una mail a stefano.carta74@gmail.com con il seguente codice di richiesta"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New System.Drawing.Point(14, 47)
        TextBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New System.Drawing.Size(542, 91)
        TextBox1.TabIndex = 3
        ' 
        ' FRM_Get_Code
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(572, 198)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Name = "FRM_Get_Code"
        Text = "Codice di Richiesta"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
