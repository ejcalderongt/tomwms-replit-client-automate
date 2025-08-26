Imports System.Data.SqlClient

Partial Public Class clsLnMotivo_ubicacion
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Dim lMax As Integer = 0

        Dim vSQL As String = "SELECT ISNULL(Max(idMotivoUbicacion),0) FROM motivo_ubicacion"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
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

    Public Shared Function GetSingle(ByVal IdMotivoUbicacion As Integer) As clsBeMotivo_ubicacion

        Try

            Dim vSQL As String = "SELECT * FROM motivo_ubicacion WHERE IdMotivoUbicacion=@IdMotivoUbicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlDataApadter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoUbicacion", IdMotivoUbicacion)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeMotivo_ubicacion()
                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using
            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeMotivo_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeMotivo_ubicacion)


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM motivo_ubicacion WHERE 1 > 0"

                If pActivo Then
                    vSQL += " AND activo = 1 "
                Else
                    vSQL += " AND activo = 0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_ubicacion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_ubicacion()
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

    Public Shared Function Get_Motivos_Ubicacion() As List(Of clsBeMotivo_ubicacion)

        Get_Motivos_Ubicacion = Nothing

        Try

            Dim lMotivoUbicacion As New List(Of clsBeMotivo_ubicacion)
            Dim lnMotivoUbicacion As New clsLnMotivo_ubicacion
            Dim vBeMotivoUbicacion As New clsBeMotivo_ubicacion

            Dim DT As New DataTable
            DT = lnMotivoUbicacion.Listar()

            For Each pm As DataRow In DT.Rows

                vBeMotivoUbicacion = New clsBeMotivo_ubicacion
                clsLnMotivo_ubicacion.Cargar(vBeMotivoUbicacion, pm)
                lMotivoUbicacion.Add(vBeMotivoUbicacion)

            Next

            DT.Dispose()

            Return lMotivoUbicacion

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
