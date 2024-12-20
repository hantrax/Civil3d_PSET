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





' This line is not mandatory, but improves loading performances


' This class is instantiated by AutoCAD for each document when
' a command is called by the user the first time in the context
' of a given document. In other words, non static data in this class
' is implicitly per-document!
Public Class SurfaceCant
    <CommandMethod("CreaSurf", CommandFlags.Modal)>
    Public Sub CreateSurf()



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


a000:   Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor
            Dim myDWG As ApplicationServices.Document
            Dim LockMode As Boolean = False
            myDWG = ApplicationServices.Application.DocumentManager.MdiActiveDocument
            myDWG.LockDocument()



            Dim cont As Integer = 1

            Dim peo As PromptEntityOptions = New PromptEntityOptions(vbCr & "Seleziona le polilinee chiuse ") ' seleziona polilinea asse principale
            peo.SetRejectMessage(vbCr & "Please select lightweight polyline only! ")
            peo.AddAllowedClass(GetType(Polyline2d), True)


            Dim acSSPrompt As PromptSelectionResult



            Using ts As Transaction = Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction()

                Dim per As PromptEntityResult = ed.GetEntity(peo)
                If per.Status <> PromptStatus.OK Then Exit Sub
                Dim poliLinea As Polyline2d = DirectCast(ts.GetObject(per.ObjectId, OpenMode.ForRead), Polyline2d)

                Dim acTypValAr(0) As TypedValue
                acTypValAr.SetValue(New TypedValue(DxfCode.Start, "POLYLINE"), 0)

                ' Assign the filter criteria to a SelectionFilter object
                Dim acSelFtr As SelectionFilter = New SelectionFilter(acTypValAr)
                Dim myPEO As New EditorInput.PromptEntityOptions("Seleziona le linee di sezioni CIVIL,,," & vbLf & "Invio per terminare.." & vbLf) ' seleziona linee di sezione

                acSSPrompt = ed.GetSelection(acSelFtr)

                Try

                    For Each sobj As SelectedObject In acSSPrompt.Value

                        Dim ent As DatabaseServices.Polyline2d = TryCast(ts.GetObject(sobj.ObjectId, OpenMode.ForWrite), DatabaseServices.Polyline2d)

                        Dim tinSurfaceId As ObjectId = TinSurface.Create(db, "Surface_" & cont)




                    Next





                Catch e As System.Exception

                End Try

                ts.Commit()
            End Using





a200:   End Sub


    End Class

