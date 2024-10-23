'Imports System.Management
Imports System.Security.Cryptography
Imports System.IO

Namespace AutoCAD_CartigliAutomatici

    Public NotInheritable Class ED_Licenza

        Private TripleDes As New TripleDESCryptoServiceProvider


        Public Function TestEncoding(stringa As String)

            Dim wrapper As New AutoCAD_CartigliAutomatici.ED_Licenza(password)
            Dim lic_encode As String = wrapper.EncryptData(stringa)

            Dim Path_Lic As String = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "Lic"
            ' My.Computer.FileSystem.WriteAllText(
            'My.Computer.FileSystem.SpecialDirectories.MyDocuments &
            '   "\lic.dat", lic_encode, False)
            Return lic_encode
        End Function
        Public Function TestDecoding(stringa As String)

            Dim dir As String = Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly.Location)



            Dim strDecode As String = Nothing
            Dim wrapper As New AutoCAD_CartigliAutomatici.ED_Licenza(password)

            ' DecryptData throws if the wrong password is used.
            Try
                Dim plainText As String = wrapper.DecryptData(stringa)
                strDecode = plainText
            Catch ex As System.Security.Cryptography.CryptographicException
                MsgBox("The data could not be decrypted with the password.",, _caption)
            End Try

            Return strDecode
        End Function

        Public Function GetDriveSerialNumber() As String
            Dim DriveSerial As Integer
            'Create a FileSystemObject object
            Dim fso As Object = CreateObject("Scripting.FileSystemObject")
            Dim Drv As Object = fso.GetDrive(fso.GetDriveName(System.Reflection.Assembly.GetExecutingAssembly.Location))

            With Drv
                If .IsReady Then
                    DriveSerial = .SerialNumber
                Else    '"Drive Not Ready!"
                    DriveSerial = -1
                End If
            End With
            Return DriveSerial.ToString("X2")
        End Function
        Public Function GetMacAddress() As String
            Dim cpuID As String = String.Empty
            Dim mc As Management.ManagementClass = New Management.ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As Management.ManagementObjectCollection = mc.GetInstances()
            For Each mo As Management.ManagementObject In moc
                If (cpuID = String.Empty And CBool(mo.Properties("IPEnabled").Value) = True) Then
                    cpuID = mo.Properties("MacAddress").Value.ToString()
                End If
            Next
            Return cpuID
        End Function
        Public Function GetNomePC() As String
            Dim PCNome As String = Environment.MachineName

            Return PCNome
        End Function
        Public Function GetDomain() As String

            Dim domainAndUserName As String = Environment.UserDomainName


            Return domainAndUserName
        End Function

        Public Function GetCPU_ID() As String

            Dim cpuID As String = String.Empty
            Dim mc As Management.ManagementClass = New Management.ManagementClass("Win32_Processor")
            Dim moc As Management.ManagementObjectCollection = mc.GetInstances()
            For Each mo As Management.ManagementObject In moc
                If (cpuID = String.Empty) Then
                    cpuID = mo.Properties("ProcessorId").Value.ToString()
                End If
            Next
            Return cpuID
        End Function
        Private Function TruncateHash(
           ByVal key As String,
           ByVal length As Integer) As Byte()

            Dim sha1 As New SHA1CryptoServiceProvider

            ' Hash the key.
            Dim keyBytes() As Byte =
                System.Text.Encoding.Unicode.GetBytes(key)
            Dim hash() As Byte = sha1.ComputeHash(keyBytes)

            ' Truncate or pad the hash.
            ReDim Preserve hash(length - 1)
            Return hash
        End Function
        Sub New(ByVal key As String)
            ' Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
        End Sub
        Public Function EncryptData(
             ByVal plaintext As String) As String

            ' Convert the plaintext string to a byte array.
            Dim plaintextBytes() As Byte =
                System.Text.Encoding.Unicode.GetBytes(plaintext)

            ' Create the stream.
            Dim ms As New System.IO.MemoryStream
            ' Create the encoder to write to the stream.
            Dim encStream As New CryptoStream(ms,
                TripleDes.CreateEncryptor(),
                System.Security.Cryptography.CryptoStreamMode.Write)

            ' Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
            encStream.FlushFinalBlock()

            ' Convert the encrypted stream to a printable string.
            Return Convert.ToBase64String(ms.ToArray)
        End Function
        Public Function DecryptData(
               ByVal encryptedtext As String) As String

            ' Convert the encrypted text string to a byte array.
            Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

            ' Create the stream.
            Dim ms As New System.IO.MemoryStream
            ' Create the decoder to write to the stream.
            Dim decStream As New CryptoStream(ms,
                TripleDes.CreateDecryptor(),
                System.Security.Cryptography.CryptoStreamMode.Write)

            ' Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
            decStream.FlushFinalBlock()

            ' Convert the plaintext stream to a string.
            Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
        End Function




    End Class
End Namespace
