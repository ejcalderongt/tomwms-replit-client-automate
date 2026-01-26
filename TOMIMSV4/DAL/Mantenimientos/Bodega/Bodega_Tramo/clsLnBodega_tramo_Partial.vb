Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors

Partial Public Class clsLnBodega_tramo
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdTramo As Integer, ByVal pIdBodega As Integer) As clsBeBodega_tramo

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM bodega_Tramo WHERE IdTramo=@IdTramo and IdBodega=@IdBodega "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeBodega_tramo

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)

                        Obj = New clsBeBodega_tramo()

                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetSingle: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingleByInventario(ByVal pIdInventario As Integer,
                                                 ByVal pIdTramo As Integer,
                                                 ByVal pIdBodega As Integer) As clsBeBodega_tramo

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.sistema, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, 
                                        bodega_tramo.fec_mod, bodega_tramo.activo, bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, 
                                        bodega_tramo.margen_superior, bodega_tramo.margen_inferior, bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, 
                                        bodega_tramo.IdFontEnc
                                        FROM bodega_ubicacion INNER JOIN
                                        bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdArea = bodega_tramo.IdArea AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega AND 
                                        bodega_ubicacion.IdSector = bodega_tramo.IdSector INNER JOIN
                                        trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion AND bodega_ubicacion.IdBodega = trans_inv_ciclico_ubic.IdBodega
                                        WHERE 
                                        trans_inv_ciclico_ubic.idinventarioenc=@IdInventario 
                                        AND bodega_ubicacion.IdTramo=@IdTramo
                                        AND bodega_ubicacion.IdBodega = @IdBodega
                                        GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, 
                                        bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                                        bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, bodega_tramo.sistema, bodega_tramo.activo "


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeBodega_tramo

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBeBodega_tramo()
                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetSingle: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdSector_And_IdBodega(ByVal pActivo As Boolean,
                                                            ByVal pIdSector As Integer,
                                                            ByVal pIdBodega As Integer) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdSector=@IdSector AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeBodega_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeBodega_tramo()
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_Sector_And_IdBodega(ByVal pIdSector As Integer, ByVal pIdBodega As Integer) As DataTable

        Get_All_Tramos_By_Sector_And_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdSector=@IdSector And IdBodega=@IdBodega order by descripcion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Get_All_Tramos_By_Sector_And_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_Sector_And_IdBodega(ByVal pIdSector As Integer,
                                                                 ByVal pIdBodega As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Tramos_By_Sector_And_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdSector=@IdSector And IdBodega=@IdBodega order by descripcion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Get_All_Tramos_By_Sector_And_IdBodega = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_IdSector_And_IdInventario(ByVal pIdSector As Integer,
                                                                       ByVal IdInventario As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = " SELECT DISTINCT bodega_tramo.IdTramo, 
                                    bodega_tramo.IdSector, 
                                    bodega_tramo.sistema, 
                                    bodega_tramo.descripcion 
                                    FROM bodega_ubicacion INNER JOIN
                                    trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion 
                                    AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_stock.IdBodega
                                    INNER JOIN
                                    bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega AND 
                                    bodega_ubicacion.IdArea = bodega_tramo.IdArea"


            vSQL += " WHERE bodega_tramo.IdSector=@IdSector 
                      AND trans_inv_stock.idinventario = @IdInventario 
                      AND trans_inv_stock.IdBodega = @IdBodega 
                         GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, 
                            bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                            bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, bodega_tramo.IdTipoRack, bodega_tramo.sistema, 
                            bodega_tramo.activo, bodega_tramo.es_rack,bodega_ubicacion.nivel,bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion,bodega_ubicacion.indice_x "

            vSQL += " ORDER BY bodega_tramo.IdSector,  bodega_tramo.IdTramo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_IdSector_And_IdInventario(ByVal pIdSector As Integer,
                                                                       ByVal IdInventario As Integer,
                                                                       ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = " SELECT DISTINCT bodega_tramo.IdTramo, 
                                    bodega_tramo.IdSector, 
                                    bodega_tramo.sistema, 
                                    bodega_tramo.descripcion 
                                    FROM bodega_ubicacion INNER JOIN
                                    trans_inv_stock ON bodega_ubicacion.IdUbicacion = trans_inv_stock.IdUbicacion 
                                    AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_stock.IdBodega
                                    INNER JOIN
                                    bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo AND bodega_ubicacion.IdSector = bodega_tramo.IdSector AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega AND 
                                    bodega_ubicacion.IdArea = bodega_tramo.IdArea"


            vSQL += " WHERE bodega_tramo.IdSector=@IdSector 
                      AND trans_inv_stock.idinventario = @IdInventario 
                      AND trans_inv_stock.IdBodega = @IdBodega 
                         GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, 
                            bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                            bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, bodega_tramo.IdTipoRack, bodega_tramo.sistema, 
                            bodega_tramo.activo, bodega_tramo.es_rack,bodega_ubicacion.nivel,bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion,bodega_ubicacion.indice_x "

            vSQL += " ORDER BY bodega_tramo.IdSector,  bodega_tramo.IdTramo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdSector_For_Inventario(ByVal pIdSector As Integer, ByVal pIdInventario As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdSector=@IdSector"

                Dim vSQL As String = "SELECT trans_inv_ciclico_ubic.idinventarioenc, bodega_tramo.IdTramo,bodega_tramo.IdSector, bodega_tramo.sistema, dbo.bodega_tramo.descripcion, 
                         bodega_tramo.user_agr, bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, bodega_tramo.activo, 
                         bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, 
                         bodega_tramo.margen_superior, bodega_tramo.margen_inferior, bodega_tramo.Codigo,bodega_tramo.Indice_x, bodega_tramo.Orientacion, 
                         bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc
                        FROM bodega_ubicacion INNER JOIN
                         trans_inv_ciclico_ubic ON bodega_ubicacion.IdUbicacion = trans_inv_ciclico_ubic.idubicacion INNER JOIN
                         bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo
                       WHERE bodega_tramo.IdSector=@IdSector AND trans_inv_ciclico_ubic.idinventarioenc = @IdInventario
                       GROUP BY trans_inv_ciclico_ubic.idinventarioenc, bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, 
                         bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, 
                         bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                         bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, 
                         bodega_tramo.sistema, bodega_tramo.activo"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllTramosBySectorForCombo(ByVal pIdSector As Integer, ByVal pIdBodega As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1625pm: Quité String.Format.
                Dim vSQL As String = "SELECT IdTramo,descripcion as Nombre FROM bodega_tramo WHERE IdSector=@IdSector AND IdBodega=@IdBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllTramosForCombo() As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdTramo,descripcion as Nombre FROM bodega_tramo"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_Con_Volumen_Ocupado(ByVal pIdProductoBodega As Integer,
                                                              ByVal pLote As String,
                                                              ByVal pIdEstado As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Dim lStock As New List(Of clsBeVW_stock_res)
        Dim lTramos As New List(Of clsBeBodega_tramo)

        Get_All_Tramos_Con_Volumen_Ocupado = Nothing

        Try

            lStock = clsLnStock.Get_All_Stock_ConTramo(pIdProductoBodega,
                                                       pLote,
                                                       pIdEstado,
                                                       lConnection,
                                                       lTransaction)

            If Not lStock Is Nothing Then

                If lStock.Count > 0 Then

                    Dim BeTramo As New clsBeBodega_tramo
                    'Dim BeProducto As New clsBeProducto
                    'Dim pCampos(3) As clsBeProducto.ProdPropiedades
                    'pCampos(0) = clsBeProducto.ProdPropiedades.Alto
                    'pCampos(1) = clsBeProducto.ProdPropiedades.Largo
                    'pCampos(2) = clsBeProducto.ProdPropiedades.Ancho

                    Dim vContador As Integer = 1

                    For Each ProdInStock As clsBeVW_stock_res In lStock

                        'BeProducto.IdProducto = ProdInStock.IdProducto
                        'BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, pCampos)

                        BeTramo = New clsBeBodega_tramo
                        BeTramo.IdTramo = ProdInStock.IdTramo
                        BeTramo.IdBodega = ProdInStock.IdBodega
                        GetSingle(BeTramo, lConnection, lTransaction)

                        '#EJC2017091018_REVISIÓN_CAMBIO_CANTIDAD_PRES_OK
                        If ProdInStock.IdPresentacion <> 0 Then
                            'BeTramo.VolumenUtilizado = (ProdInStock.CantidadPresentacion) * ProdInStock.AltoUbicacion * ProdInStock.LargoUbicacion * ProdInStock.AnchoUbicacion
                            BeTramo.VolumenUtilizado = ProdInStock.VolumenPresEnUbicacion
                        Else
                            'BeTramo.VolumenUtilizado = ProdInStock.CantidadUmBas * BeProducto.Alto * BeProducto.Largo * BeProducto.Ancho
                            BeTramo.VolumenUtilizado = ProdInStock.VolumenUmBasEnUbicacion
                        End If

                        BeTramo.Tag = vContador

                        lTramos.Add(BeTramo)

                        vContador += 1

                    Next

                    Return lTramos

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_Con_Cantidades_Ocupadas(ByVal pIdProductoBodega As Integer,
                                                                  ByVal pLote As String,
                                                                  ByVal pIdEstado As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Dim lStock As New List(Of clsBeVW_stock_res)
        Dim lTramos As New List(Of clsBeBodega_tramo)

        Get_All_Tramos_Con_Cantidades_Ocupadas = Nothing

        Try

            lStock = clsLnStock.Get_All_Stock_ConTramo(pIdProductoBodega, pLote, pIdEstado, lConnection, lTransaction)

            If Not lStock Is Nothing Then

                If lStock.Count > 0 Then

                    Dim BeTramo As New clsBeBodega_tramo
                    Dim BeProducto As New clsBeProducto
                    Dim pCampos(4) As clsBeProducto.ProdPropiedades
                    pCampos(0) = clsBeProducto.ProdPropiedades.Alto
                    pCampos(1) = clsBeProducto.ProdPropiedades.Largo
                    pCampos(2) = clsBeProducto.ProdPropiedades.Ancho
                    pCampos(3) = clsBeProducto.ProdPropiedades.Codigo

                    Dim vContador As Integer = 1

                    For Each ProdInStock As clsBeVW_stock_res In lStock

                        BeProducto.IdProducto = ProdInStock.IdProducto
                        BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, pCampos, lConnection, lTransaction)
                        BeTramo = New clsBeBodega_tramo
                        BeTramo.IdTramo = ProdInStock.IdTramo
                        BeTramo.IdBodega = ProdInStock.IdBodega
                        GetSingle(BeTramo, lConnection, lTransaction)
                        BeTramo.CantidadUtilizada = ProdInStock.CantidadUmBas
                        BeTramo.Tag = vContador
                        lTramos.Add(BeTramo)
                        vContador += 1

                    Next

                    Return lTramos

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_By_ProductoPredeterminado(ByVal pIdProductoBodega As Integer,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdTipoProductoDefault =@IdTipoProductoDefault"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoProductoDefault", pIdProductoBodega)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_tramo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_tramo()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllTramosSinProductoPredeterminado() As List(Of clsBeBodega_tramo)

        Try
            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Dim vSQL As String = String.Format("SELECT * FROM bodega_tramo WHERE IdTipoProductoDefault <>{0}", 0)
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_tramo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_tramo()

                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using
            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_Area(ByVal pActivo As Boolean,
                                               ByVal pIdBodega As Integer,
                                               ByVal pIdArea As Integer) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT bodega_tramo.* " &
                                     " FROM bodega_tramo " &
                                     " WHERE bodega_tramo.IdBodega = @IdBodega AND
                                             bodega_tramo.IdArea = @IdArea "

                If pActivo Then
                    vSQL += " AND bodega_tramo.Activo=1"
                Else
                    vSQL += " AND bodega_tramo.Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_tramo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_tramo()
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean,
                                               ByVal pIdBodega As Integer) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT bodega_tramo.*, bodega_area.IdBodega " &
                " FROM bodega_tramo INNER JOIN " &
                " bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN " &
                " bodega_area ON bodega_sector.IdArea = bodega_area.IdArea and bodega_sector.IdBodega = bodega_area.IdBodega " &
                " WHERE bodega_area.IdBodega = @IdBodega "

                If pActivo Then
                    vSQL += " AND bodega_tramo.Activo=1"
                Else
                    vSQL += " AND bodega_tramo.Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_tramo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_tramo()
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim vSQL As String = "SELECT bodega_tramo.*, bodega_area.IdBodega " &
                " FROM bodega_tramo INNER JOIN " &
                " bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN " &
                " bodega_area ON bodega_sector.IdArea = bodega_area.IdArea and bodega_sector.IdBodega = bodega_area.IdBodega " &
                " WHERE bodega_area.IdBodega = @IdBodega "
            vSQL += " AND bodega_tramo.Activo=1"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_tramo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_tramo()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdSector(ByVal pIdBodega As Integer,
                                                           ByVal pIdSector As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Dim vSQL As String = "SELECT bodega_tramo.*, bodega_area.IdBodega
                                  FROM bodega_tramo INNER JOIN 
                                       bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and 
                                                        bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN 
                                       bodega_area ON bodega_tramo.IdArea = bodega_area.IdArea and 
                                                      bodega_tramo.IdBodega = bodega_area.IdBodega 
                                  WHERE bodega_tramo.IdBodega = @IdBodega AND bodega_tramo.IdSector = @IdSector "
            vSQL += " AND bodega_tramo.Activo=1"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_tramo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_tramo()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Existe_By_IdBodega_And_IdSector(ByVal pIdBodega As Integer,
                                                           ByVal pIdSector As Integer,
                                                           ByVal pInicia As String,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Boolean

        Dim lReturn As Boolean = False

        Try

            Dim vSQL As String = "SELECT bodega_tramo.IdTramo
                                  FROM bodega_tramo 
                                  WHERE bodega_tramo.IdBodega = @IdBodega AND
                                        bodega_tramo.IdSector = @IdSector AND 
                                        bodega_tramo.Activo=1 AND
                                        bodega_tramo.descripcion LIKE @inicia + '%'"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdSector", pIdSector)
                lDTA.SelectCommand.Parameters.AddWithValue("@inicia", pInicia)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                lReturn = lDataTable.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByTramoBodega: " & ex.Message)
        End Try

        Return lReturn

    End Function


    Public Shared Function GetSingle(ByRef pBeBodega_tramo As clsBeBodega_tramo,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Bodega_tramo" &
            " Where(IdTramo = @IDTRAMO AND IdBodega=@IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Debug.Print("GetSingle_Tramo: " & pBeBodega_tramo.IdTramo)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pBeBodega_tramo.IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pBeBodega_tramo.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_tramo, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Seleccion(ByVal pIdBodega As Integer) As List(Of clsBeBodega_Tramo_Seleccion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_Tramo_Seleccion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdTramo,IdSector,descripcion As Tramo FROM bodega_tramo WHERE activo=1 and IdBodega=@IdBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_Tramo_Seleccion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_Tramo_Seleccion()

                            Obj.IdTramo = lRow.Item("IdTramo")
                            Obj.IdSector = IIf(IsDBNull(lRow.Item("IdSector")), "", lRow.Item("IdSector"))
                            Obj.Tramo = IIf(IsDBNull(lRow.Item("Tramo")), "", lRow.Item("Tramo"))
                            Obj.Seleccionar = False
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_For_Seleccion_By_IdInventarioEnc(ByVal pIdInventario As Integer) As List(Of clsBeBodega_Tramo_Seleccion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_Tramo_Seleccion)

            Dim vSQL As String = "SELECT bodega_tramo.descripcion AS Tramo, trans_inv_tramo.idtramo, 
                                  trans_inv_tramo.idinventario, bodega_tramo.IdSector
                                  FROM bodega_tramo INNER JOIN
                                  trans_inv_tramo ON bodega_tramo.IdTramo = trans_inv_tramo.idtramo INNER JOIN
                                  trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                                  bodega_tramo.IdBodega = trans_inv_enc.idbodega
                                  WHERE trans_inv_tramo.idinventario=@idinventario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_Tramo_Seleccion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_Tramo_Seleccion()

                            Obj.IdTramo = lRow.Item("idtramo")
                            Obj.IdSector = IIf(IsDBNull(lRow.Item("IdSector")), "", lRow.Item("IdSector"))
                            Obj.Tramo = IIf(IsDBNull(lRow.Item("Tramo")), "", lRow.Item("Tramo"))
                            Obj.Seleccionar = True
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Bodega(ByRef idbodega As Integer,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "DELETE FROM dbo.bodega_tramo WHERE dbo.bodega_tramo.Idtramo IN  
                               (SELECT dbo.bodega_tramo.IdTramo
                               FROM dbo.bodega_tramo INNER JOIN
                               dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector and bodega_tramo.idbodega = bodega_sector.idbodega INNER JOIN
                               dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea
                               WHERE (dbo.bodega_area.IdBodega = @idbodega)) AND bodega_tramo.IdBodega=@idbodega"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idbodega", idbodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Eliminar_Bodega_Tramo_By_IdBodega(ByVal idbodega As Integer,
                                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "DELETE FROM bodega_tramo WHERE bodega_tramo.Idtramo IN  
                               (SELECT bodega_tramo.IdTramo
                               FROM bodega_tramo INNER JOIN
                               bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector 
                               AND bodega_tramo.idbodega = bodega_sector.idbodega INNER JOIN
                               bodega_area ON bodega_sector.IdArea = bodega_area.IdArea
                               WHERE (bodega_area.IdBodega = @idbodega)) AND bodega_tramo.IdBodega=@idbodega"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idbodega", idbodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Async Function Eliminar_Bodega_Tramo_Async(ByVal idbodega As Integer,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Task(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "DELETE FROM bodega_tramo WHERE dbo.bodega_tramo.Idtramo IN  
                               (SELECT dbo.bodega_tramo.IdTramo
                               FROM dbo.bodega_tramo INNER JOIN
                               bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector 
                               AND bodega_tramo.idbodega = bodega_sector.idbodega INNER JOIN
                               bodega_area ON bodega_sector.IdArea = bodega_area.IdArea
                               WHERE (bodega_area.IdBodega = @idbodega)) AND bodega_tramo.IdBodega=@idbodega"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idbodega", idbodega))


            Dim rowsAffected As Task(Of Integer) = cmd.ExecuteNonQueryAsync()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Await rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetAllByInventario(ByVal pIdInventario As Integer) As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.sistema, bodega_tramo.descripcion, bodega_tramo.user_agr, 
                               bodega_tramo.fec_agr, bodega_tramo.user_mod, bodega_tramo.fec_mod, bodega_tramo.activo, bodega_tramo.alto, bodega_tramo.largo, 
                               bodega_tramo.ancho, bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, 
                               bodega_tramo.margen_inferior, bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, 
                               bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc,bodega_tramo.IdTipoRack,bodega_tramo.es_rack
                          FROM trans_inv_ciclico_ubic INNER JOIN
                               bodega_ubicacion ON trans_inv_ciclico_ubic.idubicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                               bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo
                         WHERE trans_inv_ciclico_ubic.idinventarioenc=@IdInventario
                      GROUP BY bodega_tramo.IdTramo, bodega_tramo.IdSector, bodega_tramo.descripcion, bodega_tramo.user_agr, bodega_tramo.fec_agr, 
                               bodega_tramo.user_mod, bodega_tramo.fec_mod, bodega_tramo.alto, bodega_tramo.largo, bodega_tramo.ancho, 
                               bodega_tramo.margen_izquierdo, bodega_tramo.margen_derecho, bodega_tramo.margen_superior, bodega_tramo.margen_inferior, 
                               bodega_tramo.Codigo, bodega_tramo.Indice_x, bodega_tramo.Orientacion, bodega_tramo.IdTipoProductoDefault, bodega_tramo.IdFontEnc, 
                               bodega_tramo.sistema, bodega_tramo.activo,bodega_tramo.IdTipoRack,bodega_tramo.es_rack "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_tramo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_tramo()
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("BodegaTramo_GetAllByInventario: " & ex.Message)
        End Try

    End Function

    Public Shared Function NivelGrafico(idtramo As Integer, IdBodega As Integer) As Integer

        Try
            Dim lNivel As Integer = 1
            Dim sp As String = "SELECT TOP(1) MIN(1) As Min
                                 FROM dbo.bodega_ubicacion
                                 WHERE (idtramo = " & idtramo & " AND Idbodega= " & IdBodega & " )
                                 Group By indice_x Order By Min DESC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lNivel = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lNivel

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar(ByVal IdBodega As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_tramo WHERE IdBodega = @IdBodega order by IdTramo, idsector, descripcion "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
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

    Public Shared Function Obtener(ByRef oBeBodega_tramo As clsBeBodega_tramo,
                                    ByRef lTransaction As SqlTransaction,
                                    ByRef lConnection As SqlConnection) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Bodega_tramo " &
            " Where(IdTramo = @IdTramo AND IdBodega=@IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_tramo.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_tramo, dt.Rows(0))
                Obtener = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdBodega As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 1

            Dim sp As String = "SELECT ISNULL(Max(IdTramo),0) FROM Bodega_tramo
                                WHERE IdBodega = @IdBodega "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

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

    Public Shared Async Function MaxID_Async(ByVal pIdBodega As Integer,
                                       ByVal lConnection As SqlConnection,
                                       ByVal lTransaction As SqlTransaction) As Task(Of Integer)

        Try

            Dim lMax As Task(Of Integer) = Nothing
            Dim NoAsyncValue As Integer = 0

            Dim sp As String = "SELECT ISNULL(Max(IdTramo),0) FROM Bodega_tramo
                                WHERE IdBodega = @IdBodega "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalarAsync()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    NoAsyncValue = Await lReturnValue + 1
                End If

            End Using

            Return NoAsyncValue

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ubicaciones_Vacias_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                                          ByVal pIdTramo As Integer,
                                                                          ByVal lConnection As SqlConnection,
                                                                          ByVal lTransaction As SqlTransaction) As Integer
        Get_Ubicaciones_Vacias_By_IdBodega_And_IdTramo = 0

        Try

            Dim sp As String = "EXEC Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega @IdBodega, @IdTramo"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                Dim vCantPosVacias As Integer = 0

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vCantPosVacias = CInt(lReturnValue)
                    Return vCantPosVacias
                End If

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Maximo_Tramo(ByVal pDescripcion As String,
                                           ByVal pIdBodega As String,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction) As Integer
        Dim lReturn As String = 0

        Try

            Dim pInicia As String = pDescripcion.Substring(0, 1)

            Const sp As String = "SELECT ISNULL(MAX(SUBSTRING(REPLACE(DESCRIPCION,'-',''),2,LEN(DESCRIPCION))),0) AS NUEVA_DESCRIPCION
                                  FROM bodega_tramo WHERE IdBodega  = @IdBodega AND descripcion LIKE @inicia + '%' 
                                        AND sistema =0 AND LEN(DESCRIPCION)>=2"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@inicia", pInicia)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lReturn = lReturnValue
                Else
                    lReturn = pDescripcion
                End If

            End Using

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

        Return lReturn

    End Function

    Public Shared Function Get_Maximo_Tramo_Sector(ByVal pDescripcion As String,
                                                   ByVal pIdSector As Integer,
                                                   ByVal pIdBodega As String,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As Integer
        Dim lReturn As String = 0

        Try

            Dim pInicia As String = pDescripcion.Substring(0, 1)

            Const sp As String = "SELECT ISNULL(MAX(REPLACE(SUBSTRING(DESCRIPCION,2,LEN(descripcion)-3),'-','')),0)   AS NUEVA_DESCRIPCION
                                  FROM bodega_tramo WHERE IdBodega  = @IdBodega AND descripcion LIKE @inicia + '%' AND IdSector  = @IdSector"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@inicia", pInicia)
                lCommand.Parameters.AddWithValue("@IdSector", pIdSector)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lReturn = lReturnValue
                Else
                    lReturn = pDescripcion
                End If

            End Using

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

        Return lReturn

    End Function

    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from bodega_tramo where IdBodega = @IdBodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                Dim rowsAffected As Integer = lCommand.ExecuteNonQuery()
                lCommand.Dispose()

                Return rowsAffected
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT27062022_1030: para importación reabasto
    Public Shared Function GetSingle_by_Descripcion(ByVal pDescripcion As String, ByVal pIdBodega As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeBodega_tramo

        GetSingle_by_Descripcion = Nothing

        Try

            Dim vSQL As String = " SELECT * FROM bodega_Tramo WHERE descripcion=@pDescripcion and IdBodega=@IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, pConection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@pDescripcion", pDescripcion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Dim Obj As clsBeBodega_tramo
                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Obj = New clsBeBodega_tramo()
                    Cargar(Obj, lRow)
                    GetSingle_by_Descripcion = Obj
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Seleccion(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_Tramo_Seleccion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_Tramo_Seleccion)

            Dim vSQL As String = "SELECT IdTramo,IdSector,descripcion As Tramo FROM bodega_tramo WHERE activo=1 and IdBodega=@IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_Tramo_Seleccion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_Tramo_Seleccion()

                        Obj.IdTramo = lRow.Item("IdTramo")
                        Obj.IdSector = IIf(IsDBNull(lRow.Item("IdSector")), "", lRow.Item("IdSector"))
                        Obj.Tramo = IIf(IsDBNull(lRow.Item("Tramo")), "", lRow.Item("Tramo"))
                        Obj.Seleccionar = False
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Tramos_For_Seleccion_By_IdInventarioEnc(ByVal pIdInventario As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_Tramo_Seleccion)

        Try

            Dim lReturnList As New List(Of clsBeBodega_Tramo_Seleccion)

            Dim vSQL As String = "SELECT bodega_tramo.descripcion AS Tramo, trans_inv_tramo.idtramo, 
                                  trans_inv_tramo.idinventario, bodega_tramo.IdSector
                                  FROM bodega_tramo INNER JOIN
                                  trans_inv_tramo ON bodega_tramo.IdTramo = trans_inv_tramo.idtramo INNER JOIN
                                  trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                                  bodega_tramo.IdBodega = trans_inv_enc.idbodega
                                  WHERE trans_inv_tramo.idinventario=@idinventario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeBodega_Tramo_Seleccion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_Tramo_Seleccion()
                        Obj.IdTramo = lRow.Item("idtramo")
                        Obj.IdSector = IIf(IsDBNull(lRow.Item("IdSector")), "", lRow.Item("IdSector"))
                        Obj.Tramo = IIf(IsDBNull(lRow.Item("Tramo")), "", lRow.Item("Tramo"))
                        Obj.Seleccionar = True
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT16012025: listar tramos para el portal web cealsa
    Public Shared Function Get_All_Tramos_By_IdTramo(ByVal pIdTramo As Integer,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeBodega_tramo)

        Get_All_Tramos_By_IdTramo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM bodega_tramo WHERE IdTramo =@pIdTramo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdTramo", pIdTramo)

                Dim lDataTable As New DataTable

                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_All_Tramos_By_IdTramo = New List(Of clsBeBodega_tramo)()
                    Dim Obj As clsBeBodega_tramo

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeBodega_tramo()
                        Cargar(Obj, lRow)
                        Get_All_Tramos_By_IdTramo.Add(Obj)
                    Next

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
