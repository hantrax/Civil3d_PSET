Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.DatabaseServices.Region
Imports Autodesk.AutoCAD.EditorInput
Imports System
Imports System.Windows.Forms
Imports System.Math
Imports System.Type
Imports Autodesk.AutoCAD.Colors
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports System.Windows.Controls
Imports Autodesk
Imports Autodesk.Aec.PropertyData.DatabaseServices
Imports ClosedXML.Excel
Imports Application = Autodesk.AutoCAD.ApplicationServices.Application
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Collections.Specialized
Imports Autodesk.Civil.DatabaseServices
Imports ClosedXML
Imports System.Globalization

Public Class _00_FRM_OpenFile

    Public myDB As DatabaseServices.Database
    Public myDWG As ApplicationServices.Document
    Public acDocMgr As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager


    Private Sub Button1_Click(sender As Object, e As EventArgs)


        myDWG = DocumentCollectionExtension.Open(acDocMgr, OpenFileDialog1.FileName.ToString, False)

        myDWG.LockDocument()
        myDWG.Editor.Regen()


    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

        'Text
        'REAL
        'Integer
        'Boolean


        Dim doc As Document = Application.DocumentManager.MdiActiveDocument

        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor
        Dim myDWG As ApplicationServices.Document

        myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
        myDWG.LockDocument()


        Dim workbook = New XLWorkbook(strNomeFile)
        Dim xlworksheet_PSET = workbook.Worksheet("PSET")

        Dim lstParametri As New List(Of ListaParametri)

        Try

            Dim lastRow As Integer = xlworksheet_PSET.LastRowUsed().RowNumber()

            ProgressBar1.Minimum = 0



            For r = 2 To lastRow
                Dim tmpParametro As New ListaParametri

                If xlworksheet_PSET.Cell(r, 1).HasFormula Then

                    tmpParametro.NParam = xlworksheet_PSET.Cell(r, 1).CachedValue
                Else

                    tmpParametro.NParam = xlworksheet_PSET.Cell(r, 1).GetString

                End If


                If xlworksheet_PSET.Cell(r, 2).HasFormula Then

                    tmpParametro.pSet = xlworksheet_PSET.Cell(r, 2).CachedValue
                Else

                    tmpParametro.pSet = xlworksheet_PSET.Cell(r, 2).GetString

                End If



                If xlworksheet_PSET.Cell(r, 3).HasFormula Then

                    tmpParametro.tipoParam = xlworksheet_PSET.Cell(r, 3).CachedValue
                Else

                    tmpParametro.tipoParam = xlworksheet_PSET.Cell(r, 3).GetString

                End If


                If xlworksheet_PSET.Cell(r, 4).HasFormula Then


                    tmpParametro.defText = xlworksheet_PSET.Cell(r, 4).CachedValue
                    If tmpParametro.defText = "" Then tmpParametro.defText = "NULL"
                Else

                    tmpParametro.defText = xlworksheet_PSET.Cell(r, 4).GetString
                    If tmpParametro.defText = "" Then tmpParametro.defText = "NULL"
                End If

                lstParametri.Add(tmpParametro)



            Next


            workbook.Dispose()


            ProgressBar1.Maximum = lstParametri.Count

            ProgressBar1.Step = 1



        Catch ex As System.Exception

            MsgBox("Something was wrong...", _caption)

            workbook.Dispose()

            Me.Close()



        End Try

        Try

            For Each parametro As ListaParametri In lstParametri


                GetOrCreateMyPropertySetDefinition(db, parametro.pSet, parametro.NParam, parametro.tipoParam, parametro.defText)

                ProgressBar1.PerformStep()

                Me.Update()

            Next



        Catch ex As AutoCAD.Runtime.Exception


            MsgBox("Something was wrong...", _caption)
            myDWG.LockDocument.Dispose()


        End Try






        MsgBox("Done!!!",, _caption)

        If lstErrore.Count > 0 Then

            Me.ListBoxErrori.DataSource = lstErrore

        Else

            Me.Close()

        End If

        Dim copy_buffer As System.Text.StringBuilder = New System.Text.StringBuilder()

        For Each item As Object In ListBoxErrori.Items
            copy_buffer.AppendLine(item.ToString())
        Next

        If copy_buffer.Length > 0 Then Clipboard.SetText(copy_buffer.ToString())

        If TipoLIC = "DEMO" Then

            My.Settings.dataUtilizzo = Today
            My.Settings.numEsecuzioni = My.Settings.numEsecuzioni - 1

        End If

