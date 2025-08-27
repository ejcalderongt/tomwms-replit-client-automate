Imports System.Data.SqlClient

Partial Public Class clsLnTipo_actualizacion_costo

    Public Shared Function GetSingle(ByVal pIdTipoActualizacionCosto As Integer) As clsBeTipo_actualizacion_costo

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM tipo_actualizacion_costo WHERE IdTipoActualizacionCosto=@IdTipoActualizacionCosto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoActualizacionCosto", pIdTipoActualizacionCosto)

                    Dim lDT As New DataTable()

                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTipo_actualizacion_costo()

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

    Public Shared Function GetAll() As List(Of clsBeTipo_actualizacion_costo)

        Dim lReturnList As New List(Of clsBeTipo_actualizacion_costo)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Dim vSQL As String = "SELECT * FROM tipo_actualizacion_costo "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTipo_actualizacion_costo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTipo_actualizacion_costo

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

    Public Shared Function GetParametro(ByVal pIdPropietario As Integer,
                                        ByVal pIdProveedor As Integer) As clsBeTipo_actualizacion_costo

        GetParametro = Nothing

        Try

            Dim vSQL As String = "SELECT IdPropietario FROM propietario_bodega WHERE IdPropietarioBodega=@IdPropietarioBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietario)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjPB As New clsBePropietario_bodega()

                            ObjPB.IdPropietario = CType(lRow("IdPropietario"), Integer)

                            Dim BePropietario As New clsBePropietarios
                            BePropietario.IdPropietario = ObjPB.IdPropietario

                            Dim BeProveedor As New clsBeProveedor
                            BeProveedor.IdProveedor = pIdProveedor

                            clsLnPropietarios.GetSingle(BePropietario.IdPropietario, lConnection, lTransaction)
                            BeProveedor = clsLnProveedor.GetSingle(BeProveedor.IdProveedor, lConnection, lTransaction)

                            Dim BeTipoActualizacionCosto As New clsBeTipo_actualizacion_costo
                            BeTipoActualizacionCosto.ObjPropietario = BePropietario
                            BeTipoActualizacionCosto.ObjProveedor = BeProveedor

                            Return BeTipoActualizacionCosto

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            '#EJC20210403:No lanzar excepción, controla a través del resultado.
            'Throw ex
        End Try

    End Function

End Class
