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


                lstParametri.Add(tmpParametro)



            Next


            workbook.Dispose()


            ProgressBar1.Maximum = lstParametri.Count

            ProgressBar1.Step = 1






        Catch ex As System.Exception

            MsgBox("Qualcosa non ha funzionato...", _caption)

            workbook.Dispose()

            Me.Close()



        End Try

        Try

            For Each parametro As ListaParametri In lstParametri


                GetOrCreateMyPropertySetDefinition(db, parametro.pSet, parametro.NParam, parametro.tipoParam)

                ProgressBar1.PerformStep()

                Me.Update()

            Next



        Catch ex As AutoCAD.Runtime.Exception


            MsgBox("Qualcosa non ha funzionato...", _caption)
            myDWG.LockDocument.Dispose()


        End Try




        'myDWG.LockDocument()


        '' The name of the property set def.

        '' Could be for objects or styles. Hard coding for simplicity.

        '' This will be the key in the dictionary

        'Dim propSetDefName As String = "ACADoorObjects"

        ''Dim propSetDefName As String = "ACANetDoorStyles"


        'Try

        '    ' (1) create prop set def


        '    Dim propSetDef As New PropertySetDefinition

        '    propSetDef.SetToStandard(db)

        '    propSetDef.SubSetDatabaseDefaults(db)

        '    ' alternatively, you can use dictionary's NewEntry

        '    'Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)

        '    'Dim propSetDef As PropertySetDefinition =

        '    '  dictPropSetDef.NewEntry()



        '    ' General tab


        '    propSetDef.Description = "Property Set Definition by ACA.NET"


        '    ' Applies To tab


        '    ' apply to objects or styles. True if style, False if objects

        '    Dim isStyle As Boolean = False

        '    Dim appliedTo = New StringCollection()

        '    appliedTo.Add("AecDbDoor")       ' apply to a door object 

        '    'appliedTo.Add("AecDbDoorStyle") ' apply to a door style

        '    ' apply to more than one object type, add more here.

        '    'appliedTo.Add("AecDbWindow")   



        '    propSetDef.SetAppliesToFilter(appliedTo, isStyle)



        '    ' Definition tab



        '    ' (2) we can add a set of property definitions. 

        '    ' We first make a container to hold them.

        '    ' This is the main part. A property set definition can contain

        '    ' a set of property definition.



        '    ' (2.1) let's first add manual property.

        '    ' Here we use text type



        '    'Dim listdef As Db.ListDefinition = New Db.ListDefinition()
        '    'Dim listdefid As ObjectId

        '    'Using trans As OpenCloseTransaction = db.TransactionManager.StartOpenCloseTransaction()
        '    '    listdefid = db.AddDBObject(listdef)
        '    '    trans.AddNewlyCreatedDBObject(listdef, True)
        '    '    Dim dict0 As Db.DictionaryListDefinition = New Db.DictionaryListDefinition(db)
        '    '    dict0.AddNewRecord("MyList", listdef)
        '    '    listdef.AddListItem("fisrt")
        '    '    listdef.AddListItem("second")
        '    '    listdef.AddListItem("555")
        '    '    trans.Commit()
        '    'End Using

        '    'Dim propDefManual2 As PropertyDefinition = New PropertyDefinition()
        '    'propDefManual2.SetToStandard(db)
        '    'propDefManual2.SubSetDatabaseDefaults(db)
        '    'propDefManual2.Name = "ManualPropList"
        '    'propDefManual2.Description = "Manual property List"
        '    'propDefManual2.DataType = Autodesk.Aec.PropertyData.DataType.List
        '    'propDefManual2.ListDefinitionId = listdef.Id
        '    'propDefManual2.DefaultData = "555"
        '    'propSetDef.Definitions.Add(propDefManual2)








        '    Dim propDefManual = New PropertyDefinition()

        '    propDefManual.SetToStandard(db)

        '    propDefManual.SubSetDatabaseDefaults(db)

        '    propDefManual.Name = "ACAManualProp"

        '    propDefManual.Description = "Manual property by ACA.NET"

        '    propDefManual.DataType = Autodesk.Aec.PropertyData.DataType.Text

        '    propDefManual.DefaultData = "ACA Default"

        '    ' add to the prop set def

        '    propSetDef.Definitions.Add(propDefManual)



        '    ' (2.2) let's add another one, automatic one this time



        '    'Dim propDefAutomatic = New PropertyDefinition()

        '    'propDefAutomatic.SetToStandard(db)

        '    'propDefAutomatic.SubSetDatabaseDefaults(db)

        '    'propDefAutomatic.Name = "ACAWidth"

        '    'propDefAutomatic.Description = "Automatic property by ACA.NET"

        '    'propDefAutomatic.SetAutomaticData("AecDbDoor", "Width")

        '    '' add to the prop set def

        '    'propSetDef.Definitions.Add(propDefAutomatic)



        '    ' similarly, add one with height



        '    'propDefAutomatic = New PropertyDefinition()

        '    'propDefAutomatic.SetToStandard(db)

        '    'propDefAutomatic.SubSetDatabaseDefaults(db)

        '    'propDefAutomatic.Name = "ACAHeight"

        '    'propDefAutomatic.Description = "Automatic property by ACA.NET"

        '    'propDefAutomatic.SetAutomaticData("AecDbDoor", "Height")

        '    ''  add to the prop set def

        '    'propSetDef.Definitions.Add(propDefAutomatic)



        '    ' (3)  finally add the prop set def to the database



        '    Using tr As Transaction = db.TransactionManager.StartTransaction()



        '        '  check the name

        '        Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)

        '        If dictPropSetDef.Has(propSetDefName, tr) Then

        '            ed.WriteMessage("error - the property set defintion already exists: " + propSetDefName + vbCrLf)

        '            Return

        '        End If



        '        dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)

        '        tr.AddNewlyCreatedDBObject(propSetDef, True)

        '        tr.Commit()



        '    End Using



        'Catch ex As AutoCAD.Runtime.Exception

        '    ed.WriteMessage("error in CreatePropSetDef: " + ex.ToString + vbCrLf)

        '    Return

        'End Try



        'ed.WriteMessage("property set definition " + propSetDefName + " is successfully created." + vbCrLf)

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
        Dim dlgresult As DialogResult = OpenFileDialog1.ShowDialog()

        ' Test result.
        If dlgresult = DialogResult.OK Then

            ' Get the file name.
            Dim path As String = OpenFileDialog1.FileName

            Try


                strNomeFile = OpenFileDialog1.FileName

                Me.ButtonOk.Enabled = True
                Me.ButtonOk.Visible = True
            Catch ex As System.Exception

                ' Report an error.
                Me.Text = "Error"

            End Try
        End If

