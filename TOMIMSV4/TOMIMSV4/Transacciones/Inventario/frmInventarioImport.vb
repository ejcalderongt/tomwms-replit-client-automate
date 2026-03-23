Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioImport

    Public IdInventario As Integer
    Private dtc, dtp, dtu, dtall As New DataTable
    Private dr() As DataRow

    Public InsertaInv As Boolean = False
    Public IdOperador As Integer = 0
    Public NomOperador As String = ""
    Public DobleVerificacion As Boolean

    Private errc As Integer

    'EFREN07052021 valida si se cargan los productos x propietario especifico o carga todos.
    Public InvTeorico_Multi_Propietario As Boolean
    Public vBodega As Integer = 0
    'EFREN10052021 el propietario bodega se obtiene desde frminventario, si es multipropietario se deja con valor 0
    Public IdPropietarioBodega As Integer = 0
    Public TipoInventario As String = ""
    ''' <summary>
    ''' #EJC20240723: Con cariño para Carolina, por su paciencia en explicarme el requerimiento de lo que habíamos perdido, pero no notado.
    ''' </summary>
    ''' <returns></returns>
    Public Property TipoTeoricoImportacion As pTipoImportacion = pTipoImportacion.WMS

    Public Enum pTipoImportacion
        WMS = 0
        ERP = 1
    End Enum

