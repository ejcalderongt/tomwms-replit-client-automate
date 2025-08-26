Imports System.Data.SqlClient

Partial Public Class clsLnPropietario_reglas_enc
    Implements IDisposable

    'Public Shared Sub CargarHH(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc, ByRef dr As DataRow)

    '    Try

    '        With oBePropietario_reglas_enc

    '            .IdReglaPropietarioEnc = IIf(IsDBNull(dr.Item("IdReglaPropietarioEnc")), 0, dr.Item("IdReglaPropietarioEnc"))
    '            .IdReglaRecepcion = IIf(IsDBNull(dr.Item("IdReglaRecepcion")), 0, dr.Item("IdReglaRecepcion"))
    '            .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
    '            .IdMensajeRegla = IIf(IsDBNull(dr.Item("IdMensajeRegla")), 0, dr.Item("IdMensajeRegla"))
    '            .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
    '            .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
    '            .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
    '            .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
    '            .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
    '            .ReglasDet = clsLnPropietario_reglas_det.Get_All_By_IdReglaPropietarioEnc(.IdReglaPropietarioEnc)
    '            .Regla.IdReglaRecepcion = .IdReglaRecepcion
    '            clsLnReglas_recepcion.GetSingle(.Regla)

    '        End With

    '    Catch ex As Exception
    '        Throw New Exception("Propietario_reglas_enc_Cargar: " & ex.Message)
    '    End Try

    'End Sub

    Public Shared Sub CargarHH(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc,
                               ByRef dr As DataRow,
                               ByRef lConnection As SqlConnection,
                               ByRef lTransaction As SqlTransaction)

        Try

            With oBePropietario_reglas_enc

                .IdReglaPropietarioEnc = IIf(IsDBNull(dr.Item("IdReglaPropietarioEnc")), 0, dr.Item("IdReglaPropietarioEnc"))
                .IdReglaRecepcion = IIf(IsDBNull(dr.Item("IdReglaRecepcion")), 0, dr.Item("IdReglaRecepcion"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdMensajeRegla = IIf(IsDBNull(dr.Item("IdMensajeRegla")), 0, dr.Item("IdMensajeRegla"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .ReglasDet = clsLnPropietario_reglas_det.Get_All_By_IdReglaPropietarioEnc(.IdReglaPropietarioEnc, lConnection, lTransaction)
                .Regla.IdReglaRecepcion = .IdReglaRecepcion
                clsLnReglas_recepcion.GetSingle(.Regla, lConnection, lTransaction)

            End With

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdReglaPropietarioEnc),0) FROM propietario_reglas_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdReglaPropietarioEnc As Integer) As clsBePropietario_reglas_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT enc.*,m.nombre AS Mensaje, r.Nombre AS Regla FROM Propietario_reglas_enc AS enc " _
                   & "INNER JOIN mensaje_regla AS m ON enc.IdMensajeRegla = m.IdMensajeRegla " _
                   & "INNER JOIN reglas_recepcion AS r ON enc.IdReglaRecepcion = r.IdReglaRecepcion WHERE IdReglaPropietarioEnc=@IdReglaPropietarioEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdReglaPropietarioEnc", pIdReglaPropietarioEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBePropietario_reglas_enc()
                            Cargar(Obj, lRow)
                            Obj.Regla.IdReglaRecepcion = Obj.IdReglaRecepcion
                            clsLnReglas_recepcion.GetSingle(Obj.Regla, lConnection, lTransaction)
                            Obj.Mensaje.IdMensajeRegla = Obj.IdMensajeRegla
                            clsLnMensaje_regla.GetSingle(Obj.Mensaje, lConnection, lTransaction)
                            Obj.IsNew = False
                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_reglas_enc)

        Dim Lista As New List(Of clsBePropietario_reglas_enc)

        Dim vSQL As String = "SELECT * FROM VW_Propietario_Regla_Recepcion 
                              WHERE IdPropietario=@IdPropietario AND activo =1 "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable()

                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim Obj As clsBePropietario_reglas_enc

                            For Each lRow As DataRow In lDT.Rows

                                Obj = New clsBePropietario_reglas_enc

                                Obj.IdReglaPropietarioEnc = CType(lRow("Código"), Integer)

                                If lRow("IdReglaRecepcion") IsNot DBNull.Value AndAlso lRow("IdReglaRecepcion") IsNot Nothing Then
                                    Obj.IdReglaRecepcion = CType(lRow("IdReglaRecepcion"), Integer)
                                    Obj.Regla.Nombre = CType(lRow("Regla"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Mensaje") IsNot DBNull.Value AndAlso lRow("Mensaje") IsNot Nothing Then
                                    Obj.Mensaje.Nombre = CType(lRow("Mensaje"), String)
                                End If

                                If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("Activo"), System.Boolean)
                                End If

                                Obj.IsNew = False

                                Lista.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Lista

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllHH(ByVal pIdPropietario As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBePropietario_reglas_enc)

        Dim Lista As New List(Of clsBePropietario_reglas_enc)

        Dim vSQL As String = "SELECT * FROM Propietario_reglas_enc WHERE IdPropietario=@IdPropietario AND Activo = 1"

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim Obj As clsBePropietario_reglas_enc

                    For Each lRow As DataRow In lDT.Rows

                        Obj = New clsBePropietario_reglas_enc

                        CargarHH(Obj, lRow, lConnection, lTransaction)

                        Lista.Add(Obj)

                    Next

                End If

            End Using

            Return Lista

        Catch ex As Exception
            Throw New Exception("PropietarioReglasEnc_GetAllHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As List(Of clsBePropietario_reglas_enc)

        Dim Lista As New List(Of clsBePropietario_reglas_enc)

        Try

            Dim vSQL As String = "SELECT * FROM Propietario_reglas_enc WHERE IdPropietario=@IdPropietario AND Activo = 1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim Obj As clsBePropietario_reglas_enc

                    For Each lRow As DataRow In lDT.Rows
                        Obj = New clsBePropietario_reglas_enc
                        CargarHH(Obj, lRow, lConnection, lTransaction)
                        Lista.Add(Obj)
                    Next

                End If

            End Using

            Return Lista

        Catch ex As Exception
            Throw New Exception("PropietarioReglasEnc_GetAllHH: " & ex.Message)
        End Try

    End Function

    Public Shared Sub Desactivar_Regla(ByVal pIdReglaPropietarioEnc As Integer)

        Try

            Dim vSQL As String = "UPDATE propietario_reglas_enc SET activo=0 WHERE IdReglaPropietarioEnc=@IdReglaPropietarioEnc"

            '#EJC202107141547_DR: Transacción agregada en desactivar_regla de propietario.
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdReglaPropietarioEnc", pIdReglaPropietarioEnc)
                        lCommand.ExecuteNonQuery()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function ExisteRegla(ByVal pIdReglaRecepcion As Integer, ByVal pIdPropietario As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM propietario_reglas_enc 
                                  WHERE IdReglaRecepcion=@IdReglaRecepcion 
                                  AND IdPropietario=@IdPropietario
                                  AND Activo =1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdReglaRecepcion", pIdReglaRecepcion)
                    lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

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


    Public Shared Sub Guarda(ByVal pObjRE As clsBePropietario_reglas_enc, ByVal pListObjR As List(Of clsBePropietario_reglas_det))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim ObjReglaENC As New clsLnPropietario_reglas_enc()
        Dim ObjReglaDET As New clsLnPropietario_reglas_det()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pObjRE.IsNew Then
                pObjRE.IdReglaPropietarioEnc = clsLnPropietario_reglas_enc.MaxID()
                ObjReglaENC.Insertar(pObjRE, lConnection, lTransaction)
            Else
                ObjReglaENC.Actualizar(pObjRE, lConnection, lTransaction)
            End If

            Dim lMaxReglaDet As Integer = clsLnPropietario_reglas_det.MaxID()
            For Each r As clsBePropietario_reglas_det In pListObjR
                If r.IsNew Then
                    lMaxReglaDet += 1
                    r.IdReglaPropietarioDet = lMaxReglaDet
                    r.IdReglaPropietarioEnc = pObjRE.IdReglaPropietarioEnc
                    ObjReglaDET.Insertar(r, lConnection, lTransaction)
                Else
                    ObjReglaDET.Actualizar(r, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

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
