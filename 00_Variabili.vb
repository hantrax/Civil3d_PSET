Imports System.Reflection
Imports Autodesk.AutoCAD.DatabaseServices


<Assembly: AssemblyVersion("2025.7.0.0")>
<Assembly: AssemblyFileVersion("2025.7.0.0")>

Module Variabili

    Public Initial_Dir As String = ""
    Public Const ANNO As Integer = 2023
    Public errore As Boolean = False
    Public nomeTXT As String = ""
    Public password As String = "!BatChEtrAnsMIT2023*!"
    Public AssVersion As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    Public _caption As String = "PSET from Excel - " & AssVersion
    Public _captionImp As String = "Import Parameters Values from Excel - " & AssVersion
    Public _captionEsp As String = "Export Parameters Values to Excel - " & AssVersion

    Public strMail As String = ""
    Public strMail1 As String = "stefano.carta74@gmail.com"
    Public strMail2 As String = "stefano.carta@pini.group"
    Public flagLic As Boolean = False
    Public TipoLIC As String = Nothing
    Public maxListdemo As Integer = 5
    Public maxEsecuzioniGiorno As Integer = 4
    Public labelDemotext As String = "DEMO 15gg - solo 10 file alla volta - Esecuzioni giornaliere rimaste: "

    Public Path_Assembly As String = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    Public Path_Image As String = Path_Assembly & "\Image\"

    Public ParteFissa As String = "!!*CartAutom2023*!!"
    Public NomeProgr As String = "ImportaPSET_Civil3D"
    Public File_LIC As String = "C:\ProgramData\" & NomeProgr & "\" & NomeProgr & "lic.dat"
    Public Path_AssemblyTXT As String = "C:\ProgramData\" & NomeProgr & "\"

    Public excelTemplate As String = Path_Assembly & "\pset_template.xlsx"
    Public ParamTemplate As String = Path_Assembly & "\param_template.xlsx"

    Public strPath As String = ""
    Public nomeCart As String = ""
    Public dwgFile As String = ""
    Public strNomeFile As String = ""
    Public LayoutTipo As String = ""

    Public strRibbon_Tab As String = "Auto PLUG-IN"
    Public strRibbon_Panel As String = "Civil 3D"
    Public lstParametri As New List(Of ListaParametri)
    Public lstErrore As New List(Of String)



    Public Structure ListaParametri
        Public pSet As String
        Public NParam As String
        Public tipoParam As String
        Public defText As String


    End Structure
    Public Structure ParametriVal
        Public HANDLEOBJ As Handle
        Public PsetName As String
        Public NParam As String
        Public tipoParam As String
        Public textVal As Object
        Public autoParam As Boolean
        Public objLayer As String

    End Structure

End Module