a5: End Sub

    Private Sub ButtonANN_Click(sender As Object, e As EventArgs) Handles ButtonANN.Click

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button_Fatto_Click(sender As Object, e As EventArgs)



        Me.Close()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonPDF.Click

        OpenFileDialog1 = New OpenFileDialog() With {
            .FileName = "Seleziona File Excel",
            .Filter = "XLSX file (*.xslx)|*.xlsx",
            .Title = "Seleziona File Excel"
        }




        ' Call ShowDialog.
        Dim dlgresult = OpenFileDialog1.ShowDialog

        ' Test result.
        If dlgresult = DialogResult.OK Then

            ' Get the file name.
            Dim path = OpenFileDialog1.FileName

            Try


                strNomeFile = OpenFileDialog1.FileName

                ButtonOk.Enabled = True
                ButtonOk.Visible = True
            Catch ex As System.Exception

                ' Report an error.
                Text = "Error"

            End Try
        End If



a3: End Sub

    Private Sub TextBox_PDFPath_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TXT_WATCHPATH_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub ListBoxErrori_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Private Sub _00_FRM_SetCart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _caption
        lstErrore.Clear()
        System.Windows.Forms.Application.CurrentCulture = New CultureInfo("EN-US")



    End Sub
    Public Function GetOrCreateMyPropertySetDefinition(db As Database, pset As String, param As String, tipo As String, defTXT As String) As ObjectId
        Dim propSetDefId As ObjectId = Nothing
        Dim propSetDefName As String = pset
        Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)


        Dim className1 As String = RXObject.GetClass(GetType(TinSurface)).Name
        Dim className2 As String = RXObject.GetClass(GetType(Solid3d)).Name


        Dim propSetDef As PropertySetDefinition


        Using tr As Transaction = db.TransactionManager.StartTransaction
            If dictPropSetDef.Has(propSetDefName, tr) = False Then
                propSetDef = dictPropSetDef.NewEntry
                propSetDef.Description = pset
                Dim isStyle As Boolean = False

                Dim appliedTo As New StringCollection

                appliedTo.Add(className1)
                appliedTo.Add(className2)
                propSetDef.SetAppliesToFilter(appliedTo, isStyle)

                dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)
                tr.AddNewlyCreatedDBObject(propSetDef, True)



            Else

                propSetDefId = dictPropSetDef.GetAt(propSetDefName)

                propSetDef = tr.GetObject(propSetDefId, OpenMode.ForWrite)


            End If

            Dim propertyExists As Boolean = False


            For Each propDef As PropertyDefinition In propSetDef.Definitions
                If propDef.Name.Equals(param, StringComparison.OrdinalIgnoreCase) Then
                    propertyExists = True


                End If
            Next


            If propertyExists = False Then
                Dim strProp As New PropertyDefinition
                strProp.SetToStandard(db)
                strProp.SubSetDatabaseDefaults(db)
                strProp.Name = param
                strProp.Description = param


                Select Case tipo.ToUpper


                    Case "TEXT"
                        strProp.DataType = Autodesk.Aec.PropertyData.DataType.Text
                        If defTXT <> "NULL" Then strProp.DefaultData = defTXT Else strProp.DefaultData = "XXXX"

                    Case "REAL"
                        'Dim value = Double.Parse(dati.Substring(variabile.Value, 11), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture) ' mi estraggo il valore
                        strProp.DataType = Autodesk.Aec.PropertyData.DataType.Real
                        If defTXT <> "NULL" Then
                            Dim tmpDou As Double = Double.Parse(defTXT, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture)

                            strProp.DefaultData = tmpDou
                        Else
                            strProp.DefaultData = 0
                        End If
                    Case "INTEGER"
                        strProp.DataType = Autodesk.Aec.PropertyData.DataType.Integer

                        If defTXT <> "NULL" Then
                            Dim tmpINT As Integer = Integer.TryParse(defTXT, tmpINT)

                            strProp.DefaultData = tmpINT
                        Else
                            strProp.DefaultData = 0
                        End If



                    Case "BOOLEAN"
                        strProp.DataType = Autodesk.Aec.PropertyData.DataType.TrueFalse


                        If defTXT <> "NULL" Then
                            If defTXT.ToUpper = "TRUE" Then strProp.DefaultData = True
                            If defTXT.ToUpper = "FALSE" Then strProp.DefaultData = False
                        Else

                            strProp.DefaultData = False
                        End If



                    Case Else

                        strProp.DataType = Autodesk.Aec.PropertyData.DataType.Text
                        If defTXT IsNot Nothing Then

                            strProp.DefaultData = defTXT

                        Else

                            strProp.DefaultData = "XXXXX
