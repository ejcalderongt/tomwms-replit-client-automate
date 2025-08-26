Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnUbicaciones_por_regla

    Public Shared Function Get_All_By_Filtros(ByVal pFiltros As clsBeUbicaciones_por_regla,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeUbicaciones_por_regla)

        Try

            Dim lReturnList As New List(Of clsBeUbicaciones_por_regla)

            Dim sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega"

            If pFiltros.IdPropietario <> 0 Then
                sp += " AND (IdPropietario IS NULL Or IdPropietario = @IdPropietario)"
                sp += " AND (regla_ubic_det_prop_Activo =1)"
            End If

            If pFiltros.IdTipoRotacion <> 0 Then
                sp += " AND (IdTipoRotacion IS NULL Or IdTipoRotacion = @IdTipoRotacion)"
            End If

            If pFiltros.IdIndiceRotacion <> 0 Then
                sp += " AND (IdIndiceRotacion IS NULL Or IdIndiceRotacion =0 Or IdIndiceRotacion = @IdIndiceRotacion)"
            End If

            If pFiltros.IdTipoProducto <> 0 Then
                sp += " AND (IdTipoProducto IS NULL Or IdTipoProducto = @IdTipoProducto)"
                sp += " AND (regla_ubic_det_tp_Activo =1)"
            End If

            If pFiltros.IdEstado <> 0 Then
                sp += " AND (IdEstado IS NULL Or IdEstado = @IdEstado)"
                sp += " AND (regla_ubic_det_pe_Activo IS NULL or regla_ubic_det_pe_Activo =1)"
            End If

            'Si pFiltros.Acepta_pallet = 0, puede colocar pallets y unidades sueltas
            'Si pFiltros.Acepta_pallet = 1, solo puede colocar pallets.

            If pFiltros.IdPresentacion <> 0 Then
                sp += " AND (IdPresentacion IS NULL Or IdPresentacion = @IdPresentacion)"
                sp += " AND (regla_ubic_det_pe_Activo IS NULL or regla_ubic_det_pe_Activo =1)"
            End If

            If pFiltros.Acepta_pallet <> 0 Then
                sp += " AND acepta_pallet = @acepta_pallet"
            End If

            'think if apply without condition or not.
            sp += " AND Dañado = @Dañado"

            'Colocar solo en el primer nivel, de lo contrario puede colocar en cualquier nivel.
            'If pFiltros.Nivel <> 0 Then
            '    sp += " AND Nivel = @Nivel"
            'End If

            'Solo ubiaciones activas
            sp += " AND Activo = 1 "

            'Solo ubiaciones NO bloqueadas
            sp += " AND Bloqueada= 0 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pFiltros.IdBodega)
            If pFiltros.IdPropietario <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pFiltros.IdPropietario)
            If pFiltros.IdTipoRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoRotacion", pFiltros.IdTipoRotacion)
            If pFiltros.IdIndiceRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", pFiltros.IdIndiceRotacion)
            If pFiltros.IdTipoProducto <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pFiltros.IdTipoProducto)
            If pFiltros.IdEstado <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdEstado", pFiltros.IdEstado)
            If pFiltros.IdPresentacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pFiltros.IdPresentacion)
            If pFiltros.Acepta_pallet <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@acepta_pallet", 1)
            'If pFiltros.Nivel <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@Nivel", 1)
            dad.SelectCommand.Parameters.AddWithValue("@Dañado", IIf(pFiltros.Dañado, 1, 0))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUbicaciones_por_regla As New clsBeUbicaciones_por_regla

            For Each dr As DataRow In dt.Rows

                vBeUbicaciones_por_regla = New clsBeUbicaciones_por_regla
                Cargar(vBeUbicaciones_por_regla, dr)
                vBeUbicaciones_por_regla.Descripcion = IIf(IsDBNull(dr.Item("Nombre_Completo")), "", dr.Item("Nombre_Completo"))
                lReturnList.Add(vBeUbicaciones_por_regla)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_Filtros(ByVal pFiltros As clsBeUbicaciones_por_regla,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim sp As String = "SELECT distinct idtramo FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega"

            If pFiltros.IdPropietario <> 0 Then
                sp += " AND (IdPropietario IS NULL Or IdPropietario = @IdPropietario)"
                sp += " AND (regla_ubic_det_prop_Activo =1)"
            End If

            If pFiltros.IdTipoRotacion <> 0 Then
                sp += " AND (IdTipoRotacion IS NULL Or IdTipoRotacion = @IdTipoRotacion)"
            End If

            If pFiltros.IdIndiceRotacion <> 0 Then
                sp += " AND (IdIndiceRotacion IS NULL Or IdIndiceRotacion = @IdIndiceRotacion)"
            End If

            If pFiltros.IdTipoProducto <> 0 Then
                sp += " AND (IdTipoProducto IS NULL Or IdTipoProducto = @IdTipoProducto)"
            End If

            If pFiltros.IdEstado <> 0 Then
                sp += " AND (IdEstado IS NULL Or IdEstado = @IdEstado)"
                sp += " AND (regla_ubic_det_pe_Activo IS NULL Or regla_ubic_det_pe_Activo = 1)"
            End If

            'Si pFiltros.Acepta_pallet = 0, puede colocar pallets y unidades sueltas
            'Si pFiltros.Acepta_pallet = 1, solo puede colocar pallets.

            If pFiltros.IdPresentacion <> 0 Then
                sp += " AND (IdPresentacion IS NULL Or IdPresentacion = @IdPresentacion)"
                'Agregar en consulta :#EJC20171212:0857AM
                'sp += " AND (regla_ubic_det_pp_Activo =1)"
            End If

            If pFiltros.Acepta_pallet <> 0 Then
                sp += " AND acepta_pallet = @acepta_pallet"
            End If

            'think if apply without condition or not.            

            sp += " AND Dañado = @Dañado"

            'Colocar solo en el primer nivel, de lo contrario puede colocar en cualquier nivel.
            If pFiltros.Nivel <> 0 Then
                sp += " AND Nivel = @Nivel"
            End If

            'Solo ubiaciones activas
            sp += " AND Activo = 1 "

            'Solo ubiaciones NO bloqueadas
            sp += " AND Bloqueada = 0 "

            sp += " AND idubicacion NOT IN (SELECT DISTINCT idubicacion FROM stock) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pFiltros.IdBodega)
            If pFiltros.IdPropietario <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pFiltros.IdPropietario)
            If pFiltros.IdTipoRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoRotacion", pFiltros.IdTipoRotacion)
            If pFiltros.IdIndiceRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", pFiltros.IdIndiceRotacion)
            If pFiltros.IdTipoProducto <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pFiltros.IdTipoProducto)
            If pFiltros.IdEstado <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdEstado", pFiltros.IdEstado)
            If pFiltros.IdPresentacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pFiltros.IdPresentacion)
            If pFiltros.Acepta_pallet <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@acepta_pallet", 1)
            dad.SelectCommand.Parameters.AddWithValue("@Dañado", IIf(pFiltros.Dañado, 1, 0))
            If pFiltros.Nivel <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@Nivel", 1)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTramo_por_regla As New clsBeBodega_tramo

            For Each dr As DataRow In dt.Rows

                vBeTramo_por_regla = New clsBeBodega_tramo
                vBeTramo_por_regla.IdTramo = dr.Item(0)
                lReturnList.Add(vBeTramo_por_regla)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_IdBodega(ByVal IdBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim spp As String = "SELECT bodega_tramo.IdTramo, bodega_tramo.descripcion 
                                 FROM  bodega_tramo INNER JOIN 
                                 bodega_sector On bodega_tramo.IdSector = bodega_sector.IdSector 
                                 AND bodega_sector.IdArea = bodega_tramo.IdArea
                                 AND bodega_sector.IdBodega = bodega_tramo.IdBodega
                                 INNER JOIN 
                                 bodega_area ON bodega_sector.IdArea = bodega_area.IdArea 
                                 AND bodega_area.IdBodega = bodega_sector.IdBodega
                                 WHERE  (bodega_area.IdBodega =@IdBodega ) "

            Dim cmd As New SqlCommand(spp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTramo_por_regla As New clsBeBodega_tramo

            For Each dr As DataRow In dt.Rows

                vBeTramo_por_regla = New clsBeBodega_tramo
                vBeTramo_por_regla.IdTramo = dr.Item(0)
                vBeTramo_por_regla.Descripcion = dr.Item(1)
                lReturnList.Add(vBeTramo_por_regla)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Vacias_By_Filtros(ByVal pFiltros As clsBeUbicaciones_por_regla,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeUbicaciones_por_regla)

        Try

            Dim lReturnList As New List(Of clsBeUbicaciones_por_regla)

            Dim sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega"

            If pFiltros.IdPropietario <> 0 Then
                sp += " AND (IdPropietario IS NULL Or IdPropietario = @IdPropietario)"
                sp += " AND (regla_ubic_det_prop_Activo IS NULL Or regla_ubic_det_prop_Activo = 1)"
            End If

            If pFiltros.IdTipoRotacion <> 0 Then
                sp += " AND (IdTipoRotacion IS NULL Or IdTipoRotacion = @IdTipoRotacion)"
            End If

            If pFiltros.IdIndiceRotacion <> 0 Then
                sp += " AND (IdIndiceRotacion IS NULL Or IdIndiceRotacion = @IdIndiceRotacion)"
            End If

            If pFiltros.IdTipoProducto <> 0 Then
                sp += " AND (IdTipoProducto IS NULL Or IdTipoProducto = @IdTipoProducto)"
                sp += " AND (regla_ubic_det_tp_Activo IS NULL Or regla_ubic_det_tp_Activo = 1)"
            End If

            If pFiltros.IdEstado <> 0 Then
                sp += " AND (IdEstado IS NULL Or IdEstado = @IdEstado)"
                sp += " AND (regla_ubic_det_pe_Activo IS NULL Or regla_ubic_det_pe_Activo = 1)"
            End If

            'Si pFiltros.Acepta_pallet = 0, puede colocar pallets y unidades sueltas
            'Si pFiltros.Acepta_pallet = 1, solo puede colocar pallets.

            If pFiltros.IdPresentacion <> 0 Then
                sp += " AND (IdPresentacion IS NULL Or IdPresentacion = @IdPresentacion)"
            End If

            If pFiltros.Acepta_pallet <> 0 Then
                sp += " AND acepta_pallet = @acepta_pallet"
            End If

            'think if apply without condition or not.            

            sp += " AND Dañado = @Dañado"

            'Colocar solo en el primer nivel, de lo contrario puede colocar en cualquier nivel.

            'If pFiltros.Nivel <> 0 Then
            '    sp += " AND Nivel = @Nivel"
            'End If

            'Solo ubiaciones activas
            sp += " AND Activo = 1 "

            'Solo ubiaciones NO bloqueadas
            sp += " AND Bloqueada= 0 "

            sp += " AND idubicacion NOT IN (SELECT DISTINCT idubicacion FROM stock) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pFiltros.IdBodega)
            If pFiltros.IdPropietario <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pFiltros.IdPropietario)
            If pFiltros.IdTipoRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoRotacion", pFiltros.IdTipoRotacion)
            If pFiltros.IdIndiceRotacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdIndiceRotacion", pFiltros.IdIndiceRotacion)
            If pFiltros.IdTipoProducto <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pFiltros.IdTipoProducto)
            If pFiltros.IdEstado <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdEstado", pFiltros.IdEstado)
            If pFiltros.IdPresentacion <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pFiltros.IdPresentacion)
            If pFiltros.Acepta_pallet <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@acepta_pallet", 1)
            dad.SelectCommand.Parameters.AddWithValue("@Dañado", IIf(pFiltros.Dañado, 1, 0))
            'If pFiltros.Nivel <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@Nivel", 1)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUbicaciones_por_regla As New clsBeUbicaciones_por_regla

            For Each dr As DataRow In dt.Rows

                vBeUbicaciones_por_regla = New clsBeUbicaciones_por_regla
                Cargar(vBeUbicaciones_por_regla, dr)
                vBeUbicaciones_por_regla.Descripcion = IIf(IsDBNull(dr.Item("Nombre_Completo")), "", dr.Item("Nombre_Completo"))
                lReturnList.Add(vBeUbicaciones_por_regla)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Ubicacion_Valida_By_IdTipoProducto(ByVal IdTipoProducto As Integer,
                                                              ByVal IdUbicacion As Integer,
                                                              ByVal IdBodega As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As Boolean

        Ubicacion_Valida_By_IdTipoProducto = False

        Try

            Dim lReturnList As New List(Of clsBeUbicaciones_por_regla)

            Dim sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega AND Idubicacion = @IdUbicacion"
            If IdTipoProducto <> 0 Then
                sp += " AND (IdTipoProducto IS NULL Or IdTipoProducto = @IdTipoProducto)"
            End If
            sp += " AND Activo = 1 "
            sp += " AND Bloqueada= 0 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

            If IdTipoProducto <> 0 Then dad.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto)

            Dim dt As New DataTable

            dad.Fill(dt)

            Ubicacion_Valida_By_IdTipoProducto = (dt.Rows.Count > 0)

            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Ubicacion_Tiene_Regla_Asociada(ByVal IdUbicacion As Integer,
                                                          ByVal IdBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Boolean

        Ubicacion_Tiene_Regla_Asociada = False

        Try

            Dim lReturnList As New List(Of clsBeUbicaciones_por_regla)

            Dim sp As String = "SELECT * FROM vw_ubicaciones_por_regla WHERE IdBodega = @IdBodega AND IdUbicacion = @IdUbicacion"
            sp += " AND Activo = 1 "
            sp += " AND Bloqueada= 0 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

            Dim dt As New DataTable

            dad.Fill(dt)

            Ubicacion_Tiene_Regla_Asociada = (dt.Rows.Count > 0)

            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class