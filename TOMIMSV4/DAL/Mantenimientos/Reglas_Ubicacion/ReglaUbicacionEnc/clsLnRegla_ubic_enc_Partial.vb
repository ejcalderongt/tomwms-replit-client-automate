Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_enc
    Implements IDisposable

    Public Shared Function Listar(idBodega As Integer, idEmpresa As Integer, ByVal pActivo As Boolean) As DataTable
        Try

            Dim sp As String = "SELECT IdReglaUbicacionEnc AS Código,Nombre FROM regla_ubic_enc " &
                "WHERE (IdBodega=" & idBodega & ") AND (IdEmpresa=" & idEmpresa & ") "

            If pActivo Then
                sp &= " AND (activo=1) "
            Else
                sp &= " AND (activo=0) "
            End If

            sp &= " ORDER BY Nombre"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dtt As New DataTable
            dad.Fill(dtt)

            Return dtt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetNombre(ByVal IdRegla As Integer) As String

        Try

            Dim sp As String = "SELECT Nombre FROM Regla_ubic_enc Where IdReglaUbicacionEnc = " & idRegla
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows(0).Item(0)

        Catch ex As Exception
            Throw ex
            Return ""
        End Try

    End Function

    Public Shared Function GetSingleWithDetails(ByRef pBeRegla_ubic_enc As clsBeRegla_ubic_enc)

        Try

            Const sp As String = "SELECT * FROM Regla_ubic_enc" &
            " Where(IdReglaUbicacionEnc = @IdReglaUbicacionEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAUBICACIONENC", pBeRegla_ubic_enc.IDREGLAUBICACIONENC))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Cargar(pBeRegla_ubic_enc, dt.Rows(0))

                pBeRegla_ubic_enc.IsNew = False

                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_Prop = clsLnRegla_ubic_det_prop.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)
                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_Ir = clsLnRegla_ubic_det_ir.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)
                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_Tr = clsLnRegla_ubic_det_tr.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)
                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_Pe = clsLnRegla_ubic_det_pe.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)
                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_tp = clsLnRegla_ubic_det_tp.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)
                pBeRegla_ubic_enc.listDetRegla_Ubic_Det_pp = clsLnRegla_ubic_det_pp.GetAllByIdReglaUbicacionEnc(pBeRegla_ubic_enc.IdReglaUbicacionEnc)

            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0


            Const sp As String = "SELECT ISNULL(Max(IdReglaUbicacionEnc),0) FROM Regla_ubic_enc"

            Using lCommand As New SqlCommand(sp, pConnection)
                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
                lCommand.Dispose()
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20170726_03:45AM
    Public Shared Function Guardar_Regla(ByVal pReglaEnc As clsBeRegla_ubic_enc) As Boolean

        Guardar_Regla = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            'Guarda/Actualiza encabezado de la regla.
            If pReglaEnc.IsNew Then
                'Ensure the Id is unic in the transacction.
                'pReglaEnc.IdReglaUbicacionEnc = MaxID(lConnection,lTransaction)
                Insertar(pReglaEnc, lConnection, lTransaction)
            Else
                Actualizar(pReglaEnc, lConnection, lTransaction)
            End If

            Dim lMaxID As Integer = -1

            'Insert/update the detail of the rule by owner 
            If Not pReglaEnc.listDetRegla_Ubic_Det_Prop Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_prop.MaxID(lConnection, lTransaction)

                For Each Prop As clsBeRegla_ubic_det_prop In pReglaEnc.listDetRegla_Ubic_Det_Prop

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    Prop.IdReglaUbicacionEnc = preglaenc.IdReglaUbicacionEnc

                    If Prop.IsNew Then
                        lMaxID += 1
                        Prop.IdReglaUbicacionDetProp = lMaxID
                        clsLnRegla_ubic_det_prop.Insertar(Prop, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_prop.Actualizar(Prop, lConnection, lTransaction)
                    End If

                Next

            End If

            'Insert/update the detail of the rule by type of rotation 
            If Not pReglaEnc.listDetRegla_Ubic_Det_Tr Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_tr.MaxID(lConnection, lTransaction)

                For Each TR As clsBeRegla_ubic_det_tr In pReglaEnc.listDetRegla_Ubic_Det_Tr

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    TR.IdReglaUbicacionEnc = preglaenc.IdReglaUbicacionEnc

                    If TR.IsNew Then
                        lMaxID += 1
                        TR.IdREglaUbicacionDetTr = lMaxID
                        clsLnRegla_ubic_det_tr.Insertar(TR, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_tr.Actualizar(TR, lConnection, lTransaction)
                    End If

                Next

            End If

            'Insert/update the detail of the rule by indice of rotation 
            If Not pReglaEnc.listDetRegla_Ubic_Det_Ir Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_ir.MaxID(lConnection, lTransaction)

                For Each Ir As clsBeRegla_ubic_det_ir In pReglaEnc.listDetRegla_Ubic_Det_Ir

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    Ir.IdReglaUbicacionEnc = preglaenc.IdReglaUbicacionEnc

                    If Ir.IsNew Then
                        lMaxID += 1
                        Ir.IdReglaUbicacionDetIr = lMaxID
                        clsLnRegla_ubic_det_ir.Insertar(Ir, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_ir.Actualizar(Ir, lConnection, lTransaction)
                    End If

                Next

            End If

            'Insert/update the detail of the rule by product state 
            If Not pReglaEnc.listDetRegla_Ubic_Det_Pe Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_pe.MaxID(lConnection, lTransaction)

                For Each Pe As clsBeRegla_ubic_det_pe In pReglaEnc.listDetRegla_Ubic_Det_Pe

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    Pe.IdReglaUbicacionEnc = pReglaEnc.IdReglaUbicacionEnc

                    If Pe.IsNew Then
                        lMaxID += 1
                        Pe.IdReglaUbicacionDetPe = lMaxID
                        clsLnRegla_ubic_det_pe.Insertar(Pe, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_pe.Actualizar(Pe, lConnection, lTransaction)
                    End If

                Next

            End If

            'Insert/update the detail of the rule by product type
            If Not pReglaEnc.listDetRegla_Ubic_Det_tp Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_tp.MaxID(lConnection, lTransaction)

                For Each tp As clsBeRegla_ubic_det_tp In pReglaEnc.listDetRegla_Ubic_Det_tp

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    tp.IdReglaUbicacionEnc = pReglaEnc.IdReglaUbicacionEnc

                    If tp.IsNew Then
                        lMaxID += 1
                        tp.IdReglaUbicacoinTP = lMaxID
                        clsLnRegla_ubic_det_tp.Insertar(tp, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_tp.Actualizar(tp, lConnection, lTransaction)
                    End If

                Next

            End If

            'Insert/update the detail of the rule by presentation
            If Not pReglaEnc.listDetRegla_Ubic_Det_pp Is Nothing Then

                lMaxID = clsLnRegla_ubic_det_pp.MaxID(lConnection, lTransaction)

                For Each pp As clsBeRegla_ubic_det_pp In pReglaEnc.listDetRegla_Ubic_Det_pp

                    'If the Id change while MaxId() is calculated the list is begin updated with the new Id.
                    pp.IdReglaUbicacionEnc = pReglaEnc.IdReglaUbicacionEnc

                    If pp.IsNew Then
                        lMaxID += 1
                        pp.IdReglaUbicacionDetPP = lMaxID
                        clsLnRegla_ubic_det_pp.Insertar(pp, lConnection, lTransaction)
                    Else
                        clsLnRegla_ubic_det_pp.Actualizar(pp, lConnection, lTransaction)
                    End If

                Next

            End If

            lTransaction.Commit()

            Guardar_Regla = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
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
