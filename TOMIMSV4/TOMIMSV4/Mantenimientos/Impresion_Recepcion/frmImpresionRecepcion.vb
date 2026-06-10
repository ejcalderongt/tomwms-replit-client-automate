Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmImpresionRecepcion

    Public pTransReDet As New clsBeTrans_re_det()
    Private pImpresoraProdSeleccionada As String = ""
    Private pImpresoraLicSeleccionada As String = ""
    Private EsPrimeraImpresion As Boolean = False

    Private Function ObtenerTextoBodegaEtiqueta() As String
        If AP.Bodega Is Nothing Then Return ""

        Dim codigo As String = Convert.ToString(AP.Bodega.Codigo).Trim()
        Dim nombre As String = Convert.ToString(AP.Bodega.Nombre).Trim()

        If AP.Bodega.ocultar_nombre_etiquetas_impresas Then
            Return codigo
        End If

        If String.IsNullOrWhiteSpace(nombre) Then Return codigo

        Return codigo & " - " & nombre
    End Function

    Private Sub frmImpresionRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Dim vCantImpresion As Integer = 0

            txtBarra.Text = pTransReDet.Codigo_Producto
            txtDescripcion.Text = pTransReDet.Nombre_producto
            txtLote.Text = pTransReDet.Lote
            txtLicencia.Text = pTransReDet.Lic_plate

            vCantImpresion = pTransReDet.cantidad_recibida

            If pTransReDet.IdPresentacion <> 0 Then
                If pTransReDet.Presentacion.Factor > 0 Then
                    vCantImpresion = pTransReDet.cantidad_recibida * pTransReDet.Presentacion.Factor
                End If
            End If

            txtCantidadBarras.Value = vCantImpresion
            txtCantidadLicencias.Value = 1

            txtBarra.Enabled = False
            txtDescripcion.Enabled = False
            txtLote.Enabled = False
            txtLicencia.Enabled = False

            Cargar_Impresoras_Windows(cmbPrinterBarra)
            Cargar_Impresoras_Windows(cmbPrinterLicencia)

            cmbPrinterLicencia.EditValue = frmRecepcion.pImpresoraLicSeleccionada
            cmbPrinterBarra.EditValue = frmRecepcion.pImpresoraProdSeleccionada

            EsPrimeraImpresion = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub cmdImpresionLicencia_Click(sender As Object, e As EventArgs) Handles cmdImpresionLicencia.Click

        Try

            Imprimir_Licencia(pTransReDet,
                              cmbPrinterLicencia.EditValue,
                              txtCantidadLicencias.Value)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Imprimir_Etiqueta(ByVal pReDet As clsBeTrans_re_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)

        Try

            '#GT15022024: valores a cargar en la etiqueta ZPL
            Dim ZPLString As String = ""
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoBarra As String = "$" & pReDet.Lic_plate.Substring(0, IIf(pReDet.Lic_plate.Length < 10, pReDet.Lic_plate.Length, 9)) ' "20240123"
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44)) '"PRAZOLEN 20MG CAJA X 15 CAPSULAS MUY LARGO PARA"
            Dim vLote As String = pReDet.Lote
            Dim vFechaVence As String = pReDet.Fecha_vence.ToShortDateString

            '#GT14022024: validamos producto y tipo etiqueta (zpl, simbologia)
            Dim pBeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pReDet.IdProductoBodega)
            Dim pTipoEtiqueta = pBeProducto.IdTipoEtiqueta
            Dim pTipoSimbologia = pBeProducto.IdSimbologia
            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, 1)

            '#GT15022024: validamos cuantas impresiones deben realizarse, considera 3 x fila (son 3 columnas)
            Dim vColaImpresiones As Double = Math.Truncate(pImpresiones / 3)
            Dim vColaFraccion As Double = pImpresiones - (vColaImpresiones * 3)

            If PrinterName <> "" Then

                If Tipo_Etiqueta IsNot Nothing Then

                    Dim tmpZPLString = Tipo_Etiqueta.codigo_zpl

                    If tmpZPLString <> "" Then
                        ZPLString = String.Format(tmpZPLString,
                                                  vEmpresa,
                                                  vCodigoBarra,
                                                  vCodigoProducto,
                                                  vNombreProducto.Trim,
                                                  vLote,
                                                  vFechaVence)
                    End If

                    If ZPLString <> "" Then
                        If vColaImpresiones > 0 Then
                            If vColaImpresiones = 1 Then
                                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            Else
                                For i = 1 To vColaImpresiones
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                                Next
                            End If
                        End If
                        If vColaFraccion > 0 Then
                            Select Case vColaFraccion
                                Case 1
                                    tmpZPLString = tmpZPLString.Substring(0, 614) & "^XZ"
                                    ZPLString = String.Format(tmpZPLString,
                                                  vEmpresa,
                                                  vCodigoBarra,
                                                  vCodigoProducto,
                                                  vNombreProducto.Trim,
                                                  vLote,
                                                  vFechaVence)
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                                Case 2
                                    tmpZPLString = tmpZPLString.Substring(0, 1074) & "^XZ"
                                    ZPLString = String.Format(tmpZPLString,
                                                  vEmpresa,
                                                  vCodigoBarra,
                                                  vCodigoProducto,
                                                  vNombreProducto.Trim,
                                                  vLote,
                                                  vFechaVence)
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            End Select
                        End If
                    Else
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el formato de etiqueta"),
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                    End If
                Else
                    Throw New Exception("GT14022024: No se cargaron las propiedades de la etiqueta.")
                End If

            Else
                DxErrorProvider1.SetError(cmbPrinterBarra, "Seleccione impresora")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub Imprimir_Licencia(ByVal pReDet As clsBeTrans_re_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)

        Try

            '#GT15022024: valores a cargar en la etiqueta ZPL
            Dim ZPLString As String = ""
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoBarra As String = "$" & pReDet.Lic_plate.Substring(0, IIf(pReDet.Lic_plate.Length < 10, pReDet.Lic_plate.Length, 9)) ' "20240123"
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44)) '"PRAZOLEN 20MG CAJA X 15 CAPSULAS MUY LARGO PARA"
            Dim vLote As String = pReDet.Lote
            Dim vFechaVence As String = pReDet.Fecha_vence.ToShortDateString

            Dim pTipoEtiqueta As Integer = AP.Bodega.IdTipoEtiquetaLicencia
            Dim pTipoSimbologia As Integer = AP.Bodega.IdSimbologiaLicencia
            Dim pClasificacion As Integer = 2
            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, pClasificacion)

            Dim vColaImpresiones = pImpresiones

            If PrinterName <> "" Then

                If Tipo_Etiqueta IsNot Nothing Then

                    Dim tmpZPLString = Tipo_Etiqueta.codigo_zpl

                    If tmpZPLString <> "" Then
                        'ZPLString = String.Format(tmpZPLString, AP.Bodega.Codigo + " - " + AP.Bodega.Nombre,
                        '                          vEmpresa,
                        '                          vCodigoProducto + " - " + vNombreProducto.Trim,
                        '                          vCodigoBarra,
                        '                          AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        '                          vLote,
                        '                          vFechaVence)

                        ZPLString = String.Format(tmpZPLString,
                                                ObtenerTextoBodegaEtiqueta(),                                     ' {0}
                                                vEmpresa,                                                         ' {1}
                                                vCodigoProducto & " - " & vNombreProducto.Trim(),                 ' {2}
                                                vCodigoBarra,                                                     ' {3}
                                                AP.UsuarioAp.Nombres & " " & AP.UsuarioAp.Apellidos & " / " & Now.ToString("yyyy-MM-dd HH:mm:ss"), ' {4}
                                                vLote,                                                            ' {5}
                                                vFechaVence,                                                      ' {6}
                                                pReDet.Nombre_presentacion,                                                    ' {7}
                                                pReDet.cantidad_recibida                                                         ' {8}
                                            )

                    End If

                    If ZPLString <> "" Then
                        If vColaImpresiones > 0 Then
                            If vColaImpresiones = 1 Then
                                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            Else
                                For i = 1 To vColaImpresiones
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                                Next
                            End If
                        End If
                    Else
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el formato de etiqueta"),
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                    End If
                Else
                    Throw New Exception("GT14022024: No se cargaron las propiedades de la etiqueta.")
                End If

            Else
                DxErrorProvider1.SetError(cmbPrinterLicencia, "seleccione impresora")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub frmImpresionRecepcion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            Imprimir_Etiqueta(pTransReDet, pImpresoraProdSeleccionada, txtCantidadBarras.Value)
        ElseIf e.Control AndAlso e.KeyCode = Keys.L Then
            Imprimir_Etiqueta(pTransReDet, pImpresoraLicSeleccionada, txtCantidadLicencias.Value)
        End If

    End Sub

    Public Function Cargar_Impresoras_Windows(ByRef Cmb As LookUpEdit) As Boolean

        Cargar_Impresoras_Windows = False

        Try

            Dim i As Integer
            Dim iList As New ArrayList
            Dim pkInstalledPrinters As String

            For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
                iList.Add(pkInstalledPrinters)
            Next

            Cmb.Properties.DataSource = iList

            If iList.Count > 0 Then
                'Buscar en la base de datos una impresora que coincida con el nombre y tipo para bof.
            End If

        Catch ex As Exception
            XtraMessageBox.Show("El servicio de impresión no está disponible o no se pudieron listar las impresoras disponibles.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdImpresionBarra_Click_1(sender As Object, e As EventArgs) Handles cmdImpresionBarra.Click

        Try

            Imprimir_Etiqueta(pTransReDet, cmbPrinterBarra.EditValue, txtCantidadBarras.Value)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPrinterBarra_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterBarra.EditValueChanged
        pImpresoraProdSeleccionada = cmbPrinterBarra.EditValue
        frmRecepcion.pImpresoraProdSeleccionada = pImpresoraProdSeleccionada
    End Sub

    Private Sub cmbPrinterLicencia_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterLicencia.EditValueChanged
        pImpresoraProdSeleccionada = cmbPrinterLicencia.EditValue
        frmRecepcion.pImpresoraLicSeleccionada = pImpresoraProdSeleccionada
    End Sub

End Class
