Imports System.Reflection
Imports TOMWMS

Public Class IMS


    Public Property Bodega As New clsBeBodega()
    Public Property Empresa As New clsBeEmpresa
    Public Property Exigir_Politica_Contraseñas() As Boolean = False
    Public Property LicenciaServidor As Boolean
    Public Property IdBodegaAnterior() As Integer
    Public Property IdConfiguracionInterface() As Integer
    Public Property Nombre_Skin As String = ""
    Public Property InterfaceSAP As Boolean = False
    Public Property UsuarioAp As New clsBeUsuario
    Public Property UsuarioInterface As New clsBeUsuario
    Public Overridable Property IdEmpresa() As String
    Public Overridable Property IdRol() As String
    Public Property HostName As String = ""
    Public Property IdBodega() As String
    Public Property NomBodega() As String
    Public Property NomEmpresa() As String
    Public Property No_Tareas() As Integer

    Public vSQL As String = ""

    Public Function Existe_Ini() As Boolean
        If IO.File.Exists(CurDir() & "\" & "Conn.ini") Then
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
            MessageBox.Show("Ingrese un dato numérico!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Texto.Focus()
        End If
    End Sub

    Public Overloads Shared Function Listar_FamiliasByPropietario(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdPropietario As Integer) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_familia.Get_All_By_IdPropietario(True, pIdPropietario)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdFamilia"
                Cmb.DataSource = DT
            End If

            Return DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_ClasificacionesByPropietario(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdPropietario As Integer) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_clasificacion.Get_All_By_Propietario(pIdPropietario)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdClasificacion"
                Cmb.DataSource = DT
            End If

            Return DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_MarcasByPropietario(ByRef Cmb As System.Windows.Forms.ComboBox,
                                                                ByVal pIdPropietario As Integer) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_marca.Get_All_By_Propietario(pIdPropietario)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdMarca"
                Cmb.DataSource = DT
            End If

            Return DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoProductoByPropietario(ByRef Cmb As System.Windows.Forms.ComboBox, ByVal pIdPropietario As Integer) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnProducto_tipo.Get_All_By_Propietario(pIdPropietario)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "NombreTipoProducto"
                Cmb.ValueMember = "IdTipoProducto"
                Cmb.DataSource = DT
            End If

            Return DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_AreasbyBodega(ByRef Cmb As ComboBox, ByVal pIdBodega As Integer) As Boolean

        Listar_AreasbyBodega = False

        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_area.Get_All_Areas_By_IdBodega(pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.DisplayMember = "Descripcion"
                Cmb.ValueMember = "IdArea"
                Cmb.DataSource = Dt
            End If

            Listar_AreasbyBodega = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_SectoresbyArea(ByRef Cmb As ComboBox, ByVal pIdArea As Integer, ByVal pIdBodega As Integer) As Boolean

        Listar_SectoresbyArea = False
        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_sector.Get_All_Sector_By_Area_And_IdBodega(pIdArea, pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.DisplayMember = "Descripcion"
                Cmb.ValueMember = "IdSector"
                Cmb.DataSource = Dt
            End If

            Listar_SectoresbyArea = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TramosBySector(ByRef Cmb As ComboBox, ByVal pIdSector As Integer, ByVal pIdBodega As Integer) As Boolean

        Listar_TramosBySector = False

        Dim Dt As New DataTable

        Try

            Dt = clsLnBodega_tramo.Get_All_Tramos_By_Sector_And_IdBodega(pIdSector, pIdBodega)

            If Dt.Rows.Count > 0 Then
                Cmb.DisplayMember = "Descripcion"
                Cmb.ValueMember = "IdTramo"
                Cmb.DataSource = Dt
            End If

            Listar_TramosBySector = Dt.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TramosByBodega(ByRef Cmb As ComboBox, ByVal pIdBodega As Integer) As Boolean

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

    Public Shared Function Listar_Entidades(ByRef Cmb As ComboBox) As Boolean

        Listar_Entidades = False

        Dim DT As New List(Of clsBeI_nav_ent)

        Try

            DT = clsLnI_nav_ent.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idnavent"
                Cmb.DataSource = DT
            End If

            Listar_Entidades = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Overloads Shared Function Listar_Empresas(ByRef Cmb As ComboBox) As Boolean

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

    Public Overloads Shared Function Listar_Paises(ByRef Cmb As ComboBox) As Boolean

        Dim DT As New List(Of clsBePaises)

        Try

            DT = clsLnPaises.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdPais"
                Cmb.DataSource = DT
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_EmpresaTransportePorEmpresa(ByRef Cmb As ComboBox, ByVal pIdEmpresa As Integer) As Boolean

        Listar_EmpresaTransportePorEmpresa = False

        Dim DT As New List(Of clsBeEmpresa_transporte)

        Try

            DT = clsLnEmpresa_transporte.GetAllByIdEmpresa(pIdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdEmpresaTransporte"
                Cmb.DataSource = DT
            End If

            Listar_EmpresaTransportePorEmpresa = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoContenedor(ByRef Cmb As ComboBox) As Boolean

        Listar_TipoContenedor = False

        Dim DT As New List(Of clsBeTipo_contenedor)

        Try

            DT = clsLnTipo_contenedor.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdTipoContenedor"
                Cmb.DataSource = DT
            End If

            Listar_TipoContenedor = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoTarima(ByRef Cmb As ComboBox) As Boolean

        Listar_TipoTarima = False

        Dim DT As New List(Of clsBeTipo_tarima)

        Try

            DT = clsLnTipo_tarima.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdTipoTarima"
                Cmb.DataSource = DT
            End If

            Listar_TipoTarima = DT.Count > 0

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
    Public Overloads Shared Function Listar_PropietariosByEmpresa(ByRef Cmb As ComboBox, ByVal pIdEmpresa As Integer) As Boolean

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

    Public Overloads Shared Function Listar_TipoCliente(ByRef Cmb As ComboBox) As Boolean

        Listar_TipoCliente = False

        Dim DT As New List(Of clsBeCliente_tipo)

        Try

            DT = clsLnCliente_tipo.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "NombreTipoCliente"
                Cmb.ValueMember = "IdTipoCliente"
                Cmb.DataSource = DT
            End If

            Listar_TipoCliente = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Parametro(ByRef Cmb As ComboBox) As Boolean

        Listar_Parametro = False

        Dim DT As New List(Of clsBeP_parametro)

        Try

            DT = clsLnP_parametro.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "descripcion"
                Cmb.ValueMember = "IdParametro"
                Cmb.DataSource = DT
            End If

            Listar_Parametro = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_PerfilSerializado(ByRef Cmb As ComboBox) As Boolean

        Listar_PerfilSerializado = False

        Dim DT As New List(Of clsBePerfil_serializado)

        Try

            DT = clsLnPerfil_serializado.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "descripcion"
                Cmb.ValueMember = "IdPerfilSerializado"
                Cmb.DataSource = DT
            End If

            Listar_PerfilSerializado = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Camara(ByRef Cmb As ComboBox) As Boolean

        Listar_Camara = False

        Dim DT As New List(Of clsBeCamara)

        Try

            DT = clsLnCamara.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdCamara"
                Cmb.DataSource = DT
            End If

            Listar_Camara = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Proveedor(ByRef Cmb As ComboBox) As Boolean

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

    Public Overloads Shared Function Listar_TipoIngresoOC(ByRef Cmb As ComboBox) As Boolean

        Listar_TipoIngresoOC = False

        Dim DT As New List(Of clsBeTrans_oc_ti)

        Try

            DT = clsLnTrans_oc_ti.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdTipoIngresoOC"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay Tipo Ingresos Definidos")
            End If

            Listar_TipoIngresoOC = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Muelles(ByRef CmbMuelles As ComboBox, ByVal IdBodega As Integer) As Boolean

        Listar_Muelles = False

        Dim DT As New List(Of clsBeBodega_muelles)

        Try

            DT = clsLnBodega_muelles.GetAllByBodega(IdBodega)

            If DT.Count > 0 Then
                CmbMuelles.DisplayMember = "Nombre"
                CmbMuelles.ValueMember = "IdMuelle"
                CmbMuelles.DataSource = DT
            Else
                Throw New Exception("No existen muelles para la bodega seleccionada")
            End If

            Listar_Muelles = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_EstadosOC(ByRef Cmb As ComboBox) As Boolean

        Listar_EstadosOC = False

        Dim DT As New List(Of clsBeTrans_oc_estado)

        Try

            DT = clsLnTrans_oc_estado.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdEstadoOC"
                Cmb.DataSource = DT
            End If

            Listar_EstadosOC = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_Aranceles(ByRef Cmb As ComboBox) As Boolean

        Listar_Aranceles = False

        Dim DT As New List(Of clsBeArancel)

        Try

            DT = clsLnArancel.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdArancel"
                Cmb.DataSource = DT
            End If

            Listar_Aranceles = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Listar_BodegasLogin(ByRef Cmb As ComboBox) As Boolean

        Listar_BodegasLogin = False

        Dim DT As New List(Of clsBeBodega)

        If IdEmpresa Is Nothing Then Exit Function

        Try

            DT = clsLnBodega.Get_All_By_IdEmpresa(IdEmpresa)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idbodega"
                Cmb.DataSource = DT
            End If

            Listar_BodegasLogin = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Bodegas() As DataTable

        Try

            Dim DT As New DataTable("Bodega")

            DT = clsLnBodega.Listar()

            Return DT

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Listar_Bodegas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return New DataTable("Error")
        End Try

    End Function

    Public Shared Function Listar_BodegasPorEmpresa(ByRef Cmb As ComboBox, pIdEmpresa As Integer) As Boolean

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
    Function Listar_BodegasPorPropietario(ByRef cmb As ComboBox, pIdPropietario As Integer) As Boolean

        Listar_BodegasPorPropietario = False

        Try

            Dim lBod As New List(Of clsBeBodega)

            lBod = clsLnBodega.Get_All_By_IdPropietario(pIdPropietario)

            If Not lBod Is Nothing Then
                cmb.DisplayMember = "nombre"
                cmb.ValueMember = "idbodega"
                cmb.DataSource = lBod
                Listar_BodegasPorPropietario = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_JornadasPorBodega(ByRef Cmb As ComboBox) As Boolean

        Listar_JornadasPorBodega = False

        Dim DT As New List(Of clsBeJornada_laboral)

        Try

            DT = clsLnJornada_laboral.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre_Jornada"
                Cmb.ValueMember = "IdJornada"
                Cmb.DataSource = DT
            End If

            Listar_JornadasPorBodega = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Turnos(ByRef Cmb As ComboBox) As Boolean

        Listar_Turnos = False

        Dim DT As New List(Of clsBeTurno)

        Try

            DT = clsLnTurno.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdTurno"
                Cmb.DataSource = DT
            End If

            Listar_Turnos = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Departamentos(ByRef CmbDepto As ComboBox, ByVal IdPais As Integer) As Boolean

        Listar_Departamentos = False

        Dim DT As New List(Of clsBePais_departamento)

        Try

            DT = clsLnPais_departamento.GetAllByIdPais(IdPais)

            If DT.Count > 0 Then
                CmbDepto.DisplayMember = "Nombre"
                CmbDepto.ValueMember = "IdDepartamento"
                CmbDepto.DataSource = DT
            End If

            Listar_Departamentos = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Region(ByRef CmbRegion As ComboBox, ByVal IdPais As Integer) As Boolean

        Listar_Region = False

        Dim DT As New List(Of clsBePais_region)

        Try

            DT = clsLnPais_region.GetAllByIdPais(IdPais)

            If DT.Count > 0 Then
                CmbRegion.DisplayMember = "Nombre"
                CmbRegion.ValueMember = "IdRegion"
                CmbRegion.DataSource = DT
            End If

            Listar_Region = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Municipios(ByRef CmbMuni As ComboBox, ByVal IdDepartamento As Integer) As Boolean

        Listar_Municipios = False

        Dim DT As New List(Of clsBePais_municipio)

        Try

            DT = clsLnPais_municipio.GetAllByIdDepartamento(IdDepartamento)

            If DT.Count > 0 Then
                CmbMuni.DisplayMember = "Nombre"
                CmbMuni.ValueMember = "IdMunicipio"
                CmbMuni.DataSource = DT
            End If

            Listar_Municipios = DT.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_MotivoUbicacion(ByRef Cmb As ComboBox) As Boolean

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
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Overloads Shared Function Listar_Reglas(ByRef Cmb As ComboBox) As Boolean

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

    Public Overloads Shared Function Listar_Mensaje(ByRef Cmb As ComboBox) As Boolean

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

    Public Shared Function Listar_DestinatarioByPropietario(ByRef Cmb As ComboBox, ByVal pIdPropietario As Integer) As Boolean

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
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Sub Cerrar_Ventana(ByVal frm As Form)

        Try

            If MessageBox.Show(String.Format("¿Salir de {0}?", frm.Text), "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                frm.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Shared Function Confirma_Transaccion() As Boolean

        If MessageBox.Show("¿Esta seguro de guardar el registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Confirma_Transaccion = True
        Else
            Confirma_Transaccion = False
        End If

    End Function

    Public Overloads Shared Function Listar_VendedoresByRuta(ByRef cmbVendedor As ComboBox, ByVal IdRuta As Integer) As Boolean

        Listar_VendedoresByRuta = False

        Dim DT As New List(Of clsBeRoad_p_vendedor)

        Try

            DT = clsLnRoad_p_vendedor.GetAllByRuta(IdRuta)

            If Not DT Is Nothing Then
                cmbVendedor.DisplayMember = "Nombre"
                cmbVendedor.ValueMember = "IdRuta"
                cmbVendedor.DataSource = DT
            End If

            Listar_VendedoresByRuta = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TiposPedido(ByRef cmbTipoPedido As ComboBox) As Boolean

        Listar_TiposPedido = False

        Dim DT As New List(Of clsBeTrans_pe_tipo)

        Try

            DT = clsLnTrans_pe_tipo.Get_All_BeTransPeTipo()

            If DT.Count > 0 Then
                cmbTipoPedido.DisplayMember = "Nombre"
                cmbTipoPedido.ValueMember = "IdTipoPedido"
                cmbTipoPedido.DataSource = DT
            End If

            Listar_TiposPedido = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetIdPropietario(ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer) As Integer

        GetIdPropietario = 0

        Try

            GetIdPropietario = clsLnPropietarios.Get_IdPropietario(pIdBodega, pIdPropietarioBodega)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Overloads Shared Function Listar_PropietariosByBodega(ByRef Cmb As ComboBox, ByVal pIdBodega As Integer, Optional ByVal ValueMemberIsIdPropietarioBodega As Boolean = True) As Boolean

        Listar_PropietariosByBodega = False

        Dim DT As New List(Of clsBePropietarios)
        Dim DT1 As New List(Of clsBePropietario_bodega)

        Try

            If ValueMemberIsIdPropietarioBodega Then

                DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega(pIdBodega)

                Dim ProBod = (From Prop In DT1 Select New With {.Id = Prop.IdPropietarioBodega, .Nombre = Prop.Propietario.Nombre_comercial}).ToList

                Cmb.ValueMember = "Id"
                Cmb.DisplayMember = "Nombre"
                Cmb.DataSource = ProBod

                Listar_PropietariosByBodega = DT1.Count > 0

            Else

                DT = clsLnPropietarios.Get_All_By_IdBodega(pIdBodega)
                Cmb.ValueMember = "IdPropietario"
                Cmb.DisplayMember = "Nombre_Comercial"
                Cmb.DataSource = DT
                Listar_PropietariosByBodega = DT.Count > 0

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_IndiceRotacion(ByRef Cmb As ComboBox) As Boolean

        Listar_IndiceRotacion = False

        Dim DT As New List(Of clsBeIndice_rotacion)

        Try

            DT = clsLnIndice_rotacion.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "descripcion"
                Cmb.ValueMember = "IdIndiceRotacion"
                Cmb.DataSource = DT
            End If

            Listar_IndiceRotacion = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_TipoRotacion(ByRef Cmb As ComboBox) As Boolean

        Listar_TipoRotacion = False

        Dim DT As New List(Of clsBeTipo_rotacion)

        Try

            DT = clsLnTipo_rotacion.GetAll(True)

            If DT.Count > 0 Then
                Cmb.DisplayMember = "descripcion"
                Cmb.ValueMember = "IdTipoRotacion"
                Cmb.DataSource = DT
            End If

            Listar_TipoRotacion = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_FontTramo(ByRef Cmb As ComboBox) As Boolean

        Listar_FontTramo = False

        Dim DT As New List(Of clsBeFont_enc)

        Try

            DT = clsLnFont_enc.GetAll()

            If DT.Count > 0 Then
                Cmb.DisplayMember = "Nombre"
                Cmb.ValueMember = "IdFontEnc"
                Cmb.DataSource = DT
            End If

            Listar_FontTramo = DT.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Shared Function Listar_UsuariosSistemas(ByRef Cmb As ComboBox, ByVal pIdEmpresa As Integer) As Boolean

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

End Class
