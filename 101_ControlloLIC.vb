Imports System.IO


Module ControlloLIC

    Function CtrlLic(ByVal File_LIC As String) As String

        Dim fileReader As String = Nothing
        Dim LicFunc As New AutoCAD_CartigliAutomatici.ED_Licenza(password)

        ' MsgBox(strDeCode)
        Dim strMac As String = LicFunc.GetMacAddress()
        Dim strHDID As String = LicFunc.GetDriveSerialNumber()
        Dim strCPUID As String = LicFunc.GetCPU_ID()
        Dim data_anno As String = Date.Now.Year.ToString
        Dim Dominio As String = LicFunc.GetDomain()
        Dim NomepC As String = LicFunc.GetNomePC()

        If Dominio.Contains("PINI") Then

            strMail = strMail2
            TipoLIC = "PINI"
            GoTo a100
        Else
            strMail = strMail1

        End If

        Dim strCode As String = NomepC & "|" & Dominio & "|" & strHDID

        If File_LIC Is Nothing Or Not File.Exists(File_LIC) Then


            MsgBox("Non è stato trovato nessun file di licenza, richiedere un codice di licenza e registrare il prodotto.",, _caption)

            Dim strEnCode As String = LicFunc.TestEncoding(strCode)

            'FrmCode.TextBox1.Text = strEnCode

            'FrmCode.Show()



            TipoLIC = strEnCode
            GoTo a100
        Else
            fileReader = File.ReadAllText(File_LIC)
            If fileReader = "" Then
                MsgBox("Non è stato trovato nessun file di licenza, richiedere un codice di licenza e registrare il prodotto.",, _caption)

                Dim strEnCode As String = LicFunc.TestEncoding(strCode)

                'FrmCode.TextBox1.Text = strEnCode

                'FrmCode.Show()



                TipoLIC = strEnCode
                'MsgBox(fileReader)
                GoTo a100
            End If


        End If

        Dim strDeCode As String = LicFunc.TestDecoding(fileReader)

        If Left(strDeCode, 4) = "DEMO" Then


            Dim strDataDemo As String = Replace(Left(strDeCode, 14), "DEMO", "")
            '  MsgBox(strDataDemo)
            Dim DataDemo As Date = Date.ParseExact(strDataDemo, "dd/MM/yyyy",
                System.Globalization.DateTimeFormatInfo.InvariantInfo)

            Dim DiffGiorni As Integer
            DiffGiorni = DateDiff(DateInterval.Day, DataDemo, Date.Today)

            If My.Settings.dataUtilizzo = Nothing Then My.Settings.dataUtilizzo = Today

            If My.Settings.dataUtilizzo = Today Then

                If My.Settings.numEsecuzioni = 0 Then
                    MsgBox("Hai superato le attivazioni giornaliere",, _caption)

                    TipoLIC = "MAXATT"

                End If

            Else

                My.Settings.dataUtilizzo = Today
                My.Settings.numEsecuzioni = 4


            End If

            If DiffGiorni > 15 Then
                MsgBox("Periodo di prova scaduto, richiedere un codice di attivazione",, _caption)

                TipoLIC = "END"
            Else
                flagLic = True
                ' MsgBox("lic ok")
                TipoLIC = "DEMO"
                GoTo a100

                'GoTo a000

            End If
        End If



        strCode = NomepC & "|" & Dominio & "|" & ParteFissa & "|" & strHDID


        '  MsgBox(strCode)
        If strCode = strDeCode Then
            flagLic = True
            TipoLIC = "CODLIC"
            ' MsgBox("lic ok")

        Else
            MsgBox("Licenza non valida",, _caption & " Licenza")
            TipoLIC = "NOLIC" 'GoTo A200


        End If



a100:   Return TipoLIC
    End Function



End Module
