' NOTE: You can use the "Rename" command on the context menu to change the class name "Barras_Pallet" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Barras_Pallet.svc or Barras_Pallet.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class Barras_Pallet
    Implements IBarras_Pallet

    Public Sub Insert(ByVal pListBarras_Pallet As List(Of clsBeI_nav_barras_pallet)) Implements IBarras_Pallet.Insert

        Try

            clsLnI_nav_barras_pallet.Guardar_Transaccion(pListBarras_Pallet)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Sub Insert_Single(ByVal pBarra_Pallet As clsBeI_nav_barras_pallet) Implements IBarras_Pallet.Insert_Single

        Try

            clsLnI_nav_barras_pallet.Insertar(pBarra_Pallet)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Sub Update_Single(ByVal pBarra_Pallet As clsBeI_nav_barras_pallet) Implements IBarras_Pallet.Update_Single

        Try

            clsLnI_nav_barras_pallet.Actualizar(pBarra_Pallet)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Function Max_IdPallet() As Integer Implements IBarras_Pallet.Max_IdPallet

        Try

            Return clsLnI_nav_barras_pallet.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Existe(ByVal pCodigoBarraPallet As String) As Boolean Implements IBarras_Pallet.Existe

        Try

            Return clsLnI_nav_barras_pallet.Existe(pCodigoBarraPallet)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All_Activos(ByVal pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet) Implements IBarras_Pallet.Get_All_Activos

        Get_All_Activos = Nothing

        Try

            Dim List As New List(Of clsBeI_nav_barras_pallet)
            List = clsLnI_nav_barras_pallet.Get_All_Activos(pActivo)
            Return List.ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All_Pendientes_Recepcion(ByVal pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet) Implements IBarras_Pallet.Get_All_Pendientes_Recepcion

        Get_All_Pendientes_Recepcion = Nothing

        Try

            Dim List As New List(Of clsBeI_nav_barras_pallet)
            List = clsLnI_nav_barras_pallet.Get_All_Pendientes_Recepcion(pActivo)
            Return List.ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class