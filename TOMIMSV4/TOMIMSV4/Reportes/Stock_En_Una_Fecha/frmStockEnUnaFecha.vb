Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMov_Reporte

    Private DT As New DataTable("StockEnUnaFecha")
    Public pBeListaProductos As New List(Of Integer)
    Public ListaMovimientos As New List(Of clsBeVW_Movimientos)
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Class clsStockEnUnaFechaRep

        Public Property Codigo As String = ""
        Public Property Nombre As String = ""
        Public Property Lote As String = ""
        Public Property FechaVence As Date = Now
        Public Property InventarioInicial As Double = 0
        Public Property Ingresos As Double = 0
        Public Property Ajustes_Positivos As Double = 0
        Public Property Ajustes_Negativos As Double = 0
        Public Property Salidas As Double = 0
        Public Property Existencia_Teorica As Double = 0
        Public Property Existencia_Actual As Double = 0
        Public Property Diferencia As Double = 0

    End Class

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

    Public Function Get_Lista_Movimientos() As Boolean

        Get_Lista_Movimientos = False
        Dim clsTransaccion As New clsTransaccion()

        Try

            Dim IdProductoBodega As Integer = 0

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)
            End If

            ListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value,
                                                                                        dtpfechaHasta.Value,
                                                                                        IdProductoBodega,
                                                                                        cmbBodega.EditValue,
                                                                                        cmbPropietario.EditValue,
                                                                                        txtLote.Text.Trim())

            If Not ListaMovimientos Is Nothing Then
                Get_Lista_Movimientos = ListaMovimientos.Count > 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Generar_Reporte()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1

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
                    'For Each ObjM In ListaMovimientos.Where(Function(x) x.TipoTarea <> clsDataContractDI.tTipoTarea.UBIC).OrderBy(Function(x) x.Codigo)

                    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.EstadoOrigen)

                        lblPrg.Text = "Procesando movimiento para producto: " & ObjM.Codigo
                        lblPrg.Refresh()

                        If ObjM.Fecha_Vence = TheGoalDate Then
                            Debug.Print("Wait a second!")
                        End If

                        If ObjM.Fecha_Vence = TheGoalDate AndAlso ObjM.EstadoOrigen = "SIN REGISTRO" AndAlso ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
                            Debug.Print("Wait a second!")
                        End If

                        If ObjM.Codigo = "030772033524" Then
                            Debug.Print("Wait a second!")
                        End If

                        BeStockEnFecha = New clsBeStockEnUnaFecha()
                        BeStockEnFecha.Codigo = ObjM.Codigo
                        BeStockEnFecha.Producto = ObjM.Producto
                        BeStockEnFecha.IdEstadoOrigen = ObjM.IdEstadoOrigen
                        BeStockEnFecha.Fecha_Vence = ObjM.Fecha_Vence
                        BeStockEnFecha.IdUnidadMedida = ObjM.IdUnidadMedida
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
                            BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
                            BeStockEnFecha.Ingresos += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI Then
                            BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI Then
                            BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
                            BeStockEnFecha.Salidas += ObjM.Cantidad
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

                    ExistenciasAl = ((Obj.Inventario_Inicial + Obj.Ingresos + Obj.AjustePositivo) - (Obj.AjusteNegativo + Obj.Salidas))

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


                Restore_LayOut_Grid()

                GridView1.OptionsView.ShowFooter = True

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

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White

            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmStockEnUnaFecha_Load(sender As Object, e As EventArgs) Handles Me.Load

        '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
        vNombreArchivoLayOutGrid = "frmStockEnUnaFecha.xml"

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

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            dgrid.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
            End If

            Generar_Reporte()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub frmStockEnUnaFecha_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en grdStockPorLote - frmstockPorLote_posicion.
            'vNombreArchivoLayOutGrid = "grdStockEnUnaFecha.xml"

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            Dim View As GridView = sender
            Dim Dif As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Diferencia"))

            If e.Column.FieldName = "Diferencia" Then

                If Val(Dif) = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                Else
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try

            Imprimir_Vista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Stock en una fecha " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_Stock_En_Una_Fecha_Del_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dgrid.ExportToXlsx(myStream)
                    myStream.Close()
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

    End Sub

    Private Sub dgrid_DoubleClick(sender As Object, e As EventArgs) Handles dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim CodigoProducto As String
                Dim Lote As String
                Dim Diferencia As Double = 0

                CodigoProducto = Dr.Item("Código")
                Lote = Dr.Item("Lote")
                Diferencia = Dr.Item("Diferencia")

                If XtraMessageBox.Show(String.Format("¿Ir a detalle de existencias?" & vbNewLine & "Código: " & CodigoProducto & vbNewLine & "Lote: " & Lote & vbNewLine & "Diferencia: " & Diferencia),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    With frmListStockSeek
                        .Modo = frmStockSeek.TipoTrans.Editar
                        .rpCodigoProducto = CodigoProducto
                        .rpLote = Lote
                        .Diferencia = Diferencia
                        .IdBodega = cmbBodega.EditValue
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmStockEnUnaFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.D Then
            ModoDepuracion = True
            MsgBox("Modo depuración activado, tenga cuidado...", MsgBoxStyle.Information, Text)
        End If

    End Sub

    Private Sub Guardar_Layout()

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
    '    Guardar_Layout()
    'End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick
        Try


            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub
End Class