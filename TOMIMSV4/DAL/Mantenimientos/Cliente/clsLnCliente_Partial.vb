Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnCliente
    Implements IDisposable

    Public Shared Function ObtenerHH(ByRef oBeCliente As clsBeCliente,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Cliente " &
            " Where(IdCliente = @IdCliente)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                CargarHH(oBeCliente, dt.Rows(0))
            End If

            cmd.Dispose()
            dad.Dispose()

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub CargarHH(ByRef oBeCliente As clsBeCliente, ByRef dr As DataRow)

        Try

            With oBeCliente

                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdTipoCliente = IIf(IsDBNull(dr.Item("IdTipoCliente")), 0, dr.Item("IdTipoCliente"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Nombre_contacto = IIf(IsDBNull(dr.Item("nombre_contacto")), "", dr.Item("nombre_contacto"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Correo_electronico = IIf(IsDBNull(dr.Item("correo_electronico")), "", dr.Item("correo_electronico"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Realiza_manufactura = IIf(IsDBNull(dr.Item("realiza_manufactura")), False, dr.Item("realiza_manufactura"))
                .Despachar_lotes_completos = IIf(IsDBNull(dr.Item("Despachar_Lotes_Completos")), False, dr.Item("Despachar_Lotes_Completos"))
                .Tipo.IdTipoCliente = .IdTipoCliente
                clsLnCliente_tipo.Obtener(.Tipo)
                .Drecciones = clsLnCliente_direccion.GetAllDireccionesByCliente(.IdCliente)
                .Tiempos = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(.IdCliente)

            End With

        Catch ex As Exception
            Throw New Exception("LnCliente_Carga: " & ex.Message)
        End Try

    End Sub

    Public Shared Function GetAll(ByVal Activos As Boolean) As List(Of clsBeCliente)

        GetAll = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Const lSQl As String = "SELECT * FROM Cliente WHERE Activo = @Activos "

                Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@Activos", Activos)
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeCliente

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeCliente
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, Optional ByVal IdBodega As Integer = 0) As List(Of clsBeCliente)

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQl As String = "SELECT * FROM VW_Cliente WHERE 1 > 0 AND IdBodega=@IdBodega "

                If pActivo Then
                    lSQl += " AND Activo=1"
                Else
                    lSQl += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeCliente

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeCliente
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                Obj.Empresa = New clsBeEmpresa
                                Obj.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                Obj.Propietario = New clsBePropietarios
                                Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            End If

                            If lRow("Tipo Cliente") IsNot DBNull.Value AndAlso lRow("Tipo Cliente") IsNot Nothing Then
                                Obj.ClienteTipo = New clsBeCliente_tipo
                                Obj.ClienteTipo.NombreTipoCliente = CType(lRow("Tipo Cliente"), String)
                            End If

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro_DT(ByVal pActivo As Boolean, Optional ByVal IdBodega As Integer = 0) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQl As String = "SELECT 
                IdCliente as Correlativo, 
                Empresa as Empresa, 
                Propietario as Propietario, 
                [Tipo Cliente] as Tipo,
                Codigo as Código,
                nombre_comercial as Nombre,
                nombre_contacto as Contacto,
                nit as NIT,
                activo as Activo,
                sistema as Sistema,
                es_bodega_recepcion as Es_Bodega_RecepcionWMS,
                es_bodega_traslado as Es_Bodega_TrasladoWMS   
                FROM VW_Cliente WHERE 1 > 0 AND (IdBodega IS NULL OR IdBodega=@IdBodega) "

                If pActivo Then
                    lSQl += " AND Activo=1"
                Else
                    lSQl += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro_Seleccion(ByVal pActivo As Boolean, Optional ByVal IdBodega As Integer = 0) As List(Of clsBeCliente)

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQl As String = "SELECT * FROM VW_Cliente WHERE activo_bodega=1 AND IdBodega=@IdBodega "

                If pActivo Then
                    lSQl += " AND Activo=1"
                Else
                    lSQl += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(lSQl, lCnn)

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeCliente

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeCliente
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                Obj.Empresa = New clsBeEmpresa
                                Obj.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                Obj.Propietario = New clsBePropietarios
                                Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            End If

                            If lRow("Tipo Cliente") IsNot DBNull.Value AndAlso lRow("Tipo Cliente") IsNot Nothing Then
                                Obj.ClienteTipo = New clsBeCliente_tipo
                                Obj.ClienteTipo.NombreTipoCliente = CType(lRow("Tipo Cliente"), String)
                            End If

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro_Seleccion_DT(ByVal pActivo As Boolean,
                                                    Optional ByVal IdBodega As Integer = 0) As DataTable


        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lCnn.Open()

                Using lTransaction As SqlTransaction = lCnn.BeginTransaction()

                    Dim lSQl As String = "SELECT IdCliente as Correlativo, 
                                            Empresa as Empresa, 
                                            Propietario as Propietario, 
                                            [Tipo Cliente] as Tipo,
                                            Codigo as Código,
                                            nombre_comercial as Nombre,
                                            nombre_contacto as Contacto,
                                            activo as Activo,
                                            sistema as Sistema,
                                            es_bodega_recepcion as Es_Bodega_RecepcionWMS,
                                            es_bodega_traslado as Es_Bodega_TrasladoWMS
                                            FROM VW_Cliente WHERE IdBodega=@IdBodega "

                    If pActivo Then
                        lSQl += " AND Activo=1"
                    Else
                        lSQl += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(lSQl, lCnn)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lCnn.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK Agregué esta función para obtener los clientes por propietario

    Public Shared Function Get_All_Clientes_By_IdPropietario_And_IdBodega(ByVal pActivo As Boolean,
                                                                          ByVal IdPropietario As Integer,
                                                                          ByVal IdBodega As Integer,
                                                                          ByVal Requerir_Cliente_Es_Bodega_WMS As Boolean,
                                                                          Optional ByVal pEs_Proveedor As Boolean = False) As DataTable


        Try

            Using lConnecion As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnecion.Open()

                Using lTransaction As SqlTransaction = lConnecion.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lSQl As String = "SELECT IdCliente as Correlativo, 
                                          Empresa as Empresa, 
                                          Propietario as Propietario, 
                                          [Tipo Cliente] as Tipo,
                                          Codigo as Código,
                                          nombre_comercial as Nombre,
                                          nombre_contacto as Contacto,
                                          activo as Activo,
                                          sistema as Sistema,
                                          es_bodega_recepcion as Es_Bodega_RecepcionWMS,
                                          es_bodega_traslado as Es_Bodega_TrasladoWMS
                                          FROM VW_Cliente 
                                          WHERE IdBodega=@IdBodega 
                                          AND IdPropietario = @IdPropietario "

                    If pActivo Then
                        lSQl += " AND Activo=1"
                    Else
                        lSQl += " AND Activo=0"
                    End If

                    lSQl &= IIf(pEs_Proveedor, " AND es_proveedor = 1 ", " AND es_proveedor = 0 ")

                    Dim vControlBanderasCliente As Boolean = False

                    vControlBanderasCliente = clsLnBodega.Get_Control_Banderas_Cliente(IdBodega,
                                                                                       lConnecion,
                                                                                       lTransaction)

                    If vControlBanderasCliente Then

                        If Requerir_Cliente_Es_Bodega_WMS Then
                            lSQl += " AND es_bodega_recepcion = 1 And es_bodega_traslado = 1 And isnull(idubicacionvirtual,0) <> @IdBodega "
                        Else
                            lSQl += " AND es_bodega_recepcion = 0 And es_bodega_traslado = 0  "
                        End If

                    End If

                    Using lDTA As New SqlDataAdapter(lSQl, lConnecion)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Clientes_By_IdPropietario_And_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnecion.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Async Function GetSingleAsync(ByVal pIdCliente As Integer) As Task(Of clsBeCliente)

        Dim BeCliente As clsBeCliente = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM cliente WHERE IdCliente=@IdCliente"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Await lConnection.OpenAsync()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            BeCliente = New clsBeCliente()
                            Cargar(BeCliente, lRow)

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return BeCliente

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdCliente As Integer) As clsBeCliente

        GetSingle = Nothing

        Dim BeCliente As clsBeCliente = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM cliente WHERE IdCliente=@IdCliente "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            BeCliente = New clsBeCliente
                            Cargar(BeCliente, lRow)
                            GetSingle = BeCliente

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdCliente(ByVal pIdCliente As Integer) As String

        Get_Codigo_By_IdCliente = ""

        Try

            Dim vSQL As String = "SELECT CODIGO FROM cliente WHERE IdCliente=@IdCliente"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Get_Codigo_By_IdCliente = lRow("Codigo")

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdCliente_And_IdPropietario(ByVal pIdCliente As Integer,
                                                                     ByVal pIdPropietario As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeCliente

        Get_Single_By_IdCliente_And_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM cliente WHERE IdCliente=@IdCliente AND IdPropietario=@IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjC As New clsBeCliente()

                    Cargar(ObjC, lRow)

                    Get_Single_By_IdCliente_And_IdPropietario = ObjC

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdCliente_And_IdPropietario(ByVal pIdCliente As Integer, ByVal pIdPropietario As Integer) As clsBeCliente

        Get_Single_By_IdCliente_And_IdPropietario = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT TOP 1 * FROM cliente WHERE IdCliente=@IdCliente AND IdPropietario=@IdPropietario"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjC As New clsBeCliente()

                            Cargar(ObjC, lRow)

                            Get_Single_By_IdCliente_And_IdPropietario = ObjC

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Cliente_And_IdPropietario(ByVal pCodigoCliente As String, ByVal pIdPropietario As Integer) As clsBeCliente

        Get_Single_By_Codigo_Cliente_And_IdPropietario = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT TOP 1 * FROM cliente WHERE Codigo=@Codigo_Cliente AND IdPropietario=@IdPropietario"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Cliente", pCodigoCliente)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjC As New clsBeCliente()
                            Cargar(ObjC, lRow)
                            Get_Single_By_Codigo_Cliente_And_IdPropietario = ObjC

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Transaccion(ByVal pBeCliente As clsBeCliente,
                                          ByVal pClienteTiemposList As List(Of clsBeCliente_tiempos),
                                          ByVal pClienteBodegaList As List(Of clsBeCliente_bodega),
                                          ByVal pDireccionesEntregaCliente As List(Of clsBeCliente_direccion))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeCliente.IdCliente = 0 Then
                pBeCliente.IdCliente = MaxID(lConnection, lTransaction) + 1
                Insertar(pBeCliente, lConnection, lTransaction)
            Else
                Actualizar(pBeCliente, lConnection, lTransaction)
            End If

            Dim lMax As Integer = clsLnCliente_tiempos.MaxID(lConnection, lTransaction)

            If pClienteTiemposList IsNot Nothing AndAlso pClienteTiemposList.Count > 0 Then

                For Each Obj As clsBeCliente_tiempos In pClienteTiemposList

                    If Obj.IdCliente = 0 Then
                        lMax += 1
                        Obj.IdTiempoCliente = lMax
                        Obj.IdCliente = pBeCliente.IdCliente
                        clsLnCliente_tiempos.Insertar(Obj, lConnection, lTransaction)
                    Else
                        clsLnCliente_tiempos.Actualizar(Obj, lConnection, lTransaction)
                    End If

                Next

            End If

            lMax = 0
            lMax = clsLnCliente_bodega.MaxID(lConnection, lTransaction)

            If pClienteBodegaList IsNot Nothing AndAlso pClienteBodegaList.Count > 0 Then

                For Each Obj As clsBeCliente_bodega In pClienteBodegaList

                    If Obj.IdClienteBodega = 0 Then
                        lMax += 1
                        Obj.IdClienteBodega = lMax
                        Obj.IdCliente = pBeCliente.IdCliente
                        clsLnCliente_bodega.Insertar(Obj, lConnection, lTransaction)
                    Else
                        clsLnCliente_bodega.Actualizar(Obj, lConnection, lTransaction)
                    End If

                Next

            End If

            lMax = 0

            lMax = clsLnCliente_direccion.MaxID(pBeCliente.IdCliente, lConnection, lTransaction)

            If pDireccionesEntregaCliente IsNot Nothing AndAlso pDireccionesEntregaCliente.Count > 0 Then

                For Each Dir As clsBeCliente_direccion In pDireccionesEntregaCliente

                    If Dir.IdCliente = 0 Then
                        lMax += 1
                        Dir.IdDireccion = lMax
                        Dir.IdCliente = pBeCliente.IdCliente
                        clsLnCliente_direccion.Insertar(Dir, lConnection, lTransaction)
                    Else
                        clsLnCliente_direccion.Actualizar(Dir, lConnection, lTransaction)
                    End If

                Next

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Shared Function Existe_Cliente_By_IdCliente(ByVal pIdCliente As Integer,
                                                       ByRef Cnn As SqlConnection,
                                                       ByRef pTransaction As SqlTransaction) As Boolean

        Existe_Cliente_By_IdCliente = False

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE IdCliente=@IdCliente"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Existe_Cliente_By_IdCliente = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Cliente_By_Codigo(ByVal pCodigo As String,
                                                   ByRef Cnn As SqlConnection,
                                                   ByRef pTransaction As SqlTransaction) As Boolean

        Existe_Cliente_By_Codigo = False

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Existe_Cliente_By_Codigo = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String,
                                  ByRef Cnn As SqlConnection,
                                  ByRef pTransaction As SqlTransaction) As clsBeCliente

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeCliente()

                    Cargar(ObjUM, lRow)

                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String) As clsBeCliente

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo=@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjUM As New clsBeCliente()

                            Cargar(ObjUM, lRow)
                            Existe = ObjUM

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Bodega_Es_Valida_Para_Recepcion(ByVal pCodigoBodega As String, ByRef Cnn As SqlConnection, ByRef pTransaction As SqlTransaction) As Boolean

        Bodega_Es_Valida_Para_Recepcion = False

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo=@Codigo AND es_bodega_recepcion = 1"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigoBodega)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Bodega_Es_Valida_Para_Recepcion = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCliente),0) FROM cliente"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ActualizarFromInterface(ByRef oBeCliente As clsBeCliente, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("cliente")
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idtipocliente", "@idtipocliente", DataType.Parametro)
            Upd.Add("idubicacionmanufactura", "@idubicacionmanufactura", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            Upd.Add("nombre_contacto", "@nombre_contacto", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("realiza_manufactura", "@realiza_manufactura", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("despachar_lotes_completos", "@despachar_lotes_completos", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("es_bodega_recepcion", "@es_bodega_recepcion", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCliente.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeCliente.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCLIENTE", oBeCliente.IdTipoCliente))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONMANUFACTURA", oBeCliente.IdUbicacionManufactura))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeCliente.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", oBeCliente.Nombre_comercial))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CONTACTO", oBeCliente.Nombre_contacto))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeCliente.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeCliente.Nit))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeCliente.Direccion))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBeCliente.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente.Activo))
            cmd.Parameters.Add(New SqlParameter("@REALIZA_MANUFACTURA", oBeCliente.Realiza_manufactura))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_LOTES_COMPLETOS", oBeCliente.Despachar_lotes_completos))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeCliente.Sistema))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_RECEPCION", oBeCliente.Es_bodega_recepcion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllBySistema(ByVal Sistema As Boolean,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        GetAllBySistema = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM Cliente WHERE Sistema = @Sistema"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Sistema", Sistema)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll: " & ex.Message)
        End Try

    End Function

    '#CKFK 20210717 2342 Función creada para obtener clientes que no han sido creados como Bodega Virtual
    Public Shared Function GetAllNotBF(ByVal IdUbicacionVirtual As Integer,
                                       ByRef lConnection As SqlConnection,
                                       ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        GetAllNotBF = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM cliente INNER JOIN propietarios ON cliente.IdPropietario = propietarios.IdPropietario 
                                    WHERE cliente.codigo not like 'BG%' AND NOT Exists(
                                    SELECT p.IdPropietario FROM proveedor p 
                                                                        WHERE p.codigo like 'BF%' 
                                                                          AND p.es_bodega_traslado = 1
                                                                          AND p.es_bodega_recepcion = 1
                                                                          AND p.sistema = 1  
                                                                          AND p.idubicacionvirtual=@idubicacionvirtual
									                                      AND p.IdPropietario = propietarios.IdPropietario)"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idubicacionvirtual", IdUbicacionVirtual)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll: " & ex.Message)
        End Try

    End Function

    '#CKFK 20210717 2334 Función creada para obtener clientes que no han sido creados como Bodega Virtual
    Public Shared Function GetAllNotBG(ByVal IdUbicacionVirtual As Integer,
                                       ByRef lConnection As SqlConnection,
                                       ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        GetAllNotBG = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM cliente 
                                              WHERE codigo not like 'BG%' AND Not Exists(
                                                                                    SELECT c.IdCliente FROM Cliente C
                                                                                    WHERE c.codigo like 'BG%' 
                                                                                        AND c.es_bodega_traslado = 1
                                                                                        AND c.es_bodega_recepcion = 1
                                                                                        AND c.sistema = 1  
                                                                                        AND c.idubicacionvirtual=@idubicacionvirtual
									                                                    AND c.IdPropietario = cliente.IdPropietario)"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idubicacionvirtual", IdUbicacionVirtual)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByRef Codigo As String,
                                                ByVal pConection As SqlConnection,
                                                ByVal pTransaction As SqlTransaction) As clsBeCliente

        Get_Single_By_Codigo = Nothing

        Try

            Const sp As String = "SELECT * FROM cliente " &
            " Where(codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo", Codigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjUM As New clsBeCliente()

                Cargar(ObjUM, lRow)

                Get_Single_By_Codigo = ObjUM

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByRef Codigo As String) As clsBeCliente

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_Codigo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM cliente " &
            " Where(codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo", Codigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjUM As New clsBeCliente()

                Cargar(ObjUM, lRow)

                Get_Single_By_Codigo = ObjUM

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeCliente As clsBeCliente,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Cliente Where(IdCliente = @IdCliente)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeCliente, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_For_Combo(ByVal IdEmpresa As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction,
                                                          Optional ByVal EsSistema As Boolean = False) As DataTable

        Get_All_By_IdEmpresa_For_Combo = Nothing

        Dim vSQL As String = ""

        Try

            If EsSistema Then
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa and es_bodega_recepcion=1 AND Activo=1 and Sistema=1"
            Else
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa AND es_bodega_recepcion=1 AND Activo=1"
            End If

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Referencia_By_IdCliente(ByVal IdCliente As Integer) As String

        Get_Referencia_By_IdCliente = ""

        Try

            Const sp As String = "SELECT referencia FROM Cliente" &
            " Where(IdCliente = @IdCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", IdCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Referencia_By_IdCliente = IIf(IsDBNull(dt.Rows(0).Item("referencia")), "", dt.Rows(0).Item("referencia"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacionVirtual_By_Codigo(ByVal Codigo_Bodega As String) As Integer

        Get_IdUbicacionVirtual_By_Codigo = 0

        Try

            Const sp As String = "SELECT IdUbicacionVirtual FROM Cliente 
            Where (Codigo = @Codigo) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Bodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdUbicacionVirtual_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdUbicacionVirtual")), 0, dt.Rows(0).Item("IdUbicacionVirtual"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacionVirtual_By_Codigo(ByVal Codigo_Bodega As String,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Get_IdUbicacionVirtual_By_Codigo = 0

        Try

            Const sp As String = "SELECT IdUbicacionVirtual FROM Cliente 
                                  Where (Codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Bodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdUbicacionVirtual_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdUbicacionVirtual")), 0, dt.Rows(0).Item("IdUbicacionVirtual"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Cliente_Bodega(ByVal IdCliente As Integer, ByVal IdBodega As Integer) As Boolean

        Try

            Tiene_Cliente_Bodega = False

            Dim vSQL As String = "SELECT * from  cliente_bodega inner join 
            bodega on bodega.IdBodega = cliente_bodega.IdBodega 
            where cliente_bodega.IdCliente = @IdCliente and cliente_bodega.IdBodega=@IdBodega"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdCliente", IdCliente))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Tiene_Cliente_Bodega = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_BodegaVirtual(ByRef oBeCliente As clsBeCliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cliente")
            Upd.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", oBeCliente.IdUbicacionVirtual))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#CKFK20220325 Función creada para obtener lista de clientes activos con transacción
    Public Shared Function Get_All(ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_All = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM cliente 
                                    WHERE activo = 1 "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll: " & ex.Message)
        End Try

    End Function

    '#CKFK20220325 Función creada para obtener lista de clientes By IdCliente
    Public Shared Function Get_All_By_IdCliente(ByVal pIdCliente As Integer,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_All_By_IdCliente = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM cliente 
                                    WHERE IdCliente = @IdCliente "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Cliente_GetAll_By_IdCliente: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Clientes_By_IdPropietario_And_IdBodega(ByVal pActivo As Boolean,
                                                                          ByVal IdPropietario As Integer,
                                                                          ByVal pNIT As String,
                                                                          ByVal IdBodega As Integer,
                                                                          ByVal Requerir_Cliente_Es_Bodega_WMS As Boolean) As DataTable

        Get_All_Clientes_By_IdPropietario_And_IdBodega = Nothing

        Try

            Using lConnecion As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnecion.Open()

                Using lTransaction As SqlTransaction = lConnecion.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lSQl As String = "SELECT IdCliente as Correlativo, 
                                          Empresa as Empresa, 
                                          Propietario as Propietario, 
                                          [Tipo Cliente] as Tipo,
                                          Codigo as Código,
                                          nombre_comercial as Nombre,
                                          nombre_contacto as Contacto,
                                          activo as Activo,
                                          sistema as Sistema,
                                          es_bodega_recepcion as Es_Bodega_RecepcionWMS,
                                          es_bodega_traslado as Es_Bodega_TrasladoWMS
                                          FROM VW_Cliente 
                                          WHERE IdBodega=@IdBodega 
                                          AND (IdPropietario = @IdPropietario OR NIT = @NIT) "

                    If pActivo Then
                        lSQl += " AND Activo=1"
                    Else
                        lSQl += " AND Activo=0"
                    End If

                    Dim vControlBanderasCliente As Boolean = False

                    vControlBanderasCliente = clsLnBodega.Get_Control_Banderas_Cliente(IdBodega,
                                                                                       lConnecion,
                                                                                       lTransaction)

                    If vControlBanderasCliente Then

                        If Requerir_Cliente_Es_Bodega_WMS Then
                            lSQl += " AND es_bodega_recepcion = 1 And es_bodega_traslado = 1 And idubicacionvirtual <> @IdBodega "
                        Else
                            lSQl += " AND es_bodega_recepcion = 0 And es_bodega_traslado = 0  "
                        End If

                    End If

                    Using lDTA As New SqlDataAdapter(lSQl, lConnecion)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@NIT", pNIT)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Clientes_By_IdPropietario_And_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnecion.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20220902 Función creada para obtener clientes que no han sido creados como Propietarios
    Public Shared Function Get_Propietarios_No_Existentes_En_Clientes(ByRef lConnection As SqlConnection,
                                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBePropietarios)

        Get_Propietarios_No_Existentes_En_Clientes = Nothing

        Dim lReturnList As New List(Of clsBePropietarios)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM propietarios 
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS  NOT IN (SELECT nit FROM cliente) "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBePropietarios

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBePropietarios
                        clsLnPropietarios.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20220902 Función creada para obtener clientes que no han sido creados como Proveedores
    Public Shared Function Get_Proveedores_No_Existentes_En_Clientes(ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeProveedor)

        Get_Proveedores_No_Existentes_En_Clientes = Nothing

        Dim lReturnList As New List(Of clsBeProveedor)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM proveedor
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS  NOT IN (SELECT nit FROM cliente)"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProveedor

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProveedor
                        clsLnProveedor.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'GT13092022: Validar que clientes no tienen una bodega asignada en CEALSA.
    Public Shared Function Get_Clientes_No_Existentes_En_Bodega(IdBodega As Integer,
                                                                lConnection As SqlConnection,
                                                                lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_Clientes_No_Existentes_En_Bodega = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM cliente 
                                    WHERE IdCliente NOT IN 
                                    (SELECT IdCliente 
                                    FROM cliente_bodega WHERE IdBodega = @IdBodega) 
                                    AND codigo  not like 'BG%' AND codigo  not like 'BF%' "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception

        End Try
    End Function


    '#GT10082023: validar existencia de cliente por propietario

    Public Shared Function Get_Cliente_By_IdPropietario(ByRef pIdPropietario As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeCliente

        Get_Cliente_By_IdPropietario = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Dim vSQL As String = "SELECT * FROM cliente WHERE IdPropietario=@pIdPropietario "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(vSQL, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(vSQL, lConnection, lTransaction)
            End If

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdPropietario", pIdPropietario))

            Dim lDT As New DataTable
            dad.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                Dim lRow As DataRow = lDT.Rows(0)
                Dim BeCliente = New clsBeCliente()
                Cargar(BeCliente, lRow)
                Get_Cliente_By_IdPropietario = BeCliente
            End If


            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Cliente_Defecto_Pruebas(ByVal IdBodega As Integer,
                                                       ByVal IdPropietario As Integer,
                                                       ByVal IdUsuario As Integer,
                                                       ByVal lConection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As clsBeCliente

        Get_Cliente_Defecto_Pruebas = Nothing

        Dim BeCliente As New clsBeCliente()

        Try

            Const sp As String = " SELECT * FROM cliente 
                                   WHERE(Codigo = @Codigo_Cliente_Pruebas) "

            Dim cmd As New SqlCommand(sp, lConection) With {.CommandType = CommandType.Text, .Transaction = lTransaction}
            cmd.Parameters.AddWithValue("@Codigo_Cliente_Pruebas", clsCasosUsoReserva.Cliente_Pruebas)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Cargar(BeCliente, lRow)
                Get_Cliente_Defecto_Pruebas = BeCliente

            Else

                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(IdBodega, lConection, lTransaction)

                BeCliente.IdCliente = MaxID(lConection, lTransaction) + 1
                BeCliente.IdEmpresa = vIdEmpresa
                BeCliente.IdPropietario = IdPropietario
                BeCliente.IdTipoCliente = 1
                BeCliente.IdUbicacionManufactura = 0
                BeCliente.Codigo = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Nombre_comercial = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Nombre_contacto = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Telefono = 1010101
                BeCliente.Nit = "5388866-9"
                BeCliente.Direccion = ""
                BeCliente.Correo_electronico = "ejcalderon@dts.com.gt"
                BeCliente.Activo = True
                BeCliente.Realiza_manufactura = False
                BeCliente.User_agr = IdUsuario
                BeCliente.Fec_agr = Now
                BeCliente.User_mod = IdUsuario
                BeCliente.Fec_mod = Now
                BeCliente.Despachar_lotes_completos = False
                BeCliente.Sistema = True
                BeCliente.Es_bodega_recepcion = True
                BeCliente.Es_Bodega_Traslado = True
                BeCliente.IdUbicacionVirtual = 0
                BeCliente.Referencia = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Control_Ultimo_Lote = False
                BeCliente.Control_Calidad = False
                BeCliente.IdUbicacionAbastecerCon = 0
                Insertar(BeCliente, lConection, lTransaction)
                Get_Cliente_Defecto_Pruebas = BeCliente

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_For_Combo_QA(ByVal IdEmpresa As Integer,
                                                             Optional ByVal EsSistema As Boolean = False) As DataTable

        Get_All_By_IdEmpresa_For_Combo_QA = Nothing

        Dim vSQL As String = ""
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If EsSistema Then
                vSQL = "SELECT IdCliente,Codigo, nombre_comercial AS Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa and es_bodega_recepcion=1 AND Activo=1 and Sistema=1"
            Else
                vSQL = "SELECT IdCliente,Codigo, nombre_comercial AS Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa AND es_bodega_recepcion=1 AND Activo=1"
            End If

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Get_All_By_IdEmpresa_For_Combo_QA = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Cliente_Defecto_Pruebas(ByVal IdBodega As Integer,
                                                       ByVal IdPropietario As Integer,
                                                       ByVal IdUsuario As Integer) As clsBeCliente

        Get_Cliente_Defecto_Pruebas = Nothing

        Dim BeCliente As New clsBeCliente()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


            Const sp As String = " SELECT * FROM cliente 
                                   WHERE(Codigo = @Codigo_Cliente_Pruebas) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text, .Transaction = lTransaction}
            cmd.Parameters.AddWithValue("@Codigo_Cliente_Pruebas", clsCasosUsoReserva.Cliente_Pruebas)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Cargar(BeCliente, lRow)
                Get_Cliente_Defecto_Pruebas = BeCliente

            Else

                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(IdBodega, lConnection, lTransaction)

                BeCliente.IdCliente = MaxID(lConnection, lTransaction) + 1
                BeCliente.IdEmpresa = vIdEmpresa
                BeCliente.IdPropietario = IdPropietario
                BeCliente.IdTipoCliente = 1
                BeCliente.IdUbicacionManufactura = 0
                BeCliente.Codigo = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Nombre_comercial = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Nombre_contacto = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Telefono = 1010101
                BeCliente.Nit = "5388866-9"
                BeCliente.Direccion = ""
                BeCliente.Correo_electronico = "ejcalderon@dts.com.gt"
                BeCliente.Activo = True
                BeCliente.Realiza_manufactura = False
                BeCliente.User_agr = IdUsuario
                BeCliente.Fec_agr = Now
                BeCliente.User_mod = IdUsuario
                BeCliente.Fec_mod = Now
                BeCliente.Despachar_lotes_completos = False
                BeCliente.Sistema = True
                BeCliente.Es_bodega_recepcion = True
                BeCliente.Es_Bodega_Traslado = True
                BeCliente.IdUbicacionVirtual = 0
                BeCliente.Referencia = clsCasosUsoReserva.Cliente_Pruebas
                BeCliente.Control_Ultimo_Lote = False
                BeCliente.Control_Calidad = False
                BeCliente.IdUbicacionAbastecerCon = 0
                Insertar(BeCliente, lConnection, lTransaction)
                Get_Cliente_Defecto_Pruebas = BeCliente

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC202401311242:Obtener un cliente que se corresponda con la bodega destino de SAP.
    ''' </summary>
    ''' <param name="Codigo_Bodega"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_IdBodega_By_Codigo(ByVal Codigo_Bodega As String,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_Codigo = 0

        Try

            Const sp As String = "SELECT IdCliente FROM Cliente 
                                  Where (Codigo = @Codigo) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Bodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_IdBodega_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdCliente")), 0, dt.Rows(0).Item("IdCliente"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CKFK20240625 Función creada para obtener lista de clientes By filtro de Es_proveedor
    Public Shared Function Get_All_By_Es_Proveedor(ByVal pEs_Proveedor As Boolean,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_All_By_Es_Proveedor = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * FROM cliente 
                                    WHERE es_proveedor = @es_proveedor "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@es_proveedor", pEs_Proveedor)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK Agregué esta función para obtener los clientes por propietario
    Public Shared Function Get_All_Clientes_By_IdPropietario_And_IdBodega_EsProveedor(ByVal pActivo As Boolean,
                                                                                      ByVal IdPropietario As Integer,
                                                                                      ByVal IdBodega As Integer,
                                                                                      ByVal Requerir_Cliente_Es_Bodega_WMS As Boolean,
                                                                                      ByVal pEs_Proveedor As Boolean) As DataTable

        Try

            Using lConnecion As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnecion.Open()

                Using lTransaction As SqlTransaction = lConnecion.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lSQl As String = "SELECT IdCliente as Correlativo, 
                                          Empresa as Empresa, 
                                          Propietario as Propietario, 
                                          [Tipo Cliente] as Tipo,
                                          Codigo as Código,
                                          nombre_comercial as Nombre,
                                          nombre_contacto as Contacto,
                                          activo as Activo,
                                          sistema as Sistema,
                                          es_bodega_recepcion as Es_Bodega_RecepcionWMS,
                                          es_bodega_traslado as Es_Bodega_TrasladoWMS
                                          FROM VW_Cliente 
                                          WHERE IdBodega=@IdBodega 
                                          AND IdPropietario = @IdPropietario "

                    If pActivo Then
                        lSQl += " AND Activo=1"
                    Else
                        lSQl += " AND Activo=0"
                    End If

                    lSQl &= IIf(pEs_Proveedor, " AND es_proveedor = 1 ", " AND es_proveedor = 0 ")

                    Dim vControlBanderasCliente As Boolean = False

                    vControlBanderasCliente = clsLnBodega.Get_Control_Banderas_Cliente(IdBodega,
                                                                                       lConnecion,
                                                                                       lTransaction)

                    If vControlBanderasCliente Then

                        If Requerir_Cliente_Es_Bodega_WMS Then
                            lSQl += " AND es_bodega_recepcion = 1 And es_bodega_traslado = 1 And isnull(idubicacionvirtual,0) <> @IdBodega "
                        Else
                            lSQl += " AND es_bodega_recepcion = 0 And es_bodega_traslado = 0  "
                        End If

                    End If

                    Using lDTA As New SqlDataAdapter(lSQl, lConnecion)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Clientes_By_IdPropietario_And_IdBodega_EsProveedor = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnecion.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_Codigo(ByVal Codigo_Bodega As String) As Integer

        Get_IdBodega_By_Codigo = 0

        Try

            Const sp As String = "SELECT IdCliente FROM Cliente 
                                  Where (Codigo = @Codigo) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Bodega))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        Get_IdBodega_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdCliente")), 0, dt.Rows(0).Item("IdCliente"))
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_For_Combo_Trans(ByVal IdEmpresa As Integer, Optional ByVal EsSistema As Boolean = False,
                                                          Optional ByVal lConnection As SqlConnection = Nothing,
                                                          Optional ByVal lTransaction As SqlTransaction = Nothing) As DataTable

        Get_All_By_IdEmpresa_For_Combo_Trans = Nothing

        Dim vSQL As String = ""

        Try

            If EsSistema Then
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa and es_bodega_recepcion=1 AND Activo=1 and Sistema=1"
            Else
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa AND es_bodega_recepcion=1 AND Activo=1"
            End If

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdPropietario(ByVal pIdBodega As Integer,
                                             ByVal IdClienteBodega As Integer) As Integer

        Get_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT IdCliente FROM cliente_bodega 
                                  WHERE IdClienteBodega=@IdClienteBodega 
                                  AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdClienteBodega", IdClienteBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Return lDT.Rows(0).Item("IdCliente")
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_For_Combo(ByVal IdEmpresa As Integer, Optional ByVal EsSistema As Boolean = False) As DataTable

        Get_All_By_IdEmpresa_For_Combo = Nothing

        Dim vSQL As String = ""

        Try

            If EsSistema Then
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa and es_bodega_recepcion=1 AND Activo=1 and Sistema=1"
            Else
                vSQL = "SELECT IdCliente,CONCAT(Codigo,' ',nombre_comercial) as Nombre FROM Cliente WHERE IdEmpresa =@IdEmpresa AND es_bodega_recepcion=1 AND Activo=1"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdCliente(ByVal pIdCliente As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdCliente = ""

        Try

            Dim vSQL As String = "SELECT CODIGO FROM cliente WHERE IdCliente=@IdCliente"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_Codigo_By_IdCliente = lRow("Codigo")

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo_And_Company(ByVal pCodigo As String,
                                                        ByVal pCodigoEmpresa As String,
                                                        ByRef Cnn As SqlConnection,
                                                        ByRef pTransaction As SqlTransaction) As clsBeCliente

        Existe_By_Codigo_And_Company = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo=@Codigo AND Codigo_Empresa_ERP = @Codigo_Empresa_ERP"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Empresa_ERP", pCodigoEmpresa)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeCliente()
                    Cargar(ObjUM, lRow)
                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo_And_Company(ByVal pCodigo As String,
                                                        ByVal pCodigoEmpresa As String) As clsBeCliente

        Existe_By_Codigo_And_Company = Nothing

        Try
            Dim vSQL As String = "SELECT * FROM cliente WHERE Codigo = @Codigo AND Codigo_Empresa_ERP = @Codigo_Empresa_ERP"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Empresa_ERP", pCodigoEmpresa)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjUM As New clsBeCliente()
                            Cargar(ObjUM, lRow)
                            lTransaction.Commit()
                            Return ObjUM
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class