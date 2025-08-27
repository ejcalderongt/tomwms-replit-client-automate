' NOTE: You can use the "Rename" command on the context menu to change the class name "TareaHH" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select TareaHH.svc or TareaHH.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection

Public Class TareaHH
    Implements ITareaHH

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pBeTarea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Guardar(ByRef pBeTarea As clsBeTarea_hh) As Integer Implements ITareaHH.Guardar

        Guardar = 0

        Try

            If pBeTarea IsNot Nothing AndAlso pBeTarea.IdTareahh > 0 Then                
                If pBeTarea.IsNew Then
                    pBeTarea.IdTareahh = clsLnTarea_hh.MaxID() + 1
                    Guardar = clsLnTarea_hh.Insertar(pBeTarea)
                Else
                    Guardar = clsLnTarea_hh.Actualizar(pBeTarea)
                End If
            End If

            Return Guardar

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pBeTarea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Eliminar(ByRef pBeTarea As clsBeTarea_hh) As Integer Implements ITareaHH.Eliminar

        Eliminar = 0

        Try

            If pBeTarea IsNot Nothing AndAlso pBeTarea.IdTareahh > 0 Then

                If pBeTarea.IsNew = False Then
                    Eliminar = clsLnTarea_hh.Eliminar(pBeTarea)
                End If

            End If

            Return Eliminar

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pIdTareaHH"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Exists(ByVal pIdTareaHH As Integer) As Boolean Implements ITareaHH.Exists

        Try
            Return clsLnTarea_hh.Exists(pIdTareaHH)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pIdTareaHH"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSingle(ByVal pIdTareaHH As Integer) As clsBeTarea_hh Implements ITareaHH.GetSingle

        Try
            Return clsLnTarea_hh.GetSingle(pIdTareaHH)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>x
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll() As List(Of clsBeTarea_hh) Implements ITareaHH.GetAll

        Try
            Return clsLnTarea_hh.GetAll().ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetTarea(ByVal pIdBodega As Integer) As DataTable Implements ITareaHH.GetTarea

        Try
            Return clsLnTarea_hh.Get_Lista_Tareas_By_IdBodega(pIdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
