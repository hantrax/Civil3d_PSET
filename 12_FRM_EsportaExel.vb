Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports System
Imports System.Windows.Forms
Imports Autodesk.Aec.PropertyData.DatabaseServices
Imports ClosedXML.Excel
Imports Application = Autodesk.AutoCAD.ApplicationServices.Application
'Imports Autodesk.Civil.DatabaseServicesz
Imports ClosedXML
Imports System.Globalization
Imports Exception = Autodesk.AutoCAD.Runtime.Exception
Imports System.IO
Imports Autodesk.Aec.DatabaseServices
Imports Autodesk.Aec.PropertyData
Imports ObjectId = Autodesk.AutoCAD.DatabaseServices.ObjectId
Imports DocumentFormat.OpenXml
Imports ClosedXML.Excel.XLWorkbook
Imports System.Windows.Controls
Imports Autodesk
Imports System.Windows.Input
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.Civil.DatabaseServices
Imports DocumentFormat.OpenXml.Drawing


Public Class Form_EsportaFile

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _captionEsp


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        SaveFileDialog1 = New SaveFileDialog() With {
            .FileName = "Select Excel File",
            .Filter = "XLSX file (*.xslx)|*.xlsx",
            .Title = "Seleziona File Excel"
        }

        ' Call ShowDialog.
        Dim dlgresult = SaveFileDialog1.ShowDialog

        ' Test result.
        If dlgresult = DialogResult.OK Then

            ' Get the file name.
            Dim path = SaveFileDialog1.FileName
            Try
                strNomeFile = SaveFileDialog1.FileName

                ButtonOK.Enabled = True
                ButtonOK.Visible = True
            Catch ex As System.Exception

                ' Report an error.
                Text = "Error"

            End Try
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonCancell.Click
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument

        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor
        Dim myDWG As ApplicationServices.Document

        Dim listaColAutom = New Dictionary(Of String, Integer) ' dizionario dei risultati (somma dei valori)

        Dim listaParam = New Dictionary(Of String, Integer) ' dizionario dei risultati (somma dei valori)



        ProgressBar1.Minimum = 0

        myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
        myDWG.LockDocument()

        If File.Exists(ParamTemplate) Then
            File.Copy(ParamTemplate, strNomeFile, True)
        Else

            MsgBox("Template File not found")

            myDWG.LockDocument.Dispose()
            GoTo aFine

        End If

        Dim workbook = New XLWorkbook(strNomeFile)



        Dim lstParametri As New List(Of ListaParametri)

        Dim acSSPrompt As PromptSelectionResult


        Using tr As Transaction = Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction()

            Dim acTypValAr(0) As TypedValue
            'acTypValAr.SetValue(New TypedValue(DxfCode.Start, "3DPOLYLINE"), 0)

            ' Assign the filter criteria to a SelectionFilter object
            Dim acSelFtr As SelectionFilter = New SelectionFilter(acTypValAr)
            Dim myPEO As New EditorInput.PromptEntityOptions("Seleziona gli oggetti da esportare...." & vbLf & "Invio per terminare.." & vbLf) ' seleziona linee di sezione

            acSSPrompt = ed.GetSelection()

            Dim lstParamSource As New List(Of ParametriVal)

            ProgressBar1.Maximum = acSSPrompt.Value.Count

            Dim lstTipo As New List(Of String)
            ProgressBar1.Step = 1
            Dim lstAutom As New List(Of Integer)

            Dim xlworksheet_Param = workbook.Worksheet("CivilPARAM")
            Try
                For Each sobj As SelectedObject In acSSPrompt.Value


                    Dim myAcadEnt As DatabaseServices.Entity

                    myAcadEnt = sobj.ObjectId.GetObject(DatabaseServices.OpenMode.ForRead)


                    Dim etyp As String = myAcadEnt.GetRXClass.DxfName.ToUpper

                    If Not lstTipo.Contains(etyp) Then

                        lstTipo.Add(etyp)

                    End If




                Next

                For Each Foglio In lstTipo

                    xlworksheet_Param.CopyTo(Foglio)
                    'workbook.Worksheets.Add(Foglio)



                Next



                Dim row As Integer = 2
                Dim cont As Integer = 4

                For Each sobj As SelectedObject In acSSPrompt.Value



                    Dim dbObject As Autodesk.AutoCAD.DatabaseServices.DBObject = tr.GetObject(sobj.ObjectId, OpenMode.ForRead)

                    Dim ids As Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection = PropertyDataServices.GetPropertySetDefinitionsUsed(dbObject)


                    Dim myAcadEnt As DatabaseServices.Entity

                    myAcadEnt = sobj.ObjectId.GetObject(DatabaseServices.OpenMode.ForRead)



                    Dim etyp As String = myAcadEnt.GetRXClass.DxfName.ToUpper

                    xlworksheet_Param = workbook.Worksheet(etyp)




                    xlworksheet_Param.Cell(1, 1).Value = "HANDLE"
                    xlworksheet_Param.Cell(1, 2).Value = "LAYER"
                    xlworksheet_Param.Cell(1, 3).Value = "OBJ TYPE"

                    'lstAutom.Add(1)
                    'lstAutom.Add(2)
                    'lstAutom.Add(3)

                    Dim lastRow As Integer = xlworksheet_Param.LastRowUsed().RowNumber()
                    row = lastRow + 1
                    Dim nomeLayer As String = myAcadEnt.Layer

                  


                    For Each propSetDefId As ObjectId In ids
                        Dim propSetDef As PropertySetDefinition = tr.GetObject(propSetDefId, OpenMode.ForRead)

                        Dim propSetId As ObjectId = PropertyDataServices.GetPropertySet(dbObject, propSetDefId)
                        Dim propSet As PropertySet = tr.GetObject(propSetId, OpenMode.ForRead)

                        For Each propDef As PropertyDefinition In propSetDef.Definitions
                            Dim val As Object = propSet.GetAt(propSet.PropertyNameToId(propDef.Name))
                            If val Is Nothing Then val = "NULL"
                            Dim TMPPar As New ParametriVal

                            If propDef.Name.ToUpper = "RFI_TIPOLOGIASEZIONE" Then

                                Dim xe = 1

                            End If

                            If Not listaParam.ContainsKey(propDef.Name) Then
                                listaParam.Add(propDef.Name, cont) ' creo la chiave
                                cont += 1
                            End If


                            TMPPar.PsetName = propSet.PropertySetDefinitionName
                            TMPPar.NParam = propDef.Name
                            TMPPar.tipoParam = propDef.DataType.ToString

                            TMPPar.textVal = val
                            If propDef.Automatic = True Then
                                TMPPar.autoParam = True
                            Else
                                TMPPar.autoParam = False

                            End If

                            If dbObject.Handle.ToString.ToUpper = "90C187E" Then

                                Dim cc As String = "QQ"


                            End If

                            TMPPar.HANDLEOBJ = dbObject.Handle

                            lstParamSource.Add(TMPPar)

                        Next

                        ' End If


                    Next




                    Dim col As Integer = 4


                    'xlworksheet_Param.Cell(1, 2).Value = "PSET"
                    'xlworksheet_Param.Cell(1, 3).Value = "PARAM NAME"
                    'xlworksheet_Param.Cell(1, 4).Value = "PARAMETER TYPE"
                    'xlworksheet_Param.Cell(1, 5).Value = "VALUE"

                    ' Dim colorx As String = xlworksheet_config.Cell(i, 3).Style.Fill.BackgroundColor.Color.Name.ToUpper 'xlworksheet_config.Cells(i, 3).Interior.Color
                    'tmpFirme.STRColor = colorx.ToString

                    For x = 0 To lstParamSource.Count - 1

                        Dim tmpParamEX As ParametriVal = lstParamSource(x)

                        xlworksheet_Param.Cell(row, 1).Value = tmpParamEX.HANDLEOBJ.ToString
                        'xlworksheet_Param.Column(1).Style.Fill.BackgroundColor = XLColor.LightBlue
                        xlworksheet_Param.Cell(row, 2).Value = nomeLayer.ToUpper
                        xlworksheet_Param.Cell(row, 3).Value = etyp.ToUpper


                        If listaParam.ContainsKey(tmpParamEX.NParam) Then

                            col = listaParam(tmpParamEX.NParam)



                        End If


                        xlworksheet_Param.Cell(1, col).Value = tmpParamEX.NParam.ToString & Environment.NewLine & tmpParamEX.PsetName.ToString & Environment.NewLine & tmpParamEX.tipoParam.ToString & Environment.NewLine & tmpParamEX.autoParam.ToString

                        If Not listaColAutom.ContainsKey(etyp.ToUpper & "|" & 1) Then listaColAutom.Add(etyp.ToUpper & "|" & 1, 1)
                        'If Not listaColAutom.ContainsKey(etyp.ToUpper & "|" & 2) Then listaColAutom.Add(etyp.ToUpper & "|" & 2, 2)
                        If Not listaColAutom.ContainsKey(etyp.ToUpper & "|" & 3) Then listaColAutom.Add(etyp.ToUpper & "|" & 3, 3)

                        If tmpParamEX.textVal.ToString = "NULL" Then

                            xlworksheet_Param.Cell(row, col).Value = ""

                        Else

                            xlworksheet_Param.Cell(row, col).Value = tmpParamEX.textVal.ToString
                        End If

                        If tmpParamEX.autoParam = True Then

                            'If Not listaColAutom.ContainsKey(etyp.ToUpper) Then


                            If Not listaColAutom.ContainsKey(etyp.ToUpper & "|" & col) Then
                                listaColAutom.Add(etyp.ToUpper & "|" & col, col) ' creo la chiave
                            End If

                            'End If



                        End If
                        col += 1
                    Next
                    'row += 1



                    lstParamSource.Clear()
                    ProgressBar1.PerformStep()
