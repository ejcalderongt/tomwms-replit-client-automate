Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnProducto
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeProducto As clsBeProducto,
                             ByRef dr As DataRow,
                             ByRef pCampos() As clsBeProducto.ProdPropiedades,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeProducto

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdProducto) Then
                    .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Propietario) Then
                    .Propietario = New clsBePropietarios
                    .Propietario.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                    clsLnPropietarios.Obtener(.Propietario, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Clasificacion) Then
                    .Clasificacion = New clsBeProducto_clasificacion
                    .Clasificacion.IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                    clsLnProducto_clasificacion.Obtener(.Clasificacion, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Familia) Then
                    .Familia = New clsBeProducto_familia
                    .Familia.IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                    clsLnProducto_familia.Obtener(.Familia, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Marca) Then
                    .Marca = New clsBeProducto_marca
                    .Marca.IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                    clsLnProducto_marca.Obtener(.Marca, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.TipoProducto) Then
                    .TipoProducto = New clsBeProducto_tipo
                    .TipoProducto.IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                    clsLnProducto_tipo.Obtener(.TipoProducto, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.UnidadMedida) Then
                    .UnidadMedida = New clsBeUnidad_medida
                    .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                    clsLnUnidad_medida.Obtener(.UnidadMedida, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Presentaciones) Then
                    .Presentaciones = New List(Of clsBeProducto_Presentacion)
                    clsLnProducto_presentacion.Get_All_By_IdProducto(.IdProducto, True, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Arancel) Then
                    .Arancel = New clsBeArancel
                    .Arancel.IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                    clsLnArancel.Obtener(.Arancel, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdCamara) Then
                    .IdCamara = IIf(IsDBNull(dr.Item("IdCamara")), 0, dr.Item("IdCamara"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdTipoRotacion) Then
                    .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdPerfilSerializado) Then
                    .IdPerfilSerializado = IIf(IsDBNull(dr.Item("IdPerfilSerializado")), 0, dr.Item("IdPerfilSerializado"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Codigo) Then
                    .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Nombre) Then
                    .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Codigos_Barra) Then
                    .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Existencia_min) Then
                    .Existencia_min = IIf(IsDBNull(dr.Item("existencia_min")), 0.0, dr.Item("existencia_min"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Existencia_max) Then
                    .Existencia_max = IIf(IsDBNull(dr.Item("existencia_max")), 0.0, dr.Item("existencia_max"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Activo) Then
                    .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Serializado) Then
                    .Serializado = IIf(IsDBNull(dr.Item("serializado")), False, dr.Item("serializado"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Genera_lote) Then
                    .Genera_lote = IIf(IsDBNull(dr.Item("genera_lote")), False, dr.Item("genera_lote"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Genera_LP) Then
                    .Genera_lp = IIf(IsDBNull(dr.Item("genera_lp_old")), False, dr.Item("genera_lp_old"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.User_agr) Then
                    .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Fec_agr) Then
                    .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.User_mod) Then
                    .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Fec_mod) Then
                    .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Costo) Then
                    .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Precio) Then
                    .Precio = IIf(IsDBNull(dr.Item("precio")), 0.0, dr.Item("precio"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Control_vencimiento) Then
                    .Control_vencimiento = IIf(IsDBNull(dr.Item("control_vencimiento")), False, dr.Item("control_vencimiento"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Control_lote) Then
                    .Control_lote = IIf(IsDBNull(dr.Item("control_lote")), False, dr.Item("control_lote"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdSimbologia) Then
                    .IdSimbologia = IIf(IsDBNull(dr.Item("IdSimbologia")), "0", dr.Item("IdSimbologia"))
                End If

                Try
                    If pCampos.Contains(clsBeProducto.ProdPropiedades.Imagen) Then
                        .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))
                    End If
                Catch ex As Exception
                End Try

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Tolerancia) Then
                    .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Ciclo_vida) Then
                    .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Peso_recepcion) Then
                    .Peso_recepcion = IIf(IsDBNull(dr.Item("peso_recepcion")), False, dr.Item("peso_recepcion"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Peso_despacho) Then
                    .Peso_despacho = IIf(IsDBNull(dr.Item("peso_despacho")), False, dr.Item("peso_despacho"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Peso_referencia) Then
                    .Peso_referencia = IIf(IsDBNull(dr.Item("peso_referencia")), 0.0, dr.Item("peso_referencia"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Peso_tolerancia) Then
                    .Peso_tolerancia = IIf(IsDBNull(dr.Item("peso_tolerancia")), 0.0, dr.Item("peso_tolerancia"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Temperatura_recepcion) Then
                    .Temperatura_recepcion = IIf(IsDBNull(dr.Item("temperatura_recepcion")), False, dr.Item("temperatura_recepcion"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Temperatura_despacho) Then
                    .Temperatura_despacho = IIf(IsDBNull(dr.Item("temperatura_despacho")), False, dr.Item("temperatura_despacho"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Temperatura_referencia) Then
                    .Temperatura_referencia = IIf(IsDBNull(dr.Item("temperatura_referencia")), 0.0, dr.Item("temperatura_referencia"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Temperatura_tolerancia) Then
                    .Temperatura_tolerancia = IIf(IsDBNull(dr.Item("temperatura_tolerancia")), 0.0, dr.Item("temperatura_tolerancia"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdIndiceRotacion) Then
                    .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Materia_prima) Then
                    .Materia_prima = IIf(IsDBNull(dr.Item("materia_prima")), False, dr.Item("materia_prima"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Kit) Then
                    .Kit = IIf(IsDBNull(dr.Item("kit")), False, dr.Item("kit"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Tolerancia) Then
                    .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                End If

                'If pCampos.Contains(clsBeProducto.ProdPropiedades.Ciclo_vida) Then
                '    .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                'End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.NoSerie) Then
                    .Noserie = IIf(IsDBNull(dr.Item("NoSerie")), "", dr.Item("NoSerie"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.NoParte) Then
                    .Noparte = IIf(IsDBNull(dr.Item("NoParte")), "", dr.Item("NoParte"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.FechaManufactura) Then
                    .Fechamanufactura = IIf(IsDBNull(dr.Item("FechaManufactura")), False, dr.Item("FechaManufactura"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Capturar_Aniada) Then
                    .Capturar_aniada = IIf(IsDBNull(dr.Item("Capturar_Aniada")), False, dr.Item("Capturar_Aniada"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Control_Peso) Then
                    .Control_peso = IIf(IsDBNull(dr.Item("Control_Peso")), False, dr.Item("Control_Peso"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Captura_Arancel) Then
                    .Captura_arancel = IIf(IsDBNull(dr.Item("captura_arancel")), False, dr.Item("captura_arancel"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Es_Hardware) Then
                    .Es_hardware = IIf(IsDBNull(dr.Item("es_hardware")), False, dr.Item("es_hardware"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Alto) Then
                    .Alto = IIf(IsDBNull(dr.Item("Alto")), 1, dr.Item("Alto"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Largo) Then
                    .Largo = IIf(IsDBNull(dr.Item("Largo")), 1, dr.Item("Largo"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Ancho) Then
                    .Ancho = IIf(IsDBNull(dr.Item("Ancho")), 1, dr.Item("Ancho"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdUnidadMedidaCobro) Then
                    .IdUnidadMedidaCobro = IIf(IsDBNull(dr.Item("IdUnidadMedidaCobro")), 0, dr.Item("IdUnidadMedidaCobro"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Dias_Inventario_Promedio) Then
                    .Dias_Inventario_Promedio = IIf(IsDBNull(dr.Item("Dias_Inventario_Promedio")), 0, dr.Item("Dias_Inventario_Promedio"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Dias_Inventario_Promedio) Then
                    .Dias_Inventario_Promedio = IIf(IsDBNull(dr.Item("Dias_Inventario_Promedio")), 0, dr.Item("Dias_Inventario_Promedio"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.Dias_Inventario_Promedio) Then
                    .Dias_Inventario_Promedio = IIf(IsDBNull(dr.Item("Dias_Inventario_Promedio")), 0, dr.Item("Dias_Inventario_Promedio"))
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.ParametroA) Then
                    .ParametroA = New clsBeProducto_parametro_a
                    .ParametroA.IdProductoParametroA = IIf(IsDBNull(dr.Item("IdProductoParametroA")), 0, dr.Item("IdProductoParametroA"))
                    clsLnProducto_parametro_a.Obtener(.ParametroA, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.ParametroB) Then
                    .ParametroB = New clsBeProducto_parametro_b
                    .ParametroB.IdProductoParametroB = IIf(IsDBNull(dr.Item("IdProductoParametroB")), 0, dr.Item("IdProductoParametroB"))
                    clsLnProducto_parametro_b.Obtener(.ParametroB, lConnection, lTransaction)
                End If

                If pCampos.Contains(clsBeProducto.ProdPropiedades.IdTipoManufactura) Then
                    .IdTipoManufactura = IIf(IsDBNull(dr.Item("IdTipoManufactura")), 0, dr.Item("IdTipoManufactura"))
                End If

            End With

        Catch ex As Exception
            Throw New Exception(String.Format("EX{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Sub

    Public Shared Function Get_Reabastecimientos_Productos() As List(Of clsBeReabasto)

        Dim lReturnList As New List(Of clsBeReabasto)

        Get_Reabastecimientos_Productos = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Revision_Producto "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeReabasto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vIdentificadorVirtual As Integer = 1

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeReabasto
                                Obj.IdReabasto = vIdentificadorVirtual
                                Obj.CodigoProducto = CType(lRow("codigo"), String)
                                Obj.NombreProducto = CType(lRow("nombre"), String)
                                Obj.Presentacion = IIf(IsDBNull(lRow("Presentacion")), "N/A", lRow("Presentacion"))
                                Obj.Estado = CType(lRow("Estado"), String)
                                Obj.Ubicacion = CType(lRow("Ubicacion"), String)
                                Obj.Minimo = IIf(IsDBNull(lRow("Minimo")), 0, lRow("Minimo"))
                                Obj.Maximo = IIf(IsDBNull(lRow("Maximo")), 0, lRow("Maximo"))
                                Obj.Disponible = IIf(IsDBNull(lRow("DisponibleUMBas")), 0, lRow("DisponibleUMBas"))
                                Obj.Factor = IIf(IsDBNull(lRow("Factor")), 0, lRow("Factor"))
                                Obj.Disponible = IIf(Obj.Factor > 0, Math.Round(Obj.Disponible / Obj.Factor, 2), Obj.Disponible)
                                Obj.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                Obj.IdPresentacion = IIf(IsDBNull(lRow("IdPresentacion")), 0, lRow("IdPresentacion"))
                                Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                                Obj.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                Obj.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                                Obj.NomUnidMedBas = CType(lRow("UmBas"), String)
                                Obj.IdPresentacionAbastercerCon = IIf(IsDBNull(lRow("IdPresentacionAbastercerCon")), 0, lRow("IdPresentacionAbastercerCon"))
                                Obj.IdUnidadMedidaBasAbastercerCon = IIf(IsDBNull(lRow("IdUmBasAbastercerCon")), 0, lRow("IdUmBasAbastercerCon"))
                                lReturnList.Add(Obj)
                                vIdentificadorVirtual += 1

                            Next

                            Get_Reabastecimientos_Productos = lReturnList

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

    ''' <summary>
    ''' Funcion que me trae el minimo y el maximo de los productos Recibe como parametro un id de producto DataTable para Reporte
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul Jun2016</remarks>
    Public Shared Function Get_Minimos_y_Maximos_By_IdProducto(ByVal pIdProducto As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = "SELECT * FROM VW_rptMinimosMaximos WHERE IdProducto=@IdProducto "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Codigo_Barra(ByVal pCodigoBarra As String, ByVal pIdProducto As String) As Boolean

        Existe_Codigo_Barra = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE codigo_barra=@codigo_barra AND IdProducto <> @IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra)
                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Existe_Codigo_Barra = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Codigo(ByVal pCodigo As String) As Boolean

        Existe_Codigo = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE codigo=@codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Existe_Codigo = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Stock_By_IdProducto(ByVal pIdProducto As Integer) As Boolean

        Existe_Stock_By_IdProducto = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM stock AS s INNER JOIN producto_bodega AS pb ON s.IdProductoBodega = pb.IdProductoBodega " _
                                                         & "INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto " _
                                                         & "WHERE p.IdProducto=@IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Existe_Stock_By_IdProducto = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Propietario(ByVal pActivoDefault As Boolean,
                                                  ByVal pActivo As Boolean,
                                                  ByVal pIdBodega As Integer,
                                                  ByVal pIdPropietario As Integer) As List(Of clsBeProducto)

        Get_All_By_Propietario = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 "

                    If pActivoDefault = False Then
                        If pActivo Then
                            vSQL += " AND Activo=1"
                        Else
                            vSQL += " AND Activo=0"
                        End If
                    Else
                        vSQL += " AND activo=1 AND activoproductobodega=1 "
                    End If

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeProducto
                                Cargar(Obj, lRow, lConnection, lTransaction)
                                lReturnList.Add(Obj)
                            Next

                            Get_All_By_Propietario = lReturnList

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

    Public Shared Function Get_All_By_Kit(ByVal pActivoDefault As Boolean,
                                          ByVal pActivo As Boolean,
                                          ByVal pIdBodega As Integer,
                                          ByVal pIdPropietario As Integer,
                                          ByVal pIdProductoExepto As Integer) As List(Of clsBeProducto)

        Get_All_By_Kit = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 AND kit=0 "

                    If pActivoDefault = False Then
                        If pActivo Then
                            vSQL += " AND Activo=1"
                        Else
                            vSQL += " AND Activo=0"
                        End If
                    Else
                        vSQL += " AND activo=1 AND activoproductobodega=1 "
                    End If

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    vSQL += String.Format(" AND IdProducto<>{0}", pIdProductoExepto)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeProducto
                                Cargar(Obj, lRow, lConnection, lTransaction)
                                lReturnList.Add(Obj)
                            Next

                            Get_All_By_Kit = lReturnList

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

    Public Shared Function Get_All_ProdKit(ByVal pActivo As Boolean,
                                           ByVal pIdProductoExepto As Integer,
                                           ByVal pIdBodega As Integer,
                                           ByVal pIdPropietario As Integer) As DataTable

        Get_All_ProdKit = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdProducto as Correlativo, 
                         Propietario, 
                         Clasificación,
                         Familia, 
                         Marca, 
                         [Tipo Producto],
                         [Unidad Medida], 
                         Código, [Código de Barra],
                         Producto as Nombre,Kit as Es_Producto_Kit, Control_Lote
                         FROM VW_Producto WHERE 1 > 0 AND kit=0 "

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    vSQL += String.Format(" AND IdProducto<>{0}", pIdProductoExepto)

                    vSQL += " ORDER BY Código"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_ProdKit = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_IdProducto_By_IdPropietario_And_Bodega(ByVal pIdPropietario As Integer,
                                                                          ByVal pIdBodega As Integer) As List(Of Integer)

        Get_All_IdProducto_By_IdPropietario_And_Bodega = Nothing

        Try

            Dim lReturnList As New List(Of Integer)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdProductoBodega FROM VW_ProductoOC WHERE 1 > 0 "

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            For Each lRow As DataRow In lDataTable.Rows
                                lReturnList.Add(lRow.Item("IdProductoBodega"))
                                Application.DoEvents()
                            Next
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario_And_Bodega_Para_Inventario(ByVal pIdPropietario As Integer,
                                                                               ByVal pIdBodega As Integer,
                                                                               ByVal pIdInventarioEnc As Integer,
                                                                               ByVal pConExistencia As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdPropietario_And_Bodega_Para_Inventario = Nothing

        Try

            Dim vSQL As String = "SELECT
                        convert(bit,0) as Seleccionar,
                        producto.codigo AS Codigo, 
                        producto.codigo_barra AS Codigo_Barra,
                        producto.nombre AS Nombre,                            
                        producto_clasificacion.nombre AS Clasificacion, 
                        producto_familia.nombre AS Familia,
                        producto_tipo.NombreTipoProducto, 
                        tipo_rotacion.Descripcion AS TipoRotacion, 
                        indice_rotacion.Descripcion AS IndiceRotacion,
                        producto_bodega.IdProductoBodega,
                        producto.IdProducto, 
                        producto_familia.IdFamilia, 
                        producto_clasificacion.IdClasificacion
                        FROM producto LEFT OUTER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        producto_bodega ON producto.IdProducto = producto_bodega.IdProducto LEFT OUTER JOIN
                        tipo_rotacion ON producto.IdTipoRotacion = tipo_rotacion.IdTipoRotacion LEFT OUTER JOIN
                        indice_rotacion ON producto.IdIndiceRotacion = indice_rotacion.IdIndiceRotacion LEFT OUTER JOIN
                        producto_familia ON producto.IdFamilia = producto_familia.IdFamilia LEFT OUTER JOIN
                        producto_clasificacion ON producto.IdClasificacion = producto_clasificacion.IdClasificacion 
                        WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND producto_bodega.IdBodega=@IdBodega"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND producto.IdPropietario=@IdPropietario"
            End If

            If pConExistencia Then
                '#EJC20180809: Buscar el el inventario congelado cuales tienen existencia y no en el stock actual porque puede variar si es a puerta abierta
                vSQL += " AND producto_bodega.IdProductoBodega in (select IdProductoBodega from trans_inv_stock where IdInventario = @IdInventarioEnc)"
            End If

            '#EJC20180809: Listar solo aquellos productos que aún no han sido adicionados al inventario
            vSQL += " AND producto_bodega.IdProductoBodega NOT in (select IdProductoBodega from trans_inv_ciclico where IdInventarioEnc = @IdInventarioEnc)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Get_All_By_IdPropietario_And_Bodega_Para_Inventario = lDataTable

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                 ByVal pIdBodega As Integer) As List(Of clsBeProducto)


        Get_All_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 "

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto
                                Cargar(Obj, lRow, lConnection, ltransaction)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_IdPropietario_And_IdBodega = lReturnList

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_PropietarioBodega(ByVal pActivoDefault As Boolean,
                                                        ByVal pActivo As Boolean,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal pIdPropietarioBodega As Integer) As List(Of clsBeProducto)

        Get_All_By_PropietarioBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 "

                    If pActivoDefault = False Then
                        If pActivo Then
                            vSQL += " AND Activo=1"
                        Else
                            vSQL += " AND Activo=0"
                        End If
                    Else
                        vSQL += " AND activo=1 AND activoproductobodega=1 "
                    End If

                    If pIdBodega <> 0 Then
                        vSQL += String.Format(" AND IdBodega={0}", pIdBodega)
                    End If

                    If pIdPropietarioBodega <> 0 Then
                        vSQL += String.Format(" AND IdPropietarioBodega={0}", pIdPropietarioBodega)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.Propietario = New clsBePropietarios()
                                    Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                    Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                                    Obj.Clasificacion = New clsBeProducto_clasificacion()
                                    Obj.Clasificacion.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                                    Obj.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                                End If

                                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                                    Obj.Familia = New clsBeProducto_familia()
                                    Obj.Familia.IdFamilia = CType(lRow("IdFamilia"), Integer)
                                    Obj.Familia.Nombre = CType(lRow("Familia"), String)
                                End If

                                If lRow("IdMarca") IsNot DBNull.Value AndAlso lRow("IdMarca") IsNot Nothing Then
                                    Obj.Marca = New clsBeProducto_marca()
                                    Obj.Marca.IdMarca = CType(lRow("IdMarca"), Integer)
                                    Obj.Marca.Nombre = CType(lRow("Marca"), String)
                                End If

                                If lRow("IdTipoProducto") IsNot DBNull.Value AndAlso lRow("IdTipoProducto") IsNot Nothing Then
                                    Obj.TipoProducto = New clsBeProducto_tipo()
                                    Obj.TipoProducto.IdTipoProducto = CType(lRow("IdTipoProducto"), String)
                                    Obj.TipoProducto.NombreTipoProducto = CType(lRow("Tipo Producto"), String)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    Obj.UnidadMedida = New clsBeUnidad_medida
                                    Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), String)
                                    Obj.UnidadMedida.Nombre = CType(lRow("Unidad Medida"), String)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("Codigo"), String)
                                End If

                                If lRow("codigo_barra") IsNot DBNull.Value AndAlso lRow("codigo_barra") IsNot Nothing Then
                                    Obj.Codigo_barra = CType(lRow("codigo_barra"), String)
                                End If

                                If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("nombre"), String)
                                End If

                                If lRow("Existencia_Min") IsNot DBNull.Value AndAlso lRow("Existencia_Min") IsNot Nothing Then
                                    Obj.Existencia_min = CType(lRow("Existencia_Min"), Double)
                                End If

                                If lRow("Existencia_Max") IsNot DBNull.Value AndAlso lRow("Existencia_Max") IsNot Nothing Then
                                    Obj.Existencia_max = CType(lRow("Existencia_Max"), Double)
                                End If

                                If lRow("Costo") IsNot DBNull.Value AndAlso lRow("Costo") IsNot Nothing Then
                                    Obj.Costo = CType(lRow("Costo"), Double)
                                End If

                                If lRow("Precio") IsNot DBNull.Value AndAlso lRow("Precio") IsNot Nothing Then
                                    Obj.Precio = CType(lRow("Precio"), Double)
                                End If

                                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                    Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_PropietarioBodega = lReturnList

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

    ''' <summary>
    ''' Se utiliza para desplegar el listado de productos en la pantalla de "Lista de productos"
    ''' </summary>
    ''' <param name="pActivo">"</param>
    ''' <returns>List(Of clsBeProducto)</returns>

    Public Shared Function Get_All_Producto(ByVal pActivo As Boolean) As List(Of clsBeProducto)

        Get_All_Producto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto WHERE 1 > 0 "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += "ORDER BY Código"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                          If lReturnList Is Nothing Then
                                                                              lReturnList = New List(Of clsBeProducto)
                                                                              Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                          End If

                                                                          SyncLock lReturnList

                                                                              Obj = New clsBeProducto

                                                                              SyncLock Obj

                                                                                  Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                                  If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                                      Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario = New clsBePropietarios()
                                                                                      Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                                  End If

                                                                                  If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                                      Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                                      Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                                  End If

                                                                                  If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                                      Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia = New clsBeProducto_familia()
                                                                                      Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                                  End If

                                                                                  If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                                      Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca = New clsBeProducto_marca()
                                                                                      Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                                  End If

                                                                                  If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                                      Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto = New clsBeProducto_tipo()
                                                                                      Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                                  End If

                                                                                  If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                                      Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida = New clsBeUnidad_medida
                                                                                      Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                                  End If

                                                                                  If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                                      Obj.Codigo = CType(lrow("Código"), String)
                                                                                  End If

                                                                                  If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                                      Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                                  End If

                                                                                  If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                                      Obj.Nombre = CType(lrow("Producto"), String)
                                                                                  End If

                                                                                  If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                                      Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                                  End If

                                                                                  If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                                      Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                                  End If

                                                                                  If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                                      Obj.Costo = CType(lrow("Costo"), Double)
                                                                                  End If

                                                                                  If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                                      Obj.Precio = CType(lrow("Precio"), Double)
                                                                                  End If

                                                                                  If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                                      lReturnList.Add(Obj)
                                                                                  End If

                                                                              End SyncLock

                                                                          End SyncLock

                                                                      End Sub)

                        End If

                        Get_All_Producto = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Producto(ByVal pActivo As Boolean,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto)

        Get_All_Producto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto WHERE 1 > 0 "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            vSQL += "ORDER BY Código"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                  If lReturnList Is Nothing Then
                                                                      lReturnList = New List(Of clsBeProducto)
                                                                      Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                  End If

                                                                  SyncLock lReturnList

                                                                      Obj = New clsBeProducto

                                                                      SyncLock Obj

                                                                          Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                          If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                              Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario = New clsBePropietarios()
                                                                              Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                          End If

                                                                          If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                              Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                              Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                          End If

                                                                          If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                              Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia = New clsBeProducto_familia()
                                                                              Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                          End If

                                                                          If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                              Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca = New clsBeProducto_marca()
                                                                              Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                          End If

                                                                          If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                              Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto = New clsBeProducto_tipo()
                                                                              Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                          End If

                                                                          If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                              Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida = New clsBeUnidad_medida
                                                                              Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                          End If

                                                                          If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                              Obj.Codigo = CType(lrow("Código"), String)
                                                                          End If

                                                                          If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                              Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                          End If

                                                                          If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                              Obj.Nombre = CType(lrow("Producto"), String)
                                                                          End If

                                                                          If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                              Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                          End If

                                                                          If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                              Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                          End If

                                                                          If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                              Obj.Costo = CType(lrow("Costo"), Double)
                                                                          End If

                                                                          If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                              Obj.Precio = CType(lrow("Precio"), Double)
                                                                          End If

                                                                          If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                              lReturnList.Add(Obj)
                                                                          End If

                                                                      End SyncLock

                                                                  End SyncLock

                                                              End Sub)

                End If

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_Lista_Producto(ByVal pActivo As Boolean) As DataTable

        Get_All_Lista_Producto = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdProducto as Correlativo, 
                         Propietario, 
                         Clasificación,
                         Familia, 
                         Marca, 
                         [Tipo Producto],
                         [Unidad Medida], 
                         Código, [Código de Barra],
                         Producto as Nombre,
                         IndiceRotacion,
                         Kit as Es_Producto_Kit,
                         Control_Vencimiento,
                         Control_Lote,
                         producto_parametro_nombreA as [Parámetro A],
                         producto_parametro_nombreB as [Parámetro B],
                         fec_agr as Fecha_Creacion,
                         noparte as NoParte,
                         noserie as NoSerie
                         FROM VW_Producto WHERE 1 > 0 "

                    If pActivo Then
                        vSQL += " AND Activo=1 "
                    Else
                        vSQL += " AND Activo=0 "
                    End If

                    vSQL += " ORDER BY Código"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandTimeout = 300
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_Lista_Producto = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto)

        Get_All_By_IdPropietario = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_Producto WHERE 1 > 0 "

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    vSQL += "AND IdPropietario=@IdPropietario"

                    vSQL += " ORDER BY Código"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProducto As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                          If lReturnList Is Nothing Then
                                                                              lReturnList = New List(Of clsBeProducto)
                                                                              Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                          End If

                                                                          SyncLock lReturnList

                                                                              BeProducto = New clsBeProducto

                                                                              SyncLock BeProducto

                                                                                  BeProducto.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                                  If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                                      BeProducto.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      BeProducto.Propietario = New clsBePropietarios()
                                                                                      BeProducto.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      BeProducto.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                                  End If

                                                                                  If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                                      BeProducto.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      BeProducto.Clasificacion = New clsBeProducto_clasificacion()
                                                                                      BeProducto.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      BeProducto.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                                  End If

                                                                                  If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                                      BeProducto.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      BeProducto.Familia = New clsBeProducto_familia()
                                                                                      BeProducto.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      BeProducto.Familia.Nombre = CType(lrow("Familia"), String)
                                                                                  End If

                                                                                  If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                                      BeProducto.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      BeProducto.Marca = New clsBeProducto_marca()
                                                                                      BeProducto.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      BeProducto.Marca.Nombre = CType(lrow("Marca"), String)
                                                                                  End If

                                                                                  If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                                      BeProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      BeProducto.TipoProducto = New clsBeProducto_tipo()
                                                                                      BeProducto.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      BeProducto.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                                  End If

                                                                                  If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                                      BeProducto.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      BeProducto.UnidadMedida = New clsBeUnidad_medida
                                                                                      BeProducto.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      BeProducto.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                                  End If

                                                                                  If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                                      BeProducto.Codigo = CType(lrow("Código"), String)
                                                                                  End If

                                                                                  If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                                      BeProducto.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                                  End If

                                                                                  If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                                      BeProducto.Nombre = CType(lrow("Producto"), String)
                                                                                  End If

                                                                                  If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                                      BeProducto.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                                  End If

                                                                                  If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                                      BeProducto.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                                  End If

                                                                                  If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                                      BeProducto.Costo = CType(lrow("Costo"), Double)
                                                                                  End If

                                                                                  If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                                      BeProducto.Precio = CType(lrow("Precio"), Double)
                                                                                  End If

                                                                                  If Not lReturnList.Exists(Function(x) x.IdProducto = BeProducto.IdProducto) Then
                                                                                      lReturnList.Add(BeProducto)
                                                                                  End If

                                                                              End SyncLock

                                                                          End SyncLock

                                                                      End Sub)
                        End If

                        Get_All_By_IdPropietario = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Producto_With_Existencia_By_IdBodega(ByVal IdBodega As Integer,
                                                                        ByVal pActivo As Boolean) As List(Of clsBeProducto)

        Dim lReturnList As New List(Of clsBeProducto)

        Get_All_Producto_With_Existencia_By_IdBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_Producto WHERE 1 > 0 "

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto
                        Dim vIdProductoBodega As Integer = 0

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then


                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                    Obj.Propietario = New clsBePropietarios()
                                    Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                    Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                                    Obj.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                                    Obj.Clasificacion = New clsBeProducto_clasificacion()
                                    Obj.Clasificacion.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                                    Obj.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                                End If

                                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                                    Obj.IdFamilia = CType(lRow("IdFamilia"), Integer)
                                    Obj.Familia = New clsBeProducto_familia()
                                    Obj.Familia.IdFamilia = CType(lRow("IdFamilia"), Integer)
                                    Obj.Familia.Nombre = CType(lRow("Familia"), String)
                                End If

                                If lRow("IdMarca") IsNot DBNull.Value AndAlso lRow("IdMarca") IsNot Nothing Then
                                    Obj.IdMarca = CType(lRow("IdMarca"), Integer)
                                    Obj.Marca = New clsBeProducto_marca()
                                    Obj.Marca.IdMarca = CType(lRow("IdMarca"), Integer)
                                    Obj.Marca.Nombre = CType(lRow("Marca"), String)
                                End If

                                If lRow("IdTipoProducto") IsNot DBNull.Value AndAlso lRow("IdTipoProducto") IsNot Nothing Then
                                    Obj.IdTipoProducto = CType(lRow("IdTipoProducto"), String)
                                    Obj.TipoProducto = New clsBeProducto_tipo()
                                    Obj.TipoProducto.IdTipoProducto = CType(lRow("IdTipoProducto"), String)
                                    Obj.TipoProducto.NombreTipoProducto = CType(lRow("Tipo Producto"), String)
                                End If

                                If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                    Obj.IdUnidadMedidaBasica = CType(lRow("IdUnidadMedidaBasica"), String)
                                    Obj.UnidadMedida = New clsBeUnidad_medida
                                    Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), String)
                                    Obj.UnidadMedida.Nombre = CType(lRow("Unidad Medida"), String)
                                End If

                                If lRow("Código") IsNot DBNull.Value AndAlso lRow("Código") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("Código"), String)
                                End If

                                If lRow("Código de Barra") IsNot DBNull.Value AndAlso lRow("Código de Barra") IsNot Nothing Then
                                    Obj.Codigo_barra = CType(lRow("Código de Barra"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("Producto"), String)
                                End If

                                If lRow("Existencia Mínima") IsNot DBNull.Value AndAlso lRow("Existencia Mínima") IsNot Nothing Then
                                    Obj.Existencia_min = CType(lRow("Existencia Mínima"), Double)
                                End If

                                If lRow("Existencia Máxima") IsNot DBNull.Value AndAlso lRow("Existencia Máxima") IsNot Nothing Then
                                    Obj.Existencia_max = CType(lRow("Existencia Máxima"), Double)
                                End If

                                If lRow("Costo") IsNot DBNull.Value AndAlso lRow("Costo") IsNot Nothing Then
                                    Obj.Costo = CType(lRow("Costo"), Double)
                                End If

                                If lRow("Precio") IsNot DBNull.Value AndAlso lRow("Precio") IsNot Nothing Then
                                    Obj.Precio = CType(lRow("Precio"), Double)
                                End If

                                vIdProductoBodega = Get_IdProductoBodega_By_IdProducto_And_IdBodega(Obj.IdProducto, IdBodega, lConnection, lTransaction)

                                Dim ObjS As New clsBeStock() _
                            With {
                            .IdProductoBodega = vIdProductoBodega,
                            .IdUnidadMedida = Obj.IdUnidadMedidaBasica
                            }

                                Obj.ExistenciaUMBas = clsLnStock.Get_Existencia_Disp_By_IdProducto(
                                                                                                ObjS,
                                                                                                IdBodega,
                                                                                                True,
                                                                                                False,
                                                                                                0,
                                                                                                False,
                                                                                                lConnection,
                                                                                                lTransaction)

                                lReturnList.Add(Obj)

                            Next

                            Get_All_Producto_With_Existencia_By_IdBodega = lReturnList

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

    Public Shared Function Get_Single_By_IdProducto(ByVal pIdProducto As Integer) As clsBeProducto

        Get_Single_By_IdProducto = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto WHERE IdProducto=@IdProducto "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()

                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False
                            Get_Single_By_IdProducto = ObjProducto

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

    Public Shared Function Get_Single_By_CodigoProducto(ByVal pCodigoProducto As String) As clsBeProducto

        Get_Single_By_CodigoProducto = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto WHERE codigo=@codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()

                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False

                            Get_Single_By_CodigoProducto = ObjProducto

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

    Public Shared Function Get_Id_Unidad_Medida_By_Codigo(ByVal Codigo As String) As Integer

        Get_Id_Unidad_Medida_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdUnidadMedidaBasica FROM producto WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", Codigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Get_Id_Unidad_Medida_By_Codigo = lDT.Rows(0).Item("IdUnidadMedidaBasica")
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

    Public Shared Function Get_Id_Unidad_Medida_By_Codigo(ByVal Codigo As String,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Get_Id_Unidad_Medida_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdUnidadMedidaBasica FROM producto WHERE Codigo=@codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", Codigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Get_Id_Unidad_Medida_By_Codigo = lDT.Rows(0).Item("IdUnidadMedidaBasica")
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByVal codigo As String) As clsBeProducto

        Get_Single_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", codigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()

                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False

                            Get_Single_By_Codigo = ObjProducto

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

    Public Shared Function Get_Single_By_Codigo(ByVal codigo As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE codigo=@codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", codigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    Get_Single_By_Codigo = ObjProducto
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String,
                                                ByVal pCampos() As clsBeProducto.ProdPropiedades,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE codigo=@codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()

                    Cargar(ObjProducto, lRow, pCampos, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    Get_Single_By_Codigo = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String,
                                                ByVal pCampos() As clsBeProducto.ProdPropiedades) As clsBeProducto

        Get_Single_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()

                            Cargar(ObjProducto, lRow, pCampos, lConnection, lTransaction)
                            ObjProducto.IsNew = False
                            Get_Single_By_Codigo = ObjProducto

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

    Public Shared Function Get_Single_By_IdProducto_And_IdPropietario(ByVal pIdProducto As Integer,
                                                                      ByVal pIdPropietario As Integer) As clsBeProducto

        Get_Single_By_IdProducto_And_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto WHERE IdProducto=@codigo AND IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()
                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False
                            Get_Single_By_IdProducto_And_IdPropietario = ObjProducto
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

    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE IdProducto=@IdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    GetSingle = ObjProducto

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByVal pCampos() As clsBeProducto.ProdPropiedades) As clsBeProducto

        GetSingle = Nothing

        Try



            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM producto WHERE IdProducto=@IdProducto "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()
                            Cargar(ObjProducto, lRow, pCampos, lConnection, lTransaction)
                            ObjProducto.IsNew = False
                            GetSingle = ObjProducto

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

    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByVal pCampos() As clsBeProducto.ProdPropiedades,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE IdProducto=@IdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar(ObjProducto, lRow, pCampos, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    GetSingle = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByVal IdBodega As Integer) As clsBeProducto

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  
                                  WHERE (IdBodega = @IdBodega 
                                  And IdProducto=@IdProducto) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim oBeProducto As New clsBeProducto()

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            GetSingle = oBeProducto

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

    '#EJC20171021_1055AM: GetSingle en producto que busca el producto por IdBodega e IdProducto con transacción para detalle de pedido
    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByVal IdBodega As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                                 " WHERE (IdBodega = @IdBodega " &
                                 " And IdProducto=@IdProducto) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim oBeProducto As New clsBeProducto()

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Return oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_BeProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As clsBeProducto

        Get_Single_BeProducto_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 p.* FROM Producto_Bodega AS pb 
                                  INNER JOIN Producto AS p ON pb.IdProducto = p.IdProducto 
                                  AND pb.IdProductoBodega=@IdProductoBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()
                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            Get_Single_BeProducto_By_IdProductoBodega = ObjProducto

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

    Public Shared Function Get_Single_BeProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_BeProducto_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 p.* FROM Producto_Bodega AS pb 
                                  INNER JOIN Producto AS p ON pb.IdProducto = p.IdProducto 
                                  AND pb.IdProductoBodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    Get_Single_BeProducto_By_IdProductoBodega = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_Producto_Bodega(ByVal pIdProductoBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_Producto_Bodega = Nothing

        Try

            Dim vSQL As String = "SELECT p.* FROM Producto_Bodega AS pb 
                                  INNER JOIN Producto AS p ON pb.IdProducto = p.IdProducto 
                                  AND pb.IdProductoBodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    Get_Single_Producto_Bodega = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = "SELECT p.* FROM Producto_Bodega AS pb 
                                  INNER JOIN Producto AS p ON pb.IdProducto = p.IdProducto 
                                  AND pb.IdProductoBodega=@IdProductoBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Dim pCampos(7) As clsBeProducto.ProdPropiedades
                    pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                    pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                    pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                    pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                    pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                    pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                    pCampos(6) = clsBeProducto.ProdPropiedades.UnidadMedida
                    Cargar(ObjProducto, lRow, pCampos, lConnection, lTransaction)
                    Get_Single_By_IdProductoBodega = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdProducto(ByVal pIdProducto As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_IdProducto = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto WHERE IdProducto=@IdProducto"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()

                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    Get_Single_By_IdProducto = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdProducto As Integer) As Boolean

        Exists = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(IdProducto) FROM producto WHERE IdProducto=@IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Exists = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdProducto As Integer,
                                  ByVal lConection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(IdProducto) FROM producto WHERE IdProducto=@IdProducto"

            Using lCommand As New SqlCommand(vSQL, lConection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'GT 17052021 Valida existencia por codigo producto para el inv. teórico importado por Excel
    Public Shared Function Exist_by_Codigo(ByVal pCodigo As String) As Boolean

        Exist_by_Codigo = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(codigo) FROM producto WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Exist_by_Codigo = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pNomProducto As String,
                                  ByVal lConection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lExists As Integer = 0

            Dim vSQL As String = "SELECT IdProducto FROM producto WHERE Nombre=@pNomProducto"

            Using lCommand As New SqlCommand(vSQL, lConection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@pNomProducto", pNomProducto)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = lReturnValue
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String,
                                  ByRef lConnection As SqlConnection,
                                  ByRef ltransaction As SqlTransaction) As clsBeProducto

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Producto WHERE codigo= @Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = ltransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjP As New clsBeProducto()
                    Cargar(ObjP, lRow, lConnection, ltransaction)
                    Existe = ObjP

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String) As clsBeProducto

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Producto WHERE codigo= @Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjP As New clsBeProducto()
                            Cargar(ObjP, lRow, lConnection, lTransaction)
                            Existe = ObjP

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

    Public Shared Function Existe(ByVal pCodigo As String,
                                  ByVal pIdUnidadMedida As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As Boolean

        Existe = False

        Try

            Dim vSQL As String = "SELECT * FROM Producto WHERE codigo= @Codigo AND IdUnidadMedidaBasica = @IdUnidadMedidaBasica"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedidaBasica", pIdUnidadMedida)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Existe = (lDT.Rows.Count > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega(ByVal pIdBodega As Integer,
                                                                                     ByVal pIdPropietarioBodega As Integer,
                                                                                     ByVal pCodigoProducto As String) As Boolean

        Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = String.Format("SELECT COUNT(pb.IdProductoBodega) 
                                                         FROM  dbo.producto_bodega As pb 
                                                         INNER JOIN dbo.producto AS p ON pb.IdProducto = p.IdProducto 
                                                         INNER JOIN propietarios As pr On p.IdPropietario = pr.IdPropietario  
                                                         INNER JOIN propietario_bodega AS ppb ON p.IdPropietario = ppb.IdPropietario  
                                                         LEFT OUTER JOIN dbo.producto_codigos_barra As pcb On p.IdProducto = pcb.IdProducto 
                                                         LEFT OUTER JOIN dbo.producto_presentacion As pp On p.IdProducto = pp.IdProducto 
                                                         WHERE pb.IdBodega={0} And ppb.IdPropietarioBodega={1} And (pb.activo = 1) And (p.activo = 1) And 
                                                         (p.codigo ='{2}') or (p.codigo_barra='{2}') or (pcb.codigo_barra ='{2}') or (pp.codigo_barra ='{2}')", pIdBodega, pIdPropietarioBodega, pCodigoProducto)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega = CInt(lReturnValue) > 0
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

    Public Shared Function Delete(ByVal pIdProducto As Integer) As Integer

        Delete = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = String.Format("DELETE FROM producto WHERE IdProducto={0}", pIdProducto)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        Delete = lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProducto_By_Codigo(ByVal pCodigo As String) As Integer

        Get_IdProducto_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto 
                                        FROM producto p
                                        LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                        ON p.IdProducto = pcb.IdProducto 
                                        LEFT OUTER JOIN producto_presentacion AS pp
                                        ON p.IdProducto = pp.IdProducto
                                        WHERE (p.codigo =@Codigo or 
                                        p.codigo_barra=@Codigo or 
                                        pcb.codigo_barra =@Codigo or 
                                        pp.codigo_barra =@Codigo)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProducto_By_Codigo = CType(lRow("IdProducto"), Integer)
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

    Public Shared Function Get_Codigo_By_IdProducto(ByVal pIdProducto As Integer) As String

        Get_Codigo_By_IdProducto = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT p.Codigo
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_Codigo_By_IdProducto = CType(lRow("Codigo"), String)

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

    Public Shared Function Get_Codigo_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As String

        Get_Codigo_By_IdProductoBodega = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT p.Codigo
                        FROM producto p INNER JOIN producto_bodega pb ON
                        p.IdProducto = pb.IdProducto
                        WHERE (pb.IdProductoBodega=@IdProductoBodega)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_Codigo_By_IdProductoBodega = CType(lRow("Codigo"), String)

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

    Public Shared Function Get_Codigo_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdProductoBodega = ""

        Try

            Dim vSQL As String = "SELECT p.Codigo
                        FROM producto p INNER JOIN producto_bodega pb ON
                        p.IdProducto = pb.IdProducto
                        WHERE (pb.IdProductoBodega=@IdProductoBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_Codigo_By_IdProductoBodega = CType(lRow("Codigo"), String)

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_By_IdProducto(ByVal pIdProducto As Integer) As String

        Get_Nombre_By_IdProducto = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT p.nombre
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_Nombre_By_IdProducto = CType(lRow("nombre"), String)

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

    Public Shared Function Get_Nombre_By_IdProducto(ByVal pIdProducto As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As String

        Get_Nombre_By_IdProducto = ""

        Try

            Dim vSQL As String = "SELECT p.nombre
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

            Dim lDT As New DataTable
            dad.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                Dim lRow As DataRow = lDT.Rows(0)
                Get_Nombre_By_IdProducto = IIf(IsDBNull(lRow("nombre")), "", CType(lRow("nombre"), String))

            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Tipo_Rotacion_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As Integer

        Get_Tipo_Rotacion_By_IdProductoBodega = 0

        Try

            Dim vSQL As String = "SELECT producto_bodega.IdProductoBodega, producto.IdTipoRotacion
                        FROM  producto_bodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto
                        WHERE producto_bodega.IdProductoBodega = @IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_Tipo_Rotacion_By_IdProductoBodega = IIf(IsDBNull(lRow("IdTipoRotacion")), 0, lRow("IdTipoRotacion"))

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Id_Producto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As Integer

        Get_Id_Producto_By_IdProductoBodega = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto
                FROM producto_bodega AS pb  
                INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                WHERE pb.activo = 1 
                and (pb.IdProductoBodega =@IdProductoBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_Id_Producto_By_IdProductoBodega = CType(lRow("IdProducto"), Integer)

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As Integer

        Get_IdProducto_By_IdProductoBodega = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto
            FROM producto_bodega AS pb  
            INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
            WHERE pb.activo = 1 
            and (pb.IdProductoBodega =@IdProductoBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProducto_By_IdProductoBodega = CType(lRow("IdProducto"), Integer)
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

    Public Shared Function Get_Single_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As clsBeProducto

        Get_Single_By_IdProductoBodega = Nothing

        Dim lIdProducto As Integer = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto
            FROM producto_bodega AS pb  
            INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
            WHERE pb.activo = 1 
            and (pb.IdProductoBodega =@IdProductoBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim pBeProducto As New clsBeProducto

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            lIdProducto = CType(lRow("IdProducto"), Integer)
                            pBeProducto.IdProducto = lIdProducto
                            pBeProducto = GetSingle(lIdProducto, lConnection, lTransaction)
                            pBeProducto.IdProductoBodega = pIdProductoBodega
                            Get_Single_By_IdProductoBodega = pBeProducto

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

    ''' <summary>
    ''' Obtiene el IdProductoBodega a partir de cualquiera de sus códigos asociados.
    ''' </summary>
    ''' <param name="pCodigo">Código de producto, Código de Barra en Producto, Código de barra en producto_codigos_barra </param>
    ''' <returns>IdProductoBodega de la tabla producto_bodega donde Activo = 1</returns>
    ''' <remarks>ejcalderon_201605166</remarks>
    Public Shared Function Get_IdProductoBodega_By_Codigo(ByVal pCodigo As String) As Integer

        Get_IdProductoBodega_By_Codigo = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT DISTINCT pb.IdProductoBodega 
                                         FROM producto_bodega AS pb  
                                         INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                                         LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                         ON p.IdProducto = pcb.IdProducto 
                                         LEFT OUTER JOIN producto_presentacion AS pp 
                                         ON p.IdProducto = pp.IdProducto 
                                         WHERE pb.activo = 1 
                                         and (p.codigo =@Codigo or p.codigo_barra=@Codigo or pcb.codigo_barra =@Codigo or pp.codigo_barra =@Codigo)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProductoBodega_By_Codigo = CType(lRow("IdProductoBodega"), Integer)

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

    Public Shared Function Get_IdTipoProducto_By_IdProducto(ByVal pIdProducto As Integer) As Integer

        Get_IdTipoProducto_By_IdProducto = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT ISNULL(IdTipoProducto,0) AS IdTipoProducto
                                          FROM producto IdProducto=@IdProducto)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdTipoProducto_By_IdProducto = CType(lRow("IdTipoProducto"), Integer)

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

    Public Shared Function Get_IdTipoProducto_By_IdProducto(ByVal pIdProducto As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Get_IdTipoProducto_By_IdProducto = 0

        Try

            Dim vSQL As String = "SELECT ISNULL(IdTipoProducto,0) AS IdTipoProducto
                                          FROM producto WHERE (IdProducto=@IdProducto)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdTipoProducto_By_IdProducto = CType(lRow("IdTipoProducto"), Integer)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProductoBodega_By_Codigo(ByVal pCodigo As String,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Get_IdProductoBodega_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT DISTINCT pb.IdProductoBodega 
                     FROM producto_bodega AS pb  
                     INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                     LEFT OUTER JOIN producto_codigos_barra AS pcb 
                     ON p.IdProducto = pcb.IdProducto 
                     LEFT OUTER JOIN producto_presentacion AS pp 
                     ON p.IdProducto = pp.IdProducto 
                     WHERE pb.activo = 1 
                     and (p.codigo =@Codigo or p.codigo_barra=@Codigo or pcb.codigo_barra =@Codigo or pp.codigo_barra =@Codigo)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdProductoBodega_By_Codigo = CType(lRow("IdProductoBodega"), Integer)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProductoBodega_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                           ByVal pIdBodega As Integer) As Integer

        Dim lIdProductoBodega As Integer = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT pb.IdProductoBodega 
                         FROM producto_bodega AS pb  
                         INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                         WHERE pb.activo = 1 
                         and (p.IdProducto =@IdProducto AND pb.IdBodega =@IdBodega) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            lIdProductoBodega = CType(lRow("IdProductoBodega"), Integer)

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lIdProductoBodega

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProductoBodega_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                           ByVal pIdBodega As Integer,
                                                                           ByRef lConection As SqlConnection,
                                                                           ByRef lTransaction As SqlTransaction) As Integer

        Dim lIdProductoBodega As Integer = 0

        Try

            Dim vSQL As String = "SELECT pb.IdProductoBodega 
                     FROM producto_bodega AS pb  
                     INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                     WHERE pb.activo = 1 
                     AND (p.IdProducto =@IdProducto AND pb.IdBodega =@IdBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    lIdProductoBodega = CType(lRow("IdProductoBodega"), Integer)

                End If

            End Using

            Return lIdProductoBodega

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function GetCodigos(ByVal pIdBodega As Integer,
    '                                  ByVal IdPropietario As Integer) As DataTable

    '    GetCodigos = New DataTable

    '    Try

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            Dim vSQL As String = "SELECT p.codigo  FROM producto AS p inner join producto_bodega pb   on p.idproducto = pb.Idproducto  WHERE p.IdPropietario = @IdPropietario AND pb.IdBodega =@IdBodega UNION SELECT pcb.codigo_barra  FROM producto AS pcb   inner join producto_bodega pb    on pcb.idproducto = pb.Idproducto  WHERE pcb.IdPropietario = @IdPropietario AND pb.IdBodega =@IdBodega UNION SELECT pcbb.codigo_barra from   producto_codigos_barra pcbb  inner join producto_bodega pb   on pcbb.idproducto = pb.Idproducto  inner join producto prod on pcbb.IdProducto = prod.IdProducto  WHERE prod.IdPropietario = @IdPropietario AND pb.IdBodega =@IdBodega UNION SELECT pp.codigo_barra from   producto_presentacion pp  inner join producto_bodega pb   on pp.idproducto = pb.Idproducto  inner join producto prod on pp.IdProducto = prod.IdProducto  and pp.IdProducto = prod.IdProducto  WHERE prod.IdPropietario = @IdPropietario AND pb.IdBodega =@IdBodega"

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
    '                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

    '                Dim lDT As New DataTable
    '                lDTA.Fill(lDT)

    '            End Using

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_Control_Vencimiento_By_IdProducto(ByVal pIdProducto As Integer) As clsBeProducto

        Get_Control_Vencimiento_By_IdProducto = Nothing

        Try

            Dim Obj As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT control_vencimiento,control_peso,control_lote FROM producto WHERE IdProducto=@IdProducto"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("Control_vencimiento") IsNot DBNull.Value AndAlso lRow("Control_vencimiento") IsNot Nothing Then
                                Obj.Control_vencimiento = CType(lRow("Control_vencimiento"), Boolean)
                            End If

                            If lRow("Control_Peso") IsNot DBNull.Value AndAlso lRow("Control_Peso") IsNot Nothing Then
                                Obj.Control_peso = CType(lRow("Control_Peso"), Boolean)
                            End If

                            If lRow("Control_lote") IsNot DBNull.Value AndAlso lRow("Control_lote") IsNot Nothing Then
                                Obj.Control_lote = CType(lRow("Control_lote"), Boolean)
                            End If

                            Get_Control_Vencimiento_By_IdProducto = Obj

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

    Public Shared Function Get_BeProducto_By_Codigo(ByVal pCodigo As String) As clsBeProducto

        Get_BeProducto_By_Codigo = Nothing

        Dim Obj As New clsBeProducto

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI 
                             WHERE 
                             (codigo =@Codigo or codigo_barra=@Codigo or 
                            codigo_barra_pcb =@Codigo or 
                            codigo_barra_presentacion =@Codigo)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(Obj, lRow, lConnection, lTransaction)

                            Obj.IdProducto = CType(lRow("IdProducto"), Integer)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("nombre"), String)
                            End If

                            If lRow("costo") IsNot DBNull.Value AndAlso lRow("costo") IsNot Nothing Then
                                Obj.Costo = CType(lRow("costo"), Double)
                            End If

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                            End If

                            If lRow("Control_vencimiento") IsNot DBNull.Value AndAlso lRow("Control_vencimiento") IsNot Nothing Then
                                Obj.Control_vencimiento = CType(lRow("Control_vencimiento"), Boolean)
                            End If

                            Get_BeProducto_By_Codigo = Obj

                        End If

                    End Using 'SqlDataAdapter

                    lTransaction.Commit()

                End Using 'ltransaction       

                lConnection.Close()

            End Using 'lConection

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Busca un producto por su código, pero ademas el IdProductoBodega por bodega.
    ''' Se utiliza para buscar un producto en el proceso de digitación del detalle del pedido.
    ''' </summary>
    ''' <param name="pCodigo">Código a traves del cuál se busca el producto</param>
    ''' <param name="IdBodega">IdBodega por el cual se busca el producto</param>
    ''' <returns>devuelve un objeto del tipo clsBeProducto</returns>
    ''' <remarks>ejcalderon_20160519</remarks>
    Public Shared Function Get_BeProducto_By_Codigo(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto

        Get_BeProducto_By_Codigo = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                                     " WHERE IdBodega = @IdBodega " &
                                     " And ((codigo =@Codigo) Or (codigo_barra=@Codigo) Or (codigo_barra_pcb =@Codigo) Or (codigo_barra_presentacion =@Codigo)) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_Codigo = oBeProducto

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

    ''' <summary>
    ''' Creé una copia de esta función para agregar filtro por IdPropietario
    ''' Busca un producto por su código, pero ademas el IdProductoBodega por bodega.
    ''' Se utiliza para buscar un producto en el proceso de digitación del detalle del pedido.
    ''' </summary>
    ''' <param name="pCodigo">Código a traves del cuál se busca el producto</param>
    ''' <param name="IdBodega">IdBodega por el cual se busca el producto</param>
    ''' <returns>devuelve un objeto del tipo clsBeProducto</returns>
    ''' <remarks>ejcalderon_20160519</remarks>
    Public Shared Function Get_BeProducto_By_Codigo(ByVal pCodigo As String,
                                                    ByVal IdBodega As Integer,
                                                    ByVal IdPropietario As Integer) As clsBeProducto

        Get_BeProducto_By_Codigo = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI 
                                          WHERE IdBodega = @IdBodega
                                            And IdPropietario = @IdPropietario
                                            And ((codigo =@Codigo) Or (codigo_barra=@Codigo) Or (codigo_barra_pcb =@Codigo) Or (codigo_barra_presentacion =@Codigo)) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_Codigo = oBeProducto

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

    '#EJC20171107_REF14_0358PM: Función GetByCodigo con transacción para importación de detalle de pedido de traslado en interface
    Public Shared Function Get_BeProducto_By_Codigo(ByVal pCodigo As String,
                                                    ByVal IdBodega As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_BeProducto_By_Codigo = Nothing

        Dim oBeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                   " WHERE IdBodega = @IdBodega " &
                   " And ((codigo =@Codigo) Or (codigo_barra=@Codigo) Or (codigo_barra_pcb =@Codigo) Or (codigo_barra_presentacion =@Codigo)) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Get_BeProducto_By_Codigo = oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProducto_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                              ByVal IdBodega As Integer) As clsBeProducto

        Get_BeProducto_By_IdProductoBodega = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI  
                            WHERE IdBodega = @IdBodega 
                            And IdProductoBodega=@IdProductoBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_IdProductoBodega = oBeProducto

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

    Public Shared Sub UpdateCosto(ByVal pConnection As SqlConnection,
                                  ByVal pTransaction As SqlTransaction,
                                  ByVal pObjP As clsBeProducto)

        Try

            Dim vSQL As String = "UPDATE Producto SET Costo=@Costo WHERE IdProducto=@IdProducto"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProducto", pObjP.IdProducto)
                lCommand.Parameters.AddWithValue("@Costo", pObjP.Costo)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        MaxID = 1

        Try

            Dim vSQL As String = "SELECT  MAX(IdProducto) + 1 as nuevo FROM producto"

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                MaxID = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProducto),0) FROM Producto "

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Peso_Referencia(ByVal pIdProducto As Integer) As Double

    '    Get_Peso_Referencia = 0

    '    Try

    '        Dim vSQL As String = "SELECT  peso_referencia FROM producto WHERE idProducto = @IdProducto "

    '        Dim sp As String = vSQL
    '        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '        Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        Dim dt As New DataTable

    '        cmd.Parameters.AddWithValue("@IdProducto", pIdProducto)

    '        dad.Fill(dt)

    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        cmd.Dispose()
    '        dad.Dispose()

    '        If dt.Rows.Count > 0 Then
    '            Get_Peso_Referencia = IIf(IsDBNull(dt.Rows(0).Item("peso_referencia")), "0", dt.Rows(0).Item("peso_referencia"))
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function get_Peso_Referencia(ByVal pIdProducto As Integer,
                                               ByVal pConnection As SqlConnection,
                                               ByVal pTransaction As SqlTransaction) As Double

        Try

            Dim lPesoRef As Double = 0

            Dim vSQL As String = "SELECT peso_referencia FROM Producto WHERE IdProducto = @IdProducto"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lPesoRef = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lPesoRef

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function InsertarValidaCodigoBarra(ByRef oBeProducto As clsBeProducto,
    '                                                 Optional ByVal pConection As SqlConnection = Nothing,
    '                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing
    '    Dim cmd As New SqlCommand

    '    Try

    '        If String.IsNullOrEmpty(oBeProducto.Codigo_barra) = False Then

    '            If Existe_Codigo_Barra(oBeProducto.Codigo_barra, oBeProducto.IdProducto) Then
    '                Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos.", oBeProducto.Codigo_barra))
    '            ElseIf clsLnProducto_codigos_barra.ExisteCodigoBarra(oBeProducto.Codigo_barra) Then
    '                Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos Código de Barra.", oBeProducto.Codigo_barra))
    '            ElseIf clsLnProducto_presentacion.Existe_Presentacion_By_Codigo_Barra(oBeProducto.Codigo_barra) Then
    '                Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos Presentación.", oBeProducto.Codigo_barra))
    '            End If

    '        End If

    '        Ins.Init("producto")
    '        Ins.Add("idproducto", "@idproducto", DataType.Parametro)
    '        Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
    '        Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
    '        Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
    '        Ins.Add("idmarca", "@idmarca", DataType.Parametro)
    '        Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
    '        Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
    '        Ins.Add("idcamara", "@idcamara", DataType.Parametro)
    '        Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
    '        Ins.Add("idperfilserializado", "@idperfilserializado", DataType.Parametro)
    '        Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
    '        Ins.Add("idsimbologia", "@idsimbologia", DataType.Parametro)
    '        Ins.Add("idarancel", "@idarancel", DataType.Parametro)
    '        Ins.Add("codigo", "@codigo", DataType.Parametro)
    '        Ins.Add("nombre", "@nombre", DataType.Parametro)
    '        Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
    '        Ins.Add("precio", "@precio", DataType.Parametro)
    '        Ins.Add("existencia_min", "@existencia_min", DataType.Parametro)
    '        Ins.Add("existencia_max", "@existencia_max", DataType.Parametro)
    '        Ins.Add("costo", "@costo", DataType.Parametro)
    '        Ins.Add("peso_referencia", "@peso_referencia", DataType.Parametro)
    '        Ins.Add("peso_tolerancia", "@peso_tolerancia", DataType.Parametro)
    '        Ins.Add("temperatura_referencia", "@temperatura_referencia", DataType.Parametro)
    '        Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", DataType.Parametro)
    '        Ins.Add("activo", "@activo", DataType.Parametro)
    '        Ins.Add("serializado", "@serializado", DataType.Parametro)
    '        Ins.Add("genera_lote", "@genera_lote", DataType.Parametro)
    '        Ins.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
    '        Ins.Add("control_lote", "@control_lote", DataType.Parametro)
    '        Ins.Add("peso_recepcion", "@peso_recepcion", DataType.Parametro)
    '        Ins.Add("peso_despacho", "@peso_despacho", DataType.Parametro)
    '        Ins.Add("temperatura_recepcion", "@temperatura_recepcion", DataType.Parametro)
    '        Ins.Add("temperatura_despacho", "@temperatura_despacho", DataType.Parametro)
    '        Ins.Add("materia_prima", "@materia_prima", DataType.Parametro)
    '        Ins.Add("kit", "@kit", DataType.Parametro)
    '        Ins.Add("tolerancia", "@tolerancia", DataType.Parametro)
    '        Ins.Add("ciclo_vida", "@ciclo_vida", DataType.Parametro)
    '        Ins.Add("user_agr", "@user_agr", DataType.Parametro)
    '        Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
    '        Ins.Add("user_mod", "@user_mod", DataType.Parametro)
    '        Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
    '        Ins.Add("imagen", "@imagen", DataType.Parametro)
    '        Ins.Add("noserie", "@noserie", DataType.Parametro)
    '        Ins.Add("noparte", "@noparte", DataType.Parametro)
    '        Ins.Add("fechamanufactura", "@fechamanufactura", DataType.Parametro)
    '        Ins.Add("capturar_aniada", "@capturar_aniada", DataType.Parametro)
    '        Ins.Add("control_peso", "@control_peso", DataType.Parametro)

    '        Dim sp As String = Ins.SQL()

    '        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

    '        cmd.CommandType = CommandType.Text

    '        If Es_Transaccion_Remota Then
    '            cmd = New SqlCommand(sp, pConection, pTransaction)
    '        Else
    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
    '            cmd = New SqlCommand(sp, lConnection, lTransaction)
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
    '        cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto.Propietario.IdPropietario))
    '        cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", IIf(oBeProducto.Clasificacion.IdClasificacion = Nothing, DBNull.Value, oBeProducto.Clasificacion.IdClasificacion)))
    '        cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", IIf(oBeProducto.Familia.IdFamilia = Nothing, DBNull.Value, oBeProducto.Familia.IdFamilia)))
    '        cmd.Parameters.Add(New SqlParameter("@IDMARCA", IIf(oBeProducto.Marca.IdMarca = Nothing, DBNull.Value, oBeProducto.Marca.IdMarca)))
    '        cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", IIf(oBeProducto.TipoProducto.IdTipoProducto = Nothing, DBNull.Value, oBeProducto.TipoProducto.IdTipoProducto)))
    '        cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeProducto.UnidadMedida.IdUnidadMedida = Nothing, DBNull.Value, oBeProducto.UnidadMedida.IdUnidadMedida)))
    '        cmd.Parameters.Add(New SqlParameter("@IDCAMARA", IIf(oBeProducto.IdCamara = Nothing, DBNull.Value, oBeProducto.IdCamara)))
    '        cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", IIf(oBeProducto.IdTipoRotacion = Nothing, DBNull.Value, oBeProducto.IdTipoRotacion)))
    '        cmd.Parameters.Add(New SqlParameter("@IDPERFILSERIALIZADO", IIf(oBeProducto.IdPerfilSerializado = Nothing, DBNull.Value, oBeProducto.IdPerfilSerializado)))
    '        cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", IIf(oBeProducto.IdIndiceRotacion = Nothing, DBNull.Value, oBeProducto.IdIndiceRotacion)))
    '        cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", IIf(oBeProducto.IdSimbologia = Nothing, DBNull.Value, oBeProducto.IdSimbologia)))
    '        cmd.Parameters.Add(New SqlParameter("@IDARANCEL", IIf(oBeProducto.Arancel.IdArancel = Nothing, DBNull.Value, oBeProducto.Arancel.IdArancel)))
    '        cmd.Parameters.Add(New SqlParameter("@CODIGO", IIf(oBeProducto.Codigo Is Nothing, DBNull.Value, oBeProducto.Codigo)))
    '        cmd.Parameters.Add(New SqlParameter("@NOMBRE", IIf(oBeProducto.Nombre Is Nothing, DBNull.Value, oBeProducto.Nombre)))
    '        cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", IIf(oBeProducto.Codigo_barra Is Nothing, DBNull.Value, oBeProducto.Codigo_barra)))
    '        cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MIN", IIf(oBeProducto.Existencia_min = Nothing, DBNull.Value, oBeProducto.Existencia_min)))
    '        cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MAX", IIf(oBeProducto.Existencia_max = Nothing, DBNull.Value, oBeProducto.Existencia_max)))
    '        cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto.Activo))
    '        cmd.Parameters.Add(New SqlParameter("@SERIALIZADO", oBeProducto.Serializado))
    '        cmd.Parameters.Add(New SqlParameter("@GENERA_LOTE", oBeProducto.Genera_lote))
    '        cmd.Parameters.Add(New SqlParameter("@COSTO", IIf(oBeProducto.Costo = Nothing, DBNull.Value, oBeProducto.Costo)))
    '        cmd.Parameters.Add(New SqlParameter("@PRECIO", IIf(oBeProducto.Precio = Nothing, DBNull.Value, oBeProducto.Precio)))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeProducto.Control_lote))

    '        If oBeProducto.Imagen IsNot Nothing Then
    '            cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeProducto.Imagen))
    '        Else
    '            cmd.Parameters.Add(New SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@PESO_RECEPCION", oBeProducto.Peso_recepcion))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHO", oBeProducto.Peso_despacho))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_REFERENCIA", IIf(oBeProducto.Peso_referencia = Nothing, DBNull.Value, oBeProducto.Peso_referencia)))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_TOLERANCIA", IIf(oBeProducto.Peso_tolerancia = Nothing, DBNull.Value, oBeProducto.Peso_tolerancia)))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_RECEPCION", oBeProducto.Temperatura_recepcion))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_DESPACHO", oBeProducto.Temperatura_despacho))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_REFERENCIA", IIf(oBeProducto.Temperatura_referencia = Nothing, DBNull.Value, oBeProducto.Temperatura_referencia)))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_TOLERANCIA", IIf(oBeProducto.Temperatura_tolerancia = Nothing, DBNull.Value, oBeProducto.Temperatura_tolerancia)))
    '        cmd.Parameters.Add(New SqlParameter("@MATERIA_PRIMA", IIf(oBeProducto.Materia_prima = Nothing, DBNull.Value, oBeProducto.Materia_prima)))
    '        cmd.Parameters.Add(New SqlParameter("@KIT", IIf(oBeProducto.Kit = Nothing, DBNull.Value, oBeProducto.Kit)))
    '        cmd.Parameters.Add(New SqlParameter("@TOLERANCIA", IIf(oBeProducto.Tolerancia = Nothing, DBNull.Value, oBeProducto.Tolerancia)))
    '        cmd.Parameters.Add(New SqlParameter("@CICLO_VIDA", IIf(oBeProducto.Ciclo_vida = Nothing, DBNull.Value, oBeProducto.Ciclo_vida)))
    '        cmd.Parameters.Add(New SqlParameter("@USER_AGR", IIf(oBeProducto.User_agr = String.Empty, DBNull.Value, oBeProducto.User_agr)))
    '        cmd.Parameters.Add(New SqlParameter("@FEC_AGR", IIf(oBeProducto.Fec_agr = Nothing, DBNull.Value, oBeProducto.Fec_agr)))
    '        cmd.Parameters.Add(New SqlParameter("@USER_MOD", IIf(oBeProducto.User_mod = String.Empty, DBNull.Value, oBeProducto.User_mod)))
    '        cmd.Parameters.Add(New SqlParameter("@FEC_MOD", IIf(oBeProducto.Fec_mod = Nothing, DBNull.Value, oBeProducto.Fec_mod)))
    '        cmd.Parameters.Add(New SqlParameter("@NOSERIE", IIf(oBeProducto.Noserie = String.Empty, DBNull.Value, oBeProducto.Noserie)))
    '        cmd.Parameters.Add(New SqlParameter("@NOPARTE", IIf(oBeProducto.Noparte = String.Empty, DBNull.Value, oBeProducto.Noparte)))
    '        cmd.Parameters.Add(New SqlParameter("@FECHAMANUFACTURA", oBeProducto.Fechamanufactura))
    '        cmd.Parameters.Add(New SqlParameter("@CAPTURAR_ANIADA", oBeProducto.Capturar_aniada))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))

    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        If Not Es_Transaccion_Remota Then
    '            lTransaction.Commit()
    '        End If

    '        Return rowsAffected

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '        If lConnection IsNot Nothing Then lConnection.Dispose()
    '        cmd.Dispose()
    '    End Try

    'End Function

    Public Shared Function Get_All_Parametro_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_parametros)

        Get_All_Parametro_By_IdProducto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_parametros)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = String.Format("SELECT * FROM VW_ProductoParametro WHERE IdProducto={0}", pIdProducto)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_parametros

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_parametros()

                                clsLnProducto_parametros.Cargar(Obj, lRow)

                                If lRow("descripcion") IsNot DBNull.Value AndAlso lRow("descripcion") IsNot Nothing Then

                                    Obj.TipoParametro = New clsBeP_parametro()
                                    Obj.TipoParametro.Descripcion = CType(lRow("descripcion"), String)
                                    Obj.TipoParametro.Tipo = CType(lRow("tipo"), String)

                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                        lDataTable.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Conversion_Presentacion_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_presentaciones_conversiones)

        Dim lReturnList As New List(Of clsBeProducto_presentaciones_conversiones)

        Try

            Dim vSQL = String.Format(" SELECT producto_presentaciones_conversiones.*, producto_presentacion.IdProducto, producto_presentacion.nombre AS NomPresOrigen, 
                         producto_presentacion_1.nombre AS NomPresDestino 
                         FROM producto_presentaciones_conversiones INNER JOIN 
                         producto_presentacion ON producto_presentaciones_conversiones.IdPresentacionOrigen = producto_presentacion.IdPresentacion INNER JOIN 
                         producto_presentacion AS producto_presentacion_1 ON  
                         producto_presentaciones_conversiones.IdPresentacionDestino = producto_presentacion_1.IdPresentacion 
                         WHERE producto_presentacion.IdProducto={0} ", pIdProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_presentaciones_conversiones

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_presentaciones_conversiones

                                Obj.IdConversion = CType(lRow("IdConversion"), Integer)

                                If lRow("IdConversion") IsNot DBNull.Value AndAlso lRow("IdConversion") IsNot Nothing Then
                                    Obj.IdConversion = CType(lRow("IdConversion"), Integer)
                                End If

                                Obj.ProductoPresentacionOrigen = New clsBeProducto_Presentacion

                                If lRow("IdPresentacionOrigen") IsNot DBNull.Value AndAlso lRow("IdPresentacionOrigen") IsNot Nothing Then
                                    Obj.IdPresentacionOrigen = CType(lRow("IdPresentacionOrigen"), Integer)
                                    Obj.ProductoPresentacionOrigen.IdPresentacion = Obj.IdPresentacionOrigen
                                    Obj.ProductoPresentacionOrigen = clsLnProducto_presentacion.GetSingle(Obj.ProductoPresentacionOrigen.IdPresentacion, lConnection, lTransaction)
                                End If

                                Obj.ProductoPresentacionDestino = New clsBeProducto_Presentacion

                                If lRow("IdPresentacionDestino") IsNot DBNull.Value AndAlso lRow("IdPresentacionDestino") IsNot Nothing Then
                                    Obj.IdPresentacionDestino = CType(lRow("IdPresentacionDestino"), Integer)
                                    Obj.ProductoPresentacionDestino.IdPresentacion = Obj.IdPresentacionDestino
                                    Obj.ProductoPresentacionDestino = clsLnProducto_presentacion.GetSingle(Obj.ProductoPresentacionDestino.IdPresentacion, lConnection, lTransaction)
                                End If

                                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                    Obj.Factor = CType(lRow("Factor"), Integer)
                                End If
                                If lRow("Inverso") IsNot DBNull.Value AndAlso lRow("Inverso") IsNot Nothing Then
                                    Obj.Inverso = CType(lRow("Inverso"), Integer)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Codigos_Barra_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_codigos_barra)

        Get_All_Codigos_Barra_By_IdProducto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_codigos_barra)

            Dim vSQL As String = String.Format("SELECT pcb.IdProductoCodigoBarra,pcb.IdProducto,pcb.IdProveedor,
                                                pr.nombre AS Producto,'' AS Proveedor,pcb.codigo_barra,pcb.activo,pcb.user_agr,
                                                pcb.fec_agr,pcb.user_mod,pcb.fec_mod FROM producto_codigos_barra AS pcb 
                                                INNER JOIN  producto AS pr ON pcb.IdProducto = pr.IdProducto 
                                                WHERE pcb.IdProducto={0}", pIdProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_codigos_barra

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_codigos_barra()

                                clsLnProducto_codigos_barra.Cargar(Obj, lRow)

                                If lRow("IdProveedor") IsNot DBNull.Value AndAlso lRow("IdProveedor") IsNot Nothing Then
                                    Obj.Proveedor = New clsBeProveedor
                                    Obj.Proveedor.Nombre = CType(lRow("Proveedor"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                        lDataTable.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Presentacion_By_IdProducto(ByVal pIdProducto As Integer, ByVal pIdBodega As Integer) As List(Of clsBeProducto_Presentacion)

        Dim lReturnList As New List(Of clsBeProducto_Presentacion)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoPresentacion WHERE IdProducto=@IdProducto AND IdBodega = @IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Presentacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Presentacion
                                clsLnProducto_presentacion.Cargar(Obj, lRow)
                                Obj.ExisteStock = clsLnProducto_presentacion.Existe_Stock(Obj.IdProducto, Obj.IdPresentacion, lConnection, lTransaction)
                                lReturnList.Add(Obj)

                            Next

                        End If

                        lDataTable.Dispose()

                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Producto_Sustituto_By_IdProductoOriginal(ByVal pIdProductoOriginal As Integer) As List(Of clsBeProducto_sustituto)

        Dim lReturnList As New List(Of clsBeProducto_sustituto)

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSustituto WHERE IdProductoOriginal=@IdProductoOriginal"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoOriginal", pIdProductoOriginal)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_sustituto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_sustituto

                                Obj.IdProductoSustituto = CType(lRow("Código"), Integer)

                                If lRow("IdProductoOriginal") IsNot DBNull.Value AndAlso lRow("IdProductoOriginal") IsNot Nothing Then
                                    Obj.IdProductoOriginal = CType(lRow("IdProductoOriginal"), Integer)
                                End If

                                If lRow("IdProductoPresentacionOriginal") IsNot DBNull.Value AndAlso lRow("IdProductoPresentacionOriginal") IsNot Nothing Then
                                    Obj.IdProductoPresentacionOriginal = CType(lRow("IdProductoPresentacionOriginal"), Integer)
                                    Obj.ProductoPresentacionOriginal = New clsBeProducto_Presentacion
                                    Obj.ProductoPresentacionOriginal.Nombre = CType(lRow("Presentación Original"), String)
                                End If

                                If lRow("IdProductoReemplazo") IsNot DBNull.Value AndAlso lRow("IdProductoReemplazo") IsNot Nothing Then
                                    Obj.IdProductoReemplazo = CType(lRow("IdProductoReemplazo"), Integer)
                                    Obj.ProductoReemplazo = New clsBeProducto
                                    Obj.ProductoReemplazo.Nombre = CType(lRow("Producto Reemplazo"), String)
                                End If

                                If lRow("IdProductoPresentacionReemplazo") IsNot DBNull.Value AndAlso lRow("IdProductoPresentacionReemplazo") IsNot Nothing Then
                                    Obj.IdProductoPresentacionReemplazo = CType(lRow("IdProductoPresentacionReemplazo"), Integer)
                                    Obj.ProductoPresentacionReemplazo = New clsBeProducto_Presentacion
                                    Obj.ProductoPresentacionReemplazo.Nombre = CType(lRow("Presentación Reemplazo"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function GetAllProductoRellenado() As List(Of clsBeProducto_rellenado)

    '    Dim lReturnList As New List(Of clsBeProducto_rellenado)

    '    Try

    '        
    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            Const lSQL As String = "SELECT * FROM VW_ProductoRellenado"

    '            
    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text

    '                Dim lDataTable As New DataTable
    '                lDTA.Fill(lDataTable)

    '                Dim Obj As clsBeProducto_rellenado

    '                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

    '                    For Each lRow As DataRow In lDataTable.Rows

    '                        Obj = New clsBeProducto_rellenado

    '                        Obj.IdRellenado = CType(lRow("IdRellenado"), Int32)

    '                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
    '                            Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
    '                            Obj.Presentacion = CType(lRow("Presentación"), String)
    '                        End If

    '                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
    '                            Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
    '                            Obj.Estado = CType(lRow("Estado"), String)
    '                        End If

    '                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
    '                            Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
    '                            Obj.Ubicacion = CType(lRow("Ubicación"), String)
    '                        End If

    '                        If lRow("IdTipoAccion") IsNot DBNull.Value AndAlso lRow("IdTipoAccion") IsNot Nothing Then
    '                            Obj.IdTipoAccion = CType(lRow("IdTipoAccion"), Int32)
    '                        End If

    '                        If lRow("Minimo") IsNot DBNull.Value AndAlso lRow("Minimo") IsNot Nothing Then
    '                            Obj.Minimo = CType(lRow("Minimo"), Double)
    '                        End If

    '                        If lRow("Maximo") IsNot DBNull.Value AndAlso lRow("Maximo") IsNot Nothing Then
    '                            Obj.Maximo = CType(lRow("Maximo"), Double)
    '                        End If

    '                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
    '                            Obj.User_agr = CType(lRow("user_agr"), String)
    '                        End If

    '                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
    '                            Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
    '                        End If

    '                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
    '                            Obj.User_mod = CType(lRow("user_mod"), String)
    '                        End If

    '                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
    '                            Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
    '                        End If

    '                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
    '                            Obj.Activo = CType(lRow("activo"), Boolean)
    '                        End If

    '                        Obj.IsNew = False

    '                        lReturnList.Add(Obj)

    '                    Next

    '                End If

    '            End Using

    '        End Using

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Public Shared Function Get_All_Presentacion_Tarima() As List(Of clsBeProducto_presentacion_tarima)

    '    Dim lReturnList As New List(Of clsBeProducto_presentacion_tarima)

    '    Try

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            Dim vSQL As String = "SELECT * FROM VW_Presentacion_Tarima"

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text

    '                Dim lDataTable As New DataTable
    '                lDTA.Fill(lDataTable)

    '                Dim Obj As clsBeProducto_presentacion_tarima

    '                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

    '                    For Each lRow As DataRow In lDataTable.Rows

    '                        Obj = New clsBeProducto_presentacion_tarima

    '                        Obj.IdPresentacionTarima = CType(lRow("IdPresentacionTarima"), Integer)

    '                        If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
    '                            Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
    '                            Obj.Presentacion = CType(lRow("nombre"), String)
    '                        End If

    '                        If lRow("TipoTarima") IsNot DBNull.Value AndAlso lRow("TipoTarima") IsNot Nothing Then
    '                            Obj.TipoTarima = CType(lRow("TipoTarima"), String)
    '                        End If

    '                        If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
    '                            Obj.Cantidad = CType(lRow("Cantidad"), String)
    '                        End If

    '                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
    '                            Obj.Activo = CType(lRow("activo"), Boolean)
    '                        End If

    '                        If lRow("IdTipoTarima") IsNot DBNull.Value AndAlso lRow("IdTipoTarima") IsNot Nothing Then
    '                            Obj.IdTipoTarima = CType(lRow("IdTipoTarima"), Integer)
    '                        End If

    '                        Obj.IsNew = False

    '                        lReturnList.Add(Obj)

    '                    Next

    '                End If

    '            End Using

    '        End Using

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_All_Bodegas_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Get_All_Bodegas_By_IdProducto = Nothing

        Try

            Dim vSQL As String = String.Format("SELECT * FROM producto_bodega WHERE IdProducto={0}", pIdProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_bodega

                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If
                                If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                    Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                                End If
                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If
                                If lRow("sistema") IsNot DBNull.Value AndAlso lRow("sistema") IsNot Nothing Then
                                    Obj.Sistema = CType(lRow("sistema"), Boolean)
                                End If
                                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                    Obj.User_agr = CType(lRow("user_agr"), String)
                                End If
                                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                    Obj.Fec_agr = CType(lRow("fec_agr"), Date)
                                End If
                                If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                    Obj.User_mod = CType(lRow("user_mod"), String)
                                End If
                                If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                    Obj.Fec_mod = CType(lRow("fec_mod"), Date)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_All_Bodegas_By_IdProducto = lReturnList

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

    Public Shared Function Existe_No_Parte_By_IdPropietario(ByVal pConnection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction,
                                                            ByVal pIdPropietario As Integer,
                                                            ByVal pNoParte As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(IdProducto) FROM producto WHERE IdPropietario=@IdPropietario AND NoParte=@NoParte"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lCommand.Parameters.AddWithValue("@NoParte", pNoParte)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_No_Serie_By_IdPropietario(ByVal pConnection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction,
                                                            ByVal pIdPropietario As Integer,
                                                            ByVal pNoSerie As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(IdProducto) FROM producto WHERE IdPropietario=@IdPropietario AND NoSerie=@NoSerie"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lCommand.Parameters.AddWithValue("@NoSerie", pNoSerie)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pListObjP"></param>
    ''' <remarks></remarks>
    Public Shared Sub Guardar_Transaccion(ByRef pListObjP As List(Of clsBeProducto),
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try

            For Each Obj As clsBeProducto In pListObjP
                Insertar(Obj, lConnection, lTransaction)
            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjP"></param>
    ''' <remarks></remarks>
    Public Shared Sub Desactivar(ByVal pObjP As clsBeProducto)

        Dim listPB As List(Of clsBeProducto_bodega) = clsLnProducto_bodega.Get_All_By_IdProducto(pObjP.IdProducto)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            pObjP.Activo = False

            Actualizar(pObjP, lConnection, lTransaction)

            For Each Obj As clsBeProducto_bodega In listPB
                Obj.Activo = False
                clsLnProducto_bodega.Actualizar(Obj, lConnection, lTransaction)
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pObjP"></param>
    ''' <param name="pListObjPR"></param>
    ''' <param name="pListObjCB"></param>
    ''' <param name="pListObjP"></param>
    ''' <param name="pListObjPS"></param>
    ''' <param name="pListObjPRE"></param>
    ''' <param name="pListObjPPT"></param>
    ''' <param name="pListObjCN"></param>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub Guardar(ByVal pObjP As clsBeProducto,
                              ByVal pListObjPR As List(Of clsBeProducto_parametros),
                              ByVal pListObjCB As List(Of clsBeProducto_codigos_barra),
                              ByVal pListObjP As List(Of clsBeProducto_Presentacion),
                              ByVal pListObjPS As List(Of clsBeProducto_sustituto),
                              ByVal pListObjPRE As List(Of clsBeProducto_rellenado),
                              ByVal pListObjPPT As List(Of clsBeProducto_presentacion_tarima),
                              ByVal pListObjPB As List(Of clsBeProducto_bodega),
                              ByVal pListObjCN As List(Of clsBeProducto_presentaciones_conversiones),
                              ByVal pListObjPK As List(Of clsBeProducto_kit_composicion),
                              ByVal pListProdImg As List(Of clsBeProducto_imagen))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pObjP.IsNew Then

                If String.IsNullOrEmpty(pObjP.Codigo_barra) = False Then
                    If Existe_Codigo_Barra(pObjP.Codigo_barra, pObjP.IdProducto) Then
                        Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos.", pObjP.Codigo_barra))
                    ElseIf clsLnProducto_codigos_barra.ExisteCodigoBarra(pObjP.Codigo_barra) Then
                        Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos Código de Barra.", pObjP.Codigo_barra))
                    ElseIf clsLnProducto_presentacion.Existe_Presentacion_By_Codigo_Barra(pObjP.Codigo_barra) Then
                        Throw New Exception(String.Format("El Código de Barra {0} ya existe en Productos Presentación.", pObjP.Codigo_barra))
                    End If
                End If

                pObjP.IdProducto = MaxID(lConnection, lTransaction) + 1
                Insertar(pObjP, lConnection, lTransaction)
            Else
                Actualizar(pObjP, lConnection, lTransaction)
            End If

            Dim lMaxID As Integer = clsLnProducto_parametros.MaxID(lConnection, lTransaction)
            For Each ObjPR As clsBeProducto_parametros In pListObjPR
                If ObjPR.IsNew Then
                    lMaxID += 1
                    ObjPR.IdProductoParametro = lMaxID
                    ObjPR.IdProducto = pObjP.IdProducto
                    clsLnProducto_parametros.Insertar(ObjPR, lConnection, lTransaction)
                Else
                    clsLnProducto_parametros.Actualizar(ObjPR, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_codigos_barra.MaxID(lConnection, lTransaction)

            For Each ObjCB As clsBeProducto_codigos_barra In pListObjCB
                If ObjCB.IsNew Then
                    ' hacer validaciones si el codigo de barra no existe en el producto, producto presentacion y aca mismo
                    lMaxID += 1
                    ObjCB.IdProductoCodigoBarra = lMaxID
                    ObjCB.IdProducto = pObjP.IdProducto
                    clsLnProducto_codigos_barra.Insertar(ObjCB, lConnection, lTransaction)
                Else
                    clsLnProducto_codigos_barra.Actualizar(ObjCB, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_presentacion.MaxID(lConnection, lTransaction)

            For Each ObjPP As clsBeProducto_Presentacion In pListObjP
                If ObjPP.IsNew Then
                    ' hacer validaciones si el codigo de barra no existe en el producto, producto presentacion y aca mismo
                    lMaxID += 1
                    ObjPP.IdPresentacion = lMaxID
                    ObjPP.IdProducto = pObjP.IdProducto
                    clsLnProducto_presentacion.Insertar(ObjPP, lConnection, lTransaction)
                Else
                    clsLnProducto_presentacion.Actualizar(ObjPP, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_sustituto.MaxID(lConnection, lTransaction)

            For Each ObjPS As clsBeProducto_sustituto In pListObjPS
                If ObjPS.IsNew Then
                    lMaxID += 1
                    ObjPS.IdProductoSustituto = lMaxID
                    ObjPS.IdProductoOriginal = pObjP.IdProducto
                    clsLnProducto_sustituto.Insertar(ObjPS, lConnection, lTransaction)
                Else
                    clsLnProducto_sustituto.Actualizar(ObjPS, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_rellenado.MaxID(lConnection, lTransaction)

            For Each ObjPRE As clsBeProducto_rellenado In pListObjPRE
                If ObjPRE.IsNew Then
                    lMaxID += 1
                    ObjPRE.IdRellenado = lMaxID
                    clsLnProducto_rellenado.Insertar(ObjPRE, lConnection, lTransaction)
                Else
                    clsLnProducto_rellenado.Actualizar(ObjPRE, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_presentacion_tarima.MaxID(lConnection, lTransaction)

            For Each ObjPPT As clsBeProducto_presentacion_tarima In pListObjPPT
                If ObjPPT.IsNew Then
                    lMaxID += 1
                    ObjPPT.IdPresentacionTarima = lMaxID
                    clsLnProducto_presentacion_tarima.Insertar(ObjPPT, lConnection, lTransaction)
                Else
                    clsLnProducto_presentacion_tarima.Actualizar(ObjPPT, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_bodega.MaxID(lConnection, lTransaction)

            For Each Obj As clsBeProducto_bodega In pListObjPB
                If Obj.IdProductoBodega = 0 Then
                    lMaxID += 1
                    Obj.IdProductoBodega = lMaxID
                    Obj.IdProducto = pObjP.IdProducto '#CKFK 20180926 12:25 PM Agregué esto porque no se estaba llenando este IdProducto y daba error
                    clsLnProducto_bodega.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnProducto_bodega.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lMaxID = 0
            lMaxID = clsLnProducto_presentaciones_conversiones.MaxID(lConnection, lTransaction)

            For Each Objj As clsBeProducto_presentaciones_conversiones In pListObjCN
                If Objj.IsNew Then
                    lMaxID += 1
                    Objj.IdConversion = lMaxID
                    clsLnProducto_presentaciones_conversiones.Insertar(Objj, lConnection, lTransaction)
                Else
                    clsLnProducto_presentaciones_conversiones.Actualizar(Objj, lConnection, lTransaction)
                End If
            Next

            If Not pListObjPK Is Nothing And pListObjPK.Count > 0 Then
                For Each ObjPk As clsBeProducto_kit_composicion In pListObjPK
                    If ObjPk.IsNew Then
                        ObjPk.IdProductoKitComposicion = clsLnProducto_kit_composicion.MaxID(lConnection, lTransaction) + 1
                        clsLnProducto_kit_composicion.Insertar(ObjPk, lConnection, lTransaction)
                    Else
                        clsLnProducto_kit_composicion.Actualizar(ObjPk, lConnection, lTransaction)
                    End If
                Next
            End If


            lMaxID = 0
            lMaxID = clsLnProducto_imagen.MaxID(lConnection, lTransaction)

            For Each Objj As clsBeProducto_imagen In pListProdImg
                If Objj.IsNew Then
                    lMaxID += 1
                    Objj.IdProductoImagen = lMaxID
                    clsLnProducto_imagen.Insertar(Objj, lConnection, lTransaction)
                Else
                    clsLnProducto_imagen.Actualizar(Objj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_All_By_Bodega_For_Seleccion_DT(ByVal pIdBodega As String,
                                                              ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_All_By_Bodega_For_Seleccion_DT = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT cast(0 as bit)as 'Seleccionar', 
                         IdProducto, Codigo, Nombre, Codigo_Barra,Familia,Clasificacion,Tipo, cast(0 as int) as IdReglaUbicPrioProd
                         FROM VW_Productos_Seleccion
                         WHERE IdBodega = @IdBodega 
                         AND Activo=1 
                         AND IdPropietarioBodega = @IdPropietarioBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_Bodega_For_Seleccion_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Bodega_For_Seleccion(ByVal pIdBodega As String,
                                                           ByVal pIdPropietarioBodega As Integer) As List(Of clsBeProducto_SelectionList)

        Dim lReturnList As New List(Of clsBeProducto_SelectionList)

        Get_All_By_Bodega_For_Seleccion = Nothing

        Try

            Dim vSQL As String = "SELECT producto.*, producto_bodega.IdBodega, propietario_bodega.IdPropietarioBodega 
                         FROM producto INNER JOIN 
                         producto_bodega ON producto.IdProducto = producto_bodega.IdProducto INNER JOIN 
                         propietario_bodega ON producto_bodega.IdBodega = propietario_bodega.IdBodega AND 
                         producto.IdPropietario = propietario_bodega.IdPropietario 
                         WHERE producto_bodega.IdBodega = @IdBodega 
                         AND producto.Activo=1 AND producto_bodega.Activo =1 
                         AND propietario_bodega.IdPropietarioBodega = @IdPropietarioBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_SelectionList

                        Dim pCamposCargar(4) As clsBeProducto.ProdPropiedades

                        pCamposCargar(0) = clsBeProducto.ProdPropiedades.IdProducto
                        pCamposCargar(1) = clsBeProducto.ProdPropiedades.Codigo
                        pCamposCargar(2) = clsBeProducto.ProdPropiedades.Nombre
                        pCamposCargar(3) = clsBeProducto.ProdPropiedades.Codigos_Barra
                        pCamposCargar(4) = clsBeProducto.ProdPropiedades.Propietario

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_SelectionList
                                Obj.Seleccionar = False
                                Cargar(Obj, lRow, pCamposCargar, lConnection, lTransaction)
                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_Bodega_For_Seleccion = lReturnList

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

    Public Shared Function Get_Codigos_Sugeridos(ByVal pIdPropietario As Integer,
                                                 ByVal pIdBodega As String,
                                                 ByVal PresentacionEsPallet As Boolean) As List(Of String)

        Dim lReturnList As New List(Of String)
        Dim vSQL As String = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                If PresentacionEsPallet Then

                    vSQL = "SELECT Codigo FROM 
                                  (SELECT p.codigo As Codigo, p.idproducto as IdProducto  
                                  FROM producto AS p inner join producto_bodega pb
                                  on p.idproducto = pb.Idproducto
                                  WHERE p.IdPropietario = @IdPropietario
                                  And pb.IdBodega =@IdBodega
                                  And pb.Activo = 1
                                  UNION 
                                  SELECT pcb.codigo_barra, pcb.Idproducto FROM producto AS pcb
                                  inner join producto_bodega pb
                                  on pcb.idproducto = pb.Idproducto
                                  WHERE pcb.IdPropietario = @IdPropietario
                                  And pb.IdBodega =@IdBodega
                                  And pcb.Activo = 1
                                  UNION
                                  SELECT pcbb.codigo_barra, pcbb.idproducto from producto_codigos_barra pcbb
                                  inner join producto_bodega pb on pcbb.idproducto = pb.Idproducto
                                  inner join producto prod on pcbb.IdProducto = prod.IdProducto
                                  WHERE prod.IdPropietario = @IdPropietario
                                  And pb.IdBodega =@IdBodega
                                  And pcbb.Activo = 1
                                  UNION SELECT pp.codigo_barra, pp.IdProducto from producto_presentacion pp
                                  inner join producto_bodega pb
                                  on pp.idproducto = pb.Idproducto  inner join producto prod
                                  on pp.IdProducto = prod.IdProducto
                                  And pp.IdProducto = prod.IdProducto
                                  WHERE prod.IdPropietario = @IdPropietario And pb.IdBodega =@IdBodega
                                  And pp.Activo = 1 ) AS T 
                     WHERE T.IdProducto IN (
                                     SELECT pp.idproducto from producto_presentacion pp   
                                     inner join producto_bodega pb  on pp.idproducto = pb.Idproducto  
                                     inner join producto prod  on pp.IdProducto = prod.IdProducto   
                                     And pp.IdProducto = prod.IdProducto 
                                     And pp.EsPallet = 1
                                     WHERE prod.IdPropietario = @IdPropietario 
                                     And pb.IdBodega =@IdBodega  And pp.Activo = 1)"

                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim vCodigo As String = ""

                        For Each lRow As DataRow In lDataTable.Rows

                            vCodigo = IIf(IsDBNull(lRow.Item("Codigo")), "", lRow.Item("Codigo"))
                            If vCodigo <> "" Then lReturnList.Add(vCodigo)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                               ByVal pIdBodega As String) As List(Of String)

        Dim lReturnList As New List(Of String)

        Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega = Nothing

        Try

            '#EJC20211216: Mostrar solo los códigos que tienen existencia.
            Dim vSQL As String = "SELECT p.codigo  
                                  FROM producto AS p inner join producto_bodega pb  
                                  on p.idproducto = pb.Idproducto  
                                  WHERE p.IdPropietario = @IdPropietario 
                                  And pb.IdBodega =@IdBodega 
                                  And pb.Activo = 1 
                                  And p.codigo IN (select codigo FROM VW_Stock_Res WHERE IdBodega = @IdBodega AND IdPropietario = @IdPropietario)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vCodigo As String = ""

                            For Each lRow As DataRow In lDataTable.Rows

                                vCodigo = IIf(IsDBNull(lRow.Item("Codigo")), "", lRow.Item("Codigo"))
                                If vCodigo <> "" Then lReturnList.Add(vCodigo)

                            Next

                            Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega = lReturnList

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

    Public Shared Function Get_Codigos_Sugeridos_By_IdProducto_And_IdPropietario(ByVal pIdProducto As Integer,
                                                                                 ByVal pIdPropietario As Integer,
                                                                                 ByVal pIdBodega As String) As List(Of String)

        Get_Codigos_Sugeridos_By_IdProducto_And_IdPropietario = Nothing

        Try

            Dim lReturnList As New List(Of String)

            Dim vSQL As String = "SELECT p.codigo  " &
                     " FROM producto AS p inner join producto_bodega pb   " &
                     " on p.idproducto = pb.Idproducto  " &
                     " WHERE p.IdPropietario = @IdPropietario " &
                     " And pb.IdBodega =@IdBodega " &
                     " And p.IdProducto=@IdProducto " &
                     " And pb.Activo = 1 " &
                     " UNION " &
                     " SELECT pcb.codigo_barra FROM producto AS pcb   " &
                     " inner join producto_bodega pb    " &
                     " on pcb.idproducto = pb.Idproducto  " &
                     " WHERE pcb.IdPropietario = @IdPropietario " &
                     " And pb.IdBodega =@IdBodega " &
                     " And pcb.IdProducto=@IdProducto " &
                     " And pcb.Activo = 1 " &
                     " UNION " &
                     " SELECT pcbb.codigo_barra from producto_codigos_barra pcbb  " &
                     " inner join producto_bodega pb on pcbb.idproducto = pb.Idproducto  " &
                     " inner join producto prod on pcbb.IdProducto = prod.IdProducto  " &
                     " WHERE prod.IdPropietario = @IdPropietario " &
                     " And pb.IdBodega =@IdBodega " &
                     " And prod.IdProducto=@IdProducto " &
                     " And pcbb.Activo = 1 " &
                     " UNION SELECT pp.codigo_barra from producto_presentacion pp  " &
                     " inner join producto_bodega pb " &
                     " on pp.idproducto = pb.Idproducto  inner join producto prod " &
                     " on pp.IdProducto = prod.IdProducto  " &
                     " And pp.IdProducto = prod.IdProducto  " &
                     " And pp.IdProducto=@IdProducto " &
                     " WHERE prod.IdPropietario = @IdPropietario And pb.IdBodega =@IdBodega " &
                     " And pp.Activo = 1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vCodigo As String = ""

                            For Each lRow As DataRow In lDataTable.Rows

                                vCodigo = IIf(IsDBNull(lRow.Item("Codigo")), "", lRow.Item("Codigo"))
                                If vCodigo <> "" Then lReturnList.Add(vCodigo)

                            Next

                            Get_Codigos_Sugeridos_By_IdProducto_And_IdPropietario = lReturnList

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

    Public Shared Function InsertarFromInterface(ByRef oBeProducto As clsBeProducto) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Ins.Init("producto")
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            If Not oBeProducto.IdPropietario = 0 Then Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            If Not oBeProducto.IdClasificacion = 0 Then Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            If Not oBeProducto.IdFamilia = 0 Then Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            If Not oBeProducto.IdMarca = 0 Then Ins.Add("idmarca", "@idmarca", DataType.Parametro)
            If Not oBeProducto.IdTipoProducto = 0 Then Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            If Not oBeProducto.IdCamara = 0 Then Ins.Add("idcamara", "@idcamara", DataType.Parametro)
            If Not oBeProducto.IdTipoRotacion = 0 Then Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            If Not oBeProducto.IdPerfilSerializado = 0 Then Ins.Add("idperfilserializado", "@idperfilserializado", DataType.Parametro)
            If Not oBeProducto.IdIndiceRotacion = 0 Then Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            If Not oBeProducto.IdSimbologia = 0 Then Ins.Add("idsimbologia", "@idsimbologia", DataType.Parametro)
            If Not oBeProducto.IdArancel = 0 Then Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("existencia_min", "@existencia_min", DataType.Parametro)
            Ins.Add("existencia_max", "@existencia_max", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("peso_referencia", "@peso_referencia", DataType.Parametro)
            Ins.Add("peso_tolerancia", "@peso_tolerancia", DataType.Parametro)
            Ins.Add("temperatura_referencia", "@temperatura_referencia", DataType.Parametro)
            Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("serializado", "@serializado", DataType.Parametro)
            Ins.Add("genera_lote", "@genera_lote", DataType.Parametro)
            Ins.Add("genera_lp_old", "@genera_lp_old", DataType.Parametro)
            Ins.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Ins.Add("control_lote", "@control_lote", DataType.Parametro)
            Ins.Add("peso_recepcion", "@peso_recepcion", DataType.Parametro)
            Ins.Add("peso_despacho", "@peso_despacho", DataType.Parametro)
            Ins.Add("temperatura_recepcion", "@temperatura_recepcion", DataType.Parametro)
            Ins.Add("temperatura_despacho", "@temperatura_despacho", DataType.Parametro)
            Ins.Add("materia_prima", "@materia_prima", DataType.Parametro)
            Ins.Add("kit", "@kit", DataType.Parametro)
            Ins.Add("tolerancia", "@tolerancia", DataType.Parametro)
            Ins.Add("ciclo_vida", "@ciclo_vida", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If Not oBeProducto.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("noserie", "@noserie", DataType.Parametro)
            Ins.Add("noparte", "@noparte", DataType.Parametro)
            Ins.Add("fechamanufactura", "@fechamanufactura", DataType.Parametro)
            Ins.Add("capturar_aniada", "@capturar_aniada", DataType.Parametro)
            Ins.Add("control_peso", "@control_peso", DataType.Parametro)
            Ins.Add("captura_arancel", "@captura_arancel", DataType.Parametro)
            Ins.Add("es_hardware", "@es_hardware", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            If Not oBeProducto.IdUnidadMedidaCobro = 0 Then Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            If Not oBeProducto.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto.IdPropietario))
            If Not oBeProducto.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto.IdClasificacion))
            If Not oBeProducto.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto.IdFamilia))
            If Not oBeProducto.IdMarca = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto.IdMarca))
            If Not oBeProducto.IdTipoProducto = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto.IdTipoProducto))
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto.IdUnidadMedidaBasica))
            If Not oBeProducto.IdCamara = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCAMARA", oBeProducto.IdCamara))
            If Not oBeProducto.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeProducto.IdTipoRotacion))
            If Not oBeProducto.IdPerfilSerializado = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPERFILSERIALIZADO", oBeProducto.IdPerfilSerializado))
            If Not oBeProducto.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeProducto.IdIndiceRotacion))
            If Not oBeProducto.IdSimbologia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", oBeProducto.IdSimbologia))
            If Not oBeProducto.IdArancel = 0 Then cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeProducto.IdArancel))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto.Precio))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MIN", oBeProducto.Existencia_min))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MAX", oBeProducto.Existencia_max))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto.Costo))
            cmd.Parameters.Add(New SqlParameter("@PESO_REFERENCIA", oBeProducto.Peso_referencia))
            cmd.Parameters.Add(New SqlParameter("@PESO_TOLERANCIA", oBeProducto.Peso_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_REFERENCIA", oBeProducto.Temperatura_referencia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_TOLERANCIA", oBeProducto.Temperatura_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto.Activo))
            cmd.Parameters.Add(New SqlParameter("@SERIALIZADO", oBeProducto.Serializado))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LOTE", oBeProducto.Genera_lote))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_OLD", oBeProducto.Genera_lp))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeProducto.Control_lote))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECEPCION", oBeProducto.Peso_recepcion))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHO", oBeProducto.Peso_despacho))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_RECEPCION", oBeProducto.Temperatura_recepcion))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_DESPACHO", oBeProducto.Temperatura_despacho))
            cmd.Parameters.Add(New SqlParameter("@MATERIA_PRIMA", oBeProducto.Materia_prima))
            cmd.Parameters.Add(New SqlParameter("@KIT", oBeProducto.Kit))
            cmd.Parameters.Add(New SqlParameter("@TOLERANCIA", oBeProducto.Tolerancia))
            cmd.Parameters.Add(New SqlParameter("@CICLO_VIDA", oBeProducto.Ciclo_vida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
            If Not oBeProducto.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeProducto.Imagen))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", oBeProducto.Noserie))
            cmd.Parameters.Add(New SqlParameter("@NOPARTE", oBeProducto.Noparte))
            cmd.Parameters.Add(New SqlParameter("@FECHAMANUFACTURA", oBeProducto.Fechamanufactura))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_ANIADA", oBeProducto.Capturar_aniada))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_ARANCEL", oBeProducto.Captura_arancel))
            cmd.Parameters.Add(New SqlParameter("@ES_HARDWARE", oBeProducto.Es_hardware))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto.Alto))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto.Ancho))
            If Not oBeProducto.IdUnidadMedidaCobro = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", oBeProducto.IdUnidadMedidaCobro))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    'Funcion PARA SABER SI EXISTE EL PRODUCTO 
    Public Shared Function Existe_Producto_By_Codigo(ByVal pCodigo As String) As Boolean

        Try

            Dim lExists As Boolean = False
            Dim vSQL As String = "SELECT COUNT(*) FROM Producto WHERE codigo=@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.Transaction = lTransaction
                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_Control_Vencimiento_By_Codigo(ByVal pCodigo As String) As Boolean

        Get_Control_Vencimiento_By_Codigo = False

        Try

            Dim BeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT control_vencimiento FROM producto WHERE Codigo=@Codigo"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("control_vencimiento") IsNot DBNull.Value AndAlso lRow("control_vencimiento") IsNot Nothing Then
                                Get_Control_Vencimiento_By_Codigo = CType(lRow("control_vencimiento"), Boolean)
                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Get_Control_Lote_By_Codigo(ByVal pCodigo As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Get_Control_Lote_By_Codigo = False

        Try

            Dim vSQL As String = "SELECT control_lote FROM producto WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("Control_lote") IsNot DBNull.Value AndAlso lRow("Control_lote") IsNot Nothing Then
                        Get_Control_Lote_By_Codigo = CType(lRow("Control_lote"), Boolean)
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Obtener_SO(ByRef oBeProducto As clsBeProducto,
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction) As Boolean

        Obtener_SO = False

        Try

            Const sp As String = " SELECT * FROM Producto 
                                   Where(IdProducto = @IdProducto)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                oBeProducto = New clsBeProducto()
                Cargar_Sin_Objetos(oBeProducto, dt.Rows(0), lConnection, lTransaction)
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto As clsBeProducto,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = " SELECT * FROM Producto 
                                   Where(IdProducto = @IdProducto)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                oBeProducto = New clsBeProducto()
                Cargar(oBeProducto, dt.Rows(0), lConnection, lTransaction)
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#EJC20171107_REF15_0401PM: Función Cargar en Partial de producto con transacción para importación de detalle de productos del pedido de traslado en interface
    Public Shared Sub Cargar(ByRef oBeProducto As clsBeProducto,
                             ByRef dr As DataRow,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            Dim IdxPropietario As Integer = -1
            Dim IdxClasificacion As Integer = -1
            Dim IdxFamilia As Integer = -1
            Dim IdxMarca As Integer = -1
            Dim IdxTipoProducto As Integer = -1
            Dim IdxUnidadMedida As Integer = -1
            Dim IdxPresentacion As Integer = -1
            Dim IdxParametros As Integer = -1
            Dim IdxCodigosBarra As Integer = -1
            Dim IdxArancel As Integer = -1

            With oBeProducto

                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))

                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Propietario = New clsBePropietarios
                .Propietario.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                If .IdPropietario <> 0 Then
                    clsLnPropietarios.ObtenerSinImagen(.Propietario, lConnection, lTransaction)
                End If

                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                .Clasificacion = New clsBeProducto_clasificacion
                .Clasificacion.IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                If .IdClasificacion <> 0 Then
                    clsLnProducto_clasificacion.Obtener(.Clasificacion, lConnection, lTransaction)
                End If

                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                .Familia = New clsBeProducto_familia
                .Familia.IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                If .IdFamilia <> 0 Then
                    clsLnProducto_familia.Obtener(.Familia, lConnection, lTransaction)
                End If

                .IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                .Marca = New clsBeProducto_marca
                .Marca.IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                If .IdMarca <> 0 Then
                    clsLnProducto_marca.Obtener(.Marca, lConnection, lTransaction)
                End If

                .IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                .TipoProducto = New clsBeProducto_tipo
                .TipoProducto.IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                If .IdTipoProducto <> 0 Then
                    clsLnProducto_tipo.Obtener(.TipoProducto, lConnection, lTransaction)
                End If

                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .Indice_Rotacion = New clsBeIndice_rotacion
                .Indice_Rotacion.IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                If .IdIndiceRotacion <> 0 Then
                    clsLnIndice_rotacion.Obtener(.Indice_Rotacion, lConnection, lTransaction)
                End If

                .IdProductoParametroA = IIf(IsDBNull(dr.Item("IdProductoParametroA")), 0, dr.Item("IdProductoParametroA"))
                .ParametroA = New clsBeProducto_parametro_a
                .ParametroA.IdProductoParametroA = IIf(IsDBNull(dr.Item("IdProductoParametroA")), 0, dr.Item("IdProductoParametroA"))
                If .IdProductoParametroA <> 0 Then
                    clsLnProducto_parametro_a.Obtener(.ParametroA, lConnection, lTransaction)
                End If

                .IdProductoParametroB = IIf(IsDBNull(dr.Item("IdProductoParametroB")), 0, dr.Item("IdProductoParametroB"))
                .ParametroB = New clsBeProducto_parametro_b
                .ParametroB.IdProductoParametroB = IIf(IsDBNull(dr.Item("IdProductoParametroB")), 0, dr.Item("IdProductoParametroB"))
                If .IdProductoParametroB <> 0 Then
                    clsLnProducto_parametro_b.Obtener(.ParametroB, lConnection, lTransaction)
                End If

                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .UnidadMedida = New clsBeUnidad_medida
                .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                If .IdUnidadMedidaBasica <> 0 Then
                    clsLnUnidad_medida.Obtener(.UnidadMedida, lConnection, lTransaction)
                End If

                .IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                .Arancel = New clsBeArancel
                .Arancel.IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                If .IdArancel <> 0 Then
                    clsLnArancel.Obtener(.Arancel, lConnection, lTransaction)
                End If

                .Presentaciones = clsLnProducto_presentacion.Get_All_By_IdProducto_HH(.IdProducto, lConnection, lTransaction)
                .Codigos_Barra = clsLnProducto_codigos_barra.Get_All_By_IdProducto(.IdProducto, True, lConnection, lTransaction)
                .Parametros = clsLnProducto_parametros.Get_All_By_IdProducto(.IdProducto, True, lConnection, lTransaction)

                .IdCamara = IIf(IsDBNull(dr.Item("IdCamara")), 0, dr.Item("IdCamara"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .IdPerfilSerializado = IIf(IsDBNull(dr.Item("IdPerfilSerializado")), 0, dr.Item("IdPerfilSerializado"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Existencia_min = IIf(IsDBNull(dr.Item("existencia_min")), 0.0, dr.Item("existencia_min"))
                .Existencia_max = IIf(IsDBNull(dr.Item("existencia_max")), 0.0, dr.Item("existencia_max"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Serializado = IIf(IsDBNull(dr.Item("serializado")), False, dr.Item("serializado"))
                .Genera_lote = IIf(IsDBNull(dr.Item("genera_lote")), False, dr.Item("genera_lote"))
                .Genera_lp = IIf(IsDBNull(dr.Item("genera_lp_old")), False, dr.Item("genera_lp_old"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                .Precio = IIf(IsDBNull(dr.Item("precio")), 0.0, dr.Item("precio"))
                .Control_vencimiento = IIf(IsDBNull(dr.Item("control_vencimiento")), False, dr.Item("control_vencimiento"))
                .Control_lote = IIf(IsDBNull(dr.Item("control_lote")), False, dr.Item("control_lote"))
                .IdSimbologia = IIf(IsDBNull(dr.Item("IdSimbologia")), "0", dr.Item("IdSimbologia"))
                .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), "0", dr.Item("IdTipoEtiqueta"))

                Try

                    .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))

                Catch ex As Exception

                End Try

                .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                .Peso_recepcion = IIf(IsDBNull(dr.Item("peso_recepcion")), False, dr.Item("peso_recepcion"))
                .Peso_despacho = IIf(IsDBNull(dr.Item("peso_despacho")), False, dr.Item("peso_despacho"))
                .Peso_referencia = IIf(IsDBNull(dr.Item("peso_referencia")), 0.0, dr.Item("peso_referencia"))
                .Peso_tolerancia = IIf(IsDBNull(dr.Item("peso_tolerancia")), 0.0, dr.Item("peso_tolerancia"))
                .Temperatura_recepcion = IIf(IsDBNull(dr.Item("temperatura_recepcion")), False, dr.Item("temperatura_recepcion"))
                .Temperatura_despacho = IIf(IsDBNull(dr.Item("temperatura_despacho")), False, dr.Item("temperatura_despacho"))
                .Temperatura_referencia = IIf(IsDBNull(dr.Item("temperatura_referencia")), 0.0, dr.Item("temperatura_referencia"))
                .Temperatura_tolerancia = IIf(IsDBNull(dr.Item("temperatura_tolerancia")), 0.0, dr.Item("temperatura_tolerancia"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .Materia_prima = IIf(IsDBNull(dr.Item("materia_prima")), False, dr.Item("materia_prima"))
                .Kit = IIf(IsDBNull(dr.Item("kit")), False, dr.Item("kit"))
                .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Noserie = IIf(IsDBNull(dr.Item("NoSerie")), "", dr.Item("NoSerie"))
                .Noparte = IIf(IsDBNull(dr.Item("NoParte")), "", dr.Item("NoParte"))
                .Fechamanufactura = IIf(IsDBNull(dr.Item("FechaManufactura")), False, dr.Item("FechaManufactura"))
                .Capturar_aniada = IIf(IsDBNull(dr.Item("Capturar_Aniada")), False, dr.Item("Capturar_Aniada"))
                .Control_peso = IIf(IsDBNull(dr.Item("Control_Peso")), False, dr.Item("Control_Peso"))
                .Captura_arancel = IIf(IsDBNull(dr.Item("captura_arancel")), False, dr.Item("captura_arancel"))
                .Es_hardware = IIf(IsDBNull(dr.Item("es_hardware")), False, dr.Item("es_hardware"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), False, dr.Item("alto"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), False, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), False, dr.Item("largo"))
                .Margen_Impresion = IIf(IsDBNull(dr.Item("margen_impresion")), 0, dr.Item("margen_impresion"))
                .IdUnidadMedidaCobro = IIf(IsDBNull(dr.Item("IdUnidadMedidaCobro")), 0, dr.Item("IdUnidadMedidaCobro"))
                .Dias_Inventario_Promedio = IIf(IsDBNull(dr.Item("Dias_Inventario_Promedio")), 90, dr.Item("Dias_Inventario_Promedio"))
                .IdTipoManufactura = IIf(IsDBNull(dr.Item("IdTipoManufactura")), 0, dr.Item("IdTipoManufactura"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar_Sin_Objetos(ByRef oBeProducto As clsBeProducto,
                                         ByRef dr As DataRow,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction)

        Try

            Dim IdxPropietario As Integer = -1
            Dim IdxClasificacion As Integer = -1
            Dim IdxFamilia As Integer = -1
            Dim IdxMarca As Integer = -1
            Dim IdxTipoProducto As Integer = -1
            Dim IdxUnidadMedida As Integer = -1
            Dim IdxPresentacion As Integer = -1
            Dim IdxParametros As Integer = -1
            Dim IdxCodigosBarra As Integer = -1
            Dim IdxArancel As Integer = -1

            With oBeProducto

                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))

                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Propietario = New clsBePropietarios
                .Propietario.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                If .IdPropietario <> 0 Then
                    'clsLnPropietarios.ObtenerSinImagen(.Propietario, lConnection, lTransaction)
                End If

                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                .Clasificacion = New clsBeProducto_clasificacion
                .Clasificacion.IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                If .IdClasificacion <> 0 Then
                    'clsLnProducto_clasificacion.Obtener(.Clasificacion, lConnection, lTransaction)
                End If

                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                .Familia = New clsBeProducto_familia
                .Familia.IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                If .IdFamilia <> 0 Then
                    'clsLnProducto_familia.Obtener(.Familia, lConnection, lTransaction)
                End If

                .IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                .Marca = New clsBeProducto_marca
                .Marca.IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                If .IdMarca <> 0 Then
                    'clsLnProducto_marca.Obtener(.Marca, lConnection, lTransaction)
                End If

                .IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                .TipoProducto = New clsBeProducto_tipo
                .TipoProducto.IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                If .IdTipoProducto <> 0 Then
                    'clsLnProducto_tipo.Obtener(.TipoProducto, lConnection, lTransaction)
                End If

                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .UnidadMedida = New clsBeUnidad_medida
                .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                If .IdUnidadMedidaBasica <> 0 Then
                    'clsLnUnidad_medida.Obtener(.UnidadMedida, lConnection, lTransaction)
                End If

                .IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                .Arancel = New clsBeArancel
                .Arancel.IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                If .IdArancel <> 0 Then
                    'clsLnArancel.Obtener(.Arancel, lConnection, lTransaction)
                End If

                '.Presentaciones = clsLnProducto_presentacion.Get_All_By_IdProducto_HH(.IdProducto, lConnection, lTransaction)
                '.Codigos_Barra = clsLnProducto_codigos_barra.Get_All_By_IdProducto(.IdProducto, True, lConnection, lTransaction)
                '.Parametros = clsLnProducto_parametros.Get_All_By_IdProducto(.IdProducto, True, lConnection, lTransaction)

                .IdCamara = IIf(IsDBNull(dr.Item("IdCamara")), 0, dr.Item("IdCamara"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .IdPerfilSerializado = IIf(IsDBNull(dr.Item("IdPerfilSerializado")), 0, dr.Item("IdPerfilSerializado"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Existencia_min = IIf(IsDBNull(dr.Item("existencia_min")), 0.0, dr.Item("existencia_min"))
                .Existencia_max = IIf(IsDBNull(dr.Item("existencia_max")), 0.0, dr.Item("existencia_max"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Serializado = IIf(IsDBNull(dr.Item("serializado")), False, dr.Item("serializado"))
                .Genera_lote = IIf(IsDBNull(dr.Item("genera_lote")), False, dr.Item("genera_lote"))
                .Genera_lp = IIf(IsDBNull(dr.Item("genera_lp_old")), False, dr.Item("genera_lp_old"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                .Precio = IIf(IsDBNull(dr.Item("precio")), 0.0, dr.Item("precio"))
                .Control_vencimiento = IIf(IsDBNull(dr.Item("control_vencimiento")), False, dr.Item("control_vencimiento"))
                .Control_lote = IIf(IsDBNull(dr.Item("control_lote")), False, dr.Item("control_lote"))
                .IdSimbologia = IIf(IsDBNull(dr.Item("IdSimbologia")), "0", dr.Item("IdSimbologia"))
                .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), "0", dr.Item("IdTipoEtiqueta"))

                Try

                    .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))

                Catch ex As Exception

                End Try

                .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                .Peso_recepcion = IIf(IsDBNull(dr.Item("peso_recepcion")), False, dr.Item("peso_recepcion"))
                .Peso_despacho = IIf(IsDBNull(dr.Item("peso_despacho")), False, dr.Item("peso_despacho"))
                .Peso_referencia = IIf(IsDBNull(dr.Item("peso_referencia")), 0.0, dr.Item("peso_referencia"))
                .Peso_tolerancia = IIf(IsDBNull(dr.Item("peso_tolerancia")), 0.0, dr.Item("peso_tolerancia"))
                .Temperatura_recepcion = IIf(IsDBNull(dr.Item("temperatura_recepcion")), False, dr.Item("temperatura_recepcion"))
                .Temperatura_despacho = IIf(IsDBNull(dr.Item("temperatura_despacho")), False, dr.Item("temperatura_despacho"))
                .Temperatura_referencia = IIf(IsDBNull(dr.Item("temperatura_referencia")), 0.0, dr.Item("temperatura_referencia"))
                .Temperatura_tolerancia = IIf(IsDBNull(dr.Item("temperatura_tolerancia")), 0.0, dr.Item("temperatura_tolerancia"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .Materia_prima = IIf(IsDBNull(dr.Item("materia_prima")), False, dr.Item("materia_prima"))
                .Kit = IIf(IsDBNull(dr.Item("kit")), False, dr.Item("kit"))
                .Tolerancia = IIf(IsDBNull(dr.Item("tolerancia")), 0, dr.Item("tolerancia"))
                .Ciclo_vida = IIf(IsDBNull(dr.Item("ciclo_vida")), 0, dr.Item("ciclo_vida"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Noserie = IIf(IsDBNull(dr.Item("NoSerie")), "", dr.Item("NoSerie"))
                .Noparte = IIf(IsDBNull(dr.Item("NoParte")), "", dr.Item("NoParte"))
                .Fechamanufactura = IIf(IsDBNull(dr.Item("FechaManufactura")), False, dr.Item("FechaManufactura"))
                .Capturar_aniada = IIf(IsDBNull(dr.Item("Capturar_Aniada")), False, dr.Item("Capturar_Aniada"))
                .Control_peso = IIf(IsDBNull(dr.Item("Control_Peso")), False, dr.Item("Control_Peso"))
                .Captura_arancel = IIf(IsDBNull(dr.Item("captura_arancel")), False, dr.Item("captura_arancel"))
                .Es_hardware = IIf(IsDBNull(dr.Item("es_hardware")), False, dr.Item("es_hardware"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), False, dr.Item("alto"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), False, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), False, dr.Item("largo"))
                .IdUnidadMedidaCobro = IIf(IsDBNull(dr.Item("IdUnidadMedidaCobro")), 0, dr.Item("IdUnidadMedidaCobro"))
                .Dias_Inventario_Promedio = IIf(IsDBNull(dr.Item("Dias_Inventario_Promedio")), 0, dr.Item("Dias_Inventario_Promedio"))
                .IdTipoManufactura = IIf(IsDBNull(dr.Item("IdTipoManufactura")), 0, dr.Item("IdTipoManufactura"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    '#JP20180125: listado de codigos para importacion de excel
    Public Shared Function GetCodigosProd_By_IdPropietarioBodega(ByVal pIdBodega As Integer,
                                                                 ByVal pIdPropietarioBodega As Integer) As DataTable

        GetCodigosProd_By_IdPropietarioBodega = Nothing

        Try

            Dim lTable As New DataTable("Result")

            Dim vSQL As String = "SELECT  producto.IdProducto, producto.codigo
                        From producto INNER Join
                        producto_bodega On producto.IdProducto = producto_bodega.IdProducto inner Join
                        propietario_bodega On propietario_bodega.IdPropietario = producto.IdPropietario
                        Where (producto_bodega.Idbodega = @Idbodega) And (propietario_bodega.IdPropietarioBodega = @IdPropietarioBodega)
                        Order By producto.codigo"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@Idbodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDataAdapter.Fill(lTable)
                        GetCodigosProd_By_IdPropietarioBodega = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'GT 07052021 lista todos los codigos de producto para importación de excel
    Public Shared Function GetCodigosProd_By_Multi_Propietario() As DataTable

        GetCodigosProd_By_Multi_Propietario = Nothing

        Try

            Dim lTable As New DataTable("Result")

            Dim vSQL As String = "SELECT DISTINCT producto.IdProducto, producto.codigo
                        From producto INNER Join
                        producto_bodega On producto.IdProducto = producto_bodega.IdProducto inner Join
                        propietario_bodega On propietario_bodega.IdPropietario = producto.IdPropietario
                        Order By producto.codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.Fill(lTable)
                        GetCodigosProd_By_Multi_Propietario = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Shared Function Tiene_Control_Por_Peso_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                                      ByVal pConnection As SqlConnection,
                                                                      ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim CrtlPeso As Boolean = False

            Dim vSQL As String = "SELECT control_peso
                                  FROM Producto INNER JOIN
                                  producto_bodega on producto.idproducto = producto_bodega.idproducto
                                  WHERE IdProductoBodega=@IdProductoBodega"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

                lCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    CrtlPeso = lReturnValue
                End If

                lCommand.Dispose()

            End Using

            Return CrtlPeso

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Control_Por_Lote_Y_Vencimiento(ByVal IdProductoBodega As Integer,
                                                                ByRef IdProducto As Integer,
                                                                ByRef Control_Lote As Boolean,
                                                                ByRef Control_Vencimiento As Boolean) As Boolean

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT producto_bodega.idproducto, Producto.Control_lote, Producto.Control_vencimiento
                    FROM Producto INNER JOIN
                    producto_bodega on producto.idproducto = producto_bodega.idproducto
                    where IdProductoBodega=@IdProductoBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                        lDataAdapter.Fill(lTable)

                        If lTable.Rows.Count > 0 Then
                            IdProducto = IIf(IsDBNull(lTable.Rows(0).Item("IdProducto")), "", lTable.Rows(0).Item("IdProducto"))
                            Control_Lote = IIf(IsDBNull(lTable.Rows(0).Item("Control_lote")), False, lTable.Rows(0).Item("Control_lote"))
                            Control_Vencimiento = IIf(IsDBNull(lTable.Rows(0).Item("Control_vencimiento")), False, lTable.Rows(0).Item("Control_vencimiento"))
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

    Public Shared Function Get_All_By_IdProductoBodega_For_Actualizacion_Inventario(ByVal pIdProductoBodega As Integer,
                                                                                   ByRef lConnection As SqlConnection,
                                                                                   ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_All_By_IdProductoBodega_For_Actualizacion_Inventario = Nothing

        Try

            Dim vSQL As String = "SELECT p.* FROM Producto_Bodega AS pb INNER JOIN Producto AS p ON pb.IdProducto = p.IdProducto " &
                " AND pb.IdProductoBodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()

                    Cargar(ObjProducto, lRow, lConnection, lTransaction)

                    Return ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv(ByVal pIdPropietario As Integer,
                                                  ByRef pListFamilia As DataTable,
                                                  ByRef pListClasificacion As DataTable,
                                                  ByRef pListMarca As DataTable,
                                                  ByRef pListTipo As DataTable,
                                                  ByRef pListUMBas As DataTable) As Boolean

        Get_Datos_Maestros_Inv = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#Obtiene lista de producto_familia
            pListFamilia = clsLnProducto_familia.Get_All_By_IdPropietario(True,
                                                                          pIdPropietario,
                                                                          lConnection,
                                                                          lTransaction)

            'Obtiene lista de producto clasificación
            pListClasificacion = clsLnProducto_clasificacion.Get_All_By_IdPropietario(True,
                                                                                      pIdPropietario,
                                                                                      lConnection,
                                                                                      lTransaction)

            'Obtiene lista de producto marca
            pListMarca = clsLnProducto_marca.Get_All_By_IdPropietario(True,
                                                                      pIdPropietario,
                                                                      lConnection,
                                                                      lTransaction)

            'Obtiene lista de tipo de producto 
            pListTipo = clsLnProducto_tipo.Get_All_By_IdPropietario(True,
                                                                    pIdPropietario,
                                                                    lConnection,
                                                                    lTransaction)

            'Obtiene lista de unidades de medida 
            pListUMBas = clsLnUnidad_medida.Get_All_By_IdPropietario(True,
                                                                     pIdPropietario,
                                                                     lConnection,
                                                                     lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv_Fam(ByVal pIdPropietario As Integer, ByRef pListFamilia As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Fam = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#Obtiene lista de producto_familia
            pListFamilia = clsLnProducto_familia.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv_Fam = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv_Clas(ByVal pIdPropietario As Integer, ByRef pListClasificacion As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Clas = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#Obtiene lista de producto_familia
            pListClasificacion = clsLnProducto_clasificacion.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv_Clas = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv_Marc(ByVal pIdPropietario As Integer, ByRef pListMarca As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Marc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de producto marca
            pListMarca = clsLnProducto_marca.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv_Marc = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv_Tip(ByVal pIdPropietario As Integer, ByRef pListTipo As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Tip = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de tipo de producto 
            pListTipo = clsLnProducto_tipo.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv_Tip = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Datos_Maestros_Inv_UmBas(ByVal pIdPropietario As Integer, ByRef pListUMBas As DataTable) As Boolean

        Get_Datos_Maestros_Inv_UmBas = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de unidades de medida 
            pListUMBas = clsLnUnidad_medida.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Get_Datos_Maestros_Inv_UmBas = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Get_Familia_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Familia_Inv = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListFamilia As New DataTable("Familia")

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#Obtiene lista de producto_familia
            pListFamilia = clsLnProducto_familia.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Return pListFamilia

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Clasificacion_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Clasificacion_Inv = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListClasificacion As New DataTable("Clasificacion")

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de producto clasificación
            pListClasificacion = clsLnProducto_clasificacion.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Return pListClasificacion

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Marca_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Marca_Inv = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListMarca As New DataTable("Marca")

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de producto marca
            pListMarca = clsLnProducto_marca.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Return pListMarca

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_UMbas_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_UMbas_Inv = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListUMBas As New DataTable("Umbas")

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de producto marca
            pListUMBas = clsLnUnidad_medida.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Return pListUMBas

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Tipo_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Tipo_Inv = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListTipo As New DataTable("Clasificacion")

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Obtiene lista de producto marca
            pListTipo = clsLnProducto_tipo.Get_All_By_IdPropietario(True, pIdPropietario, lConnection, lTransaction)

            lTransaction.Commit()

            Return pListTipo

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Productos_ForImpresionBarras(ByVal pIdPropietario As Integer,
                                                                ByVal pIdClasificacion As Integer,
                                                                ByVal pIdFamilia As Integer,
                                                                ByVal pIdMarca As Integer) As List(Of clsBeProducto_Barras_Seleccion)

        Get_All_Productos_ForImpresionBarras = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_Barras_Seleccion)

            Dim vSQL As String = "SELECT codigo,nombre,codigo_barra FROM Producto WHERE 1=1 "

            If pIdPropietario <> 0 Then
                vSQL += " AND IdPropietario=@IdPropietario "
            End If

            If pIdClasificacion <> 0 Then
                vSQL += " AND IdClasificacion=@IdClasificacion "
            End If

            If pIdFamilia <> 0 Then
                vSQL += " AND IdFamilia=@IdFamilia "
            End If

            If pIdMarca <> 0 Then
                vSQL += " AND IdMarca=@IdMarca "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdPropietario <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        End If

                        If pIdClasificacion <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)
                        End If

                        If pIdFamilia <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)
                        End If

                        If pIdMarca <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_Barras_Seleccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_Barras_Seleccion()
                                Obj.Codigo = lRow.Item("codigo")
                                Obj.Codigo_Barra = IIf(IsDBNull(lRow.Item("codigo_barra")), "", lRow.Item("codigo_barra"))
                                Obj.Descripcion = IIf(IsDBNull(lRow.Item("nombre")), "", lRow.Item("nombre"))
                                Obj.Seleccionar = False
                                lReturnList.Add(Obj)

                            Next

                            Get_All_Productos_ForImpresionBarras = lReturnList

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

    Public Shared Function Get_All_Productos_For_Impresion_Barras_By_Filtro(ByVal IdBodega As Integer,
                                                                            ByVal pIdPropietario As Integer,
                                                                            ByVal pCodigo As String,
                                                                            ByVal pNombre As String,
                                                                            ByVal pIdClasificacion As Integer,
                                                                            ByVal lConnection As SqlConnection,
                                                                            ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Productos_For_Impresion_Barras_By_Filtro = Nothing

        Try

            Dim vSQL As String = "SELECT ISNULL(P.IdClasificacion,0) AS IdClasificacion,
                    P.IdProducto,
                    P.codigo,P.nombre,P.codigo_barra,P.IdUnidadMedidaBasica 
                    FROM Producto P inner join 
                    Producto_bodega PB ON PB.IdProducto = P.IdProducto  "

            vSQL += " WHERE PB.IdBodega = @IdBodega 
                      And P.activo = 1 "

            If pIdPropietario <> 0 Then
                vSQL += " AND (P.IdPropietario = @IdPropietario ) "
            End If

            If pCodigo <> "" Then
                vSQL += String.Format(" AND (P.codigo LIKE '%{0}%' OR P.codigo_barra LIKE '%{0}%') ", pCodigo)
            End If

            If pNombre <> "" Then
                vSQL += String.Format(" AND (P.nombre LIKE '%{0}%') ", pCodigo)
            End If

            If pIdClasificacion <> 0 Then
                vSQL += " AND (P.IdClasificacion = @IdClasificacion) "
            End If

            vSQL += " ORDER BY P.codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)


                If pIdPropietario <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                End If

                If pCodigo <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", IdBodega)
                End If

                If pNombre <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", IdBodega)
                End If

                If Val(pIdClasificacion) <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", IdBodega)
                End If

                Dim lDataTable As New DataTable("Productos")
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Print_BarCodes(ByVal pIdBodega As Integer,
                                                      ByVal pIdEmpresa As Integer,
                                                      ByRef pIdPropietario As Integer,
                                                      ByVal pCodigo As String,
                                                      ByVal pNombre As String,
                                                      ByVal pIdClasificacion As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Get_All_For_Print_BarCodes = Get_All_Productos_For_Impresion_Barras_By_Filtro(pIdBodega,
                                                                                          pIdPropietario,
                                                                                          pCodigo,
                                                                                          pNombre,
                                                                                          pIdClasificacion,
                                                                                          lConnection,
                                                                                          lTransaction)

            lTransaction.Commit()


        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_UMB_Pres_ByIdProducto_And_IdBodega(ByVal IdBodega As Integer,
                                                                  ByVal IdUMBas As Integer,
                                                                  ByVal IdProducto As Integer,
                                                                  ByRef UMBas As DataTable,
                                                                  ByRef Presentaciones As DataTable,
                                                                  ByRef Stock As DataTable) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            UMBas = clsLnUnidad_medida.Get_UMBas_ByIdUMBas(IdUMBas,
                                                           lConnection,
                                                           lTransaction)

            Presentaciones = clsLnProducto_presentacion.Get_All_By_IdProducto_ForImpresion(IdProducto,
                                                                                           lConnection,
                                                                                           lTransaction)

            Stock = clsLnStock.GetAllStockDTByIdBodega_AndIdProducto(IdBodega,
                                                                     IdProducto,
                                                                     lConnection,
                                                                     lTransaction)

            lTransaction.Commit()

            Get_UMB_Pres_ByIdProducto_And_IdBodega = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxTolerancia() As Integer

        MaxTolerancia = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT  MAX(tolerancia) as tolerancia FROM producto"
            Dim sp As String = vSQL

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                MaxTolerancia = IIf(IsDBNull(dt.Rows(0).Item("tolerancia")), "0", dt.Rows(0).Item("tolerancia"))
            End If

            cmd.Dispose() : dad.Dispose()

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Lista_For_Grid_By_IdBodega(ByVal pIdBodega As Integer) As DataTable


        Get_Lista_For_Grid_By_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT prb.IdProductoBodega, 
                                         pd.codigo AS Codigo,
                                         pd.codigo_barra AS CodigoBarra,
                                         pd.nombre AS Nombre, 
                                         u.Nombre as UMBas,
                                         pd.IdUnidadMedidaBasica as IdUmBas,
                                         pd.costo as Costo,
                                         pd.kit as Kit,
                                         pd.IdProducto,
                                         pd.control_peso as ControlPeso
                                         --pd.peso_referencia as PesoReferenciaUMBas
                                    FROM producto AS pd INNER JOIN
                                        unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida INNER JOIN
                                        producto_bodega AS prb ON pd.IdProducto = prb.IdProducto INNER JOIN
                                        propietario_bodega ON pd.IdPropietario = propietario_bodega.IdPropietario AND 
                                                            prb.IdBodega = propietario_bodega.IdBodega
                                    WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += String.Format(" AND prb.IdBodega={0}", pIdBodega)
            Else
                Get_Lista_For_Grid_By_IdBodega = Nothing
                Exit Function
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Lista_For_Grid_By_IdBodega = lDataTable
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_For_Grid_By_IdPropietario_And_IdBodega(ByVal pIdPropietarioBodega As Integer,
                                                                            ByVal pIdBodega As Integer) As DataTable


        Get_Lista_For_Grid_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT prb.IdProductoBodega, 
                                          pd.codigo AS Codigo, 
                                          pd.codigo_barra AS CodigoBarra, 
                                          pd.nombre AS Nombre, 
                                          u.Nombre as UMBas,
                                          pd.IdUnidadMedidaBasica as IdUmBas,
                                          pd.costo as Costo,
                                          pd.kit as Kit,
                                          pd.IdProducto,
                                          pd.control_peso as ControlPeso,
                                          --pd.peso_referencia as PesoReferenciaUMBas,
                                          --pd.IdFamilia,
                                          --pd.IdClasificacion,
                                          --pd.IdTipoProducto
                                          isnull(pf.nombre,'N/D') as Familia,
                                          isnull(pc.nombre,'N/D') as Clasificacion,
                                          ISNULL(pt.NombreTipoProducto,'N/D') as TipoProducto
                                          FROM producto AS pd INNER JOIN
                                          unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida INNER JOIN
                                          producto_bodega AS prb ON pd.IdProducto = prb.IdProducto INNER JOIN
                                          propietario_bodega ON pd.IdPropietario = propietario_bodega.IdPropietario AND prb.IdBodega = propietario_bodega.IdBodega
                                          LEFT JOIN producto_tipo as pt ON pd.IdTipoProducto=pt.IdTipoProducto
										  LEFT JOIN producto_familia as pf ON PD.IdFamilia = PF.IdFamilia
										  LEFT JOIN producto_clasificacion as pc ON pd.IdClasificacion=pc.IdClasificacion
                                          WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += String.Format(" AND prb.IdBodega={0}", pIdBodega)
            Else
                Get_Lista_For_Grid_By_IdPropietario_And_IdBodega = Nothing
                Exit Function
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += String.Format(" AND propietario_bodega.IdPropietarioBodega={0}", pIdPropietarioBodega)
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Lista_For_Grid_By_IdPropietario_And_IdBodega = lDataTable
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_CodigoBarra_By_IdProducto(ByVal pIdProducto As Integer) As String

        Get_CodigoBarra_By_IdProducto = ""

        Try

            Dim vSQL As String = "SELECT p.codigo_barra
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_CodigoBarra_By_IdProducto = CType(lRow("codigo_barra"), String)

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

    Public Shared Function Eliminar_Transaccion(ByRef oBeProducto As clsBeProducto) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vCantRegistrosEliminados As Integer = 0
        Eliminar_Transaccion = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            vCantRegistrosEliminados = clsLnProducto_bodega.Eliminar_Todos_By_IdProducto(oBeProducto.IdProducto, lConnection, lTransaction)
            vCantRegistrosEliminados += clsLnProducto_presentacion.Eliminar_Todos_By_IdProducto(oBeProducto.IdProducto, lConnection, lTransaction)
            vCantRegistrosEliminados += Eliminar(oBeProducto, lConnection, lTransaction)

            lTransaction.Commit()

            Eliminar_Transaccion = vCantRegistrosEliminados

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_CodigoBarra_By_IdProducto(ByVal pIdProducto As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As String

        Get_CodigoBarra_By_IdProducto = ""

        Try

            Dim vSQL As String = "SELECT p.codigo_barra
                                  FROM producto p                        
                                  WHERE (p.IdProducto=@IdProducto) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_CodigoBarra_By_IdProducto = CType(lRow("codigo_barra"), String)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_CodigoProducto_By_IdProducto(ByVal pIdProducto As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As String

        Get_CodigoProducto_By_IdProducto = ""

        Try

            Dim vSQL As String = "SELECT p.codigo
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_CodigoProducto_By_IdProducto = CType(lRow("codigo"), String)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Calcula_Indices_Rotacion() As List(Of clsBeProductoRotacionBodega)


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Calcula_Indices_Rotacion = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lBodegas As New List(Of clsBeBodega)
            lBodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            Dim lProductosByBodega As New List(Of clsBeProducto_bodega)
            Dim lMovimientos As New List(Of clsBeVW_Movimientos)
            Dim vFechaIni As Date = Now
            Dim vFechaFin As Date = Now
            Dim vDeltaIngresosSalidas As Double = 0
            Dim lProductoRotacion As New List(Of clsBeProductoRotacionBodega)
            Dim BeProductoRotacion As New clsBeProductoRotacionBodega
            Dim vTotalIngresosPres As Double = 0
            Dim vTotalSalidasPres As Double = 0

            If Not lBodegas Is Nothing Then

                For Each B In lBodegas

                    lMovimientos = clsLnVW_Movimientos.Get_Top_1_By_IdBodega(B.IdBodega,
                                                                         lConnection,
                                                                         lTransaction)

                    If Not lMovimientos Is Nothing Then

                        lProductosByBodega = clsLnProducto_bodega.Get_All_By_IdBodega(B.IdBodega,
                                                                                  lConnection,
                                                                                  lTransaction)

                        If Not lProductosByBodega Is Nothing Then

                            For Each PB In lProductosByBodega

                                Debug.WriteLine("IdProductoBodega: " & PB.IdProductoBodega)

                                If PB.IdProductoBodega = 12161 Then
                                    Debug.WriteLine("IdProductoBodega: " & PB.IdProductoBodega)
                                End If

                                If Not PB.Producto.Dias_Inventario_Promedio = 0 Then
                                    vFechaIni = Now
                                    vFechaIni = vFechaIni.AddDays(PB.Producto.Dias_Inventario_Promedio * -1)
                                End If

                                'AND TipoTarea IN ('RECE', 'DESP', 'TRAS')"
                                lMovimientos = clsLnVW_Movimientos.Get_All_By_Rango_Fechas_And_IdProductoBodega(vFechaIni,
                                                                                                           vFechaFin,
                                                                                                           PB.IdProductoBodega,
                                                                                                           lConnection,
                                                                                                           lTransaction)

                                If Not lMovimientos Is Nothing Then

                                    Dim TotalIngresosEnPresentacion = From member In lMovimientos.Where(Function(x) x.TipoTarea = clsDataContractDI.tTipoTarea.RECE)
                                                                      Group member By keys = New With {Key member.IdPresentacion, Key member.EstadoOrigen}
                                                                  Into Group
                                                                      Select New With {.IdPresentacion = keys.IdPresentacion, .Estado = keys.EstadoOrigen,
                                                                .Total_Ingresos = Group.Sum(Function(x) x.Cantidad)}

                                    Dim TotalSalidasEnPresentacion = From member In lMovimientos.Where(Function(x) x.TipoTarea = clsDataContractDI.tTipoTarea.DESP OrElse x.TipoTarea = clsDataContractDI.tTipoTarea.TRAS)
                                                                     Group member By keys = New With {Key member.IdPresentacion, Key member.EstadoOrigen}
                                                                Into Group
                                                                     Select New With {.IdPresentacion = keys.IdPresentacion, .Estado = keys.EstadoOrigen,
                                                                .Total_Salidas = Group.Sum(Function(x) x.Cantidad)}


                                    If Not TotalIngresosEnPresentacion Is Nothing AndAlso Not TotalSalidasEnPresentacion Is Nothing Then
                                        vTotalIngresosPres = TotalIngresosEnPresentacion.Sum(Function(x) x.Total_Ingresos)
                                        vTotalSalidasPres = TotalSalidasEnPresentacion.Sum(Function(x) x.Total_Salidas)
                                        vDeltaIngresosSalidas = vTotalIngresosPres - vTotalSalidasPres
                                    Else
                                        vDeltaIngresosSalidas = 0
                                        vTotalIngresosPres = 0
                                        vTotalSalidasPres = 0
                                    End If

                                    BeProductoRotacion = New clsBeProductoRotacionBodega()
                                    BeProductoRotacion.IdProductoBodega = PB.IdProducto
                                    BeProductoRotacion.IdBodega = B.IdBodega
                                    BeProductoRotacion.TotalIngresos += vTotalIngresosPres
                                    BeProductoRotacion.TotalSalidas += vTotalSalidasPres
                                    BeProductoRotacion.InventarioPromedio = BeProductoRotacion.TotalIngresos - BeProductoRotacion.TotalSalidas
                                    BeProductoRotacion.IndiceRotacion = Math.Round(BeProductoRotacion.TotalSalidas / BeProductoRotacion.InventarioPromedio, 6)
                                    lProductoRotacion.Add(BeProductoRotacion)

                                End If

                            Next

                        End If

                    Else
                        'Bodega no tiene movimientos
                    End If

                Next

            End If

            Calcula_Indices_Rotacion = lProductoRotacion

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try


    End Function

    'GT24012022: lista de productos para validar existencia en proceso importacion inv inicial cealsa
    Public Shared Function Exist_By_Codigo(ByVal pCodigo As String,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto)

        Exist_By_Codigo = Nothing

        Dim oBeProducto As New List(Of clsBeProducto)
        Dim producto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM producto " &
                   " WHERE codigo =@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                oBeProducto = Nothing

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    oBeProducto = New List(Of clsBeProducto)

                    For Each lRow As DataRow In lDT.Rows

                        producto = New clsBeProducto
                        Cargar(producto, lRow, lConnection, lTransaction)
                        oBeProducto.Add(producto)
                    Next

                    Return oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Producto_By_Codigo(ByVal pCodigo As String,
                                                  ByVal IdBodega As Integer,
                                                  ByVal IdPropietario As Integer,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_Producto_By_Codigo = Nothing

        Dim oBeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  
                                  WHERE IdBodega = @IdBodega 
                                  And codigo =@Codigo and IdPropietario=@IdPropietario "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Return oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_CodigoBarra(ByVal pCodigoBarra As String,
                                                     ByVal pIdBodega As Integer) As clsBeProducto

        Get_Codigo_By_CodigoBarra = Nothing

        Try

            Dim vSQL As String = "SELECT TOP(1) *
                                  FROM VW_ProductoSI  
                                  WHERE (IdBodega = @IdBodega) and (codigo_barra_pcb = @CodigoBarra or codigo_barra_presentacion = @CodigoBarra or codigo_barra = @CodigoBarra or codigo = @CodigoBarra) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim oBeProducto As New clsBeProducto()

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_Codigo_By_CodigoBarra = oBeProducto

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


    'GT1872022_1300: valida si codigobarras pertence a codigo producto en Picking
    Public Shared Function Exist_Codigo_By_CodigoBarra(ByVal pCodigoBarra As String,
                                                       ByVal pIdProductoBodega As Integer) As Boolean

        Exist_Codigo_By_CodigoBarra = False

        Try

            Dim vSQL As String = "SELECT  distinct *
                                  FROM VW_ProductoSI  
                                  WHERE (IdBodega = 1) and (codigo_barra_pcb = @CodigoBarra or codigo_barra_presentacion = @CodigoBarra or codigo_barra = @CodigoBarra or codigo = @CodigoBarra )
								  AND IdProductoBodega=@IdProductoBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Exist_Codigo_By_CodigoBarra = True
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

    Public Shared Function Get_Product_By_CodigoBarra_By_PickingEnc(ByVal pCodigoBarra As String,
                                                                    ByVal pIdBodega As Integer,
                                                                    ByVal pIdPickingEnc As Integer) As clsBeProducto

        Get_Product_By_CodigoBarra_By_PickingEnc = Nothing

        Try

            Dim vSQL As String = "SELECT TOP(1) *
                                  FROM VW_ProductoSI  
                                  WHERE (IdBodega = @IdBodega) and 
                                        (codigo_barra_pcb = @CodigoBarra or 
                                         codigo_barra_presentacion = @CodigoBarra or 
                                         codigo_barra = @CodigoBarra or 
                                         codigo = @CodigoBarra) AND
                                         IdProductoBodega IN 
                                               (SELECT IdProductoBodega 
                                                FROM trans_picking_ubic 
                                                WHERE IdPickingEnc = @IdPickingEnc)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim oBeProducto As New clsBeProducto()

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_Product_By_CodigoBarra_By_PickingEnc = oBeProducto

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

    Public Shared Function Get_List_Product_By_CodigoBarra_By_PickingEnc(ByVal pCodigoBarra As String,
                                                                         ByVal pIdBodega As Integer,
                                                                         ByVal pIdPickingEnc As Integer) As List(Of clsBeProducto)

        Get_List_Product_By_CodigoBarra_By_PickingEnc = Nothing

        Try

            Dim vSQL As String = " SELECT DISTINCT IdBodega, IdProductoBodega, IdProducto, IdPropietario, IdClasificacion, IdFamilia, IdMarca, 
                                         IdTipoProducto, IdUnidadMedidaBasica, IdCamara, IdTipoRotacion, IdPerfilSerializado, IdIndiceRotacion, 
                                         IdSimbologia, IdArancel, codigo, nombre, codigo_barra, precio, existencia_min, existencia_max, costo, 
                                         peso_referencia, peso_tolerancia, noparte, noserie, control_peso, ciclo_vida, tolerancia, kit, materia_prima, 
                                         control_lote, control_vencimiento, genera_lote, serializado, codigo_barra_presentacion, '' codigo_barra_pcb, 
                                         NomPresentacion, activopp, IdPresentacion, factor, peso_recepcion, peso_despacho, temperatura_referencia, 
                                         temperatura_tolerancia, temperatura_recepcion, temperatura_despacho, fechamanufactura, capturar_aniada, 
                                         Arancel, user_agr, fec_agr, user_mod, fec_mod, captura_arancel, es_hardware, activo, imagen, largo, ancho,  
                                         alto, genera_lp_old, IdUnidadMedidaCobro, IdTipoEtiqueta, dias_inventario_promedio, IdProductoParametroA, 
                                         IdProductoParametroB, IdTipoManufactura, Margen_Impresion
                                         FROM VW_ProductoSI  
                                         WHERE (IdBodega = @IdBodega) and 
                                         (codigo_barra_pcb = @CodigoBarra or 
                                         codigo_barra_presentacion = @CodigoBarra or 
                                         codigo_barra = @CodigoBarra or 
                                         codigo = @CodigoBarra) AND
                                         IdProductoBodega IN 
                                         (SELECT IdProductoBodega 
                                                 FROM trans_picking_ubic 
                                                 WHERE IdPickingEnc = @IdPickingEnc) "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim oBeProducto As New clsBeProducto()
                            Dim lBeProducto As New List(Of clsBeProducto)

                            For Each lRow In lDT.Rows

                                oBeProducto = New clsBeProducto

                                Cargar(oBeProducto, lRow, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                oBeProducto.IsNew = False

                                lBeProducto.Add(oBeProducto)

                            Next

                            If Not lBeProducto Is Nothing Then

                                lBeProducto = lBeProducto.Distinct.ToList()

                                Get_List_Product_By_CodigoBarra_By_PickingEnc = lBeProducto

                            End If

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

    Public Shared Function Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc(ByVal pCodigoBarra As String,
                                                                             ByVal pIdBodega As Integer,
                                                                             ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeProducto)

        Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc = Nothing

        Try
            '#CKFK20241012 Modifiqué este query por el error detectado en Killios donde el producto tiene más de una presentación
            Dim vSQL As String = "SELECT DISTINCT IdBodega, IdProductoBodega, IdProducto, IdPropietario, IdClasificacion, IdFamilia, IdMarca, 
                                         IdTipoProducto, IdUnidadMedidaBasica, IdCamara, IdTipoRotacion, IdPerfilSerializado, IdIndiceRotacion, 
                                         IdSimbologia, IdArancel, codigo, nombre, codigo_barra, precio, existencia_min, existencia_max, costo, 
                                         peso_referencia, peso_tolerancia, noparte, noserie, control_peso, ciclo_vida, tolerancia, kit, materia_prima, 
                                         control_lote, control_vencimiento, genera_lote, serializado, codigo_barra_presentacion, '' codigo_barra_pcb, 
                                         '' NomPresentacion, activopp, 0 IdPresentacion, 0 factor, peso_recepcion, peso_despacho, temperatura_referencia, 
                                         temperatura_tolerancia, temperatura_recepcion, temperatura_despacho, fechamanufactura, capturar_aniada, 
                                         Arancel, user_agr, fec_agr, user_mod, fec_mod, captura_arancel, es_hardware, activo, imagen, largo, ancho,  
                                         alto, genera_lp_old, IdUnidadMedidaCobro, IdTipoEtiqueta, dias_inventario_promedio, IdProductoParametroA, 
                                         IdProductoParametroB, IdTipoManufactura, Margen_Impresion
                                  FROM VW_ProductoSI v
                                  WHERE (IdBodega = @IdBodega) and 
                                        (codigo_barra_pcb = @CodigoBarra or 
                                         codigo_barra_presentacion = @CodigoBarra or 
                                         codigo_barra = @CodigoBarra or 
                                         codigo = @CodigoBarra) AND
                                         EXISTS (SELECT * 
                                                FROM trans_oc_det d
                                                WHERE d.IdOrdenCompraEnc = @IdOrdenCompraEnc AND 
                                                      d.IdProductoBodega = v.IdProductoBodega AND 
                                                      d.IdUnidadMedidaBasica = v.IdUnidadMedidaBasica)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim oBeProducto As New clsBeProducto()
                            Dim lBeProducto As New List(Of clsBeProducto)

                            For Each lRow In lDT.Rows

                                oBeProducto = New clsBeProducto

                                Cargar(oBeProducto, lRow, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                oBeProducto.IsNew = False

                                lBeProducto.Add(oBeProducto)

                            Next

                            If Not lBeProducto Is Nothing Then

                                lBeProducto = lBeProducto.Distinct.ToList()

                                Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc = lBeProducto

                            End If

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

    Public Shared Function GetSingle_For_HH(ByVal pIdProducto As Integer,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As clsBeProducto

        GetSingle_For_HH = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE IdProducto=@IdProducto "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()
                    Cargar_Sin_Objetos(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    GetSingle_For_HH = ObjProducto

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeProducto)

        Get_All_By_IdPedidoEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto 
                                          WHERE IdProducto IN (SELECT producto_bodega.IdProducto
					                      FROM trans_pe_det INNER JOIN
					                      producto_bodega ON trans_pe_det.IdProductoBodega = producto_bodega.IdProductoBodega
                                          WHERE trans_pe_det.IdPedidoEnc = @IdPedidoEnc) "

            vSQL += " ORDER BY Código"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                          If lReturnList Is Nothing Then
                                                                              lReturnList = New List(Of clsBeProducto)
                                                                              Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                          End If

                                                                          SyncLock lReturnList

                                                                              Obj = New clsBeProducto

                                                                              SyncLock Obj

                                                                                  Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                                  If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                                      Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario = New clsBePropietarios()
                                                                                      Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                                  End If

                                                                                  If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                                      Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                                      Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                                  End If

                                                                                  If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                                      Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia = New clsBeProducto_familia()
                                                                                      Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                                  End If

                                                                                  If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                                      Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca = New clsBeProducto_marca()
                                                                                      Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                                  End If

                                                                                  If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                                      Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto = New clsBeProducto_tipo()
                                                                                      Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                                  End If

                                                                                  If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                                      Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida = New clsBeUnidad_medida
                                                                                      Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                                  End If

                                                                                  If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                                      Obj.Codigo = CType(lrow("Código"), String)
                                                                                  End If

                                                                                  If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                                      Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                                  End If

                                                                                  If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                                      Obj.Nombre = CType(lrow("Producto"), String)
                                                                                  End If

                                                                                  If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                                      Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                                  End If

                                                                                  If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                                      Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                                  End If

                                                                                  If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                                      Obj.Costo = CType(lrow("Costo"), Double)
                                                                                  End If

                                                                                  If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                                      Obj.Precio = CType(lrow("Precio"), Double)
                                                                                  End If

                                                                                  If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                                      lReturnList.Add(Obj)
                                                                                  End If

                                                                              End SyncLock

                                                                          End SyncLock

                                                                      End Sub)
                        End If

                        Get_All_By_IdPedidoEnc = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal IdRecepcionEnc As Integer, ByVal IdBodega As Integer) As List(Of clsBeProducto)

        Get_All_By_IdRecepcionEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto 
                                          WHERE IdProducto IN (SELECT producto_bodega.IdProducto
					                      FROM trans_re_det INNER JOIN
					                      producto_bodega ON trans_re_det.IdProductoBodega = producto_bodega.IdProductoBodega
                                          WHERE trans_re_det.IdRecepcionEnc = @IdRecepcionEnc 
                                          AND producto_bodega.IdBodega = @IdBodega ) "

            vSQL += " ORDER BY Código"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                          If lReturnList Is Nothing Then
                                                                              lReturnList = New List(Of clsBeProducto)
                                                                              Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                          End If

                                                                          SyncLock lReturnList

                                                                              Obj = New clsBeProducto

                                                                              SyncLock Obj

                                                                                  Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                                  If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                                      Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario = New clsBePropietarios()
                                                                                      Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                                      Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                                  End If

                                                                                  If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                                      Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                                      Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                                      Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                                  End If

                                                                                  If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                                      Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia = New clsBeProducto_familia()
                                                                                      Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                                      Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                                  End If

                                                                                  If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                                      Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca = New clsBeProducto_marca()
                                                                                      Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                                      Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                                  End If

                                                                                  If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                                      Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto = New clsBeProducto_tipo()
                                                                                      Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                                      Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                                  End If

                                                                                  If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                                      Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida = New clsBeUnidad_medida
                                                                                      Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                                      Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                                  End If

                                                                                  If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                                      Obj.Codigo = CType(lrow("Código"), String)
                                                                                  End If

                                                                                  If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                                      Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                                  End If

                                                                                  If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                                      Obj.Nombre = CType(lrow("Producto"), String)
                                                                                  End If

                                                                                  If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                                      Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                                  End If

                                                                                  If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                                      Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                                  End If

                                                                                  If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                                      Obj.Costo = CType(lrow("Costo"), Double)
                                                                                  End If

                                                                                  If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                                      Obj.Precio = CType(lrow("Precio"), Double)
                                                                                  End If

                                                                                  If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                                      lReturnList.Add(Obj)
                                                                                  End If

                                                                              End SyncLock

                                                                          End SyncLock

                                                                      End Sub)
                        End If

                        Get_All_By_IdRecepcionEnc = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal IdRecepcionEnc As Integer,
                                                     ByVal IdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto)

        Get_All_By_IdRecepcionEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto 
                                          WHERE IdProducto IN (SELECT producto_bodega.IdProducto
					                      FROM trans_re_det INNER JOIN
					                      producto_bodega ON trans_re_det.IdProductoBodega = producto_bodega.IdProductoBodega
                                          WHERE trans_re_det.IdRecepcionEnc = @IdRecepcionEnc 
                                          AND producto_bodega.IdBodega = @IdBodega) "

            vSQL += " ORDER BY Código"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                  If lReturnList Is Nothing Then
                                                                      lReturnList = New List(Of clsBeProducto)
                                                                      Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                  End If

                                                                  SyncLock lReturnList

                                                                      Obj = New clsBeProducto

                                                                      SyncLock Obj

                                                                          Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                          If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                              Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario = New clsBePropietarios()
                                                                              Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                          End If

                                                                          If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                              Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                              Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                          End If

                                                                          If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                              Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia = New clsBeProducto_familia()
                                                                              Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                          End If

                                                                          If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                              Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca = New clsBeProducto_marca()
                                                                              Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                          End If

                                                                          If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                              Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto = New clsBeProducto_tipo()
                                                                              Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                          End If

                                                                          If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                              Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida = New clsBeUnidad_medida
                                                                              Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                          End If

                                                                          If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                              Obj.Codigo = CType(lrow("Código"), String)
                                                                          End If

                                                                          If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                              Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                          End If

                                                                          If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                              Obj.Nombre = CType(lrow("Producto"), String)
                                                                          End If

                                                                          If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                              Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                          End If

                                                                          If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                              Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                          End If

                                                                          If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                              Obj.Costo = CType(lrow("Costo"), Double)
                                                                          End If

                                                                          If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                              Obj.Precio = CType(lrow("Precio"), Double)
                                                                          End If

                                                                          If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                              lReturnList.Add(Obj)
                                                                          End If

                                                                      End SyncLock

                                                                  End SyncLock

                                                              End Sub)
                End If

                Get_All_By_IdRecepcionEnc = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal IdOrdenCompraEnc As Integer,
                                                       ByVal IdBodega As Integer,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto)

        Get_All_By_IdOrdenCompraEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim vSQL As String = "SELECT * FROM VW_Producto 
                                          WHERE IdProducto IN (SELECT producto_bodega.IdProducto
					                      FROM trans_oc_det INNER JOIN
					                      producto_bodega ON trans_oc_det.IdProductoBodega = producto_bodega.IdProductoBodega
                                          WHERE trans_oc_det.IdOrdenCompraEnc = @IdOrdenCompraEnc 
                                          AND producto_bodega.IdBodega = @IdBodega) "

            vSQL += " ORDER BY Código"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                  If lReturnList Is Nothing Then
                                                                      lReturnList = New List(Of clsBeProducto)
                                                                      Debug.Print("Aquí era nothing, no debería entrar aquí")
                                                                  End If

                                                                  SyncLock lReturnList

                                                                      Obj = New clsBeProducto

                                                                      SyncLock Obj

                                                                          Obj.IdProducto = CType(lrow("IdProducto"), Integer)

                                                                          If lrow("IdPropietario") IsNot DBNull.Value AndAlso lrow("IdPropietario") IsNot Nothing Then
                                                                              Obj.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario = New clsBePropietarios()
                                                                              Obj.Propietario.IdPropietario = CType(lrow("IdPropietario"), Integer)
                                                                              Obj.Propietario.Nombre_comercial = CType(lrow("Propietario"), String)
                                                                          End If

                                                                          If lrow("IdClasificacion") IsNot DBNull.Value AndAlso lrow("IdClasificacion") IsNot Nothing Then
                                                                              Obj.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion = New clsBeProducto_clasificacion()
                                                                              Obj.Clasificacion.IdClasificacion = CType(lrow("IdClasificacion"), Integer)
                                                                              Obj.Clasificacion.Nombre = CType(lrow("Clasificación"), String)
                                                                          End If

                                                                          If lrow("IdFamilia") IsNot DBNull.Value AndAlso lrow("IdFamilia") IsNot Nothing Then
                                                                              Obj.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia = New clsBeProducto_familia()
                                                                              Obj.Familia.IdFamilia = CType(lrow("IdFamilia"), Integer)
                                                                              Obj.Familia.Nombre = CType(lrow("Familia"), String)
                                                                          End If

                                                                          If lrow("IdMarca") IsNot DBNull.Value AndAlso lrow("IdMarca") IsNot Nothing Then
                                                                              Obj.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca = New clsBeProducto_marca()
                                                                              Obj.Marca.IdMarca = CType(lrow("IdMarca"), Integer)
                                                                              Obj.Marca.Nombre = CType(lrow("Marca"), String)
                                                                          End If

                                                                          If lrow("IdTipoProducto") IsNot DBNull.Value AndAlso lrow("IdTipoProducto") IsNot Nothing Then
                                                                              Obj.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto = New clsBeProducto_tipo()
                                                                              Obj.TipoProducto.IdTipoProducto = CType(lrow("IdTipoProducto"), String)
                                                                              Obj.TipoProducto.NombreTipoProducto = CType(lrow("Tipo Producto"), String)
                                                                          End If

                                                                          If lrow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lrow("IdUnidadMedidaBasica") IsNot Nothing Then
                                                                              Obj.IdUnidadMedidaBasica = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida = New clsBeUnidad_medida
                                                                              Obj.UnidadMedida.IdUnidadMedida = CType(lrow("IdUnidadMedidaBasica"), String)
                                                                              Obj.UnidadMedida.Nombre = CType(lrow("Unidad Medida"), String)
                                                                          End If

                                                                          If lrow("Código") IsNot DBNull.Value AndAlso lrow("Código") IsNot Nothing Then
                                                                              Obj.Codigo = CType(lrow("Código"), String)
                                                                          End If

                                                                          If lrow("Código de Barra") IsNot DBNull.Value AndAlso lrow("Código de Barra") IsNot Nothing Then
                                                                              Obj.Codigo_barra = CType(lrow("Código de Barra"), String)
                                                                          End If

                                                                          If lrow("Producto") IsNot DBNull.Value AndAlso lrow("Producto") IsNot Nothing Then
                                                                              Obj.Nombre = CType(lrow("Producto"), String)
                                                                          End If

                                                                          If lrow("Existencia Mínima") IsNot DBNull.Value AndAlso lrow("Existencia Mínima") IsNot Nothing Then
                                                                              Obj.Existencia_min = CType(lrow("Existencia Mínima"), Double)
                                                                          End If

                                                                          If lrow("Existencia Máxima") IsNot DBNull.Value AndAlso lrow("Existencia Máxima") IsNot Nothing Then
                                                                              Obj.Existencia_max = CType(lrow("Existencia Máxima"), Double)
                                                                          End If

                                                                          If lrow("Costo") IsNot DBNull.Value AndAlso lrow("Costo") IsNot Nothing Then
                                                                              Obj.Costo = CType(lrow("Costo"), Double)
                                                                          End If

                                                                          If lrow("Precio") IsNot DBNull.Value AndAlso lrow("Precio") IsNot Nothing Then
                                                                              Obj.Precio = CType(lrow("Precio"), Double)
                                                                          End If

                                                                          If Not lReturnList.Exists(Function(x) x.IdProducto = Obj.IdProducto) Then
                                                                              lReturnList.Add(Obj)
                                                                          End If

                                                                      End SyncLock

                                                                  End SyncLock

                                                              End Sub)
                End If

                Get_All_By_IdOrdenCompraEnc = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProducto_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                              ByVal IdBodega As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_BeProducto_By_IdProductoBodega = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  
                            WHERE IdBodega = @IdBodega 
                            And IdProductoBodega=@IdProductoBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Get_BeProducto_By_IdProductoBodega = oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Producto_Clasificacion_Etiqueta() As DataTable


        Get_Producto_Clasificacion_Etiqueta = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "select Idclasificacion_etiqueta,Descripcion from producto_clasificacion_etiqueta  "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            Get_Producto_Clasificacion_Etiqueta = lDataTable
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

    Public Shared Function Get_Producto_Defecto(ByVal IdBodega As Integer,
                                                ByVal IdPropietario As Integer) As clsBeProducto

        Get_Producto_Defecto = Nothing

        Try

            Dim vSQL As String = "SELECT producto_bodega.IdBodega, producto.*
                                  FROM  producto INNER JOIN
                                  producto_bodega ON producto.IdProducto = producto_bodega.IdProducto 
                                  WHERE IdBodega = @IdBodega AND IdPropietario = @IdPropietario 
                                  AND producto.activo = 1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()
                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False
                            Get_Producto_Defecto = ObjProducto

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

    ''' <summary>
    ''' #EJC20240119: Creada para grid de recepción de becofarma para buscar por código o código de barra.
    ''' </summary>
    ''' <param name="pCodigoProducto"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_Codigo_And_Codigo_Barra(ByVal pCodigoProducto As String) As clsBeProducto

        Get_Single_By_Codigo_And_Codigo_Barra = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto WHERE (codigo=@codigo or codigo_barra = @codigo_barra) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigoProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeProducto()

                            Cargar(ObjProducto, lRow, lConnection, lTransaction)
                            ObjProducto.IsNew = False

                            Get_Single_By_Codigo_And_Codigo_Barra = ObjProducto

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

    Public Shared Function Get_Lista_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                         ByVal pIdBodega As Integer) As DataTable


        Get_Lista_By_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = "SELECT prb.IdProductoBodega,
                                         oc.No_Linea,
                                         pd.codigo AS Codigo,
                                         pd.codigo_barra AS CodigoBarra,
                                         pd.nombre AS Nombre, 
                                         u.Nombre as UMBas,
                                         pd.IdUnidadMedidaBasica as IdUmBas,
                                         pd.costo as Costo,
                                         pd.kit as Kit,
                                         pd.IdProducto,
                                         pd.control_peso as ControlPeso, 
                                         ISNULL(pp.nombre,'') Presentacion
                                  FROM producto AS pd INNER JOIN
                                       unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida INNER JOIN
                                       producto_bodega AS prb ON pd.IdProducto = prb.IdProducto INNER JOIN
                                       propietario_bodega ON pd.IdPropietario = propietario_bodega.IdPropietario AND 
                                                             prb.IdBodega = propietario_bodega.IdBodega INNER JOIN
								       trans_oc_det oc ON oc.IdProductoBodega = prb.IdProductoBodega 
                                       LEFT JOIN producto_presentacion pp ON pp.IdPresentacion = oc.IdPresentacion 
                                  WHERE oc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND 
                                        prb.IdBodega = @IdBodega 
                                  ORDER BY pd.codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Lista_By_IdOrdenCompraEnc = lDataTable
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega(ByVal pIdBodega As Integer,
                                                                                     ByVal pIdPropietarioBodega As Integer,
                                                                                     ByVal pCodigoProducto As String,
                                                                                     ByVal lConnection As SqlConnection,
                                                                                     ByVal lTransaction As SqlTransaction) As Boolean

        Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega = False

        Try

            Dim vSQL As String = String.Format("SELECT COUNT(pb.IdProductoBodega) 
                                                         FROM  dbo.producto_bodega As pb 
                                                         INNER JOIN dbo.producto AS p ON pb.IdProducto = p.IdProducto 
                                                         INNER JOIN propietarios As pr On p.IdPropietario = pr.IdPropietario  
                                                         INNER JOIN propietario_bodega AS ppb ON p.IdPropietario = ppb.IdPropietario  
                                                         LEFT OUTER JOIN dbo.producto_codigos_barra As pcb On p.IdProducto = pcb.IdProducto 
                                                         LEFT OUTER JOIN dbo.producto_presentacion As pp On p.IdProducto = pp.IdProducto 
                                                         WHERE pb.IdBodega={0} And ppb.IdPropietarioBodega={1} And (pb.activo = 1) And (p.activo = 1) And 
                                                         (p.codigo ='{2}') or (p.codigo_barra='{2}') or (pcb.codigo_barra ='{2}') or (pp.codigo_barra ='{2}')", pIdBodega, pIdPropietarioBodega, pCodigoProducto)

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega = CInt(lReturnValue) > 0
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC202470215118: Para recepción por BOF.
    ''' </summary>
    ''' <param name="pCodigo"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_IdProductoBodega_By_Codigo(ByVal pCodigo As String, ByVal pIdBodega As Integer) As Integer

        Get_IdProductoBodega_By_Codigo = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT DISTINCT pb.IdProductoBodega 
                                         FROM producto_bodega AS pb  
                                         INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                                         LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                         ON p.IdProducto = pcb.IdProducto 
                                         LEFT OUTER JOIN producto_presentacion AS pp 
                                         ON p.IdProducto = pp.IdProducto 
                                         WHERE pb.activo = 1 
                                         AND (p.codigo =@Codigo or p.codigo_barra=@Codigo or pcb.codigo_barra =@Codigo or pp.codigo_barra =@Codigo)
                                         AND pb.IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProductoBodega_By_Codigo = CType(lRow("IdProductoBodega"), Integer)

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

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As DataTable


        Get_All_By_IdPropietario = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 "

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_IdPropietario = lDataTable

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Propietario(ByVal pIdPropietario As Integer) As DataTable

        Get_All_By_Propietario = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoOC WHERE 1 > 0 "

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND IdPropietario={0}", pIdPropietario)
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_Propietario = lDataTable

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


    Public Shared Function Get_Tipo_Manufactura_By_CodigoProducto(ByVal pCodigoProducto As String) As Integer

        Get_Tipo_Manufactura_By_CodigoProducto = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdTipoManufactura FROM Producto
                                          Where(codigo = @pCodigoProducto)"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pCodigoProducto", pCodigoProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_Tipo_Manufactura_By_CodigoProducto = CType(lRow("IdTipoManufactura"), Integer)

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

    Public Shared Function Get_Control_Manufactura_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As Boolean

        Get_Control_Manufactura_By_IdProductoBodega = False

        Try

            Dim BeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdTipoManufactura 
                                          FROM producto p inner join producto_bodega pb ON p.IdProducto = pb.IdProducto 
                                          WHERE pb.IdProductoBodega =  @IdProductoBodega  "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("IdTipoManufactura") IsNot DBNull.Value AndAlso lRow("IdTipoManufactura") IsNot Nothing Then
                                BeProducto.IdTipoManufactura = CType(lRow("IdTipoManufactura"), Integer)
                                Get_Control_Manufactura_By_IdProductoBodega = (BeProducto.IdTipoManufactura > 0)
                            End If

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

    Public Shared Function Get_Control_Manufactura_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As Boolean

        Get_Control_Manufactura_By_IdProductoBodega = False

        Try

            Dim BeProducto As New clsBeProducto

            Dim vSQL As String = "SELECT IdTipoManufactura 
                                          FROM producto p inner join producto_bodega pb ON p.IdProducto = pb.IdProducto 
                                          WHERE pb.IdProductoBodega =  @IdProductoBodega  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdTipoManufactura") IsNot DBNull.Value AndAlso lRow("IdTipoManufactura") IsNot Nothing Then
                        BeProducto.IdTipoManufactura = CType(lRow("IdTipoManufactura"), Integer)
                        Get_Control_Manufactura_By_IdProductoBodega = (BeProducto.IdTipoManufactura > 0)
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Busca un producto por código o por licencia.
    ''' </summary>
    ''' <param name="pCodigo">Código o Licencia a traves del cuál se busca el producto</param>
    ''' <param name="IdBodega">IdBodega por el cual se busca el producto</param>
    ''' <returns>devuelve un objeto del tipo clsBeProducto</returns>
    ''' <remarks>ejcalderon_20160519</remarks>
    Public Shared Function Get_BeProducto_By_Codigo_Or_Licencia(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto

        Get_BeProducto_By_Codigo_Or_Licencia = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT top(1) p.*, pb.IdProductoBodega
                                          FROM trans_inv_stock_prod t INNER JOIN 
                                               producto p ON t.IdProducto = p.IdProducto INNER JOIN  
                                               producto_bodega pb ON pb.IdProducto = p.IdProducto
                                               AND pb.IdBodega = t.idbodega LEFT JOIN
											   producto_codigos_barra pc ON pc.IdProducto = p.IdProducto
                                          WHERE t.IdBodega =  @IdBodega  
                                                And ((p.codigo =@Codigo) Or (t.lic_plate=@Codigo) OR (p.codigo_barra =@Codigo)OR
												(pc.codigo_barra = @Codigo)) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_Codigo_Or_Licencia = oBeProducto

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

    Public Shared Function get_Tiene_Bono(ByVal pIdProducto As Integer,
                                          ByVal pConnection As SqlConnection,
                                          ByVal pTransaction As SqlTransaction) As Boolean

        Dim lTieneBono As Boolean = False

        Try

            Dim vSQL As String = "SELECT IdTipoManufactura FROM Producto WHERE IdProducto = @IdProducto"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lTieneBono = CInt(lReturnValue) = 1
                End If

                lCommand.Dispose()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return lTieneBono

    End Function

    Public Shared Function Get_IdProducto_By_Codigo(ByVal pCodigo As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_IdProducto_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto 
                                        FROM producto p
                                        LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                        ON p.IdProducto = pcb.IdProducto 
                                        LEFT OUTER JOIN producto_presentacion AS pp
                                        ON p.IdProducto = pp.IdProducto
                                        WHERE (p.codigo =@Codigo or 
                                        p.codigo_barra=@Codigo or 
                                        pcb.codigo_barra =@Codigo or 
                                        pp.codigo_barra =@Codigo)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdProducto_By_Codigo = CType(lRow("IdProducto"), Integer)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProductoBodega_By_Codigo_And_IdBodega(ByVal pCodigo As String,
                                                                       ByVal IdBodega As Integer) As Integer

        Get_IdProductoBodega_By_Codigo_And_IdBodega = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT DISTINCT pb.IdProductoBodega 
                                         FROM producto_bodega AS pb  
                                         INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                                         LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                         ON p.IdProducto = pcb.IdProducto 
                                         LEFT OUTER JOIN producto_presentacion AS pp 
                                         ON p.IdProducto = pp.IdProducto 
                                         WHERE pb.activo = 1 
                                         and (p.codigo =@Codigo or p.codigo_barra=@Codigo or pcb.codigo_barra =@Codigo or pp.codigo_barra =@Codigo)
                                         AND IdBodega = @IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProductoBodega_By_Codigo_And_IdBodega = CType(lRow("IdProductoBodega"), Integer)

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

    Public Shared Function Get_IdProductoBodega_By_Codigo_And_IdBodega(ByVal pCodigo As String,
                                                                       ByVal IdBodega As Integer,
                                                                       lConnection As SqlConnection,
                                                                       lTransaction As SqlTransaction) As Integer

        Get_IdProductoBodega_By_Codigo_And_IdBodega = 0

        Try

            Dim vSQL As String = "SELECT DISTINCT pb.IdProductoBodega 
                                         FROM producto_bodega AS pb  
                                         INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
                                         LEFT OUTER JOIN producto_codigos_barra AS pcb 
                                         ON p.IdProducto = pcb.IdProducto 
                                         LEFT OUTER JOIN producto_presentacion AS pp 
                                         ON p.IdProducto = pp.IdProducto 
                                         WHERE pb.activo = 1 
                                         and (p.codigo =@Codigo or p.codigo_barra=@Codigo or pcb.codigo_barra =@Codigo or pp.codigo_barra =@Codigo)
                                         AND IdBodega = @IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdProductoBodega_By_Codigo_And_IdBodega = CType(lRow("IdProductoBodega"), Integer)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Bodega_DT(ByVal pIdBodega As String) As DataTable

        Dim lReturnList As New List(Of clsBeProducto_SelectionList)

        Get_All_By_Bodega_DT = Nothing

        Try

            Dim vSQL As String = "SELECT producto.IdProducto, producto.Codigo, producto.codigo_barra as Codigo_Barra, producto.Nombre
                         FROM producto INNER JOIN 
                         producto_bodega ON producto.IdProducto = producto_bodega.IdProducto INNER JOIN 
                         propietario_bodega ON producto_bodega.IdBodega = propietario_bodega.IdBodega AND 
                         producto.IdPropietario = propietario_bodega.IdPropietario 
                         WHERE producto_bodega.IdBodega = @IdBodega 
                         AND producto.Activo=1 AND producto_bodega.Activo =1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_Bodega_DT = lDataTable

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

    '#GT06082024: traer producto que maneje una unidad de cobro o dimensiones para cobrar por variante
    Public Shared Function Get_BeProducto_Cobranza_By_Codigo(ByVal pCodigo As String,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdPropietario As Integer) As clsBeProducto

        Get_BeProducto_Cobranza_By_Codigo = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI 
                                          WHERE IdBodega = @IdBodega
                                            And IdPropietario = @IdPropietario
                                            AND (IdUnidadMedidaCobro > 0 or ((ancho>0) and (largo>0) or (alto>0)))
                                            And ((codigo =@Codigo) Or (codigo_barra=@Codigo) Or (codigo_barra_pcb =@Codigo) Or (codigo_barra_presentacion =@Codigo)) "


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_Cobranza_By_Codigo = oBeProducto

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

    Public Shared Function Get_Single_By_NoParte(ByVal pNoSerie As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_NoParte = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE NoParte=@NoParte"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@NoParte", pNoSerie)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()

                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    Get_Single_By_NoParte = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creé una copia de esta función para agregar filtro por IdPropietario
    ''' Busca un producto por su código, pero es una función más liviana
    ''' </summary>
    ''' <param name="pCodigo">Código a traves del cuál se busca el producto</param>
    ''' <param name="IdBodega">IdBodega por el cual se busca el producto</param>
    ''' <param name="IdPropietario">IdPropietario por el cual se busca el producto</param>
    ''' <returns>devuelve un objeto del tipo clsBeProducto</returns>
    ''' <remarks>CKFK20241016</remarks>
    Public Shared Function Get_BeProducto_By_Codigo_And_IdPropietario(ByVal pCodigo As String,
                                                                       ByVal IdBodega As Integer,
                                                                       ByVal IdPropietario As Integer) As clsBeProducto

        Get_BeProducto_By_Codigo_And_IdPropietario = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT p.*, pb.IdProductoBodega
                                          FROM producto_bodega pb INNER JOIN producto p ON pb.IdProducto = p.IdProducto
                                          WHERE pb.IdBodega = @IdBodega
                                            And p.IdPropietario = @IdPropietario
                                            And p.codigo =@Codigo "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar_Sin_Objetos(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_Codigo_And_IdPropietario = oBeProducto

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

    Public Shared Function Existe_By_NoParte(ByVal pNoParte As String,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef ltransaction As SqlTransaction) As clsBeProducto

        Existe_By_NoParte = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Producto WHERE noparte= @NoParte"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = ltransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@NoParte", pNoParte)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjP As New clsBeProducto()
                    Cargar(ObjP, lRow, lConnection, ltransaction)
                    Existe_By_NoParte = ObjP

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo_Or_NoParte(ByVal pCodigo As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef ltransaction As SqlTransaction) As clsBeProducto

        Existe_By_Codigo_Or_NoParte = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Producto WHERE codigo= @Codigo OR noparte = @Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = ltransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjP As New clsBeProducto()
                    Cargar(ObjP, lRow, lConnection, ltransaction)
                    Existe_By_Codigo_Or_NoParte = ObjP

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProducto_By_NoParte(ByVal pCodigo As String,
                                                     ByVal IdBodega As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_BeProducto_By_NoParte = Nothing

        Dim oBeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI 
                                  WHERE IdBodega = @IdBodega  And (noparte =@Codigo) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Get_BeProducto_By_NoParte = oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT21112024: buscar producto por propietario, bodega y sin stock porque se requiere desde ajustes stock
    Public Shared Function Get_All_Lista_Producto_SinStock(ByVal pIdPropietario As Integer, ByVal pIdPropietarioBodega As Integer, ByVal pIdBodega As Integer, ByVal pActivo As Boolean) As DataTable

        Get_All_Lista_Producto_SinStock = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " SELECT 
                                           pb.IdProductoBodega,
                                            --wp.IdProducto as Correlativo, 
                                           --wp.Propietario, 
                                           --wp.Clasificación,
                                           --wp.Familia, 
                                           --wp.Marca, 
                                           --wp.[Tipo Producto],
                                           --wp.[Unidad Medida], 
                                           wp.Código, 
                                           wp.[Código de Barra],
                                           wp.Producto as Nombre,
                                           --wp.IndiceRotacion,
                                           --wp.Kit as Es_Producto_Kit,
                                           wp.genera_lp,
                                           wp.Control_Vencimiento,
                                           wp.Control_Lote
                                           --wp.producto_parametro_nombreA as [Parámetro A],
                                           --wp.producto_parametro_nombreB as [Parámetro B],
                                           --wp.fec_agr as Fecha_Creacion,
                                           --wp.noparte as NoParte,
                                           --pb.IdProductoBodega,
                                           --ppb.IdPropietarioBodega,
	                                       --st.IdStock 
                                           FROM VW_Producto wp 
                                           INNER JOIN producto_bodega pb ON wp.IdProducto = pb.IdProducto AND pb.IdBodega = @IdBodega
                                           INNER JOIN propietario_bodega ppb ON ppb.IdPropietario = wp.IdPropietario AND ppb.IdBodega = @IdBodega
                                           LEFT JOIN stock st ON st.IdProductoBodega = pb.IdProductoBodega AND st.IdPropietarioBodega = ppb.IdPropietarioBodega
                                           WHERE 1>0 and st.IdPropietarioBodega IS NULL "


                    If pActivo Then
                        vSQL += " AND wp.Activo=1"
                    Else
                        vSQL += " AND wp.Activo=0"
                    End If

                    If pIdPropietario <> 0 Then
                        vSQL += String.Format(" AND wp.IdPropietario={0}", pIdPropietario)
                    End If

                    vSQL += " ORDER BY Código"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.CommandTimeout = 300
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_Lista_Producto_SinStock = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeProducto_By_IdProducto(ByVal pIdProducto As String, ByVal IdBodega As Integer) As clsBeProducto

        Get_BeProducto_By_IdProducto = Nothing

        Try

            Dim oBeProducto As New clsBeProducto

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                                     " WHERE IdBodega = @IdBodega " &
                                     " And (IdProducto=@IdProducto) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Cargar(oBeProducto, lRow, lConnection, lTransaction)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            oBeProducto.IsNew = False

                            Get_BeProducto_By_IdProducto = oBeProducto

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

    Public Shared Function Get_IdProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_IdProducto_By_IdProductoBodega = 0

        Try

            Dim vSQL As String = "SELECT p.IdProducto
            FROM producto_bodega AS pb  
            INNER JOIN producto AS p ON p.IdProducto = pb.IdProducto 
            WHERE pb.activo = 1 
            and (pb.IdProductoBodega =@IdProductoBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdProducto_By_IdProductoBodega = CType(lRow("IdProducto"), Integer)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#CKFK20250110: Función Get_BeProducto_By_CodigoHH con transacción para el cambio de ubicacion en la HH donde solo debe buscar por el código
    Public Shared Function Get_BeProducto_By_CodigoHH(ByVal pCodigo As String,
                                                      ByVal IdBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_BeProducto_By_CodigoHH = Nothing

        Dim oBeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                   " WHERE IdBodega = @IdBodega And codigo =@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Get_BeProducto_By_CodigoHH = oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#CKFK20250110: Función Get_BeProducto_By_Only_Codigo con transacción para  en la HH donde solo debe buscar por el código
    Public Shared Function Get_BeProducto_By_Only_Codigo(ByVal pCodigo As String,
                                                         ByVal IdBodega As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As clsBeProducto

        Get_BeProducto_By_Only_Codigo = Nothing

        Dim oBeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoSI  " &
                   " WHERE IdBodega = @IdBodega And codigo =@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(oBeProducto, lRow, lConnection, lTransaction)

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    oBeProducto.IsNew = False

                    Get_BeProducto_By_Only_Codigo = oBeProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_NoSerie(ByVal pNoSerie As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_NoSerie = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE NoSerie=@NoSerie"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@NoSerie", pNoSerie)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjProducto As New clsBeProducto()

                    Cargar(ObjProducto, lRow, lConnection, lTransaction)
                    ObjProducto.IsNew = False
                    Get_Single_By_NoSerie = ObjProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_List_Product_By_CodigoBarra_By_PickingEnc_And_Pedido(ByVal pCodigoBarra As String,
                                                                                    ByVal pIdBodega As Integer,
                                                                                    ByVal pIdPickingEnc As Integer,
                                                                                    ByVal pIdPedidoEnc As Integer) As List(Of clsBeProducto)

        Get_List_Product_By_CodigoBarra_By_PickingEnc_And_Pedido = Nothing

        Try

            Dim vSQL As String = " SELECT DISTINCT IdBodega, IdProductoBodega, IdProducto, IdPropietario, IdClasificacion, IdFamilia, IdMarca, 
                                         IdTipoProducto, IdUnidadMedidaBasica, IdCamara, IdTipoRotacion, IdPerfilSerializado, IdIndiceRotacion, 
                                         IdSimbologia, IdArancel, codigo, nombre, codigo_barra, precio, existencia_min, existencia_max, costo, 
                                         peso_referencia, peso_tolerancia, noparte, noserie, control_peso, ciclo_vida, tolerancia, kit, materia_prima, 
                                         control_lote, control_vencimiento, genera_lote, serializado, '' codigo_barra_presentacion, '' codigo_barra_pcb, 
                                         '' NomPresentacion, activopp, 0 IdPresentacion, 0 factor, peso_recepcion, peso_despacho, temperatura_referencia, 
                                         temperatura_tolerancia, temperatura_recepcion, temperatura_despacho, fechamanufactura, capturar_aniada, 
                                         Arancel, user_agr, fec_agr, user_mod, fec_mod, captura_arancel, es_hardware, activo, imagen, largo, ancho,  
                                         alto, genera_lp_old, IdUnidadMedidaCobro, IdTipoEtiqueta, dias_inventario_promedio, IdProductoParametroA, 
                                         IdProductoParametroB, IdTipoManufactura, margen_impresion
                                         FROM VW_ProductoSI  
                                         WHERE (IdBodega = @IdBodega) and 
                                         (codigo_barra_pcb = @CodigoBarra or 
                                         codigo_barra_presentacion = @CodigoBarra or 
                                         codigo_barra = @CodigoBarra or 
                                         codigo = @CodigoBarra) AND
                                         IdProductoBodega IN 
                                         (SELECT IdProductoBodega 
                                                 FROM trans_picking_ubic 
                                                 WHERE IdPickingEnc = @IdPickingEnc AND IdPedidoEnc = @IdPedidoEnc) "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBarra", pCodigoBarra)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim oBeProducto As New clsBeProducto()
                            Dim lBeProducto As New List(Of clsBeProducto)
                            Get_List_Product_By_CodigoBarra_By_PickingEnc_And_Pedido = New List(Of clsBeProducto)

                            For Each lRow In lDT.Rows

                                oBeProducto = New clsBeProducto

                                Cargar(oBeProducto, lRow, lConnection, lTransaction)

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    oBeProducto.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                oBeProducto.IsNew = False

                                lBeProducto.Add(oBeProducto)

                            Next

                            If Not lBeProducto Is Nothing Then

                                lBeProducto = lBeProducto.Distinct.ToList()
                                Get_List_Product_By_CodigoBarra_By_PickingEnc_And_Pedido = lBeProducto

                            End If

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

    Public Shared Function Get_Lista_By_IdOrdenCompraEnc_Talla_Color(ByVal pIdOrdenCompraEnc As Integer,
                                                                    ByVal pIdBodega As Integer) As DataTable


        Get_Lista_By_IdOrdenCompraEnc_Talla_Color = Nothing

        Try

            Dim vSQL As String = "SELECT prb.IdProductoBodega, oc.No_Linea, pd.codigo AS Codigo, pd.codigo_barra AS CodigoBarra, pd.nombre AS Nombre, u.Nombre AS UMBas, pd.IdUnidadMedidaBasica AS IdUmBas, pd.costo AS Costo, pd.kit AS Kit, pd.IdProducto, pd.control_peso AS ControlPeso, ISNULL(pp.nombre, '') 
                                    AS Presentacion, producto_talla_color.CodigoSKU as SKU, talla.Codigo AS Talla, color.Codigo AS Color
                                    FROM producto AS pd INNER JOIN
                                    unidad_medida AS u ON pd.IdUnidadMedidaBasica = u.IdUnidadMedida INNER JOIN
                                    producto_bodega AS prb ON pd.IdProducto = prb.IdProducto INNER JOIN
                                    propietario_bodega ON pd.IdPropietario = propietario_bodega.IdPropietario AND prb.IdBodega = propietario_bodega.IdBodega INNER JOIN
                                    trans_oc_det AS oc ON oc.IdProductoBodega = prb.IdProductoBodega INNER JOIN
                                    producto_talla_color ON oc.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor INNER JOIN
                                    talla ON producto_talla_color.IdTalla = talla.IdTalla INNER JOIN
                                    color ON producto_talla_color.IdColor = color.IdColor LEFT OUTER JOIN
                                    producto_presentacion AS pp ON pp.IdPresentacion = oc.IdPresentacion
                                  WHERE oc.IdOrdenCompraEnc = @IdOrdenCompraEnc AND 
                                        prb.IdBodega = @IdBodega 
                                  ORDER BY pd.codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Lista_By_IdOrdenCompraEnc_Talla_Color = lDataTable
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_Activo(ByVal pUltimaFechaSincro As Date, ByVal pActivo As Boolean, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto)

        Get_All_By_Activo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM producto WHERE (activo=@pActivo and fec_mod>=@pUltimaFechaSincro) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pActivo", pActivo)
                lDTA.SelectCommand.Parameters.AddWithValue("@pUltimaFechaSincro", pUltimaFechaSincro)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim oBeProducto As New clsBeProducto()
                    Dim lBeProducto As New List(Of clsBeProducto)

                    For Each lRow In lDT.Rows

                        oBeProducto = New clsBeProducto
                        Cargar(oBeProducto, lRow, lConnection, lTransaction)
                        oBeProducto.IsNew = False
                        lBeProducto.Add(oBeProducto)
                    Next

                    If Not lBeProducto Is Nothing Then
                        lBeProducto = lBeProducto.Distinct.ToList()
                        Get_All_By_Activo = lBeProducto
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdProducto(ByVal pIdProducto As Integer, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdProducto = ""

        Try

            Dim vSQL As String = "SELECT p.Codigo
                        FROM producto p                        
                        WHERE (p.IdProducto=@IdProducto)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_Codigo_By_IdProducto = CType(lRow("Codigo"), String)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class