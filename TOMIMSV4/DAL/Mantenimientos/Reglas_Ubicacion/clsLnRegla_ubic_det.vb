Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnRegla_ubic_det

    Public Shared Function Listar(encid As Integer, active As Boolean) As DataTable

        Dim sp As String = ""
        Dim s1, s2, s3, s4, s5, s6 As String
        Dim Act As Integer

        Try

            If (active) Then Act = 1 Else Act = 0

            s1 = "SELECT dbo.regla_ubic_det_pp.IdReglaUbicacionDetPP AS ID, dbo.producto_presentacion.nombre collate database_default AS Nombre, 'Presentacion' AS Tipo "
            s1 &= "FROM dbo.regla_ubic_det_pp INNER JOIN dbo.producto_presentacion On dbo.regla_ubic_det_pp.IdPresentacion = dbo.producto_presentacion.IdPresentacion "
            s1 &= String.Format("WHERE (dbo.regla_ubic_det_pp.Activo={0}) And (dbo.regla_ubic_det_pp.IdReglaUbicacionEnc ={1}) ", Act, encid)

            s2 = "SELECT dbo.regla_ubic_det_ir.IdReglaUbicacionDetIr AS ID, dbo.indice_rotacion.Descripcion collate database_default AS Nombre, 'Indice Rotacion' AS Tipo "
            s2 &= "FROM dbo.regla_ubic_det_ir INNER JOIN  dbo.indice_rotacion On dbo.regla_ubic_det_ir.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion "
            s2 &= String.Format("WHERE (dbo.regla_ubic_det_ir.Activo ={0}) AND (dbo.regla_ubic_det_ir.IdReglaUbicacionEnc ={1}) ", Act, encid)

            s3 = "SELECT dbo.regla_ubic_det_pe.IdReglaUbicacionDetPe AS ID, dbo.producto_estado.nombre collate database_default AS Nombre, 'Producto Estado' AS Tipo "
            s3 &= "FROM dbo.regla_ubic_det_pe INNER JOIN dbo.producto_estado On dbo.regla_ubic_det_pe.IdEstado = dbo.producto_estado.IdEstado "
            s3 &= String.Format("WHERE (dbo.regla_ubic_det_pe.Activo ={0}) AND (dbo.regla_ubic_det_pe.IdReglaUbicacionEnc ={1})  ", Act, encid)

            s4 = "SELECT dbo.regla_ubic_det_prop.IdReglaUbicacionDetProp AS ID, dbo.propietarios.nombre_comercial collate database_default  AS Nombre, 'Propietario' AS Tipo "
            s4 &= "FROM  dbo.propietarios INNER JOIN  dbo.regla_ubic_det_prop On dbo.propietarios.IdPropietario = dbo.regla_ubic_det_prop.IdPropietarioBodega "
            s4 &= String.Format("WHERE (dbo.regla_ubic_det_prop.Activo ={0}) AND (dbo.regla_ubic_det_prop.IdReglaUbicacionEnc ={1}) ", Act, encid)

            s5 = "SELECT dbo.regla_ubic_det_tp.IdReglaUbicacoinTP AS ID, dbo.producto_tipo.NombreTipoProducto collate database_default AS Nombre, 'Tipo Producto' AS Tipo "
            s5 &= "FROM dbo.regla_ubic_det_tp INNER JOIN  dbo.producto_tipo On dbo.regla_ubic_det_tp.IdTipoProducto = dbo.producto_tipo.IdTipoProducto "
            s5 &= String.Format("WHERE (dbo.regla_ubic_det_tp.Activo ={0}) AND (dbo.regla_ubic_det_tp.IdReglaUbicacionEnc ={1})  ", Act, encid)

            s6 = "SELECT dbo.regla_ubic_det_tr.IdREglaUbicacionDetTr AS ID, dbo.tipo_rotacion.Descripcion collate database_default  AS Nombre, 'Tipo Rotacion' AS Tipo "
            s6 &= "FROM dbo.regla_ubic_det_tr INNER JOIN  dbo.tipo_rotacion On dbo.regla_ubic_det_tr.IdTipoRotacion = dbo.tipo_rotacion.IdTipoRotacion "
            s6 &= String.Format("WHERE (dbo.regla_ubic_det_tr.Activo ={0}) AND (dbo.regla_ubic_det_tr.IdReglaUbicacionEnc ={1})   ", Act, encid)

            sp = String.Format("{0} UNION {1} UNION {2} UNION {3} UNION {4} UNION {5}", s1, s2, s3, s4, s5, s6)
            sp &= " ORDER BY Tipo,ID "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message) & vbCrLf & sp)
        End Try

    End Function

End Class
