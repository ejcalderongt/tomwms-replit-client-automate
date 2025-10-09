Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmCapturaParametroRecepcionS

    Public pIndex As Integer
    Public pIndexProdPallet As Integer
    Public pIdPropietarioBodega As Integer
    Public pIdRecepcionDet As Integer
    Public pNoLinea As Integer = 0
    Public pFechaRecepcion As Date
    Public pBeProducto As New clsBeProducto
    Public plistBeReDetParametros As List(Of clsBeTrans_re_det_parametros)
    Public pListBeStockRec As List(Of clsBeStock_rec)
    Public pListBeStockSeRec As List(Of clsBeStock_se_rec)
    Private ListBEProductoParametro As New List(Of clsBeProducto_parametros)
    Public pBePresentacionProducto As clsBeProducto_Presentacion = Nothing
    Public pListBeProductoPallet As List(Of clsBeProducto_pallet)
    Public pNumeroLP As String
    Public pIdTipoTransaccion As String
    Public Property pIdPresentacion As Integer = -1

    Public Property IdEmpresa As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property IdPropietario As Integer = 0

    Public BePres As New clsBeProducto_Presentacion
    Public Property IdTipoTransaccion As String = ""
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Function Peso_Correcto() As Boolean

        Try

            Peso_Correcto = False

            If pBeProducto.Peso_recepcion Then

                If txtPesoReferencia.Value > 0 Then

                    If String.IsNullOrEmpty(txtPesoReal.Value) Then
                        Throw New Exception("Ingrese Peso.")
                    ElseIf txtPesoReal.Value > 0 = False Then
                        Throw New Exception("El Peso debe ser mayor a 0.")
                    End If

                End If

                Dim PorcentajeToleranciaPeso As Double = Math.Round(txtPesoReferencia.Value * pBeProducto.Peso_tolerancia, 2) / 100
                Dim PesoMaximoReferencia As Double = Math.Round(txtPesoReferencia.Value + PorcentajeToleranciaPeso, 2)
                Dim PesoMinimoReferencia As Double = Math.Round(txtPesoReferencia.Value - PorcentajeToleranciaPeso, 2)
                Dim ValorPeso As Double = Math.Round(txtPesoReal.Value, 2)

                If Not (ValorPeso >= PesoMinimoReferencia AndAlso ValorPeso <= PesoMaximoReferencia) Then

                    Dim vMensaje As String = String.Format("El peso ingresado es menor a {0} o mayor a {1} (tolerancia permitida en base al peso estadístico). ¿Desea continuar?", PesoMinimoReferencia, PesoMaximoReferencia)

                    If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Peso_Correcto = True
                    Else
                        txtPesoReal.Select(0, txtPesoReal.Text.Length)
                        txtPesoReal.Focus()
                    End If

                Else
                    Peso_Correcto = True
                End If

            Else
                Peso_Correcto = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Private Function Temperatura_Correcta() As Boolean

        Temperatura_Correcta = False

        Try

            If pBeProducto.Temperatura_recepcion AndAlso pBeProducto.Temperatura_referencia <> 0 Then

                If String.IsNullOrEmpty(txtTemperaturaReal.Value) Then
                    Throw New Exception("Ingrese Temperatura.")
                ElseIf txtTemperaturaReal.Value > 0 = False Then
                    Throw New Exception("La temperatura debe ser mayor a 0.")
                End If

                Dim PorcentajeToleranciaTemp As Double = Math.Round(pBeProducto.Temperatura_referencia * pBeProducto.Temperatura_tolerancia, 2) / 100
                Dim TemperaturaMax As Double = Math.Round(pBeProducto.Temperatura_referencia + PorcentajeToleranciaTemp, 2)
                Dim TemperaturaMin As Double = Math.Round(pBeProducto.Temperatura_referencia - PorcentajeToleranciaTemp, 2)
                Dim ValorTemperatura As Double = Math.Round(txtTemperaturaReal.Value, 2)

                If Not (ValorTemperatura >= TemperaturaMin AndAlso ValorTemperatura <= TemperaturaMax) Then

                    Dim vMensaje As String = String.Format("La temperatura ingresada es menor a {0} o mayor a {1} (tolerancia permitida en base a la temperatura estadística). ¿Desea continuar?", TemperaturaMin, TemperaturaMax)

                    If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes = False Then
                        txtTemperaturaReal.Select(0, txtTemperaturaReal.Text.Length)
                        txtTemperaturaReal.Focus()
                    Else
                        Temperatura_Correcta = True
                    End If

                Else
                    Temperatura_Correcta = True
                End If
            Else
                Temperatura_Correcta = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Sub Guardar_Datos()

        Try

            Cursor = Cursors.WaitCursor

            If Peso_Correcto() AndAlso Temperatura_Correcta() Then

                If pIndex = -1 Then

                    Dim BeStock_rec As New clsBeStock_rec With
                        {.IdRecepcionDet = pIdRecepcionDet,
                        .IdPropietarioBodega = pIdPropietarioBodega,
                        .IdProductoBodega = pBeProducto.IdProductoBodega,
                        .IsNew = True
                        }

                    BeStock_rec.IdUnidadMedida = pBeProducto.IdUnidadMedidaBasica

                    If Not pBePresentacionProducto Is Nothing Then

                        BeStock_rec.Presentacion.IdPresentacion = pBePresentacionProducto.IdPresentacion
                        BeStock_rec.IdPresentacion = pBePresentacionProducto.IdPresentacion

                        If pBePresentacionProducto.EsPallet Then
                            'EJC201802
                            BeStock_rec.Cantidad = 1 * pBePresentacionProducto.CamasPorTarima * pBePresentacionProducto.CajasPorCama * pBePresentacionProducto.Factor
                        End If

                        If pBePresentacionProducto.Imprime_barra Then

                            Dim BeProdPallet As New clsBeProducto_pallet With
                                {.IdRecepcionDet = pIdRecepcionDet,
                                .IdPropietarioBodega = pIdPropietarioBodega,
                                .IdProductoBodega = pBeProducto.IdProductoBodega,
                                .IsNew = True
                                }

                            pListBeProductoPallet.Add(BeProdPallet)
                            pIndexProdPallet = pListBeProductoPallet.Count - 1

                        End If

                    Else
                        BeStock_rec.Presentacion.IdPresentacion = 0
                        BeStock_rec.IdPresentacion = 0
                        BeStock_rec.IdUnidadMedida = pBeProducto.IdUnidadMedidaBasica
                    End If

                    BeStock_rec.No_linea = pNoLinea

                    pListBeStockRec.Add(BeStock_rec)

                    pIndex = pListBeStockRec.Count - 1

                End If

                If pIndex >= 0 Then

                    pListBeStockRec(pIndex).Lic_plate = txtLicPlate.Text
                    pListBeStockRec(pIndex).Fecha_Ingreso = dtmFechaIngreso.EditValue

                    If pBeProducto.Fechamanufactura Then
                        pListBeStockRec(pIndex).Fecha_Manufactura = dtmFechaManufactura.EditValue
                    Else
                        pListBeStockRec(pIndex).Fecha_Manufactura = Nothing
                    End If

                    pListBeStockRec(pIndex).Serial = txtSerial.Text.Trim
                    pListBeStockRec(pIndex).Añada = CInt(txtAniada.Text)

                    If pBeProducto.Peso_recepcion Then
                        pListBeStockRec(pIndex).Peso = txtPesoReal.Value
                    Else
                        pListBeStockRec(pIndex).Peso = 0.0
                    End If

                    If pBeProducto.Temperatura_recepcion Then
                        pListBeStockRec(pIndex).Temperatura = txtTemperaturaReal.Value
                    Else
                        pListBeStockRec(pIndex).Temperatura = 0.0
                    End If

                    pListBeStockRec(pIndex).Fec_mod = pFechaRecepcion
                    pListBeStockRec(pIndex).User_mod = AP.UsuarioAp.IdUsuario

                    For Each Obj As clsBeStock_se_rec In pListBeStockSeRec
                        Obj.IdStockRec = pListBeStockRec(pIndex).IdStockRec
                    Next

                    For Each BeProductoParametros As clsBeProducto_parametros In ListBEProductoParametro

                        Dim BeTransReDetParametros As New clsBeTrans_re_det_parametros() With {.IdRecepcionDet = pIdRecepcionDet, .IdProductoParametro = BeProductoParametros.IdProductoParametro}

                        Select Case BeProductoParametros.TipoParametro.Tipo
                            Case "Texto"
                                BeTransReDetParametros.Valor_texto = BeProductoParametros.Valor_texto
                            Case "Númerico"
                                BeTransReDetParametros.Valor_numerico = BeProductoParametros.Valor_numerico
                            Case "Lógico"
                                BeTransReDetParametros.Valor_logico = BeProductoParametros.Valor_logico
                            Case "Fecha"
                                BeTransReDetParametros.Valor_fecha = BeProductoParametros.Valor_fecha
                            Case Else
                                Exit Select
                        End Select

                        BeTransReDetParametros.User_agr = AP.UsuarioAp.IdUsuario
                        BeTransReDetParametros.Fec_agr = pFechaRecepcion
                        BeTransReDetParametros.IsNew = BeProductoParametros.IsNew

                        If BeProductoParametros.IsNew Then
                            plistBeReDetParametros.Add(BeTransReDetParametros)
                        End If

                    Next

                    '#CKFK Agregué esto para agregar los datos en la tabla producto_pallet cuando el LP no es autogenerado
                    If Not pBePresentacionProducto Is Nothing Then

                        If pBePresentacionProducto.Imprime_barra And pBePresentacionProducto.EsPallet Then

                            With pListBeProductoPallet(pIndexProdPallet)
                                .IdPropietarioBodega = pIdPropietarioBodega
                                .IdProductoBodega = pBeProducto.IdProductoBodega
                                .IdOperadorBodega = Nothing
                                .IdPresentacion = BePres.IdPresentacion
                                .IdRecepcionDet = pIdRecepcionDet
                                .Impreso = 0
                                .IdImpresora = 1
                                .Activo = True
                                .Fecha_ingreso = Now
                                .Codigo_Barra = txtLicPlate.Text
                                .Reimpresiones = 0
                                .Fec_agr = Now
                                .Fec_mod = Now
                            End With

                        End If

                    End If

                Else

                    Dim BeStockRec As New clsBeStock_rec

                    ' ESTOS PERFILES DEBERIAN DE VALIDARSE TAMBIEN CUANDO MODIFICAN EL STOCK DE UN DETERMINADO PRODUCTO
                    If pBeProducto.IdPerfilSerializado > 0 Then

                        If pBeProducto.IdPerfilSerializado = 1 Then

                            ' SI ES PERFIL INDIVIDUAL
                            If pListBeStockSeRec IsNot Nothing AndAlso pListBeStockSeRec.Count = 0 Then
                                Throw New Exception("Ingrese al menos una serie.")
                            End If

                        ElseIf pBeProducto.IdPerfilSerializado = 2 Then

                            ' SI ES PERFIL RANGO

                            Dim BeStocSeRec As New clsBeStock_se_rec

                            If pListBeStockSeRec IsNot Nothing AndAlso pListBeStockSeRec.Count > 0 Then
                                BeStocSeRec.IdStockSeRec = pListBeStockSeRec.Max(Function(b) b.IdStockSeRec) + 1
                            Else
                                BeStocSeRec.IdStockSeRec = clsLnStock_se_rec.MaxID() + 1
                            End If

                            BeStocSeRec.NoSerie = String.Empty
                            BeStocSeRec.NoSerieInicial = txtSerieInicial.Value
                            BeStocSeRec.NoSerieFinal = txtSerieFinal.Value
                            BeStocSeRec.User_agr = AP.UsuarioAp.IdUsuario
                            BeStocSeRec.Fec_agr = pFechaRecepcion
                            BeStocSeRec.User_mod = AP.UsuarioAp.IdUsuario
                            BeStocSeRec.Fec_mod = pFechaRecepcion
                            BeStocSeRec.Activo = True
                            BeStocSeRec.IsNew = True
                            pListBeStockSeRec.Add(BeStocSeRec)

                        ElseIf pBeProducto.IdPerfilSerializado = 3 Then

                            ' SI ES PEFIL UNICO
                            If String.IsNullOrEmpty(txtSerial.Text.Trim()) Then
                                Throw New Exception("Ingrese Serial.")
                            End If

                        Else

                            BeStockRec.Serial = txtSerial.Text.Trim
                            If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then

                                Dim lIndex As Integer = -1

                                lIndex = pListBeStockRec.FindIndex(Function(b) b.Serial = BeStockRec.Serial)
                                If lIndex > -1 Then
                                    Throw New Exception(String.Format("El Serial {0} se encuentra ya ingresado.", txtSerial.Text.Trim))
                                End If
                            End If

                        End If

                        If pListBeStockRec IsNot Nothing AndAlso pListBeStockRec.Count > 0 Then
                            BeStockRec.IdStockRec = pListBeStockRec.Max(Function(b) b.IdStockRec) + 1
                        Else
                            BeStockRec.IdStockRec = clsLnStock_rec.MaxID() + 1
                        End If

                        BeStockRec.IdPropietarioBodega = pIdPropietarioBodega
                        BeStockRec.IdProductoBodega = pBeProducto.IdProductoBodega

                        BeStockRec.Lic_plate = txtLicPlate.Text

                        BeStockRec.Fecha_Ingreso = dtmFechaIngreso.EditValue

                        If pBeProducto.Fechamanufactura Then
                            BeStockRec.Fecha_Manufactura = dtmFechaManufactura.EditValue
                        Else
                            BeStockRec.Fecha_Manufactura = Date.Parse("1990-01-01")
                        End If

                        BeStockRec.Serial = txtSerial.Text.Trim
                        BeStockRec.Añada = CInt(txtAniada.Text)
                        BeStockRec.Fec_agr = pFechaRecepcion
                        BeStockRec.User_agr = AP.UsuarioAp.IdUsuario
                        BeStockRec.Fec_mod = pFechaRecepcion
                        BeStockRec.User_mod = AP.UsuarioAp.IdUsuario
                        BeStockRec.IsNew = True
                        BeStockRec.Activo = True
                        BeStockRec.IdRecepcionDet = pIdRecepcionDet

                        If pBeProducto.Peso_recepcion Then
                            BeStockRec.Peso = txtPesoReal.Value
                        Else
                            BeStockRec.Peso = 0.0
                        End If

                        If pBeProducto.Temperatura_recepcion Then
                            BeStockRec.Temperatura = txtTemperaturaReal.Value
                        Else
                            BeStockRec.Temperatura = 0.0
                        End If

                        pListBeStockRec.Add(BeStockRec)

                        For Each bo As clsBeStock_se_rec In pListBeStockSeRec
                            bo.IdStockRec = BeStockRec.IdStockRec
                        Next

                        For Each BeProductoParametro As clsBeProducto_parametros In ListBEProductoParametro

                            Dim BeTransReDetParametros As New clsBeTrans_re_det_parametros() With {.IdRecepcionDet = pIdRecepcionDet, .IdProductoParametro = BeProductoParametro.IdProductoParametro}

                            Select Case BeProductoParametro.TipoParametro.Tipo
                                Case "Texto"
                                    BeTransReDetParametros.Valor_texto = BeProductoParametro.Valor_texto
                                Case "Númerico"
                                    BeTransReDetParametros.Valor_numerico = BeProductoParametro.Valor_numerico
                                Case "Lógico"
                                    BeTransReDetParametros.Valor_logico = BeProductoParametro.Valor_logico
                                Case "Fecha"
                                    BeTransReDetParametros.Valor_fecha = BeProductoParametro.Valor_fecha
                                Case Else
                                    Exit Select
                            End Select

                            BeTransReDetParametros.User_agr = AP.UsuarioAp.IdUsuario
                            BeTransReDetParametros.Fec_agr = pFechaRecepcion
                            BeTransReDetParametros.IsNew = BeProductoParametro.IsNew

                            If BeProductoParametro.IsNew Then
                                plistBeReDetParametros.Add(BeTransReDetParametros)
                            End If

                        Next

                    End If

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("Error al guardar parámetros de recepción: {0}", ex.Message))
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Carga_Parametros_Personalizados()

        Try

            'ListBEProductoParametro la lista se llena en la funcion tiene_parametros_personalizados

            Grid.BeginUpdate()

            If pIndex >= 0 Then

                If ListBEProductoParametro.Count > 0 Then

                    For Each Obj As clsBeTrans_re_det_parametros In plistBeReDetParametros.FindAll(Function(b) b.IdRecepcionDet = pIdRecepcionDet)

                        Dim ObjPR As clsBeProducto_parametros = ListBEProductoParametro.Find(Function(b) b.IdProductoParametro = Obj.IdProductoParametro)

                        If ObjPR IsNot Nothing Then

                            Dim lRow As DataRow = DSPR.DT.NewRow

                            Select Case ObjPR.TipoParametro.Tipo
                                Case "Texto"
                                    lRow.Item("colTexto") = Obj.Valor_texto
                                Case "Númerico"
                                    lRow.Item("colNumerico") = Obj.Valor_numerico
                                Case "Lógico"
                                    lRow.Item("colLogico") = Obj.Valor_logico
                                Case "Fecha"
                                    lRow.Item("colFecha") = Obj.Valor_fecha
                                Case Else
                                    Exit Select
                            End Select

                            If ObjPR.IdParametro > 0 Then
                                lRow.Item("IdParametro") = ObjPR.IdParametro
                            End If

                            If ObjPR.TipoParametro IsNot Nothing Then
                                lRow.Item("colDescripcion") = ObjPR.TipoParametro.Descripcion
                            End If

                            lRow.Item("IdParametroDet") = Obj.IdParametroDet
                            lRow.Item("TipoParametro") = ObjPR.TipoParametro.Tipo

                            DSPR.DT.AddDTRow(lRow)

                        End If

                    Next

                End If

            Else

                If ListBEProductoParametro.Count > 0 Then

                    For Each Obj As clsBeProducto_parametros In ListBEProductoParametro

                        Obj.IsNew = True
                        Dim lRow As DataRow = DSPR.DT.NewRow

                        Select Case Obj.TipoParametro.Tipo
                            Case "Texto"
                                lRow.Item("colTexto") = Obj.Valor_texto
                            Case "Númerico"
                                lRow.Item("colNumerico") = Obj.Valor_numerico
                            Case "Lógico"
                                lRow.Item("colLogico") = Obj.Valor_logico
                            Case "Fecha"
                                lRow.Item("colFecha") = Obj.Valor_fecha
                            Case Else
                                Exit Select
                        End Select

                        lRow.Item("IdParametro") = Obj.IdParametro
                        lRow.Item("colDescripcion") = Obj.TipoParametro.Descripcion
                        lRow.Item("TipoParametro") = Obj.TipoParametro.Tipo
                        DSPR.DT.AddDTRow(lRow)

                    Next

                End If

            End If


            Grid.EndUpdate()

            Grid.ForceInitialize()

            Dim chkParamLogico As RepositoryItemCheckEdit = TryCast(GrdP.Columns("colLogico").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler chkParamLogico.CheckedChanged, AddressOf RepositoryItemCheckEdit1_CheckedChanged

            Dim txtPramTexto As RepositoryItemTextEdit = TryCast(GrdP.Columns("colTexto").RealColumnEdit, RepositoryItemTextEdit)
            AddHandler txtPramTexto.EditValueChanged, AddressOf txtPramTexto_EditValueChanged

            Dim txtParamNumero As RepositoryItemSpinEdit = TryCast(GrdP.Columns("colNumerico").RealColumnEdit, RepositoryItemSpinEdit)
            AddHandler txtParamNumero.ValueChanged, AddressOf txtParamNumero_ValueChanged

            Dim txtParamFecha As RepositoryItemDateEdit = TryCast(GrdP.Columns("colFecha").RealColumnEdit, RepositoryItemDateEdit)
            AddHandler txtParamFecha.EditValueChanged, AddressOf txtParamFecha_ValueChanged



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Presentacion()

        Try

            '#EJC202210080928: Buscar presentaciones por IdProductoBodega y no por IdProducto para evitar duplicados en combo de presentación
            Dim vIdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                                                    AP.IdBodega)

            Dim vPresGeneraLicencia As Boolean = False

            If Not vIdProductoBodega = 0 Then

                Dim lPres As New List(Of clsBeProducto_Presentacion)
                lPres = clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProductoBodega(vIdProductoBodega, True)

                cmbPresentacion.DataSource = lPres
                cmbPresentacion.DisplayMember = "Nombre"
                cmbPresentacion.ValueMember = "IdPresentacion"

                If pIdPresentacion <> -1 Then

                    Dim BePres As New clsBeProducto_Presentacion

                    'Buscar la presentación por código ingresado en grid de rec.
                    If pBeProducto.Tag.ToString <> "" Then '#CKFK 20180413 06:31 PM le agregué el .ToString porque daba excepción al realizar la comparación.

                        BePres = lPres.Find(Function(x) x.Codigo = pBeProducto.Tag.ToString) '#CKFK 20180413 06:31 PM le agregué el .ToString porque daba excepción al realizar la búsqueda.

                        If Not BePres Is Nothing Then
                            cmbPresentacion.SelectedValue = pIdPresentacion
                            vPresGeneraLicencia = BePres.Genera_lp_auto
                        End If

                    End If

                    '#EJC20240212: Si no se consigue la presentación por el código de producto en el código de la presentación (caso byb, creo)
                    'Entonces se busca por el IdPresentación pasado a la forma.
                    If BePres Is Nothing Then
                        BePres = lPres.Find(Function(x) x.IdPresentacion = pIdPresentacion)
                    End If

                    If Not BePres Is Nothing Then
                        cmbPresentacion.SelectedValue = pIdPresentacion
                        vPresGeneraLicencia = BePres.Genera_lp_auto
                    End If

                    lblLicPlate.Visible = (pBeProducto.Genera_lp OrElse vPresGeneraLicencia)
                    txtLicPlate.Visible = (pBeProducto.Genera_lp OrElse vPresGeneraLicencia)

                    If pBeProducto.Genera_lp OrElse vPresGeneraLicencia Then
                        'If clsBD.Instancia.Formato_Recepcion = 1 Then
                        'Incrementar_Licencia_BOF(IdBodega, AP.UsuarioAp.IdUsuario)
                        'End If
                        'txtLicPlate.Text = Genera_Licencia_BOF(IdBodega, AP.UsuarioAp.IdUsuario)
                        txtLicPlate.ReadOnly = True
                    Else
                        txtLicPlate.Visible = False
                        lblLicPlate.Visible = False
                    End If

                Else

                    cmbPresentacion.SelectedIndex = -1
                    '#CKFK20240809 Esta inicialización solo aplica cuando la recepción no es ciega
                    'If pIdTipoTransaccion = "MCOC00" Then
                    pBePresentacionProducto = Nothing
                    'End If

                    If pBeProducto.Genera_lp Then
                        txtLicPlate.Text = Genera_Licencia_BOF(IdBodega, AP.UsuarioAp.IdUsuario)
                        txtLicPlate.ReadOnly = True
                    Else
                        txtLicPlate.Visible = False
                        lblLicPlate.Visible = False
                    End If

                    ValidaPeso()

                End If

            Else
                Throw New Exception("El producto no está asociado a la bodega.")
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Function Tiene_Control_Temperatura() As Boolean

        Try

            Return pBeProducto.Temperatura_recepcion

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Function Tiene_Control_Peso() As Boolean

        Try

            Return pBeProducto.Peso_recepcion

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Function Tiene_Control_PorSeries() As Boolean

        Try

            Return pBeProducto.Serializado

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Function Tiene_Presentaciones() As Boolean

        Try

            Return pBeProducto.Presentaciones.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Mostrar_Parametros() As Boolean

        Mostrar_Parametros = False

        Try

            'Mostrar_Parametros = Tiene_Parametros_Personalizados() OrElse
            '                     Captura_Fecha_Manufactura() OrElse
            '                     Captura_Aniada() OrElse
            '                     Genera_LP() OrElse
            '                     Tiene_Control_Peso() OrElse
            '                     Tiene_Control_Temperatura() OrElse
            '                     Tiene_Control_PorSeries() OrElse
            '                     Tiene_Presentaciones()

            Mostrar_Parametros = Tiene_Parametros_Personalizados() OrElse
                                 Captura_Fecha_Manufactura() OrElse
                                 Captura_Aniada() OrElse
                                 Tiene_Control_Peso() OrElse
                                 Tiene_Control_Temperatura() OrElse
                                 Tiene_Control_PorSeries() OrElse
                                 Tiene_Presentaciones()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Function Captura_Fecha_Manufactura() As Boolean

        Try

            Return pBeProducto.Fechamanufactura AndAlso pBeProducto.Materia_prima

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Function

    Private Function Captura_Aniada() As Boolean

        Try

            Return pBeProducto.Capturar_aniada

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Function

    Private Function Tiene_Parametros_Personalizados() As Boolean

        Tiene_Parametros_Personalizados = False

        Try

            ListBEProductoParametro = clsLnProducto_parametros.Get_All_By_IdProducto(pBeProducto.IdProducto, True)

            Return ListBEProductoParametro.Count > 0

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Function

    Private Sub ValidaPeso()

        Try

            If pBeProducto.Peso_recepcion Then
                GrpPeso.Visible = True
                txtPesoReferencia.Value = pBeProducto.Peso_referencia
                txtPesoReal.Tag = pBeProducto.Peso_tolerancia
                lblToleranciaPeso.Text = String.Format("±{0}%", pBeProducto.Peso_tolerancia)
            Else
                GrpPeso.Visible = False
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub ValidaTemperatura()

        Try

            If pBeProducto.Temperatura_recepcion Then
                GrpTemperatura.Visible = True
                txtTemperaturaReferencia.Value = pBeProducto.Temperatura_referencia
                txtTemperaturaReal.Tag = pBeProducto.Temperatura_tolerancia
                lblTempTolerancia.Text = String.Format("±{0}%", pBeProducto.Temperatura_tolerancia)
            Else
                GrpTemperatura.Visible = False
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub validaPerfilSerializado()

        Try

            If pBeProducto.Serializado Then

                Select Case pBeProducto.IdPerfilSerializado

                    Case 1
                        ' PERFIL INDIVIDUAL
                        xtrCapParametros.TabPages.Add(Series)
                        lblSerial.Visible = False
                        txtSerial.Visible = False
                        GrpSeries.Visible = False
                        CargaSeries()
                    Case 2
                        ' PERFIL RANGO
                        xtrCapParametros.TabPages.Remove(Series)
                        lblSerial.Visible = False
                        txtSerial.Visible = False
                        GrpSeries.Visible = True
                    Case 3
                        ' PERFIL UNICO
                        xtrCapParametros.TabPages.Remove(Series)
                        lblSerial.Visible = True
                        txtSerial.Visible = True
                        GrpSeries.Visible = False
                        txtSerial.Text = pBeProducto.Noserie
                    Case Else
                        Exit Select
                End Select

            Else
                lblSerial.Visible = False
                txtSerial.Visible = False
                GrpSeries.Visible = False
            End If


        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub GuardarSerie()

        Try

            If String.IsNullOrEmpty(txtSerialL.Text.Trim) Then
                Throw New Exception("Ingrese Número de Serie.")
            End If

            If pListBeStockSeRec IsNot Nothing Then

                Dim lIndex As Integer = -1

                lIndex = pListBeStockSeRec.FindIndex(Function(b) b.IdStockSeRec = txtSerialL.Tag)

                If lIndex > -1 Then

                    pListBeStockSeRec(lIndex).NoSerie = txtSerialL.Text.Trim
                    pListBeStockSeRec(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pListBeStockSeRec(lIndex).Fec_mod = Now

                Else

                    Dim lIndexN As Integer = pListBeStockSeRec.FindIndex(Function(b) b.NoSerie = txtSerialL.Text.Trim)
                    If lIndexN > -1 Then
                        Throw New Exception(String.Format("El número de serie {0} ya existe.", txtSerialL.Text.Trim))
                    End If

                    Dim BeStockSeRec As New clsBeStock_se_rec

                    If pListBeStockSeRec IsNot Nothing AndAlso pListBeStockSeRec.Count > 0 Then
                        BeStockSeRec.IdStockSeRec = pListBeStockSeRec.Max(Function(m) m.IdStockSeRec) + 1
                    Else
                        BeStockSeRec.IdStockSeRec = clsLnStock_se_rec.MaxID() + 1
                    End If

                    BeStockSeRec.IdProductoBodega = pBeProducto.IdProductoBodega
                    BeStockSeRec.NoSerie = txtSerialL.Text.Trim
                    BeStockSeRec.NoSerieInicial = String.Empty
                    BeStockSeRec.NoSerieFinal = String.Empty
                    BeStockSeRec.User_agr = AP.UsuarioAp.IdUsuario
                    BeStockSeRec.Fec_agr = Now
                    BeStockSeRec.User_mod = AP.UsuarioAp.IdUsuario
                    BeStockSeRec.Fec_mod = Now
                    BeStockSeRec.Activo = True
                    BeStockSeRec.IsNew = True
                    pListBeStockSeRec.Add(BeStockSeRec)

                End If

            End If

            CargaSeries()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            txtSerialL.Text = String.Empty
            txtSerialL.Tag = Nothing
            txtSerialL.Focus()
        End Try

    End Sub

    Private Sub CargaSeries()

        Try

            Dim DT As New DataTable("Serie")
            DT.Columns.Add("IdStockseRec", GetType(Integer))
            DT.Columns.Add("Serie", GetType(String))

            For Each Obj As clsBeStock_se_rec In pListBeStockSeRec.FindAll(Function(b) b.IdProductoBodega = pBeProducto.IdProductoBodega)
                DT.Rows.Add(Obj.IdStockSeRec, Obj.NoSerie)
            Next

            GrdSerie.DataSource = DT

            If GridViewSerie.Columns.Count > 0 Then
                GridViewSerie.Columns("IdStockseRec").Visible = False
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GrdSerie_DoubleClick(sender As Object, e As EventArgs) Handles GrdSerie.DoubleClick

        Try

            txtSerialL.Text = String.Empty

            If (GridViewSerie.RowCount > 0) Then
                Dim Dr As DataRowView = GridViewSerie.GetFocusedRow

                If pListBeStockSeRec IsNot Nothing Then
                    Dim lIndex As Integer = -1
                    lIndex = pListBeStockSeRec.FindIndex(Function(b) b.IdStockSeRec = CInt(Dr.Item("IdStockseRec")))
                    If lIndex > -1 Then
                        txtSerialL.Text = pListBeStockSeRec(lIndex).NoSerie
                        txtSerialL.Tag = pListBeStockSeRec(lIndex).IdStockSeRec
                    End If
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtLicPlate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLicPlate.KeyPress

        '#CKFK 20180212 09:27PM Quité esta validación en txtLicPlate_KeyPress porque el LicensePlate puede es alfanumérico
        'If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
        '    e.Handled = True
        'End If

        'If e.KeyChar = "." Then
        '    e.Handled = True
        'End If

        'If Char.IsDigit(e.KeyChar) Then
        '    e.Handled = False
        'End If

    End Sub

    Private Sub txtAniada_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAniada.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

    End Sub

    Private Sub txtSerialL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerialL.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                cmdGuardar_Click(sender, e)
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try
    End Sub

    Private Sub cmdAcept_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAcept.ItemClick

        Try

            Guardar_Datos()

            DialogResult = DialogResult.OK

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmdCancel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCancel.ItemClick
        If XtraMessageBox.Show("¿Desea Cancelar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub

    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click
        GuardarSerie()
    End Sub

    Private Sub GrdP_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GrdP.RowStyle

        Try

            GrdP.OptionsBehavior.Editable = True
            GrdP.OptionsSelection.EnableAppearanceFocusedCell = False

            GrdP.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GrdP.OptionsSelection.EnableAppearanceFocusedRow = True
            GrdP.OptionsSelection.EnableAppearanceHideSelection = True
            GrdP.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GrdP.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GrdP.Appearance.FocusedRow.ForeColor = Color.White
            GrdP.Appearance.SelectedRow.ForeColor = Color.White

            GrdP.Appearance.SelectedRow.Options.UseBackColor = True
            GrdP.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub GridViewSerie_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridViewSerie.RowStyle

        Try

            GridViewSerie.OptionsBehavior.Editable = False
            GridViewSerie.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewSerie.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridViewSerie.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewSerie.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewSerie.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewSerie.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewSerie.Appearance.FocusedRow.ForeColor = Color.White
            GridViewSerie.Appearance.SelectedRow.ForeColor = Color.White

            GridViewSerie.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewSerie.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub GrdP_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GrdP.ShowingEditor

        Try

            Dim Dr As DataRowView = GrdP.GetFocusedRow
            Dim vTipoParametro As String = Dr.Item("TipoParametro")


            If GrdP.FocusedColumn.FieldName = "colTexto" AndAlso vTipoParametro <> "Texto" Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colNumerico" AndAlso vTipoParametro <> "Númerico" Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colFecha" AndAlso vTipoParametro <> "Fecha" Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colLogico" AndAlso vTipoParametro <> "Lógico" Then
                e.Cancel = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub GrdP_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GrdP.CellValueChanged

        Try

            If GrdP.RowCount > 0 Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                If pIndex >= 0 Then

                    If plistBeReDetParametros IsNot Nothing AndAlso plistBeReDetParametros.Count > 0 Then

                        Dim val As Object = Nothing
                        If e.Column.Caption = "Texto" Then
                            val = Dr.Item("colTexto")
                        ElseIf e.Column.Caption = "Númerico" Then
                            val = Dr.Item("colNumerico")
                        ElseIf e.Column.Caption = "Fecha" Then
                            val = Dr.Item("colFecha")
                        ElseIf e.Column.Caption = "Lógico" Then
                            val = Dr.Item("colLogico")
                        End If

                        lIndex = plistBeReDetParametros.FindIndex(Function(p) p.IdParametroDet = CInt(Dr.Item("IdParametroDet")))

                        If lIndex > -1 Then

                            If String.IsNullOrEmpty(plistBeReDetParametros(lIndex).Valor_texto) = False Then
                                plistBeReDetParametros(lIndex).Valor_texto = val.ToString()
                            End If

                            plistBeReDetParametros(lIndex).Valor_numerico = CDbl(val)
                            plistBeReDetParametros(lIndex).Valor_fecha = CDate(val)
                            plistBeReDetParametros(lIndex).Valor_logico = CBool(val)

                        End If

                    End If

                Else

                    If ListBEProductoParametro IsNot Nothing AndAlso ListBEProductoParametro.Count > 0 Then

                        lIndex = ListBEProductoParametro.FindIndex(Function(p) p.IdParametro = CInt(Dr.Item("IdParametro")))

                        If lIndex > -1 Then

                            Dim vTipoParametro As String = Dr.Item("TipoParametro")
                            Dim val As Object = Nothing

                            If GrdP.FocusedColumn.FieldName = "colTexto" AndAlso vTipoParametro = "Texto" Then
                                val = Dr.Item("colTexto")
                                ListBEProductoParametro(lIndex).Valor_texto = val.ToString
                            ElseIf GrdP.FocusedColumn.FieldName = "colNumerico" AndAlso vTipoParametro = "Númerico" Then
                                val = Dr.Item("colNumerico")
                                ListBEProductoParametro(lIndex).Valor_numerico = CDbl(val)
                            ElseIf GrdP.FocusedColumn.FieldName = "colFecha" AndAlso vTipoParametro = "Fecha" Then
                                val = Dr.Item("colFecha")
                                ListBEProductoParametro(lIndex).Valor_fecha = CDate(val)
                            ElseIf GrdP.FocusedColumn.FieldName = "colLogico" AndAlso vTipoParametro = "Lógico" Then
                                val = Dr.Item("colLogico")
                                ListBEProductoParametro(lIndex).Valor_logico = CBool(val)
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
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub RepositoryItemCheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit1.CheckedChanged

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If ritem IsNot Nothing Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = ListBEProductoParametro.FindIndex(Function(b) b.IdParametro = CInt(Dr.Item("IdParametro")))

                If lIndex > -1 Then
                    If ritem.Checked Then
                        ListBEProductoParametro(lIndex).Valor_logico = True
                    Else
                        ListBEProductoParametro(lIndex).Valor_logico = False
                    End If
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtPramTexto_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemTextEdit2.EditValueChanged

        Try

            Dim TextoGrd As TextEdit = TryCast(sender, TextEdit)

            If TextoGrd IsNot Nothing Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = ListBEProductoParametro.FindIndex(Function(b) b.IdParametro = CInt(Dr.Item("IdParametro")))

                If lIndex > -1 Then
                    ListBEProductoParametro(lIndex).Valor_texto = TextoGrd.Text
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtParamNumero_ValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemSpinEdit1.ValueChanged

        Try

            Dim ValorGrid As SpinEdit = TryCast(sender, SpinEdit)

            If ValorGrid IsNot Nothing Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = ListBEProductoParametro.FindIndex(Function(b) b.IdParametro = CInt(Dr.Item("IdParametro")))

                If lIndex > -1 Then
                    ListBEProductoParametro(lIndex).Valor_numerico = ValorGrid.Value
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtParamFecha_ValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemTimeEdit1.EditValueChanged

        Try

            Dim ValorGrid As TimeEdit = TryCast(sender, TimeEdit)

            If ValorGrid IsNot Nothing Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = ListBEProductoParametro.FindIndex(Function(b) b.IdParametro = CInt(Dr.Item("IdParametro")))

                If lIndex > -1 Then
                    ListBEProductoParametro(lIndex).Valor_fecha = ValorGrid.EditValue
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Set_Peso_ByPresentacion()

        Try

            pBePresentacionProducto = New clsBeProducto_Presentacion With {.IdPresentacion = cmbPresentacion.SelectedValue}
            pBePresentacionProducto = clsLnProducto_presentacion.GetSingle(pBePresentacionProducto.IdPresentacion)
            txtPesoReferencia.Value = pBePresentacionProducto.Peso
            lblToleranciaPeso.Text = String.Format("±{0}%", pBeProducto.Peso_tolerancia)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            '#MECR19092025: Se agrego nueva bitacora para logs de recepcion.
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError,
                                                 AP.UsuarioAp.IdEmpresa,
                                                 AP.IdBodega,
                                                 AP.UsuarioAp.IdUsuario,
                                                 pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Sub cmbPresentacion_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPresentacion.SelectedValueChanged

        Try

            If IsNumeric(cmbPresentacion.SelectedValue) Then

                If cmbPresentacion.SelectedIndex <> -1 Then

                    Set_Peso_ByPresentacion()

                    BePres = New clsBeProducto_Presentacion With {.IdPresentacion = cmbPresentacion.SelectedValue}
                    clsLnProducto_presentacion.GetSingle(BePres)

                    '#GT29012024: esto ya se genera desde la recepcion al cargar la presentación
                    If (BePres.EsPallet OrElse BePres.Permitir_paletizar) OrElse BePres.Genera_lp_auto Then
                        If BePres.Genera_lp_auto Then

                            If clsBD.Instancia.Formato_Recepcion = 1 Then
                                Incrementar_Licencia_BOF(IdBodega, AP.UsuarioAp.IdUsuario)
                            End If

                            txtLicPlate.Text = Genera_Licencia_BOF(IdBodega, AP.UsuarioAp.IdUsuario)
                            txtLicPlate.ReadOnly = True
                        Else
                            txtLicPlate.Text = BePres.Codigo_barra
                            txtLicPlate.ReadOnly = False
                        End If
                    Else
                        txtLicPlate.Text = ""
                        txtLicPlate.ReadOnly = False
                    End If

                    chkPaletizarPres.Visible = (BePres.Permitir_paletizar AndAlso Not BePres.EsPallet)

                    txtCajasPorCama.Value = BePres.CajasPorCama
                    txtCamasPorTarima.Value = BePres.CamasPorTarima

                    chkGeneraLPAuto.Checked = BePres.Genera_lp_auto
                    chkGeneraLPAuto.ReadOnly = True

                    If Not (pBeProducto.Genera_lp) AndAlso Not (BePres.Genera_lp_auto) Then
                        chkGeneraLPAuto.Checked = False
                    End If

                    chkPermitirPaletizar.Checked = BePres.Permitir_paletizar
                    chkPermitirPaletizar.ReadOnly = True

                    lblFactor.Text = String.Format("Factor: {0} ", BePres.Factor)

                Else
                    If pBeProducto.Peso_recepcion Then
                        GrpPeso.Visible = True
                        txtPesoReferencia.Value = pBeProducto.Peso_referencia
                        txtPesoReal.Tag = pBeProducto.Peso_tolerancia
                        lblToleranciaPeso.Text = String.Format("±{0}%", pBeProducto.Peso_tolerancia)
                    Else
                        GrpPeso.Visible = False
                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Sub


    Private Sub Calcular_Licencia()

        Try

            txtLicPlate.Text = clsLnStock_rec.Get_Nuevo_Correlativo_LicensePlate(IdEmpresa,
                                                                                 IdBodega,
                                                                                 IdPropietario,
                                                                                 pBeProducto.IdProducto)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmbPresentacion_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPresentacion.KeyDown
        If e.KeyCode = Keys.Delete Then
            cmbPresentacion.SelectedIndex = -1
            pBePresentacionProducto = Nothing
        End If
    End Sub

    Private Sub frmCapturaParametroRecepcionS_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            xtrCapParametros.TabPages.Remove(Series)

            Carga_Parametros_Personalizados()

            If String.IsNullOrEmpty(pNumeroLP) = False Then
                txtLicPlate.Text = pNumeroLP
            Else
                txtLicPlate.Text = ""
            End If

            If pBeProducto.Fechamanufactura Then
                lblFechaManufactura.Visible = True
                dtmFechaManufactura.Visible = True
            Else
                lblFechaManufactura.Visible = False
                dtmFechaManufactura.Visible = False
            End If

            txtAniada.Text = 0

            If pBeProducto.Capturar_aniada = False Then
                txtAniada.Visible = False
                lblAniada.Visible = False
            Else
                txtAniada.Visible = True
                lblAniada.Visible = True
            End If

            '#GT29012024: esta validacion se hace desde la recepción
            If Not pBeProducto.Genera_lp Then
                lblLicPlate.Visible = False
                txtLicPlate.Visible = False
            Else
                lblLicPlate.Visible = True
                txtLicPlate.Visible = True
            End If

            ValidaPeso()

            Llena_Presentacion()

            ValidaTemperatura()

            validaPerfilSerializado()

            If pIndex >= 0 Then

                If Not (pListBeStockRec(pIndex).Lic_plate = "0" OrElse pListBeStockRec(pIndex).Lic_plate = "") Then
                    txtLicPlate.Text = pListBeStockRec(pIndex).Lic_plate
                End If

                dtmFechaIngreso.EditValue = pListBeStockRec(pIndex).Fecha_Ingreso

                If pBeProducto.Fechamanufactura Then
                    dtmFechaManufactura.EditValue = pListBeStockRec(pIndex).Fecha_Manufactura
                End If

                txtSerial.Text = pListBeStockRec(pIndex).Serial
                txtAniada.Text = pListBeStockRec(pIndex).Añada

                If pBeProducto.Peso_recepcion Then
                    txtPesoReal.Value = pListBeStockRec(pIndex).Peso
                Else
                    txtPesoReal.Value = 0.0
                End If

                If pBeProducto.Temperatura_recepcion Then
                    txtTemperaturaReal.Value = pListBeStockRec(pIndex).Temperatura
                Else
                    txtTemperaturaReal.Value = 0.0
                End If

            Else
                '#EJC202210080930: Fecha actual por defecto en parámetros de recepción ciega BOF.
                dtmFechaIngreso.EditValue = Now
            End If

            CenterToScreen()

            If Not clsBD.Instancia.Formato_Recepcion = 1 Then
                Guardar_Datos()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

End Class