Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.Civil.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.Aec.DatabaseServices
Imports Autodesk.Aec.PropertyData.DatabaseServices
Imports AecPropDb = Autodesk.Aec.PropertyData.DatabaseServices
Imports System.Collections.Specialized


Public Class PropertyGroupCreator
    <CommandMethod("CreatePropertyGroup")>
    Public Sub CreatePropertyGroup()
        ' Ottieni il documento attivo e il database
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ps As PropertySetDefinition = New PropertySetDefinition()
            Dim className As String = RXObject.GetClass(GetType(Line)).Name
            Dim propManual As PropertyDefinition = New PropertyDefinition()
            propManual.SetToStandard(db)
            propManual.SubSetDatabaseDefaults(db)
            propManual.Name = "Manual Prop"
            propManual.Description = "Manual property"
            propManual.DataType = Autodesk.Aec.PropertyData.DataType.Text
            propManual.DefaultData = "Default"
            ps.Definitions.Add(propManual)
            Dim appliedTo As StringCollection = New StringCollection()
            appliedTo.Add(className)
            ps.SetAppliesToFilter(appliedTo, False)
            Dim ds As DictionaryPropertySetDefinitions = New DictionaryPropertySetDefinitions(db)
            Dim oTableName As String = "TableTest"
            ds.AddNewRecord(oTableName, ps)
            trans.AddNewlyCreatedDBObject(ps, True)

        End Using




        ' Avvia una transazione

    End Sub
End Class