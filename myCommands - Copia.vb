' (C) Copyright 2011 by  
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





' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(Civil3d_PSET_20251.MyCommands1))>
Namespace Civil3d_PSET_20251

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class MyCommands1

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

        <CommandMethod("CreatePset", CommandFlags.Session)>
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
a000:       Dim strPath As String

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(FrmImport)


            If FrmImport.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                GoTo a200
            End If






a200:   End Sub




        <CommandMethod("InsPset8", CommandFlags.Session)>
        Public Sub SurroundingSub()
            Dim propertySetName As String = "MyTest"
            Dim database = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database
            Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            Dim ed As Editor = doc.Editor

            Dim myDWG As ApplicationServices.Document

            myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
            myDWG.LockDocument()

            Dim SolidClassName = RXObject.GetClass(GetType(Solid3d)).Name
            Dim TINClassName = RXObject.GetClass(GetType(TinSurface)).Name
            Dim AlignmentClassName = RXObject.GetClass(GetType(Alignment)).Name
            Dim dictionaryPropertyDataFormat = New DictionaryPropertyDataFormat(database)

            Using transaction1 As Transaction = database.TransactionManager.StartTransaction()
                Dim propertySetDefinition = New PropertySetDefinition()
                propertySetDefinition.SetToStandard(database)
                propertySetDefinition.SubSetDatabaseDefaults(database)
                propertySetDefinition.AlternateName = propertySetName
                propertySetDefinition.IsLocked = False
                propertySetDefinition.IsVisible = True
                propertySetDefinition.IsWriteable = True
                Dim filters = New StringCollection()
                filters.Add(SolidClassName)
                filters.Add(TINClassName)
                filters.Add(AlignmentClassName)
                propertySetDefinition.SetAppliesToFilter(filters, False)
                Dim autoPropDef = New PropertyDefinition()
                autoPropDef.SetToStandard(database)
                autoPropDef.SubSetDatabaseDefaults(database)
                autoPropDef.Name = "PROVA"
                autoPropDef.Automatic = True
                autoPropDef.IsVisible = True
                autoPropDef.IsReadOnly = False
                autoPropDef.FormatId = dictionaryPropertyDataFormat.GetAt("Standard")
                autoPropDef.SetAutomaticData(TINClassName, "Testo")
                propertySetDefinition.Definitions.Add(autoPropDef)
                propertySetDefinition.SetDisplayOrder(autoPropDef, 1)
                Dim dictionaryPropertySetDefinitions = New DictionaryPropertySetDefinitions(database)

                If dictionaryPropertySetDefinitions.Has(propertySetName, transaction1) Then
                    Return
                End If
                myDWG.LockDocument.Dispose()
                dictionaryPropertySetDefinitions.AddNewRecord(propertySetName, propertySetDefinition)
                transaction1.AddNewlyCreatedDBObject(propertySetDefinition, True)
                transaction1.Commit()

            End Using
        End Sub



        <CommandMethod("InsPset5", CommandFlags.Session)>
        Public Sub AttachPropertySet()



            ' Top most objects.



            Dim doc As Document = Application.DocumentManager.MdiActiveDocument

            Dim db As Database = doc.Database

            Dim ed As Editor = doc.Editor



            ' (1) select an AEC object to attach a property set



            Dim optEnt As New PromptEntityOptions(vbLf & "Select an AEC object to attach a property set")

            optEnt.SetRejectMessage(vbLf & "Selected entity is NOT an AEC object, try again...")

            ' "Geo" is the base class for AEC object.

            ' Use this if you want to apply to all the AEC objects.

            optEnt.AddAllowedClass(GetType(TinSurface), False)

            ' If you are interested in only Door object, use this instead.

            'optEnt.AddAllowedClass(GetType(Door), False) 



            Dim resEnt As PromptEntityResult = ed.GetEntity(optEnt)

            If resEnt.Status <> PromptStatus.OK Then

                ed.WriteMessage("Selection error - aborting")

                Exit Sub

            End If



            ' We have an object to attach a property set.



            Dim idTargetObj As DatabaseServices.ObjectId = resEnt.ObjectId



            ' (2) Ask for the name of property set definition to attach.



            Dim optStr As New PromptStringOptions(vbLf + "Enter the name of property set definition to attach.")



            Dim resPropSetDef As PromptResult = ed.GetString(optStr)

            If resPropSetDef.Status <> PromptStatus.OK Then

                ed.WriteMessage("String input error - aborting")

                Exit Sub

            End If



            ' We have the name of property set definition.



            Dim propSetDefName As String = resPropSetDef.StringResult



            ' Find the property set definition with the given name.



            Dim dictPropSetDef = New Autodesk.Aec.PropertyData.DatabaseServices.DictionaryPropertySetDefinitions(db)

            Dim idPropSetDef As DatabaseServices.ObjectId = Utils.findStyle(dictPropSetDef, propSetDefName)



            ' If idPropSetDef = Nothing Then Return



            ' If we come here, we have a prop set def id and an object id. 



            ' (3) Attach the given property set to the given object.



            Try

                Using tr As Transaction = db.TransactionManager.StartTransaction()



                    Dim obj As Object = tr.GetObject(idTargetObj, OpenMode.ForWrite, False, False)



                    ' PropertyDataServices provide a convenient method to do

                    ' the actual work.



                    Autodesk.Aec.PropertyData.DatabaseServices.PropertyDataServices.AddPropertySet(obj, idPropSetDef)



                    tr.Commit()



                End Using



            Catch ex As AutoCAD.Runtime.Exception

                ed.WriteMessage("error in AttachPropertySet: " + ex.ToString + vbCrLf)

            End Try



        End Sub

        Public Class Utils



            ' Helper function: findStyle().

            '

            ' Find a style (or dictionary record) with the given name

            ' from the given dictionary, and return its object id. 



            Public Shared Function findStyle(ByRef dict As Autodesk.Aec.DatabaseServices.Dictionary, ByVal key As String) As ObjectId



                Dim doc As Document = Application.DocumentManager.MdiActiveDocument

                Dim ed As Editor = doc.Editor

                Dim db As Database = doc.Database

                ' The id of the style we are looking for. Return value

                Dim id As ObjectId = Nothing



                Try

                    Using tr As Transaction = db.TransactionManager.StartTransaction()



                        ' Do we have a property set definition with the given name? 



                        If Not dict.Has(key, tr) Then

                            ' If not, return

                            ed.WriteMessage("cannot find the style: " + key + vbCrLf)
                            Return Nothing

                        End If



                        tr.Commit()

                    End Using



                    ' Get the id of property set definition from the name



                    id = dict.GetAt(key)



                Catch ex As AutoCAD.Runtime.Exception

                    ed.WriteMessage("error in findPropertySetDef: " + ex.ToString + vbCrLf)

                End Try



                Return id



            End Function



        End Class

    End Class

End Namespace