"

                        End If

                End Select







                'Create a new Property and add it to the PropertySetDefinition
                propSetDef.Definitions.Add(strProp)
                propertyExists = False

            Else

                lstErrore.Add(propSetDef.Name & "|" & param)

            End If


            tr.Commit()

        End Using








        '    Using tr As Transaction = db.TransactionManager.StartTransaction
        '        If dictPropSetDef.Has(propSetDefName, tr) Then
        '            propSetDefId = dictPropSetDef.GetAt(propSetDefName)

        '            Dim propSetDef As PropertySetDefinition = tr.GetObject(propSetDefId, OpenMode.ForWrite)
        '            'tr.Commit()

        '            Dim isStyle As Boolean = False


        '            Dim appliedTo As New StringCollection
        '            appliedTo.Add(className)
        '            propSetDef.SetAppliesToFilter(appliedTo, isStyle)

        '            'Create a new Property and add it to the PropertySetDefinition
        '            Dim strProp As New PropertyDefinition
        '            strProp.Name = param
        '            strProp.Description = param
        '            strProp.DataType = Autodesk.Aec.PropertyData.DataType.Text
        '            strProp.DefaultData = ""
        '            'strProp.UnitType = UnitsType'


        '            propSetDef.Definitions.Add(strProp)

        '            'Add the new PropertySetDefinition to the PropertySetDefinition Dictionary
        '            dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)


        '            tr.AddNewlyCreatedDBObject(propSetDef, True)
        '            tr.Commit()

        '            propSetDefId = propSetDef.ObjectId


        '        Else
        '            'Create a New PropertySetDefinition 
        '            Dim propSetDef As PropertySetDefinition = dictPropSetDef.NewEntry
        '            propSetDef.Description = pset
        '            Dim isStyle As Boolean = False


        '            Dim appliedTo As New StringCollection
        '            appliedTo.Add(className)
        '            propSetDef.SetAppliesToFilter(appliedTo, isStyle)

        '            'Create a new Property and add it to the PropertySetDefinition
        '            Dim strProp As New PropertyDefinition
        '            strProp.Name = param
        '            strProp.Description = param
        '            strProp.DataType = Autodesk.Aec.PropertyData.DataType.Text
        '            strProp.DefaultData = ""
        '            'strProp.UnitType = UnitsType'


        '            propSetDef.Definitions.Add(strProp)

        '            'Add the new PropertySetDefinition to the PropertySetDefinition Dictionary
        '            dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)
        '            tr.AddNewlyCreatedDBObject(propSetDef, True)
        '            tr.Commit()

        '            propSetDefId = propSetDef.ObjectId
        '        End If
        '    End Using

        '    Return propSetDefId
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxErrori.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Process.Start(excelTemplate)
    End Sub
End Class