Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_producto

    Public Shared Function Exists(ByVal pNo As String, ByRef Cnn As SqlConnection, ByRef lTrans As SqlTransaction) As Boolean

        Exists = False

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_producto WHERE No= @No "

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@No", pNo)
                lDTA.SelectCommand.Transaction = lTrans

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Exists = lDT.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeI_nav_producto)
        Dim cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Dim lReturnList As New List(Of clsBeI_nav_producto)
            Const sp As String = "SELECT * FROM I_nav_producto;"

            Using cmd As New SqlCommand(sp, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60 ' ajusta si aplica

                Using dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    dad.Fill(dt)

                    For Each dr As DataRow In dt.Rows
                        Dim vBeI_nav_producto As New clsBeI_nav_producto()
                        Cargar(vBeI_nav_producto, dr)
                        lReturnList.Add(vBeI_nav_producto)

                        ' Sin transacción: pasar Nothing
                        vBeI_nav_producto.lINavConversion =
                            clsLnI_nav_conversion.Get_All_By_Codigo_Producto(vBeI_nav_producto.No, cn, Nothing)
                    Next
                End Using
            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Function

    Public Shared Function GetAll(ByRef lConnection As SqlConnection, ByRef lTrans As SqlTransaction) As List(Of clsBeI_nav_producto)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_producto)
            Const sp As String = "SELECT * FROM I_nav_producto "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_producto As New clsBeI_nav_producto

            For Each dr As DataRow In dt.Rows

                vBeI_nav_producto = New clsBeI_nav_producto
                Cargar(vBeI_nav_producto, dr)
                lReturnList.Add(vBeI_nav_producto)
                vBeI_nav_producto.lINavConversion = clsLnI_nav_conversion.Get_All_By_Codigo_Producto(vBeI_nav_producto.No,
                                                                                                     lConnection,
                                                                                                     lTrans)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class