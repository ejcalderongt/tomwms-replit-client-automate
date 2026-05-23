Imports System.ComponentModel
Imports System.Net
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGauges.Win
Imports DevExpress.XtraGauges.Win.Gauges.Digital
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.clsLnProducto

Public Class frmPrincipal02

    Private DTTareas As New DataTable("dtTareas")
    Public Call_Listar_Tareas As New MethodInvoker(AddressOf Actualizar_Tareas)
    'Public Call_Set_Indicador_Ocupacion_Bodega As New MethodInvoker(AddressOf Set_Indicador_Ocupacion_Bodega)
    Public ReadOnly Call_Set_Indicador_Ocupacion_Area_Tipo As Action(Of Integer, String) = AddressOf Set_Indicador_Ocupacion_Por_Area
    Public Call_Listar_Reabastecimiento_Productos As New MethodInvoker(AddressOf Listar_Reabastecimiento_Productos)
    'Public Call_Set_Indicador_Gen As New MethodInvoker(AddressOf Set_Indicador_Ocupacion_Bodega)
    Public Property IsInitialized As Boolean = False
    Public Property lOperadoresByBodega As New List(Of clsBeOperador_bodega)
    Private Property EstoyOcupaoChico As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private WithEvents bgWorker As New System.ComponentModel.BackgroundWorker
    Private Sub Init_DT()

        DTTareas = New DataTable("dtTareas")
        DTTareas.Columns.Add("IdTransaccion", GetType(Integer))
        DTTareas.Columns.Add("Tarea", GetType(String))
        DTTareas.Columns.Add("Inicio", GetType(Date))
        DTTareas.Columns.Add("Ult_Revision", GetType(Date))
        DTTareas.Columns.Add("TTM", GetType(Double))
        DTTareas.Columns.Add("Propietario", GetType(String))
        DTTareas.Columns.Add("Estado", GetType(String))
        DTTareas.Columns.Add("IdTareaHH", GetType(Integer))
        DTTareas.Columns.Add("Origen", GetType(String))
        DTTareas.Columns.Add("Destino", GetType(String))
        DTTareas.Columns.Add("Progreso", GetType(Double))
        DTTareas.Columns.Add("Observacion", GetType(String))

    End Sub

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Init_DT()

        DgridTareas.DataSource = DTTareas

        Try

            Set_Datata_Table_Grid_Rellenado() : Set_Columnas_Grid_Rellenado()
            Set_Datata_Table_Grid_TareasPendientesReabasto()

            dgridRellenado.DataSource = DTGridDetalleRellenado
            dgridTareasPendientesReabasto.DataSource = DTGridTareasPendientesReabasto

            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsInitialized = True
        End Try

    End Sub

#Region " Revision de Productos en Mínimo "

    Private DTGridDetalleRellenado As New DataTable("DetalleRellenado")

    Private DTGridTareasPendientesReabasto As New DataTable("TareasPendientesReabasto")
    Private Sub Set_Datata_Table_Grid_TareasPendientesReabasto()

        DTGridTareasPendientesReabasto.Columns.Clear()
        DTGridTareasPendientesReabasto.Columns.Add("IdTarea", GetType(Integer))
        DTGridTareasPendientesReabasto.Columns.Add("Codigo", GetType(String)) '1
        DTGridTareasPendientesReabasto.Columns.Add("IdUbicacion", GetType(String))
        DTGridTareasPendientesReabasto.Columns.Add("Operador", GetType(String))

    End Sub

    Private Sub Set_Datata_Table_Grid_Rellenado()

        DTGridDetalleRellenado.Columns.Clear()
        DTGridDetalleRellenado.Columns.Add("Seleccionar", GetType(Boolean)) '0
        DTGridDetalleRellenado.Columns.Add("Codigo", GetType(String)) '1
        DTGridDetalleRellenado.Columns.Add("Nombre", GetType(String)) '2
        DTGridDetalleRellenado.Columns.Add("UMBas", GetType(String)) '3
        DTGridDetalleRellenado.Columns.Add("Presentacion", GetType(String)) '4
        DTGridDetalleRellenado.Columns.Add("Estado", GetType(String)) '5
        DTGridDetalleRellenado.Columns.Add("Minimo", GetType(Double)) '6
        DTGridDetalleRellenado.Columns.Add("Maximo", GetType(Double)) '7
        DTGridDetalleRellenado.Columns.Add("DispUbic", GetType(Double)) '8
        DTGridDetalleRellenado.Columns.Add("DispBodega", GetType(Double)) '9
        DTGridDetalleRellenado.Columns.Add("CantUbicar", GetType(Double)) '10 - Cantidad a reabastecer
        DTGridDetalleRellenado.Columns.Add("AbastecerCon", GetType(String)) '11 - Um/Pres        
        DTGridDetalleRellenado.Columns.Add("StockInferior", GetType(Boolean))
        DTGridDetalleRellenado.Columns.Add("IdPropietarioBodega", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdProductoBodega", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdPresentacion", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdProductoEstado", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdUnidadMedida", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdPropietario", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdBodega", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdUbicacion", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("NomUbic", GetType(String))
        DTGridDetalleRellenado.Columns.Add("IdReabasto", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdReabastecimientoLog", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("factor", GetType(Double))
        DTGridDetalleRellenado.Columns.Add("IdTareaUbicacionEnc", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("IdOperadorBodega", GetType(Integer))
        DTGridDetalleRellenado.Columns.Add("Operador", GetType(String))
        DTGridDetalleRellenado.Columns.Add("Pickeado", GetType(Double))
        'StockInferior

    End Sub

    Private chkSeleccionarGridRellenado As New RepositoryItemCheckEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit

    Private Sub Set_Columnas_Grid_Rellenado()


        Try

            Dim ColIndexAux As Integer = 0

            gvdRellenado.OptionsView.ShowFooter = True
            gvdRellenado.OptionsView.ShowGroupPanel = False

#Region "Columna - Seleccionar"

            Dim ColSeleccionar As New GridColumn With {
                .FieldName = "Seleccionar",
                .Caption = "Seleccionar",
                .Visible = True,
                .VisibleIndex = 0,
                .ColumnEdit = chkSeleccionarGridRellenado
            }

            ColSeleccionar.OptionsColumn.AllowEdit = True

            AddHandler chkSeleccionarGridRellenado.CheckedChanged, AddressOf chkSeleccionarGridRellenado_CheckedChanged
            gvdRellenado.Columns.Add(ColSeleccionar)

#End Region

#Region "Columna - Codigo_Producto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "Codigo",
                .Caption = "Código",
                .Visible = True,
                .VisibleIndex = 1,
                .Width = 150
            }

            ColCodigoProducto.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColCodigoProducto)

#End Region

#Region "Columna - Nombre"


            Dim ColNombre As New GridColumn With {
                .FieldName = "Nombre",
                .Caption = "Nombre",
                .Visible = True,
                .VisibleIndex = 2,
                .Width = 75
            }

            ColNombre.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColNombre)

#End Region

#Region "Columna - UMBas"


            Dim ColUMBas As New GridColumn With {
                .FieldName = "UMBas",
                .Caption = "UMBas",
                .Visible = True,
                .VisibleIndex = 3,
                .Width = 75
            }

            ColUMBas.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColUMBas)

#End Region

#Region "Columna - Presentacion"


            Dim ColPresentacion As New GridColumn With {
                .FieldName = "Presentacion",
                .Caption = "Presentación",
                .Visible = True,
                .VisibleIndex = 4,
                .Width = 75
            }

            ColPresentacion.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColPresentacion)

#End Region

#Region "Columna - Estado"


            Dim ColEstado As New GridColumn With {
                .FieldName = "Estado",
                .Caption = "Estado",
                .Visible = True,
                .VisibleIndex = 5,
                .Width = 75
            }

            ColEstado.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColEstado)

#End Region

#Region "Columna - Mínimo"

            Dim ColMinimo As New GridColumn With {
                .FieldName = "Minimo",
                .Caption = "Mínimo",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 6
            }

            ColMinimo.OptionsColumn.AllowEdit = False
            ColMinimo.OptionsColumn.ReadOnly = True
            ColMinimo.DisplayFormat.FormatType = FormatType.Numeric
            ColMinimo.DisplayFormat.FormatString = "{0:n6}"
            gvdRellenado.Columns.Add(ColMinimo)

#End Region

#Region "Columna - Máximo"

            Dim ColMaximo As New GridColumn With {
                .FieldName = "Maximo",
                .Caption = "Máximo",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 7
            }

            ColMaximo.OptionsColumn.AllowEdit = False
            ColMaximo.OptionsColumn.ReadOnly = True
            ColMaximo.DisplayFormat.FormatType = FormatType.Numeric
            ColMaximo.DisplayFormat.FormatString = "{0:n6}"
            gvdRellenado.Columns.Add(ColMaximo)

#End Region

#Region "Columna - DispUbic"

            Dim ColDispUbic As New GridColumn With {
                .FieldName = "DispUbic",
                .Caption = "Disp Ubic",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 8
            }

            ColDispUbic.OptionsColumn.AllowEdit = False
            ColDispUbic.OptionsColumn.ReadOnly = True
            ColDispUbic.DisplayFormat.FormatType = FormatType.Numeric
            ColDispUbic.DisplayFormat.FormatString = "{0:n6}"
            gvdRellenado.Columns.Add(ColDispUbic)

            gvdRellenado.Columns("DispUbic").SummaryItem.SummaryType = SummaryItemType.Sum
            gvdRellenado.Columns("DispUbic").SummaryItem.DisplayFormat = "{0:n6}"
            gvdRellenado.Columns("DispUbic").DisplayFormat.FormatType = FormatType.Numeric
            gvdRellenado.Columns("DispUbic").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - DispBodega"

            Dim ColDispBodega As New GridColumn With {
                .FieldName = "DispBodega",
                .Caption = "Disp Bodega",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 9
            }

            ColDispBodega.OptionsColumn.AllowEdit = False
            ColDispBodega.OptionsColumn.ReadOnly = True
            ColDispBodega.DisplayFormat.FormatType = FormatType.Numeric
            ColDispBodega.DisplayFormat.FormatString = "{0:n6}"
            gvdRellenado.Columns.Add(ColDispBodega)

            gvdRellenado.Columns("DispBodega").SummaryItem.SummaryType = SummaryItemType.Sum
            gvdRellenado.Columns("DispBodega").SummaryItem.DisplayFormat = "{0:n6}"
            gvdRellenado.Columns("DispBodega").DisplayFormat.FormatType = FormatType.Numeric
            gvdRellenado.Columns("DispBodega").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - CantUbicar"

            Dim ColCantUbicar As New GridColumn With {
                .FieldName = "CantUbicar",
                .Caption = "Cant Ubicar",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 9
            }

            ColCantUbicar.OptionsColumn.AllowEdit = False
            ColCantUbicar.OptionsColumn.ReadOnly = True
            ColCantUbicar.DisplayFormat.FormatType = FormatType.Numeric
            ColCantUbicar.DisplayFormat.FormatString = "{0:n6}"
            gvdRellenado.Columns.Add(ColCantUbicar)

            gvdRellenado.Columns("CantUbicar").SummaryItem.SummaryType = SummaryItemType.Sum
            gvdRellenado.Columns("CantUbicar").SummaryItem.DisplayFormat = "{0:n6}"
            gvdRellenado.Columns("CantUbicar").DisplayFormat.FormatType = FormatType.Numeric
            gvdRellenado.Columns("CantUbicar").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - UM a Ubicar"


            Dim ColUMAUbicar As New GridColumn With {
                .FieldName = "AbastecerCon",
                .Caption = "Abastecer Con",
                .Visible = True,
                .VisibleIndex = 10,
                .Width = 75
            }

            ColNombre.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColUMAUbicar)

#End Region

#Region "Columna - StockInferior"

            Dim ColStockInferior As New GridColumn With {
                .FieldName = "StockInferior",
                .Caption = "StockInferior",
                .Visible = True,
                .Width = 50,
                .VisibleIndex = 11
            }

            ColStockInferior.OptionsColumn.ReadOnly = True

            gvdRellenado.Columns.Add(ColStockInferior)

#End Region

#Region "Columna - IdPropietarioBodega"

            Dim ColIdPropietarioBodega As New GridColumn With {
                .FieldName = "IdPropietarioBodega",
                .Caption = "IdPropietarioBodega",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 12
            }

            gvdRellenado.Columns.Add(ColIdPropietarioBodega)

#End Region

#Region "Columna - IdProductoBodega"

            Dim ColIdProductoBodega As New GridColumn With {
                .FieldName = "IdProductoBodega",
                .Caption = "IdProductoBodega",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 13
            }

            gvdRellenado.Columns.Add(ColIdProductoBodega)

#End Region

#Region "Columna - IdPresentacion"

            Dim ColIdPresentacion As New GridColumn With {
                .FieldName = "IdPresentacion",
                .Caption = "IdPresentacion",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 14
            }

            gvdRellenado.Columns.Add(ColIdPresentacion)

#End Region

#Region "Columna - IdProductoEstado"

            Dim ColIdProductoEstado As New GridColumn With {
                .FieldName = "IdProductoEstado",
                .Caption = "IdProductoEstado",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 15
            }

            gvdRellenado.Columns.Add(ColIdProductoEstado)

#End Region

#Region "Columna - IdUnidadMedida"

            Dim ColIdUnidadMedida As New GridColumn With {
                .FieldName = "IdUnidadMedida",
                .Caption = "IdUnidadMedida",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 16
            }

            gvdRellenado.Columns.Add(ColIdUnidadMedida)

#End Region

#Region "Columna - IdPropietario"

            Dim ColIdPropietario As New GridColumn With {
                .FieldName = "IdPropietario",
                .Caption = "IdPropietario",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 17
            }

            gvdRellenado.Columns.Add(ColIdPropietario)

#End Region

#Region "Columna - IdBodega"

            Dim ColIdBodega As New GridColumn With {
                .FieldName = "IdBodega",
                .Caption = "IdBodega",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 18
            }

            gvdRellenado.Columns.Add(ColIdBodega)

#End Region

#Region "Columna - IdUbicacion"

            Dim ColIdUbicacion As New GridColumn With {
                .FieldName = "IdUbicacion",
                .Caption = "IdUbicacion",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 19
            }

            gvdRellenado.Columns.Add(ColIdUbicacion)

#End Region

#Region "Columna - Nombre ubicación"


            Dim ColNomUbic As New GridColumn With {
                .FieldName = "NomUbic",
                .Caption = "Ubicación",
                .Visible = True,
                .VisibleIndex = 20,
                .Width = 75
            }

            ColUMBas.OptionsColumn.AllowEdit = False
            gvdRellenado.Columns.Add(ColNomUbic)

#End Region

#Region "Columna - IdReabasto"

            Dim ColIdReabasto As New GridColumn With {
                .FieldName = "IdReabasto",
                .Caption = "IdReabasto",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 17
            }

            ColIdReabasto.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColIdReabasto)

#End Region

#Region "Columna - IdReabastecimientoLog"

            Dim ColIdReabastecimientoLog As New GridColumn With {
                .FieldName = "IdReabastecimientoLog",
                .Caption = "IdReabastecimientoLog",
                .Visible = False,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 18
            }

            ColIdReabastecimientoLog.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColIdReabastecimientoLog)

#End Region

#Region "Columna - Factor"

            Dim ColFactor As New GridColumn With {
                .FieldName = "factor",
                .Caption = "Factor",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 19
            }

            ColFactor.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColFactor)

#End Region

#Region "Columna - IdTareaUbicacionEnc"

            Dim ColIdTareaUbicacionEnc As New GridColumn With {
                .FieldName = "IdTareaUbicacionEnc",
                .Caption = "IdTareaUbicacionEnc",
                .Visible = True,
                .Width = 50,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = 20
            }

            ColIdTareaUbicacionEnc.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColIdTareaUbicacionEnc)

#End Region

#Region "Columna - IdTareaUbicacionEnc"

            Dim ColPickeado As New GridColumn With {
                .FieldName = "Pickeado",
                .Caption = "Pickeado",
                .Visible = True,
                .Width = 50,
                .VisibleIndex = 21
            }

            ColPickeado.OptionsColumn.AllowEdit = False

            gvdRellenado.Columns.Add(ColPickeado)

