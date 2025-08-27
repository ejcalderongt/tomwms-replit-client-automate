Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmUbicacion_Etiqueta

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private ListUbicacionesPorTramo As New List(Of clsBeBodega_Ubicacion_Seleccion)

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property IdReglaUbicacionEnc As Integer
    Public Obj As clsBeTipo_etiqueta
    Public BeBodega As New clsBeBodega

    Private Sub Cargar_Bodega_Ubicacion()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando ubicaciones...")

            grdUbicacion.DataSource = ListUbicacionesPorTramo
            GridViewUbi.LayoutChanged()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridViewUbi.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemPropietario_CheckedChanged

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub ritemPropietario_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As clsBeBodega_Ubicacion_Seleccion = GridViewUbi.GetFocusedRow

                If ritem.Checked Then
                    ListUbicacionesPorTramo.Where(Function(x) x.IdUbicacion = Dr.IdUbicacion).FirstOrDefault.Seleccionar = True
                Else
                    ListUbicacionesPorTramo.Where(Function(x) x.IdUbicacion = Dr.IdUbicacion).FirstOrDefault.Seleccionar = False
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

    Private Sub CargaComboEtiquetas(ByVal IdTamañoEtiquetaPorDefecto As Integer)

        Try

            cmbEtiquetaU.DisplayMember = "Nombre"
            cmbEtiquetaU.ValueMember = "IdTipoEtiqueta"
            cmbEtiquetaU.DataSource = clsLnTipo_etiqueta.GetAll()
            cmbEtiquetaU.SelectedValue = IdTamañoEtiquetaPorDefecto

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbTramoUbic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTramoUbic.SelectedIndexChanged

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Listando ubicaciones...")


            ListUbicacionesPorTramo = clsLnBodega_ubicacion.Get_All_Ubicaciones_By_IdTramo(cmbTramoUbic.SelectedValue, AP.IdBodega)

            Cargar_Bodega_Ubicacion()

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Sub Invertirt()

        Try

            pgbUbic.Properties.PercentView = True
            pgbUbic.Properties.Minimum = 0
            pgbUbic.Properties.Step = 1
            pgbUbic.Visible = True
            pgbUbic.EditValue = 0

            For Each Prod As clsBeBodega_Ubicacion_Seleccion In grdUbicacion.DataSource

                ListUbicacionesPorTramo.Find(Function(x) x.IdUbicacion = Prod.IdUbicacion).Seleccionar = Not Prod.Seleccionar

                pgbUbic.PerformStep()
                pgbUbic.Update()

            Next

            Cargar_Bodega_Ubicacion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbUbic.Visible = False
        End Try

    End Sub

    Public Sub DesmarcarTodos()

        Try

            pgbUbic.Properties.PercentView = True
            pgbUbic.Properties.Minimum = 0
            pgbUbic.Properties.Step = 1
            pgbUbic.Visible = True
            pgbUbic.EditValue = 0
            Dim vMarcado As Boolean = False

            For Each Prod As clsBeBodega_Ubicacion_Seleccion In grdUbicacion.DataSource

                vMarcado = Prod.Seleccionar = False

                ListUbicacionesPorTramo.Find(Function(x) x.IdUbicacion = Prod.IdUbicacion).Seleccionar = False

                pgbUbic.PerformStep()
                pgbUbic.Update()

            Next

            Cargar_Bodega_Ubicacion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbUbic.Visible = False
        End Try

    End Sub

    Public Sub MarcarTodos()

        Try

            pgbUbic.Properties.PercentView = True
            pgbUbic.Properties.Minimum = 0
            pgbUbic.Properties.Step = 1
            pgbUbic.Visible = True
            pgbUbic.EditValue = 0

            Dim Prod As clsBeBodega_Ubicacion_Seleccion

            For Each Prod In grdUbicacion.DataSource

                ListUbicacionesPorTramo.Find(Function(x) x.IdUbicacion = Prod.IdUbicacion).Seleccionar = True

                pgbUbic.PerformStep()
                pgbUbic.Update()

            Next

            Cargar_Bodega_Ubicacion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbUbic.Visible = False
        End Try

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try

            Dim lProductosOC As New List(Of clsBeBodega_Ubicacion_Seleccion)
            Dim vBarra As String = ""

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            If DialogResult.OK = pd.ShowDialog(Me) Then

                For Each Ubic As clsBeBodega_Ubicacion_Seleccion In ListUbicacionesPorTramo.Where(Function(x) x.Seleccionar = True)
                    Imprimir_Etiqueta(Ubic.IdUbicacion, pd.PrinterSettings.PrinterName)
                Next

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

    Private Sub mnuMarcarTodos_CheckedChanged_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodos.CheckedChanged

        Try

            pgbUbic.Properties.PercentView = True
            pgbUbic.Properties.Minimum = 0
            pgbUbic.Properties.Step = 1
            pgbUbic.Visible = True
            pgbUbic.EditValue = 0

            Dim IdUbicacion As Integer
            Dim vMarcado As Boolean = mnuMarcarTodos.Checked

            '#CKFK EJC 20180419 Se modificó el marcar todos en la impresión para que solo seleccione las ubicaciones que se ven en el grid y no las del datasource.
            For i = 0 To GridViewUbi.DataRowCount - 1

                IdUbicacion = CType(GridViewUbi.GetRowCellValue(i, "IdUbicacion"), Integer)

                ListUbicacionesPorTramo.Find(Function(x) x.IdUbicacion = IdUbicacion).Seleccionar = vMarcado
                GridViewUbi.SetRowCellValue(i, "Seleccionar", vMarcado)

                pgbUbic.PerformStep()
                pgbUbic.Update()

            Next

            Cargar_Bodega_Ubicacion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbUbic.Visible = False
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Cargar_Bodega_Ubicacion()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Invertirt()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    'Private Shared Sub Imprimir_Etiqueta(ByVal Tramo As String, _
    '                              ByVal CodigoBarra As String, _
    '                              ByVal Ubicacion As String, _
    '                              ByVal PrinterName As String)

    '    '#EJC20171205:0546AM: 
    '    'Gracias a la ayuda de: http://labelary.com/viewer.html
    '    'Y de los creadores de la clase RawPrinterHelper en C#
    '    'La clase fue convertia a Vb por Telerik
    '    Dim Longitud As Integer = IIf(Tramo.IndexOf("-") < 0, 0, Tramo.IndexOf("-") - 1)

    '    Dim Pos As String = IIf(Ubicacion.Length < 4, Ubicacion.Substring(1, 1), Ubicacion.Substring(0, 2))
    '    Dim Col As String = Strings.Right("00" & Ubicacion.Substring(0, 1), 2)
    '    Dim Nivel As String = Strings.Right("00" & Ubicacion.Substring(2, 1), 2)
    '    Dim Rack As String = Strings.Right("00" & Tramo.Substring(1, Longitud), 2)
    '    Dim Tunel As String = Tramo.Substring(tramo.Length - Tramo.IndexOf("-") + 1, 1)     

    '    '^MMT: Print Mode: The ^MM command determines the action the printer takes after a label or 
    '    'group of labels has printed. This bulleted list identifies the different modes of operation:
    '    'Accepted Values:
    '    'T= Tear-off
    '    'P= Peel-off (not available on S-300)
    '    'R= Rewind
    '    'A= Applicator (depends on printer model)
    '    'C= Cutter

    '    '^PW: Print Width label width (in dots)

    '    '^LL: Label Length This command is necessary when using continuous media (media not divided into separate labels by gaps, spaces, notches, slots, or holes)
    '    ' y-axis position (in dots)

    '    '^LS: Label Shift:The ^LS command allows for compatibility with Z-130 printer formats that are 
    '            'set for less than full label width. It is used to shift all field positions to the left so the same 
    '            'commands used on a Z-130 or Z-220 Printer can be used on other Zebra printers.

    '    '^FT: Field Typeset command also sets the field position, relative to the home position OF the label designated by the ^LH command
    '    'Format  ^FTx,y

    '    Dim ZPLString As String = String.Format("
    '                                                                  ^XA
    '                                                                  ^MMT
    '                                                                  ^PW812
    '                                                                  ^LL0406
    '                                                                  ^LS0
    '                                                                  ^FT800,374^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
    '                                                                  ^FO9,359^GB800,0,1^FS
    '                                                                  ^BY6,3,283^FT491,72^BCI,,N,N                                                                      
    '                                                                  ^FD>:{5}^FS
    '                                                                  ^FT684,146^A0I,25,24^FH\^FDPos^FS
    '                                                                  ^FT687,323^A0I,25,24^FH\^FDCol^FS
    '                                                                  ^FT787,146^A0I,25,24^FH\^FDNivel^FS
    '                                                                  ^FT793,229^A0I,85,84^FH\^FD{0}^FS
    '                                                                  ^FT584,226^A0I,85,122^FH\^FD{1}^FS
    '                                                                  ^FT687,47^A0I,85,122^FH\^FD{2}^FS
    '                                                                  ^FT686,228^A0I,85,84^FH\^FD{3}^FS
    '                                                                  ^FT794,325^A0I,25,24^FH\^FDRack^FS
    '                                                                  ^FT787,48^A0I,85,84^FH\^FD{4}^FS
    '                                                                  ^FT583,323^A0I,23,24^FH\^FDTunel^FS
    '                                                                  ^FO595,192^GB211,163,8^FS
    '                                                                  ^FO595,13^GB209,172,8^FS
    '                                                                  ^FO697,18^GB0,165,4^FS
    '                                                                  ^FO702,196^GB0,154,5^FS                                                                      
    '                                                                  ^FT354,29^A0I,45,45^FB174,1,0,C^FH\^FD{5}^FS
    '                                                                  ^PQ1,0,1,Y
    '                                                                  ^XZ", Rack, Tunel, Pos, Col, Nivel, CodigoBarra)

    '    Try

    '        RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        "Impresión de ubicaciones",
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)

    '    End Try

    'End Sub

    Private Sub Imprimir_Etiqueta(ByVal IdUbicacion As Integer,
                                  ByVal PrinterName As String)

        Dim BeUbicacion As New clsBeBodega_ubicacion
        Dim Tunel As String = ""
        Dim Rack As String = ""
        BeUbicacion = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(IdUbicacion, AP.IdBodega)

        Dim Pos As String = BeUbicacion.Orientacion_pos
        Dim Col As String = Strings.Right("00" & BeUbicacion.Indice_x, 2)
        Dim Nivel As String = Strings.Right("00" & BeUbicacion.Nivel, 2)
        Dim UbicGuion As Integer = BeUbicacion.Tramo.Descripcion.IndexOf("-")
        Dim Longitud As Integer = IIf(BeUbicacion.Tramo.Descripcion.IndexOf("-") < 0, 0, BeUbicacion.Tramo.Descripcion.IndexOf("-") - 1)
        Dim RackPasillo As String = ""

        If UbicGuion > 0 Then
            '#CKFK 20180417 12:56PM Se modificó la forma de obtener el número del Rack
            Rack = Strings.Right("00" & BeUbicacion.Tramo.Descripcion.Substring(1, Longitud), 2)
            '#EJC20180419: Si el rack tiene más de 2 dígitos UbicGuion =3
            If UbicGuion >= 3 Then
                Tunel = BeUbicacion.Tramo.Descripcion.Substring(BeUbicacion.Tramo.Descripcion.Length - 1, 1)
            Else
                '#CKFK20230726 Modifiqué esta linea porque tomaba el guión de la descripción y no el tramo
                Tunel = BeUbicacion.Tramo.Descripcion.Substring(BeUbicacion.Tramo.Descripcion.Length - UbicGuion + 1, 1)
            End If
        Else
            Rack = BeUbicacion.Tramo.Descripcion.Replace("R", "")
            Tunel = ""
        End If

        If BeUbicacion.Tramo.Es_Rack Then
            RackPasillo = "Rack"
        Else
            RackPasillo = "Pasillo"
        End If

        Dim Barra = Strings.Right(String.Format("00000{0}", BeUbicacion.Codigo_barra), 5)
        Dim CodBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AP.IdBodega)

        '#EJC20180416: Modificación de ZPL por utilización de CODE128A
        'CODE128 - A
        Dim ZPLString As String = ""

        Dim BeTipoEtiqueta As New clsBeTipo_etiqueta

        If cmbEtiquetaU.SelectedValue = 3 Then

            ZPLString = String.Format(" ^XA
                                        ^MMT
                                        ^PW609
                                        ^LL0406
                                        ^LS0
                                        ^FT595,374^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                        ^FT290,374^A0I,25,24^FH\^FDBodega: {6}^FS
                                        ^FO9,359^GB800,1,1^FS
                                        ^FO15,235^GB580,120,3^FS 
                                        ^FT570,325^A0I,25,24^FH\^FD{7}^FS
                                        ^FT575,250^A0I,65,74^FH\^FD{0}^FS
                                        ^FO475,235^GB3,120,3^FS
                                        ^FT450,325^A0I,25,24^FH\^FDTunel^FS
                                        ^FT440,250^A0I,65,70^FH\^FD{1}^FS
                                        ^FO360,235^GB3,120,3^FS 
                                        ^FT320,325^A0I,25,24^FH\^FDCol^FS
                                        ^FT320,250^A0I,65,70^FH\^FD{3}^FS
                                        ^FO245,235^GB3,120,3^FS
                                        ^FT210,325^A0I,25,24^FH\^FDNivel^FS
                                        ^FT220,250^A0I,65,70^FH\^FD{4}^FS
                                        ^FO130,235^GB3,120,3^FS
                                        ^FT95,325^A0I,25,24^FH\^FDPos^FS
                                        ^FT95,250^A0I,65,70^FH\^FD{2}^FS
                                        ^BY5,3,160^FT520,55^BCI,,Y,N
                                        ^FD>:{5}^FS
                                        ^PQ1,0,1,Y
                                        ^XZ ", Rack,
                                               Tunel,
                                               Pos,
                                               Col,
                                               Nivel,
                                               Barra,
                                               CodBodega,
                                               RackPasillo)

        ElseIf cmbEtiquetaU.SelectedValue = 6 Then

            'GT26082022: se ajusta para formato 2x4 DyD es ahora tipo de etiqueta 6
            ZPLString = String.Format(" ^XA
                                        ^MMT
                                        ^PW812
                                        ^LL0406
                                        ^LS0
                                        ^FT800,474^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                        ^FT400,474^A0I,25,24^FH\^FDBodega: {6}^FS
                                        ^FO9,459^GB800,0,1^FS
                                        ^FT684,246^A0I,25,24^FH\^FDPos^FS
                                        ^FT687,423^A0I,25,24^FH\^FDCol^FS
                                        ^FT787,246^A0I,25,24^FH\^FDNivel^FS
                                        ^FT793,329^A0I,85,84^FH\^FD{0}^FS
                                        ^FT588,336^A0I,85,122^FH\^FD{1}^FS
                                        ^FT686,328^A0I,85,84^FH\^FD{3}^FS
                                        ^FT687,147^A0I,85,122^FH\^FD{2}^FS
                                        ^FT794,425^A0I,25,24^FH\^FD{7}^FS
                                        ^FT787,148^A0I,85,84^FH\^FD{4}^FS
                                        ^FT583,423^A0I,23,24^FH\^FDTunel^FS
                                        ^FO598,291^GB211,162,8^FS
                                        ^FO598,113^GB206,172,8^FS
                                        ^FO697,118^GB0,165,4^FS
                                        ^FO702,296^GB0,154,5^FS
                                        ^BY5,3,290^FT458,160^BCI,,Y,N
                                        ^FD>:{5}^FS
                                        ^PQ1,0,1,Y
                                        ^XZ ", Rack,
                                               Tunel,
                                               Pos,
                                               Col,
                                               Nivel,
                                               Barra,
                                               CodBodega,
                                               RackPasillo)

        ElseIf cmbEtiquetaU.SelectedValue = 1 Then 'formato 2x4

            ZPLString = String.Format(" ^XA
                                        ^MMT
                                        ^PW812
                                        ^LL0406
                                        ^LS0
                                        ^FT790,364^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                        ^FT400,364^A0I,25,24^FH\^FDBodega: {6}^FS
                                        ^FO9,359^GB800,0,1^FS
                                        ^FT684,146^A0I,25,24^FH\^FDPos^FS
                                        ^FT687,323^A0I,25,24^FH\^FDCol^FS
                                        ^FT787,146^A0I,25,24^FH\^FDNivel^FS
                                        ^FT793,229^A0I,85,70^FH\^FD{0}^FS
                                        ^FT588,236^A0I,85,122^FH\^FD{1}^FS
                                        ^FT686,228^A0I,85,84^FH\^FD{3}^FS
                                        ^FT687,47^A0I,85,122^FH\^FD{2}^FS
                                        ^FT794,325^A0I,25,24^FH\^FD{7}^FS
                                        ^FT787,48^A0I,85,84^FH\^FD{4}^FS
                                        ^FT583,323^A0I,23,24^FH\^FDTunel^FS
                                        ^FO590,191^GB211,162,8^FS
                                        ^FO590,13^GB211,172,8^FS
                                        ^FO697,18^GB0,165,4^FS
                                        ^FO702,196^GB0,154,5^FS
                                        ^BY5,3,275^FT478,70^BCI,,Y,N
                                        ^FD>:{5}^FS
                                        ^PQ1,0,1,Y
                                        ^XZ ", Rack,
                                               Tunel,
                                               Pos,
                                               Col,
                                               Nivel,
                                               Barra,
                                               CodBodega,
                                               RackPasillo)

            BeTipoEtiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(cmbEtiquetaU.SelectedValue)

            If Not BeTipoEtiqueta Is Nothing Then
                If Not BeTipoEtiqueta.codigo_zpl = "" Then
                    ZPLString = String.Format(BeTipoEtiqueta.codigo_zpl,
                                              Rack,
                                              Tunel,
                                              Pos,
                                              Col,
                                              Nivel,
                                              Barra,
                                              CodBodega,
                                              RackPasillo)
                End If
            End If

        ElseIf cmbEtiquetaU.SelectedValue = 4 Then
            '#GT08092023: se agrega formato 4x3 Ubicaciones-IDEALSA

            ZPLString = String.Format("  ^XA
                                        ^MMT
                                        ^PW812
                                        ^LL0406
                                        ^LS0
                                        ^FT800,474^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                        ^FT400,474^A0I,25,24^FH\^FDBodega: {6}^FS
                                        ^FO9,459^GB800,0,1^FS
                                        ^FT684,246^A0I,25,24^FH\^FDPos^FS
                                        ^FT687,423^A0I,25,24^FH\^FDCol^FS
                                        ^FT787,246^A0I,25,24^FH\^FDNivel^FS
                                        ^FT793,329^A0I,85,84^FH\^FD{0}^FS
                                        ^FT588,336^A0I,85,122^FH\^FD{1}^FS
                                        ^FT686,328^A0I,85,84^FH\^FD{3}^FS
                                        ^FT687,147^A0I,85,122^FH\^FD{2}^FS
                                        ^FT794,425^A0I,25,24^FH\^FD{7}^FS
                                        ^FT787,148^A0I,85,84^FH\^FD{4}^FS
                                        ^FT583,423^A0I,23,24^FH\^FDTunel^FS
                                        ^FO598,291^GB211,162,8^FS
                                        ^FO598,113^GB206,172,8^FS
                                        ^FO697,118^GB0,165,4^FS
                                        ^FO702,296^GB0,154,5^FS
                                        ^BY5,3,275^FT478,170^BCI,,Y,N
                                        ^FD>:{5}^FS
                                        ^PQ1,0,1,Y
                                        ^XZ ", Rack,
                                               Tunel,
                                               Pos,
                                               Col,
                                               Nivel,
                                               Barra,
                                               CodBodega,
                                               RackPasillo)

        ElseIf cmbEtiquetaU.SelectedValue = 10 Then
            '#GT08092023: se agrega formato 2X1.5 Ubicaciones-IDEALSA

            ZPLString = String.Format(" ^XA  
                                        ^MMT
                                        ^PW812
                                        ^LL0406
                                        ^LS0
                                        ^FT350,270^A0I,20,20^FH\^FD- TOM, WMS. - ^FS
                                        ^FT200,270^A0I,20,20^FH\^FD BODEGA: {2}^FS
                                        ^FO10,250^GB390,0,1^FS
                                        ^FT340,200^A0I,25,24^FH\^FD{0}^FS
                                        ^BY3,3,100^FT350,80^BCI,,Y,N
                                        ^FD>:{1}^FS
                                        ^PQ1,0,1,Y
                                        ^XZ", "R" & Rack & " - T" & Tunel & " - C" & Val(Col) & "- N" & Val(Nivel) & " - " & Pos, Barra, CodBodega)

        ElseIf cmbEtiquetaU.SelectedValue = 11 Then

            ZPLString = String.Format("^XA
                                        ^MMT
                                        ^PW609
                                        ^LL0406
                                        ^LS0
                                        ^FT580,180^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                        ^FT300,180^A0I,25,24^FH\^FDBodega: {6}^FS
                                        ^FO9,170^GB590,1,1^FS
                                        ^FT590,140^A0I,25,24^FH\^FD{7}^FS
                                        ^FT590,110^A0I,30,25^FH\^FD{0}^FS
                                        ^FT500,140^A0I,25,24^FH\^FDCol^FS
                                        ^FT500,110^A0I,30,25^FH\^FD{3}^FS
                                        ^FT500,60^A0I,25,24^FH\^FDPos^FS
                                        ^FT500,28^A0I,30,25^FH\^FD{2}^FS
                                        ^FT590,60^A0I,25,24^FH\^FDNivel^FS
                                        ^FT590,28^A0I,30,25^FH\^FD{4}^FS
                                        ^FT420,140^A0I,23,24^FH\^FDTunel^FS
                                        ^FT420,100^A0I,30,25^FH\^FD{1}^FS
                                        ^FO440,90^GB160,75,4^FS
                                        ^FO440,13^GB160,75,4^FS
                                        ^FO520,18^GB4,70,4^FS
                                        ^FO520,90^GB4,70,4^FS
                                        ^BY4,3,100^FT328,50^BCI,,Y,N
                                        ^FD>:{5}^FS
                                        ^PQ1,0,1,Y", Rack,
                                               Tunel,
                                               Pos,
                                               Col,
                                               Nivel,
                                               Barra,
                                               CodBodega,
                                               RackPasillo)

        End If

        If Tunel = "" Then
            ZPLString = ZPLString.Replace("Tunel", "")
        End If

        Try

            If ZPLString <> "" Then
                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
            Else
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el tipo de etiqueta"))
            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmbArea_EditValueChanged(sender As Object, e As EventArgs) Handles cmbArea.EditValueChanged
        Try
            If IMS.Listar_Tramos_By_Bodega_And_Area(cmbTramoUbic, AP.IdBodega, cmbArea.EditValue) Then
                CargaComboEtiquetas(BeBodega.IdTamañoEtiquetaUbicacionDefecto)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Cargando ubicaciones",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmUbicacion_Etiqueta_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Listando ubicaciones...")


        GridViewUbi.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFullFocus
        Dim ritem As RepositoryItemCheckEdit = TryCast(GridViewUbi.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
        AddHandler ritem.CheckedChanged, AddressOf ritemPropietario_CheckedChanged

        Try

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)

            If IMS.Listar_Areas_By_Bodega(cmbArea, AP.IdBodega) Then

                If IMS.Listar_Tramos_By_Bodega_And_Area(cmbTramoUbic, AP.IdBodega, cmbArea.EditValue) Then

                    CargaComboEtiquetas(BeBodega.IdTamañoEtiquetaUbicacionDefecto)

                End If

            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try


    End Sub

    Private Sub mnuMarcarUbicacionPicking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarUbicacionPicking.ItemClick

        Try

            mnuMarcarUbicacionPicking.Enabled = False

            If Not ListUbicacionesPorTramo Is Nothing Then

                If ListUbicacionesPorTramo.Where(Function(x) x.Seleccionar = True).Count > 0 Then

                    If XtraMessageBox.Show("Está seguro(a) de marcar/desmarcar las ubicaciones como posiciones de picking?",
                                            Text,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) = DialogResult.Yes Then

                        Dim vListUbicacionesPorTramo As New List(Of clsBeBodega_Ubicacion_Seleccion)
                        vListUbicacionesPorTramo = ListUbicacionesPorTramo.Where(Function(x) x.Seleccionar = True).ToList()
                        Dim vResult As Integer = 0
                        vResult = clsLnBodega_ubicacion.Set_Ubicacion_Picking(vListUbicacionesPorTramo)

                        If vResult > 0 Then

                            XtraMessageBox.Show("Cambio realizado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                End If

            End If


            mnuMarcarUbicacionPicking.Enabled = True

        Catch ex As Exception
            mnuMarcarUbicacionPicking.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            mnuMarcarUbicacionPicking.Enabled = True
        End Try

    End Sub

End Class