a300:           Next


            Catch ex As System.Exception

            End Try







            For Each Foglio In lstTipo

                Dim lastCol As Long = workbook.Worksheet(Foglio).LastColumnUsed().ColumnNumber
                Dim lastRow As Integer = workbook.Worksheet(Foglio).LastRowUsed().RowNumber()



                Dim strCol As String = Civil3d_PSET_2025.Esportaparametri.ConvertToLetter(lastCol)

                Dim tmpRangeHead As String = "A1:" & strCol & "1"
                workbook.Worksheet(Foglio).Range(tmpRangeHead).Style.Fill.BackgroundColor = XLColor.PastelOrange
                workbook.Worksheet(Foglio).Range(tmpRangeHead).Style.Border.BottomBorder = XLBorderStyleValues.Thin

                Dim tmpRange As String = "A1:" & strCol & lastRow & ""

                workbook.Worksheet(Foglio).Range(tmpRange).Style.Border.RightBorder = XLBorderStyleValues.Thin




            Next

            For Each Colonna In listaColAutom

                Dim col As Integer = Colonna.Value

                Dim Foglio As String() = Colonna.Key.Split("|")

                Dim strCol As String = Civil3d_PSET_2025.Esportaparametri.ConvertToLetter(col)
                Dim lastRow As Integer = workbook.Worksheet(Foglio(0)).LastRowUsed().RowNumber()

                Dim tmpRange As String = strCol & "1:" & strCol & lastRow & ""

                workbook.Worksheet(Foglio(0)).Range(tmpRange).Style.Fill.BackgroundColor = XLColor.LightBlue

                ' workbook.Worksheet(Foglio(0)).Column(col).Style.Fill.BackgroundColor = XLColor.LightBlue

            Next

            workbook.Worksheet("CivilPARAM").Delete()
            workbook.Save()
            workbook.Dispose()



            tr.Commit()
        End Using


        MsgBox("File Exported succesfully....",, _captionEsp)

        Me.Close()

aFine: End Sub
End Class