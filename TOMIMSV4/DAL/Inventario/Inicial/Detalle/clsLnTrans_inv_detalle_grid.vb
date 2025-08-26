Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_detalle_grid

    Public Shared Sub Cargar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_detalle_grid, ByRef dr As DataRow)

        Try
            With oBeTrans_inv_resumen
                .Idinventariodet = IIf(IsDBNull(dr.Item("idinventariodet")), 0, dr.Item("idinventariodet"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .IdUbic = IIf(IsDBNull(dr.Item("Idubic")), 0, dr.Item("Idubic"))
                .Ubic = IIf(IsDBNull(dr.Item("Ubic")), 0, dr.Item("Ubic"))
                .Idproducto = IIf(IsDBNull(dr.Item("idproducto")), 0, dr.Item("idproducto"))
                .UnidadMedida = IIf(IsDBNull(dr.Item("unimed")), "", dr.Item("unimed"))
                .presentacion = IIf(IsDBNull(dr.Item("presentacion")), " ", dr.Item("presentacion"))
                .productoestado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_detalle_grid)

        GetAll = Nothing


        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle_grid)

            Const sp As String = "SELECT  trans_inv_detalle.idinventariodet, trans_inv_detalle.idinventarioenc, trans_inv_detalle.idtramo, trans_inv_detalle.IdUbicacion AS idubic, bodega_ubicacion.descripcion AS ubic, 
                                trans_inv_detalle.idproducto, producto_estado.nombre AS estado, producto_presentacion.nombre AS presentacion, trans_inv_detalle.cantidad, unidad_medida.Nombre AS unimed, trans_inv_detalle.peso
                                FROM trans_inv_detalle INNER JOIN
                                producto_estado ON trans_inv_detalle.idproductoestado = producto_estado.IdEstado INNER JOIN
                                unidad_medida ON trans_inv_detalle.idunidadmedida = unidad_medida.IdUnidadMedida INNER JOIN
                                bodega_ubicacion ON trans_inv_detalle.IdUbicacion = bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                                producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                AND trans_inv_detalle.idproducto = producto_presentacion.IdProducto "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_detalle_grid

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_detalle_grid
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_detalle_grid)

        Get_All_By_IdInventarioEnc = Nothing


        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle_grid)

            Const sp As String = "SELECT  trans_inv_detalle.idinventariodet, trans_inv_detalle.idinventarioenc, trans_inv_detalle.idtramo, trans_inv_detalle.IdUbicacion AS idubic, bodega_ubicacion.descripcion AS ubic, 
                                trans_inv_detalle.idproducto, producto_estado.nombre AS estado, producto_presentacion.nombre AS presentacion, trans_inv_detalle.cantidad, unidad_medida.Nombre AS unimed, trans_inv_detalle.peso
                                FROM trans_inv_detalle INNER JOIN
                                producto_estado ON trans_inv_detalle.idproductoestado = producto_estado.IdEstado INNER JOIN
                                unidad_medida ON trans_inv_detalle.idunidadmedida = unidad_medida.IdUnidadMedida INNER JOIN
								trans_inv_enc on trans_inv_detalle.idinventarioenc = trans_inv_enc.idinventarioenc INNER JOIN
                                bodega_ubicacion ON trans_inv_detalle.IdUbicacion = bodega_ubicacion.IdUbicacion and trans_inv_enc.idbodega = bodega_ubicacion.IdBodega LEFT OUTER JOIN
                                producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                AND trans_inv_detalle.idproducto = producto_presentacion.IdProducto 
                                WHERE trans_inv_detalle.idinventarioenc = @idinventarioenc"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInventarioEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_detalle_grid

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_detalle_grid
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_detalle_grid)

        GetAll = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle_grid)

            Const sp As String = "SELECT  trans_inv_detalle.idinventariodet, trans_inv_detalle.idinventarioenc, trans_inv_detalle.idtramo, trans_inv_detalle.IdUbicacion AS idubic, bodega_ubicacion.descripcion AS ubic, 
                                    trans_inv_detalle.idproducto, producto_estado.nombre AS estado, producto_presentacion.nombre AS presentacion, trans_inv_detalle.cantidad, unidad_medida.Nombre AS unimed, trans_inv_detalle.peso
                                    FROM trans_inv_detalle INNER JOIN
                                    producto_estado ON trans_inv_detalle.idproductoestado = producto_estado.IdEstado INNER JOIN
                                    unidad_medida ON trans_inv_detalle.idunidadmedida = unidad_medida.IdUnidadMedida INNER JOIN
                                    bodega_ubicacion ON trans_inv_detalle.IdUbicacion = bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion AND trans_inv_detalle.idproducto = producto_presentacion.IdProducto "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_detalle_grid

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_detalle_grid
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll(ByVal pIdinventarioenc As Integer,
                                  ByVal pIdtramo As Integer,
                                  ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle_grid)

        GetAll = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle_grid)

            lReturnList = GetAll()

            If Not lReturnList Is Nothing Then

                lReturnList = lReturnList.FindAll(Function(x) x.Idinventarioenc = pIdinventarioenc).OrderBy(Function(y) y.Ubic).ThenBy(Function(z) z.productoestado).ThenBy(Function(zz) zz.presentacion).ToList()

                If (pIdtramo <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idtramo = pIdtramo).ToList()
                If (pIdProducto <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idproducto = pIdProducto).ToList()

                Return lReturnList

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc(ByVal pIdinventarioenc As Integer,
                                      ByVal pIdtramo As Integer,
                                      ByVal pIdProducto As Integer,
                                      ByVal pIdUbic As Integer) As List(Of clsBeTrans_inv_detalle_grid)

        Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle_grid)
            Dim ubic As New clsBeBodega_ubicacion

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    lReturnList = Get_All_By_IdInventarioEnc(pIdinventarioenc, lConnection, lTransaction)

                    If Not lReturnList Is Nothing Then

                        lReturnList = lReturnList.OrderBy(Function(y) y.Ubic).ThenBy(Function(z) z.productoestado).ThenBy(Function(zz) zz.presentacion).ToList()

                        If (pIdUbic <> 0) Then
                            lReturnList = lReturnList.FindAll(Function(x) x.IdUbic = pIdUbic).ToList()
                        Else
                            If (pIdtramo <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idtramo = pIdtramo).ToList()
                        End If

                        If (pIdProducto <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idproducto = pIdProducto).ToList()

                        For Each dg As clsBeTrans_inv_detalle_grid In lReturnList
                            ubic.IdUbicacion = dg.IdUbic
                            clsLnBodega_ubicacion.GetSingle(ubic, lConnection, lTransaction)
                            dg.Ubic = ubic.NombreCompleto
                        Next

                        Return lReturnList

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
