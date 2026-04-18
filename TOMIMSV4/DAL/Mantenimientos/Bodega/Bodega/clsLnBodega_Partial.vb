Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnBodega
    Implements IDisposable

    Public Shared Function GetSectores(ByRef listAreas As List(Of clsBeBodega_area)) As List(Of clsBeBodega_sector)

        GetSectores = Nothing

        Try

            Dim MainlSectores As New List(Of clsBeBodega_sector)
            Dim lSectores As New List(Of clsBeBodega_sector)

            'Cargar todos los sectors
            Dim vBeSector As New clsBeBodega_sector

            Dim Dt As New DataTable
            Dt = clsLnBodega_sector.Listar()

            Parallel.ForEach(Dt.AsEnumerable, Sub(ByVal lrow)
                                                  SyncLock lSectores
                                                      vBeSector = New clsBeBodega_sector
                                                      clsLnBodega_sector.Cargar(vBeSector, lrow)
                                                      lSectores.Add(vBeSector)
                                                  End SyncLock
                                              End Sub)

            Parallel.ForEach(listAreas, Sub(ByVal A)
                                            SyncLock MainlSectores
                                                For Each S As clsBeBodega_sector In lSectores.FindAll(Function(b) b.IdArea = A.IdArea)
                                                    MainlSectores.Add(S)
                                                Next
                                            End SyncLock
                                        End Sub)

            Return MainlSectores

        Catch ex As Exception
            Throw New Exception("BodegaGetSectroes: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSectores(ByRef listAreas As List(Of clsBeBodega_area),
                                       ByVal pIdBodega As Integer,
                                       ByRef lTransaction As SqlTransaction,
                                       ByRef lConnection As SqlConnection) As List(Of clsBeBodega_sector)

        GetSectores = Nothing

        Try

            Dim MainlSectores As New List(Of clsBeBodega_sector)
            Dim lSectores As New List(Of clsBeBodega_sector)

            'Cargar todos los sectors
            Dim vBeSector As New clsBeBodega_sector

            Dim Dt As New DataTable
            Dt = clsLnBodega_sector.Listar(pIdBodega, lTransaction, lConnection)

            Parallel.ForEach(Dt.AsEnumerable, Sub(ByVal lrow)
                                                  SyncLock lSectores
                                                      vBeSector = New clsBeBodega_sector
                                                      clsLnBodega_sector.Cargar(vBeSector, lrow)
                                                      lSectores.Add(vBeSector)
                                                  End SyncLock
                                              End Sub)

            Parallel.ForEach(listAreas, Sub(ByVal A)
                                            SyncLock MainlSectores
                                                For Each S As clsBeBodega_sector In lSectores.FindAll(Function(b) b.IdArea = A.IdArea)
                                                    MainlSectores.Add(S)
                                                Next
                                            End SyncLock
                                        End Sub)

            Return MainlSectores

        Catch ex As Exception
            Throw New Exception("BodegaGetSectroes: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetTramos(ByRef listSectores As List(Of clsBeBodega_sector),
                                     ByVal pIdBodega As Integer,
                                     ByRef lTransaction As SqlTransaction,
                                     ByRef lConnection As SqlConnection) As List(Of clsBeBodega_tramo)

        GetTramos = Nothing

        Try

            Dim MainlTramos As New List(Of clsBeBodega_tramo)
            Dim lTramos As New List(Of clsBeBodega_tramo)
            Dim vBeTramo As New clsBeBodega_tramo
            Dim Dt As New DataTable

            Dt = clsLnBodega_tramo.Listar(pIdBodega, lConnection, lTransaction)

            For Each T As DataRow In Dt.Rows
                vBeTramo = New clsBeBodega_tramo
                clsLnBodega_tramo.Cargar(vBeTramo, T)
                vBeTramo.pFont = New clsBeFont_Enc
                vBeTramo.pFont.IdFontEnc = vBeTramo.IdFontEnc
                vBeTramo.pFont = clsLnFont_enc.GetSingleByIdFontEnc(vBeTramo.IdFontEnc, lConnection, lTransaction)
                lTramos.Add(vBeTramo)
            Next

            For Each S As clsBeBodega_sector In listSectores

                For Each T As clsBeBodega_tramo In lTramos.FindAll(Function(b) b.IdSector = S.IdSector)
                    MainlTramos.Add(T)
                Next

            Next

            Dt.Dispose()

            Return MainlTramos

        Catch ex As Exception
            Throw New Exception("BodegaGetTramos: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetUbicaciones(ByRef listTramos As List(Of clsBeBodega_tramo),
                                          ByRef lTransaction As SqlTransaction,
                                          ByRef lConnection As SqlConnection) As List(Of clsBeBodega_ubicacion)

        GetUbicaciones = Nothing

        Try

            Dim MainlUbicacion As New List(Of clsBeBodega_ubicacion)
            Dim lUbicacion As New List(Of clsBeBodega_ubicacion)
            Dim DT As New DataTable
            Dim vBeUbicacion As New clsBeBodega_ubicacion
            Dim i As Integer = 1

            '#EJC20180411:Se reestructuró ésta función por optimización
            'Una copia en comentario se encuntra abajo.
            For Each T As clsBeBodega_tramo In listTramos

                Try
                    DT = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo_And_IdBodega_DT(T.IdTramo,
                                                                                              T.IdBodega,
                                                                                              lConnection,
                                                                                              lTransaction)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, MethodBase.GetCurrentMethod.Name())
                End Try

                For Each U As DataRow In DT.Rows
                    vBeUbicacion = New clsBeBodega_ubicacion
                    clsLnBodega_ubicacion.Cargar(vBeUbicacion, U, lTransaction, lConnection)
                    vBeUbicacion.Descripcion = IIf(IsDBNull(U.Item("NOMBRE_COMPLETO")), "", U.Item("NOMBRE_COMPLETO"))
                    vBeUbicacion.Tramo = T
                    lUbicacion.Add(vBeUbicacion)
                Next

                i = 0

            Next

            For Each T As clsBeBodega_tramo In listTramos
                For Each U As clsBeBodega_ubicacion In lUbicacion.FindAll(Function(b) b.IdTramo = T.IdTramo)
                    MainlUbicacion.Add(U)
                Next
            Next

            Return MainlUbicacion

        Catch ex As Exception
            Throw New Exception("BodegaGetTramos: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetMuelles() As List(Of clsBeBodega_muelles)
        Try

            Dim lMuelle As New List(Of clsBeBodega_muelles)
            Dim DT As New DataTable
            Dim vBeMuelle As New clsBeBodega_muelles

            For Each U As DataRow In DT.Rows
                vBeMuelle = New clsBeBodega_muelles
                clsLnBodega_muelles.Cargar(vBeMuelle, U)
                lMuelle.Add(vBeMuelle)
            Next

            Return lMuelle

        Catch ex As Exception
            Throw New Exception("BodegaGetTramos: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario(ByVal IdEmpresa As Integer,
                                                              ByVal IdUsuario As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT b.IdBodega as Codigo, 
                                  CONVERT(NVARCHAR(50),b.IdBodega) + ' - ' + b.nombre as Nombre 
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdEmpresa_And_IdUsuario = dt

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario_MI3(ByVal IdEmpresa As Integer, ByVal IdUsuario As Integer) As List(Of clsBodegasUsuarioRes)

        Try

            Dim vSQL As String = "SELECT b.IdBodega as Codigo, 
                                  CONVERT(NVARCHAR(50),b.IdBodega) + ' - ' + b.nombre as Nombre 
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lista As List(Of clsBodegasUsuarioRes) = New List(Of clsBodegasUsuarioRes)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each lRow As DataRow In dt.Rows
                    Dim tmp As clsBodegasUsuarioRes = New clsBodegasUsuarioRes()

                    If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                        tmp.Codigo = CType(lRow("Codigo"), Integer)
                    End If
                    If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                        tmp.Nombre = CType(lRow("Nombre"), String)
                    End If

                    lista.Add(tmp)

                Next
            End If

            Get_All_By_IdEmpresa_And_IdUsuario_MI3 = lista

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetAllByEmpresaCopy(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)
        Try

            Dim vSQL As String = "SELECT * FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Dim vBeBodega As New clsBeBodega
            Dim lBodegas As New List(Of clsBeBodega)

            For Each Dr As DataRow In dt.Rows

                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, Dr)
                vBeBodega.Empresa.IdEmpresa = vBeBodega.IdEmpresa
                vBeBodega.Empresa.Nombre = clsLnEmpresa.GetNombreEmpresa(vBeBodega.IdEmpresa)
                lBodegas.Add(vBeBodega)

            Next

            Return lBodegas

        Catch ex As Exception
            Throw New Exception("ListarBodega: " & ex.Message)
        End Try

    End Function

    'Public Shared Function Get_All_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        Dim vSQL As String = "SELECT * FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1"
    '        Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        Dim vBeBodega As New clsBeBodega
    '        Dim lBodegas As New List(Of clsBeBodega)

    '        For Each Dr As DataRow In dt.Rows

    '            vBeBodega = New clsBeBodega
    '            Cargar(vBeBodega, Dr)
    '            vBeBodega.Empresa.IdEmpresa = vBeBodega.IdEmpresa
    '            vBeBodega.Empresa.Nombre = clsLnEmpresa.GetNombreEmpresa(vBeBodega.IdEmpresa)
    '            lBodegas.Add(vBeBodega)

    '        Next

    '        lTransaction.Commit()

    '        Return lBodegas

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '        If lConnection IsNot Nothing Then lConnection.Dispose()
    '    End Try

    'End Function

    Public Shared Function Android_Get_All_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT *
                                  FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega
            Dim lBodegas As New List(Of clsBeBodega)

            For Each Dr As DataRow In dt.Rows
                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, Dr)
                lBodegas.Add(vBeBodega)
            Next

            lTransaction.Commit()

            Return lBodegas

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal IdPropietario As Integer) As List(Of clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT bodega.* "
            vSQL += " FROM bodega INNER JOIN propietario_bodega ON bodega.IdBodega = propietario_bodega.IdBodega"
            vSQL += " INNER JOIN  propietarios ON propietarios.IdPropietario = propietario_bodega.IdPropietario"
            vSQL += " WHERE bodega.activo = 1 AND propietarios.activo=1 AND propietario_bodega.IdPropietario = @IdPropietario"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega
            Dim lBodegas As New List(Of clsBeBodega)

            For Each Dr As DataRow In dt.Rows

                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, Dr)
                lBodegas.Add(vBeBodega)

            Next

            lTransaction.Commit()

            cmd.Dispose()
            dad.Dispose()

            Get_All_By_IdPropietario = lBodegas

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#EJC20220314: Utilizar un cargar universal.
    'Public Shared Sub CargarHH(ByRef oBeBodega As clsBeBodega, ByRef dr As DataRow)

    '    Try

    '        With oBeBodega

    '            .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
    '            .IdPais = IIf(IsDBNull(dr.Item("IdPais")), 0, dr.Item("IdPais"))
    '            .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
    '            .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
    '            .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
    '            .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
    '            .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
    '            .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
    '            .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
    '            .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
    '            .Encargado = IIf(IsDBNull(dr.Item("encargado")), "", dr.Item("encargado"))
    '            .Ubic_recepcion = IIf(IsDBNull(dr.Item("ubic_recepcion")), "", dr.Item("ubic_recepcion"))
    '            .Ubic_picking = IIf(IsDBNull(dr.Item("ubic_picking")), "", dr.Item("ubic_picking"))
    '            .Ubic_despacho = IIf(IsDBNull(dr.Item("ubic_despacho")), "", dr.Item("ubic_despacho"))
    '            .Ubic_merma = IIf(IsDBNull(dr.Item("ubic_merma")), "", dr.Item("ubic_merma"))
    '            .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
    '            .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
    '            .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
    '            .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
    '            .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
    '            .Coordenada_x = IIf(IsDBNull(dr.Item("coordenada_x")), "", dr.Item("coordenada_x"))
    '            .Coordenada_y = IIf(IsDBNull(dr.Item("coordenada_y")), "", dr.Item("coordenada_y"))
    '            .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
    '            .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
    '            .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
    '            .Reservar_stocks_por_linea = IIf(IsDBNull(dr.Item("reservar_stocks_por_linea")), False, dr.Item("reservar_stocks_por_linea"))
    '            .Rechazar_pedido_por_stock = IIf(IsDBNull(dr.Item("rechazar_pedido_por_stock")), False, dr.Item("rechazar_pedido_por_stock"))
    '            .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), "", dr.Item("IdTipoTransaccion"))
    '            .Zoom = IIf(IsDBNull(dr.Item("zoom")), 0.0, dr.Item("zoom"))
    '            .IdMotivoUbicacionDañadoPicking = IIf(IsDBNull(dr.Item("IdMotivoUbicacionDañadoPicking")), 0, dr.Item("IdMotivoUbicacionDañadoPicking"))
    '            .cambio_ubicacion_auto = IIf(IsDBNull(dr.Item("cambio_ubicacion_auto")), False, dr.Item("cambio_ubicacion_auto"))
    '            .codigo_bodega_erp = IIf(IsDBNull(dr.Item("codigo_bodega_erp")), "", dr.Item("codigo_bodega_erp"))
    '            .ubic_producto_ne = IIf(IsDBNull(dr.Item("ubic_producto_ne")), 0, dr.Item("ubic_producto_ne"))
    '            .IdProductoEstadoNE = IIf(IsDBNull(dr.Item("IdProductoEstadoNE")), 0, dr.Item("IdProductoEstadoNE"))

    '        End With

    '    Catch ex As Exception
    '        Throw New Exception("Bodega_Cargar: " & ex.Message)
    '    End Try

    'End Sub

    '#EJC20220314: Utilizar un cargar universal.
    'Public Shared Sub Android_Cargar(ByRef oBeBodega As clsBeBodegaBase, ByRef dr As DataRow)

    '    Try

    '        With oBeBodega

    '            .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
    '            .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
    '            .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
    '            .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
    '            .bloquear_lp_hh = IIf(IsDBNull(dr.Item("bloquear_lp_hh")), False, dr.Item("bloquear_lp_hh"))
    '            .captura_estiba_ingreso = IIf(IsDBNull(dr.Item("captura_estiba_ingreso")), False, dr.Item("captura_estiba_ingreso"))
    '            .captura_pallet_no_estandar = IIf(IsDBNull(dr.Item("captura_pallet_no_estandar")), False, dr.Item("captura_pallet_no_estandar"))
    '            .priorizar_ubicrec_sobre_ubicest = IIf(IsDBNull(dr.Item("priorizar_ubicrec_sobre_ubicest")), False, dr.Item("priorizar_ubicrec_sobre_ubicest"))
    '            .ubic_merma = IIf(IsDBNull(dr.Item("ubic_merma")), "", dr.Item("ubic_merma"))
    '            .validar_disponibilidad_ubicaicon_destino = IIf(IsDBNull(dr.Item("validar_disponibilidad_ubicaicon_destino")), False, dr.Item("validar_disponibilidad_ubicaicon_destino"))
    '            .ubic_producto_ne = IIf(IsDBNull(dr.Item("ubic_producto_ne")), 0, dr.Item("ubic_producto_ne")) '#AT 20220126  Agregué ubic_producto_ne, IdProductoEstadoNE
    '            .IdProductoEstadoNE = IIf(IsDBNull(dr.Item("IdProductoEstadoNE")), 0, dr.Item("IdProductoEstadoNE"))
    '            .Mostrar_Area_En_HH = IIf(IsDBNull(dr.Item("mostrar_area_en_hh")), False, dr.Item("mostrar_area_en_hh"))
    '            .confirmar_codigo_en_picking = IIf(IsDBNull(dr.Item("confirmar_codigo_en_picking")), False, dr.Item("confirmar_codigo_en_picking"))
    '            .inferir_origen_en_cambio_ubic = IIf(IsDBNull(dr.Item("confirmar_codigo_en_picking")), False, dr.Item("confirmar_codigo_en_picking"))


    '        End With

    '    Catch ex As Exception
    '        Throw New Exception("Bodega_Cargar: " & ex.Message)
    '    End Try

    'End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdBodega),0) FROM bodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetBodega(ByVal pIdBodega As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1625pm: Quité String.Format.
                Dim vSQL As String = "SELECT * FROM bodega WHERE IdBodega=@IdBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)
                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdEmpresa_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdEmpresa_By_IdBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdEmpresa FROM bodega WHERE IdBodega=@IdBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable.Rows.Count > 0 Then
                        Get_IdEmpresa_By_IdBodega = IIf(IsDBNull(lDataTable.Rows(0).Item("IdEmpresa")), "", lDataTable.Rows(0).Item("IdEmpresa"))
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_CodigoBodega(ByVal pCodBodega As String) As Integer

        Get_IdBodega_By_CodigoBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE codigo=@codigo"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable.Rows.Count > 0 Then
                        Get_IdBodega_By_CodigoBodega = IIf(IsDBNull(lDataTable.Rows(0).Item("IdBodega")), "0", lDataTable.Rows(0).Item("IdBodega"))
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_CodigoBodega(ByVal pCodBodega As String,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_CodigoBodega = 0

        Try

            Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE codigo=@codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Get_IdBodega_By_CodigoBodega = IIf(IsDBNull(lDataTable.Rows(0).Item("IdBodega")), "0", lDataTable.Rows(0).Item("IdBodega"))
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All(ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega)

        Dim lReturnList As New List(Of clsBeBodega)
        Dim vBeBodega As New clsBeBodega

        Try

            Dim vSQL As String = "SELECT * FROM Bodega WHERE Activo = 1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    For Each lRow As DataRow In lDataTable.Rows
                        vBeBodega = New clsBeBodega
                        Cargar(vBeBodega, lRow)
                        lReturnList.Add(vBeBodega)
                    Next
                End If

                Return lReturnList

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Bodega_By_IdBodega(ByVal pIdBodega As Integer) As String

        Get_Nombre_Bodega_By_IdBodega = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Dim vSQL As String = "SELECT concat(CODIGO, ' - ', Nombre) as Nombre FROM bodega WHERE IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            Get_Nombre_Bodega_By_IdBodega = IIf(IsDBNull(lDataTable.Rows(0).Item("Nombre")), "", lDataTable.Rows(0).Item("Nombre"))
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

    Public Shared Function Get_Codigo_Bodega_By_Nombre_Bodega(ByVal pBodega As String,
                                                             ByRef lConnection As SqlConnection,
                                                             ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_Bodega_By_Nombre_Bodega = ""

        Try

            Dim vSQL As String = "SELECT codigo FROM bodega WHERE nombre=@pBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@pBodega", pBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Get_Codigo_Bodega_By_Nombre_Bodega = IIf(IsDBNull(lDataTable.Rows(0).Item("codigo")), "", lDataTable.Rows(0).Item("codigo"))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estructura_By_IdBodega(ByRef pBeBodega As clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega " &
            " Where(IdBodega = @IdBodega)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar_Estructura(pBeBodega, dt.Rows(0), lTransaction, lConnection)
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Estructura_By_IdBodega_And_IdInventarioEnc(ByVal IdBodega As Integer,
                                                                          ByVal IdInventarioEnc As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Estructura_By_IdBodega_And_IdInventarioEnc = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Ubicaciones_Inventario_Ciclico 
                                WHERE IdBodega = @IdBodega AND IdInventarioEnc = @IdInventarioEnc ORDER BY TRAMO "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", IdInventarioEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_Estructura_By_IdBodega_And_IdInventarioEnc = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
        End Try

    End Function

    Public Shared Sub Cargar_Estructura(ByRef oBeBodega As clsBeBodega,
                                       ByRef dr As DataRow,
                                       ByRef lTransaction As SqlTransaction,
                                       ByRef lConnection As SqlConnection)

        Try

            With oBeBodega
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPais = IIf(IsDBNull(dr.Item("IdPais")), 0, dr.Item("IdPais"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Encargado = IIf(IsDBNull(dr.Item("encargado")), "", dr.Item("encargado"))
                .Ubic_recepcion = IIf(IsDBNull(dr.Item("ubic_recepcion")), "", dr.Item("ubic_recepcion"))
                .Ubic_picking = IIf(IsDBNull(dr.Item("ubic_picking")), "", dr.Item("ubic_picking"))
                .Ubic_despacho = IIf(IsDBNull(dr.Item("ubic_despacho")), "", dr.Item("ubic_despacho"))
                .Ubic_merma = IIf(IsDBNull(dr.Item("ubic_merma")), "", dr.Item("ubic_merma"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Coordenada_x = IIf(IsDBNull(dr.Item("coordenada_x")), "", dr.Item("coordenada_x"))
                .Coordenada_y = IIf(IsDBNull(dr.Item("coordenada_y")), "", dr.Item("coordenada_y"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Reservar_stocks_por_linea = IIf(IsDBNull(dr.Item("reservar_stocks_por_linea")), False, dr.Item("reservar_stocks_por_linea"))
                .Rechazar_pedido_por_stock = IIf(IsDBNull(dr.Item("rechazar_pedido_por_stock")), False, dr.Item("rechazar_pedido_por_stock"))
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), "", dr.Item("IdTipoTransaccion"))
                .Zoom = IIf(IsDBNull(dr.Item("zoom")), 0.0, dr.Item("zoom"))
                .Areas = clsLnBodega_area.Get_All_By_IdBodega(True, .IdBodega, lTransaction, lConnection)
                .Sectores = GetSectores(.Areas, .IdBodega, lTransaction, lConnection)
                .Tramos = GetTramos(.Sectores, .IdBodega, lTransaction, lConnection)
                .Ubicaciones = GetUbicaciones(.Tramos, lTransaction, lConnection)

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Listar(ByVal IdEmpresa As Integer,
                                  ByVal IdBodegaAExcluir As Integer,
                                  ByVal pActivo As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Bodega WHERE IdEmpresa = @IdEmpresa "

            If pActivo Then
                sp += " AND Activo=1 "
            Else
                sp += " AND Activo=0 "
            End If

            If IdBodegaAExcluir <> 0 Then
                sp += " AND Correlativo <>  " & IdBodegaAExcluir
            End If

            sp += " ORDER BY Correlativo "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_Empresa_ForCombo(ByVal IdEmpresa As Integer) As DataTable
        Try

            Const sp As String = "SELECT IdBodega, ISNULL(CODIGO,'ND') + ' - ' + Nombre AS Nombre  
                                    FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1 "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
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

    '#CKFK 20180131 10:30 PM Creé la función para obtener el IdMotivoUbicacionDañadoPicking
    Public Shared Function Get_IdMotivoUbicacion_Dañado_Picking(ByVal pIdBodega As Integer,
                                                             Optional ByRef pConnection As SqlConnection = Nothing,
                                                             Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_IdMotivoUbicacion_Dañado_Picking = 0

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT IdMotivoUbicacionDañadoPicking FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20210220 06:29 PM Creé la función para obtener el Id_Motivo_Ubic_Reabasto
    Public Shared Function Get_Id_Motivo_Ubic_Reabasto(ByVal pIdBodega As Integer,
                                                             Optional ByRef pConnection As SqlConnection = Nothing,
                                                             Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_Id_Motivo_Ubic_Reabasto = 0

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT Id_Motivo_Ubic_Reabasto FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_IdBuenEstado_Producto_By_IdBodega(ByVal pIdBodega As Integer,
                                                                 Optional ByRef pConnection As SqlConnection = Nothing,
                                                                 Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_IdBuenEstado_Producto_By_IdBodega = 0

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT IdProductoEstadoNE FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_IdProductoEstadoNE_By_IdBodega(ByVal pIdBodega As Integer,
                                                             Optional ByRef pConnection As SqlConnection = Nothing,
                                                             Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_IdProductoEstadoNE_By_IdBodega = 0

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT IdProductoEstadoNE FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20180201 10:30 PM Creé la función para obtener la ubicación por defecto para producto dañado
    Public Shared Function Get_IdUbicMerma_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicMerma_By_IdBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim vSQL As String = "SELECT ubic_merma FROM bodega WHERE IdBodega=@IdBodega"

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Return lReturnValue
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Recepcion_By_IdBodega(ByVal pIdBodega As Integer,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicacion_Recepcion_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_recepcion FROM bodega WHERE IdBodega=@IdBodega "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                '#EJC20191205: Agregué validación para IdBodega vacío.
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    If IsNumeric(lReturnValue) Then
                        Return lReturnValue
                    Else
                        Throw New Exception("La ubicación por defecto para recepción no está definida para la bodega código: " & pIdBodega)
                    End If
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicDespacho_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicDespacho_By_IdBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim vSQL As String = "SELECT ubic_despacho FROM bodega WHERE IdBodega=@IdBodega"

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Return Val(lReturnValue)
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicNE_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicNE_By_IdBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim vSQL As String = "SELECT ubic_producto_ne FROM bodega WHERE IdBodega=@IdBodega"

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Return Val(lReturnValue)
                    End If

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicNE_By_IdBodega(ByVal pIdBodega As Integer,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicNE_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_producto_ne FROM bodega WHERE IdBodega=@IdBodega"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return Val(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicDespacho_By_IdBodega(ByVal pIdBodega As Integer,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Get_IdUbicDespacho_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_despacho FROM bodega WHERE IdBodega=@IdBodega "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return lReturnValue
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Picking_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicacion_Picking_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_picking FROM bodega WHERE IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Return lReturnValue
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUbicacion_Picking_By_IdBodega(ByVal pIdBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As Integer

        Get_IdUbicacion_Picking_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_picking FROM bodega WHERE IdBodega=@IdBodega"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return lReturnValue
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CKFK 20180420 01:57 AM Creé la función para obtener el cambio_ubicacion_auto
    Public Shared Function Get_Parametro_Cambio_Ubicacion_Auto(ByVal pIdBodega As Integer,
                                                               Optional ByRef pConnection As SqlConnection = Nothing,
                                                               Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_Parametro_Cambio_Ubicacion_Auto = False

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT cambio_ubicacion_auto FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20190131 Le cambié el nombre a esta función porque ya existía otra igual
    Public Shared Function Exists_By_IdEmpresa(ByVal pIdEmpresa As Integer) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega WHERE IdEmpresa=@IdEmpresa"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_Codigo(ByVal pCodigo) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega WHERE Codigo=@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_Codigo(ByVal pCodigo As String,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega WHERE Codigo=@Codigo"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_Codigo(ByVal pCodigo As String,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE Codigo=@Codigo"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_IdBodega_By_Codigo = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_CodigoArea(ByVal pCodigo As String,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_CodigoArea = 0

        Try

            Dim vSQL As String = "SELECT b.IdBodega 
                                  FROM bodega b INNER JOIN bodega_area ba ON b.IdBodega = ba.IdBodega
                                  WHERE ba.Codigo=@Codigo"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_IdBodega_By_CodigoArea = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodegaWMS_By_Codigo_ERP(ByVal pCodigoBodegaERP As String) As Integer

        Get_IdBodegaWMS_By_Codigo_ERP = 0

        Try

            Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE Codigo_Bodega_ERP =@Codigo_Bodega_ERP"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@Codigo_Bodega_ERP", pCodigoBodegaERP)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_IdBodegaWMS_By_Codigo_ERP = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodegaWMS_By_Codigo(ByVal pCodigoBodega As String) As Integer

        Get_IdBodegaWMS_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE Codigo =@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@Codigo", pCodigoBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_IdBodegaWMS_By_Codigo = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodegaWMS_By_Codigo(ByVal pCodigoBodega As String,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As Integer

        Get_IdBodegaWMS_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdBodega FROM bodega WHERE Codigo =@Codigo"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Codigo", pCodigoBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_IdBodegaWMS_By_Codigo = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_And_Nombre_By_IdBodegaWMS(ByVal IdEmpresa As Integer,
                                                                ByVal IdBodega As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_And_Nombre_By_IdBodegaWMS = Nothing

        Try

            '#EJC20190311_0654PM: Se agregó código + Nombre
            Const sp As String = "SELECT IdBodega, ISNULL(CODIGO,'ND') + ' - ' + Nombre AS Origen  
                                    FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1 AND IdBodega = @IdBodega "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Codigo_And_Nombre_By_IdBodegaWMS = IIf(IsDBNull(dt.Rows(0).Item("Origen")), "", dt.Rows(0).Item("Origen"))
            End If

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Valor_Porcentaje_IVA_By_CodigoBodega(ByVal vIdBodega As String) As String

        Get_Valor_Porcentaje_IVA_By_CodigoBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Const sp As String = "SELECT valor_porcentaje_iva 
                                          FROM Bodega 
                                          WHERE 
                                          idbodega = @vIdBodega AND Activo=1  "

                    Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.AddWithValue("@vIdBodega", vIdBodega)
                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Get_Valor_Porcentaje_IVA_By_CodigoBodega = IIf(IsDBNull(dt.Rows(0).Item("valor_porcentaje_iva")), 12, dt.Rows(0).Item("valor_porcentaje_iva"))
                    End If

                    cmd.Dispose()
                    dad.Dispose()

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Codigo_Barra_By_CodigoBodega(ByVal vIdBodega As String) As String

        Get_Codigo_Barra_By_CodigoBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Const sp As String = "SELECT codigo_barra 
                                          FROM Bodega 
                                          WHERE 
                                          idbodega = @vIdBodega AND Activo=1  "

                    Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.AddWithValue("@vIdBodega", vIdBodega)
                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Get_Codigo_Barra_By_CodigoBodega = IIf(IsDBNull(dt.Rows(0).Item("codigo_barra")), 12, dt.Rows(0).Item("codigo_barra"))
                    End If

                    cmd.Dispose()
                    dad.Dispose()

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdBodega(ByVal IdBodega As Integer,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdBodega = Nothing

        Try

            '#EJC20190311_0654PM: Se agregó código + Nombre
            Const sp As String = "SELECT Codigo 
                                  FROM Bodega 
                                  WHERE IdBodega = @IdBodega 
                                  AND Activo=1  "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Codigo_By_IdBodega = IIf(IsDBNull(dt.Rows(0).Item("Codigo")), "", dt.Rows(0).Item("Codigo"))
            End If

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdBodega(ByVal IdBodega As Integer) As String

        Get_Codigo_By_IdBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '#EJC20190311_0654PM: Se agregó código + Nombre
                    Const sp As String = "SELECT Codigo 
                                          FROM Bodega WHERE 
                                          IdBodega = @IdBodega 
                                          AND 
                                          Activo=1  "


                    Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Get_Codigo_By_IdBodega = IIf(IsDBNull(dt.Rows(0).Item("Codigo")), "", dt.Rows(0).Item("Codigo"))
                    End If

                    cmd.Dispose()
                    dad.Dispose()

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Inserta_Ubicaciones_Por_Defecto(ByVal IdBodega As Integer,
                                                           ByVal IdUsuario As Integer) As Boolean

        Inserta_Ubicaciones_Por_Defecto = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim BeBodegaArea As New clsBeBodega_area
                    BeBodegaArea.IdArea = clsLnBodega_area.MaxID(IdBodega, lConnection, lTransaction) + 1
                    BeBodegaArea.IdBodega = IdBodega
                    BeBodegaArea.Descripcion = "SISTEMA"
                    BeBodegaArea.Sistema = True
                    BeBodegaArea.User_agr = IdUsuario
                    BeBodegaArea.Fec_agr = Now
                    BeBodegaArea.User_mod = IdUsuario
                    BeBodegaArea.Fec_mod = Now
                    BeBodegaArea.Codigo = BeBodegaArea.IdArea
                    BeBodegaArea.Activo = True
                    BeBodegaArea.Alto = 0
                    BeBodegaArea.Largo = 0
                    BeBodegaArea.Ancho = 0
                    BeBodegaArea.Margen_izquierdo = 0
                    BeBodegaArea.Margen_derecho = 0
                    BeBodegaArea.Margen_superior = 0
                    BeBodegaArea.Margen_inferior = 0
                    clsLnBodega_area.Insertar(BeBodegaArea, lConnection, lTransaction)

                    Dim BeBodegaSector As New clsBeBodega_sector
                    BeBodegaSector.IdSector = clsLnBodega_sector.MaxID(IdBodega,
                                                                       lConnection,
                                                                       lTransaction) + 1
                    BeBodegaSector.IdArea = BeBodegaArea.IdArea
                    BeBodegaSector.IdBodega = IdBodega
                    BeBodegaSector.Descripcion = "FLUJO DE MERCANCÍAS"
                    BeBodegaSector.Sistema = True
                    BeBodegaSector.User_agr = IdUsuario
                    BeBodegaSector.Fec_agr = Now
                    BeBodegaSector.User_mod = IdUsuario
                    BeBodegaSector.Fec_mod = Now
                    BeBodegaSector.Codigo = BeBodegaSector.IdSector
                    BeBodegaSector.Activo = True
                    BeBodegaSector.Alto = 0
                    BeBodegaSector.Largo = 0
                    BeBodegaSector.Ancho = 0
                    BeBodegaSector.Margen_izquierdo = 0
                    BeBodegaSector.Margen_derecho = 0
                    BeBodegaSector.Margen_superior = 0
                    BeBodegaSector.Margen_inferior = 0
                    clsLnBodega_sector.Insertar(BeBodegaSector,
                                                lConnection,
                                                lTransaction)

                    Dim BeBodegaTramo As New clsBeBodega_tramo
                    BeBodegaTramo.IdSector = BeBodegaSector.IdSector
                    BeBodegaTramo.IdTramo = clsLnBodega_tramo.MaxID(IdBodega,
                                                                    lConnection,
                                                                    lTransaction) + 1
                    BeBodegaTramo.IdArea = BeBodegaArea.IdArea
                    BeBodegaTramo.IdBodega = IdBodega
                    BeBodegaTramo.Codigo = BeBodegaTramo.IdTramo
                    BeBodegaTramo.Descripcion = "RECEPCIÓN"
                    BeBodegaTramo.Sistema = True
                    BeBodegaTramo.Es_Rack = False
                    BeBodegaTramo.User_agr = IdUsuario
                    BeBodegaTramo.Fec_agr = Now
                    BeBodegaTramo.User_mod = IdUsuario
                    BeBodegaTramo.Fec_mod = Now
                    BeBodegaTramo.Activo = True
                    BeBodegaTramo.Alto = 0
                    BeBodegaTramo.Largo = 0
                    BeBodegaTramo.Ancho = 0
                    BeBodegaTramo.Margen_izquierdo = 0
                    BeBodegaTramo.Margen_derecho = 0
                    BeBodegaTramo.Margen_superior = 0
                    BeBodegaTramo.Margen_inferior = 0
                    BeBodegaTramo.IdFontEnc = 0
                    clsLnBodega_tramo.Insertar(BeBodegaTramo, lConnection, lTransaction)

                    Dim pObjBU As New clsBeBodega_ubicacion
                    pObjBU.IdUbicacion = clsLnBodega_ubicacion.MaxID(IdBodega,
                                                                     lConnection,
                                                                     lTransaction) + 1
                    pObjBU.IdTramo = BeBodegaTramo.IdTramo
                    pObjBU.IdSector = BeBodegaSector.IdSector
                    pObjBU.IdArea = BeBodegaArea.IdArea
                    pObjBU.IdBodega = IdBodega
                    pObjBU.Sistema = True
                    pObjBU.Descripcion = "RECEPCIÓN"
                    pObjBU.Codigo_barra = pObjBU.IdUbicacion
                    pObjBU.Codigo_barra2 = pObjBU.IdUbicacion
                    pObjBU.User_agr = IdUsuario
                    pObjBU.Fec_agr = Now
                    pObjBU.User_mod = IdUsuario
                    pObjBU.Fec_mod = Now
                    pObjBU.Dañado = False
                    pObjBU.Alto = 0
                    pObjBU.Largo = 0
                    pObjBU.Ancho = 0
                    pObjBU.Activo = True
                    pObjBU.Bloqueada = False
                    pObjBU.Nivel = 1
                    pObjBU.Acepta_pallet = True
                    pObjBU.Ubicacion_picking = False
                    pObjBU.Ubicacion_recepcion = True
                    pObjBU.Ubicacion_despacho = False
                    pObjBU.Ubicacion_merma = False
                    pObjBU.Ubicacion_Virtual = False
                    pObjBU.Margen_izquierdo = 0
                    pObjBU.Margen_derecho = 0
                    pObjBU.Margen_superior = 0
                    pObjBU.Margen_inferior = 0
                    pObjBU.Orientacion_pos = 0
                    pObjBU.IdIndiceRotacion = 1
                    pObjBU.IdTipoRotacion = 0
                    pObjBU.Indice_x = 0
                    clsLnBodega_ubicacion.Insertar(pObjBU,
                                                   lConnection,
                                                   lTransaction)

                    Actualizar_IdUbicacion_Recepcion(pObjBU.IdUbicacion,
                                                     IdBodega,
                                                     lConnection,
                                                     lTransaction)

                    BeBodegaTramo = New clsBeBodega_tramo
                    BeBodegaTramo.IdSector = BeBodegaSector.IdSector
                    BeBodegaTramo.IdTramo = clsLnBodega_tramo.MaxID(IdBodega,
                                                                    lConnection,
                                                                    lTransaction) + 1
                    BeBodegaTramo.IdArea = BeBodegaArea.IdArea
                    BeBodegaTramo.IdBodega = IdBodega
                    BeBodegaTramo.Codigo = BeBodegaTramo.IdTramo
                    BeBodegaTramo.Descripcion = "TRÁNSITO"
                    BeBodegaTramo.Sistema = True
                    BeBodegaTramo.Es_Rack = False
                    BeBodegaTramo.User_agr = IdUsuario
                    BeBodegaTramo.Fec_agr = Now
                    BeBodegaTramo.User_mod = IdUsuario
                    BeBodegaTramo.Fec_mod = Now
                    BeBodegaTramo.Activo = True
                    BeBodegaTramo.Alto = 0
                    BeBodegaTramo.Largo = 0
                    BeBodegaTramo.Ancho = 0
                    BeBodegaTramo.Margen_izquierdo = 0
                    BeBodegaTramo.Margen_derecho = 0
                    BeBodegaTramo.Margen_superior = 0
                    BeBodegaTramo.Margen_inferior = 0
                    BeBodegaTramo.IdFontEnc = 0
                    clsLnBodega_tramo.Insertar(BeBodegaTramo,
                                               lConnection,
                                               lTransaction)

                    pObjBU = New clsBeBodega_ubicacion()
                    pObjBU.IdUbicacion = clsLnBodega_ubicacion.MaxID(IdBodega,
                                                                     lConnection,
                                                                     lTransaction) + 1
                    pObjBU.IdTramo = BeBodegaTramo.IdTramo
                    pObjBU.IdSector = BeBodegaSector.IdSector
                    pObjBU.IdArea = BeBodegaArea.IdArea
                    pObjBU.IdBodega = IdBodega
                    pObjBU.Sistema = True
                    pObjBU.Descripcion = "TRÁNSITO"
                    pObjBU.Codigo_barra = pObjBU.IdUbicacion
                    pObjBU.Codigo_barra2 = pObjBU.IdUbicacion
                    pObjBU.User_agr = IdUsuario
                    pObjBU.Fec_agr = Now
                    pObjBU.User_mod = IdUsuario
                    pObjBU.Fec_mod = Now
                    pObjBU.Dañado = False
                    pObjBU.Alto = 0
                    pObjBU.Largo = 0
                    pObjBU.Ancho = 0
                    pObjBU.Activo = True
                    pObjBU.Bloqueada = False
                    pObjBU.Nivel = 1
                    pObjBU.Acepta_pallet = True
                    pObjBU.Ubicacion_picking = False
                    pObjBU.Ubicacion_recepcion = False
                    pObjBU.Ubicacion_despacho = True
                    pObjBU.Ubicacion_merma = False
                    pObjBU.Ubicacion_Virtual = False
                    pObjBU.Margen_izquierdo = 0
                    pObjBU.Margen_derecho = 0
                    pObjBU.Margen_superior = 0
                    pObjBU.Margen_inferior = 0
                    pObjBU.Orientacion_pos = 0
                    pObjBU.IdIndiceRotacion = Nothing
                    pObjBU.IdTipoRotacion = Nothing
                    pObjBU.Indice_x = 0
                    clsLnBodega_ubicacion.Insertar(pObjBU,
                                                   lConnection,
                                                   lTransaction)

                    Actualizar_IdUbicacion_Despacho(pObjBU.IdUbicacion,
                                                    IdBodega,
                                                    lConnection,
                                                    lTransaction)

                    BeBodegaTramo = New clsBeBodega_tramo
                    BeBodegaTramo.IdSector = BeBodegaSector.IdSector
                    BeBodegaTramo.IdTramo = clsLnBodega_tramo.MaxID(IdBodega,
                                                                    lConnection,
                                                                    lTransaction) + 1
                    BeBodegaTramo.IdArea = BeBodegaArea.IdArea
                    BeBodegaTramo.IdBodega = IdBodega
                    BeBodegaTramo.Codigo = BeBodegaTramo.IdTramo
                    BeBodegaTramo.Descripcion = "PICKING"
                    BeBodegaTramo.Sistema = True
                    BeBodegaTramo.Es_Rack = False
                    BeBodegaTramo.User_agr = IdUsuario
                    BeBodegaTramo.Fec_agr = Now
                    BeBodegaTramo.User_mod = IdUsuario
                    BeBodegaTramo.Fec_mod = Now
                    BeBodegaTramo.Activo = True
                    BeBodegaTramo.Alto = 0
                    BeBodegaTramo.Largo = 0
                    BeBodegaTramo.Ancho = 0
                    BeBodegaTramo.Margen_izquierdo = 0
                    BeBodegaTramo.Margen_derecho = 0
                    BeBodegaTramo.Margen_superior = 0
                    BeBodegaTramo.Margen_inferior = 0
                    BeBodegaTramo.IdFontEnc = 0
                    clsLnBodega_tramo.Insertar(BeBodegaTramo,
                                               lConnection,
                                               lTransaction)

                    pObjBU = New clsBeBodega_ubicacion()
                    pObjBU.IdUbicacion = clsLnBodega_ubicacion.MaxID(IdBodega,
                                                                     lConnection,
                                                                     lTransaction) + 1
                    pObjBU.IdTramo = BeBodegaTramo.IdTramo
                    pObjBU.IdTramo = BeBodegaTramo.IdTramo
                    pObjBU.IdSector = BeBodegaSector.IdSector
                    pObjBU.IdArea = BeBodegaArea.IdArea
                    pObjBU.IdBodega = IdBodega
                    pObjBU.Sistema = True
                    pObjBU.Descripcion = "PICKING"
                    pObjBU.Codigo_barra = pObjBU.IdUbicacion
                    pObjBU.Codigo_barra2 = pObjBU.IdUbicacion
                    pObjBU.User_agr = IdUsuario
                    pObjBU.Fec_agr = Now
                    pObjBU.User_mod = IdUsuario
                    pObjBU.Fec_mod = Now
                    pObjBU.Dañado = False
                    pObjBU.Alto = 0
                    pObjBU.Largo = 0
                    pObjBU.Ancho = 0
                    pObjBU.Activo = True
                    pObjBU.Bloqueada = False
                    pObjBU.Nivel = 1
                    pObjBU.Acepta_pallet = True
                    pObjBU.Ubicacion_picking = True
                    pObjBU.Ubicacion_recepcion = False
                    pObjBU.Ubicacion_despacho = False
                    pObjBU.Ubicacion_merma = False
                    pObjBU.Ubicacion_Virtual = False
                    pObjBU.Margen_izquierdo = 0
                    pObjBU.Margen_derecho = 0
                    pObjBU.Margen_superior = 0
                    pObjBU.Margen_inferior = 0
                    pObjBU.Orientacion_pos = 0
                    pObjBU.IdIndiceRotacion = 1
                    pObjBU.IdTipoRotacion = Nothing
                    pObjBU.Indice_x = 0
                    clsLnBodega_ubicacion.Insertar(pObjBU,
                                                   lConnection,
                                                   lTransaction)

                    Actualizar_IdUbicacion_Picking(pObjBU.IdUbicacion,
                                                   IdBodega,
                                                   lConnection,
                                                   lTransaction)


                    BeBodegaTramo = New clsBeBodega_tramo
                    BeBodegaTramo.IdSector = BeBodegaSector.IdSector
                    BeBodegaTramo.IdTramo = clsLnBodega_tramo.MaxID(IdBodega,
                                                                    lConnection,
                                                                    lTransaction) + 1
                    BeBodegaTramo.IdArea = BeBodegaArea.IdArea
                    BeBodegaTramo.IdBodega = IdBodega
                    BeBodegaTramo.Codigo = BeBodegaTramo.IdTramo
                    BeBodegaTramo.Descripcion = "MERMA"
                    BeBodegaTramo.Sistema = True
                    BeBodegaTramo.Es_Rack = False
                    BeBodegaTramo.User_agr = IdUsuario
                    BeBodegaTramo.Fec_agr = Now
                    BeBodegaTramo.User_mod = IdUsuario
                    BeBodegaTramo.Fec_mod = Now
                    BeBodegaTramo.Activo = True
                    BeBodegaTramo.Alto = 0
                    BeBodegaTramo.Largo = 0
                    BeBodegaTramo.Ancho = 0
                    BeBodegaTramo.Margen_izquierdo = 0
                    BeBodegaTramo.Margen_derecho = 0
                    BeBodegaTramo.Margen_superior = 0
                    BeBodegaTramo.Margen_inferior = 0
                    BeBodegaTramo.IdFontEnc = 0
                    clsLnBodega_tramo.Insertar(BeBodegaTramo, lConnection, lTransaction)

                    pObjBU = New clsBeBodega_ubicacion()
                    pObjBU.IdUbicacion = clsLnBodega_ubicacion.MaxID(IdBodega,
                                                                     lConnection,
                                                                     lTransaction) + 1
                    pObjBU.IdTramo = BeBodegaTramo.IdTramo
                    pObjBU.IdSector = BeBodegaSector.IdSector
                    pObjBU.IdArea = BeBodegaArea.IdArea
                    pObjBU.IdBodega = IdBodega
                    pObjBU.Sistema = True
                    pObjBU.Descripcion = "MERMA"
                    pObjBU.Codigo_barra = pObjBU.IdUbicacion
                    pObjBU.Codigo_barra2 = pObjBU.IdUbicacion
                    pObjBU.User_agr = IdUsuario
                    pObjBU.Fec_agr = Now
                    pObjBU.User_mod = IdUsuario
                    pObjBU.Fec_mod = Now
                    pObjBU.Dañado = False
                    pObjBU.Alto = 0
                    pObjBU.Largo = 0
                    pObjBU.Ancho = 0
                    pObjBU.Activo = True
                    pObjBU.Bloqueada = False
                    pObjBU.Nivel = 1
                    pObjBU.Acepta_pallet = True
                    pObjBU.Ubicacion_picking = False
                    pObjBU.Ubicacion_recepcion = False
                    pObjBU.Ubicacion_despacho = False
                    pObjBU.Ubicacion_merma = True
                    pObjBU.Ubicacion_Virtual = False
                    pObjBU.Margen_izquierdo = 0
                    pObjBU.Margen_derecho = 0
                    pObjBU.Margen_superior = 0
                    pObjBU.Margen_inferior = 0
                    pObjBU.Orientacion_pos = 0
                    pObjBU.IdIndiceRotacion = 1
                    pObjBU.IdTipoRotacion = Nothing
                    pObjBU.Indice_x = 0
                    clsLnBodega_ubicacion.Insertar(pObjBU,
                                                   lConnection,
                                                   lTransaction)

                    Actualizar_IdUbicacion_Merma(pObjBU.IdUbicacion,
                                                 IdBodega,
                                                 lConnection,
                                                 lTransaction)

                    lTransaction.Commit()

                    Inserta_Ubicaciones_Por_Defecto = True

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdEmpresa_By_IdBodega(ByVal pIdBodega As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Integer

        Get_IdEmpresa_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT IdEmpresa FROM bodega WHERE IdBodega=@IdBodega "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return lReturnValue
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Indicadores_Ocupacion_By_IdBodega(ByVal pIdBodega As Integer,
                                                                 ByRef UbicacionesVacias As Integer,
                                                                 ByRef UbicacionesOcupadas As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Indicadores_Ocupacion_By_IdBodega = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT COUNT(distinct IDUBICACION) AS UBICACIONES_VACIAS
                                FROM VW_OcupacionBodega
                                WHERE IDSTOCK =0
                                AND (IdBodega = @IdBodega)
                                GROUP BY IDBODEGA "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    UbicacionesVacias = lReturnValue
                Else
                    UbicacionesVacias = 0
                End If

            End Using

            sp = "SELECT COUNT(distinct IDUBICACION) AS UBICACIONES_OCUPADAS
                                    FROM VW_OcupacionBodega
                                    WHERE IDSTOCK <> 0
                                    AND (IdBodega = @IdBodega)
                                    GROUP BY IDBODEGA "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    UbicacionesOcupadas = lReturnValue
                Else
                    UbicacionesOcupadas = 0
                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#CKFK 20210717 Creé el GetAll de Empresa con transacción y conexión por referencia
    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega)

        Try

            Dim lReturnList As New List(Of clsBeBodega)
            Const sp As String = "SELECT * FROM Bodega WHERE Activo = 1"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega

            For Each lRow As DataRow In dt.Rows
                vBeBodega = New clsBeBodega()
                Cargar(vBeBodega, lRow)
                lReturnList.Add(vBeBodega)
            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared vIncrementoYAnt As Double = 0

    Public Shared Function Unificar_Bodegas(ByVal IdBodegaDestino As Integer,
                                            ByVal lBodegasSeleccionadas As List(Of clsBeBodegaSeleccion),
                                            ByVal pIdUsuario As Integer) As Boolean

        Unificar_Bodegas = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vIdMaxArea As Integer = 0
                    Dim vIdMaxSector As Integer = 0
                    Dim vIdMaxTramo As Integer = 0
                    Dim vIdMaxUbicacion As Integer = 0

                    Dim lSectores As New List(Of clsBeBodega_sector)
                    Dim lTramos As New List(Of clsBeBodega_tramo)
                    Dim lUbicaciones As New List(Of clsBeBodega_ubicacion)
                    Dim vIncrementoX As Double = 0
                    Dim vIncrementoY As Double = 0

                    Dim BeBodega As New clsBeBodega()

                    For Each Bodega In lBodegasSeleccionadas.OrderBy(Function(x) x.Codigo)

                        If Not clsLnBodega_area.Existe_Codigo_By_IdBodega(Bodega.Codigo,
                                                                          IdBodegaDestino,
                                                                          lConnection,
                                                                          ltransaction) Then

                            BeBodega = GetSingle_By_Idbodega(Bodega.IdBodega,
                                                             lConnection,
                                                             ltransaction)

                            vIdMaxArea = clsLnBodega_area.MaxID(IdBodegaDestino,
                                                                lConnection,
                                                                ltransaction)

                            If Not BeBodega Is Nothing Then

                                Dim BeBodegaArea As New clsBeBodega_area()
                                BeBodegaArea.IdBodega = IdBodegaDestino
                                BeBodegaArea.IdArea = vIdMaxArea
                                BeBodegaArea.Codigo = Bodega.Codigo
                                BeBodegaArea.Descripcion = BeBodega.Nombre
                                BeBodegaArea.Activo = True
                                BeBodegaArea.Fec_agr = Now
                                BeBodegaArea.Fec_mod = Now
                                BeBodegaArea.User_agr = pIdUsuario
                                BeBodegaArea.User_mod = pIdUsuario
                                BeBodegaArea.Sistema = 0
                                BeBodegaArea.Alto = BeBodega.Alto
                                BeBodegaArea.Largo = BeBodega.Largo
                                BeBodegaArea.Ancho = BeBodega.Ancho
                                BeBodegaArea.Margen_izquierdo = 0
                                BeBodegaArea.Margen_derecho = 0
                                BeBodegaArea.Margen_superior = 0
                                BeBodegaArea.Margen_inferior = 0
                                BeBodegaArea.Grupo = 0

                                clsLnBodega_area.Insertar(BeBodegaArea,
                                                          lConnection,
                                                          ltransaction)

                                lSectores = clsLnBodega_sector.Get_All_By_IdArea_And_IdBodega(BeBodega.IdBodega,
                                                                                              lConnection,
                                                                                              ltransaction)

                                vIdMaxSector = clsLnBodega_sector.MaxID(IdBodegaDestino,
                                                                        lConnection,
                                                                        ltransaction)

                                vIdMaxTramo = clsLnBodega_tramo.MaxID(IdBodegaDestino,
                                                                      lConnection,
                                                                      ltransaction)

                                vIdMaxUbicacion = clsLnBodega_ubicacion.MaxID(IdBodegaDestino,
                                                                              lConnection,
                                                                              ltransaction)

                                If Not lSectores Is Nothing Then

                                    If lSectores.Count > 0 Then

                                        Dim BeBodegaSector As clsBeBodega_sector

                                        For Each Sector In lSectores

                                            BeBodegaSector = New clsBeBodega_sector()
                                            BeBodegaSector.IdBodega = IdBodegaDestino
                                            BeBodegaSector.IdSector = Sector.IdSector + vIdMaxSector
                                            BeBodegaSector.IdArea = vIdMaxArea
                                            BeBodegaSector.Sistema = Sector.Sistema
                                            BeBodegaSector.Descripcion = "SECTOR " & BeBodegaSector.IdSector
                                            BeBodegaSector.User_agr = pIdUsuario
                                            BeBodegaSector.Fec_agr = Now
                                            BeBodegaSector.User_mod = pIdUsuario
                                            BeBodegaSector.Fec_mod = Now
                                            BeBodegaSector.Activo = True
                                            BeBodegaSector.Alto = Sector.Alto
                                            BeBodegaSector.Largo = Sector.Largo
                                            BeBodegaSector.Ancho = Sector.Ancho
                                            BeBodegaSector.Margen_izquierdo = Sector.Margen_izquierdo
                                            BeBodegaSector.Margen_derecho = Sector.Margen_derecho
                                            BeBodegaSector.Margen_superior = Sector.Margen_superior
                                            BeBodegaSector.Margen_inferior = Sector.Margen_inferior
                                            BeBodegaSector.Codigo = "S" & Right("00" & BeBodegaSector.IdSector, 2)
                                            BeBodegaSector.IdSectorIzquierda = Sector.IdSectorIzquierda
                                            BeBodegaSector.IdSectorDerecha = Sector.IdSectorDerecha
                                            BeBodegaSector.Horizontal = Sector.Horizontal
                                            BeBodegaSector.Pos_x = vIncrementoX + Sector.Pos_x
                                            BeBodegaSector.Pos_y = Sector.Pos_y
                                            clsLnBodega_sector.Insertar(BeBodegaSector,
                                                                        lConnection,
                                                                        ltransaction)

                                            lTramos = clsLnBodega_tramo.Get_All_By_IdBodega_And_IdSector(BeBodega.IdBodega,
                                                                                                        Sector.IdSector,
                                                                                                        lConnection,
                                                                                                        ltransaction)

                                            If Not lTramos Is Nothing Then

                                                If lTramos.Count > 0 Then

                                                    Dim BeBodegaTramo As clsBeBodega_tramo
                                                    Dim pInicia As String = ""
                                                    Dim pFinaliza As String = ""
                                                    Dim pMaximo As Integer = 0
                                                    Dim existe As Boolean

                                                    For Each Tramo In lTramos

                                                        pInicia = Tramo.Descripcion.Substring(0, 1)
                                                        pFinaliza = Tramo.Descripcion.Substring(Tramo.Descripcion.Length - 1, 1)

                                                        existe = clsLnBodega_tramo.Existe_By_IdBodega_And_IdSector(IdBodegaDestino,
                                                                                                                   BeBodegaSector.IdSector,
                                                                                                                   pInicia,
                                                                                                                   lConnection,
                                                                                                                   ltransaction)


                                                        If existe Then
                                                            pMaximo = clsLnBodega_tramo.Get_Maximo_Tramo_Sector(Tramo.Descripcion,
                                                                                                                BeBodegaSector.IdSector,
                                                                                                                IdBodegaDestino,
                                                                                                                lConnection,
                                                                                                                ltransaction)
                                                        Else
                                                            pMaximo = clsLnBodega_tramo.Get_Maximo_Tramo(Tramo.Descripcion,
                                                                                                         IdBodegaDestino,
                                                                                                         lConnection,
                                                                                                         ltransaction) + 1
                                                        End If


                                                        BeBodegaTramo = New clsBeBodega_tramo
                                                        BeBodegaTramo.IdArea = vIdMaxArea
                                                        BeBodegaTramo.IdSector = BeBodegaSector.IdSector
                                                        BeBodegaTramo.IdBodega = IdBodegaDestino
                                                        BeBodegaTramo.IdTramo = Tramo.IdTramo + vIdMaxTramo
                                                        BeBodegaTramo.Sistema = Tramo.Sistema
                                                        BeBodegaTramo.Descripcion = pInicia & Right("000" & pMaximo, 3) & "-" & pFinaliza
                                                        BeBodegaTramo.User_agr = pIdUsuario
                                                        BeBodegaTramo.Fec_agr = Now
                                                        BeBodegaTramo.User_mod = pIdUsuario
                                                        BeBodegaTramo.Fec_mod = Now
                                                        BeBodegaTramo.Activo = Tramo.Activo
                                                        BeBodegaTramo.Alto = Tramo.Alto
                                                        BeBodegaTramo.Largo = Tramo.Largo
                                                        BeBodegaTramo.Ancho = Tramo.Ancho
                                                        BeBodegaTramo.Margen_izquierdo = Tramo.Margen_izquierdo
                                                        BeBodegaTramo.Margen_derecho = Tramo.Margen_derecho
                                                        BeBodegaTramo.Margen_superior = Tramo.Margen_superior
                                                        BeBodegaTramo.Margen_inferior = Tramo.Margen_inferior
                                                        BeBodegaTramo.Codigo = BeBodegaTramo.IdTramo
                                                        BeBodegaTramo.Indice_x = Tramo.Indice_x
                                                        BeBodegaTramo.Orientacion = Tramo.Orientacion
                                                        BeBodegaTramo.IdTipoProductoDefault = Tramo.IdTipoProductoDefault
                                                        BeBodegaTramo.IdFontEnc = Tramo.IdFontEnc
                                                        BeBodegaTramo.IdTipoRack = Tramo.IdTipoRack
                                                        BeBodegaTramo.Es_Rack = Tramo.Es_Rack
                                                        BeBodegaTramo.Horizontal = Tramo.Horizontal
                                                        BeBodegaTramo.Orden_Descendente = IIf(IsDBNull(Tramo.Orden_Descendente), 0, Tramo.Orden_Descendente)

                                                        clsLnBodega_tramo.Insertar(BeBodegaTramo,
                                                                                   lConnection,
                                                                                   ltransaction)

                                                        lUbicaciones = New List(Of clsBeBodega_ubicacion)
                                                        lUbicaciones = clsLnBodega_ubicacion.Get_All_By_IdBodega_And_IdSector_And_IdTramo(BeBodega.IdBodega,
                                                                                                                                          Sector.IdSector,
                                                                                                                                          Tramo.IdTramo,
                                                                                                                                          lConnection,
                                                                                                                                          ltransaction)

                                                        If Not lUbicaciones Is Nothing Then

                                                            If lUbicaciones.Count > 0 Then

                                                                For Each U In lUbicaciones

                                                                    Dim BeBodegaUbicacion As New clsBeBodega_ubicacion()
                                                                    BeBodegaUbicacion.IdBodega = IdBodegaDestino
                                                                    BeBodegaUbicacion.IdArea = vIdMaxArea
                                                                    BeBodegaUbicacion.IdSector = BeBodegaSector.IdSector
                                                                    BeBodegaUbicacion.IdTramo = BeBodegaTramo.IdTramo
                                                                    BeBodegaUbicacion.IdUbicacion = U.IdUbicacion + vIdMaxUbicacion
                                                                    BeBodegaUbicacion.Descripcion = U.Descripcion
                                                                    BeBodegaUbicacion.Ancho = U.Ancho
                                                                    BeBodegaUbicacion.Largo = U.Largo
                                                                    BeBodegaUbicacion.Alto = U.Alto
                                                                    BeBodegaUbicacion.Nivel = U.Nivel
                                                                    BeBodegaUbicacion.Indice_x = U.Indice_x
                                                                    BeBodegaUbicacion.IdIndiceRotacion = U.IdIndiceRotacion
                                                                    BeBodegaUbicacion.IdTipoRotacion = U.IdTipoRotacion
                                                                    BeBodegaUbicacion.Sistema = U.Sistema
                                                                    BeBodegaUbicacion.Codigo_barra = Right("00000" & BeBodegaUbicacion.IdUbicacion, 5)
                                                                    BeBodegaUbicacion.Codigo_barra2 = U.Codigo_barra2
                                                                    BeBodegaUbicacion.User_agr = U.User_agr
                                                                    BeBodegaUbicacion.Fec_agr = U.Fec_agr
                                                                    BeBodegaUbicacion.User_mod = U.User_mod
                                                                    BeBodegaUbicacion.Fec_mod = U.Fec_mod
                                                                    BeBodegaUbicacion.Dañado = U.Dañado
                                                                    BeBodegaUbicacion.Activo = U.Activo
                                                                    BeBodegaUbicacion.Bloqueada = U.Bloqueada
                                                                    BeBodegaUbicacion.Acepta_pallet = U.Acepta_pallet
                                                                    BeBodegaUbicacion.Ubicacion_picking = U.Ubicacion_picking
                                                                    BeBodegaUbicacion.Ubicacion_recepcion = U.Ubicacion_recepcion
                                                                    BeBodegaUbicacion.Ubicacion_despacho = U.Ubicacion_despacho
                                                                    BeBodegaUbicacion.Ubicacion_merma = U.Ubicacion_merma
                                                                    BeBodegaUbicacion.Margen_izquierdo = U.Margen_izquierdo
                                                                    BeBodegaUbicacion.Margen_derecho = U.Margen_derecho
                                                                    BeBodegaUbicacion.Margen_superior = U.Margen_superior
                                                                    BeBodegaUbicacion.Margen_inferior = U.Margen_inferior
                                                                    BeBodegaUbicacion.Orientacion_pos = U.Orientacion_pos
                                                                    BeBodegaUbicacion.Ubicacion_Virtual = U.Ubicacion_Virtual
                                                                    BeBodegaUbicacion.ubicacion_ne = U.ubicacion_ne
                                                                    clsLnBodega_ubicacion.Insertar(BeBodegaUbicacion,
                                                                                                   lConnection,
                                                                                                   ltransaction)

                                                                Next

                                                            End If

                                                        End If

                                                    Next

                                                End If

                                            End If

                                        Next

                                    End If 'If lSectores.Count > 0 Then

                                End If 'If Not lSectores Is Nothing Then

                                vIncrementoX += BeBodega.Ancho + 1

                                If vIncrementoYAnt < BeBodega.Largo + 1 Then
                                    vIncrementoYAnt = BeBodega.Largo + 1
                                End If

                            End If 'If Not BeBodega Is Nothing Then

                        End If 'Existe_Codigo_By_IdBodega

                    Next

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220419: Obtiene el IdUbicacionRecepción por defecto de la bodega.
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_IdUbicacion_Recepcion_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicacion_Recepcion_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT ubic_recepcion FROM bodega WHERE IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        '#EJC20191205: Agregué validación para IdBodega vacío.
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            If IsNumeric(lReturnValue) Then
                                Return lReturnValue
                            Else
                                Throw New Exception("La ubicación por defecto para recepción no está definida para la bodega código: " & pIdBodega)
                            End If
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario_DT_(ByVal IdEmpresa As Integer,
                                                                 ByVal IdUsuario As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vBeBodega As New clsBeBodega()
        Dim lReturnList As New List(Of clsBeBodega)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT b.*
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdEmpresa_And_IdUsuario_DT_ = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                End Try
            End If
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20231203: Por favor, no tocar esta función, a menos que tengan un indulto presidencial de Fidel.
    ''' </summary>
    ''' <param name="IdEmpresa"></param>
    ''' <param name="IdUsuario"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario_DT(ByVal IdEmpresa As Integer,
                                                                 ByVal IdUsuario As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdEmpresa_And_IdUsuario_DT = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT b.IdBodega as Codigo, 
                                  CONVERT(NVARCHAR(50),b.IdBodega) + ' - ' + b.nombre as Nombre 
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdEmpresa_And_IdUsuario_DT = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                End Try
            End If
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function Habilitar_Reemplazo(ByVal pIdBodega As Integer,
                                               ByVal pValor As Boolean,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega")
            Upd.Add("permitir_reemplazo_picking", "@Valor", DataType.Parametro)
            Upd.Add("permitir_no_encontrado_picking", "@Valor", DataType.Parametro)
            Upd.Add("permitir_reemplazo_verificacion", "@Valor", DataType.Parametro)
            Upd.Where("IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            cmd.Parameters.Add(New SqlParameter("@Valor", pValor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Shared Function Get_Liberar_Stock_Despachos_Parciales(ByVal IdBodega As Integer,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As Boolean

        Get_Liberar_Stock_Despachos_Parciales = False

        Try

            Const sp As String = "SELECT Liberar_Stock_Despachos_Parciales 
                                  FROM Bodega 
                                  WHERE IdBodega = @IdBodega 
                                  AND Activo=1  "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Liberar_Stock_Despachos_Parciales = IIf(IsDBNull(dt.Rows(0).Item("Liberar_Stock_Despachos_Parciales")), False, dt.Rows(0).Item("Liberar_Stock_Despachos_Parciales"))
            End If

            cmd.Dispose()
            dad.Dispose()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC202309111442: Obtiene el parámetro que indica si la bodega permite documentos en decimales o no.
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Permitir_Decimales(ByVal pIdBodega As Integer,
                                                  Optional ByRef pConnection As SqlConnection = Nothing,
                                                  Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_Permitir_Decimales = False

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT permitir_decimales FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1"
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega
            Dim lBodegas As New List(Of clsBeBodega)

            For Each Dr As DataRow In dt.Rows

                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, Dr)
                vBeBodega.Empresa.IdEmpresa = vBeBodega.IdEmpresa
                vBeBodega.Empresa.Nombre = clsLnEmpresa.GetNombreEmpresa(vBeBodega.IdEmpresa)
                lBodegas.Add(vBeBodega)

            Next

            lTransaction.Commit()

            Return lBodegas

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario(ByVal IdEmpresa As Integer,
                                                              ByVal IdUsuario As Integer) As List(Of clsBeBodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vBeBodega As New clsBeBodega()
        Dim lReturnList As New List(Of clsBeBodega)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT B.*
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            For Each lRow As DataRow In dt.Rows
                vBeBodega = New clsBeBodega()
                Cargar(vBeBodega, lRow)
                lReturnList.Add(vBeBodega)
            Next

            Get_All_By_IdEmpresa_And_IdUsuario = lReturnList

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                End Try
            End If
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdUsuario_DT_QA(ByVal IdEmpresa As Integer,
                                                                    ByVal IdUsuario As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vBeBodega As New clsBeBodega()
        Dim lReturnList As New List(Of clsBeBodega)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT B.IdBodega, B.Codigo
                                  FROM usuario_bodega AS ub 
                                  INNER JOIN usuario AS u ON ub.IdUsuario = u.IdUsuario 
                                  INNER JOIN bodega AS b ON ub.IdBodega = b.IdBodega 
                                  WHERE b.IdEmpresa=@IdEmpresa AND b.activo=1 AND u.IdUsuario=@IdUsuario "


            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdEmpresa_And_IdUsuario_DT_QA = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                End Try
            End If
            Throw New Exception("ListarBodega: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Nombre_Bodega_By_IdBodega(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As String

        Get_Nombre_Bodega_By_IdBodega = ""

        Try

            Dim vSQL As String = "SELECT Nombre FROM bodega WHERE IdBodega=@IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Get_Nombre_Bodega_By_IdBodega = IIf(IsDBNull(lDataTable.Rows(0).Item("Nombre")), "", lDataTable.Rows(0).Item("Nombre"))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

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


    Public Shared Function Exists_By_IdBodega(ByVal pIdBodega As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega WHERE IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdConfiguracionInterface_By_IdBodega(ByVal pIdBodega As String) As Integer

        Get_IdConfiguracionInterface_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT IdConfiguracionInterface FROM bodega WHERE IdBodega =@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_IdConfiguracionInterface_By_IdBodega = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_IdBodega(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega WHERE IdBodega=@IdBodega "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estructura_By_IdBodega_And_IdInventarioEnc(ByVal IdBodega As Integer,
                                                                          ByVal IdInventarioEnc As Integer,
                                                                          ByVal lConnection As SqlConnection,
                                                                          ByVal lTransaction As SqlTransaction) As DataTable

        Get_Estructura_By_IdBodega_And_IdInventarioEnc = Nothing

        Try

            Dim sp As String = "SELECT * FROM VW_Ubicaciones_Inventario_Ciclico 
                                WHERE IdBodega = @IdBodega AND IdInventarioEnc = @IdInventarioEnc ORDER BY TRAMO "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", IdInventarioEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Get_Estructura_By_IdBodega_And_IdInventarioEnc = dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa(ByVal IdEmpresa As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeBodega)

        Try

            Dim vSQL As String = "SELECT * FROM Bodega WHERE IdEmpresa =@IdEmpresa AND Activo=1"
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeBodega As New clsBeBodega
            Dim lBodegas As New List(Of clsBeBodega)

            For Each Dr As DataRow In dt.Rows

                vBeBodega = New clsBeBodega
                Cargar(vBeBodega, Dr)
                vBeBodega.Empresa.IdEmpresa = vBeBodega.IdEmpresa
                vBeBodega.Empresa.Nombre = clsLnEmpresa.GetNombreEmpresa(vBeBodega.IdEmpresa)
                lBodegas.Add(vBeBodega)

            Next

            Return lBodegas

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIndicadores_Ocupacion_By_Area(ByVal pArea As String,
                                                            ByRef UbicacionesVacias As Integer,
                                                            ByRef UbicacionesOcupadas As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetIndicadores_Ocupacion_By_Area = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = " SELECT COUNT(distinct IDUBICACION) AS UBICACIONES_VACIAS
                                FROM VW_OcupacionBodega
                                WHERE IDSTOCK =0 and Area=@Area "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@Area", pArea)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    UbicacionesVacias = lReturnValue
                Else
                    UbicacionesVacias = 0
                End If

            End Using

            sp = "SELECT COUNT(distinct IDUBICACION) AS UBICACIONES_OCUPADAS
                                    FROM VW_OcupacionBodega
                                    WHERE IDSTOCK <> 0 and Area=@Area"



            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@Area", pArea)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    UbicacionesOcupadas = lReturnValue
                Else
                    UbicacionesOcupadas = 0
                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetOcupacionAreaTipoDT(idBodega As Integer,
                                                  ByRef ubicacionesVacias As Integer,
                                                  ByRef ubicacionesOcupadas As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            cn.Open()

            ' 1) Traer el detalle de ocupación por Área/Tipo/Estado
            Using da As New SqlDataAdapter("
            SELECT Area, Tipo, Estado, Cantidad, TotalAreaTipo, PorcentajeEnAreaTipo
            FROM dbo.VW_Ocupacion_Area_Resumen
            WHERE IdBodega = @IdBodega
            ORDER BY Area, Tipo, Estado", cn)

                da.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
                da.Fill(dt)
            End Using

            ' 2) Traer en UNA sola consulta las ubicaciones vacías y ocupadas
            Using cmd As New SqlCommand("
            SELECT
                COUNT(DISTINCT CASE WHEN IDSTOCK = 0  THEN IDUBICACION END) AS UBICACIONES_VACIAS,
                COUNT(DISTINCT CASE WHEN IDSTOCK <> 0 THEN IDUBICACION END) AS UBICACIONES_OCUPADAS
            FROM dbo.VW_OcupacionBodega
            WHERE IdBodega = @IdBodega;", cn)

                cmd.Parameters.AddWithValue("@IdBodega", idBodega)

                Using rdr = cmd.ExecuteReader()
                    If rdr.Read() Then
                        ubicacionesVacias = If(IsDBNull(rdr(0)), 0, Convert.ToInt32(rdr(0)))
                        ubicacionesOcupadas = If(IsDBNull(rdr(1)), 0, Convert.ToInt32(rdr(1)))
                    Else
                        ubicacionesVacias = 0
                        ubicacionesOcupadas = 0
                    End If
                End Using
            End Using
        End Using

        ' 3) Agregar columna "Serie" (Tipo - Estado) como en tu implementación original
        If Not dt.Columns.Contains("Serie") Then dt.Columns.Add("Serie", GetType(String))
        For Each r As DataRow In dt.Rows
            r("Serie") = $"{r("Tipo")} - {r("Estado")}"   ' p.ej. "FISCAL - Ocupadas"
        Next

        Return dt
    End Function

    Public Shared Function Get_Maneja_Talla_Color_By_IdBodega(ByVal pIdBodega As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As Boolean

        Get_Maneja_Talla_Color_By_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT control_talla_color FROM bodega WHERE IdBodega=@IdBodega"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Maneja_Talla_Color_By_IdBodega = CBool(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#MA20260415 Metodo para obtener el estado por defecto del rack - mejoras en la cumbre
    Public Shared Function Get_Estado_Defecto_Rack(ByVal pIdBodega As Integer,
                                               Optional ByRef pConnection As SqlConnection = Nothing,
                                               Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_Estado_Defecto_Rack = 0

        Try
            If Not Es_Transaccion_Remota Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            Dim vSQL As String = "SELECT Estado_Defecto_Rack FROM bodega WHERE IdBodega=@IdBodega"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                       IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) With {
            .CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Return CInt(lReturnValue)
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function GetUbicacionesVaciasPorArea(idBodega As Integer, area As String) As DataTable
        Dim dt As New DataTable()

        Try
            Using cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using cmd As New SqlCommand()
                    cmd.Connection = cn
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText =
                        "SELECT " &
                        "    Ubicacion, " &
                        "    Area  " &
                        " FROM VW_OcupacionBodega " &
                        "WHERE IdBodega = @IdBodega " &
                        "  AND Area = @Area " &
                        "  AND ISNULL(IdStock, 0) = 0 " &
                        "ORDER BY Ubicacion"

                    cmd.Parameters.AddWithValue("@IdBodega", idBodega)
                    cmd.Parameters.AddWithValue("@Area", area)

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            Return dt

        Catch ex As Exception
            Throw New Exception("Error al obtener ubicaciones vacías por área: " & ex.Message, ex)
        End Try
    End Function

End Class