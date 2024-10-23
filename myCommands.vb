' (C) Copyright 2011 by  
'
Imports System
Imports System.Windows.Controls
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.Civil.DatabaseServices
Imports Autodesk.Aec.ApplicationServices


' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(Civil3d_PSET_2025.MyCommands))>
Namespace Civil3d_PSET_2025

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class MyCommands

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
        <CommandMethod("InsPset1", CommandFlags.Session)>
        Public Sub AggiungiParametri()
            Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
            Dim acEd As Editor = acDoc.Editor
            Dim acCurDb As Database = acDoc.Database

            Dim civDoc As CivilDocument = CivilApplication.ActiveDocument

            Dim myDWG As ApplicationServices.Document

            myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
            myDWG.LockDocument()

            Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
                Dim opt As PromptEntityOptions = New PromptEntityOptions(vbLf & "Seleziona un solido 3D: ")
                opt.SetRejectMessage(vbLf & "Seleziona un oggetto valido.")
                opt.AddAllowedClass(GetType(Solid3d), True)
                Dim res As PromptEntityResult = acEd.GetEntity(opt)

                If res.Status = PromptStatus.OK Then
                    Dim solido3d As Solid3d = TryCast(acTrans.GetObject(res.ObjectId, OpenMode.ForWrite), Solid3d)

                    If solido3d IsNot Nothing Then
                        AddCustomPropertySet(solido3d, acTrans, "ParametroPersonalizzato", "Valore")
                        acEd.WriteMessage(vbLf & "Parametro aggiunto al solido 3D.")
                    End If
                End If

                acTrans.Commit()
                acTrans.Dispose()
                myDWG.LockDocument.Dispose()







            End Using


        End Sub

        Private Sub AddCustomPropertySet(ByVal solido As Solid3d, ByVal acTrans As Transaction, ByVal nomeParametro As String, ByVal valore As String)
            Dim extDict As DBDictionary = CType(solido.ExtensionDictionary.GetObject(OpenMode.ForWrite), DBDictionary)
            Dim propSetDict As DBDictionary

            If Not extDict.Contains("PropertySet") Then
                propSetDict = New DBDictionary()
                extDict.SetAt("PropertySet", propSetDict)
                acTrans.AddNewlyCreatedDBObject(propSetDict, True)
            Else
                propSetDict = CType(extDict.GetAt("PropertySet").GetObject(OpenMode.ForWrite), DBDictionary)
            End If

            If Not propSetDict.Contains(nomeParametro) Then
                Dim xRec As Xrecord = New Xrecord()
                xRec.Data = New ResultBuffer(New TypedValue(CInt(DxfCode.Text), valore))
                propSetDict.SetAt(nomeParametro, xRec)
                acTrans.AddNewlyCreatedDBObject(xRec, True)
            Else
                Dim xRec As Xrecord = CType(propSetDict.GetAt(nomeParametro).GetObject(OpenMode.ForWrite), Xrecord)
                xRec.Data = New ResultBuffer(New TypedValue(CInt(DxfCode.Text), valore))
            End If
        End Sub

        ' Modal Command with pickfirst selection
        <CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal + CommandFlags.UsePickSet)>
        Public Sub MyPickFirst() ' This method can have any name
            Dim result As PromptSelectionResult = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection()
            If (result.Status = PromptStatus.OK) Then
                ' There are selected entities
                ' Put your command using pickfirst set code here
            Else
                ' There are no selected entities
                ' Put your command code here
            End If
        End Sub

        ' Application Session Command with localized name
        <CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal + CommandFlags.Session)>
        Public Sub MySessionCmd() ' This method can have any name
            ' Put your command code here
        End Sub

        ' LispFunction is similar to CommandMethod but it creates a lisp 
        ' callable function. Many return types are supported not just string
        ' or integer.
        <LispFunction("MyLispFunction", "MyLispFunctionLocal")>
        Public Function MyLispFunction(ByVal args As ResultBuffer) ' This method can have any name
            ' Put your command code here

            ' Return a value to the AutoCAD Lisp Interpreter
            Return 1
        End Function

    End Class

End Namespace
