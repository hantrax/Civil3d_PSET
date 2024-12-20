Imports System.Windows.Forms
Imports Autodesk.AutoCAD.DatabaseServices
Imports ClosedXML.Excel
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.DatabaseServices.Region
Imports Autodesk.AutoCAD.EditorInput
Imports System
Imports System.Math
Imports System.Type
Imports Autodesk.AutoCAD.Colors
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports System.Windows.Controls
Imports Autodesk
Imports Autodesk.Aec.PropertyData.DatabaseServices
Imports Application = Autodesk.AutoCAD.ApplicationServices.Application
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Collections.Specialized
Imports Autodesk.Civil.DatabaseServices
Imports ClosedXML
Imports System.Globalization

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



        Try


            For Each worksheet As IXLWorksheet In workbook.Worksheets


                Dim lastRow As Integer = worksheet.LastRowUsed().RowNumber()
                Dim lastCol As Integer = worksheet.LastColumnUsed().ColumnNumber

                Dim tmpParam As New ParametriVal
                For r = 1 To lastRow

                    tmpParam.HANDLEOBJ = worksheet.Cell(1, 1).Value.to


                    For c = 1 To lastCol




                    Next




                Next




            Next





            ProgressBar1.Minimum = 0




            For i = 1 To lastCol

                Dim tmpPar As String = xlworksheet_Param.Cell(1, i).Value

                lstParam.Add(xlworksheet_Param.Cell(1, i).Value)


            Next


            For i = 2 To lastRow




            Next

        Catch

        End Try
    End Sub

    Private Sub ButtonANN_Click(sender As Object, e As EventArgs) Handles ButtonANN.Click

    End Sub

    Private Sub Form_ImportaFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class