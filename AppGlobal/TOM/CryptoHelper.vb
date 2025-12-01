Imports System.Security.Cryptography
Imports System.Text

Public Class CryptoHelper

    Public Shared Function Encriptar(texto As String) As String
        If String.IsNullOrEmpty(texto) Then Return ""
        Dim bytes = Encoding.UTF8.GetBytes(texto)
        Dim protectedBytes = ProtectedData.Protect(bytes, Nothing, DataProtectionScope.CurrentUser)
        Return Convert.ToBase64String(protectedBytes)
    End Function

    Public Shared Function Desencriptar(textoEncriptado As String) As String
        If String.IsNullOrEmpty(textoEncriptado) Then Return ""
        Dim protectedBytes = Convert.FromBase64String(textoEncriptado)
        Dim bytes = ProtectedData.Unprotect(protectedBytes, Nothing, DataProtectionScope.CurrentUser)
        Return Encoding.UTF8.GetString(bytes)
    End Function

End Class
