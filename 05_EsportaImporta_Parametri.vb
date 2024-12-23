' (C) Copyright 2011 by stefano carta
'
Imports System
Imports System.Windows.Controls
Imports Autodesk
Imports System.Windows.Input
Imports Autodesk.Aec.DatabaseServices
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.Civil.DatabaseServices
Imports ObjectId = Autodesk.AutoCAD.DatabaseServices.ObjectId

Imports Autodesk.Aec.PropertyData
Imports Autodesk.Aec.PropertyData.DatabaseServices
Imports System.Collections.Specialized
Imports ClosedXML.Excel.XLWorkbook
Imports ClosedXML
Imports ClosedXML.Excel
Imports System.Globalization

Imports DocumentFormat.OpenXml.Packaging
Imports System.IO.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml
Imports Cell = DocumentFormat.OpenXml.Spreadsheet.Cell
Imports Row = DocumentFormat.OpenXml.Spreadsheet.Row
Imports DocumentFormat.OpenXml.VariantTypes
Imports Autodesk.AutoCAD.Colors




' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(Civil3d_PSET_2025.Esportaparametri))>
Namespace Civil3d_PSET_2025

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class Esportaparametri

        ' The CommandMethod attribute can be applied to any public  member 
        ' function of any public class.
        ' The function should take no arguments and return nothing.
        ' If the method is an instance member then the enclosing class is 
        ' instantiated for each document. If the member is a static member then
        ' the enclosing class is NOT instantiated.
        '
        ' NOTE: CommandMethod has overloads where you can provide helpid and
        ' context menu.

        ' Modal Command with localized name
        ' AutoCAD will search for a resource string with Id "MyCommandLocal" in the 
        ' same namespace as this command class. 
        ' If a resource string is not found, then the string "MyLocalCommand" is used 
        ' as the localized command name.
        ' To view/edit the resx file defining the resource strings for this command, 
        ' * click the 'Show All Files' button in the Solution Explorer;
        ' * expand the tree node for myCommands.vb;
        ' * and double click on myCommands.resx
        '<CommandMethod("ProvaPSET", "MyCommand", "MyCommandLocal", CommandFlags.Modal)>

        <CommandMethod("ExpParam", CommandFlags.Session)>
        Public Sub ExportParameters()



            Dim statoLIC As String = CtrlLic(File_LIC)

            Dim FrmCode As New FRM_Get_Code

            Dim FrmExport As New Form_EsportaFile

            Select Case statoLIC

                Case "PINI"
                    FrmExport.Label_mail.Text = strMail2
                    GoTo a000
                Case "CODLIC"
                    FrmExport.Label_mail.Text = strMail1
                    GoTo a000
                Case "MAXATT"
                    FrmExport.Label_mail.Text = strMail1
                    GoTo a200
                Case "END"
                    FrmExport.Label_mail.Text = strMail1
                    GoTo a200
                Case "DEMO"
                    FrmExport.Label_mail.Text = strMail1
                    MsgBox("Versione Demo...hai ancora:" & My.Settings.numEsecuzioni.ToString & " per oggi..verranno processati solo solo 10 cartigli alla volta...",, _caption)

                    GoTo a000
                Case "NOLIC"
                    GoTo a200
                Case Else

                    FrmCode.TextBox1.Text = TipoLIC
                    FrmCode.ShowDialog()


                    GoTo a200

            End Select

            '****inizio programma*****************


a000:       Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmExport)







            If FrmExport.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                GoTo a200
            End If






a200:   End Sub
        <CommandMethod("ImpParam", CommandFlags.Session)>
        Public Sub ImportParameters()



            Dim statoLIC As String = CtrlLic(File_LIC)

            Dim FrmCode As New FRM_Get_Code

            Dim FrmImport As New Form_ImportaFile


            Select Case statoLIC

                Case "PINI"
                    FrmImport.Label_mail.Text = strMail2
                    GoTo a000
                Case "CODLIC"
                    FrmImport.Label_mail.Text = strMail1
                    GoTo a000
                Case "MAXATT"
                    FrmImport.Label_mail.Text = strMail1
                    GoTo a200
                Case "END"
                    FrmImport.Label_mail.Text = strMail1
                    GoTo a200
                Case "DEMO"
                    FrmImport.Label_mail.Text = strMail1
                    MsgBox("Versione Demo...hai ancora:" & My.Settings.numEsecuzioni.ToString & " per oggi..verranno processati solo solo 10 cartigli alla volta...",, _caption)

                    GoTo a000
                Case "NOLIC"
                    GoTo a200
                Case Else

                    FrmCode.TextBox1.Text = TipoLIC
                    FrmCode.ShowDialog()


                    GoTo a200

            End Select

            '****inizio programma*****************


a000:       Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmImport)


            If FrmImport.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                GoTo a200
            End If





a200:   End Sub

        Public Shared Function StringToHandle(ByVal strHandle As String) As Handle
            Dim handle As Handle = New Handle()

            Try
                Dim nHandle As Int64 = Convert.ToInt64(strHandle, 16)
                handle = New Handle(nHandle)
            Catch __unusedFormatException1__ As System.FormatException
            End Try

            Return handle
        End Function

        Public Shared Function HandleToObjectId(ByVal db As Database, ByVal h As Handle) As ObjectId
            Dim id As ObjectId = ObjectId.Null

            Try
                id = db.GetObjectId(False, h, 0)
            Catch x As Autodesk.AutoCAD.Runtime.Exception

                If x.ErrorStatus <> Autodesk.AutoCAD.Runtime.ErrorStatus.UnknownHandle Then
                    Throw x
                End If
            End Try

            Return id
        End Function
        Public Shared Sub AddLayer(ByVal nameLayer As String, ByVal nColor As Integer) ' | per nessun color 9999 per nessun colore
            Dim myDWG As ApplicationServices.Document
            Dim myDB As DatabaseServices.Database
            Dim myTransMan As DatabaseServices.TransactionManager
            Dim myTrans As DatabaseServices.Transaction

            myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
            myDB = myDWG.Database
            myTransMan = myDWG.TransactionManager
            myTrans = myTransMan.StartTransaction

            Dim myLT As DatabaseServices.LayerTable
            Dim myLayer As New DatabaseServices.LayerTableRecord
            Dim mySTE As DatabaseServices.SymbolTableEnumerator
            Dim myLTR As DatabaseServices.LayerTableRecord
            Dim contr_Layer As Integer = 0

            myLT = myDB.LayerTableId.GetObject(DatabaseServices.OpenMode.ForWrite)

            mySTE = myLT.GetEnumerator
            While mySTE.MoveNext
                myLTR = mySTE.Current.GetObject(DatabaseServices.OpenMode.ForRead)

                If UCase(myLTR.Name) = UCase(nameLayer) Then contr_Layer = 1

            End While

            If contr_Layer = 0 Then
                myLayer.Name = nameLayer
                If nColor <> 9999 Then myLayer.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, nColor)
                myLT.Add(myLayer)

                myTrans.AddNewlyCreatedDBObject(myLayer, True)
            End If

            myTrans.Commit()
            myTrans.Dispose()
            myTransMan.Dispose()

        End Sub
        Public Shared Function ConvertToLetter(iCol As Long) As String
            Dim a As Long
            Dim b As Long
            a = iCol
            ConvertToLetter = ""
            Do While iCol > 0
                a = Int((iCol - 1) / 26)
                b = (iCol - 1) Mod 26
                ConvertToLetter = Chr(b + 65) & ConvertToLetter
                iCol = a
            Loop
        End Function
    End Class

End Namespace
