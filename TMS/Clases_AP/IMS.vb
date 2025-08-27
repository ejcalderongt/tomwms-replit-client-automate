Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class IMS

    Public Property UsuarioAp As New clsBeUsuario
    Public Overridable Property IdEmpresa() As Integer
    Public Overridable Property IdRol() As String

    Public Property HostName As String = ""
    Public Property IdBodega() As Integer
    Public Property NomBodega() As String
    Public Property NomEmpresa() As String
    Public Property Exigir_Politica_Contraseñas() As Boolean = False
    Public Property No_Tareas() As Integer
    Public Property LicenciaServidor As Boolean
    Public Property IdBodegaAnterior() As Integer
    Public Property IdConfiguracionInterface() As Integer

    Public vSQL As String = ""

    Public Function Existe_Ini() As Boolean
        Dim AppPath As String = CurDir() & "\"
        If IO.File.Exists(AppPath & "Conn.ini") Then
            Existe_Ini = True
        Else
            Existe_Ini = False
        End If
    End Function

    Public Shared Sub WaitCur()
        Cursor.Current = Cursors.WaitCursor
    End Sub

    Public Shared Sub DefCur()
        Cursor.Current = Cursors.Default
    End Sub

    Public Shared Sub EsNumerico(ByRef Texto As Object)
        If Texto.Text <> "" And Not IsNumeric(Texto.Text) Then
            Texto.Text = ""
            XtraMessageBox.Show("Ingrese un dato numérico!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Texto.Focus()
        End If
    End Sub

    Public Overloads Shared Function Listar_Operadores(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Operadores = False

        Dim DT As New DataTable

        Try

            DT = clsLnOperador.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdOperador"
                Cmb.Properties.DataSource = DT
                Cmb.EditValue = DT.Rows(0).Item("IdOperador")
                Cmb.ItemIndex = 0
            End If

            Listar_Operadores = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_OperadoresByInventario(ByRef Cmb As LookUpEdit, ByVal IdUbic As Integer, ByVal IdInventario As Integer) As Boolean

        Listar_OperadoresByInventario = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_inv_operador.GetAllForCombo(IdUbic, IdInventario)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombres"
                Cmb.Properties.ValueMember = "IdOperador"
                Cmb.Properties.DataSource = DT
                Cmb.EditValue = DT.Rows(0).Item("IdOperador")
                Cmb.ItemIndex = 0
            End If

            Listar_OperadoresByInventario = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_OperadoresByStock(ByRef Cmb As LookUpEdit, ByVal IdUbic As Integer, ByVal IdInventario As Integer, ByVal IdStock As Integer) As Boolean

        Listar_OperadoresByStock = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_inv_ciclico.GetAllForCombo(IdUbic, IdInventario, IdStock)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombres"
                Cmb.Properties.ValueMember = "IdOperador"
                Cmb.Properties.DataSource = DT
                Cmb.EditValue = DT.Rows(0).Item("IdOperador")
                Cmb.ItemIndex = 0
            End If

            Listar_OperadoresByStock = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Areas_By_Bodega(ByRef Cmb As LookUpEdit, ByVal pIdBodega As Integer) As Boolean

        Listar_Areas_By_Bodega = False

        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_area.Get_All_Areas_By_IdBodega_For_Combo(pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdArea"
                Cmb.Properties.DataSource = Dt
                Cmb.ItemIndex = 0
                Cmb.EditValue = Dt.Rows(0).Item("IdArea")
            End If

            Listar_Areas_By_Bodega = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Sectores_By_Area(ByRef Cmb As LookUpEdit, ByVal pIdArea As Integer, ByVal pIdBodega As Integer) As Boolean

        Listar_Sectores_By_Area = False
        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_sector.Get_All_Sector_By_IdArea_And_IdBodega_For_Combo(pIdArea, pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdSector"
                Cmb.Properties.DataSource = Dt
                Cmb.ItemIndex = 0
                Cmb.EditValue = Dt.Rows(0).Item("IdSector")
            End If

            Listar_Sectores_By_Area = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TramosBySector(ByRef Cmb As LookUpEdit, ByVal pIdSector As Integer, ByVal pIdBodega As Integer) As Boolean

        Listar_TramosBySector = False

        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_tramo.GetAllTramosBySectorForCombo(pIdSector, pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTramo"
                Cmb.Properties.DataSource = Dt
            End If

            Listar_TramosBySector = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TramosByBodega(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdBodega As Integer) As Boolean

        Listar_TramosByBodega = False

        Dim Dt As New List(Of clsBeBodega_tramo)

        Try

            Dt = clsLnBodega_tramo.Get_All_By_IdBodega(True, pIdBodega)

            If Dt.Count > 0 Then
                Cmb.DisplayMember = "Descripcion"
                Cmb.ValueMember = "IdTramo"
                Cmb.DataSource = Dt
            End If

            Listar_TramosByBodega = Dt.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TramosInventario(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TramosInventario = False

        Dim Dt As DataTable

        Try

            Dt = clsLnBodega_tramo.GetAllTramosForCombo()

            If Dt.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTramo"
                Cmb.Properties.DataSource = Dt
            End If

            Listar_TramosInventario = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Empresas(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_Empresas = False

        Dim DT As New List(Of clsBeEmpresa)

        Try

            DT = clsLnEmpresa.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdEmpresa"
                Cmb.DataSource = DT
            End If

            Listar_Empresas = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Empresas(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Empresas = False

        Dim DT As New DataTable

        Try

            DT = clsLnEmpresa.GetAllForComboBox()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdEmpresa"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Empresas = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Unidad_Medida(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Unidad_Medida = False

        Dim DT As New DataTable

        Try

            DT = clsLnUnidad_medida.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdUnidadMedida"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Unidad_Medida = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Presentaciones(ByRef Cmb As LookUpEdit, ByVal pIdProducto As Integer) As Boolean

        Listar_Presentaciones = False

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_presentacion.Get_All_By_IdProducto_For_Combo(pIdProducto)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "IdPresentacion"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Presentaciones = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_ProductoEstado(ByRef Cmb As LookUpEdit) As Boolean

        Listar_ProductoEstado = False

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_estado.GetAllByForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "IdEstado"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_ProductoEstado = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_ProductoEstadoNE(ByRef Cmb As LookUpEdit) As Boolean

        Listar_ProductoEstadoNE = False

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_estado.Get_All_For_Combo_NE()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "IdEstado"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_ProductoEstadoNE = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Paises(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Paises = False

        Dim DT As New DataTable

        Try

            DT = clsLnPaises.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdPais"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
                Return True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_EmpresaTransportePorEmpresa(ByRef Cmb As LookUpEdit, ByVal pIdEmpresa As Integer) As Boolean

        Listar_EmpresaTransportePorEmpresa = False

        Dim DT As New DataTable

        Try

            DT = clsLnEmpresa_transporte.Get_All_For_Combo(pIdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "IdEmpresaTransporte"
                Cmb.Properties.DataSource = DT
            End If

            Listar_EmpresaTransportePorEmpresa = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoContenedor(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoContenedor = False

        Dim DT As New DataTable

        Try

            DT = clsLnTipo_contenedor.Get_All_For_Combo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoContenedor"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_TipoContenedor = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoTarima(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoTarima = False

        Dim DT As New DataTable

        Try

            DT = clsLnTipo_tarima.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoTarima"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_TipoTarima = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoTarea(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoTarea = False

        Dim DT As New DataTable

        Try

            DT = clsLnSis_tipo_tarea.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoTarea"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_TipoTarea = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Creada por Bismarck Traña
    ''' Funcion retorna el listado de los propietarios que se encuentran registrados en una empresa
    ''' </summary>
    ''' <param name="Cmb">Combobox donde se cargara la lista de Propietarios</param>
    ''' ''' <param name="pIdEmpresa">idempresa que sea filtrar los propietarios</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function Listar_Propietarios_By_IdEmpresa(ByRef Cmb As LookUpEdit, ByVal pIdEmpresa As Integer) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnPropietarios.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdPropietario"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
                Cmb.Properties.PopupWidth = 700
                Cmb.Properties.PopulateColumns()
                Cmb.Properties.Columns(0).Visible = False
                Cmb.Properties.BestFit()
                Cmb.Properties.NullText = ""
            End If

            Return DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_PropietariosByEmpresaExcel(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdEmpresa As Integer) As Boolean

        Dim DT As New List(Of clsBePropietarios)

        Try

            DT = clsLnPropietarios.Get_All_By_IdEmpresa_Class(pIdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre_comercial"
                Cmb.ValueMember = "IdPropietario"
                Cmb.DataSource = DT
            End If

            Return DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoCliente(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoCliente = False

        Dim DT As New DataTable

        Try

            DT = clsLnCliente_tipo.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "NombreTipoCliente"
                Cmb.Properties.ValueMember = "IdTipoCliente"
                Cmb.Properties.DataSource = DT
            End If

            Listar_TipoCliente = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Parametro(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Parametro = False

        Dim DT As New DataTable

        Try

            DT = clsLnP_parametro.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "descripcion"
                Cmb.Properties.ValueMember = "IdParametro"
                Cmb.Properties.DataSource = DT
            End If

            Listar_Parametro = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_PerfilSerializado(ByRef Cmb As LookUpEdit) As Boolean

        Listar_PerfilSerializado = False

        Dim DT As New DataTable

        Try

            DT = clsLnPerfil_serializado.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdPerfilSerializado"
                Cmb.Properties.DataSource = DT
            End If

            Listar_PerfilSerializado = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Camara(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Camara = False

        Dim DT As New DataTable

        Try

            DT = clsLnCamara.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdCamara"
                Cmb.Properties.DataSource = DT
            End If

            Listar_Camara = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Proveedor(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_Proveedor = False

        Dim DT As New List(Of clsBeProveedor)

        Try

            DT = clsLnProveedor.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdProveedor"
                Cmb.DataSource = DT
            End If

            Listar_Proveedor = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoIngresoOC(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoIngresoOC = False

        Dim DT As New DataTable

        Try


            'DT = clsLnTrans_oc_ti.GetAllForCombo()
            'GT 28042021 el error es por los guiones bajos. Pero es prueba.
            DT = clsLnTrans_oc_ti.Get_All_ForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoIngreso"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            Else
                Throw New Exception("No hay Tipo Ingresos Definidos")
            End If

            Listar_TipoIngresoOC = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Muelles(ByRef CmbMuelles As LookUpEdit, ByVal IdBodega As Integer) As Boolean

        Listar_Muelles = False

        Dim DT As New DataTable

        Try

            DT = clsLnBodega_muelles.Get_All_By_IdBodega_For_Combo(IdBodega)

            If DT.Rows.Count > 0 Then
                CmbMuelles.Properties.DisplayMember = "nombre"
                CmbMuelles.Properties.ValueMember = "IdMuelle"
                CmbMuelles.Properties.DataSource = DT
                CmbMuelles.ItemIndex = 0
            Else
                Throw New Exception("No existen muelles para la bodega seleccionada")
            End If

            Listar_Muelles = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_RoadRutas(ByRef CmbRoadRutas As LookUpEdit) As Boolean

        Listar_RoadRutas = False

        Dim DT As New DataTable

        Try

            DT = clsLnRoad_ruta.Listar_RoadRutas(clsLnRoad_ruta.TipoRuta.Pedido)

            If DT.Rows.Count > 0 Then
                CmbRoadRutas.Properties.DisplayMember = "Nombre"
                CmbRoadRutas.Properties.ValueMember = "IdRuta"
                CmbRoadRutas.Properties.DataSource = DT
                CmbRoadRutas.ItemIndex = 0
                'Else
                '   Throw New Exception("No existen rutas road para la bodega seleccionada")
            End If

            Listar_RoadRutas = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_EstadosOC(ByRef Cmb As LookUpEdit) As Boolean

        Listar_EstadosOC = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_oc_estado.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdEstado"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_EstadosOC = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Aranceles(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Aranceles = False

        Dim DT As New DataTable

        Try

            DT = clsLnArancel.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdArancel"
                Cmb.Properties.DataSource = DT
            End If

            Listar_Aranceles = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Regimen_Fiscal(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Regimen_Fiscal = False

        Dim DT As New DataTable

        Try

            DT = clsLnRegimen_fiscal.Get_All_For_Combo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "descripcion"
                Cmb.Properties.ValueMember = "codigo_regimen"
                Cmb.Properties.DataSource = DT
            End If

            Listar_Regimen_Fiscal = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Listar_Bodegas_Login(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_Bodegas_Login = False

        Dim DT As New DataTable

        If IdEmpresa = 0 Then Exit Function

        Try

            DT = clsLnBodega.Get_All_By_Empresa_ForCombo(IdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdBodega"
                Cmb.DataSource = DT
            End If

            Listar_Bodegas_Login = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Listar_BodegasLogin(ByRef Cmb As LookUpEdit) As Boolean

        Listar_BodegasLogin = False

        Dim DT As New DataTable

        If IdEmpresa = 0 Then Exit Function

        Try

            DT = clsLnBodega.Get_All_By_Empresa_ForCombo(IdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdBodega"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_BodegasLogin = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Listar_Bodegas_By_Usuario(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Bodegas_By_Usuario = False

        Dim DT As New DataTable

        Try

            DT = clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario_DT(IdEmpresa,
                                                                   AP.UsuarioAp.IdUsuario)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "Codigo"
                Cmb.Properties.DataSource = DT
                Cmb.EditValue = AP.IdBodega
            Else
                Throw New Exception("No hay definidas bodegas para la empresa: " & IdEmpresa)
            End If

            Listar_Bodegas_By_Usuario = DT.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Listar_Bodegas() As DataTable

        Try

            Dim DT As New DataTable("Bodega")

            DT = clsLnBodega.Listar_Bodegas_Activas()

            Return DT

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Listar_Bodegas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return New DataTable("Error")
        End Try

    End Function

    Public Shared Function Listar_Bodegas_Por_Empresa(ByRef Cmb As LookUpEdit, pIdEmpresa As Integer) As Boolean

        Listar_Bodegas_Por_Empresa = False

        Dim DT As New DataTable

        Try

            DT = clsLnBodega.Get_All_By_Empresa_ForCombo(pIdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdBodega"
                Cmb.Properties.DataSource = DT
                'Cmb.ItemIndex = 0
            End If

            Listar_Bodegas_Por_Empresa = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Lista_Pedido_Ingreso(ByRef Cmb As LookUpEdit) As Boolean

        Lista_Pedido_Ingreso = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_oc_ti.Get_All_ForCombo

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoIngresoOC"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Lista_Pedido_Ingreso = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Lista_Pedido_Salida(ByRef Cmb As LookUpEdit) As Boolean

        Lista_Pedido_Salida = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_pe_tipo.Get_All_ForCombo

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoPedido"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Lista_Pedido_Salida = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Tipo_Ajuste(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Tipo_Ajuste = False

        Dim DT As New DataTable

        Try

            DT = clsLnAjuste_tipo.Get_All_ForCombo

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoAjuste"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Tipo_Ajuste = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_ClientesByEmpresa(ByRef Cmb As LookUpEdit, pIdEmpresa As Integer) As Boolean

        Listar_ClientesByEmpresa = False

        Dim DT As New DataTable

        Try

            DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdCliente"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_ClientesByEmpresa = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Producto_Familia(ByRef Cmb As LookUpEdit, ByVal pIdPropietario As Integer) As Boolean

        Listar_Producto_Familia = False

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_familia.Get_All_By_IdPropietario(True, pIdPropietario)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdFamilia"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Producto_Familia = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    ''' <summary>
    ''' Metodo Creado por Bismarck
    ''' 
    ''' Carga el listado de Bodega en los cuales es encuntra asignado un propietario
    ''' 
    ''' </summary>
    ''' <param name="cmb">Combobox que sera llenado con las Bodega</param>
    ''' <param name="pIdPropietario">Filtro para llenar el combobox</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function Listar_BodegasPorPropietario(ByRef cmb As LookUpEdit, pIdPropietario As Integer, Optional ByVal idBodega As Integer = -1) As Boolean

        Listar_BodegasPorPropietario = False

        Try

            Dim lBod As New List(Of clsBeBodega)

            lBod = clsLnBodega.Get_All_By_IdPropietario(pIdPropietario)

            If Not lBod Is Nothing Then
                cmb.Properties.DisplayMember = "Nombre"
                cmb.Properties.ValueMember = "IdBodega"
                cmb.Properties.DataSource = lBod
                cmb.ItemIndex = 0
                Listar_BodegasPorPropietario = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_JornadasPorBodega(ByRef Cmb As LookUpEdit, pIdBodega As Integer) As Boolean

        Listar_JornadasPorBodega = False

        Dim DT As New DataTable

        Try

            DT = clsLnJornada_laboral.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre_jornada"
                Cmb.Properties.ValueMember = "IdJornada"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_JornadasPorBodega = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Turnos(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Turnos = False

        Dim DT As New DataTable

        Try

            DT = clsLnTurno.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "IdTurno"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_Turnos = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Departamentos(ByRef CmbDepto As LookUpEdit, ByVal IdPais As Integer) As Boolean

        Listar_Departamentos = False

        Dim DT As New DataTable

        Try

            DT = clsLnPais_departamento.GetAllForCombo(IdPais)

            If DT.Rows.Count > 0 Then
                CmbDepto.Properties.DisplayMember = "Nombre"
                CmbDepto.Properties.ValueMember = "IdDepartamento"
                CmbDepto.Properties.DataSource = DT
                CmbDepto.ItemIndex = 0
            End If

            Listar_Departamentos = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Region(ByRef CmbRegion As LookUpEdit, ByVal IdPais As Integer) As Boolean

        Listar_Region = False

        Dim DT As New DataTable

        Try

            DT = clsLnPais_region.GetAllForCombo(IdPais)

            If DT.Rows.Count > 0 Then
                CmbRegion.Properties.DisplayMember = "Nombre"
                CmbRegion.Properties.ValueMember = "IdRegion"
                CmbRegion.Properties.DataSource = DT
                CmbRegion.ItemIndex = 0
            End If

            Listar_Region = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Municipios(ByRef CmbMuni As LookUpEdit, ByVal IdDepartamento As Integer) As Boolean

        Listar_Municipios = False

        Dim DT As New DataTable

        Try

            DT = clsLnPais_municipio.GetAllForCombo(IdDepartamento)

            If DT.Rows.Count > 0 Then
                CmbMuni.Properties.DisplayMember = "Nombre"
                CmbMuni.Properties.ValueMember = "IdMunicipio"
                CmbMuni.Properties.DataSource = DT
                CmbMuni.ItemIndex = 0
            End If

            Listar_Municipios = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_MotivoUbicacion(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_MotivoUbicacion = False

        Dim DT As New List(Of clsBeMotivo_ubicacion)

        Try

            DT = clsLnMotivo_ubicacion.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdMotivoUbicacion"
                Cmb.DataSource = DT
            End If

            Listar_MotivoUbicacion = DT.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Overloads Shared Function Listar_Reglas(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_Reglas = False

        Dim DT As New List(Of clsBeReglas_recepcion)

        Try

            DT = clsLnReglas_recepcion.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdReglaRecepcion"
                Cmb.DataSource = DT
            End If

            Listar_Reglas = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Mensaje(ByRef Cmb As System.Windows.Forms.ComboBox) As Boolean

        Listar_Mensaje = False

        Dim DT As New List(Of clsBeMensaje_regla)

        Try

            DT = clsLnMensaje_regla.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdMensajeRegla"
                Cmb.DataSource = DT
            End If

            Listar_Mensaje = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Listar_DestinatarioByPropietario(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdPropietario As Integer) As Boolean

        Listar_DestinatarioByPropietario = False

        Dim DT As New List(Of clsBePropietario_destinatario)

        Try

            DT = clsLnPropietario_destinatario.GetAllByIdPropietario(pIdPropietario)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdDestinatarioPropietario"
                Cmb.DataSource = DT
            End If

            Listar_DestinatarioByPropietario = DT.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Sub Cerrar_Ventana(ByVal frm As Form)

        Try

            If XtraMessageBox.Show(String.Format("¿Salir de {0}?", frm.Text), "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                frm.Close()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Function Confirma_Transaccion() As Boolean

        If XtraMessageBox.Show("¿Esta seguro de guardar el registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Confirma_Transaccion = True
        Else
            Confirma_Transaccion = False
        End If

    End Function

    Public Overloads Shared Function Listar_VendedoresByRuta(ByRef cmbVendedor As LookUpEdit, ByVal IdRuta As Integer) As Boolean

        Listar_VendedoresByRuta = False

        Dim DT As New DataTable

        Try

            DT = clsLnRoad_p_vendedor.GetAllByRutaForCombo(IdRuta)

            If Not DT Is Nothing Then
                cmbVendedor.Properties.DisplayMember = "nombre"
                cmbVendedor.Properties.ValueMember = "IdRuta"
                cmbVendedor.Properties.DataSource = DT
                cmbVendedor.ItemIndex = 0
            End If

            Listar_VendedoresByRuta = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TiposPedido(ByRef cmbTipoPedido As LookUpEdit) As Boolean

        Listar_TiposPedido = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_pe_tipo.Get_All_ForCombo()

            If DT.Rows.Count > 0 Then
                cmbTipoPedido.Properties.DisplayMember = "Descripcion"
                cmbTipoPedido.Properties.ValueMember = "IdTipoPedido"
                cmbTipoPedido.Properties.DataSource = DT
                cmbTipoPedido.ItemIndex = 2
            End If

            Listar_TiposPedido = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_IdPropietario_By_IdBodega(ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer) As Integer

        Get_IdPropietario_By_IdBodega = 0

        Try

            Get_IdPropietario_By_IdBodega = clsLnPropietarios.Get_IdPropietario(pIdBodega, pIdPropietarioBodega)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Overloads Shared Function Listar_Propietarios_By_IdBodega(ByRef Cmb As LookUpEdit, ByVal pIdBodega As Integer, Optional ByVal ValueMemberIsIdPropietarioBodega As Boolean = True) As Boolean

        Listar_Propietarios_By_IdBodega = False

        Dim DT1 As New DataTable

        Try

            If ValueMemberIsIdPropietarioBodega Then

                DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(pIdBodega)

                Cmb.Properties.ValueMember = "IdPropietarioBodega"
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.DataSource = DT1
                Cmb.Properties.PopulateColumns()

                If Cmb.Properties.Columns.Count > 0 Then
                    Cmb.Properties.Columns(0).Visible = False
                    Cmb.Properties.Columns(1).Visible = False
                End If

                Cmb.Properties.PopupWidth = 700
                Cmb.Properties.BestFit()
                Cmb.Properties.NullText = ""

                If DT1.Rows.Count > 0 Then
                    Cmb.EditValue = IIf(IsDBNull(DT1.Rows(0).Item("IdPropietarioBodega")), 0, DT1.Rows(0).Item("IdPropietarioBodega"))
                    Listar_Propietarios_By_IdBodega = DT1.Rows.Count > 0
                End If

            Else

                Cmb.Properties.ValueMember = "IdPropietario"
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.DataSource = DT1
                Cmb.Properties.PopulateColumns()
                Cmb.Properties.Columns(0).Visible = False
                Cmb.Properties.Columns(1).Visible = False
                Cmb.Properties.PopupWidth = 700
                Cmb.Properties.BestFit()
                Cmb.Properties.NullText = ""
                Cmb.ItemIndex = 0
                Listar_Propietarios_By_IdBodega = DT1.Rows.Count > 0

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_IndiceRotacion(ByRef Cmb As LookUpEdit) As Boolean

        Listar_IndiceRotacion = False

        Dim DT As New DataTable

        Try

            DT = clsLnIndice_rotacion.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdIndiceRotacion"
                Cmb.Properties.DataSource = DT
            End If

            Listar_IndiceRotacion = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoInventario(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoInventario = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_inv_enc.GetAllForComboTipoInv()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Descripcion"
                Cmb.Properties.ValueMember = "IdTipoInv"
                Cmb.Properties.DataSource = DT
            End If

            Listar_TipoInventario = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoConteo(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoConteo = False

        Dim DT As New DataTable

        Try

            DT = clsLnTrans_inv_enc.GetAllForComboTipoConteo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Descripcion"
                Cmb.Properties.ValueMember = "IdTipoConteo"
                Cmb.Properties.DataSource = DT
            End If

            Listar_TipoConteo = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoRotacion(ByRef Cmb As LookUpEdit) As Boolean

        Listar_TipoRotacion = False

        Dim DT As New DataTable

        Try

            DT = clsLnTipo_rotacion.GetAllForCombo(True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdTipoRotacion"
                Cmb.Properties.DataSource = DT
            End If

            Listar_TipoRotacion = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_FontTramo(ByRef Cmb As LookUpEdit) As Boolean

        Listar_FontTramo = False

        Dim DT As New DataTable

        Try

            DT = clsLnFont_enc.GetAllForCombo()

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdFont"
                Cmb.Properties.DataSource = DT
            End If

            Listar_FontTramo = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_MotivosAjuste(ByRef Cmb As LookUpEdit) As Boolean

        Listar_MotivosAjuste = False

        Dim Dt As New DataTable

        Try

            Dt = clsLnAjuste_motivo.GetAllForCombo()

            If Dt.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "nombre"
                Cmb.Properties.ValueMember = "idmotivoajuste"
                Cmb.Properties.DataSource = Dt
            End If

            Listar_MotivosAjuste = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Listar_ClientesByEmpresaSistema(ByRef Cmb As LookUpEdit, pIdEmpresa As Integer) As Boolean

        Listar_ClientesByEmpresaSistema = False

        Dim DT As New DataTable

        Try

            DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa, True)

            If DT.Rows.Count > 0 Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdCliente"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

            Listar_ClientesByEmpresaSistema = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

#Region "Funciones interface"

    Public Shared Function Listar_BodegasPorEmpresa(ByRef Cmb As System.Windows.Forms.ComboBox, pIdEmpresa As Integer) As Boolean

        Listar_BodegasPorEmpresa = False

        Dim DT As New List(Of clsBeBodega)

        Try

            DT = clsLnBodega.Get_All_By_IdEmpresa(pIdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idbodega"
                Cmb.DataSource = DT
            End If

            Listar_BodegasPorEmpresa = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Overloads Shared Function Listar_PropietariosByEmpresa(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdEmpresa As Integer) As Boolean

        Dim DT As New List(Of clsBePropietarios)

        Try

            DT = clsLnPropietarios.Get_All_By_IdEmpresa_Class(pIdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre_comercial"
                Cmb.ValueMember = "IdPropietario"
                Cmb.DataSource = DT
            End If

            Return DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_UsuariosSistemas(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdEmpresa As Integer) As Boolean

        Dim DT As New List(Of clsBeUsuario)

        Try

            DT = clsLnUsuario.GetAllUsuariosSistema(pIdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombres"
                Cmb.ValueMember = "IdUsuario"
                Cmb.DataSource = DT
            End If

            Return DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

#End Region

End Class
