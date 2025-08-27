Imports System.Runtime.InteropServices
Imports SAPbobsCOM

Public Class clsSAP_Config


    Private Shared oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    ''' <summary>
    ''' Verifica si un campo de usuario existe en una tabla específica.
    ''' </summary>
    ''' <param name="tableName">Nombre de la tabla.</param>
    ''' <param name="fieldName">Nombre del campo de usuario.</param>
    ''' <returns>True si el campo existe, False en caso contrario.</returns>
    Public Shared Function CampoDeUsuarioExiste(tableName As String, fieldName As String) As Boolean

        Conectar_A_SAP(oCompany, pEmpresa.Killios)

        If Not oCompany.Connected Then
            Throw New Exception("Error al conectar: " & oCompany.GetLastErrorDescription())
            Return False
        End If

        Dim existeCampo As Boolean = False

        Try

            Dim oUserFieldsMD As UserFieldsMD = oCompany.GetBusinessObject(BoObjectTypes.oUserFields)
            existeCampo = oUserFieldsMD.GetByKey(tableName, fieldName)

        Catch ex As Exception
            Console.WriteLine("Error al verificar el campo: " & ex.Message)
            existeCampo = False

        Finally
            Desconectar_SAP(oCompany)
        End Try

        Return existeCampo

    End Function

    Public Shared Function Validar_Campo_Usuario(ByVal tableName As String,
                                                 ByVal fieldName As String,
                                                 ByVal description As String,
                                                 ByVal tipo_campo As BoFieldTypes,
                                                 ByVal editsize_tamaño_campo As Integer,
                                                 ByVal default_value As String) As Boolean

        Conectar_A_SAP(oCompany, pEmpresa.Killios)

        If Not oCompany.Connected Then
            Throw New Exception("Error al conectar: " & oCompany.GetLastErrorDescription())
            Return False
        End If

        Dim existeCampo As Boolean = False

        Try

            Dim query As String = $"SELECT COUNT(*) FROM CUFD WHERE TableID = '{tableName}' AND AliasID = '{fieldName}'"
            Dim recordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            recordset.DoQuery(query)

            If Not recordset.EoF AndAlso Convert.ToInt32(recordset.Fields.Item(0).Value) > 0 Then
                existeCampo = True
            End If

        Catch ex As Exception
            existeCampo = False
            Throw New Exception("Error al verificar el campo: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
        End Try

        Return existeCampo

    End Function

End Class
