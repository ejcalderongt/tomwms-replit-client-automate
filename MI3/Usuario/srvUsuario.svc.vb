' NOTE: You can use the "Rename" command on the context menu to change the class name "srvUsuario" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select srvUsuario.svc or srvUsuario.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class srvUsuario
    Implements IsrvUsuario

    Public Function Usuario_Valido_By_Obj(ByRef Usuario As clsBeUsuario) As Boolean Implements IsrvUsuario.Usuario_Valido_By_Obj

        Usuario_Valido_By_Obj = False

        Try

            If clsLnUsuario.Usuario_Valido_For_TMS_By_IdEmpresa(Usuario) Then
                Usuario_Valido_By_Obj = True
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Usuario_Valido_By_Params(ByVal pCodigo As String, ByVal pClave As String, ByVal pIdEmpresa As Integer) As Boolean Implements IsrvUsuario.Usuario_Valido_By_Params

        Usuario_Valido_By_Params = False

        Try

            Dim BeUsuario As New clsBeUsuario
            BeUsuario.Codigo = pCodigo
            BeUsuario.Clave = pClave
            BeUsuario.IdEmpresa = pIdEmpresa

            If clsLnUsuario.Usuario_Valido_For_TMS_By_IdEmpresa(BeUsuario) Then
                Usuario_Valido_By_Params = True
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


End Class
