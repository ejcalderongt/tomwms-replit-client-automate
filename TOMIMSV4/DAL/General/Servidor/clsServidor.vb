Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsServidor

    Public Shared Function FechaHoraServidor() As Integer

        Try

            Dim lFechaHora As Integer = String.Empty

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(("SELECT GETDATE() AS FechaHora"), lConnection)
                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lFechaHora = lReturnValue.ToString
                    End If

                End Using

            End Using

            Return lFechaHora

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_Fecha_Servidor() As Date

        Try

            Dim lFecha As Date = New Date(1900, 1, 1)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(("SELECT GETDATE()"), lConnection)

                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lFecha = Date.Parse(lReturnValue)
                    End If

                End Using

            End Using

            Return lFecha

        Catch ex As Exception
            Throw New Exception(String.Format("Aquí: {0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function


    ''' <summary>
    ''' Creada por Ricardo Garcìa
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function HoraServivdor() As Integer

        Try

            Dim lHora As Integer = String.Empty

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(("SELECT CONVERT(VARCHAR(8), GETDATE(), 108) 'Hora'"), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lHora = lReturnValue.ToString
                    End If

                End Using

            End Using

            Return lHora

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' Creada por Ricardo Garcìa
    ''' </summary>
    ''' <param name="pNombreBaseDatos"></param>
    ''' <returns></returns>
    Public Shared Function ExisteBaseDatos(ByVal pNombreBaseDatos As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM dbo.sysdatabases WHERE NAME=@NAME"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@NAME", pNombreBaseDatos)

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Inicializa_BD_By_IdBodega(ByVal pIdBodega As Integer) As Boolean

        Inicializa_BD_By_IdBodega = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = ""

            sp = "DELETE a
                  FROM transacciones_log a
                  INNER JOIN producto_bodega b
                  ON a.IdProductoBodega = b.IdProductoBodega
                  WHERE b.IdBodega = @IdBodega"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE a
                FROM trans_ubic_hh_op a
                INNER JOIN operador_bodega b
                  ON a.IdOperadorBodega = b.IdOperadorBodega
                WHERE b.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "Delete from trans_ubic_hh_det where idtareaubicacionenc in (
                    SELECT IdTareaUbicacionEnc
                    FROM trans_ubic_hh_enc a
                    INNER JOIN propietario_bodega b
                      ON a.IdPropietarioBodega = b.IdPropietarioBodega
                      WHERE b.IdBodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE a
                  FROM trans_ubic_hh_stock a
                  INNER JOIN producto_bodega b
                  ON a.IdProductoBodega = b.IdProductoBodega
                  WHERE b.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE a
                    FROM trans_ubic_hh_enc a
                    INNER JOIN propietario_bodega b
                      ON a.IdPropietarioBodega = b.IdPropietarioBodega
                    WHERE b.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE a
                  FROM trans_re_op a
                  INNER JOIN trans_re_enc b
                  ON a.IdRecepcionEnc = b.IdRecepcionEnc
                  INNER JOIN propietario_bodega c 
                  ON b.IdPropietarioBodega = c.IdPropietario
                  WHERE c.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_det_parametros 
                  WHERE IdRecepcionEnc in (
                            select idrecepcionenc
                            FROM trans_re_enc a inner join propietario_bodega b
                            on a.idpropietariobodega = b.idpropietariobodega 
                            and b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_op 
                  WHERE IdRecepcionEnc in (
	                SELECT idrecepcionenc 
                    FROM trans_re_enc a INNER JOIN propietario_bodega b
	                on a.idpropietariobodega = b.idpropietariobodega 
	                and b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_oc WHERE IdRecepcionEnc IN (
	                SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
	                ON a.idpropietariobodega = b.idpropietariobodega 
	                AND b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_img WHERE IdRecepcionEnc IN (
	                SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
	                ON a.idpropietariobodega = b.idpropietariobodega 
	                AND b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_fact WHERE IdRecepcionEnc IN (
	                SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
	                ON a.idpropietariobodega = b.idpropietariobodega 
	                AND b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE a
                    FROM producto_pallet a
                    INNER JOIN producto_bodega b
                    ON a.IdProductoBodega = b.IdProductoBodega
                    WHERE b.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            sp = "DELETE FROM trans_re_det_parametros WHERE IdRecepcionEnc IN (
	                SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
	                ON a.idpropietariobodega = b.idpropietariobodega 
	                AND b.idbodega = @IdBodega)"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()


            sp = "DELETE a
            FROM trans_re_enc a
            INNER JOIN propietario_bodega b
            ON a.IdPropietarioBodega = b.IdPropietarioBodega
            WHERE b.IdBodega = @IdBodega"

            cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            rowsAffected = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Get_Fecha_Servidor(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Date

        Try

            Dim lFecha As Date = New Date(1900, 1, 1)
            Dim vSQL As String = "SELECT GETDATE()"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lFecha = Date.Parse(lReturnValue)
                End If

            End Using

            Return lFecha

        Catch ex As Exception
            Throw New Exception(String.Format("Aquí: {0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

End Class
