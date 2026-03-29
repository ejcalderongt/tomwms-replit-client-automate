Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Public Class clsLnBodega_ubicacion

    Public Shared Sub Cargar(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, ByRef dr As DataRow)

        Try

            With oBeBodega_ubicacion

                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_barra2 = IIf(IsDBNull(dr.Item("codigo_barra2")), "", dr.Item("codigo_barra2"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .Ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                .Ubicacion_recepcion = IIf(IsDBNull(dr.Item("ubicacion_recepcion")), False, dr.Item("ubicacion_recepcion"))
                .Ubicacion_despacho = IIf(IsDBNull(dr.Item("ubicacion_despacho")), False, dr.Item("ubicacion_despacho"))
                .Ubicacion_merma = IIf(IsDBNull(dr.Item("ubicacion_merma")), False, dr.Item("ubicacion_merma"))
                .Ubicacion_Virtual = IIf(IsDBNull(dr.Item("ubicacion_virtual")), False, dr.Item("ubicacion_virtual"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Orientacion_pos = IIf(IsDBNull(dr.Item("orientacion_pos")), "", dr.Item("orientacion_pos"))
                .ubicacion_ne = IIf(IsDBNull(dr.Item("ubicacion_ne")), False, dr.Item("ubicacion_ne"))
                .Tramo.IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Tramo.IdBodega = .IdBodega
                .Ubicacion_muelle = IIf(IsDBNull(dr.Item("ubicacion_muelle")), False, dr.Item("ubicacion_muelle"))

                clsLnBodega_tramo.Obtener(.Tramo)

                .Sector.IdSector = .Tramo.IdSector
                .Sector.IdBodega = .Tramo.IdBodega
                clsLnBodega_sector.Obtener(.Sector)

                .Posicion_X = IIf(IsDBNull(dr.Item("Posicion_X")), 0, dr.Item("Posicion_X"))
                .Posicion_Y = IIf(IsDBNull(dr.Item("Posicion_Y")), 0, dr.Item("Posicion_Y"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar_With_Tramo(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, ByRef dr As DataRow,
                                               ByRef lTransaction As SqlTransaction,
                                               ByRef lConnection As SqlConnection)

        Try

            With oBeBodega_ubicacion

                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_barra2 = IIf(IsDBNull(dr.Item("codigo_barra2")), "", dr.Item("codigo_barra2"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .Ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                .Ubicacion_recepcion = IIf(IsDBNull(dr.Item("ubicacion_recepcion")), False, dr.Item("ubicacion_recepcion"))
                .Ubicacion_despacho = IIf(IsDBNull(dr.Item("ubicacion_despacho")), False, dr.Item("ubicacion_despacho"))
                .Ubicacion_merma = IIf(IsDBNull(dr.Item("ubicacion_merma")), False, dr.Item("ubicacion_merma"))
                .Ubicacion_Virtual = IIf(IsDBNull(dr.Item("ubicacion_virtual")), False, dr.Item("ubicacion_virtual"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Orientacion_pos = IIf(IsDBNull(dr.Item("orientacion_pos")), "", dr.Item("orientacion_pos"))
                .Tramo.IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Tramo.IdBodega = .IdBodega
                .ubicacion_ne = IIf(IsDBNull(dr.Item("ubicacion_ne")), False, dr.Item("ubicacion_ne"))
                .Ubicacion_muelle = IIf(IsDBNull(dr.Item("ubicacion_muelle")), False, dr.Item("ubicacion_muelle"))

                clsLnBodega_tramo.Obtener(.Tramo, lTransaction, lConnection)

                .Sector.IdSector = .Tramo.IdSector
                .Sector.IdBodega = .IdBodega

                .Posicion_X = IIf(IsDBNull(dr.Item("Posicion_X")), 0, dr.Item("Posicion_X"))
                .Posicion_Y = IIf(IsDBNull(dr.Item("Posicion_Y")), 0, dr.Item("Posicion_Y"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar_With_Tramo_And_Sector(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, ByRef dr As DataRow,
                                                   ByRef lTransaction As SqlTransaction,
                                                   ByRef lConnection As SqlConnection)
        Try

            With oBeBodega_ubicacion

                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_barra2 = IIf(IsDBNull(dr.Item("codigo_barra2")), "", dr.Item("codigo_barra2"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .Ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                .Ubicacion_recepcion = IIf(IsDBNull(dr.Item("ubicacion_recepcion")), False, dr.Item("ubicacion_recepcion"))
                .Ubicacion_despacho = IIf(IsDBNull(dr.Item("ubicacion_despacho")), False, dr.Item("ubicacion_despacho"))
                .Ubicacion_merma = IIf(IsDBNull(dr.Item("ubicacion_merma")), False, dr.Item("ubicacion_merma"))
                .Ubicacion_Virtual = IIf(IsDBNull(dr.Item("ubicacion_virtual")), False, dr.Item("ubicacion_virtual"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Orientacion_pos = IIf(IsDBNull(dr.Item("orientacion_pos")), "", dr.Item("orientacion_pos"))
                .Tramo.IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Tramo.IdBodega = .IdBodega
                .ubicacion_ne = IIf(IsDBNull(dr.Item("ubicacion_ne")), False, dr.Item("ubicacion_ne"))
                .Ubicacion_muelle = IIf(IsDBNull(dr.Item("ubicacion_muelle")), False, dr.Item("ubicacion_muelle"))

                clsLnBodega_tramo.Obtener(.Tramo, lTransaction, lConnection)

                .Sector.IdSector = .Tramo.IdSector
                .Sector.IdBodega = .IdBodega
                clsLnBodega_sector.Obtener(.Sector, lTransaction, lConnection)

                .NombreCompleto = Get_Nombre_Completo_By_IdUbicacion(.IdUbicacion, .IdBodega, lConnection, lTransaction)

                .Posicion_X = IIf(IsDBNull(dr.Item("Posicion_X")), 0, dr.Item("Posicion_X"))
                .Posicion_Y = IIf(IsDBNull(dr.Item("Posicion_Y")), 0, dr.Item("Posicion_Y"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion,
                             ByRef dr As DataRow,
                             ByRef lTransaction As SqlTransaction,
                             ByRef lConnection As SqlConnection)
        Try

            With oBeBodega_ubicacion

                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_barra2 = IIf(IsDBNull(dr.Item("codigo_barra2")), "", dr.Item("codigo_barra2"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .Ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                .Ubicacion_recepcion = IIf(IsDBNull(dr.Item("ubicacion_recepcion")), False, dr.Item("ubicacion_recepcion"))
                .Ubicacion_despacho = IIf(IsDBNull(dr.Item("ubicacion_despacho")), False, dr.Item("ubicacion_despacho"))
                .Ubicacion_merma = IIf(IsDBNull(dr.Item("ubicacion_merma")), False, dr.Item("ubicacion_merma"))
                .Ubicacion_Virtual = IIf(IsDBNull(dr.Item("ubicacion_virtual")), False, dr.Item("ubicacion_virtual"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Orientacion_pos = IIf(IsDBNull(dr.Item("orientacion_pos")), "", dr.Item("orientacion_pos"))
                .Tramo.IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Sector.IdSector = .Tramo.IdSector
                .ubicacion_ne = IIf(IsDBNull(dr.Item("ubicacion_ne")), False, dr.Item("ubicacion_ne"))

                .Posicion_X = IIf(IsDBNull(dr.Item("Posicion_X")), 0, dr.Item("Posicion_X"))
                .Posicion_Y = IIf(IsDBNull(dr.Item("Posicion_Y")), 0, dr.Item("Posicion_Y"))

                '#EJC20180411: Deshabilitado por un tema de performance en graficador.
                'clsLnBodega_tramo.Obtener(.Tramo, lTransaction, lConnection) '#CKFK 20180603 03:48 quité el comentario para obtener los datos del tramo
                'clsLnBodega_sector.Obtener(.Sector, lTransaction, lConnection)
                '.CodigoOrientacionPos = clsLnBodega_orientacion_pos.Get_Codigo_By_IdOrientacionPos(.Orientacion_pos,lConnection,lTransaction)

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_ubicacion")
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            '"#EJC20190312: Agregar exclusión para generar ubicaciones por defecto de forma automática
            If Not oBeBodega_ubicacion.IdIndiceRotacion = 0 Then Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            If Not oBeBodega_ubicacion.IdTipoRotacion = 0 Then Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("dañado", "@dañado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Ins.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Ins.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Ins.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Ins.Add("ubicacion_virtual", "@ubicacion_virtual", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("ubicacion_ne", "@ubicacion_ne", DataType.Parametro)
            If Not oBeBodega_ubicacion.Orientacion_pos Is Nothing Then Ins.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)
            Ins.Add("ubicacion_muelle", "@ubicacion_muelle", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeBodega_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_ubicacion.Indice_x))
            If Not oBeBodega_ubicacion.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeBodega_ubicacion.IdIndiceRotacion))
            If Not oBeBodega_ubicacion.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeBodega_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeBodega_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeBodega_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeBodega_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeBodega_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeBodega_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeBodega_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeBodega_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeBodega_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_VIRTUAL", oBeBodega_ubicacion.Ubicacion_Virtual))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_NE", oBeBodega_ubicacion.ubicacion_ne))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MUELLE", oBeBodega_ubicacion.Ubicacion_muelle))
            If Not oBeBodega_ubicacion.Orientacion_pos Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeBodega_ubicacion.Orientacion_pos))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Async Function Insertar_Async(ByVal oBeBodega_ubicacion As clsBeBodega_ubicacion,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Task(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_ubicacion")
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            '"#EJC20190312: Agregar exclusión para generar ubicaciones por defecto de forma automática
            If Not oBeBodega_ubicacion.IdIndiceRotacion = 0 Then Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            If Not oBeBodega_ubicacion.IdTipoRotacion = 0 Then Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("dañado", "@dañado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Ins.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Ins.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Ins.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Ins.Add("ubicacion_virtual", "@ubicacion_virtual", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("ubicacion_ne", "@ubicacion_ne", DataType.Parametro)
            Ins.Add("ubicacion_muelle", "@ubicacion_muelle", DataType.Parametro)

            If Not oBeBodega_ubicacion.Orientacion_pos Is Nothing Then Ins.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeBodega_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_ubicacion.Indice_x))
            If Not oBeBodega_ubicacion.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeBodega_ubicacion.IdIndiceRotacion))
            If Not oBeBodega_ubicacion.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeBodega_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeBodega_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeBodega_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeBodega_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeBodega_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeBodega_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeBodega_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeBodega_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeBodega_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_VIRTUAL", oBeBodega_ubicacion.Ubicacion_Virtual))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_NE", oBeBodega_ubicacion.ubicacion_ne))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MUELLE", oBeBodega_ubicacion.Ubicacion_muelle))

            If Not oBeBodega_ubicacion.Orientacion_pos Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeBodega_ubicacion.Orientacion_pos))

            Dim rowsAffected As Task(Of Integer) = cmd.ExecuteNonQueryAsync()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Await rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega_ubicacion")
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idsector", "@idsector", DataType.Parametro)
            Upd.Add("idarea", "@idarea", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("indice_x", "@indice_x", DataType.Parametro)
            Upd.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Upd.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("dañado", "@dañado", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Upd.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Upd.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Upd.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Upd.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Upd.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Upd.Add("ubicacion_virtual", "@ubicacion_virtual", DataType.Parametro)
            Upd.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Upd.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Upd.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Upd.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Upd.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)
            Upd.Add("ubicacion_ne", "@ubicacion_ne", DataType.Parametro)
            Upd.Add("ubicacion_muelle", "@ubicacion_muelle", DataType.Parametro)
            Upd.Where("IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeBodega_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_ubicacion.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeBodega_ubicacion.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeBodega_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeBodega_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeBodega_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeBodega_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeBodega_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeBodega_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeBodega_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeBodega_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeBodega_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_VIRTUAL", oBeBodega_ubicacion.Ubicacion_Virtual))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeBodega_ubicacion.Orientacion_pos))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_NE", oBeBodega_ubicacion.ubicacion_ne))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MUELLE", oBeBodega_ubicacion.Ubicacion_muelle))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega_ubicacion" &
             "  Where(IdUbicacion = @IdUbicacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega_ubicacion"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion) As Boolean

        Obtener = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Bodega_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CM_10052018: Función creada para obtener el picking por ordenamiento de Tramos,columna,nivel.
    Public Shared Function Obtener_For_Picking(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion
                                  Where(IdUbicacion = @IdUbicacion AND IdBodega = @IdBodega) 
                                  ORDER BY bodega_ubicacion.IdTramo, 
                                  bodega_ubicacion.Indice_x,
                                  bodega_ubicacion.Nivel,
                                  bodega_ubicacion.IdUbicacion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_ubicacion.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_ubicacion, dt.Rows(0))
            Else
                '#EJC20191121: Esto en teoría no debería de suceder, solo si cambiaran la ubicación de picking y cargan un picking con una ubicación anterior.
                'Antentamente Erik, del pasado.
                MsgBox("No se encontró la definición de la ubicación de picking asoicada a la bodega", MsgBoxStyle.Exclamation, "Singularidad-20191121")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#EJC20191121: Función creada para obtener el picking por ordenamiento de Tramos,columna,nivel.
    Public Shared Function Obtener_For_Picking(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion
                                  Where(IdUbicacion = @IdUbicacion AND IdBodega = @IdBodega) 
                                  ORDER BY bodega_ubicacion.IdTramo, 
                                  bodega_ubicacion.Indice_x,
                                  bodega_ubicacion.Nivel,
                                  bodega_ubicacion.IdUbicacion"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_ubicacion.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
            Else
                '#EJC20191121: Esto en teoría no debería de suceder, solo si cambiaran la ubicación de picking y cargan un picking con una ubicación anterior.
                'Antentamente Erik, del pasado.
                MsgBox("No se encontró la definición de la ubicación de picking asoicada a la bodega", MsgBoxStyle.Exclamation, "Singularidad-20191121")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_For_Picking(ByRef oBeBodega_ubicacion As clsBeBodega_ubicacion,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As Boolean

        Get_For_Picking = False

        Try

            Const sp As String = "SELECT * FROM Bodega_ubicacion 
                                  Where(IdUbicacion = @IdUbicacion)
                                  ORDER BY bodega_ubicacion.IdTramo, 
                                  bodega_ubicacion.Indice_x,
                                  bodega_ubicacion.Nivel,
                                  bodega_ubicacion.IdUbicacion "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeBodega_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
                Return True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)
            Const sp As String = "SELECT * FROM Bodega_ubicacion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_ubicacion As New clsBeBodega_ubicacion

            For Each dr As DataRow In dt.Rows

                vBeBodega_ubicacion = New clsBeBodega_ubicacion
                Cargar(vBeBodega_ubicacion, dr)
                lReturnList.Add(vBeBodega_ubicacion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetUbicacionesStock() As List(Of clsBeBodega_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_ubicacion)
            Const sp As String = "select * from bodega_ubicacion as ubic, stock as stock where ubic.IdUbicacion=stock.IdUbicacion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_ubicacion As New clsBeBodega_ubicacion

            For Each dr As DataRow In dt.Rows

                vBeBodega_ubicacion = New clsBeBodega_ubicacion
                Cargar(vBeBodega_ubicacion, dr)
                lReturnList.Add(vBeBodega_ubicacion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeBodega_ubicacion As clsBeBodega_ubicacion)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega_ubicacion " &
            " Where(IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", pBeBodega_ubicacion.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega_ubicacion.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_ubicacion, dt.Rows(0), lTransaction, lConnection)
            Else
                pBeBodega_ubicacion = Nothing
            End If

            lTransaction.Commit()

            Return True

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdBodega As Integer) As Integer

        Try

            Dim lMax As Integer = 1

            Dim sp As String = "SELECT ISNULL(Max(IdUbicacion),0) FROM Bodega_ubicacion 
                                WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Set_Ubicacion_Picking(ByVal ListUbicacionesPorTramo As List(Of clsBeBodega_Ubicacion_Seleccion)) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer = 0

        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            For Each Ubic In ListUbicacionesPorTramo

                Upd.Init("bodega_ubicacion")
                Upd.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
                Upd.Where("IdUbicacion = @IdUbicacion AND IdBodega=@IdBodega")

                Dim sp As String = Upd.SQL()

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                cmd.Parameters.Add(New SqlParameter("@IDUBICACION", Ubic.IdUbicacion))
                cmd.Parameters.Add(New SqlParameter("@IDBODEGA", Ubic.IdBodega))
                cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", Not Ubic.Ubicacion_Picking))

                rowsAffected += cmd.ExecuteNonQuery()

                cmd.Dispose()

            Next

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    'GT29062022_1200: Actualización de ubicaciones para reabasto
    Public Shared Function Update_To_Reabasto(ByVal IdTramo As Integer, ByVal IdBodega As Integer,
                                                          ByVal colIni As Integer, ByVal colFin As Integer,
                                                          ByVal NivelIni As Integer, ByVal NivelFin As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Try

            Upd.Init("bodega_ubicacion")
            Upd.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Upd.Where("(IdTramo = @IdTramo AND IdBodega=@IdBodega 
                                       and indice_x between @colIni and @colFin and nivel between @NivelIni and @NivelFin)")

            Dim sp As String = Upd.SQL()


            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@COLINI", colIni))
            cmd.Parameters.Add(New SqlParameter("@COLFIN", colFin))
            cmd.Parameters.Add(New SqlParameter("@NIVELINI", NivelIni))
            cmd.Parameters.Add(New SqlParameter("@NIVELFIN", NivelFin))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", 1)) 'campo a actualizar como verdadero para reabasto

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Ubicaciones_Misma_Posicion(ByVal pIdBodega As Integer,
                                                      ByVal pIdTramo As Integer,
                                                      ByVal pIndiceX As Integer,
                                                      ByVal pNivel As Integer,
                                                      ByVal pIdUbicacionExcluir As Integer) As List(Of clsBeBodega_ubicacion)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * 
                              FROM Bodega_ubicacion
                              WHERE IdBodega = @IdBodega
                                AND IdTramo = @IdTramo
                                AND Indice_x = @IndiceX
                                AND Nivel = @Nivel
                                AND IdUbicacion <> @IdUbicacionExcluir
                                AND Activo = 1"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTramo", pIdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IndiceX", pIndiceX))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Nivel", pNivel))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacionExcluir", pIdUbicacionExcluir))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lReturn As New List(Of clsBeBodega_ubicacion)

            For Each dr As DataRow In dt.Rows
                Dim be As New clsBeBodega_ubicacion
                Cargar(be, dr, lTransaction, lConnection)
                lReturn.Add(be)
            Next

            lTransaction.Commit()
            Return lReturn

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function
End Class

