Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceCliente.
''' </summary>
Public Class ServiceCliente
    Implements IServiceCliente

    ''' <summary>
    ''' Inserts the specified Be cliente.    
    ''' </summary>
    ''' <param name="pBeCliente">Be cliente.</param>    
    ''' <param name="pClienteTiemposList">cliente tiempos list.</param>    
    ''' <param name="pClienteBodegaList">cliente bodega list.</param>
    ''' <param name="pDireccionesEntregaCliente">The p direcciones entrega cliente.</param>
    ''' <seealso cref="clsBeCliente">clsBeCliente</seealso>
    ''' <exception cref="FaultException">If fails while insert</exception>
    Public Sub Insert(ByVal pBeCliente As clsBeCliente,
                      ByVal pClienteTiemposList As List(Of clsBeCliente_tiempos),
                      ByVal pClienteBodegaList As List(Of clsBeCliente_bodega),
                      ByVal pDireccionesEntregaCliente As List(Of clsBeCliente_direccion)) _
                                  Implements IServiceCliente.Insert

        Try

            clsLnCliente.Guardar_Transaccion(pBeCliente,
                                             pClienteTiemposList,
                                             pClienteBodegaList,
                                             pDireccionesEntregaCliente)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Insert_s the single.
    ''' </summary>
    ''' <param name="pBeCliente">The p be cliente.</param>
    ''' <exception cref="System.ServiceModel.FaultException"></exception>
    Public Sub Insert_Single(ByVal pBeCliente As clsBeCliente) _
                                  Implements IServiceCliente.Insert_Single

        Try

            clsLnCliente.Insertar(pBeCliente)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Updates the single Client.
    ''' </summary>
    ''' <param name="pBeCliente">The Be client object.</param>
    ''' <returns></returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Update_Single(ByVal pBeCliente As clsBeCliente) As Integer _
                                  Implements IServiceCliente.Update_Single

        Update_Single = 0

        Try

            Return clsLnCliente.Actualizar(pBeCliente)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Disable the specified object of BeCliente, set the Activo flag to false.
    ''' </summary>    
    ''' <param name="pObjC">The BeCliente object.</param>
    ''' <see cref="clsBeCliente" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Disable(ByVal pObjC As clsBeCliente) Implements IServiceCliente.Disable

        Try
            pObjC.Activo = False
            clsLnCliente.Actualizar(pObjC)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Gets all Clients by filter.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo]. then only Active Clients apear.</param>
    ''' <returns>List(Of clsBeCliente).</returns>
    ''' <see cref="clsBeCliente" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filter(ByVal pActivo As Boolean) As List(Of clsBeCliente) Implements IServiceCliente.Get_All_Filter

        Try
            Dim List As New List(Of clsBeCliente)
            List = clsLnCliente.Get_All_Filtro(pActivo)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get the single Client filter by the Client Id and the product owner Id.
    ''' </summary>
    ''' <param name="pIdCliente">The identifier of cliente.</param>
    ''' <param name="pIdPropietario">The identifier of product owner.</param>
    ''' <returns>clsBeCliente.</returns>
    ''' <see cref="clsBeCliente" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_Propietario(ByVal pIdCliente As Integer, ByVal pIdPropietario As Integer) As clsBeCliente Implements IServiceCliente.Get_Single_By_Propietario

        Try

            Return clsLnCliente.Get_Single_By_IdCliente_And_IdPropietario(pIdCliente, pIdPropietario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get the single Client by Id.
    ''' </summary>
    ''' <param name="IdCliente">The identifier of client.</param>
    ''' <returns>A object of clsBeCliente.</returns>
    ''' <see cref="clsBeCliente" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdCliente(ByVal IdCliente As Integer) As clsBeCliente Implements IServiceCliente.Get_Single_By_IdCliente

        Try

            Return clsLnCliente.GetSingle(IdCliente)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Gets all the limit times rules by client.
    ''' </summary>
    ''' <param name="IdCliente">The identifier cliente.</param>
    ''' <returns>List(Of clsBeCliente_tiempos).</returns>
    ''' <see cref="clsBeProveedor_tiempos" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Tiempos_By_IdCliente(ByVal IdCliente As Integer) As List(Of clsBeCliente_tiempos) Implements IServiceCliente.Get_All_Tiempos_By_IdCliente

        Try
            Return clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(IdCliente)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get_s the all_ direcciones_ entrega_ by_ identifier cliente.
    ''' </summary>
    ''' <param name="pIdCliente">The p identifier cliente.</param>
    ''' <returns>List(Of clsBeCliente_direccion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Direcciones_Entrega_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_direccion) Implements IServiceCliente.Get_All_Direcciones_Entrega_By_IdCliente

        Try
            Return clsLnCliente_direccion.GetAllDireccionesByCliente(pIdCliente)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ bodegas_ by_ cliente.
    ''' </summary>
    ''' <param name="pIdCliente">The p identifier cliente.</param>
    ''' <returns>List(Of clsBeCliente_bodega).</returns>
    ''' <see cref="clsBeCliente_bodega" />
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Bodegas_By_Cliente(ByVal pIdCliente As Integer) As List(Of clsBeCliente_bodega) Implements IServiceCliente.Get_All_Bodegas_By_Cliente

        Try
            Return clsLnCliente_bodega.Get_All_By_IdCliente(pIdCliente)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Max_s the id_ dir_ entrega.
    ''' </summary>
    ''' <param name="pIdCliente">The p identifier cliente.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_Id_Dir_Entrega(ByVal pIdCliente As Integer) As Integer Implements IServiceCliente.Max_Id_Dir_Entrega

        Try

            Return clsLnCliente_direccion.MaxID(pIdCliente)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Maximums the identifier cliente.
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdCliente() As Integer Implements IServiceCliente.Max_IdCliente

        Try

            Return clsLnCliente.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Existe cliente
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe(Cliente As String) As clsBeCliente Implements IServiceCliente.Existe

        Try

            Return clsLnCliente.Existe(Cliente)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
