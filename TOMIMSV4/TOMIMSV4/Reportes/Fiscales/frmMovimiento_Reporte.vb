Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimiento_Reporte


    Private DT As New DataTable("StockEnUnaFecha")
    Public pBeListaProductos As New List(Of Integer)
    Public ListaMovimientos As New List(Of clsBeVW_MovimientosRetroactivo)
    'Public ListaMovimientos_Reporte As New List(Of clsBeVW_Movimientos)
    Public ListaMovimientos_Reporte As New List(Of clsBeVW_MovimientosRetroactivo)
    Public ListaMovimientos_Reporte2 As New List(Of clsBeVW_MovimientosRetroactivo)
    Public Lista_Movimientos_Inv_Inicial As New List(Of clsBeVW_MovimientosRetroactivo)
    Public Lista_Movimientos_Rango_Fecha As New List(Of clsBeVW_MovimientosRetroactivo)


    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMovimiento_Reporte_Load(sender As Object, e As EventArgs) Handles Me.Load

        'AP.Listar_Bodegas_By_Usuario(cmbBodega)
        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Try

            SetDatataTable()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub Generar_Reporte()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1
            Dim Idx2 As Integer = -1

            ListaMovimientos.Clear()
            RepMovEnUnaFecha.Clear()
            lblPrg.Visible = True

            If Get_Lista_Movimientos() Then

                RepMovEnUnaFecha.Clear()

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = ListaMovimientos.Count
                prg.Properties.Minimum = 0

                If Not ListaMovimientos Is Nothing Then

                    Dim TheGoalDate As Date = New Date(2019, 8, 30)

                    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.EstadoOrigen)
                        'For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.Fecha)

                        lblPrg.Text = "Procesando movimiento para producto: " & ObjM.Codigo
                        lblPrg.Refresh()

                        If ObjM.Fecha_Vence = TheGoalDate Then
                            Debug.Print("Wait a second!")
                        End If

                        If ObjM.Fecha_Vence = TheGoalDate AndAlso ObjM.EstadoOrigen = "SIN REGISTRO" AndAlso ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
                            Debug.Print("Wait a second!")
                        End If

                        BeStockEnFecha = New clsBeStockEnUnaFecha()
                        BeStockEnFecha.Codigo = ObjM.Codigo
                        BeStockEnFecha.Producto = ObjM.Producto
                        BeStockEnFecha.IdEstadoOrigen = ObjM.IdEstadoOrigen
                        BeStockEnFecha.Fecha_Vence = ObjM.Fecha_Vence
                        BeStockEnFecha.IdUnidadMedida = ObjM.IdUnidadMedida
                        BeStockEnFecha.IdMovimiento = ObjM.IdMovimiento
                        clsPublic.CopyObject(ObjM, BeStockEnFecha)

                        Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                         AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                                         AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)


                        If Idx <> -1 Then 'Lo encontró por lote.

                            Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                              AndAlso x.Lote = BeStockEnFecha.Lote _
                              AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)


                            If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
                                '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
                                Debug.Print("Espera")
                                'Magia por EJC para corregir cagada.
                                If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
                                    'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
                                    Debug.Print(BeStockEnFecha.Codigo)
                                End If
                            End If

                            'Si no tiene contro por lote...
                            If BeStockEnFecha.Lote = "" Then

                                Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                                  AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)

                                If Idx1 = -1 Then 'No coincide la fecha de vencimiento.. no pasa nada
                                    Idx = Idx1
                                Else
                                    Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                                      AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date _
                                                                      AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen)

                                    If Idx1 = -1 Then 'No coincide el estado
                                        Idx = Idx1
                                    Else
                                        Idx = Idx1
                                    End If

                                End If

                            End If

                        Else

                            Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                             AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)


                            If Idx <> -1 Then 'Lo encontró por FechaVence.

                                Debug.Print(BeStockEnFecha.Codigo)

                                If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
                                    'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
                                    Debug.Print(BeStockEnFecha.Codigo)
                                End If

                            End If

                        End If



                        If Idx = -1 Then
                            RepMovEnUnaFecha.Add(BeStockEnFecha)
                        Else
                            BeStockEnFecha = RepMovEnUnaFecha(Idx) 'Puntero =>
                        End If



                        If ObjM.TipoTarea = clsDataContractDI.tTipoTarea.INVE Then

                            If BeStockEnFecha.Codigo = ObjM.Codigo And BeStockEnFecha.Cantidad = 0 Then
                                BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
                            End If

                            'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
                            BeStockEnFecha.Ingresos += ObjM.Cantidad

                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI Then
                            BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI Then
                            BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
                            'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then

                            BeStockEnFecha.Salidas += ObjM.Salidas
                        Else
                            Debug.Print(ObjM.TipoTarea)
                        End If

                        Debug.Print(ObjM.TipoTarea)

                        prg.PerformStep()

                        Application.DoEvents()

                    Next

                End If

                Llena_Grid()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            prg.Visible = False
        End Try

    End Sub

    Public Function Get_Lista_Movimientos() As Boolean

        Get_Lista_Movimientos = False
        Dim IdProductoBodega As Integer = 0
        Dim clsTransaccion As New clsTransaccion
        'clsTransaccion.Begin_Transaction()
        ListaMovimientos = New List(Of clsBeVW_MovimientosRetroactivo)
        ListaMovimientos_Reporte = New List(Of clsBeVW_MovimientosRetroactivo)
        ListaMovimientos_Reporte2 = New List(Of clsBeVW_MovimientosRetroactivo)

        Try

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)

                'ListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value,
                '                                                                            dtpfechaHasta.Value,
                '                                                                            IdProductoBodega,
                '                                                                            cmbBodega.EditValue,
                '                                                                            cmbPropietario.EditValue)
            Else

                If ProductoEspecifico Is Nothing OrElse ProductoEspecifico.IdProducto = 0 Then
                    lblPrg.Text = "Obteniendo listado de productos"
                    lblPrg.Refresh()


                    'GT 09042021 trae lista de productos en stock_jornada
                    'Lista_Movimientos_Inv_Inicial = clsLnStock_jornada.Get_Movimientos_Inv_Inicial(cmbPropietario.EditValue, dtpfechaHasta.Value)


                    'GT 08042021 trae mov de propietario y productos con ticket. La fecha es de recepcion/ticket
                    ListaMovimientos_Reporte = clsLnTrans_movimientos.Get_All_Movimientos_Reporte_By_IdProducto(
                                                                                            dtpfechaHasta.Value,
                                                                                            cmbBodega.EditValue,
                                                                                           cmbPropietario.EditValue)
                    'GT 9042021 pruebo tomar la lista de reporte como el inv inicial
                    For Each producto In ListaMovimientos_Reporte
                        producto.TipoTarea = 6
                        ListaMovimientos_Reporte2.Add(producto)
                    Next

                    For Each linea In ListaMovimientos_Reporte

                        'ListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_Rango_Fechas(dtpFechaDesde.Value, dtpfechaHasta.Value, cmbPropietario.EditValue)
                        Lista_Movimientos_Rango_Fecha = clsLnTrans_movimientos.Get_All_Movimientos_Reporte_By_Rango_Fechas(linea.Fecha, dtpfechaHasta.Value, linea.Codigo, cmbPropietario.EditValue)

                        For Each resultado In Lista_Movimientos_Rango_Fecha

                            Dim repetido = ListaMovimientos.FindIndex(Function(x) x.IdMovimiento = resultado.IdMovimiento)

                            If repetido <> 1 Then
                                ListaMovimientos.Add(resultado)
                            End If

                        Next
                    Next

                    For Each fila In ListaMovimientos_Reporte2

                        ListaMovimientos.Add(fila)

                    Next

                End If

            End If

            If Not ListaMovimientos Is Nothing Then
                Get_Lista_Movimientos = ListaMovimientos.Count > 0
            End If

            'clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            'clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            'clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Sub Llena_Grid()

        Dim clsTransaccion As New clsTransaccion
        Dim ExistenciasAl As Double
        Dim ExistenciaActualConFechaYEstado As Double
        Dim Diferencia As Double
        Dim pBeStock As New clsBeStock
        Dim pBeStockSinEstado As New clsBeStock
        Dim BeDiferencia As New clsBeDiferencias_movimientos
        Dim vIdDiferencia As Integer = 0
        Dim vExistenciaSinEstado As Double = 0
        Dim lMovimientos As New List(Of clsBeTrans_movimientos)

        Try

            clsTransaccion.Begin_Transaction()

            Dim Lista = From i In RepMovEnUnaFecha Group i By Keys = New With {Key i.IdProductoBodega,
                                                                                Key i.Codigo,
                                                                                Key i.Producto,
                                                                                Key i.UMBas,
                                                                                Key i.EstadoOrigen,
                                                                                Key i.IdEstadoOrigen,
                                                                                Key i.IdPresentacion,
                                                                                Key i.IdUnidadMedida,
                                                                                Key i.Lote,
                                                                                Key i.Fecha_Vence} Into Group
                        Select New With {Keys.Codigo,
                                        Keys.Producto,
                                        Keys.UMBas,
                                        Keys.IdEstadoOrigen,
                                        Keys.Lote,
                                        Keys.EstadoOrigen,
                                        Keys.Fecha_Vence,
                                        Keys.IdUnidadMedida,
                                        Keys.IdPresentacion,
                                        Keys.IdProductoBodega,
                                        .AjusteNegativo = Group.Sum(Function(x) x.Ajuste_Negativo),
                                        .AjustePositivo = Group.Sum(Function(x) x.Ajuste_Positivo),
                                        .Ingresos = Group.Sum(Function(x) x.Ingresos),
                                        .Salidas = Group.Sum(Function(x) x.Salidas),
                                        .Inventario_Inicial = Group.Sum(Function(x) x.Inventario_Inicial)}

            dgrid.DataSource = Nothing

            If Lista IsNot Nothing AndAlso Lista.Count > 0 Then

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = Lista.Count
                prg.Properties.Minimum = 0
                prg.EditValue = 0

                DT.Rows.Clear()

                If ModoDepuracion Then

                    vIdDiferencia = clsLnDiferencias_movimientos.MaxID() + 1

                    clsLnDiferencias_movimientos.EliminarTodos()

                End If

                For Each Obj In Lista

                    lblPrg.Text = "Calculando inventarios para producto: " & Obj.Codigo
                    lblPrg.Refresh()


                    'ExistenciasAl = ((Obj.Inventario_Inicial + Obj.Ingresos + Obj.AjustePositivo) - (Obj.AjusteNegativo + Obj.Salidas))
                    ExistenciasAl = ((Obj.Ingresos + Obj.AjustePositivo) - (Obj.AjusteNegativo + Obj.Salidas))

                    pBeStock.IdProductoBodega = Obj.IdProductoBodega
                    pBeStock.ProductoEstado = New clsBeProducto_estado
                    pBeStock.ProductoEstado.IdEstado = Obj.IdEstadoOrigen
                    pBeStock.IdProductoEstado = Obj.IdEstadoOrigen
                    pBeStock.Presentacion = New clsBeProducto_Presentacion
                    pBeStock.IdPresentacion = Obj.IdPresentacion
                    pBeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    pBeStock.IdUnidadMedida = Obj.IdUnidadMedida
                    pBeStock.Fecha_vence = Obj.Fecha_Vence
                    pBeStock.Lote = Obj.Lote
                    pBeStock.IsReportStockEnFecha = True

                    If Obj.IdPresentacion <> 0 Then
                        ExistenciasAl = Math.Round(ExistenciasAl / pBeStock.Presentacion.Factor, 6)
                        Obj.Ingresos = Math.Round(Obj.Ingresos / pBeStock.Presentacion.Factor, 6)
                        Obj.Salidas = Math.Round(Obj.Salidas / pBeStock.Presentacion.Factor, 6)

                    End If

                    clsPublic.CopyObject(pBeStock, pBeStockSinEstado)

                    clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                 cmbBodega.EditValue,
                                                                 True,
                                                                 True,
                                                                 0,
                                                                 False,
                                                                 clsTransaccion.lConnection,
                                                                 clsTransaccion.lTransaction)

                    ExistenciaActualConFechaYEstado = pBeStock.Cantidad

                    clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStockSinEstado,
                                                                cmbBodega.EditValue,
                                                                False,
                                                                True,
                                                                0,
                                                                False,
                                                                clsTransaccion.lConnection,
                                                                clsTransaccion.lTransaction)

                    vExistenciaSinEstado = pBeStockSinEstado.Cantidad

                    If Obj.IdPresentacion <> 0 Then
                        vExistenciaSinEstado = Math.Round(vExistenciaSinEstado / pBeStock.Presentacion.Factor, 6)
                    End If

                    BeDiferencia = New clsBeDiferencias_movimientos()

                    If ExistenciasAl > 0 Then

                        Diferencia = Math.Round(ExistenciaActualConFechaYEstado, 3) - Math.Round(ExistenciasAl, 3)

                        If ExistenciasAl > ExistenciaActualConFechaYEstado Then
                            BeDiferencia.FaltaStock = True
                        End If

                    Else
                        Diferencia = Math.Round(ExistenciaActualConFechaYEstado, 3) + Math.Round(ExistenciasAl, 3)
                    End If

                    If Diferencia <> 0 Then

                        Dim vDiferenciaExistencia As Double = vExistenciaSinEstado - ExistenciaActualConFechaYEstado

                        If vDiferenciaExistencia > 0 Then
                            Diferencia += vDiferenciaExistencia
                        End If

                    End If

                    If Diferencia <> 0 AndAlso ModoDepuracion Then

                        If ExistenciasAl < 0 AndAlso ExistenciaActualConFechaYEstado = 0 Then

                            lMovimientos = clsLnTrans_movimientos.Get_Movimiento_Despacho_By_Stock(pBeStock)

                            If Not lMovimientos Is Nothing Then

                                If lMovimientos.Count > 0 Then

                                    For Each M In lMovimientos

                                        If Diferencia <> 0 Then

                                            Debug.Print("IdMovimiento: " & M.IdMovimiento & " " & M.Cantidad)

                                            If M.Cantidad >= Math.Abs(Diferencia) Then
                                                M.Cantidad += Diferencia
                                                Diferencia += 1
                                            Else
                                                M.Cantidad = 0
                                                Diferencia += M.Cantidad
                                            End If

                                            M.Serie = "#EJCAJUSTEDESFASE"

                                            If M.Cantidad = 0 Then
                                                clsLnTrans_movimientos.Eliminar(M)
                                            Else
                                                clsLnTrans_movimientos.Actualizar(M)
                                            End If

                                            lblPrg.Text = "Ajustando: " & Obj.Codigo & " Cantidad: " & Diferencia
                                            lblPrg.Refresh()

                                        End If

                                    Next

                                End If

                            End If

                        End If

                    End If


                    If Diferencia <> 0 AndAlso ModoDepuracion Then

                        Debug.Print("Why difference is Not 0? at: " & Obj.Codigo)

                        vExistenciaSinEstado = pBeStock.Cantidad

                        BeDiferencia.IdDiferencia = vIdDiferencia
                        BeDiferencia.IdProductoBodega = Obj.IdProductoBodega
                        BeDiferencia.Codigo = Obj.Codigo
                        BeDiferencia.Nombre = Obj.Producto
                        BeDiferencia.Lote = Obj.Lote
                        BeDiferencia.IdProductoEstado = Obj.IdEstadoOrigen
                        BeDiferencia.Estado = Obj.EstadoOrigen
                        BeDiferencia.FechaVence = Obj.Fecha_Vence
                        BeDiferencia.InventarioInicial = Obj.Inventario_Inicial
                        BeDiferencia.Ingresos = Obj.Ingresos
                        BeDiferencia.AjustesPositivos = Obj.AjustePositivo
                        BeDiferencia.AjustesNegativos = Obj.AjusteNegativo
                        BeDiferencia.Salidas = Obj.Salidas
                        BeDiferencia.ExistenciaAl = ExistenciasAl
                        BeDiferencia.ExistenciaActual = ExistenciaActualConFechaYEstado
                        BeDiferencia.ExistenciaSinEstado = vExistenciaSinEstado
                        BeDiferencia.Diferencia = Diferencia
                        clsLnDiferencias_movimientos.Insertar(BeDiferencia)

                        vIdDiferencia += 1

                    End If

                    DT.Rows.Add(Obj.Codigo,
                                Obj.Producto,
                                Obj.Lote,
                                Obj.EstadoOrigen,
                                Obj.Fecha_Vence,
                                Obj.Inventario_Inicial,
                                Obj.Ingresos,
                                Obj.AjustePositivo,
                                Obj.AjusteNegativo,
                                Obj.Salidas,
                                ExistenciasAl,
                                ExistenciaActualConFechaYEstado,
                                Diferencia)

                    Application.DoEvents()

                    prg.PerformStep()

                Next

                clsTransaccion.Commit_Transaction()

                dgrid.DataSource = DT

                GridView1.OptionsView.ShowFooter = True

                'GT quite agrupación para ver que pasa
                GridView1.Columns("Código").Group()

                DT.Columns("Existencia_Al").Caption = "Existencia_Al: " & dtpfechaHasta.Value.Date

                Dim item0 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Inventario_Inicial",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Inv_Ini: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Inventario_Inicial")}
                GridView1.GroupSummary.Add(item0)

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ingresos",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Ingresos: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ingresos")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ajustes_P",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ajustes_P")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ajustes_N",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Ajustes_N: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ajustes_N")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Salidas: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Salidas")}
                GridView1.GroupSummary.Add(item3)

                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Diferencia",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Dif: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Diferencia")}
                GridView1.GroupSummary.Add(item4)

                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Existencia_Al",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Sum: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Existencia_Al")}
                GridView1.GroupSummary.Add(item5)

                Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Existencia_Actual",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Sum: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Existencia_Actual")}
                GridView1.GroupSummary.Add(item6)

                GridView1.Columns("Ingresos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ingresos").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Ingresos").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ingresos").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Inventario_Inicial").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Inventario_Inicial").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Inventario_Inicial").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Inventario_Inicial").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Ajustes_P").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ajustes_P").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Ajustes_P").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ajustes_P").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Ajustes_N").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ajustes_N").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Ajustes_N").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ajustes_N").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Salidas").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Al").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Al").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Al").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Al").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Actual").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Actual").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:n6}"

                GridView1.ExpandAllGroups()

                GridView1.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
            lblPrg.Visible = False
            clsTransaccion.Close_Conection()
        End Try

    End Sub


    Private Sub SetDatataTable()

        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Nombre", GetType(String))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("Inventario_Inicial", GetType(Double))
        DT.Columns.Add("Ingresos", GetType(Double))
        DT.Columns.Add("Ajustes_P", GetType(Double))
        DT.Columns.Add("Ajustes_N", GetType(Double))
        DT.Columns.Add("Salidas", GetType(Double))
        DT.Columns.Add("Existencia_Al", GetType(Double))
        DT.Columns.Add("Existencia_Actual", GetType(Double))
        DT.Columns.Add("Diferencia", GetType(Double))

    End Sub

End Class