#Region " Metodos principales "

    Private Sub Paste()

        Dim vArr() As String
        Dim vRow() As String
        Dim vIdxFilaExcel, vIdxColExcel, vIdxColGrid, vR, vRL, vRows As Integer

        lblPrg.Text = ""

        grdData.SuspendLayout()
        grdData.Rows.Clear()

        Try

            vArr = Clipboard.GetText().Split(Environment.NewLine)

            vR = grdData.Rows.Count

            vRows = vArr.Length
            vRow = vArr(0).Split(vbTab)
            grdData.Rows.Add(vRows)

            prg.Maximum = vArr.Length - 1
            prg.Visible = True

            lblPrg.Text = "Pegando datos desde portapapeles"
            lblPrg.Refresh()
            lblPrg.Visible = True

            For vIdxFilaExcel = 0 To vArr.Length - 1

                If vArr(vIdxFilaExcel) <> "" Then

                    lblPrg.Text = "Procesando fila: " & vIdxFilaExcel & " de: " & vArr.Length - 1
                    lblPrg.Refresh()

                    grdData.Item(0, vR).Value = "ERR"

                    Try

                        vRow = vArr(vIdxFilaExcel).Split(vbTab)
                        vIdxColGrid = 1 'Iniciar en columna Estado. 
                        vRL = vRow.Length  'If vRL > 5 Then vRL = 5

                        '#EJC20180528: Utilizar nombres de columnas no índices!!!
                        If vRow(vIdxColExcel).TrimStart <> "" Then
                            grdData.Item("ColId", vR).Value = vIdxFilaExcel + 1
                            grdData.Item("ColEstado", vR).Value = "PROCESANDO"
                            If vRow.Length > 1 Then grdData.Item("ColCodigo", vR).Value = vRow(0).TrimStart
                            If vRow.Length > 2 Then grdData.Item("ColPresentacion", vR).Value = vRow(1).TrimStart
                            If vRow.Length > 3 Then grdData.Item("ColCantidad", vR).Value = vRow(2).TrimStart
                            If vRow.Length > 4 Then grdData.Item("ColPeso", vR).Value = vRow(3).TrimStart
                            If vRow.Length > 5 Then grdData.Item("ColUnidadMedida", vR).Value = vRow(4).TrimStart
                            If vRow.Length > 6 Then grdData.Item("ColLote", vR).Value = vRow(5).TrimStart
                            If vRow.Length > 7 Then grdData.Item("colFechaVence", vR).Value = vRow(6).TrimStart
                            If vRow.Length >= 8 Then grdData.Item("ColUbicacion", vR).Value = vRow(7).TrimStart
                            vR = vR + 1
                        End If

                    Catch ex As Exception
                        grdData.Item(0, vR).Value = ex.Message
                    End Try

                End If

                prg.Value = vIdxFilaExcel

                Application.DoEvents()

            Next

            grdData.Rows.RemoveAt(grdData.Rows.Count - 1)

            prg.Value = 0
            prg.Visible = False

            lblRegs.Caption = "Registros: " & grdData.Rows.Count()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

        grdData.ResumeLayout()

    End Sub

    Private Sub Validar_Datos()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim rc, ii, cc, vprod, vpres, vuni, vIdPropietarioBodega As Integer
        Dim cod, pres, UM, vPeso, vLote, vEstado As String
        Dim vCantidad As String = ""
        Dim Cantidad, Peso As Double
        Dim cantval, pesoval, costo, precio As Boolean
        Dim vFecha_Vence As DateTime = Now
        Dim vIdUnidadMedida As Integer = 0
        Dim BeProducto As New clsBeProducto
        Dim vUbicacion As Integer
        'GT01122021: campos para LP y cod_variante
        Dim vLicensePlate As String = ""
        Dim vCodVariante As String = ""
        Dim vErrorDescription As String = ""
        '#GT24112022_0800: campos DyD
        Dim vCosto As Double
        Dim vPrecio As Double
        Dim vParametro_a As String = ""
        Dim vParametro_b As String = ""
        Dim vColor As String = ""
        Dim vTalla As String = ""

        Dim correlativo_a As Integer
        Dim correlativo_b As Integer

        rc = grdData.Rows.Count  'If rc > 3 Then rc = 3

        lblPrg.Text = ""
        grdData.EndEdit()

        Llena_Catalogos()

        errc = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            prg.Maximum = rc
            prg.Visible = True

            lblPrg.Text = "Validando datos..."
            lblPrg.Refresh()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Procesando archivo...")

            'Application.DoEvents()

            'Dim vCantRegistros As Integer = Carga.lInvenarioTeorico.Rows().Count()

            'For Each ProdInv As DataRow In Carga.lInvenarioTeorico.Rows()

            '    SplashScreenManager.Default.SetWaitFormDescription("Código: " & ProdInv("Codigo") & " " & i & " de " & vCantRegistros)

            For ii = 0 To rc - 1

                correlativo_a = 0
                correlativo_b = 0

                SplashScreenManager.Default.SetWaitFormDescription("Validando fila: " & ii + 1 & " de: " & rc - 1)
                lblPrg.Text = "Validando fila: " & ii + 1 & " de: " & rc - 1
                lblPrg.Refresh()

                If ii = 118 Then
                    '
                End If

                Debug.WriteLine("procesando: " & ii)

                prg.Value = ii : prg.Refresh() : Application.DoEvents()

                For cc = 0 To grdData.ColumnCount - 4
                    grdData.Rows(ii).Cells(cc).Style.BackColor = Color.White
                Next

                vprod = 0 : vpres = 0 : vuni = 0 : cantval = False : pesoval = False : costo = False : precio = False

                vEstado = IIf(IsDBNull(grdData.Rows(ii).Cells("ColEstado").Value), "", grdData.Rows(ii).Cells("ColEstado").Value)
                cod = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo").Value), "", grdData.Rows(ii).Cells("ColCodigo").Value)
                pres = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPresentacion").Value), "", grdData.Rows(ii).Cells("ColPresentacion").Value)

                vLicensePlate = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLp").Value), "", grdData.Rows(ii).Cells("ColLp").Value)
                vCodVariante = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodVariante").Value), "", grdData.Rows(ii).Cells("ColCodVariante").Value)

                Dim pCampos(8) As clsBeProducto.ProdPropiedades
                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pCampos(2) = clsBeProducto.ProdPropiedades.Codigo
                pCampos(3) = clsBeProducto.ProdPropiedades.Propietario
                pCampos(4) = clsBeProducto.ProdPropiedades.UnidadMedida
                pCampos(5) = clsBeProducto.ProdPropiedades.Costo
                pCampos(6) = clsBeProducto.ProdPropiedades.Precio
                pCampos(7) = clsBeProducto.ProdPropiedades.ParametroA
                pCampos(8) = clsBeProducto.ProdPropiedades.ParametroB

                'EFREN 10052021 se utiliza un metodo sobrecargado, el método original no devuelve todas las propiedades de Producto
                BeProducto = clsLnProducto.Get_Single_By_Codigo(cod, pCampos,
                                                                lConnection,
                                                                lTransaction)

                If Not BeProducto Is Nothing Then

                    'EFREN10052021 Se obtiene el nombre comercial y el id propietario
                    'Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeProducto.Propietario.IdPropietario)
                    vIdPropietarioBodega = cmbPropietario.EditValue

                    If vIdPropietarioBodega <> 0 Then

                        'grdData.Rows(ii).Cells("colIdPropietarioBodega").Value = vIdPropietarioBodega
                        'grdData.Rows(ii).Cells("colnombre_propietario").Value = BeProducto.Propietario.Nombre_comercial

                    Else
                        Marcar_Error(ii, "colIdPropietarioBodega", "No se encontró bodega del propietario")
                        Marcar_Error(ii, "colnombre_propietario", "No se encontró nombre del propietario")
                    End If

                    Try
                        vCantidad = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCantidad").Value), "0", grdData.Rows(ii).Cells("ColCantidad").Value)
                    Catch ex As Exception
                        Marcar_Error(ii, "ColCantidad", ex.Message)
                    End Try

                    vPeso = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPeso").Value), "0", grdData.Rows(ii).Cells("ColPeso").Value)
                    UM = IIf(IsDBNull(grdData.Rows(ii).Cells("ColUnidadMedida").Value), "", grdData.Rows(ii).Cells("ColUnidadMedida").Value)
                    vUbicacion = IIf(IsDBNull(grdData.Rows(ii).Cells("ColUbicacion").Value), 0, grdData.Rows(ii).Cells("ColUbicacion").Value)
                    '#GT24112022_0900: campos DyD
                    vCosto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCosto").Value), 0, grdData.Rows(ii).Cells("ColCosto").Value)
                    vPrecio = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPrecio").Value), 0, grdData.Rows(ii).Cells("ColPrecio").Value)
                    vParametro_a = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_a").Value), "", grdData.Rows(ii).Cells("ColParametro_a").Value)
                    vParametro_b = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_b").Value), "", grdData.Rows(ii).Cells("ColParametro_b").Value)
                    vTalla = IIf(IsDBNull(grdData.Rows(ii).Cells("ColTalla").Value), "", grdData.Rows(ii).Cells("ColTalla").Value)
                    vColor = IIf(IsDBNull(grdData.Rows(ii).Cells("ColColor").Value), "", grdData.Rows(ii).Cells("ColColor").Value)

                    If BeProducto.Control_lote Then
                        vLote = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLote").Value), "", grdData.Rows(ii).Cells("ColLote").Value)
                    Else
                        vLote = ""
                    End If

                    grdData.Rows(ii).Cells("ColLote").Value = vLote

                    If BeProducto.Control_vencimiento Then

                        Try
                            vFecha_Vence = IIf(IsDBNull(grdData.Rows(ii).Cells("colFechaVence").Value), "01/01/1900", grdData.Rows(ii).Cells("colFechaVence").Value)
                        Catch ex As Exception
                            Marcar_Error(ii, "ColFechaVence", ex.Message)
                        End Try

                    Else
                        vFecha_Vence = New Date(1900, 1, 1)
                    End If

                    grdData.Rows(ii).Cells("colFechaVence").Value = vFecha_Vence

                    If cod <> "" Then

                        grdData.Rows(ii).Cells("ColEstado").Value = "VALIDADO"

                        If pres <> "" Then vpres = True
                        If vCantidad <> "" Then cantval = True
                        If vPeso <> "" Then pesoval = True
                        If vCosto <> 0 Then costo = True
                        If vPrecio <> 0 Then precio = True

                        If (vCantidad = "") Then vCantidad = "0" : If (vPeso = "") Then vPeso = "0"

                        'EFREN07052021 si es multi propietario, el producto se busca en toda la lista
                        If cod <> "" And InvTeorico_Multi_Propietario Then

                            dr = dtall.Select("Codigo='" & cod & "'")

                            If (dr.Count > 0) Then
                                vprod = dr(0).Item("IdProducto")
                                grdData.Rows(ii).Cells("colIdProducto").Value = vprod
                            Else
                                MsgBox("El código de producto: " & cod & " No existe.", MsgBoxStyle.Exclamation, Text)
                                Marcar_Error(ii, "ColCodigo", "El código no existe en maestro")
                                'cod = ""
                            End If

                        Else
                            dr = dtc.Select("Codigo='" & cod & "'")

                            If (dr.Count > 0) Then
                                vprod = dr(0).Item("IdProducto")
                                grdData.Rows(ii).Cells("colIdProducto").Value = vprod
                            Else
                                MsgBox("El código de producto: " & cod & " No existe.", MsgBoxStyle.Exclamation, Text)
                                Marcar_Error(ii, "ColCodigo", "El código no existe en maestro")
                                'cod = ""
                            End If
                        End If

                        'EFREN 200720211214: La llave es null, pero en procesos, para el inv. se requiere idpresentación
                        ' Presentacion
                        If pres <> "" Then

                            dr = dtp.Select("IdProducto=" & vprod & " AND Nombre='" & pres & "'")

                            If (dr.Count > 0) Then
                                vpres = dr(0).Item("IdPresentacion")
                                grdData.Rows(ii).Cells("ColIdPresentacion").Value = vpres
                            Else

                                Dim vNomPresSinCNP As String = clsPublic.Quitar_Caracteres_No_Permitidos(pres)

                                dr = dtp.Select("IdProducto=" & vprod & " AND Nombre='" & vNomPresSinCNP & "'")

                                If (dr.Count > 0) Then
                                    vpres = dr(0).Item("IdPresentacion")
                                    grdData.Rows(ii).Cells("ColIdPresentacion").Value = vpres
                                Else
                                    '#EJC20180528: No se obtuvo la presentación con el nombre.
                                    Marcar_Error(ii, "ColPresentacion", " Nombre de presentación incorrecto")
                                End If

                            End If
                        Else
                            '#EJC20211116: Se va a recibir en UMBAS
                            grdData.Rows(ii).Cells("ColIdPresentacion").Value = 0
                        End If

                        ' Cantidad
                        '#EJC20180528: Quité try de Jaros para validar la cantidad.
                        Cantidad = IIf(Val(vCantidad) <= 0, 0, Val(vCantidad))

                        ' Unidad medida
                        If UM <> "" Then
                            dr = dtu.Select("Nombre='" & UM & "'")
                            If (dr.Count > 0) Then
                                vuni = dr(0).Item("IdUnidadMedida")
                                grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                            Else
                                If UM = "UN" OrElse UM = "UNI" OrElse UM = "UNIDAD" Then
                                    If Not BeProducto.UnidadMedida Is Nothing Then
                                        If BeProducto.UnidadMedida.Nombre.StartsWith(UM) Then
                                            vuni = BeProducto.UnidadMedida.IdUnidadMedida
                                            grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                                        Else
                                            Marcar_Error(ii, "ColUnidadMedida", "Unidad de medida incorrecta")
                                        End If
                                    Else
                                        Marcar_Error(ii, "ColUnidadMedida", "Unidad de medida incorrecta")
                                    End If
                                End If
                            End If
                        Else
                            '#EJC20180528: Si la UM es vacía se busca por defecto la UMBas del producto ;)
                            vIdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(cod)
                            If vIdUnidadMedida = 0 Then
                                '#EJC20180528: La unidad de medida básica no puede ser vacía coño!!
                                Marcar_Error(ii, "ColUnidadMedida", "El producto requiere una unidad de medida")
                            Else
                                vuni = vIdUnidadMedida
                                grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                                grdData.Rows(ii).Cells("ColUnidadMedida").Value = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(vuni)
                            End If
                        End If

                        'EFREN10112021: Se valida existencia de la idubicación
                        If vUbicacion > 0 Then

                            Dim BeBodegaUbicacion = New clsBeBodega_ubicacion()
                            BeBodegaUbicacion.IdUbicacion = vUbicacion
                            BeBodegaUbicacion.IdBodega = AP.IdBodega
                            clsLnBodega_ubicacion.GetSingle(BeBodegaUbicacion)

                            If BeBodegaUbicacion IsNot Nothing Then
                                grdData.Rows(ii).Cells("ColUbicacion").Value = vUbicacion
                            Else
                                Marcar_Error(ii, "ColUbicacion", "Ubicación no existe en WMS")
                            End If

                        End If

                        '#EJC20180528: Quité try de Jaros para validar el peso.
                        Peso = IIf(Val(vPeso) <= 0, 0, Val(vPeso))

                        '#GT24112022_1730: se valida que parametro a y b correspondan con el registrado, y se envia el ID

                        If vParametro_a <> "" Then

                            If Not BeProducto.ParametroA Is Nothing Then

                                If IsNumeric(vParametro_a) Then
                                    If BeProducto.ParametroA.IdProductoParametroA <> vParametro_a Then
                                        Marcar_Error(ii, "ColParametro_a", "El valor en la columna ParametroA no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_a").Value = BeProducto.ParametroA.IdProductoParametroA
                                    End If
                                Else
                                    If BeProducto.ParametroA.Nombre <> vParametro_a Then
                                        Marcar_Error(ii, "ColParametro_a", "El valor en la columna ParametroA no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_a").Value = BeProducto.ParametroA.IdProductoParametroA
                                    End If
                                End If

                            Else
                                '#GT30112022_0900: si es texto, se usa como nombre del parametro, el codigo sera igual que el id
                                If IsNumeric(vParametro_a) = False Then
                                    Dim parametro_a As New clsBeProducto_parametro_a()
                                    correlativo_a = clsLnProducto_parametro_a.MaxID(lConnection, lTransaction) + 1
                                    parametro_a.IdProductoParametroA = correlativo_a
                                    parametro_a.Codigo = correlativo_a.ToString
                                    parametro_a.Nombre = vParametro_a.Trim
                                    parametro_a.Fec_agr = Now
                                    parametro_a.User_agr = AP.UsuarioAp.IdUsuario
                                    parametro_a.Fec_mod = Now
                                    parametro_a.User_mod = AP.UsuarioAp.IdUsuario
                                    parametro_a.Activo = 1
                                    clsLnProducto_parametro_a.Insertar(parametro_a, lConnection, lTransaction)
                                    grdData.Rows(ii).Cells("ColParametro_a").Value = correlativo_a
                                    'BeProducto.IdProductoParametroA = correlativo_a
                                    'clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)

                                End If

                            End If

                        End If

                        If vParametro_b <> "" Then

                            If Not BeProducto.ParametroB Is Nothing Then

                                If IsNumeric(vParametro_b) Then
                                    If BeProducto.ParametroB.IdProductoParametroB <> vParametro_b Then
                                        Marcar_Error(ii, "ColParametro_b", "El valor en la columna ParametroB no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_b").Value = BeProducto.ParametroB.IdProductoParametroB
                                    End If
                                Else
                                    If BeProducto.ParametroB.Nombre <> vParametro_b Then
                                        Marcar_Error(ii, "ColParametro_b", "El valor en la columna ParametroB no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_b").Value = BeProducto.ParametroB.IdProductoParametroB
                                    End If
                                End If

                            Else
                                '#GT30112022_0900: si es texto, se usa como nombre del parametro, el codigo sera igual que el id
                                If IsNumeric(vParametro_b) = False Then
                                    Dim parametro_b As New clsBeProducto_parametro_b()
                                    correlativo_b = clsLnProducto_parametro_b.MaxID(lConnection, lTransaction) + 1
                                    parametro_b.IdProductoParametroB = correlativo_b
                                    parametro_b.Codigo = correlativo_b.ToString
                                    parametro_b.Nombre = vParametro_b.Trim
                                    parametro_b.Fec_agr = Now
                                    parametro_b.User_agr = AP.UsuarioAp.IdUsuario
                                    parametro_b.Fec_mod = Now
                                    parametro_b.User_mod = AP.UsuarioAp.IdUsuario
                                    parametro_b.Activo = 1
                                    clsLnProducto_parametro_b.Insertar(parametro_b, lConnection, lTransaction)
                                    grdData.Rows(ii).Cells("ColParametro_a").Value = correlativo_b

                                    'BeProducto.IdProductoParametroB = correlativo_b
                                    'clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)

                                End If

                            End If

                        End If

                        If correlativo_a <> 0 OrElse correlativo_b <> 0 Then
                            BeProducto = clsLnProducto.Get_Single_By_Codigo(BeProducto.Codigo, lConnection, lTransaction)
                            BeProducto.IdProductoParametroA = correlativo_a
                            BeProducto.IdProductoParametroB = correlativo_b
                            clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)
                        End If

                        If AP.Bodega.Control_Talla_Color Then

                            If vColor = "" Then
                                Marcar_Error(ii, "ColColor", "Debe ingresar el color del producto")
                            End If

                            If vTalla = "" Then
                                Marcar_Error(ii, "ColTalla", "Debe ingresar la talla del producto")
                            End If

                            If vColor <> "" Then
                                Dim BeColor As clsBeColor = clsLnColor.Get_Single_By_Codigo(vColor, lConnection, lTransaction)
                                If BeColor Is Nothing Then
                                    Marcar_Error(ii, "ColColor", "El valor en la columna color no existe")
                                End If
                            End If

                            If vTalla <> "" Then
                                Dim BeTalla As clsBeTalla = clsLnTalla.Get_Single_By_Codigo(vTalla, lConnection, lTransaction)
                                If BeTalla Is Nothing Then
                                    Marcar_Error(ii, "ColTalla", "El valor en la columna talla no existe")
                                End If
                            End If

                        End If

                    End If

                    Else
                    MsgBox("El código de producto: " & cod & " No existe.", MsgBoxStyle.Exclamation, Text)
                    Marcar_Error(ii, "ColCodigo", "El código no existe en maestro")
                End If

                grdData.CurrentCell = grdData.Rows(ii).Cells(0)

                Application.DoEvents()

            Next

            lTransaction.Commit()

            lblPrg.Text = "Validación finalizada"
            lblPrg.Refresh()

        Catch ex As Exception
            errc = errc + 1
            vErrorDescription = ex.Message
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
        Finally
            prg.Value = 0
            prg.Visible = False
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

        lblPrg.Text = ""

        grdData.ClearSelection()

        If errc > 0 Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error al procesar fila {0}: ", ii + 1) & " " & vErrorDescription, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub Importar_Datos()

        Dim lInventarioTeorico As New List(Of clsBeTrans_inv_stock_prod)
        Dim BeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod
        Dim rc, ii, vIdProducto, vIdPresentacion, vIdUnidadMedida As Integer
        Dim Cantidad, Peso As Double
        Dim vLote As String = ""
        Dim vNombre_comercial As String = ""
        Dim vCodigoProducto As String = ""
        Dim vFechaVence As DateTime = Now
        Dim sFechaVence As String = ""
        Dim vUbicacion As Integer
        Dim vLicense_plate As String = ""
        Dim vCodigo_Variante As String = ""
        Dim ExisteInventarioTeorico As Boolean = False
        '#GT24112022_1500: campos DyD
        Dim vCosto As Double
        Dim vPrecio As Double
        Dim vParametro_a As String = ""
        Dim vParametro_b As String = ""
        Dim vCodigo_Area_SAP As String = ""
        Dim Color As String = ""
        Dim Talla As String = ""
        Dim IdProductoTallaColor As Integer = 0

        Cursor.Current = Cursors.WaitCursor

        'EFREN190720212030: Se limpia la lista, porque al reintentar importar, mantiene la data anterior.
        lInventarioTeorico.Clear()

        Try

            rc = grdData.Rows.Count

            prg.Maximum = rc

            lblPrg.Text = "Preparando lista"
            lblPrg.Refresh()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Aplicando inventario...")

            For ii = 0 To rc - 1

                lblPrg.Text = "Preparando fila: " & ii & " para insert..."
                lblPrg.Refresh()

                SplashScreenManager.Default.SetWaitFormDescription("Preparando fila: " & ii + 1 & " de: " & rc)
                lblPrg.Text = "Preparando fila: " & ii & " para insert..."
                lblPrg.Refresh()

                Try
                    Cantidad = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCantidad").Value), 0, CDbl(grdData.Rows(ii).Cells("ColCantidad").Value))
                Catch ex As Exception
                    Cantidad = 0
                End Try

                Try
                    Peso = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPeso").Value), 0, Val(grdData.Rows(ii).Cells("ColPeso").Value))
                Catch ex As Exception
                    Peso = 0
                End Try

                vIdProducto = grdData.Rows(ii).Cells("ColIdProducto").Value
                vIdPresentacion = grdData.Rows(ii).Cells("ColIdPresentacion").Value
                vCodigoProducto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo").Value), "", grdData.Rows(ii).Cells("ColCodigo").Value)
                vIdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(vCodigoProducto) '#CM_20190807: para idealsa se cargaba el idunidadmedida del maestros de productos, no del excel'IIf(IsDBNull(grdData.Rows(ii).Cells("ColIdUnidadMedida").Value), "", grdData.Rows(ii).Cells("ColIdUnidadMedida").Value)
                vLote = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLote").Value), "", grdData.Rows(ii).Cells("ColLote").Value)
                sFechaVence = IIf(IsDBNull(grdData.Rows(ii).Cells("ColFechaVence").Value), "01/01/1900", grdData.Rows(ii).Cells("ColFechaVence").Value)
                vUbicacion = grdData.Rows(ii).Cells("ColUbicacion").Value
                vLicense_plate = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLp").Value), "", grdData.Rows(ii).Cells("ColLp").Value)
                vCodigo_Variante = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodVariante").Value), "", grdData.Rows(ii).Cells("ColCodVariante").Value)
                '#GT24112022_1500: campos DyD
                vCosto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCosto").Value), 0, grdData.Rows(ii).Cells("ColCosto").Value)
                vPrecio = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPrecio").Value), 0, grdData.Rows(ii).Cells("ColPrecio").Value)
                vParametro_a = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_a").Value), "", grdData.Rows(ii).Cells("ColParametro_a").Value)
                vParametro_b = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_b").Value), "", grdData.Rows(ii).Cells("ColParametro_b").Value)
                vCodigo_Area_SAP = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo_Area").Value), "", grdData.Rows(ii).Cells("ColCodigo_Area").Value)
                Color = IIf(IsDBNull(grdData.Rows(ii).Cells("ColColor").Value), "", grdData.Rows(ii).Cells("ColColor").Value)
                Talla = IIf(IsDBNull(grdData.Rows(ii).Cells("ColTalla").Value), "", grdData.Rows(ii).Cells("ColTalla").Value)
                IdProductoTallaColor = IIf(IsDBNull(grdData.Rows(ii).Cells("ColIdProductoTallaColor").Value), "", grdData.Rows(ii).Cells("ColIdProductoTallaColor").Value)

                If sFechaVence <> "" Then
                    vFechaVence = CDate(sFechaVence)
                Else
                    vFechaVence = New Date(1900, 1, 1)
                End If

                If vCodigoProducto = "11000504" Then
                    'Debug.Print("Espera")
                End If

                'EFREN17112021: Se omiten campos, porque no son funcionales en la clase.
                'vIdPropietarioBodega = grdData.Rows(ii).Cells("colIdPropietarioBodega").Value
                'vNombre_comercial = grdData.Rows(ii).Cells("colnombre_propietario").Value
                'item.IdPropietarioBodega = vIdPropietarioBodega
                'item.nombre_propietario = pBeProducto.Propietario.Nombre_comercial

                BeTrans_inv_stock_prod = New clsBeTrans_inv_stock_prod
                BeTrans_inv_stock_prod.Idinventario = IdInventario
                BeTrans_inv_stock_prod.IdProducto = vIdProducto
                BeTrans_inv_stock_prod.IdPresentacion = vIdPresentacion
                BeTrans_inv_stock_prod.Cant = Cantidad
                BeTrans_inv_stock_prod.Peso = Peso
                BeTrans_inv_stock_prod.IdUnidadMedida = vIdUnidadMedida
                BeTrans_inv_stock_prod.Lote = vLote
                BeTrans_inv_stock_prod.Fecha_vence = vFechaVence
                BeTrans_inv_stock_prod.Codigo = vCodigoProducto
                BeTrans_inv_stock_prod.IdBodega = AP.IdBodega
                BeTrans_inv_stock_prod.IdUbicacion = vUbicacion
                BeTrans_inv_stock_prod.License_plate = vLicense_plate
                BeTrans_inv_stock_prod.Codigo_variante = vCodigo_Variante
                '#GT25112022_1200: campos DyD
                BeTrans_inv_stock_prod.Costo = vCosto
                BeTrans_inv_stock_prod.Precio = vPrecio
                BeTrans_inv_stock_prod.Parametro_a = vParametro_a
                BeTrans_inv_stock_prod.Parametro_b = vParametro_b
                BeTrans_inv_stock_prod.TipoTeoricoImportacion = TipoTeoricoImportacion
                BeTrans_inv_stock_prod.Codigo_Area = vCodigo_Area_SAP
                BeTrans_inv_stock_prod.Codigo_Talla = Talla
                BeTrans_inv_stock_prod.Codigo_Color = Color
                BeTrans_inv_stock_prod.IdProductoTallaColor = IdProductoTallaColor

                '#AT20251015 Campos para MAMPA
                lInventarioTeorico.Add(BeTrans_inv_stock_prod)

                prg.Value = ii

                Application.DoEvents()

            Next

            lblPrg.Text = "Insertando registros..."
            lblPrg.Refresh()

            ExisteInventarioTeorico = clsLnTrans_inv_stock_prod.Exist(IdInventario,
                                                                      TipoTeoricoImportacion)

            If ExisteInventarioTeorico Then

                If MsgBox("Eliminar inventario teórico existente?",
                                    MessageBoxButtons.YesNo,
                                    "Inventario") = vbYes Then

                    clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                                InsertaInv,
                                                                AP.IdBodega,
                                                                AP.IdEmpresa,
                                                                IdOperador,
                                                                NomOperador,
                                                                DobleVerificacion,
                                                                prg,
                                                                True,
                                                                ExisteInventarioTeorico)
                Else
                    clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                                InsertaInv,
                                                                AP.IdBodega,
                                                                AP.IdEmpresa,
                                                                IdOperador,
                                                                NomOperador,
                                                                DobleVerificacion,
                                                                prg,
                                                                False,
                                                                ExisteInventarioTeorico)
                End If

            Else

                clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                             InsertaInv,
                                                             AP.IdBodega,
                                                             AP.IdEmpresa,
                                                             IdOperador,
                                                             NomOperador,
                                                             DobleVerificacion,
                                                             prg,
                                                             False,
                                                             ExisteInventarioTeorico)
            End If

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show("Se aplicó el inventario inicial correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            DialogResult = DialogResult.OK

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region " Eventos "

    Private Sub mnuPaste_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPegar.ItemClick
        Paste()
        Validar_Datos()
    End Sub

    Private Sub mnuValidar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuValidar.ItemClick
        If grdData.Rows.Count > 0 Then Validar_Datos()
    End Sub

    Private Sub mnuAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAplicar.ItemClick
        Aplicar_Teorico()
    End Sub

    Private Sub Aplicar_Teorico(Optional ByVal ConfirmarImportacion As Boolean = True,
                               Optional ByVal PreguntaValidarDatos As Boolean = True)

        Try

            If grdData.Rows.Count = 0 Then
                MsgBox("El inventario esta vacío.") : Return
            End If

            If ConfirmarImportacion Then
                If MessageBox.Show("¿Importar datos?", "", MessageBoxButtons.YesNo) = DialogResult.No Then Return
            End If

            If PreguntaValidarDatos Then
                If MessageBox.Show("¿Validar datos?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Validar_Datos()
                End If
            Else
                Validar_Datos()
            End If

            If errc > 0 Then
                MsgBox("No se puede completar la importacion, primero debe corregir los errores.") : Return
            End If

            lblPrg.Text = "Importando datos ...  "

            Importar_Datos()

            lblPrg.Text = ""

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdAdd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAdd.ItemClick
        grdData.Rows.Add(1)
        grdData.FirstDisplayedScrollingRowIndex = grdData.Rows.Count - 1
        grdData.Rows(grdData.Rows.Count - 1).Cells(0).Selected = True
    End Sub

    Private Sub cmdDel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdDel.ItemClick
        Try
            grdData.Rows.Remove(grdData.CurrentRow)
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            grdData.Rows.Clear()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

#End Region

#Region " Aux "

    Private Sub Marcar_Error(ri As Integer, NombreColumna As String, ByVal Mensaje As String)
        errc += 1
        grdData.Rows(ri).Cells(0).Style.BackColor = Color.Salmon
        grdData.Rows(ri).Cells(0).Value = "ERROR"
        grdData.Rows(ri).Cells("ColError").Value = Mensaje
        grdData.Rows(ri).Cells(NombreColumna).Style.BackColor = Color.Salmon
    End Sub

    Private Sub Importar_Excel()

        Try

            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Inventario " + TipoInventario,
                .pTipoMantenimiento = "Inventario",
                .Listar = Nothing,
                .IdInventarioEnc = IdInventario}

            If Carga.ShowDialog() = DialogResult.OK Then

                Dim i As Integer = 0
                Dim vContador As Integer = 1

                prg.Visible = True
                lblPrg.Visible = True

                prg.Maximum = Carga.lInvenarioTeorico.Rows.Count

                RibbonControl.Enabled = False

                grdData.SuspendLayout()
                grdData.Rows.Clear()

                lblRegs.Caption = "Registros: " & Carga.lInvenarioTeorico.Rows.Count()

                InsertaInv = Carga.InsertaInv
                IdOperador = Carga.IdOperador
                NomOperador = Carga.NomOperador

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Procesando archivo...")

                Application.DoEvents()

                Dim vCantRegistros As Integer = Carga.lInvenarioTeorico.Rows().Count()

                For Each ProdInv As DataRow In Carga.lInvenarioTeorico.Rows()

                    SplashScreenManager.Default.SetWaitFormDescription("Código: " & ProdInv("Codigo") & " " & i & " de " & vCantRegistros)

                    lblPrg.Text = "Llenando datos para: " & ProdInv("Codigo")
                    lblPrg.Refresh()

                    i = grdData.Rows.Add(ProdInv("EstadoProcesamiento"),
                               ProdInv("Contador"))

                    grdData.Item("ColCodigo", i).Value = ProdInv("Codigo")
                    grdData.Item("ColPresentacion", i).Value = ProdInv("Presentacion")
                    grdData.Item("ColCantidad", i).Value = ProdInv("Cantidad")
                    grdData.Item("ColPeso", i).Value = ProdInv("Peso")
                    grdData.Item("ColUnidadMedida", i).Value = ProdInv("UM")
                    grdData.Item("ColLote", i).Value = ProdInv("Lote")
                    grdData.Item("colFechaVence", i).Value = ProdInv("Vence")
                    grdData.Item("ColUbicacion", i).Value = ProdInv("Ubicacion")
                    'GT01122021: se agrega LP y cod_variante
                    grdData.Item("ColLp", i).Value = ProdInv("LP")
                    grdData.Item("ColCodVariante", i).Value = ProdInv("CodVariante")
                    '#GT24112022_0800: campos DyD
                    grdData.Item("ColCosto", i).Value = ProdInv("Costo")
                    grdData.Item("ColPrecio", i).Value = ProdInv("Precio")
                    grdData.Item("ColParametro_a", i).Value = ProdInv("Parametro_a")
                    grdData.Item("ColParametro_b", i).Value = ProdInv("Parametro_b")
                    grdData.Item("ColCodigo_Area", i).Value = ProdInv("Codigo_Area")
                    grdData.Item("ColTalla", i).Value = ProdInv("Talla")
                    grdData.Item("ColColor", i).Value = ProdInv("Color")
                    grdData.Item("ColIdProductoTallaColor", i).Value = ProdInv("IdProductoTallaColor")

                    prg.Value = i

                    vContador += 1

                    Application.DoEvents()

                Next

                grdData.ResumeLayout()

                Aplicar_Teorico(False, False)

            End If

            Carga.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            RibbonControl.Enabled = True
        End Try

    End Sub

    Private Sub frmInventarioImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub mnuImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarExcel.ItemClick
        Importar_Excel()
    End Sub

    Private Sub Llena_Catalogos()

        Try

            'EFREN09052021 si es multi propietario, no se filtra por propietario especifico.
            If InvTeorico_Multi_Propietario Then

                dtall = clsLnProducto.GetCodigosProd_By_Multi_Propietario()

                dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega(AP.IdBodega)
                dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega()

            Else
                'EFREN10052021 el propietario especifico se obtiene desde frm inventario, no es necesario obtenerlo del combobox
                dtc = clsLnProducto.GetCodigosProd_By_IdPropietarioBodega(AP.IdBodega, IdPropietarioBodega)
                dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega(AP.IdBodega, IdPropietarioBodega)
                dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega(IdPropietarioBodega)

            End If

            'dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega(AP.IdBodega, cmbPropietario.EditValue)
            'dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega(cmbPropietario.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmInventarioImport_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, AP.IdBodega)

            lblPrg.Text = ""

            vBodega = AP.IdBodega

            If InvTeorico_Multi_Propietario Then
                cmbPropietario.Visible = False
                lblPropietario.Visible = False
            Else
                cmbPropietario.EditValue = IdPropietarioBodega
                cmbPropietario.Enabled = False
            End If

            Importar_Excel()

            Set_Tipo_Importacion()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub Set_Tipo_Importacion()

        Try

            Select Case TipoTeoricoImportacion
                Case pTipoImportacion.ERP
                    lblTipoImportacion.Caption = "Importación de teórico ERP"
                Case pTipoImportacion.WMS
                    lblTipoImportacion.Caption = "Importación de teórico WMS"
            End Select


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
        End Try

    End Sub

#End Region


End Class