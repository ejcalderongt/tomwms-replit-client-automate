Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_prio_producto

    Public Shared Function ListarUbicaciones(PropID As Integer, reglaid As Integer) As DataTable

        Dim du As New DataTable
        Dim dr() As DataRow
        Dim sp As String = " "
        Dim i, j, id, ir, val As Integer

        Try

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            sp = "SELECT regla_ubic_sel.IdUbicacion, regla_ubic_sel.IdReglaUbicacionEnc "
            sp &= "FROM regla_ubic_sel INNER Join producto ON regla_ubic_sel.IdUbicacion = producto.IdProducto "

            '#HS20171023_1630pm: Quité String.Format.
            sp &= "WHERE (producto.IdPropietario =@IdPropietario) AND (regla_ubic_sel.IdReglaUbicacionEnc =@IdReglaUbicacionEnc) "

            Dim cmd2 As New SqlCommand(sp, lConnection)
            cmd2.CommandType = CommandType.Text
            Dim dad2 As New SqlDataAdapter(cmd2)
            dad2.Fill(du)

            '#HS20171023_1630pm: Quité String.Format.
            sp = "SELECT Nombre, IdProducto, 0 FROM producto WHERE (IdPropietario =@IdPropietario) ORDER BY Nombre "

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", PropID)
            dad.SelectCommand.Parameters.AddWithValue("@IdReglaUbicacionEnc", reglaid)

            Dim dt As New DataTable
            dad.Fill(dt)

            For i = 0 To dt.Rows.Count - 1

                val = 0
                id = dt.Rows(i).Item(1)

                dr = du.Select("IdUbicacion=" & id)

                If dr.Count > 0 Then

                    For j = 0 To dr.Count - 1
                        ir = dr(j).Item(1)
                        If (ir = reglaid) Then val = 1
                    Next

                End If

                dt.Rows(i).Item(2) = val

            Next

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRegla_Ubic_Prio_Enc(ByVal IdReglaUbicPrioEnc As Integer, Optional ByVal LlenarParametrosProducto As Boolean = False) As List(Of clsBeRegla_ubic_prio_producto)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_prio_producto)
            Const sp As String = "SELECT * FROM Regla_ubic_prio_producto WHERE IdReglaUbicPrioEnc = @IdReglaUbicPrioEnc "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdReglaUbicPrioEnc", IdReglaUbicPrioEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_prio_producto As New clsBeRegla_ubic_prio_producto

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_prio_producto = New clsBeRegla_ubic_prio_producto
                Cargar(vBeRegla_ubic_prio_producto, dr)
                vBeRegla_ubic_prio_producto.IsNew = False

                If LlenarParametrosProducto Then

                    Dim pCamposCargar(4) As clsBeProducto.ProdPropiedades
                    pCamposCargar(0) = clsBeProducto.ProdPropiedades.IdProducto
                    pCamposCargar(1) = clsBeProducto.ProdPropiedades.Codigo
                    pCamposCargar(2) = clsBeProducto.ProdPropiedades.Nombre
                    pCamposCargar(3) = clsBeProducto.ProdPropiedades.Codigos_Barra
                    pCamposCargar(4) = clsBeProducto.ProdPropiedades.Propietario
                    vBeRegla_ubic_prio_producto.Producto.IdProducto = vBeRegla_ubic_prio_producto.IdProducto
                    vBeRegla_ubic_prio_producto.Producto = clsLnProducto.GetSingle(vBeRegla_ubic_prio_producto.IdProducto, pCamposCargar)

                End If

                lReturnList.Add(vBeRegla_ubic_prio_producto)

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

End Class