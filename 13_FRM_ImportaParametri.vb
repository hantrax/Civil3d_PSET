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

Public Class Form_ImportaFile
    Private Sub ButtonPDF_Click(sender As Object, e As EventArgs) Handles ButtonPDF.Click

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

        Dim xlworksheet_Param = workbook.Worksheet("CivilPARAM")
        Dim lstParam As New List(Of String)
        Dim lstParamSource As New List(Of ParametriVal)



        Using tr As Transaction = Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction()
            Try
                For Each worksheet As IXLWorksheet In workbook.Worksheets


                    Dim lastRow As Integer = worksheet.LastRowUsed().RowNumber()
                    Dim lastCol As Integer = worksheet.LastColumnUsed().ColumnNumber

                    Dim tmpParam As New ParametriVal
                    For r = 2 To lastRow

                        For c = 1 To lastCol
                            Dim pr As String = worksheet.Cell(1, 1).Value
                            Dim ln As Long = Convert.ToInt64(pr, 16)

                            ' Not create a Handle from the long integer

                            Dim hn As Handle = New Handle(ln)

                            ' And attempt to get an ObjectId for the Handle


                            tmpParam.HANDLEOBJ = hn

                            Dim tmpHead As String = worksheet.Cell(1, c).Value

                            Dim HeadCell As String() = tmpHead.Split("|")

                            tmpParam.NParam = HeadCell(0)
                            tmpParam.PsetName = HeadCell(1)
                            tmpParam.tipoParam = HeadCell(2)
                            tmpParam.autoParam = HeadCell(3)
                            tmpParam.textVal = worksheet.Cell(r, c).Value


                        Next

                        lstParamSource.Add(tmpParam)

                    Next

                Next


                For Each obj In lstParamSource

                    Dim id As ObjectId = db.GetObjectId(False, obj.HANDLEOBJ, 0)
                    Dim dbObject As Autodesk.AutoCAD.DatabaseServices.DBObject = tr.GetObject(id, OpenMode.ForRead)
                    Dim objCur As Autodesk.Aec.DatabaseServices.DBObject = tr.GetObject(id, OpenMode.ForWrite)


                    Dim ids As Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection = PropertyDataServices.GetPropertySetDefinitionsUsed(DBObject)


                    For Each propSetDefId As ObjectId In ids
                        Dim propSetDef As PropertySetDefinition = tr.GetObject(propSetDefId, OpenMode.ForWrite)

                        Dim propSetId As ObjectId = PropertyDataServices.GetPropertySet(dbObject, propSetDefId)
                        Dim propSet As PropertySet = tr.GetObject(propSetId, OpenMode.ForWrite)

                        For Each propDef As PropertyDefinition In propSetDef.Definitions

                            If Not propDef.Automatic Then
                                If propSet.PropertySetDefinitionName.ToUpper = obj.NParam.ToUpper Then

                                    If obj.tipoParam.ToUpper = "TEXT" Then propSet.SetAt(propSet.PropertyNameToId(propDef.Name), obj.textVal.ToString)
                                    If obj.tipoParam.ToUpper = "REAL" Then propSet.SetAt(propSet.PropertyNameToId(propDef.Name), obj.textVal)
                                    If obj.tipoParam.ToUpper = "BOOLEAN" Then propSet.SetAt(propSet.PropertyNameToId(propDef.Name), obj.textVal)
                                    If obj.tipoParam.ToUpper = "INTEGER" Then propSet.SetAt(propSet.PropertyNameToId(propDef.Name), obj.textVal)

                                End If
                            End If


                        Next

                        ' End If


                    Next



                Next


                ProgressBar1.Minimum = 0



            Catch

            End Try


            tr.Commit()

        End Using




    End Sub

    Private Sub ButtonANN_Click(sender As Object, e As EventArgs) Handles ButtonANN.Click

    End Sub

    Private Sub Form_ImportaFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class