#End Region


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkSeleccionarGridRellenado_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim vIdReabastecimientoLog As Integer = 0
                Dim vIdReabasto As Integer = 0
                Dim vIdProductoBodega As Integer = 0
                Dim vIdUbicacion As Integer = 0
                Dim vIdProductoEstado As Integer = 0

                Dim BeReabasto As New clsBeTrans_reabastecimiento_log()

                If Not ritem Is Nothing Then

                    Dim Dr As DataRowView = gvdRellenado.GetFocusedRow
                    Dim lIndex As Integer = -1

                    lIndex = pListReabastosLog.FindIndex(Function(b) b.IdProductoBodega = Dr.Item("IdProductoBodega") _
                                                             AndAlso b.IdPresentacion = Dr.Item("IdPresentacion") _
                                                             AndAlso b.IdProductoEstado = Dr.Item("IdProductoEstado") _
                                                             AndAlso b.IdUnidadMedidaBasica = Dr.Item("IdUnidadMedida") _
                                                             AndAlso b.IdUbicacion = Dr.Item("IdUbicacion") _
                                                             AndAlso b.IdRellenado = Dr.Item("IdReabasto") _
                                                             AndAlso b.IdReabastecimientoLog = Dr.Item("IdReabastecimientoLog") _
                                                             AndAlso b.Cantidad_A_Ubicar > 0)

                    If lIndex > -1 Then

                        If ritem.Checked = False Then
                            pListReabastosLog.RemoveAll(Function(b) b.IdProductoBodega = Dr.Item("IdProductoBodega") _
                                                            AndAlso b.IdPresentacion = Dr.Item("IdPresentacion") _
                                                            AndAlso b.IdProductoEstado = Dr.Item("IdProductoEstado") _
                                                            AndAlso b.IdUnidadMedidaBasica = Dr.Item("IdUnidadMedida") _
                                                            AndAlso b.IdUbicacion = Dr.Item("IdUbicacion") _
                                                            AndAlso b.IdRellenado = Dr.Item("IdReabasto") _
                                                            AndAlso b.IdReabastecimientoLog = Dr.Item("IdReabastecimientoLog"))
                            'MsgBox("Existìa, pero lo desmarcaron y lo eliminè y todos felices")
                        End If

                    Else

                        If ritem.Checked Then

                            If Not Dr Is Nothing Then

                                If CDbl(Dr.Item("DispBodega")) <> 0 And CDbl(Dr.Item("DispBodega")) <> CDbl(Dr.Item("DispUbic")) Then


                                    vIdReabastecimientoLog = Dr.Item("IdReabastecimientoLog")
                                    vIdReabasto = Dr.Item("IdReabasto")
                                    vIdProductoBodega = Dr.Item("IdProductoBodega")
                                    vIdUbicacion = Dr.Item("IdUbicacion")
                                    vIdProductoEstado = Dr.Item("IdProductoEstado")

                                    BeReabasto = New clsBeTrans_reabastecimiento_log()
                                    BeReabasto = ListReabastosPendientes.Find(Function(x) x.IdReabastecimientoLog = vIdReabastecimientoLog _
                                                                                  AndAlso x.IdRellenado = vIdReabasto _
                                                                                  AndAlso x.IdProductoBodega = vIdProductoBodega _
                                                                                  AndAlso x.IdUbicacion = vIdUbicacion _
                                                                                  AndAlso x.IdProductoEstado = vIdProductoEstado _
                                                                                  AndAlso x.Cantidad_A_Ubicar > 0 _
                                                                                  AndAlso x.IdTareaUbicacionEnc = 0)

                                    If Not BeReabasto Is Nothing Then

                                        If BeReabasto.Cantidad_A_Ubicar > 0 Then

                                            BeReabasto.Seleccionado = True
                                            pListReabastosLog.Add(BeReabasto)

                                        Else
                                            XtraMessageBox.Show("WMS no pudo determinar la cantidad a ubicar. Código de error: 20211123_0930", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            ritem.Checked = False
                                        End If

                                    Else
                                        XtraMessageBox.Show("WMS no encontró el detalle del reabastecimiento. Código de error: 20211123_0931", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        ritem.Checked = False
                                    End If


                                Else

                                    XtraMessageBox.Show("Este producto no puede ser seleccionado porque no hay producto disponible en Bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ritem.Checked = False

                                End If

                            End If

                        End If

                    End If

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

    'gvdRellenado
    Private Sub gvdRellenado_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvdRellenado.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Dim View As GridView = sender

                '#EJC20210223: Formateo condicional de columnas por reabasto.
                If gvdRellenado.RowCount > 0 Then

                    If View.Columns.ColumnByFieldName("Maximo") IsNot Nothing Then

                        Dim Codigo_Producto As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Codigo"))
                        Dim max As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Maximo"))
                        Dim disbod As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("DispBodega"))
                        Dim cantubicar As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("CantUbicar"))
                        Dim dispubic As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("DispUbic"))

                        If Codigo_Producto = "40488" Then
                            Debug.WriteLine("Codigo_Producto Dispo: " & Codigo_Producto)
                        End If

                        Dim val As Double = Math.Round(max - disbod, 2)

                        If (cantubicar = 0) AndAlso (disbod = 0) Then
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#F89D42") 'Anaranjado
                            e.Appearance.BackColor2 = Color.Transparent
                        ElseIf Math.Round(disbod, 2) = 0 Then 'OrElse (disbod = dispubic AndAlso dispubic > 0)
                            e.Appearance.BackColor = Color.Salmon 'Rojo
                            e.Appearance.BackColor2 = Color.SeaShell
                        ElseIf (disbod < dispubic) Then
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#FFD042") 'Amarillo
                            e.Appearance.BackColor2 = Color.Transparent
                        ElseIf (disbod > 0 AndAlso cantubicar > disbod) Then
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#F5A9BC") 'Rosadito
                            e.Appearance.BackColor2 = Color.Transparent
                        Else
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#63C76B") 'Verde
                            e.Appearance.BackColor2 = Color.Transparent
                        End If

                    End If

                End If

            End If

        Catch ex As Exception

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

