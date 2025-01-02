Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Customization


<Assembly: CommandClass(GetType(Civil3d_PSET_2025.SurroundingClass))>
Namespace Civil3d_PSET_2025

    Class SurroundingClass
        <CommandMethod("RIBBpsetImp", CommandFlags.Session)>
        Public Shared Sub CreateRibbonTabAndPanel_Method()
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

            Try
                Dim cs As CustomizationSection = New CustomizationSection(CStr(Application.GetSystemVariable("MENUNAME")))
                Dim curWorkspace As String = CStr(Application.GetSystemVariable("WSCURRENT"))

                'Dim TabSrc1 As Boolean = GetRibbonTAB(strRibbon_Tab)

                ' If TabSrc1 = False Then
                'aggiungel tab
                CreateRibbonTabAndPanel(cs, curWorkspace, strRibbon_Tab, strRibbon_Panel)
                ' End If

                cs.Save()
                'aggiunge i bottoni
                AddRibbonButtonAndSeparator_Method()

                cs.Save()

                MsgBox("Ribbon aggiornato...chiudere e riaprire AutoCAD",, _caption)

            Catch ex As System.Exception
                ed.WriteMessage(Environment.NewLine & ex.Message)
            End Try
        End Sub

        Public Shared Sub CreateRibbonTabAndPanel(ByVal cs As CustomizationSection, ByVal toWorkspace As String, ByVal tabName As String, ByVal panelName As String)
            Dim root As RibbonRoot = cs.MenuGroup.RibbonRoot
            Dim panels As RibbonPanelSourceCollection = root.RibbonPanelSources


            Dim panelSrc1 As RibbonPanelSource = root.FindPanel(strRibbon_Panel & "_PanelSourceID")

            Dim panelSrc As RibbonPanelSource = Nothing

            If panelSrc1 Is Nothing Then panelSrc = New RibbonPanelSource(root)


            If panelSrc1 Is Nothing Then
                panelSrc.Text = CSharpImpl.__Assign(panelSrc.Name, panelName)
                panelSrc.ElementID = CSharpImpl.__Assign(panelSrc.Id, panelName & "_PanelSourceID")
                panels.Add(panelSrc)



            Else

                ' panelSrc = panelSrc1

            End If

            Dim TabSrc1 As RibbonTabSource = root.FindTab(strRibbon_Tab & "_TabSourceID")

            Dim TabSrc As RibbonTabSource = Nothing

            If TabSrc1 Is Nothing Then TabSrc = New RibbonTabSource(root)


            'Dim TabSrc1 As RibbonTabSource = GetRibbonTAB(strRibbon_Tab)

            If TabSrc1 Is Nothing Then

                TabSrc.Name = CSharpImpl.__Assign(TabSrc.Text, tabName)
                TabSrc.ElementID = CSharpImpl.__Assign(TabSrc.Id, tabName & "_TabSourceID")
                root.RibbonTabSources.Add(TabSrc)




            Else

                TabSrc = TabSrc1

            End If

            If panelSrc1 Is Nothing Then
                Dim ribPanelSourceRef As RibbonPanelSourceReference = New RibbonPanelSourceReference(TabSrc)
                ribPanelSourceRef.PanelId = panelSrc.ElementID
                TabSrc.Items.Add(ribPanelSourceRef)



            End If

            If TabSrc1 Is Nothing Then
                Dim tabSrcRef As WSRibbonTabSourceReference = WSRibbonTabSourceReference.Create(TabSrc)

                Dim curWsIndex As Integer = cs.Workspaces.IndexOfWorkspaceName(toWorkspace)
                Dim wsRibbonRoot As WSRibbonRoot = cs.Workspaces(curWsIndex).WorkspaceRibbonRoot
                tabSrcRef.SetParent(wsRibbonRoot)
                wsRibbonRoot.WorkspaceTabs.Add(tabSrcRef)

            End If


        End Sub

        Public Shared Function GetRibbonTAB(ByVal tabName As String)
            Dim ribCntrl As Autodesk.Windows.RibbonControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl
            Dim flag As Boolean = False
            Dim ribTAB As New Autodesk.Windows.RibbonTab

            For Each tab As Autodesk.Windows.RibbonTab In ribCntrl.Tabs
                If tab.Name = tabName Then

                    tab.IsActive = True
                    ribTAB = tab
                    flag = True
                    Exit For

                End If
            Next
            Return TAB()
        End Function

        Public Shared Function GetRibbonPAN(ByVal panName As String)
            Dim ribCntrl As Autodesk.Windows.RibbonControl = Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl
            Dim flag As Boolean = False

            Dim ctrPan As Autodesk.Windows.RibbonPanel = ribCntrl.FindPanel(panName, True)

            If ctrPan Is Nothing Then flag = False
            Return ctrPan
        End Function


        Private Class CSharpImpl

            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class

        Public Shared Sub AddRibbonButtonAndSeparator_Method()
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

            Try
                Dim cs As CustomizationSection = New CustomizationSection(CStr(Application.GetSystemVariable("MENUNAME")))
                Dim curWorkspace As String = CStr(Application.GetSystemVariable("WSCURRENT"))
                Dim panelSrc As RibbonPanelSource = GetRibbonPanel(cs, strRibbon_Panel)
                If panelSrc Is Nothing Then Return
                Dim macGroup As MacroGroup = cs.MenuGroup.MacroGroups(0)
                panelSrc.Items.Clear()
                Dim row As RibbonRow = New RibbonRow()
                panelSrc.Items.Add(row)


                Dim button1 As RibbonCommandButton = New RibbonCommandButton(row)
                button1.Text = "PSET from Excel"
                Dim menuMac1 As MenuMacro = macGroup.CreateMenuMacro("Pset Import", "^C^CPsetImpt ", "PsetImpt", "PSET Import from Excel", MacroType.Any, Path_Image & "creapset.ico", Path_Image & "creapset.ico", "creapset_Label_Id")
                button1.MacroID = menuMac1.ElementID
                button1.ButtonStyle = RibbonButtonStyle.LargeWithText
                button1.KeyTip = "Pset Import"
                button1.TooltipTitle = "Pset Import"
                row.Items.Add(button1)
                'Dim separator1 As RibbonSeparator = New RibbonSeparator(row)
                'separator1.SeparatorStyle = RibbonSeparatorStyle.Line
                'row.Items.Add(separator1)


                Dim button2 As RibbonCommandButton = New RibbonCommandButton(row)
                button2.Text = "Export to Excel"
                Dim menuMac2 As MenuMacro = macGroup.CreateMenuMacro("ExpParam", "^C^CExpParam ", "Export Parameters to Excel", "ExpParam", MacroType.Any, Path_Image & "esporta.png", Path_Image & "esporta.png", "esporta_Label_Id")
                button2.MacroID = menuMac2.ElementID
                button2.ButtonStyle = RibbonButtonStyle.LargeWithText
                button2.KeyTip = "Export Parameters to Excel"
                button2.TooltipTitle = "Export Parameters to Excel"
                row.Items.Add(button2)

                Dim button3 As RibbonCommandButton = New RibbonCommandButton(row)
                button3.Text = "Import from Excel"
                Dim menuMac3 As MenuMacro = macGroup.CreateMenuMacro("ImpParam", "^C^CImpParam ", "Import Parameters from Excel", "ImpParam", MacroType.Any, Path_Image & "importa.png", Path_Image & "importa.png", "importa_Label_Id")
                button3.MacroID = menuMac3.ElementID
                button3.ButtonStyle = RibbonButtonStyle.LargeWithText
                button3.KeyTip = "Import Parameters from Excel"
                button3.TooltipTitle = "Import parameters from Excel"
                row.Items.Add(button3)
                Dim separator3 As RibbonSeparator = New RibbonSeparator(row)
                separator3.SeparatorStyle = RibbonSeparatorStyle.Line


                Dim button4 As RibbonCommandButton = New RibbonCommandButton(row)
                button4.Text = "Copy Parameters"
                Dim menuMac4 As MenuMacro = macGroup.CreateMenuMacro("copyPar", "^C^CcopyPar ", "Copy Parameters values from object", "copyPar", MacroType.Any, Path_Image & "copyparam.ico", Path_Image & "copyparam.ico", "copyPar_Label_Id")
                button4.MacroID = menuMac4.ElementID
                button4.ButtonStyle = RibbonButtonStyle.LargeWithText
                button4.KeyTip = "Copy Parameters values"
                button4.TooltipTitle = "Copy Parameters values"
                row.Items.Add(button4)
                Dim separator4 As RibbonSeparator = New RibbonSeparator(row)
                separator4.SeparatorStyle = RibbonSeparatorStyle.Line

                cs.Save()
            Catch ex As System.Exception
                ed.WriteMessage(Environment.NewLine & ex.Message)
            End Try
        End Sub
        Public Shared Function GetRibbonPanel(ByVal cs As CustomizationSection, ByVal panelName As String) As RibbonPanelSource
            Dim csList As New System.Collections.ObjectModel.Collection(Of CustomizationSection)
            csList.Add(cs)
            Return RibbonRoot.FindPanelSource(csList, cs.MenuGroup.Name, panelName & "_PanelSourceID")
        End Function

    End Class


End Namespace

