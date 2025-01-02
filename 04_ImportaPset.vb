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
Imports ClosedXML.Excel
Imports System.Globalization


Imports Autodesk.Internal.InfoCenter
Imports Application = Autodesk.AutoCAD.ApplicationServices.Application
Imports DBObject = Autodesk.AutoCAD.DatabaseServices.DBObject



' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(Civil3d_PSET_2025.ImportaPset))>
Namespace Civil3d_PSET_2025

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class ImportaPset

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

        <CommandMethod("PsetImp", CommandFlags.Session)>
        Public Sub Createset()

           

            Dim statoLIC As String = CtrlLic(File_LIC)

            Dim FrmCode As New FRM_Get_Code

            Dim FrmImport As New _00_FRM_OpenFile


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

        <CommandMethod("copyPar", CommandFlags.Session)>
        Public Sub CopyParam()
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            Dim ed As Editor = doc.Editor
            Dim db As Database = doc.Database

            Dim myDWG As ApplicationServices.Document

            myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
            myDWG.LockDocument()

            Dim res As PromptEntityResult = ed.GetEntity(vbCrLf & "Select Source entity: ")
            If res.Status <> PromptStatus.OK Then Exit Sub

            Dim res1 As PromptEntityResult = ed.GetEntity(vbCrLf & "Select Target entity: ")
            If res1.Status <> PromptStatus.OK Then Exit Sub

            Dim lstParamSource As New List(Of ParametriVal)
            Dim lstParamTarget As New List(Of ParametriVal)
            Dim dicParamSource = New Dictionary(Of String, ParametriVal)


            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim dbObject As Autodesk.AutoCAD.DatabaseServices.DBObject = tr.GetObject(res.ObjectId, OpenMode.ForRead)

                Dim ids As AutoCAD.DatabaseServices.ObjectIdCollection = PropertyDataServices.GetPropertySetDefinitionsUsed(dbObject)

                For Each propSetDefId As ObjectId In ids
                    Dim propSetDef As PropertySetDefinition = tr.GetObject(propSetDefId, OpenMode.ForRead)


                    ' If propSetDef.Name.Contains("PSET") = True Then

                    Dim propSetId As ObjectId = PropertyDataServices.GetPropertySet(dbObject, propSetDefId)
                    Dim propSet As PropertySet = tr.GetObject(propSetId, OpenMode.ForRead)

                    'ed.WriteMessage(vbCrLf & "Property Set Defintion Name: " & propSetDef.Name)

                    For Each propDef As PropertyDefinition In propSetDef.Definitions
                        Dim val As Object = propSet.GetAt(propSet.PropertyNameToId(propDef.Name))
                        If val IsNot Nothing Then
                            Dim TMPPar As New ParametriVal

                            TMPPar.PsetName = propSet.PropertySetDefinitionName
                            TMPPar.NParam = propDef.Name
                            TMPPar.tipoParam = propDef.DataType.ToString
                            TMPPar.textVal = val


                            If propDef.Automatic = False Then

                                If Not dicParamSource.ContainsKey(TMPPar.NParam) Then
                                    dicParamSource.Add(TMPPar.NParam, TMPPar) ' creo la chiave
                                End If


                                lstParamSource.Add(TMPPar)
                            End If

                        Else
                            Dim TMPPar As New ParametriVal

                            TMPPar.PsetName = propSet.PropertySetDefinitionName
                            TMPPar.NParam = propDef.Name
                            TMPPar.tipoParam = propDef.DataType.ToString
                            TMPPar.textVal = "NULL"

                            If Not dicParamSource.ContainsKey(TMPPar.NParam) Then
                                dicParamSource.Add(TMPPar.NParam, TMPPar) ' creo la chiave
                            End If

                        End If
                    Next

                Next
                'scrivo i dati nel target object
                dbObject = tr.GetObject(res1.ObjectId, OpenMode.ForRead)

                ids = PropertyDataServices.GetPropertySetDefinitionsUsed(dbObject)

                If ids.Count = 0 Then

                    MsgBox("Target Object has no PropertySet")
                    GoTo a300

                End If

                Dim dbObj As Autodesk.AutoCAD.DatabaseServices.DBObject = tr.GetObject(res1.ObjectId, OpenMode.ForWrite)


                For Each ParamCustom In lstParamSource

                    SetMyPropertySetPropertyOBJ(dbObj, ParamCustom.tipoParam, ParamCustom.textVal, db, ParamCustom.NParam, ParamCustom.PsetName)

                Next


a300:           myDWG.LockDocument.Dispose()
                tr.Commit()
            End Using
        End Sub
        Public Function SetMyPropertySetPropertyOBJ(ByRef dbObject As DBObject, tipoParam As String, textValue As Object, db As Database, MyParameter As String, PSET As String) As Boolean
            Dim propSetDefId As ObjectId = GetMyPropertySetDefinition(db, PSET)

            If propSetDefId = ObjectId.Null Then Return False

            'PropertyDataServices.AddPropertySet(dbObject, propSetDefId)

            Dim propSetId As ObjectId = PropertyDataServices.GetPropertySet(dbObject, propSetDefId)
            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim propSet As PropertySet = tr.GetObject(propSetId, OpenMode.ForWrite)

                'Dim strProp As PropertyDefinition = propSet.GetAt(propSet.PropertyNameToId(MyParameter))

                Select Case tipoParam.ToUpper
                    Case "TEXT"

                        propSet.SetAt(propSet.PropertyNameToId(MyParameter), textValue.ToString)

                    Case "REAL"

                        propSet.SetAt(propSet.PropertyNameToId(MyParameter), textValue)

                    Case "INTEGER"

                        propSet.SetAt(propSet.PropertyNameToId(MyParameter), textValue)
                    Case Else

                        propSet.SetAt(propSet.PropertyNameToId(MyParameter), textValue)

                End Select

                tr.Commit()
            End Using
            Return True
        End Function
        Private Function GetMyPropertySetDefinition(db As Database, MyParameter As String) As ObjectId
            Dim propSetDefId As ObjectId = Nothing
            Dim propSetDefName As String = MyParameter
            Dim dictPropSetDef = New DictionaryPropertySetDefinitions(db)

            Using tr As Transaction = db.TransactionManager.StartTransaction
                If dictPropSetDef.Has(propSetDefName, tr) Then
                    propSetDefId = dictPropSetDef.GetAt(propSetDefName)
                    tr.Commit()

                End If
            End Using

            Return propSetDefId
        End Function
    End Class

End Namespace
