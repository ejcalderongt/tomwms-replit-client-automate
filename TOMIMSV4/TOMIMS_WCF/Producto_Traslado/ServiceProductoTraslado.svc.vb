Imports System.Reflection

Public Class ServiceProductoTraslado
    Implements IServiceProductoTraslado


    Public Function MaxIDTraslado() As Integer Implements IServiceProductoTraslado.MaxIDTraslado
        Try
            Return clsLnTrans_tras_enc.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insert(ByRef ObjPF As clsBeTrans_tras_enc) As Integer Implements IServiceProductoTraslado.Insert
        Try            
            Return clsLnTrans_tras_enc.Insertar(ObjPF)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ObjPF As clsBeTrans_tras_enc) As Integer Implements IServiceProductoTraslado.Update
        Try            
            Return clsLnTrans_tras_enc.Actualizar(ObjPF)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Disable(ByRef ObjPF As clsBeTrans_tras_enc) As Integer Implements IServiceProductoTraslado.Disable
        Try            
            ObjPF.Activo = False
            Return clsLnTrans_tras_enc.Actualizar(ObjPF)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    'Public Sub GuardarTransaccion(pListObjPF As List(Of clsBeTrans_tras_enc)) Implements IServiceProductoTraslado.GuardarTransaccion

    'End Sub

    Public Function Get_All_Traslados(pActivo As Boolean, pFiltro As String) As DataTable Implements IServiceProductoTraslado.Get_All_Traslados
        Try
            Return clsLnTrans_tras_enc.ListarTraslados(pActivo, pFiltro)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