#End Region

    Public Property gBeRecepcion As New clsBeTrans_re_enc
    Private pListReabastosLog As New List(Of clsBeTrans_reabastecimiento_log)
    Private lTiempoMedioTipoTarea As New List(Of clsBeTipo_tarea_tiempos)

    Private Sub OnAppointmentChangedInsertedDeleted1(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        Bodega_muellesTableAdapter.Update(DSCalendarioTarea)
        DSCalendarioTarea.AcceptChanges()
    End Sub
    Private Sub OnAppointmentChangedInsertedDeleted2(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        PropietariosTableAdapter.Update(DSCalendarioTarea)
        DSCalendarioTarea.AcceptChanges()
    End Sub
    Private Sub Get_Tiempos_Tareas()

        Try

            lTiempoMedioTipoTarea = clsLnTipo_tarea_tiempos.Get_All_By_IdEmpresa_And_IdBodega(AP.IdEmpresa, AP.IdBodega)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmPrincipal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Inicializando")

        Me.Cursor = Cursors.WaitCursor

        Try

            dtpFechaInicio.Value = DateAdd(DateInterval.Day, -AP.Bodega.Rango_Dias_Documentos, Now)

            lblCantPosiciones.Text = "Cantidad de ubicaciones " & vbCrLf & AP.Bodega.Nombre

            SchedulerP.Start = Now
            SchedulerM.Start = Now

            EstoyOcupaoChico = True

            Task.Run(Sub()
                         Get_Tiempos_Tareas()
                     End Sub)

            If Not bgwTareas.IsBusy Then
                bgwTareas.RunWorkerAsync()
            End If

            EstoyOcupaoChico = False

            If gvdRellenado.RowCount > 0 Then
                gvdRellenado.SelectRows(0, 1)
            End If

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            lOperadoresByBodega = clsLnOperador_bodega.Get_All_By_IdBodega(AP.IdBodega)

            '#EJC20312041004: BackgrounWorker para obtener información del sistema.
            bgWorker.WorkerReportsProgress = True
            bgWorker.WorkerSupportsCancellation = True
            bgWorker.RunWorkerAsync()

            '#EJC20191028:
            'Ejecutar cada 1 minuto (parametrizar mas tarde)
            tmrTareas.Interval = tmrTareas.Interval * 5000
            tmrTareas.Enabled = False
            mnuTimerMonitor.Caption = "Activar actualización automática"

            Configurar_Calendario()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            'prg.Visible = False
            Me.Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As RowClickEventArgs) Handles GridView1.RowClick

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim fi As Date = Dr.Item("Inicio")
                SchedulerM.GoToDate(fi)
                SchedulerP.GoToDate(fi)

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

    Private Sub mnuEnviarTarea_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarTarea.ItemClick

        Me.Cursor = Cursors.WaitCursor

        Dim vIdOperador As Integer = 0
        Dim vIdOperadorBodega As Integer = 0
        Dim vNombreOperador As String = ""

        Try

            If pListReabastosLog IsNot Nothing Then

                If pListReabastosLog.TrueForAll(Function(b) b.Seleccionado = False) Then
                    XtraMessageBox.Show("Seleccione productos para generar tarea.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If XtraMessageBox.Show("¿Enviar tarea de los productos seleccionados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        For Each bo In pListReabastosLog

                            If bo.Stock_Inferior Then

                                If (MessageBox.Show(String.Format("No hay stock suficiente en bodega para producto {0}. ¿Continuar?", bo.Codigo_Producto),
                                                   Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) = False Then
                                    Return
                                End If

                            End If

                        Next

                        '#EJC202302082023: Cambiar operador por defecto en reabasto.
                        If XtraMessageBox.Show("¿Cambiar operador por defecto?", "Asignación de reabasto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            Try

                                Dim Operador As New frmOperador_List() With
                               {.Modo = frmBodegaUbicacion_List.pModo.Seleccion}
                                Operador.ShowDialog()

                                If Operador.pObj IsNot Nothing AndAlso Operador.pObj.IdOperador <> 0 Then
                                    vIdOperador = Operador.pObj.IdOperador
                                    vNombreOperador = Operador.pObj.Nombres

                                    For Each R In pListReabastosLog
                                        R.IdOperadorDefecto = vIdOperador
                                        vIdOperadorBodega = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(vIdOperador, R.IdBodega)
                                        R.IdOperadorBodega = vIdOperadorBodega
                                    Next

                                End If

                                Operador.Close()
                                Operador.Dispose()

                            Catch ex As Exception

                                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error)

                                Dim vMsgError As String = ex.Message
                                clsLnLog_error_wms.Agregar_Error(vMsgError)

                            End Try

                        End If

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Enviando...")

                        If clsLnTrans_reabastecimiento_log.Enviar_Tareas(pListReabastosLog,
                                                                         AP.HostName,
                                                                         AP.IdBodega,
                                                                         AP.UsuarioAp.IdUsuario,
                                                                         AP.IdEmpresa,
                                                                         vIdOperadorBodega,
                                                                         txtprgreabasto) Then

                            pListReabastosLog = New List(Of clsBeTrans_reabastecimiento_log)

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show("Tarea(s) generada(s).", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Task.Run(Sub()
                                         If IsHandleCreated Then BeginInvoke(Call_Listar_Reabastecimiento_Productos)
                                     End Sub)


                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub mnuActualizarFromMenu_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarFromMenu.ItemClick

        Try

            If TabPane1.SelectedPageIndex = 0 Then
                Task.Run(Sub()
                             If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)
                         End Sub)
            ElseIf TabPane1.SelectedPageIndex = 1 Then
                Task.Run(Sub()
                             Task.Run(Sub()
                                          If IsHandleCreated Then BeginInvoke(Call_Listar_Reabastecimiento_Productos)
                                      End Sub)
                         End Sub)
            ElseIf TabPane1.SelectedPageIndex = 4 Then
                If Not bgwTareas.IsBusy Then
                    bgwTareas.RunWorkerAsync()
                End If
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            EstoyOcupaoChico = False
        End Try

    End Sub

    Private Sub ActualizarGridView(ByVal DTTareas As DataTable)
        Try
            DgridTareas.DataSource = DTTareas
            GridView1.RefreshData()
        Catch ex As Exception
        End Try
    End Sub

    Public Property ContadorTareasRecepcion As Integer = 0
    Public Property ContadorTareas As Integer = 0
    Public Property ContadorTareasUbicacion As Integer = 0

    Private Sub Actualizar_Tareas()

        Dim clsTransaccion As New clsTransaccion()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Actualizando tareas")

            lblprg.Caption = "Init Async Refresh Proc.."
            ContadorTareasRecepcion = 0 : ContadorTareas = 0

            Try

                Dim DT As New DataTable
                Dim vIdTransaccion As Integer = 0
                Dim BeTransOCEnc As New clsBeTrans_oc_enc
                Dim vOrigen As String = ""
                Dim vDestino As String = ""
                Dim vProgreso As Double = 0
                Dim BePickingEnc As New clsBeTrans_picking_enc()
                Dim BeRecepcion As New clsBeTrans_re_enc

                clsTransaccion.Begin_Transaction()

                '#EJC20260522_PRINCIPAL02_TAREAS_READMODEL: Carga enriquecida en lote para evitar roundtrips por tarea.
                DT = clsLnTarea_hh.Get_Lista_Tareas_Monitor_By_IdBodega(AP.IdBodega,
                                                                        dtpFechaInicio.Value,
                                                                        dtpFechaFin.Value,
                                                                        clsTransaccion.lConnection,
                                                                        clsTransaccion.lTransaction)

                SyncLock DTTareas

                    DTTareas.BeginLoadData()

                    DTTareas.Rows.Clear()

                    Dim ListaIdReOC As New List(Of Integer)
                    Dim DetalleOC As New List(Of clsBeTrans_oc_det)
                    Dim vCantidadSoli = 0
                    Dim vCantidadRec = 0

                    Dim vUsaReadModelTareas As Boolean = DT.Columns.Contains("Origen") AndAlso
                                                         DT.Columns.Contains("Destino") AndAlso
                                                         DT.Columns.Contains("Progreso") AndAlso
                                                         DT.Columns.Contains("CalendarioAsunto")

                    For Each r In DT.Rows()

                        vIdTransaccion = r("Correlativo")
                        '#AT20250124 Limpiar las variables
                        vOrigen = ""
                        vDestino = ""

                        If vUsaReadModelTareas Then

                            vOrigen = IIf(IsDBNull(r("Origen")), "", r("Origen")).ToString()
                            vDestino = IIf(IsDBNull(r("Destino")), "", r("Destino")).ToString()
                            vProgreso = IIf(IsDBNull(r("Progreso")), 0, r("Progreso"))

                            If Not IsDBNull(r("CalendarioInicio")) AndAlso Not IsDBNull(r("CalendarioFin")) Then
                                Agregar_Tarea_Calendario(IIf(IsDBNull(r("CalendarioAsunto")), "", r("CalendarioAsunto")).ToString(),
                                                         r("CalendarioInicio"),
                                                         r("CalendarioFin"),
                                                         IIf(IsDBNull(r("CalendarioIdMuelle")), 1, r("CalendarioIdMuelle")),
                                                         IIf(IsDBNull(r("CalendarioIdTarea")), vIdTransaccion, r("CalendarioIdTarea")),
                                                         IIf(IsDBNull(r("CalendarioUbicacion")), "", r("CalendarioUbicacion")).ToString())
                            End If

                            If r.Item("Tarea") = "Recepción" Then
                                ContadorTareasRecepcion += 1
                            ElseIf r.Item("Tarea") = "Picking" Then
                                ContadorTareas += 1
                            ElseIf r.Item("Tarea") = "Ubicación" Then
                                ContadorTareasUbicacion += 1
                            End If

                        ElseIf r.Item("Tarea") = "Recepción" Then

                            BeRecepcion = clsLnTrans_re_enc.GetSingle(vIdTransaccion, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            ListaIdReOC = clsLnTrans_re_oc.Get_IdOrdenCompraEnc_By_IdRecepcionEnc(vIdTransaccion,
                                                                                                  clsTransaccion.lConnection,
                                                                                                  clsTransaccion.lTransaction)

                            If ListaIdReOC.Count > 0 Then

                                Dim vIdOrdenCompraEnc As Integer = ListaIdReOC(0)

                                BeTransOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(vIdOrdenCompraEnc,
                                                                                                clsTransaccion.lConnection,
                                                                                                clsTransaccion.lTransaction)

                                Agregar_Tarea_Calendario("Recepción# " & vIdTransaccion,
                                                         BeRecepcion.Fecha_recepcion,
                                                         BeRecepcion.Hora_fin_pc,
                                                         BeRecepcion.IdMuelle,
                                                         BeRecepcion.IdRecepcionEnc,
                                                         BeRecepcion.UbicacionRecepcion)

                            End If

                            vOrigen = BeTransOCEnc.ProveedorBodega.Proveedor.Nombre
                            vDestino = AP.NomBodega

                            For Each pIdOrdenCompraEnc In ListaIdReOC

                                DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc_Sin_Refs(pIdOrdenCompraEnc,
                                                                                                          clsTransaccion.lConnection,
                                                                                                          clsTransaccion.lTransaction)

                                If Not DetalleOC Is Nothing Then
                                    vCantidadSoli = DetalleOC.Sum(Function(x) x.Cantidad)
                                    vCantidadRec += DetalleOC.Sum(Function(x) x.Cantidad_recibida)
                                End If

                            Next

                            If vCantidadSoli > 0 AndAlso vCantidadRec > 0 Then
                                vProgreso = Math.Round((vCantidadRec / vCantidadSoli), 2)
                            Else
                                vProgreso = 0
                            End If

                            vCantidadSoli = 0 : vCantidadRec = 0

                            ContadorTareasRecepcion += 1

                            If Not BeRecepcion Is Nothing Then
                                Agregar_Tarea_Calendario("Recepcinón# " & vIdTransaccion,
                                                        BeRecepcion.Fecha_recepcion,
                                                        BeRecepcion.Hora_fin_pc,
                                                        BeRecepcion.IdMuelle,
                                                        BeRecepcion.IdRecepcionEnc,
                                                        BeRecepcion.UbicacionRecepcion)
                            End If

                        ElseIf r.Item("Tarea") = "Picking" Then

                            BePickingEnc = clsLnTrans_picking_enc.GetSingle(vIdTransaccion,
                                                                            clsTransaccion.lConnection,
                                                                            clsTransaccion.lTransaction)

                            If Not BePickingEnc Is Nothing Then

                                Dim vCantidadSolicitada As Double = BePickingEnc.ListaPickingUbic.Sum(Function(x) x.Cantidad_Solicitada)
                                Dim vCantidadRecibida As Double = BePickingEnc.ListaPickingUbic.Sum(Function(x) x.Cantidad_Recibida)
                                Dim vDeltaDifPickeado As Double = 0

                                If vCantidadSolicitada > 0 AndAlso vCantidadRecibida > 0 Then
                                    vProgreso = Math.Round((vCantidadRecibida / vCantidadSolicitada), 2)
                                Else
                                    vProgreso = 0
                                End If

                                Agregar_Tarea_Calendario("Picking# " & vIdTransaccion,
                                                         BePickingEnc.Fecha_picking,
                                                         BePickingEnc.Fecha_Fin_Preparacion,
                                                         BePickingEnc.IdBodegaMuelle,
                                                         BePickingEnc.IdPickingEnc,
                                                         BePickingEnc.IdUbicacionPicking)

                                ContadorTareas += 1

                                '#AT20250125 Agergar valores a las variables vOrigen y vDestino
                                vOrigen = BePickingEnc.NombreBodega
                                vDestino = ""

                            Else
                                vProgreso = 0
                            End If

                        ElseIf r.Item("Tarea") = "Ubicación" Then

                            Dim BeTareaUbicacion As New clsBeTrans_ubic_hh_enc
                            BeTareaUbicacion = clsLnTrans_ubic_hh_enc.GetSingle(vIdTransaccion, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            ContadorTareasUbicacion += 1

                            Agregar_Tarea_Calendario("Cambio Ubicación/Estado: #" & vIdTransaccion,
                                                       BeTareaUbicacion.Fec_agr,
                                                       BeTareaUbicacion.Fec_agr.AddHours(1),
                                                       1,
                                                       BeTareaUbicacion.IdTareaUbicacionEnc,
                                                       BeTareaUbicacion.Nombre_Operador)

                        End If

                        DTTareas.Rows.Add(vIdTransaccion,
                                          r("Tarea"),
                                          r("Inicio"),
                                          r("Ult_Revision"),
                                          r("TTM"),
                                          r("Propietario"),
                                          r("Estado"),
                                          r("IdTareaHH"),
                                          vOrigen,
                                          vDestino,
                                          vProgreso,
                                          r("Observacion"))

                    Next

                    DTTareas.EndLoadData()

                End SyncLock

                clsTransaccion.Commit_Transaction()

                If GridView1.Columns.Count > 0 Then

                    '#EJC20180726: Agregué formateo, por horas.
                    GridView1.Columns("Inicio").DisplayFormat.FormatType = FormatType.DateTime
                    GridView1.Columns("Inicio").DisplayFormat.FormatString = "G"

                    '#EJC20180726: Agregué formateo, fecha_fin
                    GridView1.Columns("Ult_Revision").DisplayFormat.FormatType = FormatType.DateTime
                    GridView1.Columns("Ult_Revision").DisplayFormat.FormatString = "G"
                    GridView1.Columns("Ult_Revision").Caption = "Ult_Revisión"

                    '#EJC20180726: Agregué formateo, TIEMPO TOTAL EN MINUTOS, desde el inicio hasta la fecha actual
                    GridView1.Columns("TTM").DisplayFormat.FormatType = FormatType.Numeric
                    GridView1.Columns("TTM").DisplayFormat.FormatString = "{0:n2}"

                    GridView1.Columns("IdTransaccion").SummaryItem.SummaryType = SummaryItemType.Count
                    GridView1.Columns("IdTransaccion").SummaryItem.DisplayFormat = "Tareas: {0:n2}"
                    GridView1.Columns("IdTransaccion").DisplayFormat.FormatType = FormatType.Numeric

                    GridView1.Columns("IdTareaHH").Visible = False

                    '#EJC20180  726: Agregué formateo, TIEMPO TOTAL EN MINUTOS, desde el inicio hasta la fecha actual
                    GridView1.Columns("Progreso").DisplayFormat.FormatType = FormatType.Numeric
                    GridView1.Columns("Progreso").DisplayFormat.FormatString = "P"

                    GridView1.BestFitColumns()

                End If

            Catch ex As Exception
                Throw ex
            Finally
                lblprg.Caption = "UA: " & Now
            End Try

            'Set_Indicadores_Gen()

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            clsTransaccion.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Public Class clsBeReabastoReserva

        Public Property IdBodega As Integer = 0
        Public Property IdProductoBodega As Integer = 0
        Public Property CodigoProducto As String = ""
        Public Property IdProductoEstado As Integer = 0
        Public Property IdUnidadMedidaBasica As Integer = 0
        Public Property IdPresentacion As Integer = 0
        Public Property IdPresentacionAbastecerCon As Integer = 0
        Public Property CantidadReabasto As Double = 0
        Public Property IdUbicacion As Integer = 0
        Public Property IdRellenado As Integer = 0

    End Class

    Private ListReabastosPendientesProcesar As List(Of clsBeTrans_reabastecimiento_log)
    Private ListReabastosGenerados As List(Of clsBeTrans_reabastecimiento_log)
    Private ListReabastosPendientes As List(Of clsBeTrans_reabastecimiento_log)
    Private ListaReabastoReserva As New List(Of clsBeReabastoReserva)
    Private Property IsLoading() As Boolean = False

    Private Sub Listar_Reabastecimiento_Productos()

        Dim clsTransaccion As New clsTransaccion()

        prg.Visible = True

        Try

            If IsLoading Then Exit Sub

            IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

            ListaReabastoReserva = New List(Of clsBeReabastoReserva)

            clsTransaccion.Begin_Transaction()

            dgridRellenado.SuspendLayout()

            dgridTareasPendientesReabasto.SuspendLayout()

            DTGridDetalleRellenado.Rows.Clear()

            DTGridTareasPendientesReabasto.Rows.Clear()

            Application.DoEvents()

            ListReabastosPendientes = New List(Of clsBeTrans_reabastecimiento_log)

            '#CKFK 20211120 Agregué el IdBodega a esta función porque al crear el reabastecimiento en el producto es por Operador y se debe asignar el 
            'el IdOperadorBodega correspondiente
            ListReabastosPendientes = clsLnTrans_reabastecimiento_log.Get_Reabastecimientos_Productos(AP.IdBodega,
                                                                                                      clsTransaccion.lConnection,
                                                                                                      clsTransaccion.lTransaction)

            Dim lMaximo As Double
            Dim lDisp As Double
            Dim NomUbic As String
            Dim fila As Integer = 0
            Dim BePresentacionAbastecerCon As New clsBeProducto_Presentacion
            Dim vCantidadRelacionDeFactor As Double = 0
            Dim vNomUMAUbicar As String = ""
            Dim DeltaLlenado As Double = 0
            Dim vExistenciaDisponibleUMReabasto As Double = 0
            Dim vIdMaxReabastecimientoLog As Integer = clsLnTrans_reabastecimiento_log.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
            Dim vIdxListReabastosPendientesProcesar As Integer = 0
            Dim vIdxListReabastosGenerados As Integer = 0
            Dim vIdxTareaHHReabastoNoProcesadaCompletamente As Integer = -1
            Dim vIdTareaUbicacionHH As Integer = 0
            Dim pBeStockDisponible As New clsBeStock()
            Dim pBeStockDisponibleEnUbicacion As New clsBeStock()
            Dim vCantidadReservadaEnMemoriaPorReabasto As Double = 0
            Dim vMaximoAuxEnPres As Double = 0
            Dim vMinimoAuxEnPres As Double = 0
            Dim vDisponibleEnUbicacionPres As Double = 0
            Dim vDisponibleEnStockPres As Double = 0
            Dim vCantidadAUbicarEnMemoriaPorUbicacionDestino As Double = 0
            Dim lReabastoExistente As New List(Of clsBeReabastoReserva)
            Dim lReabastoExistentePorUbicacion As New List(Of clsBeReabastoReserva)
            Dim BeBodega As New clsBeBodega
            Dim vCantidadTarima As Double = 0
            Dim vDeltaTarimasRellenado As Double = 0
            Dim vCantidadTarimasCompletasAUbicar As Double = 0
            Dim vIdTareaUbicacion As Integer = 0
            Dim BeTareaUbicacion As New clsBeTrans_ubic_hh_enc
            Dim lBeTareaUbicacionDet As New List(Of clsBeTrans_ubic_hh_det)
            '#EJC20260522_PRINCIPAL02_REABASTO_CACHE: Evita consultar la misma presentacion por cada regla de rellenado.
            Dim vPresentacionesAbastecerConPorId As New Dictionary(Of Integer, clsBeProducto_Presentacion)

            If ListReabastosPendientes Is Nothing Then Exit Sub

            prg.Properties.Maximum = ListReabastosPendientes.Count
            prg.EditValue = 0
            prg.Properties.Step = 1
            prg.Properties.PercentView = True
            prg.Properties.Maximum = ListReabastosPendientes.Count
            prg.Properties.Minimum = 0

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            txtprgreabasto.Clear()

            ListReabastosPendientesProcesar = New List(Of clsBeTrans_reabastecimiento_log)

            '#EJC20210303: Si no existen tareas enviadas de reabasto a la HH entonces, se pueden agregar al monitor de pendientes.
            'Esta lista determina si la HH ya procesó o no la tarea, hasta que la HH no la procese, se seguirá mostrando en el monitor.
            ListReabastosPendientesProcesar = clsLnTrans_reabastecimiento_log.Get_All_Pendientes_De_Procesar_Trans(AP.IdBodega,
                                                                                                                   dtpFechaInicio.Value,
                                                                                                                   dtpFechaFin.Value,
                                                                                                                   clsTransaccion.lConnection,
                                                                                                                   clsTransaccion.lTransaction)

            ListReabastosGenerados = New List(Of clsBeTrans_reabastecimiento_log)

            '#EJC20210303: Esta lista, determina si la inexistencia ya se insertó o no previamente en la tabla.
            ListReabastosGenerados = clsLnTrans_reabastecimiento_log.Get_All_Generados(AP.IdBodega,
                                                                                       dtpFechaInicio.Value,
                                                                                       dtpFechaFin.Value,
                                                                                       clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction)


            For Each vReabasto In ListReabastosPendientes.OrderBy(Function(x) x.Codigo_Producto)
                ''222147 está por encima del máximo
                ''222182 aún no llega al mínimo

                pBeStockDisponibleEnUbicacion = New clsBeStock()

                If vReabasto.Codigo_Producto = "00020502" Then
                    Debug.WriteLine("Codigo_Producto Dispo: " & vReabasto.CantidadPresDispo &
                                    " CantidadSfUbicDestino: " & vReabasto.CantidadSFUbicDestino &
                                    " Abastecer con: " & vReabasto.IdPresentacionAbastercerCon &
                                    " Abastecer a: " & vReabasto.IdPresentacion)
                End If


                vIdxListReabastosGenerados = ListReabastosGenerados.FindIndex(Function(x) x.IdBodega = AP.IdBodega _
                                                                              AndAlso x.IdRellenado = vReabasto.IdRellenado _
                                                                              AndAlso x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                              AndAlso x.IdUbicacion = vReabasto.IdUbicacion _
                                                                              AndAlso x.IdTareaUbicacionEnc <> 0)

                If vIdxListReabastosGenerados <> -1 Then
                    Continue For
                End If

                DeltaLlenado = 0

                '#EJC20210303: Si ya se insertó previamente, obtener el vIdReabastecimientoLog ya insertado previamente (para asociarle la tarea)
                vIdxListReabastosGenerados = ListReabastosGenerados.FindIndex(Function(x) x.IdBodega = AP.IdBodega _
                                                                              AndAlso x.IdRellenado = vReabasto.IdRellenado _
                                                                              AndAlso x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                              AndAlso x.IdUbicacion = vReabasto.IdUbicacion _
                                                                              AndAlso x.IdTareaUbicacionEnc = 0)

                If vIdxListReabastosGenerados <> -1 Then
                    vReabasto.IdReabastecimientoLog = ListReabastosGenerados(vIdxListReabastosGenerados).IdReabastecimientoLog
                    vReabasto.IdRellenado = ListReabastosGenerados(vIdxListReabastosGenerados).IdRellenado
                End If

                Application.DoEvents()

                ' CONSULTA CUANTO HAY EN BODEGA
                Dim BeStockUbicDestino As New clsBeStock() With {.IdProductoBodega = vReabasto.IdProductoBodega,
                    .ProductoEstado = New clsBeProducto_estado()}
                BeStockUbicDestino.ProductoEstado.IdEstado = vReabasto.IdProductoEstado
                BeStockUbicDestino.Presentacion = New clsBeProducto_Presentacion
                BeStockUbicDestino.Presentacion.IdPresentacion = vReabasto.IdPresentacion

                vNomUMAUbicar = vReabasto.NombreUmBas

                '#EJC20210301: El abastecimiento se quiere realizar con unidad de medida básica
                If vReabasto.IdUmBasAbastercerCon <> 0 Then
                    BeStockUbicDestino.IdUnidadMedida = vReabasto.IdUmBasAbastercerCon
                    vNomUMAUbicar = vReabasto.NombreUmBas
                ElseIf vReabasto.IdPresentacionAbastercerCon <> 0 Then '#EJC20210301:el reabastecimiento se quiere realizar con una presentación.
                    BeStockUbicDestino.IdUnidadMedida = vReabasto.IdUnidadMedidaBasica
                    BeStockUbicDestino.IdPresentacion = vReabasto.IdPresentacionAbastercerCon
                    If Not vPresentacionesAbastecerConPorId.TryGetValue(vReabasto.IdPresentacionAbastercerCon, BePresentacionAbastecerCon) Then
                        BePresentacionAbastecerCon = New clsBeProducto_Presentacion()
                        BePresentacionAbastecerCon.IdPresentacion = vReabasto.IdPresentacionAbastercerCon
                        BePresentacionAbastecerCon.Nombre = vReabasto.NombrePresentacionAbastecerCon
                        BePresentacionAbastecerCon.Factor = vReabasto.FactorAbastecerCon
                        BePresentacionAbastecerCon = clsLnProducto_presentacion.GetSingle(vReabasto.IdPresentacionAbastercerCon,
                                                                                          clsTransaccion.lConnection,
                                                                                          clsTransaccion.lTransaction)
                        vPresentacionesAbastecerConPorId(vReabasto.IdPresentacionAbastercerCon) = BePresentacionAbastecerCon
                    End If

                Else '#EJC20210301:  esto no debería ocurrir, pero lo dejo para que me preguntes Carol jaja.
                    'quiere decir que no se definió con que se quiere reabastecer, por defecto consultaré cuanto hay en umbas.
                    BeStockUbicDestino.IdUnidadMedida = vReabasto.IdUnidadMedidaBasica
                    vNomUMAUbicar = vReabasto.NombreUmBas
                End If

                pBeStockDisponible.IdProductoBodega = vReabasto.IdProductoBodega
                pBeStockDisponible.IdProductoEstado = vReabasto.IdProductoEstado
                pBeStockDisponible.ProductoEstado.IdEstado = vReabasto.IdProductoEstado
                pBeStockDisponible.IdUnidadMedida = vReabasto.IdUnidadMedidaBasica
                pBeStockDisponible.IdBodega = vReabasto.IdBodega

                If vReabasto.IdPresentacionAbastercerCon <> 0 Then
                    pBeStockDisponible.IdPresentacion = vReabasto.IdPresentacionAbastercerCon
                Else
                    pBeStockDisponible.IdPresentacion = vReabasto.IdPresentacion
                End If

                If vReabasto.Codigo_Producto = "00020502" Then
                    Debug.WriteLine("que sopa aqui")
                End If

                vDisponibleEnStockPres = 0 : vDisponibleEnUbicacionPres = 0 : vCantidadReservadaEnMemoriaPorReabasto = 0 : vCantidadAUbicarEnMemoriaPorUbicacionDestino = 0

                CopyObject(pBeStockDisponible, pBeStockDisponibleEnUbicacion)

                '#EJC20211216: Buscar la existencia por función no desde la vista.
                If clsLnStock.Get_Existencia_Disp_Menos_Picking_By_IdProducto_By_IdUbicacion(pBeStockDisponible,
                                                                                             pBeStockDisponibleEnUbicacion,
                                                                                             vReabasto.IdBodega,
                                                                                             vReabasto.IdUbicacion,
                                                                                             True,
                                                                                             False,
                                                                                             0,
                                                                                             False,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction) Then

                    vReabasto.Stock_Disponible = pBeStockDisponible.Cantidad
                    vReabasto.Pickeado = pBeStockDisponible.Añada

                End If

                If vReabasto.IdPresentacionAbastercerCon <> 0 Then
                    If vReabasto.IdPresentacion = 0 Then
                        pBeStockDisponibleEnUbicacion.IdPresentacion = vReabasto.IdPresentacion
                    Else
                        pBeStockDisponibleEnUbicacion.IdPresentacion = vReabasto.IdPresentacionAbastercerCon
                    End If
                Else
                    pBeStockDisponibleEnUbicacion.IdPresentacion = vReabasto.IdPresentacion
                End If

                '#EJC202303091457: Solicitud de Carolina para BYB - Reabasto.
                If Not (pBeStockDisponibleEnUbicacion.Cantidad_Reservada - pBeStockDisponibleEnUbicacion.Añada) = 0 Then

                    If chkExcluirUbicacionDestinoLlena.Checked Then

                        txtprgreabasto.AppendText(vbNewLine)
                        txtprgreabasto.AppendText("La ubicación: " & vReabasto.Ubicacion & " tiene: " & vReabasto.Stock_Disponible & " en stock del producto:" & vReabasto.Codigo_Producto & " se omitirá la tarea de rebasto. ")
                        txtprgreabasto.AppendText(vbNewLine)
                        txtprgreabasto.Refresh()
                        txtprgreabasto.SelectionStart = txtprgreabasto.TextLength
                        txtprgreabasto.ScrollToCaret()

                        Continue For

                    End If

                Else
                    Debug.WriteLine("espera_202303091554")
                End If

                '#EJC20211221: Excluir lo que no tiene existencia.
                If vReabasto.Stock_Disponible = 0 Then
                    If mnuExcluirSinExistencia.Checked Then Continue For
                End If

                If Not ListaReabastoReserva Is Nothing Then

                    If ListaReabastoReserva.Count > 0 Then

                        lReabastoExistente = ListaReabastoReserva.FindAll(Function(x) x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                          AndAlso x.IdBodega = vReabasto.IdBodega _
                                                                          AndAlso x.IdUnidadMedidaBasica = vReabasto.IdUnidadMedidaBasica _
                                                                          AndAlso x.IdPresentacionAbastecerCon = vReabasto.IdPresentacionAbastercerCon).ToList()

                        If Not lReabastoExistente Is Nothing Then
                            If lReabastoExistente.Count > 0 Then
                                vCantidadReservadaEnMemoriaPorReabasto = lReabastoExistente.Sum(Function(x) x.CantidadReabasto)
                            End If
                        End If

                    End If

                End If

                If Not ListaReabastoReserva Is Nothing Then

                    If ListaReabastoReserva.Count > 0 Then

                        If vReabasto.IdPresentacion = 0 Then
                            lReabastoExistentePorUbicacion = ListaReabastoReserva.FindAll(Function(x) x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                                          AndAlso x.IdBodega = vReabasto.IdBodega _
                                                                                          AndAlso x.IdUnidadMedidaBasica = vReabasto.IdUnidadMedidaBasica _
                                                                                          AndAlso x.IdUbicacion = vReabasto.IdUbicacion _
                                                                                          AndAlso x.IdPresentacion = vReabasto.IdPresentacion).ToList()
                        Else
                            lReabastoExistentePorUbicacion = ListaReabastoReserva.FindAll(Function(x) x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                                          AndAlso x.IdBodega = vReabasto.IdBodega _
                                                                                          AndAlso x.IdUnidadMedidaBasica = vReabasto.IdUnidadMedidaBasica _
                                                                                          AndAlso x.IdUbicacion = vReabasto.IdUbicacion _
                                                                                          AndAlso x.IdPresentacion = vReabasto.IdPresentacionAbastercerCon).ToList()
                        End If


                        If Not lReabastoExistentePorUbicacion Is Nothing Then
                            If lReabastoExistentePorUbicacion.Count > 0 Then
                                vCantidadAUbicarEnMemoriaPorUbicacionDestino = lReabastoExistente.Sum(Function(x) x.CantidadReabasto)
                            End If
                        End If

                    End If

                End If

                If vReabasto.IdPresentacionAbastercerCon <> 0 Then

                    If Not BePresentacionAbastecerCon Is Nothing Then

                        If BePresentacionAbastecerCon.Factor > 0 Then
                            If pBeStockDisponibleEnUbicacion.IdPresentacion <> 0 Then
                                vDisponibleEnUbicacionPres = pBeStockDisponibleEnUbicacion.Cantidad
                                vReabasto.Stock_Ubicacion = pBeStockDisponibleEnUbicacion.Cantidad * BePresentacionAbastecerCon.Factor
                            End If
                            vDisponibleEnStockPres = vReabasto.Stock_Disponible '- vCantidadReservadaEnMemoriaPorReabasto
                        Else
                            Throw New Exception("No está definido el factor de conversión para el código: " & vReabasto.Codigo_Producto & " para abastecer con presentación: " & vReabasto.NombrePresentacionAbastecerCon)
                        End If

                    End If

                Else
                    vReabasto.Stock_Ubicacion = pBeStockDisponibleEnUbicacion.Cantidad
                    'Reabasto.Stock_Ubicacion = vReabasto.CantidadSFUbicDestino
                End If

                If (vReabasto.IdPresentacion = 0 AndAlso vReabasto.IdPresentacionAbastercerCon <> 0) Then

                    If vReabasto.IdPresentacionAbastercerCon <> 0 Then

                        If BePresentacionAbastecerCon.Factor > 0 Then
                            vMaximoAuxEnPres = Math.Round(vReabasto.Maximo / BePresentacionAbastecerCon.Factor, 6)
                            vMinimoAuxEnPres = Math.Round(vReabasto.Minimo / BePresentacionAbastecerCon.Factor, 6)
                        Else
                            Throw New Exception("No está definido el factor de conversión para el código: " & vReabasto.Codigo_Producto & " para abastecer con presentación: " & vReabasto.NombrePresentacionAbastecerCon)
                        End If


                    End If

                Else
                    vMaximoAuxEnPres = vReabasto.Maximo
                    vMinimoAuxEnPres = vReabasto.Minimo
                End If

                If vReabasto.IdPresentacionAbastercerCon = 0 Then

                    If vReabasto.Stock_Ubicacion > vReabasto.Maximo Then
                        If vReabasto.Stock_Ubicacion <= vReabasto.Minimo Then
                            DeltaLlenado = vReabasto.Maximo '- vReabasto.Stock_Ubicacion
                        Else
                            DeltaLlenado = 0
                        End If
                    Else
                        If vReabasto.Stock_Ubicacion <= vReabasto.Minimo Then
                            DeltaLlenado = vReabasto.Maximo
                        Else
                            DeltaLlenado = 0 'A petición de Marcelo #20211122_1720_PANAMÁ
                        End If
                    End If

                Else

                    If vDisponibleEnStockPres > vMaximoAuxEnPres Then

                        If (vDisponibleEnUbicacionPres <= vMinimoAuxEnPres) Then

                            If vCantidadAUbicarEnMemoriaPorUbicacionDestino = 0 Then
                                DeltaLlenado = vMaximoAuxEnPres - vDisponibleEnUbicacionPres '- vReabasto.Stock_Ubicacion
                            Else

                                If vCantidadAUbicarEnMemoriaPorUbicacionDestino > (vMaximoAuxEnPres - vDisponibleEnUbicacionPres) Then
                                    DeltaLlenado = 0
                                Else

                                    Dim vDelta1 As Double = vCantidadAUbicarEnMemoriaPorUbicacionDestino - (vMaximoAuxEnPres - vDisponibleEnUbicacionPres)

                                    If vDelta1 > 0 Then
                                        DeltaLlenado = vDelta1
                                    Else
                                        DeltaLlenado = 0
                                    End If

                                End If

                            End If

                        Else

                            If vReabasto.IdPresentacion <> 0 Then

                                DeltaLlenado = (vMaximoAuxEnPres - vDisponibleEnUbicacionPres)

                                If DeltaLlenado < 0 Then '#EJC20211221: La posición tiene sobrestock
                                    '#EJC20211221: Excluir lo que tiene sobre stock.
                                    If mnuExcluirArribaDeMaximo.Checked Then
                                        DeltaLlenado = 0
                                        Continue For
                                    End If
                                ElseIf DeltaLlenado > 0 Then '#EJC20211221: La posición aun no llega al mínimo
                                    If Not mnuExcluirArribaDelMinimo.Checked Then
                                        DeltaLlenado = (vMaximoAuxEnPres - vDisponibleEnUbicacionPres)
                                    Else
                                        '#EJC20211221: Excluir lo que aun no llega al mínimo.
                                        DeltaLlenado = 0
                                        Continue For
                                    End If
                                ElseIf DeltaLlenado = 0 Then
                                    If mnuExcluirArribaDeMaximo.Checked Then
                                        Continue For
                                    End If
                                End If

                            Else

                                DeltaLlenado = (vReabasto.Maximo - 0)

                                If DeltaLlenado < 0 Then '#EJC20211221: La posición tiene sobrestock
                                    '#EJC20211221: Excluir lo que tiene sobre stock.
                                    If mnuExcluirArribaDeMaximo.Checked Then
                                        DeltaLlenado = 0
                                        Continue For
                                    End If
                                ElseIf DeltaLlenado > 0 Then '#EJC20211221: La posición aun no llega al mínimo
                                    If Not mnuExcluirArribaDelMinimo.Checked Then
                                        DeltaLlenado = (vMaximoAuxEnPres - vDisponibleEnUbicacionPres)
                                    Else
                                        '#EJC20211221: Excluir lo que aun no llega al mínimo.
                                        DeltaLlenado = 0
                                        Continue For
                                    End If
                                ElseIf DeltaLlenado = 0 Then
                                    If mnuExcluirArribaDeMaximo.Checked Then
                                        Continue For
                                    End If
                                End If

                            End If

                        End If

                    Else

                        If vDisponibleEnUbicacionPres <= vMinimoAuxEnPres Then
                            DeltaLlenado = (vMaximoAuxEnPres - vDisponibleEnUbicacionPres)
                        Else
                            DeltaLlenado = 0 'A petición de Marcelo #20211122_1720_PANAMÁ
                            If mnuExcluirArribaDeMaximo.Checked Then Continue For
                        End If

                    End If

                End If

                If DeltaLlenado > 0 OrElse DeltaLlenado = 0 Then

                    vReabasto.Cantidad_A_Ubicar = Math.Round(DeltaLlenado, 6)
                    vReabasto.Cantidad_A_Ubicar = vReabasto.Cantidad_A_Ubicar

                    If Not vReabasto.IdPresentacionAbastercerCon = 0 Then

                        If Not BePresentacionAbastecerCon Is Nothing Then

                            If Not BePresentacionAbastecerCon.CajasPorCama = 0 OrElse BePresentacionAbastecerCon.CamasPorTarima = 0 Then

                                vCantidadTarima = BePresentacionAbastecerCon.CajasPorCama * BePresentacionAbastecerCon.CamasPorTarima

                                If BeBodega.Ubicar_Tarimas_Completas_Reabasto Then

                                    If vCantidadTarima > 0 Then

                                        '#EJC20220201: Determinar la cantidad de tarimas (completas) a ubicar para no bajar parciales.
                                        vDeltaTarimasRellenado = vReabasto.Cantidad_A_Ubicar / vCantidadTarima

                                        If vDeltaTarimasRellenado > 0 Then

                                            vCantidadTarimasCompletasAUbicar = Math.Truncate(vDeltaTarimasRellenado)

                                            If vReabasto.Cantidad_A_Ubicar > vCantidadTarima Then

                                                If vReabasto.Cantidad_A_Ubicar > (vCantidadTarimasCompletasAUbicar * vCantidadTarima) Then

                                                    '#EJC20220222:Reducir la cantidad a ubicar a la estiba de una tarima completa.
                                                    'Porque la cantidad supuesta, excede una tarima y se dejaría un parcial en rack.
                                                    vReabasto.Cantidad_A_Ubicar = (vCantidadTarimasCompletasAUbicar * vCantidadTarima)

                                                ElseIf vReabasto.Cantidad_A_Ubicar = (vCantidadTarimasCompletasAUbicar * vCantidadTarima) Then

                                                    '#EJC20220222:No hacer nada, la cantidad a ubicar coincide con tarimas completas.

                                                Else

                                                    '#EJC20220222_2356: Hay que ubicar más de una tarima, 
                                                    'Pero no se si puedo reservar más, me falta analizar esta condición.

                                                    Dim vMsgError As String = "#Ubicar_Tarimas_Completas_Reabasto: La cantidad aubicar es: " & vReabasto.Cantidad_A_Ubicar &
                                                        " El vDeltaTarimasRellenado = " & vDeltaTarimasRellenado &
                                                        " La cantidad de tarimas completas a ubicar es de: " & vCantidadTarimasCompletasAUbicar &
                                                        " (vCantidadTarimasCompletasAUbicar * vCantidadTarima) = " & (vCantidadTarimasCompletasAUbicar * vCantidadTarima)
                                                    clsLnLog_error_wms.Agregar_Error(vMsgError)

                                                End If

                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    If BeStockUbicDestino.IdPresentacion = 0 Then
                        vExistenciaDisponibleUMReabasto = vReabasto.Stock_Disponible
                        vReabasto.Stock_Disponible = vReabasto.Stock_Disponible
                    Else
                        vExistenciaDisponibleUMReabasto = vReabasto.Stock_Disponible
                    End If

                    If vReabasto.IdPresentacionAbastercerCon <> 0 Then
                        If Not BePresentacionAbastecerCon Is Nothing Then
                            vNomUMAUbicar = BePresentacionAbastecerCon.Nombre
                            If Not BePresentacionAbastecerCon.Factor = 0 Then
                                If vReabasto.IdPresentacion = 0 Then
                                    vCantidadRelacionDeFactor = DeltaLlenado 'Math.Round(/ BePresentacionAbastecerCon.Factor, 6)
                                    vCantidadRelacionDeFactor = Math.Ceiling(vCantidadRelacionDeFactor)
                                    vReabasto.Cantidad_A_Ubicar = vCantidadRelacionDeFactor
                                    vReabasto.Factor = BePresentacionAbastecerCon.Factor
                                Else
                                    Debug.WriteLine("creo que aquí falta algo.")
                                End If
                            End If
                        End If
                    End If

                    If vReabasto.IdPresentacionAbastercerCon <> 0 Then
                        If Not (vReabasto.IdPresentacion = 0) Then
                            vReabasto.Stock_Inferior = IIf(vDisponibleEnStockPres < (DeltaLlenado), True, False)
                        Else
                            Dim vDeltaLlennado2 As Double = DeltaLlenado  'Math.Round(DeltaLlenado / BePresentacionAbastecerCon.Factor, 6)
                            vReabasto.Stock_Inferior = IIf(vExistenciaDisponibleUMReabasto < (vDeltaLlennado2), True, False)
                        End If
                    End If

                    If Not vReabasto.IdPresentacionAbastercerCon = 0 Then

                        '#EJC20211216: Aquí me hace falta validar bien
                        If vDisponibleEnStockPres > vCantidadReservadaEnMemoriaPorReabasto Then
                            vDisponibleEnStockPres -= vCantidadReservadaEnMemoriaPorReabasto
                        ElseIf vDisponibleEnStockPres <= vCantidadReservadaEnMemoriaPorReabasto AndAlso (vDisponibleEnStockPres <> 0 AndAlso vCantidadReservadaEnMemoriaPorReabasto <> 0) Then
                            vDisponibleEnStockPres = 0
                        Else
                            vDisponibleEnStockPres -= vCantidadReservadaEnMemoriaPorReabasto
                        End If

                    Else

                        '#EJC20211216: Aquí me hace falta validar bien
                        If vReabasto.Stock_Ubicacion > vCantidadReservadaEnMemoriaPorReabasto Then
                            vReabasto.Stock_Disponible -= vReabasto.Cantidad_A_Ubicar
                        ElseIf vReabasto.Stock_Ubicacion <= vCantidadReservadaEnMemoriaPorReabasto AndAlso (vReabasto.Stock_Ubicacion <> 0 AndAlso vCantidadReservadaEnMemoriaPorReabasto <> 0) Then
                            vReabasto.Stock_Disponible = 0
                        Else
                            vReabasto.Stock_Disponible -= vCantidadReservadaEnMemoriaPorReabasto
                        End If

                    End If

                    If Not vReabasto.Stock_Inferior Then

                        Dim vReabastoExistente As New clsBeReabastoReserva
                        vReabastoExistente.IdProductoBodega = vReabasto.IdProductoBodega
                        vReabastoExistente.IdUnidadMedidaBasica = vReabasto.IdUnidadMedidaBasica
                        vReabastoExistente.IdProductoEstado = vReabasto.IdProductoEstado
                        vReabastoExistente.CantidadReabasto = vReabasto.Cantidad_A_Ubicar
                        vReabastoExistente.CodigoProducto = vReabasto.Codigo_Producto
                        vReabastoExistente.IdBodega = vReabasto.IdBodega
                        vReabastoExistente.IdUbicacion = vReabasto.IdUbicacion
                        vReabastoExistente.IdPresentacion = vReabasto.IdPresentacion
                        vReabastoExistente.IdPresentacionAbastecerCon = vReabasto.IdPresentacionAbastercerCon
                        vReabastoExistente.IdRellenado = vReabasto.IdRellenado
                        ListaReabastoReserva.Add(vReabastoExistente)

                    Else
                        '#EJC20211221: Excluir los reabastos donde la cantidad a ubicar es menor que el disponible.
                        If mnuExcluirStockInferior.Checked Then
                            Continue For
                        End If
                    End If

                    lMaximo = Math.Round(vReabasto.Maximo, 2)
                    lDisp = Math.Round(vReabasto.Stock_Disponible, 2)

                    '#CKFK 20210223 Agregué la bodega a la funciónGet_Nombre_Completo_By_IdUbicacion
                    'NomUbic = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vReabasto.IdUbicacion, vReabasto.IdBodega)
                    NomUbic = vReabasto.Ubicacion

                    '#EJC20210303: Si no existen tareas enviadas de reabasto a la HH entonces, se pueden agregar al monitor de pendientes.
                    'Esta lista determina si la HH ya procesó o no la tarea, hasta que la HH no la procese, se seguirá mostrando en el monitor.
                    vIdxListReabastosPendientesProcesar = ListReabastosPendientesProcesar.FindIndex(Function(x) x.IdBodega = AP.IdBodega _
                                                                                                    AndAlso x.IdRellenado = vReabasto.IdRellenado _
                                                                                                    AndAlso x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                                                    AndAlso x.IdUbicacion = vReabasto.IdUbicacion)

                    '#EJC20210303: Esta lista, determina si la inexistencia ya se insertó o no previamente en la tabla.
                    vIdxListReabastosGenerados = ListReabastosGenerados.FindIndex(Function(x) x.IdBodega = AP.IdBodega _
                                                                                  AndAlso x.IdRellenado = vReabasto.IdRellenado _
                                                                                  AndAlso x.IdProductoBodega = vReabasto.IdProductoBodega _
                                                                                  AndAlso x.IdUbicacion = vReabasto.IdUbicacion _
                                                                                  AndAlso x.IdTareaUbicacionEnc <> 0)


                    If vIdxListReabastosPendientesProcesar <> -1 Then

                        Dim BeReabastoPendientesProcesar As New clsBeTrans_reabastecimiento_log
                        BeReabastoPendientesProcesar = ListReabastosPendientesProcesar(vIdxListReabastosPendientesProcesar)

                        If Not BeReabastoPendientesProcesar Is Nothing Then

                            BeTareaUbicacion = clsLnTrans_ubic_hh_enc.GetSingle(BeReabastoPendientesProcesar.IdTareaUbicacionEnc,
                                                                                clsTransaccion.lConnection,
                                                                                clsTransaccion.lTransaction)


                            If Not BeTareaUbicacion Is Nothing Then

                                lBeTareaUbicacionDet = clsLnTrans_ubic_hh_det.Get_All_By_Id_Trans_Ubic_Enc(BeReabastoPendientesProcesar.IdTareaUbicacionEnc,
                                                                                                           clsTransaccion.lConnection,
                                                                                                           clsTransaccion.lTransaction)

                                If Not lBeTareaUbicacionDet Is Nothing Then
                                    If lBeTareaUbicacionDet.Count > 0 Then
                                        BeTareaUbicacion.Nombre_Operador = lBeTareaUbicacionDet.FirstOrDefault.Operador.Nombres
                                    End If
                                End If

                                DTGridTareasPendientesReabasto.Rows.Add(BeTareaUbicacion.IdTareaUbicacionEnc,
                                                                        BeReabastoPendientesProcesar.Codigo_Producto,
                                                                        BeReabastoPendientesProcesar.Ubicacion,
                                                                        BeTareaUbicacion.Nombre_Operador)

                                If Not dgridTareasPendientesReabasto.Visible OrElse DTGridTareasPendientesReabasto.Rows.Count > 0 Then
                                    dgridTareasPendientesReabasto.Visible = True
                                    dgridTareasPendientesReabasto.BringToFront()
                                End If

                                Continue For

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                    If vReabasto.Cantidad_A_Ubicar > 0 OrElse vReabasto.Cantidad_A_Ubicar = 0 Then

                        vIdTareaUbicacionHH = 0

                        '#EJC20210303: Si no existen tareas enviadas de reabasto a la HH entonces, se pueden agregar al monitor de pendientes.
                        'If Not ListReabastosPendientesProcesar.Count > 0 Then
                        If vIdxListReabastosPendientesProcesar = -1 Then

                            '#EJC20210303: Si no se han insertado previamente...
                            'If ListReabastosGenerados.Count = 0 Then
                            If vIdxListReabastosGenerados = -1 Then

                                '#EJC20211230
                                Dim vExisteReabastecimientoParaHoy As Boolean = clsLnTrans_reabastecimiento_log.Existe_IdReabastecimientoLog(vReabasto.IdReabastecimientoLog,
                                                                                                                                            vReabasto.IdRellenado,
                                                                                                                                            Now,
                                                                                                                                            clsTransaccion.lConnection,
                                                                                                                                            clsTransaccion.lTransaction)

                                If Not vExisteReabastecimientoParaHoy Then
                                    vReabasto.IdReabastecimientoLog = 0
                                End If

                                If vReabasto.IdReabastecimientoLog = 0 Then
                                    vReabasto.IdReabastecimientoLog = vIdMaxReabastecimientoLog
                                    vReabasto.IdReabastecimientoLog = vIdMaxReabastecimientoLog
                                End If

                                vReabasto.Fecha_Generacion_Inexistencia = New Date(Now.Year,
                                                                                   Now.Month,
                                                                                   Now.Day)
                                vReabasto.Hora_Generacion_Inexistencia = Now
                                vReabasto.Procesado_HH = False

                                Try

                                    If Not clsLnTrans_reabastecimiento_log.Existe_IdReabastecimientoLog(vReabasto.IdReabastecimientoLog,
                                                                                                        vReabasto.IdRellenado,
                                                                                                        clsTransaccion.lConnection,
                                                                                                        clsTransaccion.lTransaction) Then

                                        vReabasto.IdReabastecimientoLog = vIdMaxReabastecimientoLog

                                        clsLnTrans_reabastecimiento_log.Insertar(vReabasto,
                                                                                 clsTransaccion.lConnection,
                                                                                 clsTransaccion.lTransaction)

                                        vIdMaxReabastecimientoLog += 1

                                    Else

                                        '#CKFK20230223 Puse esto en comentario porque cuando ya existe no se debe sobre escribir el 
                                        'IdReabastecimientoLog
                                        ' vReabasto.IdReabastecimientoLog = vIdMaxReabastecimientoLog

                                        vReabasto.Fec_mod = Now

                                        clsLnTrans_reabastecimiento_log.Actualizar(vReabasto,
                                                                               clsTransaccion.lConnection,
                                                                               clsTransaccion.lTransaction)

                                    End If


                                Catch ex As Exception
                                    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                                End Try


                                Debug.WriteLine("IdReabastoLog: " & vReabasto.IdReabastecimientoLog & " IdRellenado: " & vReabasto.IdRellenado)

                            Else
                                vIdTareaUbicacionHH = ListReabastosGenerados(vIdxListReabastosGenerados).IdTareaUbicacionEnc
                            End If

                            If (vReabasto.Cantidad_A_Ubicar = 0) Then
                                vReabasto.Stock_Inferior = True
                            End If
                            If vReabasto.Pickeado > 0 Then
                                Debug.Write("espera")
                            End If
                            If vIdTareaUbicacionHH = 0 Then

                                DTGridDetalleRellenado.Rows.Add(False,
                                                              vReabasto.Codigo_Producto,
                                                              vReabasto.Nombre_Producto,
                                                              vReabasto.NombreUmBas,
                                                              vReabasto.Presentacion,
                                                              vReabasto.Estado,
                                                              vReabasto.Minimo,
                                                              vReabasto.Maximo,
                                                              IIf(vReabasto.IdPresentacion = 0, vReabasto.CantidadSFUbicDestino, vDisponibleEnUbicacionPres),
                                                              IIf(vReabasto.IdPresentacionAbastercerCon = 0, vReabasto.Stock_Disponible, vDisponibleEnStockPres),
                                                              Math.Round(vReabasto.Cantidad_A_Ubicar, 2),
                                                              vNomUMAUbicar,
                                                              vReabasto.Stock_Inferior,
                                                              vReabasto.IdPropietarioBodega,
                                                              vReabasto.IdProductoBodega,
                                                              vReabasto.IdPresentacion,
                                                              vReabasto.IdProductoEstado,
                                                              vReabasto.IdUnidadMedidaBasica,
                                                              vReabasto.IdPropietario,
                                                              vReabasto.IdBodega,
                                                              vReabasto.IdUbicacion,
                                                              vReabasto.IdUbicacion.ToString & " - " & NomUbic,
                                                              vReabasto.IdRellenado,
                                                              vReabasto.IdReabastecimientoLog,
                                                              vReabasto.Factor,
                                                              vReabasto.IdTareaUbicacionEnc,
                                                              0, 'IdOperadorBodega
                                                              "", 'Operador
                                                              vReabasto.Pickeado)


                                fila += 1

                            End If

                            Application.DoEvents()

                        Else

                            Debug.WriteLine("alguna condición no se cumplió " & vIdxListReabastosPendientesProcesar)

                            'Math.Round(vReabasto.Stock_Ubicacion, 2)
                            DTGridDetalleRellenado.Rows.Add(False,
                                                            vReabasto.Codigo_Producto,
                                                            vReabasto.Nombre_Producto,
                                                            vReabasto.NombreUmBas,
                                                            vReabasto.Presentacion,
                                                            vReabasto.Estado,
                                                            vReabasto.Minimo,
                                                            vReabasto.Maximo,
                                                            IIf(vReabasto.IdPresentacion = 0, vReabasto.CantidadSFUbicDestino, vDisponibleEnUbicacionPres),
                                                            IIf(vReabasto.IdPresentacionAbastercerCon = 0, vReabasto.Stock_Disponible, vDisponibleEnStockPres),
                                                            Math.Round(vReabasto.Cantidad_A_Ubicar, 2),
                                                            vNomUMAUbicar,
                                                            vReabasto.Stock_Inferior,
                                                            vReabasto.IdPropietarioBodega,
                                                            vReabasto.IdProductoBodega,
                                                            vReabasto.IdPresentacion,
                                                            vReabasto.IdProductoEstado,
                                                            vReabasto.IdUnidadMedidaBasica,
                                                            vReabasto.IdPropietario,
                                                            vReabasto.IdBodega,
                                                            vReabasto.IdUbicacion,
                                                            vReabasto.IdUbicacion.ToString() & " - " & NomUbic,
                                                            vReabasto.IdRellenado,
                                                            vReabasto.IdReabastecimientoLog,
                                                            vReabasto.Factor,
                                                            vReabasto.IdTareaUbicacionEnc,
                                                            0, 'IdOperadorBodega
                                                            "", 'Operador
                                                              vReabasto.Pickeado)

                        End If

                        Application.DoEvents()

                    Else
                        Debug.WriteLine("La cantidad a ubicar es 0, por alguna condición.")
                    End If

                Else
                    Debug.WriteLine("Codigo_Producto: " & vReabasto.Codigo_Producto & " Dispo: " & vReabasto.CantidadPresDispo &
                                    " CantidadSfUbicDestino: " & vReabasto.CantidadSFUbicDestino &
                                    " Abastecer con: " & vReabasto.IdPresentacionAbastercerCon &
                                    " Abastecer a: " & vReabasto.IdPresentacion)
                    Debug.WriteLine("No hay producto en la bodega para abastecer la regla de rellenado.")
                End If

                prg.PerformStep()
                prg.Update()

                Application.DoEvents()

            Next

            Application.DoEvents()

            gvdRellenado.OptionsView.ShowFooter = True

            dgridTareasPendientesReabasto.Visible = (DTGridTareasPendientesReabasto.Rows.Count > 0)

            If gvdRellenado.Columns.Count > 0 Then

                gvdRellenado.BestFitColumns()

                gvdRellenado.Columns("Minimo").SummaryItem.SummaryType = SummaryItemType.Sum
                gvdRellenado.Columns("Minimo").SummaryItem.DisplayFormat = "{0:n2}"
                gvdRellenado.Columns("Minimo").DisplayFormat.FormatType = FormatType.Numeric
                gvdRellenado.Columns("Minimo").DisplayFormat.FormatString = "{0:n2}"
                gvdRellenado.Columns("Maximo").SummaryItem.SummaryType = SummaryItemType.Sum
                gvdRellenado.Columns("Maximo").SummaryItem.DisplayFormat = "{0:n2}"
                gvdRellenado.Columns("Maximo").DisplayFormat.FormatType = FormatType.Numeric
                gvdRellenado.Columns("Maximo").DisplayFormat.FormatString = "{0:n2}"
                gvdRellenado.Columns("DispUbic").SummaryItem.SummaryType = SummaryItemType.Sum
                gvdRellenado.Columns("DispUbic").SummaryItem.DisplayFormat = "{0:n2}"
                gvdRellenado.Columns("DispUbic").DisplayFormat.FormatType = FormatType.Numeric
                gvdRellenado.Columns("DispUbic").DisplayFormat.FormatString = "{0:n2}"
                gvdRellenado.Columns("DispBodega").SummaryItem.SummaryType = SummaryItemType.Sum
                gvdRellenado.Columns("DispBodega").SummaryItem.DisplayFormat = "{0:n2}"
                gvdRellenado.Columns("DispBodega").DisplayFormat.FormatType = FormatType.Numeric
                gvdRellenado.Columns("DispBodega").DisplayFormat.FormatString = "{0:n2}"
                gvdRellenado.Columns("CantUbicar").SummaryItem.SummaryType = SummaryItemType.Sum
                gvdRellenado.Columns("CantUbicar").SummaryItem.DisplayFormat = "{0:n2}"
                gvdRellenado.Columns("CantUbicar").DisplayFormat.FormatType = FormatType.Numeric
                gvdRellenado.Columns("CantUbicar").DisplayFormat.FormatString = "{0:n2}"
                gvdRellenado.Columns("IdPropietarioBodega").Visible = False
                gvdRellenado.Columns("IdProductoBodega").Visible = False
                gvdRellenado.Columns("IdPresentacion").Visible = False
                gvdRellenado.Columns("IdProductoEstado").Visible = False
                gvdRellenado.Columns("IdUnidadMedida").Visible = False
                gvdRellenado.Columns("IdPropietario").Visible = False
                gvdRellenado.Columns("IdBodega").Visible = False
                gvdRellenado.Columns("IdUbicacion").Visible = False
                gvdRellenado.Columns("NomUbic").Visible = True
                gvdRellenado.Columns("Codigo").SummaryItem.SummaryType = SummaryItemType.Count
                gvdRellenado.Columns("Codigo").SummaryItem.DisplayFormat = "Reg.: {0:n0}"

            End If

            If gvReabastoPendiente.Columns.Count > 0 Then
                gvReabastoPendiente.BestFitColumns()
            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            prg.EditValue = 0
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
            prg.Visible = False
            dgridRellenado.ResumeLayout()
            dgridTareasPendientesReabasto.ResumeLayout()
            IsLoading = False
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gvReabastoPendiente_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvReabastoPendiente.RowStyle

        Try

            gvReabastoPendiente.OptionsBehavior.Editable = False
            gvReabastoPendiente.OptionsSelection.EnableAppearanceFocusedCell = False
            gvReabastoPendiente.FocusRectStyle = DrawFocusRectStyle.RowFocus
            gvReabastoPendiente.OptionsSelection.EnableAppearanceFocusedRow = True
            gvReabastoPendiente.OptionsSelection.EnableAppearanceHideSelection = True
            gvReabastoPendiente.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gvReabastoPendiente.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gvReabastoPendiente.Appearance.FocusedRow.ForeColor = Color.White
            gvReabastoPendiente.Appearance.SelectedRow.ForeColor = Color.White
            gvReabastoPendiente.Appearance.SelectedRow.Options.UseBackColor = True
            gvReabastoPendiente.Appearance.SelectedRow.Options.UseForeColor = True

            If e.RowHandle >= 0 Then

                Try

                    gvReabastoPendiente.OptionsBehavior.Editable = False
                    gvReabastoPendiente.OptionsSelection.EnableAppearanceFocusedCell = False

                    gvReabastoPendiente.FocusRectStyle = DrawFocusRectStyle.RowFocus

                    gvReabastoPendiente.OptionsSelection.EnableAppearanceFocusedRow = True
                    gvReabastoPendiente.OptionsSelection.EnableAppearanceHideSelection = True
                    gvReabastoPendiente.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
                    gvReabastoPendiente.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

                    gvReabastoPendiente.Appearance.FocusedRow.ForeColor = Color.White
                    gvReabastoPendiente.Appearance.SelectedRow.ForeColor = Color.White

                    gvReabastoPendiente.Appearance.SelectedRow.Options.UseBackColor = True
                    gvReabastoPendiente.Appearance.SelectedRow.Options.UseForeColor = True

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

                Dim View As GridView = sender

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

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles DgridTareas.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                Dim vIdTransaccion As Integer = Dr.Item("IdTransaccion")

                If Dr.Item("Tarea") = "Recepción" Then

                    gBeRecepcion = clsLnTrans_re_enc.GetSingle(vIdTransaccion)

                    Cierra_Instancia_Previa(frmRecepcion)

                    With frmRecepcion
                        .Modo = frmRecepcion.TipoTrans.Editar
                        .MdiParent = MdiParent
                        .gBeRecepcionEnc = gBeRecepcion
                        .Show()
                        .Focus()
                    End With

                ElseIf Dr.Item("Tarea") = "Despacho" Then

                    Cierra_Instancia_Previa(frmDespacho)

                    With frmDespacho
                        .Modo = frmDespacho.TipoTrans.Editar
                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Despacho")
                        .BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(vIdTransaccion)
                        SplashScreenManager.CloseForm(False)
                        .WindowState = FormWindowState.Maximized
                        .Show()
                        .Focus()
                    End With

                ElseIf Dr.Item("Tarea") = "Picking" Then

                    Cierra_Instancia_Previa(frmPicking)

                    Dim Pick As New frmPicking
                    Pick.Text = "Picking " & vIdTransaccion
                    Pick.Modo = frmPicking.TipoTrans.Editar
                    Pick.BePickingEnc = clsLnTrans_picking_enc.GetSingle(vIdTransaccion)
                    Pick.MdiParent = MdiParent
                    Pick.WindowState = FormWindowState.Normal
                    Pick.Show()
                    Pick.Focus()

                ElseIf Dr.Item("Tarea") = "Inventario" Then

                    Dim Obj As New clsBeTrans_inv_enc

                    Obj = clsLnTrans_inv_enc.Get_Single_By_IdInventarioEnc(vIdTransaccion)

                    Cierra_Instancia_Previa(frmInventario)

                    With frmInventario
                        .Modo = frmInventario.TipoTrans.Editar
                        .gBeTransInvEnc = Obj
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Maximized
                        .Show()
                        .Focus()
                    End With

                ElseIf Dr.Item("Tarea") = "Ubicación" Then

                    Dim Obj As New clsBeTrans_ubic_hh_enc

                    Obj = clsLnTrans_ubic_hh_enc.GetSingle(vIdTransaccion)

                    Cierra_Instancia_Previa(frmCambioUbicacion)

                    With frmCambioUbicacion
                        .tipoOperacion = IIf(Obj.Cambio_estado, 3, 2)
                        .Modo = frmCambioUbicacion.TipoTrans.Editar
                        .gBeTransubicacionHHEnc = Obj
                        .MdiParent = MdiParent
                        .Show()
                        .Focus()
                    End With

                End If

                GridView1.FocusedRowHandle = lSelectionIndex

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

    Private Sub GridView1_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell

        Dim vTiempoMedioTarea As Double = 0
        Dim BeTiempoTarea As New clsBeTipo_tarea_tiempos

        Try

            Dim vrow As DataRowView = GridView1.GetRow(e.RowHandle)

            If Not vrow Is Nothing Then

                Dim vTipoTarea As String = vrow("Tarea").ToString()
                Dim vProgreso As Double = vrow("Progreso").ToString()
                Dim cell As GridCellInfo = e.Cell

                If vTipoTarea = "Recepción" AndAlso e.Column.Name = "GridColumn9" Then
                    Console.Write("Pausa")
                End If

                If e.Column.Name = "colTTM" Then

                    Dim TiempoTranscurridoEnMinutos As Double = Val(vrow("TTM").ToString())

                    Select Case vTipoTarea

                        Case "Recepción"

                            BeTiempoTarea = lTiempoMedioTipoTarea.Find(Function(x) x.IdTipoTarea = 1)

                            If Not BeTiempoTarea Is Nothing Then
                                vTiempoMedioTarea = BeTiempoTarea.TiempoMedioMinutos
                            Else
                                vTiempoMedioTarea = 60 '1 Hora x defecto
                            End If

                        Case "Picking"

                            BeTiempoTarea = lTiempoMedioTipoTarea.Find(Function(x) x.IdTipoTarea = 8)

                            If Not BeTiempoTarea Is Nothing Then
                                vTiempoMedioTarea = BeTiempoTarea.TiempoMedioMinutos
                            Else
                                vTiempoMedioTarea = 60 '1 Hora x defecto
                            End If

                        Case "Ubicación"

                            BeTiempoTarea = lTiempoMedioTipoTarea.Find(Function(x) x.IdTipoTarea = 2)

                            If Not BeTiempoTarea Is Nothing Then
                                vTiempoMedioTarea = BeTiempoTarea.TiempoMedioMinutos
                            Else
                                vTiempoMedioTarea = 60 '1 Hora x defecto
                            End If

                        Case "Cambio Estado"

                            BeTiempoTarea = lTiempoMedioTipoTarea.Find(Function(x) x.IdTipoTarea = 3)

                            If Not BeTiempoTarea Is Nothing Then
                                vTiempoMedioTarea = BeTiempoTarea.TiempoMedioMinutos
                            Else
                                vTiempoMedioTarea = 60 '1 Hora x defecto
                            End If

                    End Select

                    Dim StadoUnoRate As Double = (vTiempoMedioTarea / 3)
                    Dim StadoDosRate As Double = (vTiempoMedioTarea / 2)
                    Dim StadoTresRate As Double = vTiempoMedioTarea

                    'Dim cell As GridCellInfo = e.Cell


                    If (Not cell Is Nothing) Then

                        If TiempoTranscurridoEnMinutos >= StadoUnoRate AndAlso TiempoTranscurridoEnMinutos <= StadoDosRate Then

                            Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                            ViewInfo.ContextImage = My.Resources.green_ball
                            ViewInfo.CalcViewInfo(e.Graphics)

                        ElseIf TiempoTranscurridoEnMinutos >= StadoDosRate AndAlso TiempoTranscurridoEnMinutos <= StadoTresRate Then

                            Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                            ViewInfo.ContextImage = My.Resources.yellow_ball
                            ViewInfo.CalcViewInfo(e.Graphics)

                        ElseIf TiempoTranscurridoEnMinutos >= StadoTresRate Then

                            Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                            ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_3.png") 'My.Resources.red_ball
                            ViewInfo.ContextImage = My.Resources.red_ball
                            ViewInfo.CalcViewInfo(e.Graphics)

                        Else

                        End If


                    End If

                    'Application.DoEvents()

                ElseIf e.Column.Name = "colProgreso" OrElse e.Column.Name = "GridColumn9" Then

                    Dim StadoUnoRate As Double = (100 / 4)
                    Dim StadoDosRate As Double = (100 / 2)

                    vProgreso = vProgreso * 100

                    If vProgreso <> 0 Then

                    End If

                    If vProgreso >= 0 AndAlso vProgreso <= 20 Then

                        Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                        ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_5.png")
                        ViewInfo.CalcViewInfo(e.Graphics)

                    ElseIf vProgreso >= 21 AndAlso vProgreso <= 40 Then

                        Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                        ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_4.png")
                        ViewInfo.CalcViewInfo(e.Graphics)

                    ElseIf vProgreso >= 41 AndAlso vProgreso <= 60 Then

                        Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                        ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_3.png")
                        ViewInfo.CalcViewInfo(e.Graphics)

                    ElseIf vProgreso >= 61 AndAlso vProgreso <= 80 Then


                        Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                        ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_2.png")
                        ViewInfo.CalcViewInfo(e.Graphics)

                    ElseIf vProgreso >= 81 AndAlso vProgreso <= 100 Then

                        Dim ViewInfo As TextEditViewInfo = cell.ViewInfo
                        ViewInfo.ContextImage = Helpers.IconSetImageLoader.GetDefault(DgridTareas.LookAndFeel).GetImage("Rating5_1.png")
                        ViewInfo.CalcViewInfo(e.Graphics)

                    End If

                End If

            End If

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

    End Sub

    'Private Sub dtpFechaInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaInicio.ValueChanged

    '    Try

    '        Dim fechaInicio As Date = dtpFechaInicio.Value.Date
    '        Dim fechaFin As Date = dtpFechaFin.Value.Date

    '        If fechaInicio > fechaFin Then
    '            Throw New Exception("Seleccione un rango de fechas válido.")
    '        End If

    '        Task.Run(Sub()
    '                     If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)
    '                 End Sub)

    '        Task.Run(Sub()
    '                     If IsHandleCreated Then BeginInvoke(Call_Set_Indicador_Ocupacion_Bodega)
    '                 End Sub)

    '        Task.Run(Sub()
    '                     If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)
    '                 End Sub)

    '        Task.Run(Sub()
    '                     If IsHandleCreated Then BeginInvoke(Call_Set_Indicador_Ocupacion_Bodega)
    '                 End Sub)

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '    Finally
    '        EstoyOcupaoChico = False
    '    End Try

    'End Sub

    Private Sub dtpFechaInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaInicio.ValueChanged

        Try
            Dim fechaInicio As Date = dtpFechaInicio.Value.Date
            Dim fechaFin As Date = dtpFechaFin.Value.Date

            If fechaInicio > fechaFin Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            End If

            If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)

            If Not bgwTareas.IsBusy Then
                bgwTareas.RunWorkerAsync()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            clsLnLog_error_wms.Agregar_Error(ex.Message)
        Finally
            EstoyOcupaoChico = False
        End Try

    End Sub

    Private Sub dtpFechaFin_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaFin.ValueChanged

        Try
            If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)

            If Not bgwTareas.IsBusy Then
                bgwTareas.RunWorkerAsync()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            EstoyOcupaoChico = False
        End Try

    End Sub

    Private Sub tmrTareas_Tick(sender As Object, e As EventArgs) Handles tmrTareas.Tick

        Try

            tmrTareas.Enabled = False

            If IsHandleCreated Then BeginInvoke(Call_Listar_Tareas)

            tmrTareas.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuDashBoard_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDashBoard.ItemClick

        Try

            With frmDashViewer1
                .IdentificadorTablero = frmDashViewer1.pTipoTablero.Tipo_Movimientos
                .MdiParent = Me.MdiParent
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub TabPane1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabPane1.TabIndexChanged

        Try

            Debug.WriteLine("Cambio de index el panel...")


        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub TabPane1_VisibleChanged(sender As Object, e As EventArgs) Handles TabPane1.VisibleChanged
        Debug.WriteLine("Cambio de index el panel...")
    End Sub

    Private Sub TabRellenado_VisibleChanged(sender As Object, e As EventArgs) Handles TabRellenado.VisibleChanged

        Try

            If TabRellenado.PageVisible Then

                mnuReabastecimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                Task.Run(Sub()
                             Task.Run(Sub()
                                          If IsHandleCreated Then BeginInvoke(Call_Listar_Reabastecimiento_Productos)
                                      End Sub)
                         End Sub)

            Else
                mnuReabastecimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuReabastecimiento_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReabastecimiento.ItemClick

        Try

            Imprimir_Vista_Reabastecimiento()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Imprimir_Vista_Reabastecimiento()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

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
            printLink.Component = dgridRellenado
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuCalcularIndicesRotacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCalcularIndicesRotacion.ItemClick

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Calculando índices de rotación...")

            Dim lProductosIndiceRotacion As New List(Of clsBeProductoRotacionBodega)
            lProductosIndiceRotacion = Calcula_Indices_Rotacion()
            dgridInidicesRotacion.DataSource = lProductosIndiceRotacion

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub TabPane1_SelectedPageChanged(sender As Object, e As DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs) Handles TabPane1.SelectedPageChanged

        Try

            Debug.WriteLine("Cambio de index el panel...")

            rpgReabasto.Visible = (TabPane1.SelectedPageIndex = 1)
            chkOcupacionPorArea.Visibility = IIf((TabPane1.SelectedPageIndex = 4), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuExcluirArribaDelMinimo_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExcluirArribaDelMinimo.CheckedChanged

        Try

            If Not IsInitialized Then Exit Sub

            mnuExcluirArribaDelMinimo.Enabled = False : rpgReabasto.Enabled = False

            Listar_Reabastecimiento_Productos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuExcluirArribaDelMinimo.Enabled = True : rpgReabasto.Enabled = True
        End Try

    End Sub

    Private Sub mnuExcluirArribaDeMaximo_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExcluirArribaDeMaximo.CheckedChanged

        Try

            If Not IsInitialized Then Exit Sub

            mnuExcluirArribaDeMaximo.Enabled = False : rpgReabasto.Enabled = False

            Listar_Reabastecimiento_Productos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuExcluirArribaDeMaximo.Enabled = True : rpgReabasto.Enabled = True
        End Try

    End Sub

    Private Sub mnuExcluirSinExistencia_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExcluirSinExistencia.CheckedChanged

        If Not IsInitialized Then Exit Sub

        Try

            mnuExcluirSinExistencia.Enabled = False : rpgReabasto.Enabled = False

            Listar_Reabastecimiento_Productos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuExcluirSinExistencia.Enabled = True : rpgReabasto.Enabled = True
        End Try

    End Sub

    Private Sub mnuExcluirStockInferior_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExcluirStockInferior.CheckedChanged

        If Not IsInitialized Then Exit Sub

        Try

            mnuExcluirStockInferior.Enabled = False : rpgReabasto.Enabled = False

            Listar_Reabastecimiento_Productos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuExcluirStockInferior.Enabled = True : rpgReabasto.Enabled = True
        End Try

    End Sub

    Private Sub mnuTimerMonitor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTimerMonitor.ItemClick

        tmrTareas.Enabled = Not tmrTareas.Enabled

        If tmrTareas.Enabled Then
            mnuTimerMonitor.Caption = "Desactivar actualización automática"
        Else
            mnuTimerMonitor.Caption = "Activar actualización automática"
        End If

    End Sub

    Private Sub dvPicking_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs) Handles dvPicking.ConfigureDataConnection

        Try

            Dim connectionParameters As CustomStringConnectionParameters = New CustomStringConnectionParameters(clsBD.Instancia.CadenaConexionSQLClient)
            e.ConnectionParameters = connectionParameters

            Dim oParam As New MsSqlConnectionParameters()
            oParam.AuthorizationType = MsSqlAuthorizationType.SqlServer
            oParam.ServerName = clsBD.Instancia.Server
            oParam.DatabaseName = clsBD.Instancia.NombreBD
            oParam.UserName = clsBD.Instancia.Usuario
            oParam.Password = clsBD.Instancia.Clave
            e.ConnectionParameters = oParam
            e.ConnectionParameters = connectionParameters

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Procesar_Registro_Tarea_Reabasto_No_Procesada()

        Try

            If (gvReabastoPendiente.RowCount > 0) Then

                Dim Dr As DataRowView = gvReabastoPendiente.GetFocusedRow
                Dim BeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc
                BeTransubicacionHHEnc = clsLnTrans_ubic_hh_enc.GetSingle(Dr.Item("IdTarea"))

                Cierra_Instancia_Previa(frmCambioUbicacion)

                With frmCambioUbicacion
                    .tipoOperacion = pTipoOperacion.CambioUbic
                    .Modo = frmCambioUbicacion.TipoTrans.Editar
                    .InvokeListarTareasUbicacion = AddressOf Listar_Reabastecimiento_Productos
                    .gBeTransubicacionHHEnc = BeTransubicacionHHEnc
                    .InvokeListarUbicHH = AddressOf Listar_Reabastecimiento_Productos
                    .MdiParent = MdiParent
                    .Show()
                    .Focus()
                End With

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dgridTareasPendientesReabasto_DoubleClick(sender As Object, e As EventArgs) Handles dgridTareasPendientesReabasto.DoubleClick
        Procesar_Registro_Tarea_Reabasto_No_Procesada()
    End Sub

    Public Property tipoOperacion As pTipoOperacion

    Enum pTipoOperacion
        CambioUbic = 2
        CambioEst = 3
    End Enum

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

    Private Sub chkOcultarTareasPendientesReabasto_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkOcultarTareasPendientesReabasto.CheckedChanged

        dgridTareasPendientesReabasto.Visible = Not dgridTareasPendientesReabasto.Visible

    End Sub

    Private Sub txtUbicacionesVacias_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles txtUbicacionesVacias.LinkClicked

        Try

            With frmrptUbicacionesVacias
                .MdiParent = Me.MdiParent
                .Show()
                .Focus()
            End With

        Catch ex As Exception

        End Try


    End Sub
    Private Sub ToolTipController1_GetActiveObjectInfo(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo

        Try

            If e.SelectedControl IsNot DgridTareas Then Return

            Dim info As ToolTipControlInfo = Nothing
            Dim view As GridView = TryCast(DgridTareas.GetViewAt(e.ControlMousePosition), GridView)
            If view Is Nothing Then Return
            Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            Dim lOperadoresAsociados As New List(Of String)
            Dim lOperadoresRecepcion As New List(Of clsBeTrans_re_op)
            Dim lOperadoresPicking As New List(Of clsBeTrans_picking_op)
            Dim BeOperadorBodega As New clsBeOperador_bodega

            If hi.HitTest = GridHitTest.RowCell Then

                Dim o As Object = hi.HitTest.ToString() + hi.RowHandle.ToString()
                Dim IdTransaccion As String = view.GetRowCellDisplayText(hi.RowHandle, view.Columns("IdTransaccion"))
                Dim TipoTransaccion As String = view.GetRowCellValue(hi.RowHandle, view.Columns("Tarea"))

                If Not IdTransaccion.Trim = "" Then

                    lOperadoresAsociados = New List(Of String)

                    Select Case TipoTransaccion

                        Case "Recepción"

                            lOperadoresRecepcion = clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(IdTransaccion)

                            If Not lOperadoresRecepcion Is Nothing Then

                                If lOperadoresRecepcion.Count > 0 Then

                                    For Each OpRec In lOperadoresRecepcion

                                        BeOperadorBodega = lOperadoresByBodega.Find(Function(x) x.IdOperadorBodega = OpRec.IdOperadorBodega)

                                        If Not BeOperadorBodega Is Nothing Then
                                            lOperadoresAsociados.Add(BeOperadorBodega.Operador.Nombres + " " + BeOperadorBodega.Operador.Apellidos)
                                        End If

                                    Next

                                End If

                            End If

                        Case "Picking"

                            lOperadoresPicking = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(IdTransaccion)

                            If Not lOperadoresPicking Is Nothing Then

                                If lOperadoresPicking.Count > 0 Then

                                    For Each OpPick In lOperadoresPicking

                                        BeOperadorBodega = lOperadoresByBodega.Find(Function(x) x.IdOperadorBodega = OpPick.IdOperadorBodega)

                                        If Not BeOperadorBodega Is Nothing Then
                                            lOperadoresAsociados.Add(BeOperadorBodega.Operador.Nombres + " " + BeOperadorBodega.Operador.Apellidos)
                                        End If

                                    Next

                                End If

                            End If

                    End Select

                    Dim vCadenaOperadores As String = ""

                    If Not lOperadoresAsociados Is Nothing Then

                        If lOperadoresAsociados.Count > 0 Then

                            For Each OpInTrans In lOperadoresAsociados
                                vCadenaOperadores += OpInTrans & vbNewLine
                            Next

                        End If

                    End If

                    If Not String.IsNullOrEmpty(vCadenaOperadores) Then
                        info = New ToolTipControlInfo(o, vCadenaOperadores)
                    End If

                End If

            End If

            If info IsNot Nothing Then e.Info = info

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkExcluirUbicacionDestinoLlena_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkExcluirUbicacionDestinoLlena.CheckedChanged

        If Not IsInitialized Then Exit Sub

        Try

            chkExcluirUbicacionDestinoLlena.Enabled = False : rpgReabasto.Enabled = False

            txtprgreabasto.Visible = chkExcluirUbicacionDestinoLlena.Checked

            Listar_Reabastecimiento_Productos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            chkExcluirUbicacionDestinoLlena.Enabled = True : rpgReabasto.Enabled = True
        End Try

    End Sub

    Private Sub mnuOcultarLogReabasto_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuOcultarLogReabasto.ItemClick
        txtprgreabasto.Visible = Not txtprgreabasto.Visible
    End Sub
    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork

        Try

            If bgWorker.CancellationPending Then Return

            'Carga bajo demanda: una sola lectura por ejecución del worker.
            'Evita el ciclo permanente con WMI + ping que degradaba el BOF.
            Dim systemInfo As TOMWMSSystemInfo.system_info_bof_wms =
                TOMWMSSystemInfo.Get_System_Info(False)
            bgWorker.ReportProgress(0, systemInfo)

        Catch ex As Exception
            Console.WriteLine("Error al leer memoria RAM: " & ex.Message)
        End Try

    End Sub

    Private Sub bgWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker.ProgressChanged

        Try

            Dim systemInfo As TOMWMSSystemInfo.system_info_bof_wms = e.UserState
            lblOSVersion.Text = $"Sistema Operativo: {systemInfo.OSVersion}"
            lblDiskSpace.Text = $"Espacio en Disco Disponible: {FormatBytes(systemInfo.DiskSpaceAvailable)}"
            lblSerialNumber.Text = $"Número de Serie: {systemInfo.SerialNumber}"
            lblMacAddress.Text = $"Dirección MAC: {systemInfo.MacAddress}"
            lblProcessor.Text = $"Procesador: {systemInfo.ProcessorName}, {systemInfo.ProcessorSpeed}"
            lblUsedMemory.Text = $"Memoria RAM Utilizada: {systemInfo.UsedMemoryPercentage:F2}%"
            lblAvailableMemory.Text = $"Memoria RAM Disponible: {systemInfo.AvailableMemoryPercentage:F2}%"
            lblAppMemoryUsage.Text = $"Uso de Memoria de la Aplicación: {TOMWMSSystemInfo.Get_Application_Memory_Usage() / 1024 / 1024:F2} MB"
            lblDotNetVersion.Text = $"Version .NET Framework: {TOMWMSSystemInfo.get_dot_net_framework_version()}"

        Catch ex As Exception
            Console.WriteLine("Error al ejecutar bgWorker_ProgressChanged: " & ex.Message)
        End Try

    End Sub
    Private Sub Update_Memory_Usage()

        Try

            Dim totalMemoryInMb As Double = TOMWMSSystemInfo.Get_Total_Memory_In_Mb()
            Dim appMemoryUsageInMb As Double = TOMWMSSystemInfo.Get_Application_Memory_Usage() / 1024 / 1024
            Dim appMemoryUsagePercentage As Double = (appMemoryUsageInMb / totalMemoryInMb) * 100
            lblAppMemoryUsage.Text = $"Uso de Memoria de la Aplicación: {appMemoryUsageInMb:F2} MB ({appMemoryUsagePercentage:F2}%)"

        Catch ex As Exception
            Console.WriteLine("UpdateMemoryUsage: " & ex.Message)
        End Try

    End Sub

    Private Function FormatBytes(bytes As Long) As String

        FormatBytes = ""

        Try

            Dim sizes() As String = {"B", "KB", "MB", "GB", "TB"}
            Dim order As Integer = 0

            While bytes >= 1024 AndAlso order < sizes.Length - 1
                order += 1
                bytes = bytes \ 1024
            End While

            FormatBytes = String.Format("{0:0.##} {1}", bytes, sizes(order))

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Function

    Private Sub Configurar_Calendario()

        Try

            ' Configura las propiedades básicas del Scheduler
            SchedulerControl1.Start = Date.Today
            SchedulerControl1.GroupType = SchedulerGroupType.Resource

            Dim lMuelles As New List(Of clsBeBodega_muelles)
            lMuelles = clsLnBodega_muelles.Get_All_By_IdBodega(AP.IdBodega)

            If Not lMuelles Is Nothing Then

                ' Asegúrate de que el Storage está inicializado
                If SchedulerControl1.Storage Is Nothing Then
                    SchedulerControl1.Storage = New SchedulerStorage()
                End If

                For Each M In lMuelles
                    SchedulerControl1.Storage.Resources.Items.Add(SchedulerControl1.Storage.CreateResource(M.IdMuelle, M.Nombre))
                Next

            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub Agregar_Tarea_Calendario(ByVal Mensaje As String,
                                         ByVal Inicio As Date,
                                         ByVal Fin As Date,
                                         ByVal IdMuelle As Integer,
                                         ByVal Idtarea As Integer,
                                         ByVal Ubicacion As String)

        Try

            Dim storage As SchedulerStorage = SchedulerControl1.Storage

            ' Agrega tareas de Recepción y Picking para Muelle 1
            Dim receptionTaskM1 As Appointment = storage.CreateAppointment(AppointmentType.Normal)
            receptionTaskM1.Subject = Mensaje
            receptionTaskM1.Start = Inicio
            receptionTaskM1.End = Fin
            receptionTaskM1.ResourceId = IIf(IdMuelle = 0, 1, IdMuelle) ' ID del recurso Muelle 1
            receptionTaskM1.LabelKey = Idtarea
            receptionTaskM1.Location = Ubicacion

            If Not Existe_Tarea_Calendario(receptionTaskM1) Then
                storage.Appointments.Add(receptionTaskM1)
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try


    End Sub

    Private Function Existe_Tarea_Calendario(tarea As Appointment) As Boolean
        For Each appt As Appointment In SchedulerControl1.Storage.Appointments.Items
            If appt.Subject = tarea.Subject AndAlso appt.ResourceId = tarea.ResourceId Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Llenar_Gauge(valor As String, pGaugue As GaugeControl)
        ' Llenar el DigitalGauge con un valor específico
        If pGaugue.Gauges.Count > 0 Then
            Dim digitalGauge As DigitalGauge = CType(pGaugue.Gauges(0), DigitalGauge)
            digitalGauge.Text = valor
        End If
    End Sub

    Private Sub Set_Indicador_Ocupacion_Por_Area(ByVal idBodega As Integer, ByVal etiquetaBodega As String)

        Try
            Dim vCantUbicacionesVacias As Integer = 0
            Dim vCantUbicacionesOcupadas As Integer = 0

            ' 1) Traer datos
            Dim dt As DataTable = clsLnBodega.GetOcupacionAreaTipoDT(idBodega, vCantUbicacionesVacias, vCantUbicacionesOcupadas)
            If dt Is Nothing Then dt = New DataTable()

            ' Si no hay estructura/filas
            If dt.Columns.Count = 0 OrElse dt.Rows.Count = 0 Then
                ccOcupacion.Series.Clear()
                ArcScaleComponent1.BeginInit()
                ArcScaleComponent1.MinValue = 0
                ArcScaleComponent1.MaxValue = 100
                ArcScaleComponent1.Value = 0
                ArcScaleComponent1.EndInit()

                txtCantidadPosiciones.Text = "0.00"
                txtUbicacionesOcupadas.Text = "0.00"
                txtUbicacionesVacias.Text = "0.00"
                Exit Sub
            End If

            ' 2) Normalizar columnas auxiliares (GENÉRICO: sin GENERAL/FISCAL)
            If Not dt.Columns.Contains("Serie") Then dt.Columns.Add("Serie", GetType(String))
            If Not dt.Columns.Contains("SortEstado") Then dt.Columns.Add("SortEstado", GetType(Integer))
            If Not dt.Columns.Contains("Grupo") Then dt.Columns.Add("Grupo", GetType(String))

            For Each r As DataRow In dt.Rows
                Dim estado As String = If(r.IsNull("Estado"), "", CStr(r("Estado")).Trim())
                r("Grupo") = etiquetaBodega
                r("Serie") = $"{etiquetaBodega} - {estado}"
                r("SortEstado") = If(String.Equals(estado, "Ocupadas", StringComparison.OrdinalIgnoreCase), 0, 1)
            Next

            ' 3) Orden
            Dim dv As New DataView(dt)
            dv.Sort = "Area ASC, SortEstado ASC, Cantidad DESC"

            ' 4) Chart
            ccOcupacion.BeginInit()
            ccOcupacion.Series.Clear()
            ccOcupacion.DataSource = dv

            ccOcupacion.SeriesDataMember = "Serie"
            With ccOcupacion.SeriesTemplate
                .ArgumentDataMember = "Area"
                .ValueDataMembers.Clear()
                .ValueDataMembers.AddRange(New String() {"Cantidad"})
                .ValueScaleType = ScaleType.Numerical

                .View = New SideBySideStackedBarSeriesView()

                Dim lbl As New StackedBarSeriesLabel()
                lbl.TextPattern = "{V:n0}"
                lbl.Position = BarSeriesLabelPosition.Center
                .Label = lbl
                .LabelsVisibility = DefaultBoolean.False
            End With

            ccOcupacion.Legend.Visibility = DefaultBoolean.True
            ccOcupacion.ToolTipEnabled = DefaultBoolean.True
            ccOcupacion.SeriesTemplate.ToolTipPointPattern =
            "Área: {A}" & vbCrLf &
            "Serie: {S}" & vbCrLf &
            "Valor: {V:n0}"

            ccOcupacion.CrosshairEnabled = DefaultBoolean.True
            ccOcupacion.SeriesTemplate.CrosshairLabelPattern = "{S}: {V:n0}"

            Dim diagram = TryCast(ccOcupacion.Diagram, XYDiagram)
            If diagram IsNot Nothing Then
                diagram.AxisY.Title.Text = "Ubicaciones (cantidad)"
                diagram.AxisY.Title.Visibility = DefaultBoolean.True
                diagram.AxisY.Label.TextPattern = "{V:n0}"

                diagram.AxisX.Title.Text = "Área"
                diagram.AxisX.Title.Visibility = DefaultBoolean.True
                diagram.AxisX.Label.Angle = -45
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = True
                diagram.AxisX.QualitativeScaleOptions.AutoGrid = False
                diagram.AxisX.WholeRange.SideMarginsValue = 0.5

                diagram.EnableAxisXScrolling = True
                diagram.EnableAxisXZooming = True
            End If

            ccOcupacion.EndInit()

            ' 5) Colores + grupo apilado (si mañana agregás más bodegas, esto ya sirve)
            For Each s As DevExpress.XtraCharts.Series In ccOcupacion.Series
                Dim v = TryCast(s.View, SideBySideStackedBarSeriesView)
                If v Is Nothing Then Continue For

                v.StackedGroup = etiquetaBodega
                v.BarWidth = 0.8

                Dim esOcupada As Boolean = s.Name.IndexOf("Ocupadas", StringComparison.OrdinalIgnoreCase) >= 0
                v.Color = If(esOcupada, Color.Firebrick, Color.LimeGreen)
            Next

            ' 6) Gauge con % (solo esta bodega)
            Dim ocupadasTotalObj As Object = dt.Compute("SUM(Cantidad)", "Estado = 'Ocupadas'")
            Dim ocupadasTotal As Decimal = If(IsDBNull(ocupadasTotalObj), 0D, Convert.ToDecimal(ocupadasTotalObj))

            Dim totalUbicObj As Object = dt.Compute("SUM(Cantidad)", Nothing)
            Dim totalUbic As Decimal = If(IsDBNull(totalUbicObj), 0D, Convert.ToDecimal(totalUbicObj))

            Dim perc As Single = If(totalUbic <= 0D, 0F, CSng((ocupadasTotal * 100D) / totalUbic))

            ArcScaleComponent1.BeginInit()
            ArcScaleComponent1.MinValue = 0
            ArcScaleComponent1.MaxValue = 100
            ArcScaleComponent1.Value = perc
            ArcScaleComponent1.EndInit()

            ' (Opcional) handler para drilldown por área (si lo usás)
            RemoveHandler ccOcupacion.ObjectSelected, AddressOf ccOcupacion_ObjectSelected_AreaGauge
            AddHandler ccOcupacion.ObjectSelected, AddressOf ccOcupacion_ObjectSelected_AreaGauge

            ' 7) Textos (usa los contadores que ya devuelve tu LN)
            Dim vTotalUbicaciones As Integer = (vCantUbicacionesVacias + vCantUbicacionesOcupadas)

            txtCantidadPosiciones.Text = vTotalUbicaciones.ToString("N2")
            txtUbicacionesOcupadas.Text = vCantUbicacionesOcupadas.ToString("N2")
            txtUbicacionesVacias.Text = vCantUbicacionesVacias.ToString("N2")

        Catch ex As Exception
            XtraMessageBox.Show("Error al graficar ocupación: " & ex.Message, Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkOcupacionPorArea_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkOcupacionPorArea.CheckedChanged

        Task.Run(Sub()
                     If IsHandleCreated Then
                         Me.BeginInvoke(Call_Set_Indicador_Ocupacion_Area_Tipo, AP.IdBodega, $"{AP.Bodega.Nombre}")
                     End If
                 End Sub)
    End Sub

    Private Sub Set_Indicador_Ocupacion_DosBodegas(ByVal idBodegaGeneral As Integer)
        Try

            Dim vCantUbicacionesVacias As Integer = 0
            Dim vCantUbicacionesOcupadas As Integer = 0
            Dim vTotalUbicaciones As Integer = 0

            ' 1) Traer datos de ambas bodegas
            Dim dtG As DataTable = clsLnBodega.GetOcupacionAreaTipoDT(idBodegaGeneral, vCantUbicacionesVacias, vCantUbicacionesOcupadas) ' Tipo = GENERAL
            'Dim dtF As DataTable = clsLnBodega.GetOcupacionAreaTipoDT(idBodegaFiscal)  ' Tipo = FISCAL

            ' Si alguna viene Nothing, crea vacía con mismas columnas
            If dtG Is Nothing Then dtG = New DataTable()
            'If dtF Is Nothing Then dtF = dtG.Clone()

            ' 2) Normalizar columnas auxiliares
            For Each dt In New DataTable() {dtG}
                If Not dt.Columns.Contains("Serie") Then dt.Columns.Add("Serie", GetType(String))
                If Not dt.Columns.Contains("SortEstado") Then dt.Columns.Add("SortEstado", GetType(Integer))
                For Each r As DataRow In dt.Rows
                    ' Asegurar "Tipo" con valores estándar
                    Dim tipo As String = CStr(r("Tipo")).ToUpperInvariant()
                    If tipo <> "GENERAL" AndAlso tipo <> "FISCAL" Then
                        ' Por si la vista devolviera otra cosa, forzamos por Id consultado
                        tipo = If(dt Is dtG, "GENERAL", "FISCAL")
                        r("Tipo") = tipo
                    End If
                    r("Serie") = $"{tipo} - {r("Estado")}"              ' p.ej. "GENERAL - Ocupadas"
                    r("SortEstado") = If(String.Equals(CStr(r("Estado")), "Ocupadas", StringComparison.OrdinalIgnoreCase), 0, 1)
                Next
            Next

            ' 3) Unir ambos en un solo DataTable
            Dim dtAll As DataTable = dtG.Clone()
            If Not dtAll.Columns.Contains("Serie") Then dtAll.Columns.Add("Serie", GetType(String))
            If Not dtAll.Columns.Contains("SortEstado") Then dtAll.Columns.Add("SortEstado", GetType(Integer))
            For Each r As DataRow In dtG.Rows : dtAll.ImportRow(r) : Next
            'For Each r As DataRow In dtF.Rows : dtAll.ImportRow(r) : Next

            ' Si no hay filas, limpia y sal
            If dtAll.Rows.Count = 0 Then
                ccOcupacion.Series.Clear()
                Exit Sub
            End If

            ' 4) Orden (sin CASE en DataView)
            Dim dv As New DataView(dtAll)
            ' Ordena por Área, luego primero Ocupadas (0) y después Vacías (1), y cantidad desc
            dv.Sort = "Area ASC, SortEstado ASC, Cantidad DESC"

            ' 5) Chart
            ccOcupacion.BeginInit()
            ccOcupacion.Series.Clear()
            ccOcupacion.DataSource = dv

            ' Series = (Tipo + Estado) → máx. 4
            ccOcupacion.SeriesDataMember = "Serie"
            With ccOcupacion.SeriesTemplate
                .ArgumentDataMember = "Area"                  ' eje X: Área
                .ValueDataMembers.Clear()
                .ValueDataMembers.AddRange(New String() {"Cantidad"})
                .ValueScaleType = ScaleType.Numerical

                ' Vista apilada lado a lado (dos pilas: GENERAL y FISCAL)
                .View = New SideBySideStackedBarSeriesView()

                ' Etiquetas (center para stacked; desactivadas para menos ruido)
                Dim lbl As New StackedBarSeriesLabel()
                lbl.TextPattern = "{V:n0}"
                lbl.Position = BarSeriesLabelPosition.Center
                lbl.Visible = False
                .Label = lbl
            End With

            ' Leyenda y ayudas
            ccOcupacion.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
            ccOcupacion.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True
            ccOcupacion.SeriesTemplate.ToolTipPointPattern = "Área: {A}" & vbCrLf &
                                                             "Serie: {S}" & vbCrLf &
                                                             "Valor: {V:n0}"
            ccOcupacion.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True
            ccOcupacion.SeriesTemplate.CrosshairLabelPattern = "{S}: {V:n0}"

            ' Ejes y navegación
            Dim diagram = TryCast(ccOcupacion.Diagram, DevExpress.XtraCharts.XYDiagram)
            If diagram IsNot Nothing Then
                diagram.AxisY.Title.Text = "Ubicaciones (cantidad)"
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                diagram.AxisY.Label.TextPattern = "{V:n0}"

                diagram.AxisX.Title.Text = "Área"
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                diagram.AxisX.Label.Angle = -45
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = True
                diagram.AxisX.QualitativeScaleOptions.AutoGrid = False
                diagram.AxisX.WholeRange.SideMarginsValue = 0.5

                diagram.EnableAxisXScrolling = True
                diagram.EnableAxisXZooming = True
            End If

            ' Crear series y agrupar pilas por Tipo (GENERAL vs FISCAL)
            ccOcupacion.RefreshData()
            For Each s As DevExpress.XtraCharts.Series In ccOcupacion.Series
                Dim v = TryCast(s.View, DevExpress.XtraCharts.SideBySideStackedBarSeriesView)
                If v IsNot Nothing Then
                    v.StackedGroup = If(s.Name.StartsWith("GENERAL", StringComparison.OrdinalIgnoreCase), "GENERAL", "FISCAL")
                    v.BarWidth = 0.8
                End If
            Next

            ccOcupacion.EndInit()

            ' 6) Actualizar el gauge con % COMBINADO (GENERAL + FISCAL)
            'Dim ocupadasTotal As Decimal = Convert.ToDecimal(dtAll.Compute("SUM(Cantidad)", "Estado = 'Ocupadas'"))
            Dim result As Object = dtAll.Compute("SUM(Cantidad)", "Estado = 'Ocupadas'")
            Dim ocupadasTotal As Decimal = If(IsDBNull(result), 0D, Convert.ToDecimal(result))
            Dim totalUbic As Decimal = Convert.ToDecimal(dtAll.Compute("SUM(Cantidad)", Nothing))
            Dim perc As Single = If(totalUbic <= 0D, 0F, CSng((ocupadasTotal * 100D) / totalUbic))

            ArcScaleComponent1.BeginInit()
            ArcScaleComponent1.MinValue = 0
            ArcScaleComponent1.MaxValue = 100
            ArcScaleComponent1.Value = perc
            ArcScaleComponent1.EndInit()

            ' (Opcional) Handlers de drill-down por área → actualizar gauge con el % del área
            RemoveHandler ccOcupacion.ObjectSelected, AddressOf ccOcupacion_ObjectSelected_AreaGauge
            AddHandler ccOcupacion.ObjectSelected, AddressOf ccOcupacion_ObjectSelected_AreaGauge

            vTotalUbicaciones = (vCantUbicacionesVacias + vCantUbicacionesOcupadas)

            Dim BeOcupacionBodega As New clsBeDh_ocupacion_bodega
            BeOcupacionBodega.IdOcupacionBodega = clsLnDh_ocupacion_bodega.MaxID() + 1
            BeOcupacionBodega.IdBodega = AP.IdBodega
            BeOcupacionBodega.Cant_ubicaciones_ocupadas = vCantUbicacionesOcupadas
            BeOcupacionBodega.Cant_ubicaciones_vacias = vCantUbicacionesVacias
            BeOcupacionBodega.Fecha = Now
            clsLnDh_ocupacion_bodega.Insertar(BeOcupacionBodega)

            txtCantidadPosiciones.Text = vTotalUbicaciones.ToString("N2")
            txtUbicacionesOcupadas.Text = vCantUbicacionesOcupadas.ToString("N2")
            txtUbicacionesVacias.Text = vCantUbicacionesVacias.ToString("N2")

        Catch ex As Exception
            XtraMessageBox.Show("Error al graficar ocupación (General + Fiscal): " & ex.Message, Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ' Drill-down opcional (click en un área → gauge con % de esa área sobre ambas bodegas)
    Private Sub ccOcupacion_ObjectSelected_AreaGauge(ByVal sender As Object, ByVal e As DevExpress.XtraCharts.HotTrackEventArgs)
        Try
            Dim p = TryCast(e.HitInfo.SeriesPoint, DevExpress.XtraCharts.SeriesPoint)
            If p Is Nothing Then Exit Sub
            Dim area As String = p.Argument
            If String.IsNullOrWhiteSpace(area) Then Exit Sub

            Dim dv As DataView = TryCast(ccOcupacion.DataSource, DataView)
            If dv Is Nothing Then Exit Sub

            Dim expr As String = $"Area = '{area.Replace("'", "''")}'"
            Dim ocupadas As Decimal = Convert.ToDecimal(dv.Table.Compute("SUM(Cantidad)", expr & " AND Estado = 'Ocupadas'"))
            Dim total As Decimal = Convert.ToDecimal(dv.Table.Compute("SUM(Cantidad)", expr))
            Dim perc As Single = If(total <= 0D, 0F, CSng((ocupadas * 100D) / total))

            ArcScaleComponent1.BeginInit()
            ArcScaleComponent1.MinValue = 0
            ArcScaleComponent1.MaxValue = 100
            ArcScaleComponent1.Value = perc
            ArcScaleComponent1.EndInit()
        Catch
            ' Silencioso
        End Try
    End Sub

    Private Function BuildDT_OcupacionGrid(dtSource As DataTable) As DataTable

        Dim dt As New DataTable()

        dt.Columns.Add("Area", GetType(String))
        dt.Columns.Add("Codigo", GetType(String))
        dt.Columns.Add("Ocupadas", GetType(Integer))
        dt.Columns.Add("Vacias", GetType(Integer))
        dt.Columns.Add("Total", GetType(Integer))
        dt.Columns.Add("Porcentaje", GetType(String))

        If dtSource Is Nothing OrElse dtSource.Rows.Count = 0 Then
            Return dt
        End If

        'Agrupar por Área
        Dim areas = dtSource.DefaultView.ToTable(True, "Area")

        Dim totalOcupadas As Integer = 0
        Dim totalVacias As Integer = 0

        For Each rArea As DataRow In areas.Rows

            Dim area As String = rArea("Area").ToString()

            Dim ocupadasObj = dtSource.Compute("SUM(Cantidad)", $"Area='{area}' AND Estado='Ocupadas'")
            Dim vaciasObj = dtSource.Compute("SUM(Cantidad)", $"Area='{area}' AND Estado='Vacías'")

            Dim ocupadas As Integer = If(IsDBNull(ocupadasObj), 0, CInt(ocupadasObj))
            Dim vacias As Integer = If(IsDBNull(vaciasObj), 0, CInt(vaciasObj))

            Dim total As Integer = ocupadas + vacias
            Dim porcentaje As Integer = If(total = 0, 0, CInt((ocupadas * 100) / total))

            totalOcupadas += ocupadas
            totalVacias += vacias

            'Código = las primeras letras o el código de área si existe
            Dim codigo As String = ""
            If dtSource.Columns.Contains("Codigo") Then
                codigo = dtSource.Select($"Area='{area}'")(0)("Codigo").ToString()
            End If

            dt.Rows.Add(
                area,
                codigo,
                ocupadas,
                vacias,
                total,
                porcentaje.ToString() & "%"
            )

        Next

        'Fila TOTAL
        Dim totalGeneral As Integer = totalOcupadas + totalVacias
        Dim porcGeneral As Integer = If(totalGeneral = 0, 0, CInt((totalOcupadas * 100) / totalGeneral))

        dt.Rows.Add(
            "TOTAL",
            "",
            totalOcupadas,
            totalVacias,
            totalGeneral,
            porcGeneral.ToString() & "%"
        )

        Return dt

    End Function

    Private Sub mnuImprimirRepOcupacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimirRepOcupacion.ItemClick
        Dim vacias As Integer = 0
        Dim ocupadas As Integer = 0

        Dim dtOcupacion As DataTable = clsLnBodega.GetOcupacionAreaTipoDT(AP.IdBodega, vacias, ocupadas)

        Dim titulo As String = $"Ocupación de bodega: {AP.IdBodega} - {AP.Bodega.Nombre}"

        Using f As New frmReporteOcupacion(AP.IdBodega, dtOcupacion, titulo)
            f.ShowDialog(Me)   'modal
        End Using
    End Sub

    Private Class IndicadorOcupacionResult
        Public Property Vacias As Integer
        Public Property Ocupadas As Integer
        Public ReadOnly Property Total As Integer
            Get
                Return Vacias + Ocupadas
            End Get
        End Property
    End Class

    Private Sub Pintar_Indicador_Ocupacion(vCantUbicacionesVacias As Integer,
                                       vCantUbicacionesOcupadas As Integer)

        Dim vTotalUbicaciones As Integer = vCantUbicacionesVacias + vCantUbicacionesOcupadas

        ArcScaleComponent1.BeginInit()
        ArcScaleComponent1.MinValue = 0
        ArcScaleComponent1.MaxValue = 100

        If vTotalUbicaciones > 0 Then
            ArcScaleComponent1.Value = (vCantUbicacionesOcupadas * 100D) / vTotalUbicaciones
        Else
            ArcScaleComponent1.Value = 0
        End If

        ArcScaleComponent1.EndInit()

        ccOcupacion.BeginInit()

        Dim series1 As New DevExpress.XtraCharts.Series("Pallet Position", ViewType.Bar)
        series1.Points.Add(New SeriesPoint("Vacías", CDbl(vCantUbicacionesVacias)))
        series1.Points.Add(New SeriesPoint("Ocupadas", CDbl(vCantUbicacionesOcupadas)))
        series1.ValueScaleType = ScaleType.Numerical

        ccOcupacion.Series.Clear()
        ccOcupacion.Series.Add(series1)

        Dim diagram As XYDiagram = TryCast(ccOcupacion.Diagram, XYDiagram)

        If diagram IsNot Nothing Then
            diagram.AxisY.Title.Text = "Ocupación de bodega por ubicación"
            diagram.AxisY.Title.Visibility = DefaultBoolean.True
        End If

        ccOcupacion.EndInit()

        txtCantidadPosiciones.Text = vTotalUbicaciones.ToString("N2")
        txtUbicacionesOcupadas.Text = vCantUbicacionesOcupadas.ToString("N2")
        txtUbicacionesVacias.Text = vCantUbicacionesVacias.ToString("N2")

    End Sub

    Private Sub bgwTareas_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwTareas.RunWorkerCompleted

        If e.Error IsNot Nothing Then
            XtraMessageBox.Show(e.Error.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If TypeOf e.Result Is IndicadorOcupacionResult Then
            Dim result = CType(e.Result, IndicadorOcupacionResult)
            Pintar_Indicador_Ocupacion(result.Vacias, result.Ocupadas)
        ElseIf CStr(e.Result) = "AREA" Then
            Set_Indicador_Ocupacion_DosBodegas(AP.IdBodega)
        End If

    End Sub

    Private Sub bgwTareas_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwTareas.DoWork

        System.Threading.Thread.Sleep(2000)

        Dim result As New IndicadorOcupacionResult()

        clsLnBodega.Get_Indicadores_Ocupacion_By_IdBodega(
            AP.IdBodega,
            result.Vacias,
            result.Ocupadas
        )

        e.Result = result

    End Sub
End Class