a3: End Sub

    Private Sub TextBox_PDFPath_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TXT_WATCHPATH_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub _00_FRM_SetCart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _caption




    End Sub
    Public Function GetOrCreateMyPropertySetDefinition(db As Database, pset As String, param As String, tipo As String) As ObjectId
        Dim propSetDefId As ObjectId = Nothing
        Dim propSetDefName As String = pset
        Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)
        Dim className As String = RXObject.GetClass(GetType(Line)).Name


        Using tr As Transaction = db.TransactionManager.StartTransaction
            If dictPropSetDef.Has(propSetDefName, tr) Then
                propSetDefId = dictPropSetDef.GetAt(propSetDefName)
                tr.Commit()
            Else
                'Create a New PropertySetDefinition 
                Dim propSetDef As PropertySetDefinition = dictPropSetDef.NewEntry
                propSetDef.Description = pset
                Dim isStyle As Boolean = False


                Dim appliedTo As New StringCollection
                appliedTo.Add(className)
                propSetDef.SetAppliesToFilter(appliedTo, isStyle)

                'Create a new Property and add it to the PropertySetDefinition
                Dim strProp As New PropertyDefinition
                strProp.Name = param
                strProp.Description = param
                strProp.DataType = Autodesk.Aec.PropertyData.DataType.Text
                strProp.DefaultData = ""
                'strProp.UnitType = UnitsType'


                propSetDef.Definitions.Add(strProp)

                'Add the new PropertySetDefinition to the PropertySetDefinition Dictionary
                dictPropSetDef.AddNewRecord(propSetDefName, propSetDef)
                tr.AddNewlyCreatedDBObject(propSetDef, True)
                tr.Commit()

                propSetDefId = propSetDef.ObjectId
            End If
        End Using

        Return propSetDefId
    End Function



End Class